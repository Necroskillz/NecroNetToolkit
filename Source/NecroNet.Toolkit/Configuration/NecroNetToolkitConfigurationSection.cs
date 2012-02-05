using System.Configuration;

namespace NecroNet.Toolkit.Configuration
{
	internal class NecroNetToolkitConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("mail", IsRequired = false)]
		public NecroNetToolkitMailConfigurationElement Mail
		{
			get { return (NecroNetToolkitMailConfigurationElement)this["mail"]; }
			set { this["mail"] = value; }
		}

		[ConfigurationProperty("transaction", IsRequired = false)]
		public NecroNetToolkitTransactionConfigurationElement Transaction
		{
			get { return (NecroNetToolkitTransactionConfigurationElement)this["transaction"]; }
			set { this["transaction"] = value; }
		}

		[ConfigurationProperty("selectListManager", IsRequired = false)]
		public NecroNetToolkitSelectListManagerConfigurationElement SelectListManager
		{
			get { return (NecroNetToolkitSelectListManagerConfigurationElement)this["selectListManager"]; }
			set { this["selectListManager"] = value; }
		}

		[ConfigurationProperty("http", IsRequired = false)]
		public NecroNetToolkitHttpConfigurationElement Http
		{
			get { return (NecroNetToolkitHttpConfigurationElement)this["http"]; }
			set { this["http"] = value; }
		}
	}
}