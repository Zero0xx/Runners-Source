using System;

// Token: 0x02000812 RID: 2066
public class ServerOperatorMessageEntry
{
	// Token: 0x0600376D RID: 14189 RVA: 0x00124384 File Offset: 0x00122584
	public ServerOperatorMessageEntry()
	{
		this.m_messageId = 2346789;
		this.m_content = string.Empty;
		this.m_expireTiem = 0;
		this.m_presentState = new ServerPresentState();
	}

	// Token: 0x0600376E RID: 14190 RVA: 0x001243C0 File Offset: 0x001225C0
	public void CopyTo(ServerOperatorMessageEntry to)
	{
		to.m_messageId = this.m_messageId;
		to.m_content = this.m_content;
		to.m_expireTiem = this.m_expireTiem;
		this.m_presentState.CopyTo(to.m_presentState);
	}

	// Token: 0x04002EBA RID: 11962
	public int m_messageId;

	// Token: 0x04002EBB RID: 11963
	public string m_content;

	// Token: 0x04002EBC RID: 11964
	public int m_expireTiem;

	// Token: 0x04002EBD RID: 11965
	public ServerPresentState m_presentState;
}
