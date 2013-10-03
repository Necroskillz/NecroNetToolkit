using System.Linq;
using System.Collections.Generic;
using System;

namespace NecroNet.Toolkit.Mvc
{
	public enum SelectListOptimization
	{
		/// <summary>
		/// Do not optimize (default).
		/// </summary>
		None,
		/// <summary>
		/// If there are two items in the result select list, and one of them is null line, delete the null line.
		/// </summary>
		DeleteNullLineIfSelectListHasOneItemAndNullLine = 0x01,
		/// <summary>
		/// If there are two items in the result select list, and one of them is null line, set the item after null line as selected.
		/// </summary>
		PreselectIfSelectListHasOneItemAndNullLine = 0x02,
		/// <summary>
		/// Always set the first item after null line as selected.
		/// </summary>
		AlwaysPreselectFirstItem = 0x04
	}
}