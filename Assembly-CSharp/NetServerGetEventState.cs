using System;
using LitJson;

// Token: 0x020006E8 RID: 1768
public class NetServerGetEventState : NetBase
{
	// Token: 0x06002F8F RID: 12175 RVA: 0x001135C0 File Offset: 0x001117C0
	public NetServerGetEventState(int eventId)
	{
		this.paramEventId = eventId;
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x001135D0 File Offset: 0x001117D0
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventState");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string getEventStateString = instance.GetGetEventStateString(this.paramEventId);
			Debug.Log("CPlusPlusLink.actRetry");
			base.WriteJsonString(getEventStateString);
		}
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x00113618 File Offset: 0x00111818
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_EventState(jdata);
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x00113624 File Offset: 0x00111824
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x00113628 File Offset: 0x00111828
	private void SetParameter_EventId()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x00113640 File Offset: 0x00111840
	private void GetResponse_EventState(JsonData jdata)
	{
		this.resultEventState = NetUtil.AnalyzeEventState(jdata);
	}

	// Token: 0x04002A8F RID: 10895
	public int paramEventId;

	// Token: 0x04002A90 RID: 10896
	public ServerEventState resultEventState;
}
