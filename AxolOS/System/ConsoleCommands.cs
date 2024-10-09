using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxolOS.System
{
	public static class ConsoleCommands
	{
		public static void RunCommand(string command)
		{
			string[] words = command.Split(' ');
			if (words.Length > 0)
			{
				if (words[0] == "info")//Wypisujemy informacje o systemie
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					WriteMessage.WriteLogo();
					Console.WriteLine();
					Console.Write(WriteMessage.CenterText("AxolOS"));
					Console.Write(WriteMessage.CenterText(Kernel.Version));
					Console.Write(WriteMessage.CenterText("Created by Szymekk"));
					Console.Write(WriteMessage.CenterText("https://youtube.com/Szymekk"));
					Console.ForegroundColor = ConsoleColor.White;
				}
				else if (words[0] == "format")//Formatujemy dysk
				{
					if (Kernel.fs.Disks[0].Partitions.Count > 0)
					{
						Kernel.fs.Disks[0].DeletePartition(0);
					}
					Kernel.fs.Disks[0].Clear();
					Kernel.fs.Disks[0].CreatePartition((int)Kernel.fs.Disks[0].Size / (1024 * 1024));
					Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
					WriteMessage.WriteOK("Success!");
					WriteMessage.WriteWarn("AxolOS will reboot in 3 seconds!");
					Thread.Sleep(3000);
					Cosmos.System.Power.Reboot();
				}
				else if (words[0] == "space")//Wypisujemy ile jest wolnego miejsca na dysku
				{
					long free = Kernel.fs.GetAvailableFreeSpace(Kernel.Path);
					Console.WriteLine("Free space: " + free / 1024 + "KB");
				}
				else if (words[0] == "dir")//Wypisujemy wszystkie foldery i pliki w Kernel.Path
				{
					var Directories = Directory.GetDirectories(Kernel.Path);
					var Files = Directory.GetFiles(Kernel.Path);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Directories (" + Directories.Length + ")");
					Console.ForegroundColor = ConsoleColor.Gray;
					for (int i = 0; i < Directories.Length; i++)
					{
						Console.WriteLine(Directories[i]);
					}
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Files (" + Files.Length + ")");
					Console.ForegroundColor = ConsoleColor.Gray;
					for (int i = 0; i < Files.Length; i++)
					{
						Console.WriteLine(Files[i]);
					}
				}
				else if (words[0] == "echo")//Zapisujemy plik. np Tekst>Nazwa.txt
				{
					if (words.Length > 1)
					{
						string wholeString = "";
						for (int i = 1; i < words.Length; i++)
						{
							wholeString += words[i] + " ";
						}
						int pathIndex = wholeString.LastIndexOf('>');
						string text = wholeString.Substring(0, pathIndex);
						string path = wholeString.Substring(pathIndex + 1);
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						var file_stream = File.Create(path);
						file_stream.Close();
						File.WriteAllText(path, text);
					}
					else
						WriteMessage.WriteError("Invalid Syntax!");
				}
				else if (words[0] == "cat")//Wypisujemy zawartość pliku
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (File.Exists(path))
						{
							string text = File.ReadAllText(path);
							Console.ForegroundColor = ConsoleColor.Gray;
							Console.WriteLine(text);
						}
						else
							WriteMessage.WriteError("File " + path + " not found!");
					}
					else
						WriteMessage.WriteError("Invalid Syntax!");
				}
				else if (words[0] == "del")//Usuwamy plik
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (File.Exists(path))
						{
							File.Delete(path);
							WriteMessage.WriteOK("Deleted " + path + "!");
						}
						else
							WriteMessage.WriteError("File " + path + " not found!");
					}
					else
						WriteMessage.WriteError("Invalid Syntax!");
				}
				else if (words[0] == "mkdir")//Tworzymy folder
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						Directory.CreateDirectory(path);
					}
					else
						WriteMessage.WriteError("Invalid Syntax!");
				}
				else if (words[0] == "cd")//Zmieniamy Kernel.Path
				{
					if (words.Length > 1)
					{
						if (words[1] == "..")
						{
							if (Kernel.Path != @"0:\")
							{
								string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
								Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf(@"\") + 1);
								return;
							}
							else
								return;
						}
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path + @"\";
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (!path.EndsWith(@"\"))
							path += @"\";
						if (Directory.Exists(path))
							Kernel.Path = path;
						else
							WriteMessage.WriteError("Directory " + path + " not found!");
					}
					else
						Kernel.Path = @"0:\";
				}//W następnym tutorialu dodamy "Invalid command" itp
				else if (words[0] == "gui")
				{
					Boot.onBoot();
				}
			}
			else
			{
				WriteMessage.WriteError("Please enter a valid command!");
			}
		}
	}
}
