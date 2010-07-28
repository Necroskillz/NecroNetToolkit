using System;

namespace NecroNet.Toolkit.EntityFramework
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class EntitySetNameAttribute : Attribute
	{
		public EntitySetNameAttribute(string name)
		{
			EntitySetName = name;
		}

		public string EntitySetName { get; set; }
	}
}