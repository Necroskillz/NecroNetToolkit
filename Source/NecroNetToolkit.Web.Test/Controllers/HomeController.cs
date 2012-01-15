using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using NecroNet.Toolkit.Data;
using NecroNet.Toolkit.Mail;
using NecroNetToolkit.Web.Model;
using NecroNetToolkit.Web.Test.Models;

namespace NecroNetToolkit.Web.Test.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICityRepository _cityRepository;
		private readonly IRepository<City> _repository; 

		public HomeController(ICityRepository cityRepository, IRepository<City> repository)
		{
			_cityRepository = cityRepository;
			_repository = repository;
		}

		public ActionResult Index()
		{
			//var model = new EmailModel();
			//model.Message = "Hello there";

			//IMailBot mailBot = new MailBot();

			//mailBot.SendHtmlMail("necroskillz@gmail.com", "subj", "Email", model);

			//using (var allDeals = new AllDealsEntities())
			//{
			//    using (var doplatky = new DoplatkyEntities())
			//    {
			//        using (var transaction = new TransactionScope())
			//        {
			//            allDeals.Categories.AddObject(new Category
			//                                            {
			//                                                Name = "XXX"
			//                                            });

			//            var d = doplatky.Drugs.FirstOrDefault();

			//            d.Code = "6546";

			//            doplatky.Deliveries.AddObject(new Delivery
			//                                            {
			//                                                Code = "654",
			//                                                Name = "Del1",
			//                                            });

			//            doplatky.SaveChanges();

			//            var deal = allDeals.ActualDeals.FirstOrDefault();

			//            deal.CharityDeal = 54.55m;

			//            allDeals.SaveChanges();

			//            transaction.Complete();
			//        }
			//    }
			//}


			//using (var doplatky = UnitOfWork.Start<DoplatkyEntities>())
			//{
			//    using (var allDeals = UnitOfWork.Start<AllDealsEntities>())
			//    {
			//        using (var dt = doplatky.BeginTransaction())
			//        {
			//            using (var at = allDeals.BeginTransaction())
			//            {
			//                doplatky.Context.AsActual<DoplatkyEntities>().ATCs.AddObject(new ATC {Code = "cc", Name = "ccc"});

			//                doplatky.Flush();

			//                allDeals.Flush();

			//                dt.Commit();

			//                at.Commit();
			//            }
			//        }
			//    }
			//}

			//using (var scope = UnitOfWork.Start<AllDealsEntities>())
			//{
			//    var r = new ActualDealRepository();
			//    r.Clear();
			//}
			using (UnitOfWork.Start<AllDealsEntities>())
			{
				_cityRepository.Get(c => c.ID == 1);
			}

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
