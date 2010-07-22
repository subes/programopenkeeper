using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProgramOpenKeeper
{

	internal sealed class Program
	{

		private const int SLEEP_TIME = 1000;
		
		[STAThread]
		private static void Main(string[] args)
		{
			if(args.Length != 3){
				string title = "ProgramOpenKeeper.exe - by Edwin Stang";
				string message = "Usage:\n" +
					"\tProgramOpenKeeper.exe \"PROCESSNAME\" \"EXEPATH\" \"ARGUMENTS\"\n" +
					"Example:\n" +
					"\tProgramOpenKeeper.exe \"OUTLOOK\" \"C:\\Programme\\Microsoft Office\\OFFICE11\\OUTLOOK.EXE\" \"\"\n\n" +
					"This program checks if the specified process runs (PROCESSNAME without .exe)\n" +
					"and starts it from the specified EXEPATH with the specified ARGUMENTS minimized,\n" +
					"if it was not found.\nIt expects exactly 3 arguments.\n" +
					"To exit this app, close it via task manager.\n" +
					"\n" +
					"It was originally designed to keep Outlook open all the time,\n" +
					"even when clicking close accidentially.\n" +
					"So the time you miss meetings because if this should be over.\n" +
					"\n" +
					"Have fun! :)";
					
				MessageBox.Show(message, title);
				return;
			}
			
			string exe = args[1];
			string arguments = args[2];
			string processName = args[0];
			//string workingDir = "";
			
			while(true){
				Process[] processes = Process.GetProcessesByName(processName);
				
				if(processes.Length == 0){
					ProcessStartInfo startInfo = new ProcessStartInfo();
				    startInfo.FileName = exe;
				    startInfo.Arguments = arguments;
				    startInfo.ErrorDialog = true;
				    //startInfo.WorkingDirectory = workingDir;
				    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
				
				    // Create a new Process object
				    Process process = new Process();
				
				    // Assign the ProcessStartInfo to the Process
				    process.StartInfo = startInfo;
				
		            // Start the new process.
		            process.Start();
		            process.WaitForExit();
				}
				
				//Retry in a few seconds
				Thread.Sleep(SLEEP_TIME);
			}
		}
		
	}
}
