using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using NecroNet.Toolkit.Mail;
using NecroNetToolkit.Web.Test.Models;

namespace NecroNetToolkit.Web.Test.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			//var model = new EmailModel();
			//model.Message = "Hello there";

			//IMailBot mailBot = new MailBot();

			//mailBot.SendHtmlMail("necroskillz@gmail.com", "subj", "Email", model);

			using (var allDeals = new AllDealsEntities())
			{
				using (var doplatky = new DoplatkyEntities())
				{
					using (var transaction = new TransactionScope())
					{
						allDeals.Categories.AddObject(new Category
														{
															Name = "XXX"
														});

						var d = doplatky.Drugs.FirstOrDefault();

						d.Code = "6546";

						doplatky.Deliveries.AddObject(new Delivery
														{
															Code = "654",
															Name = "Del1",
														});

						doplatky.SaveChanges();

						var deal = allDeals.ActualDeals.FirstOrDefault();

						deal.CharityDeal = 54.55m;

						allDeals.SaveChanges();

						transaction.Complete();
					}
				}
			}

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
