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

		[ConfigurationProperty("unitOfWork", IsRequired = false)]
		public NecroNetToolkitUnitOfWorkConfigurationElement UnitOfWork
		{
			get { return (NecroNetToolkitUnitOfWorkConfigurationElement)this["unitOfWork"]; }
			set { this["unitOfWork"] = value; }
		}
	}
}