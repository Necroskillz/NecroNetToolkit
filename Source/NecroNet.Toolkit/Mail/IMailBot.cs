using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace NecroNet.Toolkit.Mail
{
	public interface IMailBot : IDisposable
	{
		/// <summary>
		/// Event that fires when sending of a single email is completed. Fires once for every email.
		/// </summary>
		event EventHandler<EmailSendingCompletedEventArgs> SendingCompleted;

		/// <summary>
		/// Sends a custom email message.
		/// </summary>
		/// <param name="message">The message to send.</param>
		/// <param name="loadConfigValues">Whether to overwrite message attributes with values from web.config.</param>
		void SendMail(MailMessage message, bool loadConfigValues = false);

		/// <summary>
		/// Sends an email with html body and specified subject to a single recipient.
		/// Body is rendered by using asp.net mvc view and a model.
		/// </summary>
		/// <param name="to">The address of the recipient.</param>
		/// <param name="subject">The subject of the email message.</param>
		/// <param name="templateViewName">The name of partial view used to render email body.</param>
		/// <param name="model">The model for the specified view.</param>
		void SendHtmlMail(string to, string subject, string templateViewName, object model);

		/// <summary>
		/// Sends an email with html body and specified subject to a multiple recipients.
		/// Body is rendered by using asp.net mvc view and a model.
		/// </summary>
		/// <param name="to">The address of the recipient.</param>
		/// <param name="subject">The subject of the email message.</param>
		/// <param name="templateViewName">The name of partial view used to render email body.</param>
		/// <param name="model">The model for the specified view.</param>
		void SendMassHtmlMail(IEnumerable<string> to, string subject, string templateViewName, object model);

		/// <summary>
		/// Sends an email with text body and specified subject to a single recipient.
		/// </summary>
		/// <param name="to">The address of the recipient.</param>
		/// <param name="subject">The subject of the email message.</param>
		/// <param name="body">Text representing the email body.</param>
		void SendTextMail(string to, string subject, string body);

		/// <summary>
		/// Sends an email with text body and specified subject to a multiple recipients.
		/// </summary>
		/// <param name="to">The address of the recipient.</param>
		/// <param name="subject">The subject of the email message.</param>
		/// <param name="body">Text representing the email body.</param>
		void SendMassTextMail(IEnumerable<string> to, string subject, string body);
	}
}