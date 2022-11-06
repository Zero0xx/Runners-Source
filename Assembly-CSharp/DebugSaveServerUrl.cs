using System;

// Token: 0x020001BF RID: 447
public class DebugSaveServerUrl
{
	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00048A58 File Offset: 0x00046C58
	public static string Url
	{
		get
		{
			return DebugSaveServerUrl.s_Url;
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00048A60 File Offset: 0x00046C60
	public static void LoadURL()
	{
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00048A64 File Offset: 0x00046C64
	public static void SaveURL(string url)
	{
	}

	// Token: 0x04000A0A RID: 2570
	private const string SaveURLName = "DebugServerURL.txt";

	// Token: 0x04000A0B RID: 2571
	private static string s_Url;
}
