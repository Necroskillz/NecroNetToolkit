using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace NecroNet.Toolkit.Configuration
{
	internal class EmailAddressElement : ConfigurationElement
	{
		[ConfigurationProperty("address", IsRequired = true, DefaultValue = "null")]
		[RegexStringValidator(@"^(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(null)$")]
		public string Address
		{
			get { return (string)this["address"]; }
			set { this["address"] = value; }
		}

		[ConfigurationProperty("displayName", IsRequired = false, DefaultValue = null)]
		public string DisplayName
		{
			get { return (string)this["displayName"]; }
			set { this["displayName"] = value; }
		}

		[ConfigurationProperty("displayNameEncoding", IsRequired = false, DefaultValue = "UTF-8")]
		public string DisplayNameEncoding
		{
			get { return (string)this["displayNameEncoding"]; }
			set { this["displayNameEncoding"] = value; }
		}

		public MailAddress MailAddress
		{
			get
			{
				if(string.IsNullOrEmpty(Address) || Address.Equals("null")) return null;
				return new MailAddress(Address, DisplayName, Encoding.GetEncoding(DisplayNameEncoding));
			}
		}

	}
}
