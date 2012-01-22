using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace NecroNet.Toolkit.Mvc.Html
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString ContentOrDifferentContent(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter, Func<MvcHtmlString> differentContentGetter)
		{
			return condition ? contentGetter() : differentContentGetter();
		}

		public static MvcHtmlString ContentOrText(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter, string text)
		{
			return helper.ContentOrDifferentContent(condition, contentGetter, () => MvcHtmlString.Create(text));
		}

		public static MvcHtmlString ContentOrNothing(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter)
		{
			return helper.ContentOrDifferentContent(condition, contentGetter, () => MvcHtmlString.Empty);
		}

		public static HelperResult ContentOrDifferentContent(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content, Func<dynamic, HelperResult> differentContent)
		{
			return condition ? content(null) : differentContent(null);
		}

		public static HelperResult ContentOrText(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content, string text)
		{
			return condition ? content(null) : new HelperResult(w => w.Write(text));
		}

		public static HelperResult ContentOrNothing(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content)
		{
			return condition ? content(null) : new HelperResult(w => {});
		}

		public static MvcHtmlString WrapIn(this HtmlHelper helper, string tagName, MvcHtmlString what, object wrapperHtmlAttributes = null)
		{
			var tag = new TagBuilder(tagName);
			if(wrapperHtmlAttributes != null)
			{
				tag.MergeAttributes(new RouteValueDictionary(wrapperHtmlAttributes));
			}

			tag.InnerHtml = what.ToString();

			return MvcHtmlString.Create(tag.ToString());
		}
	}
}