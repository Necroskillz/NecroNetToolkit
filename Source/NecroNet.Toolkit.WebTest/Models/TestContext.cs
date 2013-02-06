using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace NecroNet.Toolkit.WebTest.Models
{
	public class TestContext : DbContext
	{
		public DbSet<Test1> Test1 { get; set; }
		public DbSet<Test2> Test2 { get; set; }
	}

	public class Test2
	{
		public int Id { get; set; }
		public Test1 Test1 { get; set; }
	}

	public class Test1
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}