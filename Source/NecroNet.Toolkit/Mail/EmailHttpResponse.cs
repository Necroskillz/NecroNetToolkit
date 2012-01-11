// This code is courtesy of https://github.com/andrewdavey/postal

using System.Web;

namespace NecroNet.Toolkit.Mail
{
	public class EmailHttpResponse : HttpResponseBase
	{
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return virtualPath;
		}
	}
}