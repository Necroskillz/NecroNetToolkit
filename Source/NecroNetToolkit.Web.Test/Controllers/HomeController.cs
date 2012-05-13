using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NecroNet.Toolkit;
using NecroNet.Toolkit.Data;
using NecroNetToolkit.Web.Model;

namespace NecroNetToolkit.Web.Test.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICityRepository _cityRepository;
		private readonly IRepository<City> _repository;
		private readonly IUnitOfWorkManager _unitOfWorkManager;
		private readonly ITransactionFactory _transactionFactory;

		public HomeController(ICityRepository cityRepository, IRepository<City> repository, IUnitOfWorkManager unitOfWorkManager, ITransactionFactory transactionFactory)
		{
			_cityRepository = cityRepository;
			_repository = repository;
			_unitOfWorkManager = unitOfWorkManager;
			_transactionFactory = transactionFactory;
		}

		public ActionResult Index()
		{
			var city = _cityRepository.GetQueryable().FirstOrDefault(c => c.ID == 1);
			return View();
		}

		[HttpPost]
		public ActionResult Index(IndexViewModel model, int id, object bleh)
		{
			

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
