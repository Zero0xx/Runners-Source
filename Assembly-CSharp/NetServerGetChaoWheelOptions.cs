using System;
using LitJson;

// Token: 0x02000699 RID: 1689
public class NetServerGetChaoWheelOptions : NetBase
{
	// Token: 0x06002D8F RID: 11663 RVA: 0x0010FF44 File Offset: 0x0010E144
	protected override void DoRequest()
	{
		base.SetAction("Chao/getChaoWheelOptions");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06002D90 RID: 11664 RVA: 0x0010FF7C File Offset: 0x0010E17C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_ChaoWheelOptions(jdata);
	}

	// Token: 0x06002D91 RID: 11665 RVA: 0x0010FF88 File Offset: 0x0010E188
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0010FF8C File Offset: 0x0010E18C
	// (set) Token: 0x06002D93 RID: 11667 RVA: 0x0010FF94 File Offset: 0x0010E194
	public ServerChaoWheelOptions resultChaoWheelOptions { get; private set; }

	// Token: 0x06002D94 RID: 11668 RVA: 0x0010FFA0 File Offset: 0x0010E1A0
	private void GetResponse_ChaoWheelOptions(JsonData jdata)
	{
		if (NetUtil.IsExist(jdata, "chaoWheelOptions"))
		{
			this.resultChaoWheelOptions = NetUtil.AnalyzeChaoWheelOptionsJson(jdata, "chaoWheelOptions");
		}
		else
		{
			this.resultChaoWheelOptions = NetUtil.AnalyzeChaoWheelOptionsJson(jdata, "wheelOptions");
		}
	}
}
