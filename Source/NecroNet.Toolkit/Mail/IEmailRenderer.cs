// This code is courtesy of https://github.com/andrewdavey/postal

namespace NecroNet.Toolkit.Mail
{
	internal interface IEmailRenderer
	{
		string Render(string viewName, object model);
	}
}