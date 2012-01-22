﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace NecroNet.Toolkit.Mvc.Html
{
	public static class HtmlHelperExtensions
	{
		/// <summary>
		/// Renders the content specified by <paramref name="contentGetter"/> if <paramref name="condition"/> is true; otherwise renders content specified by <paramref name="differentContentGetter"/>.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="contentGetter">The delegate to get content if <paramref name="condition"/> is true.</param>
		/// <param name="differentContentGetter">The delegate to get content if <paramref name="condition"/> is false.</param>
		public static MvcHtmlString ContentOrDifferentContent(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter, Func<MvcHtmlString> differentContentGetter)
		{
			return condition ? contentGetter() : differentContentGetter();
		}

		/// <summary>
		/// Renders the content specified by <paramref name="contentGetter"/> if <paramref name="condition"/> is true; otherwise renders the specified text.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="contentGetter">The delegate to get content if <paramref name="condition"/> is true.</param>
		/// <param name="text">Text to render if <paramref name="condition"/> is false.</param>
		public static MvcHtmlString ContentOrText(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter, string text)
		{
			return helper.ContentOrDifferentContent(condition, contentGetter, () => MvcHtmlString.Create(text));
		}

		/// <summary>
		/// Renders the content specified by <paramref name="contentGetter"/> if <paramref name="condition"/> is true; otherwise renders nothing.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="contentGetter">The delegate to get content if <paramref name="condition"/> is true.</param>
		public static MvcHtmlString ContentOrNothing(this HtmlHelper helper, bool condition, Func<MvcHtmlString> contentGetter)
		{
			return helper.ContentOrDifferentContent(condition, contentGetter, () => MvcHtmlString.Empty);
		}

		/// <summary>
		/// Renders the content specified by <paramref name="content"/> if <paramref name="condition"/> is true; otherwise renders content specified by <paramref name="differentContent"/>.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="content">Razor to render if <paramref name="condition"/> is true.</param>
		/// <param name="differentContent">Razor to render if <paramref name="condition"/> is false.</param>
		public static HelperResult ContentOrDifferentContent(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content, Func<dynamic, HelperResult> differentContent)
		{
			return condition ? content(null) : differentContent(null);
		}

		/// <summary>
		/// Renders the content specified by <paramref name="content"/> if <paramref name="condition"/> is true; otherwise renders the specified text.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="content">Razor to render if <paramref name="condition"/> is true.</param>
		/// <param name="text">Text to render if <paramref name="condition"/> is false.</param>
		public static HelperResult ContentOrText(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content, string text)
		{
			return condition ? content(null) : new HelperResult(w => w.Write(text));
		}


		/// <summary>
		/// Renders the content specified by <paramref name="content"/> if <paramref name="condition"/> is true; otherwise renders nothing.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">The condition used to decide what to render.</param>
		/// <param name="content">Razor to render if <paramref name="condition"/> is true.</param>
		public static HelperResult ContentOrNothing(this HtmlHelper helper, bool condition, Func<dynamic, HelperResult> content)
		{
			return condition ? content(null) : new HelperResult(w => {});
		}

		/// <summary>
		/// Wraps the specified <see cref="MvcHtmlString"/> in a html tag specified by <paramref name="tagName"/>. Allows to specify html attributes for the 'wrapper' tag. Renders nothing if specified <see cref="MvcHtmlString"/> is empty.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="tagName">The name of the wrapper tag.</param>
		/// <param name="what">What to wrap.</param>
		/// <param name="wrapperHtmlAttributes">Html attributes of the wrapper tag.</param>
		public static MvcHtmlString WrapIn(this HtmlHelper helper, string tagName, MvcHtmlString what, object wrapperHtmlAttributes = null)
		{
			var content = what.ToString();
			if(string.IsNullOrEmpty(content))
			{
				return MvcHtmlString.Empty;
			}

			var tag = new TagBuilder(tagName);
			if(wrapperHtmlAttributes != null)
			{
				tag.MergeAttributes(new RouteValueDictionary(wrapperHtmlAttributes));
			}

			tag.InnerHtml = content;

			return MvcHtmlString.Create(tag.ToString());
		}
	}
}