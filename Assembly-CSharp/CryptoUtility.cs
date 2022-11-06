using System;
using System.Security.Cryptography;
using System.Text;
using LitJson;
using UnityEngine;

// Token: 0x02000A32 RID: 2610
public class CryptoUtility
{
	// Token: 0x17000957 RID: 2391
	// (get) Token: 0x06004544 RID: 17732 RVA: 0x00163D4C File Offset: 0x00161F4C
	public static bool CryptoFlag
	{
		get
		{
			bool result = true;
			DebugGameObject instance = SingletonGameObject<DebugGameObject>.Instance;
			if (instance != null)
			{
				result = instance.crypt;
			}
			return result;
		}
	}

	// Token: 0x17000958 RID: 2392
	// (get) Token: 0x06004545 RID: 17733 RVA: 0x00163D78 File Offset: 0x00161F78
	// (set) Token: 0x06004546 RID: 17734 RVA: 0x00163F48 File Offset: 0x00162148
	public static string code
	{
		get
		{
			if (string.IsNullOrEmpty(CryptoUtility.CryptoCode))
			{
				for (int i = 0; i < 16; i++)
				{
					int num = (UnityEngine.Random.Range(0, 50) + i) % 18;
					if (num < 10)
					{
						CryptoUtility.CryptoCode += num;
					}
					else
					{
						switch (num % 12)
						{
						case 0:
							CryptoUtility.CryptoCode += "A";
							break;
						case 1:
							CryptoUtility.CryptoCode += "B";
							break;
						case 2:
							CryptoUtility.CryptoCode += "C";
							break;
						case 3:
							CryptoUtility.CryptoCode += "D";
							break;
						case 4:
							CryptoUtility.CryptoCode += "E";
							break;
						case 5:
							CryptoUtility.CryptoCode += "F";
							break;
						case 6:
							CryptoUtility.CryptoCode += "a";
							break;
						case 7:
							CryptoUtility.CryptoCode += "b";
							break;
						case 8:
							CryptoUtility.CryptoCode += "c";
							break;
						case 9:
							CryptoUtility.CryptoCode += "d";
							break;
						case 10:
							CryptoUtility.CryptoCode += "e";
							break;
						case 11:
							CryptoUtility.CryptoCode += "f";
							break;
						}
					}
				}
			}
			return CryptoUtility.CryptoCode;
		}
		set
		{
			CryptoUtility.CryptoCode = value;
		}
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x00163F50 File Offset: 0x00162150
	public static string Encrypt(string text)
	{
		AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
		aesCryptoServiceProvider.Mode = CipherMode.CBC;
		aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
		aesCryptoServiceProvider.BlockSize = 128;
		aesCryptoServiceProvider.KeySize = 128;
		aesCryptoServiceProvider.IV = Encoding.UTF8.GetBytes(CryptoUtility.code);
		aesCryptoServiceProvider.Key = Encoding.UTF8.GetBytes("vMdkkY8bfVmUS6qr");
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		string result;
		using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor())
		{
			byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			string text2 = Convert.ToBase64String(inArray);
			result = text2;
		}
		return result;
	}

	// Token: 0x06004548 RID: 17736 RVA: 0x00164014 File Offset: 0x00162214
	public static string Decrypt(string text)
	{
		AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
		aesCryptoServiceProvider.Mode = CipherMode.CBC;
		aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
		aesCryptoServiceProvider.BlockSize = 128;
		aesCryptoServiceProvider.KeySize = 128;
		aesCryptoServiceProvider.IV = Encoding.UTF8.GetBytes(CryptoUtility.code);
		aesCryptoServiceProvider.Key = Encoding.UTF8.GetBytes("vMdkkY8bfVmUS6qr");
		string cryptoCode = text.Substring(0, 16);
		byte[] array = Convert.FromBase64String(WWW.UnEscapeURL(text));
		string result;
		using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor())
		{
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			string @string = Encoding.UTF8.GetString(bytes);
			CryptoUtility.CryptoCode = cryptoCode;
			result = @string;
		}
		return result;
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x001640EC File Offset: 0x001622EC
	public static JsonData SRDecryptJson(JsonData json)
	{
		JsonData result = json;
		int jsonInt = NetUtil.GetJsonInt(json, "secure");
		if (jsonInt != 0)
		{
			string jsonString = NetUtil.GetJsonString(json, "key");
			if (!string.IsNullOrEmpty(jsonString))
			{
				string jsonString2 = NetUtil.GetJsonString(json, "param");
				CryptoUtility.code = jsonString;
				string json2 = CryptoUtility.Decrypt(jsonString2);
				result = JsonMapper.ToObject(json2);
			}
		}
		else
		{
			result = NetUtil.GetJsonObject(json, "param");
		}
		return result;
	}

	// Token: 0x040039DD RID: 14813
	private const string CryptoKey = "vMdkkY8bfVmUS6qr";

	// Token: 0x040039DE RID: 14814
	private static string CryptoCode = string.Empty;
}
