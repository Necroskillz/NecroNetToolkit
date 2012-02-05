using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using JetBrains.Annotations;
using NecroNet.Toolkit;
using NecroNet.Toolkit.Data;
using NecroNetToolkit.Web.Model;
using IsolationLevel = System.Transactions.IsolationLevel;

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

			using (var scope = _transactionFactory.BeginTransaction(isolationLevel:IsolationLevel.ReadUncommitted))
			{
				var c = _cityRepository.GetList();

				_cityRepository.Add(new City
										{
											Name = "blah",
											Slug = "blah"
										});


				_unitOfWorkManager.GetCurrent<AllDealsEntities>().Flush();

				((IObjectContextAdapter) _unitOfWorkManager.GetCurrent<AllDealsEntities>().Context.AsActual<AllDealsEntities>()).
					ObjectContext.ExecuteStoreCommand("exec uspIsoLvl");

				scope.Complete();
			}

			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
