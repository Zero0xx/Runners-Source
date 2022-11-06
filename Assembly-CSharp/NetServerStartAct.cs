using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071F RID: 1823
public class NetServerStartAct : NetBase
{
	// Token: 0x060030C8 RID: 12488 RVA: 0x001159AC File Offset: 0x00113BAC
	public NetServerStartAct() : this(null, null, null, false, null)
	{
	}

	// Token: 0x060030C9 RID: 12489 RVA: 0x001159CC File Offset: 0x00113BCC
	public NetServerStartAct(List<ItemType> modifiersItem, List<BoostItemType> modifiersBoostItem, List<string> distanceFriendList, bool tutorial, int? eventId)
	{
		this.paramModifiersItem = new List<ItemType>();
		if (modifiersItem != null)
		{
			for (int i = 0; i < modifiersItem.Count; i++)
			{
				this.paramModifiersItem.Add(modifiersItem[i]);
			}
		}
		this.paramModifiersBoostItem = new List<BoostItemType>();
		if (modifiersBoostItem != null)
		{
			for (int j = 0; j < modifiersBoostItem.Count; j++)
			{
				this.paramModifiersBoostItem.Add(modifiersBoostItem[j]);
			}
		}
		this.paramFriendIdList = distanceFriendList;
		this.paramTutorial = tutorial;
		this.paramEventId = eventId;
	}

