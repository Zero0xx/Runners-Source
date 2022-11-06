using System;
using LitJson;

// Token: 0x02000798 RID: 1944
public class NetServerGetWheelOptions : NetBase
{
	// Token: 0x060033A0 RID: 13216 RVA: 0x0011BE88 File Offset: 0x0011A088
	protected override void DoRequest()
	{
		base.SetAction("Spin/getWheelOptions");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x0011BEC0 File Offset: 0x0011A0C0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_WheelOptions(jdata);
	}

	// Token: 0x060033A2 RID: 13218 RVA: 0x0011BECC File Offset: 0x0011A0CC
	protected override void DoDidSuccessEmulation()
	{
		this.resultWheelOptions = ServerInterface.WheelOptions;
		this.resultWheelOptions.RefreshFakeState();
	}

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x060033A3 RID: 13219 RVA: 0x0011BEE4 File Offset: 0x0011A0E4
	// (set) Token: 0x060033A4 RID: 13220 RVA: 0x0011BEEC File Offset: 0x0011A0EC
	public ServerWheelOptions resultWheelOptions { get; private set; }

	// Token: 0x060033A5 RID: 13221 RVA: 0x0011BEF8 File Offset: 0x0011A0F8
	private void GetResponse_WheelOptions(JsonData jdata)
	{
		this.resultWheelOptions = NetUtil.AnalyzeWheelOptionsJson(jdata, "wheelOptions");
	}
}
