using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace NecroNet.Toolkit.Mail
{
	public class EmailSendingCompletedEventArgs : EventArgs
	{
		public bool Success { get; set; }
		public Exception Exception { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public IEnumerable<MailAddress> To { get; set; }

		public EmailSendingCompletedEventArgs(bool success, IEnumerable<MailAddress> to, string subject, string body, Exception exception = null)
		{
			To = to;
			Body = body;
			Subject = subject;
			Success = success;
			Exception = exception;
		}
	}
}