	// Token: 0x060030CA RID: 12490 RVA: 0x00115A6C File Offset: 0x00113C6C
	protected override void DoRequest()
	{
		base.SetAction("Game/actStart");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.paramModifiersItem.Count; i++)
			{
				ServerItem serverItem = new ServerItem(this.paramModifiersItem[i]);
				ServerItem.Id id = serverItem.id;
				list.Add((int)id);
			}
			for (int j = 0; j < this.paramModifiersBoostItem.Count; j++)
			{
				ServerItem serverItem2 = new ServerItem(this.paramModifiersBoostItem[j]);
				ServerItem.Id id2 = serverItem2.id;
				list.Add((int)id2);
			}
			int eventId = (this.paramEventId == null) ? -1 : this.paramEventId.Value;
			string actStartString = instance.GetActStartString(list, this.paramFriendIdList, this.paramTutorial, eventId);
			Debug.Log("NetServerPostGameResults.json = " + actStartString);
			base.WriteJsonString(actStartString);
		}
	}

	// Token: 0x060030CB RID: 12491 RVA: 0x00115B7C File Offset: 0x00113D7C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_PlayerState(jdata);
		this.GetResponse_MileageBonus(jdata);
		NetUtil.GetResponse_CampaignList(jdata);
		this.GetResponse_DistanceFriendList(jdata);
	}

	// Token: 0x060030CC RID: 12492 RVA: 0x00115B9C File Offset: 0x00113D9C
	protected override void DoDidSuccessEmulation()
	{
		if (this.resultPlayerState.m_numEnergy < 1)
		{
			base.resultStCd = ServerInterface.StatusCode.NotEnoughEnergy;
			return;
		}
		this.resultPlayerState.m_numEnergy--;
		this.resultPlayerState.m_numContinuesUsed = 0;
		DateTime now = DateTime.Now;
		if (this.resultPlayerState.m_energyRenewsAt <= now)
		{
			this.resultPlayerState.m_energyRenewsAt = now + new TimeSpan(0, 0, (int)ServerInterface.SettingState.m_energyRefreshTime);
		}
	}

	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x060030CD RID: 12493 RVA: 0x00115C24 File Offset: 0x00113E24
	// (set) Token: 0x060030CE RID: 12494 RVA: 0x00115C2C File Offset: 0x00113E2C
	public List<ItemType> paramModifiersItem { get; set; }

	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x060030CF RID: 12495 RVA: 0x00115C38 File Offset: 0x00113E38
	// (set) Token: 0x060030D0 RID: 12496 RVA: 0x00115C40 File Offset: 0x00113E40
	public List<BoostItemType> paramModifiersBoostItem { get; set; }

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x060030D1 RID: 12497 RVA: 0x00115C4C File Offset: 0x00113E4C
	// (set) Token: 0x060030D2 RID: 12498 RVA: 0x00115C54 File Offset: 0x00113E54
	public List<string> paramFriendIdList { get; set; }

	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x060030D3 RID: 12499 RVA: 0x00115C60 File Offset: 0x00113E60
	// (set) Token: 0x060030D4 RID: 12500 RVA: 0x00115C68 File Offset: 0x00113E68
	public bool paramTutorial { get; set; }

	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x060030D5 RID: 12501 RVA: 0x00115C74 File Offset: 0x00113E74
	// (set) Token: 0x060030D6 RID: 12502 RVA: 0x00115C7C File Offset: 0x00113E7C
	public int? paramEventId { get; set; }

	// Token: 0x060030D7 RID: 12503 RVA: 0x00115C88 File Offset: 0x00113E88
	private void SetParameter_Modifiers()
	{
		List<object> list = new List<object>();
		for (int i = 0; i < this.paramModifiersItem.Count; i++)
		{
			ServerItem serverItem = new ServerItem(this.paramModifiersItem[i]);
			ServerItem.Id id = serverItem.id;
			list.Add((int)id);
		}
		for (int j = 0; j < this.paramModifiersBoostItem.Count; j++)
		{
			ServerItem serverItem2 = new ServerItem(this.paramModifiersBoostItem[j]);
			ServerItem.Id id2 = serverItem2.id;
			list.Add((int)id2);
		}
		base.WriteActionParamArray("modifire", list);
		list.Clear();
	}

	// Token: 0x060030D8 RID: 12504 RVA: 0x00115D38 File Offset: 0x00113F38
	private void SetParameter_FriendIdList()
	{
		List<object> list = new List<object>();
		for (int i = 0; i < this.paramFriendIdList.Count; i++)
		{
			list.Add(this.paramFriendIdList[i]);
		}
		base.WriteActionParamArray("distanceFriendList", list);
		list.Clear();
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x00115D90 File Offset: 0x00113F90
	private void SetParameter_Tutorial()
	{
		base.WriteActionParamValue("tutorial", (!this.paramTutorial) ? 0 : 1);
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x00115DC0 File Offset: 0x00113FC0
	private void SetParameter_EventId()
	{
		if (this.paramEventId != null)
		{
			base.WriteActionParamValue("eventId", this.paramEventId);
		}
	}

	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x060030DB RID: 12507 RVA: 0x00115DF8 File Offset: 0x00113FF8
	// (set) Token: 0x060030DC RID: 12508 RVA: 0x00115E00 File Offset: 0x00114000
	public ServerPlayerState resultPlayerState { get; private set; }

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x060030DD RID: 12509 RVA: 0x00115E0C File Offset: 0x0011400C
	// (set) Token: 0x060030DE RID: 12510 RVA: 0x00115E14 File Offset: 0x00114014
	public List<ServerDistanceFriendEntry> resultDistanceFriendEntry { get; private set; }

	// Token: 0x060030DF RID: 12511 RVA: 0x00115E20 File Offset: 0x00114020
	private void GetResponse_PlayerState(JsonData jdata)
	{
		this.resultPlayerState = NetUtil.AnalyzePlayerStateJson(jdata, "playerState");
	}

	// Token: 0x060030E0 RID: 12512 RVA: 0x00115E34 File Offset: 0x00114034
	private void GetResponse_MileageBonus(JsonData jdata)
	{
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x00115E38 File Offset: 0x00114038
	private void GetResponse_DistanceFriendList(JsonData jdata)
	{
		this.resultDistanceFriendEntry = new List<ServerDistanceFriendEntry>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "distanceFriendList");
		if (jsonArray != null)
		{
			int count = jsonArray.Count;
			for (int i = 0; i < count; i++)
			{
				ServerDistanceFriendEntry serverDistanceFriendEntry = new ServerDistanceFriendEntry();
				serverDistanceFriendEntry.m_friendId = NetUtil.GetJsonString(jsonArray[i], "friendId");
				serverDistanceFriendEntry.m_distance = NetUtil.GetJsonInt(jsonArray[i], "distance");
				this.resultDistanceFriendEntry.Add(serverDistanceFriendEntry);
			}
		}
	}
}
