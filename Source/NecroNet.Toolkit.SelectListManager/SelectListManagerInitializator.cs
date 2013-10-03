using System;
using System.Collections.Generic;
using System.Linq;
using NecroNet.Toolkit.Mvc.Resources;

namespace NecroNet.Toolkit.Mvc
{
	internal class SelectListManagerInitializator : ISelectListManagerInitializator
	{
		private readonly SelectListManagerConfiguration _configuration = SelectListManagerConfiguration.Default;
		private readonly SelectListManagerRegistry _registry = new SelectListManagerRegistry();

		public int CacheDuration { set { _configuration.CacheDuration = value; } }
		public string DefaultNullLineText { set { _configuration.DefaultNullLineText = value; } }

		public void SetCacheProviderType(Type type)
		{
			if (!type.GetInterfaces().Any(t => t == typeof (ISelectListManagerCacheProvider)))
			{
				throw new InvalidOperationException(Res.ExceptionMessage_WrongCacheProviderType);
			}

			_configuration.CacheProviderType = type;
		}

		public void SetDataProviderFactory(Func<ISelectListManagerDataProvider> type)
		{
			_configuration.DataProviderFactory = type;
		}

		public ISelectListManagerRegistry Registry
		{
			get
			{
				return _registry;
			}
		}

		internal SelectListManagerConfiguration GetConfiguration()
		{
			if (_configuration.DataProviderFactory == null)
			{
				throw new InvalidOperationException(Res.ExceptionMessage_DataProviderTypeNotSet);
			}

			return _configuration;
		}

		internal SelectListManagerRegistry GetRegistry()
		{
			return _registry;
		}
	}
}