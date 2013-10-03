using NecroNet.Toolkit.Mvc;

// Uncomment this if you want to use WebActivator, otherwise call SelectListManagerConfig.ConfigureSelectListManager from global.asax or use your favourite method (whatever it is).
//[assembly:PreApplicationStartMethod(typeof(SelectListManagerConfig), "ConfigureSelectListManager")]

namespace $rootnamespace$
{
	public class SelectListManagerConfig
	{
		public static void ConfigureSelectListManager()
		{
			SelectListManager.Initialize(c =>
			{
				// To start using SelectListManager, create a data provider class. It's a very simple class with only one method.
				// This class implements ISelectListDataProvider.
				// If you use DI, you might want to resolve the data provider using that.
				c.SetDataProviderFactory(() => new SelectListDataProvider());

				// Then there are a couple of other configuration options you can adjust. Everything has a default value, so this is optional.
				// c.CacheDuration = ...; // default is 1440 seconds
				// c.DefaultNullLineText = ...; // default is "--"
				// c.SetCacheProviderType(typeof(...)); // default implementation caches using ASP.NET cache

				// Final step of the configuration is to add the select list configurations. There is quite a few options you can set.
				// For more information, check the documentation: https://github.com/Necroskillz/NecroNetToolkit/wiki/SelectListManager
				// c.Registry.Add<Test1>(i => i.Name, i => i.Id);
				
				// Example usage:
				// ISelectListManager selectListManager // = new SelectListManager, or inject using DI
				// var items = selectListManager.GetItems<Test1>();
			});
		}
	}
}