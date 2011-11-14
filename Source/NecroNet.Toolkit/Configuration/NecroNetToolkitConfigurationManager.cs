using System;
using System.Configuration;

namespace NecroNet.Toolkit.Configuration
{
	internal static class NecroNetToolkitConfigurationManager
	{
		internal static NecroNetToolkitConfigurationSection Configuration { get; set; }

		static NecroNetToolkitConfigurationManager()
		{
			Configuration = ConfigurationManager.GetSection("necroNetToolkit") as NecroNetToolkitConfigurationSection;
		}

		public static void EnsureConfig()
		{
			if (Configuration == null)
			{
				throw new ConfigurationErrorsException("Configuration section 'necroNetToolkit' was not found.");
			}
		}
	}
}