using System;

namespace NecroNet.Toolkit.Mvc
{
	/// <summary>
	/// Exception used to indicate that a resource was not found on the server and the reponse should be 404 - Not Found.
	/// </summary>
	public class Http404Exception : Exception
	{
	}
}