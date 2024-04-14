using AxolOS.System;
using Cosmos.System.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using AxolOS.Graphics;
using Cosmos.Core.Memory;

namespace AxolOS
{
	public class Kernel : Sys.Kernel
	{
		public static string Version = "1.0.0";
		public static string Path = @"0:\";
		public static CosmosVFS fs;
		public static bool RunGui;
		int lastHeapCollect;
		protected override void BeforeRun()
		{
			//Zmieniamy wielkość konsolki na 90 znaków X 30 linjek. Zmienia to też czcionke.
			Console.SetWindowSize(90, 30);
			//Dodajemy https://pl.wikipedia.org/wiki/CP437
			Console.OutputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
			//Tworzymy FileSystem
			fs = new Cosmos.System.FileSystem.CosmosVFS();
			Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Booting AxolOS " + Version);
			Console.ForegroundColor = ConsoleColor.White;
		}

		protected override void Run()
		{
			if(!RunGui)
			{
				Console.Write(Path + ">");
				var command = Console.ReadLine();
				ConsoleCommands.RunCommand(command);
				Console.ForegroundColor = ConsoleColor.White;
			}
			else
			{
				GUI.Update();
			}
			if(lastHeapCollect >= 20)//Czyści niepotrzebny ram, jednocześnie nie zabija fpsów ;p
			{
				Heap.Collect();
				lastHeapCollect = 0;
			}
			else
				lastHeapCollect++;
		}
	}
}
