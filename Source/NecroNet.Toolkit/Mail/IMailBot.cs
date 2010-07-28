using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Mail
{
	public interface IMailBot : IDisposable
	{
		/// <summary>
		/// Event, that fires when sending of a single email is completed. Fires once for every email.
		/// </summary>
		event EventHandler<EmailSendingCompletedEventArgs> SendingCompleted;

		/// <summary>
		/// Sends an email with html body and specified subject to a single recipient.
		/// Body is rendered by using asp.net mvc view and a model. (some restrictions apply)
		/// </summary>
		/// <param name="to">Address of the recipient.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="templateView">Partial view (.ascx) used to render email body.</param>
		/// <param name="model">Model for the specified view.</param>
		void SendHtmlMail(string to, string subject, string templateView, object model);

		/// <summary>
		/// Sends an email with html body and specified subject to a multiple recipients.
		/// Body is rendered by using asp.net mvc view and a model. (some restrictions apply)
		/// </summary>
		/// <param name="to">Addresses of recipients.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="templateView">Partial view (.ascx) used to render email body.</param>
		/// <param name="model">Model for the specified view.</param>
		void SendMassHtmlMail(IEnumerable<string> to, string subject, string templateView, object model);

		/// <summary>
		/// Sends an email with text body and specified subject to a single recipient.
		/// </summary>
		/// <param name="to">Address of the recipient.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="body">Text representing the email body.</param>
		void SendTextMail(string to, string subject, string body);

		/// <summary>
		/// Sends an email with text body and specified subject to a single recipient.
		/// Body is rendered with string.Format(bodyTemplate, args) method.
		/// </summary>
		/// <param name="to">Address of the recipient.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="bodyTemplate">Text format for the email body.</param>
		/// <param name="args">Arguments for the email body format.</param>
		void SendTextMail(string to, string subject, string bodyTemplate, params object[] args);

		/// <summary>
		/// Sends an email with text body and specified subject to a multiple recipients.
		/// </summary>
		/// <param name="to">Addresses of recipients.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="body">Text representing the email body.</param>
		void SendMassTextMail(IEnumerable<string> to, string subject, string body);

		/// <summary>
		/// Sends an email with text body and specified subject to a multiple recipients.
		/// Body is rendered with string.Format(bodyTemplate, args) method.
		/// </summary>
		/// <param name="to">Addresses of recipients.</param>
		/// <param name="subject">Subject of the email message.</param>
		/// <param name="bodyTemplate">Text format for the email body.</param>
		/// <param name="args">Arguments for the email body format.</param>
		void SendMassTextMail(IEnumerable<string> to, string subject, string bodyTemplate, params object[] args);
	}
}