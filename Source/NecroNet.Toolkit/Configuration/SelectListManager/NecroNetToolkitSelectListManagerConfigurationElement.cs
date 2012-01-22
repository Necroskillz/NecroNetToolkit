using System.Configuration;

namespace NecroNet.Toolkit.Configuration
{
	internal class NecroNetToolkitSelectListManagerConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("cacheDuration", IsRequired = true, DefaultValue = 1440)]
		public int CacheDuration
		{
			get { return (int)this["cacheDuration"]; }
			set { this["cacheDuration"] = value; }
		}
	}
}