using System;
using NecroNet.Toolkit.Mvc;

namespace NecroNet.Toolkit
{
	public static class Guard
	{
		/// <summary>
		/// Checks if value of object is null. If it is, throws <see cref="ArgumentException"/>.
		/// </summary>
		/// <param name="value">The object to check</param>
		/// <param name="parameterName">Name to identify the parameter</param>
		/// <param name="exceptionMessage">Message to pass to the exception</param>
		public static void IsNotNull(object value, string parameterName, string exceptionMessage = "Value cannot be null.")
		{
			if(value == null)
			{
				throw new ArgumentNullException(parameterName, exceptionMessage);
			}
		}

		/// <summary>
		/// Checks if string is null or empty. If it is, throws <see cref="ArgumentException"/>.
		/// </summary>
		/// <param name="value">The string to check</param>
		/// <param name="parameterName">Name to identify the parameter</param>
		/// <param name="exceptionMessage">Message to pass to the exception</param>
		public static void IsNotNullOrEmpty(string value, string parameterName, string exceptionMessage = "String cannot be null or empty.")
		{
			if(string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(parameterName, exceptionMessage);
			}
		}

		/// <summary>
		/// Checks if value of object is null. If it is, throws <see cref="Http404Exception"/>.
		/// </summary>
		/// <param name="value">The object to check</param>
		public static void IsNot404(object value)
		{
			if(value == null)
			{
				throw new Http404Exception();
			}
		}
	}
}