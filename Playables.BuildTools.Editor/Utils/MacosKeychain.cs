using System.Collections;
using UnityEngine;

public class MacosKeychain
{

	public static string GetKeychainPassword(string accountName,string serviceName)
	{
		var value = ShellHelper.Bash($"security find-generic-password -a {accountName} -s {serviceName} -w -g");
		return value.TrimEnd( '\r', '\n' );
	}
}