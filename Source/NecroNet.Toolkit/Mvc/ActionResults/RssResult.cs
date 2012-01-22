using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace NecroNet.Toolkit.Mvc
{
	public class RssResult<T> : FileResult
	{
		/// <summary>
		/// Collection of items that the feed is based on.
		/// </summary>
		public IEnumerable<T> Items { get; set; }

		/// <summary>
		/// The title of the feed.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The description of the feed.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Delegate used to get the title of an item.
		/// </summary>
		public Func<T, string> GetItemTitle { get; set; }

		/// <summary>
		/// Delegate used to get the url (ID) of an item.
		/// </summary>
		public Func<T, UrlHelper, string> GetItemUrl { get; set; }

		/// <summary>
		/// Delegate used to get the content of an item.
		/// </summary>
		public Func<T, UrlHelper, string> GetItemContent { get; set; }

		/// <summary>
		/// Delegate used to get the summary of an item.
		/// </summary>
		public Func<T, UrlHelper, string> GetItemSummary { get; set; }

		/// <summary>
		/// Delegate used to get the date an item was published.
		/// </summary>
		public Func<T, DateTime> GetItemPusblished { get; set; }

		/// <summary>
		/// Delegate used to get the date an item was updated.
		/// </summary>
		public Func<T, DateTime> GetItemUpdated { get; set; }

		private RequestContext _requestContext;

		protected RssResult() : base("application/rss+xml") { }

		/// <summary>
		/// Creates an <see cref="ActionResult"/> that represents RSS feed.
		/// </summary>
		/// <param name="items">Collection of items that the feed is based on.</param>
		/// <param name="title">The title of the feed.</param>
		/// <param name="description">The description of the feed.</param>
		/// <param name="getItemTitle">Delegate used to get the title of an item.</param>
		/// <param name="getItemUrl">Delegate used to get the url (ID) of an item.</param>
		/// <param name="getItemContent">Delegate used to get the content of an item.</param>
		/// <param name="getItemPublished">Delegate used to get the date an item was published.</param>
		/// <param name="getItemSummary">Delegate used to get the summary of an item. If null, the content is used instead.</param>
		/// <param name="getItemUpdated">Delegate used to get the date an item was updated. If null, the publish date it used instead.</param>
		public RssResult(IEnumerable<T> items, string title, string description, Func<T, string> getItemTitle,
						 Func<T, UrlHelper, string> getItemUrl, Func<T, UrlHelper, string> getItemContent,
						 Func<T, DateTime> getItemPublished, Func<T, UrlHelper, string> getItemSummary = null,
						 Func<T, DateTime> getItemUpdated = null)
			: this()
		{
			Items = items;
			Title = title;
			Description = description;

			GetItemTitle = getItemTitle;
			GetItemUrl = getItemUrl;
			GetItemContent = getItemContent;
			GetItemPusblished = getItemPublished;
			GetItemSummary = getItemSummary ?? getItemContent;
			GetItemUpdated = getItemUpdated ?? getItemPublished;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			_requestContext = context.RequestContext;
			base.ExecuteResult(context);
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			var url = new UrlHelper(_requestContext);
			var items = from item in Items
						select new SyndicationItem(title: GetItemTitle(item),
												   content: GetItemContent(item, url),
												   itemAlternateLink: new Uri(GetItemUrl(item, url)),
												   id: GetItemUrl(item, url),
												   lastUpdatedTime: GetItemUpdated(item))
						{
							PublishDate = GetItemPusblished(item),
							Summary = new TextSyndicationContent(GetItemSummary(item, url), TextSyndicationContentKind.XHtml)
						};


			var feed = new SyndicationFeed(
				Title,
				Description,
				_requestContext.HttpContext.Request.Url,
				items);

			var formatter = new Rss20FeedFormatter(feed);

			using (var writer = XmlWriter.Create(response.Output))
			{
				formatter.WriteTo(writer);
			}
		}
	}
}