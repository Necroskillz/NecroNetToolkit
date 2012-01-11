using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using NecroNet.Toolkit.Mail;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.MailTests
{
	[TestFixture]
	public class MailBotTests
	{
		private const string EmailAddress = "someone@somewhere.com";
		private const string EmailAddress2 = "someone1@somewhere1.com";
		private const string EmailSubject = "Some subject";
		private const string EmailBody = "Some body";
		private const string MailDirectory = "C:\\TempMail";
		private const string BodyTemplate = "{0} {1}";
		private const string FirstParam = "foo";
		private const string SecondParam = "bar";
		private const string HtmlEmailBody = "<p>Some body<p>";
		private const string TemplateViewPath = "~/MailTests/EmailTemplate.ascx";

		private IMailBot _mailBot;

		private void SendMail(Action sendAction)
		{
			if (Directory.Exists(MailDirectory))
			{
				Directory.Delete(MailDirectory, true);
			}
			Directory.CreateDirectory(MailDirectory);

			var smtpClient = new SmtpClient();

			_mailBot = new MailBot();

			var smtpClientFieldInfo = typeof(MailBot).GetField("_smtpClient", BindingFlags.NonPublic | BindingFlags.Instance);
			smtpClientFieldInfo.SetValue(_mailBot, smtpClient);

			sendAction();

			var fieldInfo = typeof(MailBot).GetField("_queuedMails", BindingFlags.NonPublic | BindingFlags.Instance);
			while ((int)fieldInfo.GetValue(_mailBot) != 0)
			{
				Thread.Sleep(10);
			}
		}

		private static string GetCurrentMail()
		{
			return File.ReadAllText(Directory.EnumerateFiles(MailDirectory).First());
		}

		[Test]
		public void MailBot_SendTextMail_ShouldSendAnEmailWithSpecifiedBodyAndSubjectToSpecifiedAddress()
		{
			SendMail(() => _mailBot.SendTextMail(EmailAddress, EmailSubject, EmailBody));

			string mail = GetCurrentMail();

			Assert.That(mail, Is.StringContaining(EmailAddress));
			Assert.That(mail, Is.StringContaining(EmailSubject));
			Assert.That(mail, Is.StringContaining(Convert.ToBase64String(Encoding.UTF8.GetBytes(EmailBody))));
		}

		[Test]
		public void MailBot_SendMassTextMail_ShouldSendAnEmailWithSpecifiedBodyAndSubjectToMultipleSpecifiedAddresses()
		{
			SendMail(() => _mailBot.SendMassTextMail(new[] { EmailAddress, EmailAddress2 }, EmailSubject, EmailBody));

			string mail = GetCurrentMail();

			Assert.That(mail, Is.StringContaining(EmailAddress));
			Assert.That(mail, Is.StringContaining(EmailAddress2));
			Assert.That(mail, Is.StringContaining(EmailSubject));
			Assert.That(mail, Is.StringContaining(Convert.ToBase64String(Encoding.UTF8.GetBytes(EmailBody))));
		}
	}
}
