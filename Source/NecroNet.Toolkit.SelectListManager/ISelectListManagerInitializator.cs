using System;
using System.Collections.Generic;
using System.Linq;

namespace NecroNet.Toolkit.Mvc
{
	public interface ISelectListManagerInitializator
	{
		int CacheDuration { set; }
		string DefaultNullLineText { set; }
		void SetCacheProviderType(Type type);
		void SetDataProviderFactory(Func<ISelectListManagerDataProvider> factory);
		ISelectListManagerRegistry Registry { get; }
	}
}