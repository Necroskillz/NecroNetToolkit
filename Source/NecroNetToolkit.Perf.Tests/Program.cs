using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using EnsureThat;
using Kent.Boogaart.HelperTrinity.Extensions;

namespace NecroNetToolkit.Perf.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = File.ReadAllText("x.txt");
			var watch = new Stopwatch();
			watch.Start();

			//if (string.IsNullOrEmpty(input))
			//{
			//    throw new ArgumentException("sss");
			//}

			//Guard.IsNotNullOrEmpty(input, "input");

			//Guard.Throw(g => g.If(input).IsNullOrEmpty());

			//Ensure.That(input).IsNotNullOrEmpty();

			//input.AssertNotNullOrEmpty("input");

			//Guard.IsNotNullOrEmpty(input, "input");

			Guard.Require(!string.IsNullOrEmpty(input), () => new ArgumentException("sss"));

			watch.Stop();

			Dump("standard if", watch);
		}

		private static void Dump(string label, Stopwatch watch)
		{
			Console.WriteLine("Results for test [" + label + "]");
			Console.WriteLine(watch.ElapsedMilliseconds + " msec | " + watch.ElapsedTicks + " ticks");
		}
	}

	public static class Guard
	{
		public static void Require(bool condition, Func<Exception> exceptionFactory)
		{
			if(!condition)
			{
				throw exceptionFactory();
			}
		}
	}

	//public static class Guard
	//{
	//    /// <summary>
	//    /// Checks if value of object is null. If it is, throws <see cref="ArgumentException"/>.
	//    /// </summary>
	//    /// <param name="value">The object to check</param>
	//    /// <param name="parameterName">Name to identify the parameter</param>
	//    /// <param name="exceptionMessage">Message to pass to the exception</param>
	//    public static void IsNotNull(object value, string parameterName, string exceptionMessage = "Value cannot be null.")
	//    {
	//        if (value == null)
	//        {
	//            throw new ArgumentNullException(parameterName, exceptionMessage);
	//        }
	//    }

	//    /// <summary>
	//    /// Checks if string is null or empty. If it is, throws <see cref="ArgumentException"/>.
	//    /// </summary>
	//    /// <param name="value">The string to check</param>
	//    /// <param name="parameterName">Name to identify the parameter</param>
	//    /// <param name="exceptionMessage">Message to pass to the exception</param>
	//    public static void IsNotNullOrEmpty(string value, string parameterName, string exceptionMessage = "String cannot be null or empty.")
	//    {
	//        GetValue(value, parameterName, exceptionMessage);
	//    }

	//    private static void GetValue(string value, string parameterName, string exceptionMessage)
	//    {
	//        X.Y(value,parameterName,exceptionMessage);
	//    }

	//    /// <summary>
	//    /// Checks if value of object is null. If it is, throws <see cref="Http404Exception"/>.
	//    /// </summary>
	//    /// <param name="value">The object to check</param>
	//    public static void IsNot404(object value)
	//    {
	//        if (value == null)
	//        {
	//            throw new Http404Exception();
	//        }
	//    }
	//}

	//public static class X
	//{
	//    public static void Y(string value, string parameterName, string exceptionMessage)
	//    {
	//        if (string.IsNullOrEmpty(value))
	//        {
	//            throw new ArgumentNullException(parameterName, exceptionMessage);
	//        }
	//    }
	//}
}
