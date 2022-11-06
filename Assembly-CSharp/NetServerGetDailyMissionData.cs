using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x02000718 RID: 1816
public class NetServerGetDailyMissionData : NetBase
{
	// Token: 0x0600303D RID: 12349 RVA: 0x00114730 File Offset: 0x00112930
	protected override void DoRequest()
	{
		base.SetAction("Game/getDailyChalData");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x0600303E RID: 12350 RVA: 0x00114768 File Offset: 0x00112968
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_IncentiveList(jdata);
		this.GetResponse_NumContinue(jdata);
		this.GetResponse_NumDailyChalDay(jdata);
		this.GetResponse_MaxDailyChalDay(jdata);
		this.GetResponse_MaxIncentive(jdata);
		this.GetResponse_ChalEndTime(jdata);
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x001147A0 File Offset: 0x001129A0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x06003040 RID: 12352 RVA: 0x001147A4 File Offset: 0x001129A4
	// (set) Token: 0x06003041 RID: 12353 RVA: 0x001147AC File Offset: 0x001129AC
	public int resultNumContinue { get; private set; }

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x06003042 RID: 12354 RVA: 0x001147B8 File Offset: 0x001129B8
	public int resultIncentives
	{
		get
		{
			if (this.resultDailyMissionIncentiveList != null)
			{
				return this.resultDailyMissionIncentiveList.Count;
			}
			return 0;
		}
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x06003043 RID: 12355 RVA: 0x001147D4 File Offset: 0x001129D4
	// (set) Token: 0x06003044 RID: 12356 RVA: 0x001147DC File Offset: 0x001129DC
	public int resultNumDailyChalDay { get; private set; }

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x06003045 RID: 12357 RVA: 0x001147E8 File Offset: 0x001129E8
	// (set) Token: 0x06003046 RID: 12358 RVA: 0x001147F0 File Offset: 0x001129F0
	public int resultMaxDailyChalDay { get; private set; }

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x06003047 RID: 12359 RVA: 0x001147FC File Offset: 0x001129FC
	// (set) Token: 0x06003048 RID: 12360 RVA: 0x00114804 File Offset: 0x00112A04
	public int resultMaxIncentive { get; private set; }

	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x06003049 RID: 12361 RVA: 0x00114810 File Offset: 0x00112A10
	// (set) Token: 0x0600304A RID: 12362 RVA: 0x00114818 File Offset: 0x00112A18
	public DateTime resultChalEndTime { get; private set; }

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x0600304B RID: 12363 RVA: 0x00114824 File Offset: 0x00112A24
	// (set) Token: 0x0600304C RID: 12364 RVA: 0x0011482C File Offset: 0x00112A2C
	protected List<ServerDailyChallengeIncentive> resultDailyMissionIncentiveList { get; set; }

	// Token: 0x0600304D RID: 12365 RVA: 0x00114838 File Offset: 0x00112A38
	public ServerDailyChallengeIncentive GetResultDailyMissionIncentive(int index)
	{
		if (0 <= index && this.resultIncentives > index)
		{
			return this.resultDailyMissionIncentiveList[index];
		}
		return null;
	}

	// Token: 0x0600304E RID: 12366 RVA: 0x0011485C File Offset: 0x00112A5C
	private void GetResponse_IncentiveList(JsonData jdata)
	{
		this.resultDailyMissionIncentiveList = new List<ServerDailyChallengeIncentive>();
		JsonData jsonArray = NetUtil.GetJsonArray(jdata, "incentiveList");
		int count = jsonArray.Count;
		for (int i = 0; i < count; i++)
		{
			JsonData jdata2 = jsonArray[i];
			ServerDailyChallengeIncentive item = NetUtil.AnalyzeDailyMissionIncentiveJson(jdata2, string.Empty);
			this.resultDailyMissionIncentiveList.Add(item);
		}
	}

	// Token: 0x0600304F RID: 12367 RVA: 0x001148BC File Offset: 0x00112ABC
	private void GetResponse_NumContinue(JsonData jdata)
	{
		this.resultNumContinue = NetUtil.GetJsonInt(jdata, "numDailyChalCont");
	}

	// Token: 0x06003050 RID: 12368 RVA: 0x001148D0 File Offset: 0x00112AD0
	private void GetResponse_NumDailyChalDay(JsonData jdata)
	{
		this.resultNumDailyChalDay = NetUtil.GetJsonInt(jdata, "numDailyChalDay");
	}

	// Token: 0x06003051 RID: 12369 RVA: 0x001148E4 File Offset: 0x00112AE4
	private void GetResponse_MaxDailyChalDay(JsonData jdata)
	{
		this.resultMaxDailyChalDay = NetUtil.GetJsonInt(jdata, "maxDailyChalDay");
	}

	// Token: 0x06003052 RID: 12370 RVA: 0x001148F8 File Offset: 0x00112AF8
	private void GetResponse_MaxIncentive(JsonData jdata)
	{
		this.resultMaxIncentive = NetUtil.GetJsonInt(jdata, "incentiveListCont");
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x0011490C File Offset: 0x00112B0C
	private void GetResponse_ChalEndTime(JsonData jdata)
	{
		this.resultChalEndTime = NetUtil.GetLocalDateTime(NetUtil.GetJsonLong(jdata, "chalEndTime"));
		Debug.Log("resultChalEndTime:" + this.resultChalEndTime.ToString());
	}
}
