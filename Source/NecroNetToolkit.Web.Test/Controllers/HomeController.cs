﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Caching;
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

		public HomeController(ICityRepository cityRepository, IRepository<City> repository)
		{
			_cityRepository = cityRepository;
			_repository = repository;
		}

		public ActionResult Index()
		{
			//var city = _cityRepository.Get(c => c.ID == 1);


			List<int> list = null;

			if(list == null)
			{
				Throw.New<InvalidOperationException>();
			}

			list.Add(1);
			
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
