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
		public IEnumerable<T> Items { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public Func<T, string> GetItemTitle { get; set; }
		public Func<T, UrlHelper, string> GetItemUrl { get; set; }
		public Func<T, UrlHelper, string> GetItemContent { get; set; }
		public Func<T, UrlHelper, string> GetItemSummary { get; set; }
		public Func<T, DateTime> GetItemPusblished { get; set; }
		public Func<T, DateTime> GetItemUpdated { get; set; }

		private RequestContext _requestContext;

		protected RssResult() : base("application/rss+xml") { }

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