using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NecroNet.Toolkit.Mvc.Html
{
	public static class InputExtensions
	{
		public static MvcHtmlString RadioGroupFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items)
		{
			var property = ExpressionHelper.GetExpressionText(expression);
			var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(property);

			var div = new TagBuilder("div");
			div.MergeAttribute("id", name);

			var radio = new TagBuilder("input");
			radio.MergeAttribute("type", "radio");
			radio.MergeAttribute("name", name);

			var builder = new StringBuilder();

			foreach (var item in items)
			{
				radio.MergeAttribute("value", item.Value, replaceExisting: true);

				if (item.Selected)
				{
					radio.MergeAttribute("checked", "checked");
				}
				else
				{
					if (radio.Attributes.ContainsKey("checked"))
					{
						radio.Attributes.Remove("checked");
					}
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
			if (values == null)
			{
				return;
			}

			var output = helper.ViewContext.HttpContext.Response.Output;

			var properties = TypeDescriptor.GetProperties(values);
			foreach (PropertyDescriptor property in properties)
			{
				output.WriteLine(helper.Hidden(property.Name, property.GetValue(values)));
			}
		}

		public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
		{
			var propertyName = ExpressionHelper.GetExpressionText(expression);

			var input = new TagBuilder("input");
			input.MergeAttribute("id", helper.ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName));
			input.MergeAttribute("name", helper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName));
			input.MergeAttribute("value", value == null ? null : value.ToString());
			input.MergeAttribute("type", "text");
			input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			input.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(propertyName));

			return MvcHtmlString.Create(input.ToString());
		}

		public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes)
		{
			var propertyName = ExpressionHelper.GetExpressionText(expression);

			var input = new TagBuilder("input");
			input.MergeAttribute("id", helper.ViewData.TemplateInfo.GetFullHtmlFieldId(propertyName));
			input.MergeAttribute("name", helper.ViewData.TemplateInfo.GetFullHtmlFieldName(propertyName));
			input.MergeAttribute("value", value == null ? null : value.ToString());
			input.MergeAttribute("type", "hidden");
			input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			input.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(propertyName));

			return MvcHtmlString.Create(input.ToString());
		}

		public static MvcHtmlString Submit(this HtmlHelper helper, string text, object htmlAttributes)
		{
			var input = new TagBuilder("input");
			input.MergeAttribute("value", text);
			input.MergeAttribute("type", "submit");
			if (htmlAttributes != null)
			{
				input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			}

			return MvcHtmlString.Create(input.ToString());
		}

		public static MvcHtmlString Submit(this HtmlHelper helper, string text)
		{
			return helper.Submit(text, null);
		}
	}
}