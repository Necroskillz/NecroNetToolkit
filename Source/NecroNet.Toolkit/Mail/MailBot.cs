using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using NecroNet.Toolkit.Configuration;
using RazorEngine;

namespace NecroNet.Toolkit.Mail
{
	public class MailBot : IMailBot
	{
		private static readonly NecroNetToolkitMailConfigurationElement _config;
		private int _queuedMails;
		private bool _disposed;

		private SmtpClient _smtpClient;
		private SmtpClient SmtpClient
		{
			get
			{
				if(_smtpClient == null)
				{
					_smtpClient = new SmtpClient();
					if(_config.Host.UseSsl)
					{
						_smtpClient.EnableSsl = true;
					}
				}

				return _smtpClient;
			}
		}

		static MailBot()
		{
			NecroNetToolkitConfigurationManager.EnsureConfig();

			_config = NecroNetToolkitConfigurationManager.Configuration.Mail;
			if(_config == null)
			{
				throw new ConfigurationErrorsException("Configuration element 'mail' in section 'necroNetToolkit' was not found.");
			}
		}

		public void Dispose()
		{
			if(_queuedMails == 0)
			{
				if(SmtpClient != null)
				{
					SmtpClient.Dispose();
				}
			}
			else
			{
				_disposed = true;
			}
		}

		public event EventHandler<EmailSendingCompletedEventArgs> SendingCompleted;

		public void RaiseSendingCompleted(bool success, IEnumerable<MailAddress> to, string subject, string body, Exception exception = null)
		{
			if(SendingCompleted != null)
			{
				SendingCompleted(this, new EmailSendingCompletedEventArgs(success, to, subject, body, exception));
			}
		}

		private static string RenderPartialToString(string controlName, object model)
		{
			var viewData = new ViewDataDictionary(model);
			var viewPage = new ViewPage { ViewData = viewData };
			var control = viewPage.LoadControl(controlName);

			viewPage.Controls.Add(control);

			var builder = new StringBuilder();
			using(var stringWriter = new StringWriter(builder))
			{
				using(var textWriter = new HtmlTextWriter(stringWriter))
				{
					viewPage.RenderControl(textWriter);
				}
			}

			return builder.ToString();
		}

		private static MailMessage PrepareMessage()
		{
			var message = new MailMessage();
			PopulateMessage(message);

			return message;
		}

		private static void PopulateMessage(MailMessage message)
		{
			var replyTo = _config.ReplyTo.MailAddress;
			var sender = _config.Sender.MailAddress;

			message.From = _config.From.MailAddress;
			message.SubjectEncoding = Encoding.GetEncoding(_config.Encoding.Subject);
			message.BodyEncoding = Encoding.GetEncoding(_config.Encoding.Body);

			if (replyTo != null)
			{
				message.ReplyToList.Add(replyTo);
			}
			if (sender != null)
			{
				message.Sender = sender;
			}
		}

		public void SendMail(MailMessage message, bool loadConfigValues = false)
		{
			if(loadConfigValues)
			{
				PopulateMessage(message);
			}

			SendAsync(message);
		}

		public void SendHtmlMail(string to, string subject, string templateView, object model)
		{
			SendMassHtmlMail(new[] { to }, subject, templateView, model);
		}

		public void SendMassHtmlMail(IEnumerable<string> to, string subject, string templateView, object model)
		{
			var message = PrepareMessage();
			message.Body = RenderPartialToString(templateView, model);
			message.IsBodyHtml = true;
			message.Subject = subject;
			
			foreach (var recipient in to)
			{
				message.To.Add(recipient);
			}

			SendAsync(message);
		}

		public void SendRazorMail<TModel>(string to, string subject, string templateViewServerPath, TModel model)
		{
			SendMassRazorMail(new[] { to }, subject, templateViewServerPath, model);
		}

		public void SendMassRazorMail<TModel>(IEnumerable<string> to, string subject, string templateViewServerPath, TModel model)
		{
			var message = PrepareMessage();
			message.Body = ParseRazorTemplate(templateViewServerPath, model);
			message.IsBodyHtml = true;
			message.Subject = subject;

			foreach (var recipient in to)
			{
				message.To.Add(recipient);
			}

			SendAsync(message);
		}

		private static string ParseRazorTemplate<TModel>(string templateViewServerPath, TModel model)
		{
			var template = File.ReadAllText(templateViewServerPath);
			template = Regex.Replace(template, "@model .*", string.Empty);

			return Razor.Parse(template, model);
		}

		public void SendTextMail(string to, string subject, string bodyTemplate, params object[] args)
		{
			SendTextMail(to, subject, string.Format(bodyTemplate, args));
		}

		public void SendTextMail(string to, string subject, string body)
		{
			SendMassTextMail(new[] { to }, subject, body);
		}

		public void SendMassTextMail(IEnumerable<string> to, string subject, string bodyTemplate, params object[] args)
		{
			SendMassTextMail(to, subject, string.Format(bodyTemplate, args));
		}

		public void SendMassTextMail(IEnumerable<string> to, string subject, string body)
		{
			var message = PrepareMessage();
			message.Body = body;
			message.Subject = subject;

			foreach(var recipient in to)
			{
				message.To.Add(recipient);
			}

			SendAsync(message);
		}

		private void SendAsync(MailMessage message)
		{
			_queuedMails++;
			Task.Factory.StartNew(() => SmtpClient.Send(message)).ContinueWith(t =>
																				{
																					_queuedMails--;
																					RaiseSendingCompleted(!t.IsFaulted, message.To, message.Subject, message.Body, t.Exception);
																					message.Dispose();
																					CheckDispose();
																				});
		}

		private void CheckDispose()
		{
			if(_queuedMails == 0 && _disposed)
			{
				if(SmtpClient != null)
				{
					SmtpClient.Dispose();
				}
			}
		}
	}
}
