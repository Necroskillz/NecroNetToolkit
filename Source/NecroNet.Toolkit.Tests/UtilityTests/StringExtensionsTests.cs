using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class StringExtensionsTests
	{
		[Test]
		public void ToCsv_ShouldReturnDelimitedValues()
		{
			const string delimiter = "...";
			var test = new[] {"LOL", "ROFL", "1337"};

			var csv = test.ToCsv(delimiter);

			Assert.That(csv, Is.EqualTo("LOL...ROFL...1337"));
		}

		[Test]
		public void IsNumeric_ShouldReturnTrueIfInputIsStringThatRepresentsANumber()
		{
			const string integer = "5";
			const string floating = "5.6";
			const string negative = "-10";

			Assert.That(integer.IsNumeric());
			Assert.That(floating.IsNumeric());
			Assert.That(negative.IsNumeric());
		}

		[Test]
		public void IsNumeric_ShouldReturnFalseIfInputIsNotAStringRepresentingANumber()
		{
			const string nullString = null;
			const string literal = "bla";
			const string failInt = "6ss";

			Assert.That(nullString.IsNumeric(), Is.False);
			Assert.That(string.Empty.IsNumeric(), Is.False);
			Assert.That(literal.IsNumeric(), Is.False);
			Assert.That(failInt.IsNumeric(), Is.False);
		}
	}
}
