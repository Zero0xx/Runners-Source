using System;

// Token: 0x02000A2D RID: 2605
public class AESCryptKey
{
	// Token: 0x06004515 RID: 17685 RVA: 0x001636D8 File Offset: 0x001618D8
	public static string GetKY()
	{
		return AESCryptKey.KY;
	}

	// Token: 0x06004516 RID: 17686 RVA: 0x001636E0 File Offset: 0x001618E0
	public static string GetIV()
	{
		return AESCryptKey.IV;
	}

	// Token: 0x040039D4 RID: 14804
	private static string KY_DEFINE = "u-4Z~jWARVUjkNSz";

	// Token: 0x040039D5 RID: 14805
	private static string IV_DEFINE = "Zb2*_.gj/uZ)@4hG9nAN,.H6Ew4n2N5e";

	// Token: 0x040039D6 RID: 14806
	private static string KY = AESCryptKey.KY_DEFINE;

	// Token: 0x040039D7 RID: 14807
	private static string IV = AESCryptKey.IV_DEFINE;
}
