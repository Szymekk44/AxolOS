using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxolOS.System
{
	public static class ProcessManager
	{
		public static List<Process> ProcessList = new List<Process>();
		public static void Update()
		{
			foreach (Process process in ProcessList)
			{
				process.Run();
			}
		}
		public static void Start(Process process)
		{
			ProcessList.Add(process);
			process.Start();
		}
		public static void Stop(Process process)
		{
			ProcessList.Remove(process);
		}
	}
}
