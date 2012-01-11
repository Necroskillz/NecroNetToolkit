using System;

namespace NecroNet.Toolkit.Internals
{
	internal static class ExceptionHelper
	{
		internal static void ThrowInvalidOp(string message, params object[] args)
		{
			throw new InvalidOperationException(string.Format(message, args));
		}
	}
}