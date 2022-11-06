using System;
using LitJson;

// Token: 0x02000757 RID: 1879
public class NetServerLoginBonusSelect : NetBase
{
	// Token: 0x06003243 RID: 12867 RVA: 0x00118F50 File Offset: 0x00117150
	public NetServerLoginBonusSelect() : this(0, 0, 0, 0, 0)
	{
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x00118F60 File Offset: 0x00117160
	public NetServerLoginBonusSelect(int rewardId, int rewardDays, int rewardSelect, int firstRewardDays, int firstRewardSelect)
	{
		this.paramRewardId = rewardId;
		this.paramRewardDays = rewardDays;
		this.paramRewardSelect = rewardSelect;
		this.paramFirstRewardDays = firstRewardDays;
		this.paramFirstRewardSelect = firstRewardSelect;
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x00118F90 File Offset: 0x00117190
	protected override void DoRequest()
	{
		base.SetAction("Login/loginBonusSelect");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string jsonString = instance.LoginBonusSelectString(this.paramRewardId, this.paramRewardDays, this.paramRewardSelect, this.paramFirstRewardDays, this.paramFirstRewardSelect);
			base.WriteJsonString(jsonString);
		}
	}

	// Token: 0x06003246 RID: 12870 RVA: 0x00118FE8 File Offset: 0x001171E8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LoginBonusRewardList(jdata);
		this.GetResponse_FirstLoginBonusRewardList(jdata);
	}

	// Token: 0x06003247 RID: 12871 RVA: 0x00118FF8 File Offset: 0x001171F8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06003248 RID: 12872 RVA: 0x00118FFC File Offset: 0x001171FC
	// (set) Token: 0x06003249 RID: 12873 RVA: 0x00119004 File Offset: 0x00117204
	public ServerLoginBonusReward loginBonusReward { get; private set; }

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x0600324A RID: 12874 RVA: 0x00119010 File Offset: 0x00117210
	// (set) Token: 0x0600324B RID: 12875 RVA: 0x00119018 File Offset: 0x00117218
	public ServerLoginBonusReward firstLoginBonusReward { get; private set; }

	// Token: 0x0600324C RID: 12876 RVA: 0x00119024 File Offset: 0x00117224
	private void GetResponse_LoginBonusRewardList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "rewardList");
		if (jsonArray == null)
		{
			this.loginBonusReward = null;
			return;
		}
		this.loginBonusReward = new ServerLoginBonusReward();
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			this.loginBonusReward.m_itemList.Add(NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty));
		}
	}

	// Token: 0x0600324D RID: 12877 RVA: 0x00119090 File Offset: 0x00117290
	private void GetResponse_FirstLoginBonusRewardList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "firstRewardList");
		this.firstLoginBonusReward = new ServerLoginBonusReward();
		if (jsonArray == null)
		{
			this.firstLoginBonusReward = null;
			return;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			this.firstLoginBonusReward.m_itemList.Add(NetUtil.AnalyzeItemStateJson(jsonArray[i], string.Empty));
		}
	}

	// Token: 0x04002B69 RID: 11113
	public int paramRewardId;

	// Token: 0x04002B6A RID: 11114
	public int paramRewardDays;

	// Token: 0x04002B6B RID: 11115
	public int paramRewardSelect;

	// Token: 0x04002B6C RID: 11116
	public int paramFirstRewardDays;

	// Token: 0x04002B6D RID: 11117
	public int paramFirstRewardSelect;
}
