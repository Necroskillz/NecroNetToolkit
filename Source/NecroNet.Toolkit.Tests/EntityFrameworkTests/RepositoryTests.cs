using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NecroNet.Toolkit.Data;
using NecroNet.Toolkit.Tests.Fakes;
using NecroNet.Toolkit.Tests.Helpers;
using NUnit.Framework;
using PagedList;

namespace NecroNet.Toolkit.Tests.EntityFrameworkTests
{
	[TestFixture]
	public class RepositoryTests
	{
		private static class TestParameters
		{
			public const int TestDataCount = 20;
		}

		private ITestPersonRepository _personRepository;
		private List<Person> _testData;

		private int _nextId = 1;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			_personRepository = new TestPersonRepository();
			_testData = new List<Person>();

			for (int i = 1; i <= TestParameters.TestDataCount; i++)
			{
				_testData.Add(CreateTestPerson());
			}
		}

		[SetUp]
		public void SetUp()
		{
			var contextFieldInfo = typeof (TestPersonRepository).GetField("_objectContext", BindingFlags.NonPublic | BindingFlags.Instance);
			var peopleFieldInfo = typeof(FakeObjectContext).GetField("_people", BindingFlags.NonPublic | BindingFlags.Instance);
			var context = new FakeObjectContext();
			var people = new FakeObjectSet<Person>(_testData);
			
			peopleFieldInfo.SetValue(context, people);
			contextFieldInfo.SetValue(_personRepository, context);
		}

