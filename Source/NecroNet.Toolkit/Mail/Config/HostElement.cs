using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Mail
{
	public class HostElement : ConfigurationElement
	{
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
		public bool UseSsl
		{
			get { return (bool)this["useSsl"]; }
			set { this["useSsl"] = value; }
		}
	}
}
