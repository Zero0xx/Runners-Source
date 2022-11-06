using System;

// Token: 0x020003D4 RID: 980
internal class CharaName
{
	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000A9500 File Offset: 0x000A7700
	public static string[] PrefixName
	{
		get
		{
			return CharacterDataNameInfo.PrefixNameList;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000A9508 File Offset: 0x000A7708
	public static string[] Name
	{
		get
		{
			return CharacterDataNameInfo.CharaNameLowerList;
		}
	}
}
