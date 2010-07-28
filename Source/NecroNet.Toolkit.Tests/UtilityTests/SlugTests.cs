using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NecroNet.Toolkit.Tests.UtilityTests
{
	[TestFixture]
	public class SlugTests
	{
		private const string Title = "Some testing title";

		[Test]
		public void Slug_Generate_ShouldGenerateLowercaseUrlSlugSeparatedByDashes()
		{
			var slug = Slug.Generate(Title);

			Assert.That(slug, Is.EqualTo("some-testing-title"));
		}

		[Test]
		public void Slug_GenerateWithIdParameter_ShouldPrependIdToTheSlug()
		{
			var slug = Slug.Generate(Title, 1);

			Assert.That(slug, Is.EqualTo("1-some-testing-title"));
		}
	}
}
