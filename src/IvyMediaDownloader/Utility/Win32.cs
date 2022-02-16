using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Invary.Utility
{
	class Win32
	{
		public const int WM_CLIPBOARDUPDATE = 0x31D;

		[DllImport("user32.dll", SetLastError = true)]
		public extern static void AddClipboardFormatListener(IntPtr hwnd);

		[DllImport("user32.dll", SetLastError = true)]
		public extern static void RemoveClipboardFormatListener(IntPtr hwnd);
	}
}
