using System;

// Token: 0x0200080F RID: 2063
public class ServerNextVersionState
{
	// Token: 0x06003744 RID: 14148 RVA: 0x00123B1C File Offset: 0x00121D1C
	public ServerNextVersionState()
	{
		this.m_buyRSRNum = 0;
		this.m_freeRSRNum = 0;
		this.m_userName = string.Empty;
		this.m_userId = string.Empty;
		this.m_url = string.Empty;
		this.m_eMsg = string.Empty;
		this.m_jMsg = string.Empty;
	}

	// Token: 0x04002E9A RID: 11930
	public int m_buyRSRNum;

	// Token: 0x04002E9B RID: 11931
	public int m_freeRSRNum;

	// Token: 0x04002E9C RID: 11932
	public string m_userName;

	// Token: 0x04002E9D RID: 11933
	public string m_userId;

	// Token: 0x04002E9E RID: 11934
	public string m_url;

	// Token: 0x04002E9F RID: 11935
	public string m_eMsg;

	// Token: 0x04002EA0 RID: 11936
	public string m_jMsg;
}
