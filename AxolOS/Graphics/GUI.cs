using AxolOS.Apps;
using AxolOS.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;

namespace AxolOS.Graphics
{
	public static class GUI
	{
		public static int ScreenSizeX = 1920, ScreenSizeY = 1080;
		public static SVGAIICanvas MainCanvas;
		public static Bitmap Wallpaper, Cursor;
		public static Colors colors = new Colors();
		public static bool Clicked;
		public static Process currentProcess;
		public static int MX, MY;
		static int oldX, oldY;
		public static PCScreenFont FontDefault = PCScreenFont.Default;
		public static void Update()
		{
			MX = (int)MouseManager.X;
			MY = (int)MouseManager.Y;
			MainCanvas.DrawImage(Wallpaper, 0, 0);
			Move();
			ProcessManager.Update();
			MainCanvas.DrawImageAlpha(Cursor, (int)MouseManager.X, (int)MouseManager.Y);
			if (MouseManager.MouseState == MouseState.Left)
				Clicked = true;
			else if(MouseManager.MouseState == MouseState.None && Clicked)
			{
				Clicked = false;
				currentProcess = null;
			}
			MainCanvas.Display();
		}
		public static void Move()
		{
			if(currentProcess != null)
			{
				currentProcess.WindowData.WinPos.X = (int)MouseManager.X - oldX;

				currentProcess.WindowData.WinPos.Y = (int)MouseManager.Y - oldY;
			}
			else if(MouseManager.MouseState == MouseState.Left && !Clicked)
			{
				foreach (var proc in ProcessManager.ProcessList)
				{
					if (!proc.WindowData.MoveAble)
						continue;
					if(MX > proc.WindowData.WinPos.X && MX < proc.WindowData.WinPos.X + proc.WindowData.WinPos.Width)
					{
						if(MY > proc.WindowData.WinPos.Y && MY < proc.WindowData.WinPos.Y + Window.TopSize)
						{
							currentProcess = proc;
							oldX = MX - proc.WindowData.WinPos.X;
							oldY = MY - proc.WindowData.WinPos.Y;
						}
					}
				}
			}
		}
		public static void StartGUI()
		{
			MainCanvas = new SVGAIICanvas(new Mode((uint)ScreenSizeX, (uint)ScreenSizeY, ColorDepth.ColorDepth32));
			MouseManager.ScreenWidth = (uint)ScreenSizeX;
			MouseManager.ScreenHeight = (uint)ScreenSizeY;
			MouseManager.X = (uint)ScreenSizeX / 2;
			MouseManager.Y = (uint)ScreenSizeY / 2;
			ProcessManager.Start(new Messagebox { WindowData = new WindowData { WinPos = new Rectangle(100, 100, 350, 200) }, Name = "Hello World!" });
			ProcessManager.Start(new Messagebox { WindowData = new WindowData { WinPos = new Rectangle(100, 450, 350, 200) }, Name = "Szymekk" });
		}
	}
}
