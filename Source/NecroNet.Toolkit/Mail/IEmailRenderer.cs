// This code is courtesy of https://github.com/andrewdavey/postal

namespace NecroNet.Toolkit.Mail
{
	public interface IEmailRenderer
	{
		string Render(string viewName, object model);
	}
}