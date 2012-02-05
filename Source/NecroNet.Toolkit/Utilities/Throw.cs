using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using NecroNet.Toolkit.Internals;

namespace NecroNet.Toolkit
{
	public static class Throw
	{
		/// <summary>
		/// Throws an excepion of the specified type.
		/// </summary>
		/// <typeparam name="TException">The type of exception to throw.</typeparam>
		[JetBrains.Annotations.TerminatesProgram]
		public static void New<TException>() where TException: Exception, new()
		{
			throw new TException();
		}

		/// <summary>
		/// Throws an exception of the specified type with the specified formatted message.
		/// </summary>
		/// <typeparam name="TException">The type of exception to throw.</typeparam>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void New<TException>(string message, params object[] args) where TException : Exception, new()
		{
			var exception = new TException();
			var messageField = typeof (TException).GetField("_message", BindingFlags.NonPublic | BindingFlags.Instance);
			messageField.SetValue(exception, string.Format(CultureInfo.CurrentCulture, message, args));
			throw exception;
		}

		/// <summary>
		/// Throws an <see cref="ArgumentException"/>.
		/// </summary>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentException()
		{
			throw new ArgumentException();
		}

		/// <summary>
		/// Throws an <see cref="ArgumentException"/> with the specified formatted message.
		/// </summary>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentException(string message, params object[] args)
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args));
		}

		/// <summary>
		/// Throws an <see cref="ArgumentException"/> with the specified parameter name and the specified formatted message.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentException(string paramName, string message, params object[] args)
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args), paramName);
		}

		/// <summary>
		/// Throws an <see cref="ArgumentException"/> with parameter name parsed from the specified expression and the specified formatted message.
		/// </summary>
		/// <param name="paramNameSelector">An expression with path to the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentException(Expression<Func<object>> paramNameSelector, string message, params object[] args)
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args), paramNameSelector.GetPath());
		}

		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/>.
		/// </summary>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentNullException()
		{
			throw new ArgumentNullException();
		}

		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/> with the specified parameter name.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentNullException(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/> with the specified parameter name and the specified formatted message.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentNullException(string paramName, string message, params object[] args)
		{
			throw new ArgumentNullException(paramName, string.Format(CultureInfo.CurrentCulture, message, args));
		}

		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/> with parameter name parsed from the specified expression.
		/// </summary>
		/// <param name="paramNameSelector">An expression with path to the parameter that caused current exception.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentNullException(Expression<Func<object>> paramNameSelector)
		{
			throw new ArgumentNullException(paramNameSelector.GetPath());
		}

		/// <summary>
		/// Throws an <see cref="ArgumentNullException"/> with parameter name parsed from the specified expression and the specified formatted message.
		/// </summary>
		/// <param name="paramNameSelector">An expression with path to the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentNullException(Expression<Func<object>> paramNameSelector, string message, params object[] args)
		{
			throw new ArgumentNullException(paramNameSelector.GetPath(), string.Format(CultureInfo.CurrentCulture, message, args));
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/>
		/// </summary>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException()
		{
			throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> with the specified parameter name.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException(string paramName)
		{
			throw new ArgumentOutOfRangeException(paramName);
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> with the specified parameter name and the specified formatted message.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException(string paramName, string message, params object[] args)
		{
			throw new ArgumentOutOfRangeException(paramName, string.Format(CultureInfo.CurrentCulture, message, args));
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> with the specified parameter name, the specified actual value and the specified formatted message.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused current exception.</param>
		/// <param name="actualValue">The value of the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException(string paramName, object actualValue, string message, params object[] args)
		{
			throw new ArgumentOutOfRangeException(paramName, actualValue, string.Format(CultureInfo.CurrentCulture, message, args));
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> with parameter name parsed from the specified expression.
		/// </summary>
		/// <param name="paramNameSelector">An expression with path to the parameter that caused current exception.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException(Expression<Func<object>> paramNameSelector)
		{
			throw new ArgumentOutOfRangeException(paramNameSelector.GetPath());
		}

		/// <summary>
		/// Throws an <see cref="ArgumentOutOfRangeException"/> with parameter name and value parsed from the specified expression and the specified formatted message.
		/// </summary>
		/// <param name="paramSelector">An expression with path to the parameter that caused current exception.</param>
		/// <param name="message">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		[JetBrains.Annotations.TerminatesProgram]
		public static void NewArgumentOutOfRangeException(Expression<Func<object>> paramSelector, string message, params object[] args)
		{
			throw new ArgumentOutOfRangeException(paramSelector.GetPath(), paramSelector.Compile()(), string.Format(CultureInfo.CurrentCulture, message, args));
		}
	}
}