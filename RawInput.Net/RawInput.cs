using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RawInput.Net
{
    public static class RawInput
    {
		static class Import
		{
			public const string Lib = "RawInput.dll";
		}

		[DllImport(Import.Lib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int InitialiseGamepads(IntPtr hwnd);
		[DllImport(Import.Lib, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr GetDevicePath(int index);
		[DllImport(Import.Lib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int PollDeviceChange();
		[DllImport(Import.Lib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int ProcessInput(IntPtr wParam, IntPtr lParam,
			out ushort button, out int x, out int y);
	}
}
