using System.Diagnostics;

public static class ShellHelper
{
	public static string Bash(string cmd)
	{
		var escapedArgs = cmd.Replace("\"", "\\\"");

		var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "/bin/bash",
				Arguments = $"-c \"{escapedArgs}\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
			}
		};
		process.Start();
		string result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		return result;
	}
	public static string Cmd(string cmd, string arg)
	{
		var escapedArgs = arg.Replace("\"", "\\\"");

		var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = cmd,
				Arguments = escapedArgs,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
			}
		};
		process.Start();
		string result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		return result;
	}
}