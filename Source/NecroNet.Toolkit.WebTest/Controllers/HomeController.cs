using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using NecroNet.Toolkit.Entity;
using NecroNet.Toolkit.WebTest.Models;

namespace NecroNet.Toolkit.WebTest.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			using (var uow = new UnitOfWork())
			{
				uow.GetRepository<Test1>().Get(1);

				uow.Save();
			}
			return View();
		}
	}
}