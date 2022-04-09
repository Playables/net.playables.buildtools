using UnityEditor.iOS.Xcode;

public class PlistKey<T> where T: PlistElement
{
	public string key;

	public static implicit operator PlistKey<T>(string keyString)
	{
		return new PlistKey<T>()
		{ 
			key = keyString
		};
	}
}