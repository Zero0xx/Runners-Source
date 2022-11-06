using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000756 RID: 1878
public class NetServerLoginBonus : NetBase
{
	// Token: 0x06003227 RID: 12839 RVA: 0x00118B64 File Offset: 0x00116D64
	protected override void DoRequest()
	{
		base.SetAction("Login/loginBonus");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003228 RID: 12840 RVA: 0x00118B9C File Offset: 0x00116D9C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_LoginBonusStatus(jdata);
		this.GetResponse_StartTime(jdata);
		this.GetResponse_EndTime(jdata);
		this.GetResponse_LoginBonusRewardList(jdata);
		this.GetResponse_FirstLoginBonusRewardList(jdata);
		this.GetResponse_RewardId(jdata);
		this.GetResponse_RewardDays(jdata);
		this.GetResponse_FirstRewardDays(jdata);
	}

	// Token: 0x06003229 RID: 12841 RVA: 0x00118BE4 File Offset: 0x00116DE4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x0600322A RID: 12842 RVA: 0x00118BE8 File Offset: 0x00116DE8
	// (set) Token: 0x0600322B RID: 12843 RVA: 0x00118BF0 File Offset: 0x00116DF0
	public ServerLoginBonusState loginBonusState { get; private set; }

	// Token: 0x170006CC RID: 1740
	// (get) Token: 0x0600322C RID: 12844 RVA: 0x00118BFC File Offset: 0x00116DFC
	// (set) Token: 0x0600322D RID: 12845 RVA: 0x00118C04 File Offset: 0x00116E04
	public DateTime startTime { get; private set; }

	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x0600322E RID: 12846 RVA: 0x00118C10 File Offset: 0x00116E10
	// (set) Token: 0x0600322F RID: 12847 RVA: 0x00118C18 File Offset: 0x00116E18
	public DateTime endTime { get; private set; }

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x06003230 RID: 12848 RVA: 0x00118C24 File Offset: 0x00116E24
	// (set) Token: 0x06003231 RID: 12849 RVA: 0x00118C2C File Offset: 0x00116E2C
	public List<ServerLoginBonusReward> loginBonusRewardList { get; private set; }

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x06003232 RID: 12850 RVA: 0x00118C38 File Offset: 0x00116E38
	// (set) Token: 0x06003233 RID: 12851 RVA: 0x00118C40 File Offset: 0x00116E40
	public List<ServerLoginBonusReward> firstLoginBonusRewardList { get; private set; }

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x06003234 RID: 12852 RVA: 0x00118C4C File Offset: 0x00116E4C
	// (set) Token: 0x06003235 RID: 12853 RVA: 0x00118C54 File Offset: 0x00116E54
	public int rewardId { get; private set; }

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06003236 RID: 12854 RVA: 0x00118C60 File Offset: 0x00116E60
	// (set) Token: 0x06003237 RID: 12855 RVA: 0x00118C68 File Offset: 0x00116E68
	public int rewardDays { get; private set; }

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06003238 RID: 12856 RVA: 0x00118C74 File Offset: 0x00116E74
	// (set) Token: 0x06003239 RID: 12857 RVA: 0x00118C7C File Offset: 0x00116E7C
	public int firstRewardDays { get; private set; }

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x0600323A RID: 12858 RVA: 0x00118C88 File Offset: 0x00116E88
	public int resultLoginBonusRewardCount
	{
		get
		{
			if (this.loginBonusRewardList != null)
			{
				return this.loginBonusRewardList.Count;
			}
			return 0;
		}
	}

	// Token: 0x0600323B RID: 12859 RVA: 0x00118CA4 File Offset: 0x00116EA4
	private void GetResponse_LoginBonusStatus(JsonData jdata)
	{
		this.loginBonusState = new ServerLoginBonusState();
		JsonData jsonObject = NetUtil.GetJsonObject(jdata, "loginBonusStatus");
		if (jsonObject != null)
		{
			this.loginBonusState = new ServerLoginBonusState
			{
				m_numLogin = NetUtil.GetJsonInt(jsonObject, "numLogin"),
				m_numBonus = NetUtil.GetJsonInt(jsonObject, "numBonus"),
				m_lastBonusTime = NetUtil.GetLocalDateTime((long)NetUtil.GetJsonInt(jsonObject, "lastBonusTime"))
			};
		}
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x00118D14 File Offset: 0x00116F14
	private void GetResponse_StartTime(JsonData jdata)
	{
		this.startTime = NetUtil.GetLocalDateTime((long)NetUtil.GetJsonInt(jdata, "startTime"));
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x00118D30 File Offset: 0x00116F30
	private void GetResponse_EndTime(JsonData jdata)
	{
		this.endTime = NetUtil.GetLocalDateTime((long)NetUtil.GetJsonInt(jdata, "endTime"));
	}

	// Token: 0x0600323E RID: 12862 RVA: 0x00118D4C File Offset: 0x00116F4C
	private void GetResponse_LoginBonusRewardList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "loginBonusRewardList");
		this.loginBonusRewardList = new List<ServerLoginBonusReward>();
		if (jsonArray == null)
		{
			return;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerLoginBonusReward serverLoginBonusReward = new ServerLoginBonusReward();
			JsonData jsonArray2 = NetUtil.GetJsonArray(jdata2, "selectRewardList");
			int count2 = jsonArray2.Count;
			for (int j = 0; j < count2; j++)
			{
				JsonData jsonArray3 = NetUtil.GetJsonArray(jsonArray2[j], "itemList");
				int count3 = jsonArray3.Count;
				for (int k = 0; k < count3; k++)
				{
					serverLoginBonusReward.m_itemList.Add(NetUtil.AnalyzeItemStateJson(jsonArray3[k], string.Empty));
				}
			}
			this.loginBonusRewardList.Add(serverLoginBonusReward);
		}
	}

	// Token: 0x0600323F RID: 12863 RVA: 0x00118E30 File Offset: 0x00117030
	private void GetResponse_FirstLoginBonusRewardList(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "firstLoginBonusRewardList");
		this.firstLoginBonusRewardList = new List<ServerLoginBonusReward>();
		if (jsonArray == null)
		{
			return;
		}
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerLoginBonusReward serverLoginBonusReward = new ServerLoginBonusReward();
			JsonData jsonArray2 = NetUtil.GetJsonArray(jdata2, "selectRewardList");
			int count2 = jsonArray2.Count;
			for (int j = 0; j < count2; j++)
			{
				JsonData jsonArray3 = NetUtil.GetJsonArray(jsonArray2[j], "itemList");
				int count3 = jsonArray3.Count;
				for (int k = 0; k < count3; k++)
				{
					serverLoginBonusReward.m_itemList.Add(NetUtil.AnalyzeItemStateJson(jsonArray3[k], string.Empty));
				}
			}
			this.firstLoginBonusRewardList.Add(serverLoginBonusReward);
		}
	}

	// Token: 0x06003240 RID: 12864 RVA: 0x00118F14 File Offset: 0x00117114
	private void GetResponse_RewardId(JsonData jdata)
	{
		this.rewardId = NetUtil.GetJsonInt(jdata, "rewardId");
	}

	// Token: 0x06003241 RID: 12865 RVA: 0x00118F28 File Offset: 0x00117128
	private void GetResponse_RewardDays(JsonData jdata)
	{
		this.rewardDays = NetUtil.GetJsonInt(jdata, "rewardDays");
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x00118F3C File Offset: 0x0011713C
	private void GetResponse_FirstRewardDays(JsonData jdata)
	{
		this.firstRewardDays = NetUtil.GetJsonInt(jdata, "firstRewardDays");
	}
}
