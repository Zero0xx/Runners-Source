using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E4 RID: 1764
public class NetServerGetEventList : NetBase
{
	// Token: 0x06002F79 RID: 12153 RVA: 0x001132D0 File Offset: 0x001114D0
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventList");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x00113308 File Offset: 0x00111508
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_EventList(jdata);
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x00113314 File Offset: 0x00111514
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002F7C RID: 12156 RVA: 0x00113318 File Offset: 0x00111518
	private void GetResponse_EventList(JsonData jdata)
	{
		this.resultEventList = NetUtil.AnalyzeEventList(jdata);
	}

	// Token: 0x04002A83 RID: 10883
	public List<ServerEventEntry> resultEventList;
}
