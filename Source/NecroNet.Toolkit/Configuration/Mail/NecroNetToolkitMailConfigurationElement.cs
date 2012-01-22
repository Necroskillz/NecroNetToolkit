using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Configuration
{
	internal class NecroNetToolkitMailConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("from", IsRequired = true)]
		public EmailAddressElement From
		{
			get { return (EmailAddressElement)this["from"]; }
			set { this["from"] = value; }
		}

		[ConfigurationProperty("sender", IsRequired = false)]
		public EmailAddressElement Sender
		{
			get { return (EmailAddressElement)this["sender"]; }
			set { this["sender"] = value; }
		}

		[ConfigurationProperty("replyTo", IsRequired = false)]
		public EmailAddressElement ReplyTo
		{
			get { return (EmailAddressElement)this["replyTo"]; }
			set { this["replyTo"] = value; }
		}

		[ConfigurationProperty("encoding", IsRequired = false)]
		public EncodingElement Encoding
		{
			get { return (EncodingElement)this["encoding"]; }
			set { this["encoding"] = value; }
		}

		[ConfigurationProperty("host", IsRequired = false)]
		public HostElement Host
		{
			get { return (HostElement)this["host"]; }
			set { this["host"] = value; }
		}

		[ConfigurationProperty("emailViewDirectory", IsRequired = false, DefaultValue = "Emails")]
		public string EmailDirectory
		{
			get { return (string)this["emailViewDirectory"]; }
			set { this["emailViewDirectory"] = value; }
		}
	}
}
