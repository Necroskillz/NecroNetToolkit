using System;
using System.Collections.Generic;
using System.Linq;
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
			var model = new EmailModel();
			model.Message = "Hello there";

			IMailBot mailBot = new MailBot();

			mailBot.SendHtmlMail("necroskillz@gmail.com", "subj", "Email", model);

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
