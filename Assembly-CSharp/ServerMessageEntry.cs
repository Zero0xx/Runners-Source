using System;

// Token: 0x0200080C RID: 2060
public class ServerMessageEntry
{
	// Token: 0x06003742 RID: 14146 RVA: 0x00123A48 File Offset: 0x00121C48
	public ServerMessageEntry()
	{
		this.m_messageId = 2346789;
		this.m_fromId = "0123456789abcdef";
		this.m_messageType = ServerMessageEntry.MessageType.SendEnergy;
		this.m_messageState = ServerMessageEntry.MessageState.Unread;
		this.m_name = "0123456789abcdef";
		this.m_url = "0123456789abcdef";
		this.m_expireTiem = 0;
		this.m_presentState = new ServerPresentState();
	}

	// Token: 0x06003743 RID: 14147 RVA: 0x00123AA8 File Offset: 0x00121CA8
	public void CopyTo(ServerMessageEntry to)
	{
		to.m_messageId = this.m_messageId;
		to.m_fromId = this.m_fromId;
		to.m_messageType = this.m_messageType;
		to.m_messageState = this.m_messageState;
		to.m_name = this.m_name;
		to.m_url = this.m_url;
		to.m_expireTiem = this.m_expireTiem;
		this.m_presentState.CopyTo(to.m_presentState);
	}

	// Token: 0x04002E85 RID: 11909
	public int m_messageId;

	// Token: 0x04002E86 RID: 11910
	public ServerMessageEntry.MessageType m_messageType;

	// Token: 0x04002E87 RID: 11911
	public string m_fromId;

	// Token: 0x04002E88 RID: 11912
	public string m_name;

	// Token: 0x04002E89 RID: 11913
	public string m_url;

	// Token: 0x04002E8A RID: 11914
	public ServerMessageEntry.MessageState m_messageState;

	// Token: 0x04002E8B RID: 11915
	public int m_expireTiem;

	// Token: 0x04002E8C RID: 11916
	public ServerPresentState m_presentState;

	// Token: 0x0200080D RID: 2061
	public enum MessageState
	{
		// Token: 0x04002E8E RID: 11918
		Unread,
		// Token: 0x04002E8F RID: 11919
		Read,
		// Token: 0x04002E90 RID: 11920
		Used,
		// Token: 0x04002E91 RID: 11921
		Deleted
	}

	// Token: 0x0200080E RID: 2062
	public enum MessageType
	{
		// Token: 0x04002E93 RID: 11923
		RequestEnergy,
		// Token: 0x04002E94 RID: 11924
		ReturnRequestEnergy,
		// Token: 0x04002E95 RID: 11925
		SendEnergy,
		// Token: 0x04002E96 RID: 11926
		ReturnSendEnergy,
		// Token: 0x04002E97 RID: 11927
		LentChao,
		// Token: 0x04002E98 RID: 11928
		InviteCode,
		// Token: 0x04002E99 RID: 11929
		Unknown = -1
	}
}
