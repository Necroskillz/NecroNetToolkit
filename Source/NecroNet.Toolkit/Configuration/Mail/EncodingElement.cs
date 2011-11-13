using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Configuration
{
	internal class EncodingElement : ConfigurationElement
	{
		[ConfigurationProperty("subject", IsRequired = false, DefaultValue = "UTF-8")]
		public string Subject
		{
			get { return (string)this["subject"]; }
			set { this["subject"] = value; }
		}

		[ConfigurationProperty("body", IsRequired = false, DefaultValue = "UTF-8")]
		public string Body
		{
			get { return (string)this["body"]; }
			set { this["body"] = value; }
		}
	}
}