		private Person CreateTestPerson()
		{
			int id = _nextId++;
			return new Person
			       	{
			       		Id = id,
						Firstname = "Name" + id,
						Surname = "Surname" + id,
						Age = id * 5
			       	};
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_ShouldThrowWhenInitializingWithoutEntitySetNameAttribute()
		{
			Assert.That(() =>
			            	{
			            		var repository = new BrokenRepositoryNoSetNameAttribute();
			            		var obj = repository.Get(p => p.Id == 1);
			            	}, Throws.InvalidOperationException);
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_ShouldThrowWhenInitializingWithNonExistingEntitySetName()
		{
			Assert.That(() =>
			            	{
			            		var repository = new BrokenRepositoryInvalidEntitySetName();
								var obj = repository.Get(p => p.Id == 1);
			            	}, Throws.InvalidOperationException);
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_Get_ShouldReturnSinglePerson()
		{
			Person person = _personRepository.Get(p => p.Id == 1);
			
			Assert.That(person, Is.Not.Null);
			Assert.That(person.Id, Is.EqualTo(1));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_Add_ShouldAddPersonToTheRepository()
		{
			var person = CreateTestPerson();

			_personRepository.Add(person);

			Person addedPerson = _personRepository.Get(p => p.Id == person.Id);

			Assert.That(addedPerson, Is.SameAs(person));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_RemoveWithEntityParameter_ShouldRemovePersonFromTheRepository()
		{
			Person person = _personRepository.Get(p => p.Id == 1);
			_personRepository.Remove(person);
			person = _personRepository.Get(p => p.Id == 1);

			Assert.That(person, Is.Null);
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_RemoveWithExpressionParameter_ShouldRemovePersonFromTheRepository()
		{
			_personRepository.Remove(p => p.Id == 1);
			Person person = _personRepository.Get(p => p.Id == 1);

			Assert.That(person, Is.Null);
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_Count_ShouldReturnCountOfAllPeopleInTheRepository()
		{
			int count = _personRepository.Count();

			Assert.That(count, Is.EqualTo(TestParameters.TestDataCount));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_CountWithExpressionParameter_ShouldReturnCountOfPeopleThatSatisfySpecifiedCondition()
		{
			int count = _personRepository.Count(p => p.Id < 5);

			Assert.That(count, Is.EqualTo(4));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_RemoveRange_ShoudlRemovePeopleThatSatisfySpecifiedCondition()
		{
			_personRepository.RemoveRange(p => p.Id < 5);

			Assert.That(_personRepository.Count(), Is.EqualTo(16));
			Assert.That(_personRepository.Count(p => p.Id < 5), Is.EqualTo(0));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerable_ShouldReturnAllPeopleAsIEnumerableOfPerson()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable();

			Assert.That(people, Is.Not.Null);
			Assert.That(people.Count(), Is.EqualTo(TestParameters.TestDataCount));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerableWithExpressionPredicateParameter_ShouldReturnPeopleThatSatisfySpecifiedCondition()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable(p => p.Id < 5);

			Assert.That(people.Count(), Is.EqualTo(4));

			foreach (var person in people)
			{
				Assert.That(person.Id, Is.LessThan(5));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerableWithKeySelectorParameterAscending_ShouldReturnPeopleInAscedingOrder()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable(p => p.Firstname);

			Assert.That(people, Is.Ordered.By("Firstname"));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerableWithKeySelectorParameterDescending_ShouldReturnPeopleInDescendingOrder()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable(p => p.Firstname, ascending: false);

			Assert.That(people, Is.Ordered.By("Firstname").Descending);
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerableWithPredicateAndKeySelectorParameterAscending_ShouldReturnPeopleThatSatisfySpecifiedConditionInAscedingOrder()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable(p => p.Id < 5, p => p.Firstname);

			Assert.That(people.Count(), Is.EqualTo(4));
			Assert.That(people, Is.Ordered.By("Firstname"));

			foreach(var person in people)
			{
				Assert.That(person.Id, Is.LessThan(5));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetEnumerableWithPredicateAndKeySelectorParameterDescending_ShouldReturnPeopleThatSatisfySpecifiedConditionIDescendingOrder()
		{
			IEnumerable<Person> people = _personRepository.GetEnumerable(p => p.Id < 5, p => p.Firstname, ascending: false);

			Assert.That(people.Count(), Is.EqualTo(4));
			Assert.That(people, Is.Ordered.By("Firstname").Descending);

			foreach(var person in people)
			{
				Assert.That(person.Id, Is.LessThan(5));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetList_ShouldReturnIListWithAllPeopleInTheRepository()
		{
			IList<Person> people = _personRepository.GetList();

			Assert.That(people, Is.Not.Null);
			Assert.That(people, Has.Count.EqualTo(TestParameters.TestDataCount));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetListWithExpressionParameter_ShouldReturnPeopleThatSatisfySpecifiedCondition()
		{
			IList<Person> people = _personRepository.GetList(p => p.Id < 5);

			Assert.That(people.Count(), Is.EqualTo(4));

			foreach(var person in people)
			{
				Assert.That(person.Id, Is.LessThan(5));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetPage_ShouldReturnPageOfPeople()
		{
			IPagedList<Person> people = _personRepository.GetPage(2, 5, p => p.Id);

			Assert.That(people, Has.Count.EqualTo(5));
			Assert.That(people.PageNumber, Is.EqualTo(2));

			int id = 6;
			foreach(var person in people)
			{
				Assert.That(person.Id, Is.EqualTo(id++));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetPagedListWithExpressionParameter_ShoudReturnPageOfPeopleThatSatisfySpecifiedCondition()
		{
			IPagedList<Person> people = _personRepository.GetPage(p => p.Id < 10, 2, 3, p=>p.Id);

			Assert.That(people, Has.Count.EqualTo(3));
			Assert.That(people.PageNumber, Is.EqualTo(2));

			int id = 4;
			foreach(var person in people)
			{
				Assert.That(person.Id, Is.EqualTo(id++));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetSortedPagedListAscending_ShouldReturnSortedPageOfPeopleAscending()
		{
			ISortedPagedList<Person> people = _personRepository.GetSortedPagedList(3, 5, "Firstname", SortDirection.Asc);

			Assert.That(people, Has.Count.EqualTo(5));
			Assert.That(people, Is.Ordered.By("Firstname"));
			Assert.That(people.PageNumber, Is.EqualTo(3));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetSortedPagedListDescending_ShouldReturnSortedPageOfPeopleDescending()
		{
			ISortedPagedList<Person> people = _personRepository.GetSortedPagedList(3, 5, "Firstname", SortDirection.Desc);

			Assert.That(people, Has.Count.EqualTo(5));
			Assert.That(people, Is.Ordered.By("Firstname").Descending);
			Assert.That(people.PageNumber, Is.EqualTo(3));
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetSortedPagedListWithExpressionParameterAscending_ShouldReturnSortedPageOfPeopleThatSatisfySpecifiedConditionAscending()
		{
			ISortedPagedList<Person> people = _personRepository.GetSortedPagedList(p => p.Id < 10, 2, 5, "Firstname", SortDirection.Asc);

			Assert.That(people, Has.Count.EqualTo(4));
			Assert.That(people, Is.Ordered.By("Firstname"));
			Assert.That(people.PageNumber, Is.EqualTo(2));
			Assert.That(people.IsLastPage, Is.True);

			foreach(var person in people)
			{
				Assert.That(person.Id, Is.LessThan(10));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_GetSortedPagedListWithExpressionParameterDescending_ShouldReturnSortedPageOfPeopleThatSatisfySpecifiedConditionDescending()
		{
			ISortedPagedList<Person> people = _personRepository.GetSortedPagedList(p => p.Id < 10, 2, 5, "Firstname", SortDirection.Desc);

			Assert.That(people, Has.Count.EqualTo(4));
			Assert.That(people, Is.Ordered.By("Firstname").Descending);
			Assert.That(people.PageNumber, Is.EqualTo(2));
			Assert.That(people.IsLastPage, Is.True);

			foreach(var person in people)
			{
				Assert.That(person.Id, Is.LessThan(10));
			}
		}

		[Test]
		public void DescendantOfUltimateEntityRepositoryBaseOfPerson_IncludeWithExpressionParameter_ShouldCorrectlyEncodePropertyPath()
		{
			_personRepository.WithInclude(p => p.Cat.Color).WithInclude(p => p.Firstname);
			var key = typeof(UltimateRepositoryBase<>).MakeGenericType(new[] { typeof(Person) }).GetField("QueryConfigKey", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(_personRepository);
			var queryConfig = Local.Data.Get<QueryConfig>(key);

			var include1 = queryConfig.Includes[0];
			var include2 = queryConfig.Includes[1];

			Assert.That(include1.ToString(), Is.EqualTo(((Expression<Func<Person, string>>)(p => p.Cat.Color)).ToString()));
			Assert.That(include2.ToString(), Is.EqualTo(((Expression<Func<Person, string>>)(p => p.Firstname)).ToString()));
		}
	}
}
