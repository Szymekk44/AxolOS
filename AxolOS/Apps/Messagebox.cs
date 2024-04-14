using AxolOS.Graphics;
using AxolOS.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxolOS.Apps
{
	public class Messagebox : Process
	{
		public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
			GUI.MainCanvas.DrawFilledRectangle(GUI.colors.ColorMain, x, y + Window.TopSize, sizeX, sizeY-Window.TopSize);
		}
	}
}
