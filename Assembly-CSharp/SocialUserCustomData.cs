using System;

// Token: 0x02000A12 RID: 2578
public class SocialUserCustomData
{
	// Token: 0x17000939 RID: 2361
	// (get) Token: 0x06004425 RID: 17445 RVA: 0x0016003C File Offset: 0x0015E23C
	// (set) Token: 0x06004426 RID: 17446 RVA: 0x00160044 File Offset: 0x0015E244
	public string ActionId
	{
		get
		{
			return this.m_actionId;
		}
		set
		{
			this.m_actionId = value;
		}
	}

	// Token: 0x1700093A RID: 2362
	// (get) Token: 0x06004427 RID: 17447 RVA: 0x00160050 File Offset: 0x0015E250
	// (set) Token: 0x06004428 RID: 17448 RVA: 0x00160058 File Offset: 0x0015E258
	public string ObjectId
	{
		get
		{
			return this.m_objectId;
		}
		set
		{
			this.m_objectId = value;
		}
	}

	// Token: 0x1700093B RID: 2363
	// (get) Token: 0x06004429 RID: 17449 RVA: 0x00160064 File Offset: 0x0015E264
	// (set) Token: 0x0600442A RID: 17450 RVA: 0x0016006C File Offset: 0x0015E26C
	public string GameId
	{
		get
		{
			return this.m_gameId;
		}
		set
		{
			this.m_gameId = value;
		}
	}

	// Token: 0x0400397A RID: 14714
	private string m_actionId = string.Empty;

	// Token: 0x0400397B RID: 14715
	private string m_objectId = string.Empty;

	// Token: 0x0400397C RID: 14716
	private string m_gameId = string.Empty;
}
