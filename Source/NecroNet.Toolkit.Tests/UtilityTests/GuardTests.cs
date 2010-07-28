using System;
using NecroNet.Toolkit.Mvc;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class GuardTests
	{
		private readonly object _nullObj = null;
		private readonly object _obj = new object();
		private readonly string _nullString = null;
		private const string String = "Hello";
		private const string ExceptionMessage = "msg";

		[Test]
		public void Guard_IsNotNull_ShouldThrowIfValueIsNull()
		{
			Assert.That(() => Guard.IsNotNull(_nullObj, "_nullObj"), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Guard_IsNotNullWithMessageParameter_ShouldThrowExeptionWithSpecifiedMessage()
		{
			Assert.That(() => Guard.IsNotNull(_nullObj, "_nullObj", ExceptionMessage), Throws.InstanceOf<ArgumentNullException>().With.Message.StartsWith("msg"));
		}

		[Test]
		public void Guard_IsNotNull_ShoudNotThrowWhenValueIsNotNull()
		{
			Guard.IsNotNull(_obj, "_obj");
		}

		[Test]
		public void Guard_IsNotNullOrEmpty_ShouldThrowIfStringIsNull()
		{
			Assert.That(() => Guard.IsNotNullOrEmpty(_nullString, "_nullString"), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Guard_IsNotNullOrEmpty_ShouldThrowIfStringIsEmpty()
		{
			Assert.That(() => Guard.IsNotNullOrEmpty(string.Empty, "string.Empty"), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Guard_IsNotNullOrEmptyWithMessageParameter_ShouldThrowExeptionWithSpecifiedMessage()
		{
			Assert.That(() => Guard.IsNotNullOrEmpty(_nullString, "_nullString", ExceptionMessage), Throws.InstanceOf<ArgumentNullException>().With.Message.StartsWith("msg"));
		}

		[Test]
		public void Guard_IsNotNullOrEmpty_ShoudNotThrowWhenValueIsNotNullAndNotEmpty()
		{
			Guard.IsNotNullOrEmpty(String, "String");
		}

		[Test]
		public void Guard_IsNot404_ShouldThrowIfValueIsNull()
		{
			Assert.That(() => Guard.IsNot404(_nullObj), Throws.InstanceOf<Http404Exception>());
		}

		[Test]
		public void Guard_IsNot404_ShoudNotThrowWhenValueIsNotNull()
		{
			Guard.IsNot404(_obj);
		}
	}
}
