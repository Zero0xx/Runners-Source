using System;
using LitJson;

// Token: 0x02000759 RID: 1881
public class NetServerReLogin : NetBase
{
	// Token: 0x06003275 RID: 12917 RVA: 0x00119570 File Offset: 0x00117770
	protected override void DoRequest()
	{
		base.SetAction("Login/reLogin");
		CPlusPlusLink instance = CPlusPlusLink.Instance;
		if (instance != null)
		{
			string onlySendBaseParamString = instance.GetOnlySendBaseParamString();
			base.WriteJsonString(onlySendBaseParamString);
		}
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x001195A8 File Offset: 0x001177A8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_SessionId(jdata);
	}

	// Token: 0x06003277 RID: 12919 RVA: 0x001195B4 File Offset: 0x001177B4
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06003278 RID: 12920 RVA: 0x001195B8 File Offset: 0x001177B8
	// (set) Token: 0x06003279 RID: 12921 RVA: 0x001195C0 File Offset: 0x001177C0
	public string resultSessionId { get; private set; }

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x0600327A RID: 12922 RVA: 0x001195CC File Offset: 0x001177CC
	// (set) Token: 0x0600327B RID: 12923 RVA: 0x001195D4 File Offset: 0x001177D4
	public int sessionTimeLimit { get; private set; }

	// Token: 0x0600327C RID: 12924 RVA: 0x001195E0 File Offset: 0x001177E0
	private void GetResponse_SessionId(JsonData jdata)
	{
		this.resultSessionId = NetUtil.GetJsonString(jdata, "sessionId");
		this.sessionTimeLimit = NetUtil.GetJsonInt(jdata, "sessionTimeLimit");
	}
}
