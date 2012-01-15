using System;
using System.Configuration;
using System.Data;

namespace NecroNet.Toolkit.Configuration
{
	internal class TransactionElement : ConfigurationElement
	{
		[ConfigurationProperty("isolationLevel", IsRequired = false, DefaultValue = IsolationLevel.ReadCommitted)]
		public IsolationLevel IsolationLevel
		{
			get { return (IsolationLevel)this["isolationLevel"]; }
			set { this["isolationLevel"] = value; }
		}
	}
}