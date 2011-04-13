namespace NecroNet.Toolkit.Tests.Fakes
{
	public class Person
	{
		public int Id { get; set; }
		public string Firstname { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
		public Cat Cat { get; set; }

	}

	public class Cat
	{
		public string Color { get; set; }
	}
}