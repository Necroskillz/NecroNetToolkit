using System;
using System.Configuration;
using NecroNet.Toolkit.Resources;

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
			if(Configuration == null)
			{
				Throw.New<InvalidOperationException>(Res.ExceptionMessage_ConfigurationManager_SectionNotFound);
			}
		}

		public static T GetOption<T>(Func<NecroNetToolkitConfigurationSection, T> optionGetter, T defaultValue = default(T))
		{
			if (Configuration == null)
			{
				return defaultValue;
			}

			var value = optionGetter(Configuration);

			if(value == null)
			{
				return defaultValue;
			}

			return value;
		}
	}
}