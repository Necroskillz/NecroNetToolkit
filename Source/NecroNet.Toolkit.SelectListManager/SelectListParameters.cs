using System;
using System.Linq.Expressions;

namespace NecroNet.Toolkit.Mvc
{
	internal class SelectListParameters<T, TOrderByProperty>
	{
		public SelectListParameters(Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, TOrderByProperty> orderBySelector, Func<T, bool> predicate, bool nullLine, string nullLineText, SelectListOptimization optimization, bool noCache)
			: this(textSelector, valueSelector, predicate, nullLine, nullLineText, optimization, noCache)
		{
			OrderBySelector = orderBySelector;
		}

		public SelectListParameters(Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, bool> predicate, bool nullLine, string nullLineText, SelectListOptimization optimization, bool noCache)
		{
			TextSelector = textSelector;
			ValueSelector = valueSelector;
			Predicate = predicate;
			NullLine = nullLine;
			NullLineText = nullLineText;
			ForceNoCache = noCache;
		}

		public Func<T, string> TextSelector { get; set; }
		public Func<T, object> ValueSelector { get; set; }
		public Func<T, TOrderByProperty> OrderBySelector { get; set; }
		public Func<T, bool> Predicate { get; set; }
		public bool NullLine { get; set; }
		public bool ForceNoCache { get; set; }
		public string NullLineText { get; set; }
		public SelectListOptimization Optimization { get; set; }
	}
}