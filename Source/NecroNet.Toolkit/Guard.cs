using System;
using NecroNet.Toolkit.Mvc;

namespace NecroNet.Toolkit
{
	public static class Guard
	{
		public static void IsNotNull(object value, string parameterName, string exceptionMessage)
		{
			if(value == null)
			{
				throw new ArgumentNullException(parameterName, exceptionMessage);
			}
		}

		public static void IsNotNull(object value, string parameterName)
		{
			IsNotNull(value, parameterName, "Value cannot be null.");
		}

		public static void IsNotNullOrEmpty(string value, string parameterName, string exceptionMessage)
		{
			if(string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, exceptionMessage);
			}
		}

		public static void IsNotNullOrEmpty(string value, string parameterName)
		{
			IsNotNullOrEmpty(value, parameterName, "String cannot be null or empty.");
		}

		public static void IsNot404(object value)
		{
			if(value == null)
			{
				throw new Http404Exception();
			}
		}
	}
}