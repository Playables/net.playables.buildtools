
internal static class CommandlineParseUtils
{
	public static bool HasArgument(string[] args,string arg)
	{
		return System.Array.IndexOf (args, arg) != -1;
	}

	public static string GetArgumentData(string[] args, string searchArg)
	{
		if(!HasArgument(args,searchArg))
			return null;

		int argId = System.Array.IndexOf (args, searchArg);
		if(argId >= args.Length-1) // last one (no data)
			return null;

		string dataStr = args [argId + 1];

		if (dataStr.StartsWith ("-")) // next one is not data (starts with "-")
			return null;

		return dataStr;
	}
}