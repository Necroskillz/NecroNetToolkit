using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit
{
	public static class Slug
	{
		/// <summary>
		/// Generates a 'slug' from string. Slugs are used for nice web page url parameters. e.g: 'NecroNet Toolkit is awesome!!' becomes 'necronet-toolkit-is-awesome'.
		/// </summary>
		public static string Generate(string text)
		{
			return text.ToSlug('-');
		}

		/// <summary>
		/// Generates a 'slug' from string and appends specified id. Slugs are used for nice web page url parameters. e.g: 'NecroNet Toolkit is awesome!!' becomes 'necronet-toolkit-is-awesome-&lt;id&gt;'.
		/// </summary>
		public static string Generate(string text, int id)
		{
			return string.Format("{1}-{0}", text.ToSlug('-'), id);
		}
	}
}
