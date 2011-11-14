using System.Configuration;

namespace NecroNet.Toolkit.Configuration
{
	internal class NecroNetToolkitUnitOfWorkConfigurationElement : ConfigurationElement
	{
		[ConfigurationProperty("transaction", IsRequired = false)]
		public TransactionElement Transaction
		{
			get { return (TransactionElement)this["transaction"]; }
			set { this["transaction"] = value; }
		}
	}
}