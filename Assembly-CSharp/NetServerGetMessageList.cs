using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000772 RID: 1906
public class NetServerGetMessageList : NetBase
{
	// Token: 0x060032B4 RID: 12980 RVA: 0x00119C48 File Offset: 0x00117E48
	protected override void DoRequest()
	{
		base.SetAction("Message/getMessageList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060032B5 RID: 12981 RVA: 0x00119C80 File Offset: 0x00117E80
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_MessageList(jdata);
		this.GetResponse_OperatorMessageList(jdata);
		this.GetResponse_TotalMessaga(jdata);
		this.GetResponse_TotalOperatorMessaga(jdata);
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x00119CAC File Offset: 0x00117EAC
	protected override void DoDidSuccessEmulation()
	{
		this.resultMessageEntriesList = new List<ServerMessageEntry>();
		this.resultOperatorMessageEntriesList = new List<ServerOperatorMessageEntry>();
		for (int i = 0; i < 2; i++)
		{
			ServerOperatorMessageEntry serverOperatorMessageEntry = new ServerOperatorMessageEntry();
			serverOperatorMessageEntry.m_messageId = i + 1;
			serverOperatorMessageEntry.m_content = "dummy content " + (i + 1);
			serverOperatorMessageEntry.m_presentState.m_itemId = 400000;
			serverOperatorMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
			this.resultOperatorMessageEntriesList.Add(serverOperatorMessageEntry);
		}
		ServerMessageEntry serverMessageEntry = new ServerMessageEntry();
		serverMessageEntry.m_messageId = 1;
		serverMessageEntry.m_name = "dummy_0001";
		serverMessageEntry.m_url = (serverMessageEntry.m_fromId = "0123456789abcdefg1");
		serverMessageEntry.m_presentState.m_itemId = 900000;
		serverMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
		serverMessageEntry.m_messageState = ServerMessageEntry.MessageState.Unread;
		serverMessageEntry.m_messageType = ServerMessageEntry.MessageType.SendEnergy;
		this.resultMessageEntriesList.Add(serverMessageEntry);
		serverMessageEntry = new ServerMessageEntry();
		serverMessageEntry.m_messageId = 2;
		serverMessageEntry.m_name = "dummy_0002";
		serverMessageEntry.m_url = (serverMessageEntry.m_fromId = "0123456789abcdefg2");
		serverMessageEntry.m_presentState.m_itemId = 900000;
		serverMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
		serverMessageEntry.m_messageState = ServerMessageEntry.MessageState.Unread;
		serverMessageEntry.m_messageType = ServerMessageEntry.MessageType.ReturnSendEnergy;
		this.resultMessageEntriesList.Add(serverMessageEntry);
		serverMessageEntry = new ServerMessageEntry();
		serverMessageEntry.m_messageId = 3;
		serverMessageEntry.m_name = "dummy_0003";
		serverMessageEntry.m_url = (serverMessageEntry.m_fromId = "0123456789abcdefg3");
		serverMessageEntry.m_presentState.m_itemId = 900000;
		serverMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
		serverMessageEntry.m_messageState = ServerMessageEntry.MessageState.Unread;
		serverMessageEntry.m_messageType = ServerMessageEntry.MessageType.RequestEnergy;
		this.resultMessageEntriesList.Add(serverMessageEntry);
		serverMessageEntry = new ServerMessageEntry();
		serverMessageEntry.m_messageId = 4;
		serverMessageEntry.m_name = "dummy_0004";
		serverMessageEntry.m_url = (serverMessageEntry.m_fromId = "0123456789abcdefg4");
		serverMessageEntry.m_presentState.m_itemId = 900000;
		serverMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
		serverMessageEntry.m_messageState = ServerMessageEntry.MessageState.Unread;
		serverMessageEntry.m_messageType = ServerMessageEntry.MessageType.ReturnRequestEnergy;
		this.resultMessageEntriesList.Add(serverMessageEntry);
		serverMessageEntry = new ServerMessageEntry();
		serverMessageEntry.m_messageId = 5;
		serverMessageEntry.m_name = "dummy_0005";
		serverMessageEntry.m_url = (serverMessageEntry.m_fromId = "0123456789abcdefg5");
		serverMessageEntry.m_presentState.m_itemId = 900000;
		serverMessageEntry.m_expireTiem = NetUtil.GetCurrentUnixTime() + 10000000;
		serverMessageEntry.m_messageState = ServerMessageEntry.MessageState.Unread;
		serverMessageEntry.m_messageType = ServerMessageEntry.MessageType.LentChao;
		this.resultMessageEntriesList.Add(serverMessageEntry);
	}

	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x060032B7 RID: 12983 RVA: 0x00119F3C File Offset: 0x0011813C
	// (set) Token: 0x060032B8 RID: 12984 RVA: 0x00119F44 File Offset: 0x00118144
	public int resultTotalMessages { get; private set; }

	// Token: 0x170006E5 RID: 1765
	// (get) Token: 0x060032B9 RID: 12985 RVA: 0x00119F50 File Offset: 0x00118150
	// (set) Token: 0x060032BA RID: 12986 RVA: 0x00119F58 File Offset: 0x00118158
	public int resultTotalOperatorMessages { get; private set; }

	// Token: 0x170006E6 RID: 1766
	// (get) Token: 0x060032BB RID: 12987 RVA: 0x00119F64 File Offset: 0x00118164
	public int resultMessages
	{
		get
		{
			return (this.resultMessageEntriesList == null) ? 0 : this.resultMessageEntriesList.Count;
		}
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x060032BC RID: 12988 RVA: 0x00119F84 File Offset: 0x00118184
	public int resultOperatorMessages
	{
		get
		{
			return (this.resultOperatorMessageEntriesList == null) ? 0 : this.resultOperatorMessageEntriesList.Count;
		}
	}

	// Token: 0x170006E8 RID: 1768
	// (get) Token: 0x060032BD RID: 12989 RVA: 0x00119FA4 File Offset: 0x001181A4
	// (set) Token: 0x060032BE RID: 12990 RVA: 0x00119FAC File Offset: 0x001181AC
	private List<ServerMessageEntry> resultMessageEntriesList { get; set; }

	// Token: 0x060032BF RID: 12991 RVA: 0x00119FB8 File Offset: 0x001181B8
	public ServerMessageEntry GetResultMessageEntry(int index)
	{
		if (0 <= index && this.resultMessages > index)
		{
			return this.resultMessageEntriesList[index];
		}
		return null;
	}

	// Token: 0x170006E9 RID: 1769
	// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00119FDC File Offset: 0x001181DC
	// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00119FE4 File Offset: 0x001181E4
	private List<ServerOperatorMessageEntry> resultOperatorMessageEntriesList { get; set; }

	// Token: 0x060032C2 RID: 12994 RVA: 0x00119FF0 File Offset: 0x001181F0
	public ServerOperatorMessageEntry GetResultOperatorMessageEntry(int index)
	{
		if (0 <= index && this.resultOperatorMessages > index)
		{
			return this.resultOperatorMessageEntriesList[index];
		}
		return null;
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x0011A014 File Offset: 0x00118214
	private void GetResponse_MessageList(JsonData jdata)
	{
		this.resultMessageEntriesList = new List<ServerMessageEntry>();
		int count = NetUtil.GetJsonArray(jdata, "messageList").Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jsonArrayObject = NetUtil.GetJsonArrayObject(jdata, "messageList", i);
			ServerMessageEntry item = NetUtil.AnalyzeMessageEntryJson(jsonArrayObject, string.Empty);
			this.resultMessageEntriesList.Add(item);
		}
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x0011A074 File Offset: 0x00118274
	private void GetResponse_TotalMessaga(JsonData jdata)
	{
		this.resultTotalMessages = NetUtil.GetJsonInt(jdata, "totalMessage");
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x0011A088 File Offset: 0x00118288
	private void GetResponse_OperatorMessageList(JsonData jdata)
	{
		this.resultOperatorMessageEntriesList = new List<ServerOperatorMessageEntry>();
		int count = NetUtil.GetJsonArray(jdata, "operatorMessageList").Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jsonArrayObject = NetUtil.GetJsonArrayObject(jdata, "operatorMessageList", i);
			ServerOperatorMessageEntry item = NetUtil.AnalyzeOperatorMessageEntryJson(jsonArrayObject, string.Empty);
			this.resultOperatorMessageEntriesList.Add(item);
		}
	}

	// Token: 0x060032C6 RID: 12998 RVA: 0x0011A0E8 File Offset: 0x001182E8
	private void GetResponse_TotalOperatorMessaga(JsonData jdata)
	{
		this.resultTotalOperatorMessages = NetUtil.GetJsonInt(jdata, "totalOperatorMessage");
	}
}
