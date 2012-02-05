using System.Configuration;

namespace NecroNet.Toolkit.Configuration
{
	public class NecroNetToolkitHttpConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("defaultCookieExpiry", IsRequired = false, DefaultValue = 365)]
		public int DefaultCookieExpiry
		{
			get { return (int)this["defaultCookieExpiry"]; }
			set { this["defaultCookieExpiry"] = value; }
		}
	}
}