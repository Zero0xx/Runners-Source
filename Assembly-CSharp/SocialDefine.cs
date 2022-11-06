using System;

// Token: 0x02000A09 RID: 2569
public class SocialDefine
{
	// Token: 0x0400395B RID: 14683
	public static readonly string[] ScoreQueryName = new string[]
	{
		"/testrunners:store"
	};

	// Token: 0x0400395C RID: 14684
	public static readonly string[] ScoreJsonKeyName = new string[]
	{
		"gamedata"
	};

	// Token: 0x02000A0A RID: 2570
	public enum ScoreType
	{
		// Token: 0x0400395E RID: 14686
		TYPE_NONE = -1,
		// Token: 0x0400395F RID: 14687
		TYPE_GAMEDATA,
		// Token: 0x04003960 RID: 14688
		TYPE_NUM
	}
}
