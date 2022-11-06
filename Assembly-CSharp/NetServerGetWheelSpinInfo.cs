using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x0200079A RID: 1946
public class NetServerGetWheelSpinInfo : NetBase
{
	// Token: 0x060033AF RID: 13231 RVA: 0x0011BFF0 File Offset: 0x0011A1F0
	protected override void DoRequest()
	{
		base.SetAction("Spin/getWheelSpinInfo");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x060033B0 RID: 13232 RVA: 0x0011C028 File Offset: 0x0011A228
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_WheelSpinInfo(jdata);
	}

	// Token: 0x060033B1 RID: 13233 RVA: 0x0011C034 File Offset: 0x0011A234
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x060033B2 RID: 13234 RVA: 0x0011C038 File Offset: 0x0011A238
	// (set) Token: 0x060033B3 RID: 13235 RVA: 0x0011C040 File Offset: 0x0011A240
	public List<ServerWheelSpinInfo> resultWheelSpinInfos { get; private set; }

	// Token: 0x060033B4 RID: 13236 RVA: 0x0011C04C File Offset: 0x0011A24C
	private void GetResponse_WheelSpinInfo(JsonData jdata)
	{
		this.resultWheelSpinInfos = NetUtil.AnalyzeWheelSpinInfo(jdata, "infoList");
	}
}
