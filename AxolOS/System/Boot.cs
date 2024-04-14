using AxolOS.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.Graphics;

namespace AxolOS.System
{
	public static class Boot
	{
		public static void onBoot()
		{
			Kernel.RunGui = true;
			GUI.Wallpaper = new Bitmap(Resources.Files.AxolOSBackgroundRaw);
			GUI.Cursor = new Bitmap(Resources.Files.AxolOSCursorRaw);
			GUI.StartGUI();
		}
	}
}
