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
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Submit(this HtmlHelper helper, string text, object htmlAttributes)
		{
			var input = new TagBuilder("input");
			input.MergeAttribute("value", text);
			input.MergeAttribute("type", "submit");
			if(htmlAttributes != null)
			{
				input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			}

			return MvcHtmlString.Create(input.ToString());
		}

		public static MvcHtmlString Submit(this HtmlHelper helper, string text)
		{
			return helper.Submit(text, null);
		}

		[Obsolete("Use standard Html.Raw instead.")]
		public static MvcHtmlString Markup(this HtmlHelper helper, string html)
		{
			return MvcHtmlString.Create(html);
		}

		public static MvcHtmlString SimpleLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
		{
			var property = ((MemberExpression)expression.Body).Member;
			var displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
			string text = displayNameAttribute == null ? property.Name : ((DisplayNameAttribute)displayNameAttribute).DisplayName;

			return helper.SimpleLabel(text);
		}

		public static MvcHtmlString SimpleLabel(this HtmlHelper helper, string text)
		{
			var label = new TagBuilder("label");
			label.SetInnerText(text);
			return MvcHtmlString.Create(label.ToString());
		}

		public static MvcHtmlString RadioGroupFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items)
		{
			var name = ((MemberExpression)expression.Body).Member.Name;
			var div = new TagBuilder("div");
			div.MergeAttribute("id", name);

			var radio = new TagBuilder("input");
			radio.MergeAttribute("type", "radio");
			radio.MergeAttribute("name", name);

			var builder = new StringBuilder();

			foreach(var item in items)
			{
				radio.MergeAttribute("value", item.Value, replaceExisting: true);

				if(item.Selected)
				{
					radio.MergeAttribute("checked", "checked");
				}

				builder.Append(radio.ToString());
				builder.Append(' ');
				builder.Append(item.Text);
			}

			div.InnerHtml = builder.ToString();

			return MvcHtmlString.Create(div.ToString());
		}

		public static void RenderHiddenFields(this HtmlHelper helper, object values)
		{
			if(values == null)
			{
				return;
			}

			var output = helper.ViewContext.HttpContext.Response.Output;

			var properties = TypeDescriptor.GetProperties(values);
			foreach(PropertyDescriptor property in properties)
			{
				output.WriteLine(helper.Hidden(property.Name, property.GetValue(values)));
			}
		}
	}
}
