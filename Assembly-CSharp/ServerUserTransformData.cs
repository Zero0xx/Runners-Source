using System;

// Token: 0x02000825 RID: 2085
public class ServerUserTransformData
{
	// Token: 0x060037FD RID: 14333 RVA: 0x00127B58 File Offset: 0x00125D58
	public ServerUserTransformData()
	{
		this.m_userId = string.Empty;
		this.m_facebookId = string.Empty;
	}

	// Token: 0x060037FE RID: 14334 RVA: 0x00127B78 File Offset: 0x00125D78
	public void Dump()
	{
		Debug.Log(string.Format("userId={0}, facebookId={1}", this.m_userId, this.m_facebookId));
	}

	// Token: 0x04002F4E RID: 12110
	public string m_userId;

	// Token: 0x04002F4F RID: 12111
	public string m_facebookId;
}
