using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NecroNet.Toolkit.Mvc.Html
{
	public static class LabelExtensions
	{
		/// <summary>
		/// Generates simple label (&lt;label&gt; withour 'for' attribute)
		/// </summary>
		public static MvcHtmlString SimpleLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

			var text = metadata.DisplayName ?? metadata.PropertyName;

			return helper.SimpleLabel(text);
		}

		/// <summary>
		/// Generates simple label (&lt;label&gt; withour 'for' attribute)
		/// </summary>
		public static MvcHtmlString SimpleLabel(this HtmlHelper helper, string text)
		{
			var label = new TagBuilder("label");
			label.SetInnerText(text);
			return MvcHtmlString.Create(label.ToString());
		}
	}
}
