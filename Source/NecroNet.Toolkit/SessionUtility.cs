using System.Web;

namespace NecroNet.Toolkit
{
	public static class Session
	{
		private static ISessionUtility _sessionUtility = new SessionUtility();

		public static ISessionUtility Utility
		{
			get
			{
				return _sessionUtility;
			}
		}

		public static void ChangeContext(ISessionUtility utility)
		{
			_sessionUtility = utility;
		}

		internal class SessionUtility : ISessionUtility
		{
			public T GetValue<T>(string key)
			{
				return GetValue(key, default(T));
			}

			public T GetValue<T>(string key, T defaultValue)
			{
				var value = HttpContext.Current.Session[key];
				return value != null ? (T)value : defaultValue;
			}

			public void SetValue(string key, object value)
			{
				HttpContext.Current.Session[key] = value;
			}

			public void ClearValue(string key)
			{
				HttpContext.Current.Session.Remove(key);
			}
		}
	}
}
