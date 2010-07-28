using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit
{
	public static class Slug
	{
		public static string Generate(string text)
		{
			return text.ToSlug('-');
		}

		public static string Generate(string text, int id)
		{
			return string.Format("{1}-{0}", text.ToSlug('-'), id);
		}
	}
}
