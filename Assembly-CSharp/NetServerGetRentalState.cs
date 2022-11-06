using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

// Token: 0x0200069C RID: 1692
public class NetServerGetRentalState : NetBase
{
	// Token: 0x06002DA5 RID: 11685 RVA: 0x0011010C File Offset: 0x0010E30C
	public NetServerGetRentalState() : this(null)
	{
	}

	// Token: 0x06002DA6 RID: 11686 RVA: 0x00110118 File Offset: 0x0010E318
	public NetServerGetRentalState(string[] friendIdList)
	{
		this.friendIdList = friendIdList;
	}

	// Token: 0x06002DA7 RID: 11687 RVA: 0x00110128 File Offset: 0x0010E328
	protected override void DoRequest()
	{
		base.SetAction("Chao/getRentalState");
		this.SetParameter_FriendId();
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x0011013C File Offset: 0x0010E33C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LastOffset(jdata);
		this.GetResponse_ChaoRentalStatesList(jdata);
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x0011014C File Offset: 0x0010E34C
	protected override void DoDidSuccessEmulation()
	{
		this.resultChaoRentalStatesList = new List<ServerChaoRentalState>();
		int num = this.friendIdList.Length;
		for (int i = 0; i < num; i++)
		{
			ServerChaoRentalState serverChaoRentalState = new ServerChaoRentalState();
			serverChaoRentalState.FriendId = UnityEngine.Random.Range(0f, 1E+11f).ToString();
			serverChaoRentalState.Name = "dummy_" + i;
			serverChaoRentalState.RentalState = UnityEngine.Random.Range(0, 1);
			serverChaoRentalState.ChaoData = new ServerChaoData();
			serverChaoRentalState.ChaoData.Id = UnityEngine.Random.Range(400000, 400011);
			serverChaoRentalState.ChaoData.Level = 1;
			serverChaoRentalState.ChaoData.Rarity = 0;
			this.resultChaoRentalStatesList.Add(serverChaoRentalState);
		}
	}

	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x06002DAA RID: 11690 RVA: 0x00110210 File Offset: 0x0010E410
	// (set) Token: 0x06002DAB RID: 11691 RVA: 0x00110218 File Offset: 0x0010E418
	public string[] friendIdList { get; set; }

	// Token: 0x06002DAC RID: 11692 RVA: 0x00110224 File Offset: 0x0010E424
	private void SetParameter_FriendId()
	{
		List<object> list = new List<object>();
		foreach (string item in this.friendIdList)
		{
			list.Add(item);
		}
		base.WriteActionParamArray("friendId", list);
	}

	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0011026C File Offset: 0x0010E46C
	// (set) Token: 0x06002DAE RID: 11694 RVA: 0x00110274 File Offset: 0x0010E474
	public int resultLastOffset { get; private set; }

	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x06002DAF RID: 11695 RVA: 0x00110280 File Offset: 0x0010E480
	public int resultStates
	{
		get
		{
			return (this.resultChaoRentalStatesList == null) ? 0 : this.resultChaoRentalStatesList.Count;
		}
	}

	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x001102A0 File Offset: 0x0010E4A0
	// (set) Token: 0x06002DB1 RID: 11697 RVA: 0x001102A8 File Offset: 0x0010E4A8
	protected List<ServerChaoRentalState> resultChaoRentalStatesList { get; set; }

	// Token: 0x06002DB2 RID: 11698 RVA: 0x001102B4 File Offset: 0x0010E4B4
	public ServerChaoRentalState GetResultChaoRentalState(int index)
	{
		if (0 <= index && this.resultStates > index)
		{
			return this.resultChaoRentalStatesList[index];
		}
		return null;
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x001102D8 File Offset: 0x0010E4D8
	private void GetResponse_LastOffset(JsonData jdata)
	{
		this.resultLastOffset = NetUtil.GetJsonInt(jdata, "lastOffset");
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x001102EC File Offset: 0x0010E4EC
	private void GetResponse_ChaoRentalStatesList(JsonData jdata)
	{
		this.resultChaoRentalStatesList = new List<ServerChaoRentalState>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "chaoRentalState");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerChaoRentalState item = NetUtil.AnalyzeChaoRentalStateJson(jdata2, string.Empty);
			this.resultChaoRentalStatesList.Add(item);
		}
	}
}
