using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using NecroNet.Toolkit.Entity;
using NecroNet.Toolkit.Mvc;
using NecroNet.Toolkit.WebTest.Models;

namespace NecroNet.Toolkit.WebTest.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			using (var uow = new UnitOfWork())
			{
				ISelectListManager s = new SelectListManager();
				var items = s.GetItems<Test1>();

				items.Select(x => x);
			}
			return View();
		}
	}
}