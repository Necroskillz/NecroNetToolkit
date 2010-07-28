using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit.Tests.Helpers
{
	public static class MovieTestDataProvider
	{
		public static List<Movie> GetTestData()
		{
			return new List<Movie>
			       	{
			       		new Movie {Id = 1, Name = "The Bourne Identity", Director = "Doug Liman"},
			       		new Movie {Id = 2, Name = "The Bourne Supremacy", Director = "Paul Greengrass"},
			       		new Movie {Id = 3, Name = "The Bourne Ultimatum", Director = "Paul Greengrass"},
			       		new Movie {Id = 4, Name = "Identity", Director = "Jason Bourne"},
			       		new Movie {Id = 5, Name = "Kick-Ass", Director = "Matthew Vaughn"}
			       	};
		}
	}
}
