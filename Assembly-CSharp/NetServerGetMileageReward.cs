using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200071C RID: 1820
public class NetServerGetMileageReward : NetBase
{
	// Token: 0x0600308A RID: 12426 RVA: 0x00114FE8 File Offset: 0x001131E8
	public NetServerGetMileageReward(int episode, int chapter)
	{
		this.m_episode = episode;
		this.m_chapter = chapter;
		this.m_rewardList = new List<ServerMileageReward>();
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x0011500C File Offset: 0x0011320C
	protected override void DoRequest()
	{
		base.SetAction("Game/getMileageReward");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getMileageRewardString = instance.GetGetMileageRewardString(this.m_episode, this.m_chapter);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getMileageRewardString);
		}
	}

	// Token: 0x0600308C RID: 12428 RVA: 0x0011505C File Offset: 0x0011325C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_MileageReward(jdata);
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x00115068 File Offset: 0x00113268
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x0600308E RID: 12430 RVA: 0x0011506C File Offset: 0x0011326C
	private void SetParameter_EpisodeChapter()
	{
		base.WriteActionParamValue("episode", this.m_episode);
		base.WriteActionParamValue("chapter", this.m_chapter);
	}

	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x0600308F RID: 12431 RVA: 0x001150A8 File Offset: 0x001132A8
	// (set) Token: 0x06003090 RID: 12432 RVA: 0x001150B0 File Offset: 0x001132B0
	public List<ServerMileageReward> m_rewardList { get; private set; }

	// Token: 0x06003091 RID: 12433 RVA: 0x001150BC File Offset: 0x001132BC
	private void GetResponse_MileageReward(JsonData jdata)
	{
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "mileageMapRewardList");
		if (jsonArray == null)
		{
			return;
		}
		for (int i = 0; i < jsonArray.Count; i++)
		{
			ServerMileageReward serverMileageReward = this.AnalyzeMileageRewardJson(jsonArray[i]);
			serverMileageReward.m_startTime = ServerInterface.MileageMapState.m_chapterStartTime;
			this.m_rewardList.Add(serverMileageReward);
		}
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x00115120 File Offset: 0x00113320
	private ServerMileageReward AnalyzeMileageRewardJson(JsonData jdata)
	{
		if (jdata == null)
		{
			return null;
		}
		ServerMileageReward serverMileageReward = new ServerMileageReward();
		if (serverMileageReward != null)
		{
			serverMileageReward.m_episode = this.m_episode;
			serverMileageReward.m_chapter = this.m_chapter;
			serverMileageReward.m_type = NetUtil.GetJsonInt(jdata, "type");
			serverMileageReward.m_point = NetUtil.GetJsonInt(jdata, "point");
			serverMileageReward.m_itemId = NetUtil.GetJsonInt(jdata, "itemId");
			serverMileageReward.m_numItem = NetUtil.GetJsonInt(jdata, "numItem");
			serverMileageReward.m_limitTime = NetUtil.GetJsonInt(jdata, "limitTime");
		}
		return serverMileageReward;
	}

	// Token: 0x04002AF0 RID: 10992
	private int m_episode;

	// Token: 0x04002AF1 RID: 10993
	private int m_chapter;
}
