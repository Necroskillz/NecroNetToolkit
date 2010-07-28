using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NecroNet.Toolkit
{
	public interface ISessionUtility
	{
		T GetValue<T>(string key);
		T GetValue<T>(string key, T defaultValue);
		void SetValue(string key, object value);
		void ClearValue(string key);
	}
}
