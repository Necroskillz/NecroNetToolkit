using System;
using System.Configuration;
using System.Transactions;

namespace NecroNet.Toolkit.Configuration
{
	internal class NecroNetToolkitTransactionConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("isolationLevel", IsRequired = false, DefaultValue = IsolationLevel.ReadCommitted)]
		public IsolationLevel IsolationLevel
		{
			get { return (IsolationLevel)this["isolationLevel"]; }
			set { this["isolationLevel"] = value; }
		}

		[ConfigurationProperty("scopeOption", IsRequired = false, DefaultValue = TransactionScopeOption.Required)]
		public TransactionScopeOption ScopeOption
		{
			get { return (TransactionScopeOption)this["scopeOption"]; }
			set { this["scopeOption"] = value; }
		}

		[ConfigurationProperty("timeout", IsRequired = false, DefaultValue = 0)]
		public int Timeout
		{
			get { return (int)this["timeout"]; }
			set { this["timeout"] = value; }
		}
	}
}