using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using NecroNet.Toolkit.Configuration;

namespace NecroNet.Toolkit.Mail
{
	public class MailBot : IMailBot
	{
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
					if(NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.Host.UseSsl))
					{
						_smtpClient.EnableSsl = true;
					}
				}

				return _smtpClient;
			}
		}

		private readonly IEmailRenderer _emailRenderer;

		static MailBot()
		{
			NecroNetToolkitConfigurationManager.EnsureConfig();
		}

		public MailBot()
		{
			_emailRenderer = new EmailRenderer(ViewEngines.Engines, null);
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

		private void RaiseSendingCompleted(bool success, IEnumerable<MailAddress> to, string subject, string body, Exception exception = null)
		{
			if(SendingCompleted != null)
			{
				SendingCompleted(this, new EmailSendingCompletedEventArgs(success, to, subject, body, exception));
			}
		}

		private static MailMessage PrepareMessage()
		{
			var message = new MailMessage();
			PopulateMessage(message);

			return message;
		}

		private static void PopulateMessage(MailMessage message)
		{
			var replyTo = NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.ReplyTo.MailAddress);
			var sender = NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.Sender.MailAddress);

			message.From = NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.From.MailAddress);
			message.SubjectEncoding = Encoding.GetEncoding(NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.Encoding.Subject));
			message.BodyEncoding = Encoding.GetEncoding(NecroNetToolkitConfigurationManager.GetOption(c => c.Mail.Encoding.Body));

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

		public void SendHtmlMail(string to, string subject, string templateViewName, object model)
		{
			SendMassHtmlMail(new[] { to }, subject, templateViewName, model);
		}

		public void SendMassHtmlMail(IEnumerable<string> to, string subject, string templateViewName, object model)
		{
			var message = PrepareMessage();
			message.Body = _emailRenderer.Render(templateViewName, model);
			message.IsBodyHtml = true;
			message.Subject = subject;
			
			foreach (var recipient in to)
			{
				message.To.Add(recipient);
			}

			SendAsync(message);
		}

		public void SendTextMail(string to, string subject, string body)
		{
			SendMassTextMail(new[] { to }, subject, body);
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
			if(_queuedMails == 0 && _disposed && SmtpClient != null)
			{
				SmtpClient.Dispose();
			}
		}
	}
}
