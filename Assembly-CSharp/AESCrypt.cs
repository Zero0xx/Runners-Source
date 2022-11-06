using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000A2C RID: 2604
public class AESCrypt
{
	// Token: 0x0600450D RID: 17677 RVA: 0x00163380 File Offset: 0x00161580
	public static bool HaveKey()
	{
		string ky = AESCryptKey.GetKY();
		return !ky.Equals(string.Empty);
	}

	// Token: 0x0600450E RID: 17678 RVA: 0x001633A4 File Offset: 0x001615A4
	private static RijndaelManaged _init(ref byte[] kyb, ref byte[] ivb)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		rijndaelManaged.Padding = AESCrypt.mPaddingMode;
		rijndaelManaged.Mode = AESCrypt.mCipherMode;
		rijndaelManaged.KeySize = AESCrypt.mKeySize;
		rijndaelManaged.BlockSize = AESCrypt.mBlockSize;
		string ky = AESCryptKey.GetKY();
		string iv = AESCryptKey.GetIV();
		kyb = Encoding.UTF8.GetBytes(ky);
		ivb = Encoding.UTF8.GetBytes(iv);
		return rijndaelManaged;
	}

	// Token: 0x0600450F RID: 17679 RVA: 0x0016340C File Offset: 0x0016160C
	public static string Encrypt(int iDeInt)
	{
		return AESCrypt.Encrypt(iDeInt.ToString());
	}

	// Token: 0x06004510 RID: 17680 RVA: 0x0016341C File Offset: 0x0016161C
	public static string Encrypt(float iDeFloat)
	{
		return AESCrypt.Encrypt(iDeFloat.ToString());
	}

	// Token: 0x06004511 RID: 17681 RVA: 0x0016342C File Offset: 0x0016162C
	public static string Encrypt(string iDeText)
	{
		if (!AESCrypt.HaveKey())
		{
			return iDeText;
		}
		string result;
		try
		{
			byte[] rgbKey = null;
			byte[] rgbIV = null;
			using (RijndaelManaged rijndaelManaged = AESCrypt._init(ref rgbKey, ref rgbIV))
			{
				ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rgbKey, rgbIV);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
					{
						byte[] bytes = Encoding.UTF8.GetBytes(iDeText);
						cryptoStream.Write(bytes, 0, bytes.Length);
						cryptoStream.FlushFinalBlock();
						byte[] inArray = memoryStream.ToArray();
						result = Convert.ToBase64String(inArray);
					}
				}
			}
		}
		catch
		{
			result = iDeText;
		}
		return result;
	}

	// Token: 0x06004512 RID: 17682 RVA: 0x0016355C File Offset: 0x0016175C
	public static string Decrypt(string iEnText)
	{
		if (!AESCrypt.HaveKey())
		{
			return iEnText;
		}
		string result;
		try
		{
			byte[] rgbKey = null;
			byte[] rgbIV = null;
			using (RijndaelManaged rijndaelManaged = AESCrypt._init(ref rgbKey, ref rgbIV))
			{
				ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rgbKey, rgbIV);
				byte[] array = Convert.FromBase64String(iEnText);
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						byte[] array2 = new byte[array.Length];
						cryptoStream.Read(array2, 0, array2.Length);
						result = Encoding.UTF8.GetString(array2).Replace("\0", string.Empty);
					}
				}
			}
		}
		catch
		{
			result = iEnText;
		}
		return result;
	}

	// Token: 0x040039D0 RID: 14800
	private static PaddingMode mPaddingMode = PaddingMode.Zeros;

	// Token: 0x040039D1 RID: 14801
	private static CipherMode mCipherMode = CipherMode.CBC;

	// Token: 0x040039D2 RID: 14802
	private static int mKeySize = 128;

	// Token: 0x040039D3 RID: 14803
	private static int mBlockSize = 256;
}
