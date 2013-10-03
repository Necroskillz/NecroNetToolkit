using System;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.Toolkit.Mvc
{
	internal class SelectListManagerConfiguration
	{
		public string DefaultNullLineText { get; set; }
		public int CacheDuration { get; set; }
		public Type CacheProviderType { get; set; }
		public Func<ISelectListManagerDataProvider> DataProviderFactory { get; set; }

		public static SelectListManagerConfiguration Default
		{
			get
			{
				return new SelectListManagerConfiguration
					   {
						   CacheDuration = 1440,
						   DefaultNullLineText = "--",
						   CacheProviderType = typeof(AspNetCacheProvider)
					   };
			}
		}
	}
}