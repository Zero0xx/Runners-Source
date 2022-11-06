using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E7 RID: 1767
public class NetServerGetEventReward : NetBase
{
	// Token: 0x06002F89 RID: 12169 RVA: 0x00113530 File Offset: 0x00111730
	public NetServerGetEventReward(int eventId)
	{
		this.paramEventId = eventId;
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x00113540 File Offset: 0x00111740
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventReward");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string eventRewardString = instance.GetEventRewardString(this.paramEventId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(eventRewardString);
		}
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x00113588 File Offset: 0x00111788
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_EventRewardList(jdata);
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x00113594 File Offset: 0x00111794
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x00113598 File Offset: 0x00111798
	private void SetParameter_EventId()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x001135B0 File Offset: 0x001117B0
	private void GetResponse_EventRewardList(JsonData jdata)
	{
		this.resultEventRewardList = NetUtil.AnalyzeEventReward(jdata);
	}

	// Token: 0x04002A8D RID: 10893
	public int paramEventId;

	// Token: 0x04002A8E RID: 10894
	public List<ServerEventReward> resultEventRewardList;
}
