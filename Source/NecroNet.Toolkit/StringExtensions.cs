using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NecroNet.Toolkit
{
	public static class StringExtensions
	{
		public static string ToSlug(this string s, char delimiter)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}

			var builder = new StringBuilder();
			bool next = false;

			foreach (char c in s)
			{
				if (builder.Length == 0)
				{
					if (!char.IsLetterOrDigit(c))
					{
						continue;
					}
					builder.Append(char.ToLowerInvariant(c));
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						next = true;
						continue;
					}

					if (next)
					{
						builder.Append(delimiter);
						builder.Append(char.ToLowerInvariant(c));
						next = false;
					}
					else
					{
						builder.Append(char.ToLowerInvariant(c));
					}
				}
			}

			return builder.ToString();
		}

		public static string ToCsv(this IEnumerable<string> source, string delimiter)
		{
			var builder = new StringBuilder();

			foreach (string item in source)
			{
				builder.Append(item);
				builder.Append(delimiter);
			}

			if (builder.Length > 0)
			{
				builder.Remove(builder.Length - delimiter.Length, delimiter.Length);
			}

			return builder.ToString();
		}

		public static bool IsNumeric(this string expression)
		{
			double retNum;

			bool isNum = double.TryParse(expression, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
			return isNum;
		}
	}
}
