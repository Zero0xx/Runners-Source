using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000774 RID: 1908
public class NetServerUpdateMessage : NetBase
{
	// Token: 0x060032D2 RID: 13010 RVA: 0x0011A1B4 File Offset: 0x001183B4
	public NetServerUpdateMessage(List<int> messageIdList, List<int> operatorMessageIdList)
	{
		this.paramMessageIdList = messageIdList;
		this.paramOperatorMessageIdList = operatorMessageIdList;
	}

	// Token: 0x060032D3 RID: 13011 RVA: 0x0011A1CC File Offset: 0x001183CC
	protected override void DoRequest()
	{
		base.SetAction("Message/getMessage");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getMessageString = instance.GetGetMessageString(this.paramMessageIdList, this.paramOperatorMessageIdList);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getMessageString);
		}
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x0011A21C File Offset: 0x0011841C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_CharacterState(jdata);
		this.GetResponse_ChaoState(jdata);
		this.GetResponse_PresentStateList(jdata);
		this.GetResponse_MissingMessage(jdata);
		this.GetResponse_MissingOperatorMessage(jdata);
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x0011A254 File Offset: 0x00118454
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x060032D6 RID: 13014 RVA: 0x0011A258 File Offset: 0x00118458
	// (set) Token: 0x060032D7 RID: 13015 RVA: 0x0011A260 File Offset: 0x00118460
	public List<int> paramMessageIdList { get; set; }

	// Token: 0x170006ED RID: 1773
	// (get) Token: 0x060032D8 RID: 13016 RVA: 0x0011A26C File Offset: 0x0011846C
	// (set) Token: 0x060032D9 RID: 13017 RVA: 0x0011A274 File Offset: 0x00118474
	public List<int> paramOperatorMessageIdList { get; set; }

	// Token: 0x170006EE RID: 1774
	// (get) Token: 0x060032DA RID: 13018 RVA: 0x0011A280 File Offset: 0x00118480
	// (set) Token: 0x060032DB RID: 13019 RVA: 0x0011A288 File Offset: 0x00118488
	public ServerMessageEntry.MessageState paramMessageState { get; set; }

	// Token: 0x060032DC RID: 13020 RVA: 0x0011A294 File Offset: 0x00118494
	private void SetParameter_MessageId()
	{
		if (this.paramMessageIdList == null)
		{
			if (ServerInterface.MessageList != null && ServerInterface.MessageList.Count > 0)
			{
				base.WriteActionParamValue("messageId", 0);
			}
		}
		else
		{
			List<object> list = new List<object>();
			if (list != null)
			{
				foreach (int num in this.paramMessageIdList)
				{
					object item = num;
					list.Add(item);
				}
				if (list.Count != 0)
				{
					base.WriteActionParamArray("messageId", list);
				}
			}
		}
		if (this.paramOperatorMessageIdList == null)
		{
			if (ServerInterface.OperatorMessageList != null && ServerInterface.OperatorMessageList.Count > 0)
			{
				base.WriteActionParamValue("operationMessageId", 0);
			}
		}
		else
		{
			List<object> list2 = new List<object>();
			if (list2 != null)
			{
				foreach (int num2 in this.paramOperatorMessageIdList)
				{
					object item2 = num2;
					list2.Add(item2);
				}
				if (list2.Count != 0)
				{
					base.WriteActionParamArray("operationMessageId", list2);
				}
			}
		}
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x060032DD RID: 13021 RVA: 0x0011A41C File Offset: 0x0011861C
	// (set) Token: 0x060032DE RID: 13022 RVA: 0x0011A424 File Offset: 0x00118624
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x060032DF RID: 13023 RVA: 0x0011A430 File Offset: 0x00118630
	// (set) Token: 0x060032E0 RID: 13024 RVA: 0x0011A438 File Offset: 0x00118638
	public ServerCharacterState[] resultCharacterState { get; private set; }

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x060032E1 RID: 13025 RVA: 0x0011A444 File Offset: 0x00118644
	// (set) Token: 0x060032E2 RID: 13026 RVA: 0x0011A44C File Offset: 0x0011864C
	public List<ServerChaoState> resultChaoState { get; private set; }

	// Token: 0x170006F2 RID: 1778
	// (get) Token: 0x060032E3 RID: 13027 RVA: 0x0011A458 File Offset: 0x00118658
	public int resultPresentStates
	{
		get
		{
			return (this.resultPresentStateList == null) ? 0 : this.resultPresentStateList.Count;
		}
	}

	// Token: 0x170006F3 RID: 1779
	// (get) Token: 0x060032E4 RID: 13028 RVA: 0x0011A478 File Offset: 0x00118678
	public int resultMissingMessages
	{
		get
		{
			return (this.resultMissingMessageIdList == null) ? 0 : this.resultMissingMessageIdList.Count;
		}
	}

	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x060032E5 RID: 13029 RVA: 0x0011A498 File Offset: 0x00118698
	public int resultMissingOperatorMessages
	{
		get
		{
			return (this.resultMissingOperatorMessageIdList == null) ? 0 : this.resultMissingOperatorMessageIdList.Count;
		}
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x060032E6 RID: 13030 RVA: 0x0011A4B8 File Offset: 0x001186B8
	// (set) Token: 0x060032E7 RID: 13031 RVA: 0x0011A4C0 File Offset: 0x001186C0
	private List<ServerPresentState> resultPresentStateList { get; set; }

	// Token: 0x060032E8 RID: 13032 RVA: 0x0011A4CC File Offset: 0x001186CC
	public ServerPresentState GetResultPresentState(int index)
	{
		if (0 <= index && this.resultPresentStates > index)
		{
			return this.resultPresentStateList[index];
		}
		return null;
	}

	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x060032E9 RID: 13033 RVA: 0x0011A4F0 File Offset: 0x001186F0
	// (set) Token: 0x060032EA RID: 13034 RVA: 0x0011A4F8 File Offset: 0x001186F8
	private List<int> resultMissingMessageIdList { get; set; }

	// Token: 0x060032EB RID: 13035 RVA: 0x0011A504 File Offset: 0x00118704
	public int GetResultMissingMessageId(int index)
	{
		if (0 <= index && this.resultMissingMessages > index)
		{
			return this.resultMissingMessageIdList[index];
		}
		return int.MinValue;
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x060032EC RID: 13036 RVA: 0x0011A52C File Offset: 0x0011872C
	// (set) Token: 0x060032ED RID: 13037 RVA: 0x0011A534 File Offset: 0x00118734
	private List<int> resultMissingOperatorMessageIdList { get; set; }

	// Token: 0x060032EE RID: 13038 RVA: 0x0011A540 File Offset: 0x00118740
	public int GetResultMissingOperatorMessageId(int index)
	{
		if (0 <= index && this.resultMissingOperatorMessages > index)
		{
			return this.resultMissingOperatorMessageIdList[index];
		}
		return int.MinValue;
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x0011A568 File Offset: 0x00118768
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x0011A57C File Offset: 0x0011877C
	private void GetResponse_CharacterState(JsonData jdata)
	{
		this.resultCharacterState = NetUtil.AnalyzePlayerState_CharactersStates(jdata);
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x0011A58C File Offset: 0x0011878C
	private void GetResponse_ChaoState(JsonData jdata)
	{
		this.resultChaoState = NetUtil.AnalyzePlayerState_ChaoStates(jdata);
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x0011A59C File Offset: 0x0011879C
	private void GetResponse_PresentStateList(JsonData jdata)
	{
		this.resultPresentStateList = new List<ServerPresentState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "presentList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerPresentState item = NetUtil.AnalyzePresentStateJson(jdata2, string.Empty);
			this.resultPresentStateList.Add(item);
		}
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x0011A5FC File Offset: 0x001187FC
	private void GetResponse_MissingMessage(JsonData jdata)
	{
		this.resultMissingMessageIdList = new List<int>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "notRecvMessageList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			int jsonInt = NetUtil.GetJsonInt(jsonArray[i]);
			this.resultMissingMessageIdList.Add(jsonInt);
		}
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x0011A654 File Offset: 0x00118854
	private void GetResponse_MissingOperatorMessage(JsonData jdata)
	{
		this.resultMissingOperatorMessageIdList = new List<int>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "notRecvOperatorMessageList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			int jsonInt = NetUtil.GetJsonInt(jsonArray[i]);
			this.resultMissingOperatorMessageIdList.Add(jsonInt);
		}
	}

	// Token: 0x04002B93 RID: 11155
	public const int INVALID_ID = -2147483648;
}
