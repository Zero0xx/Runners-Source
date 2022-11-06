using System;

// Token: 0x0200010A RID: 266
public struct SendBaseNativeParam
{
	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0002EEFC File Offset: 0x0002D0FC
	// (set) Token: 0x060007EA RID: 2026 RVA: 0x0002EF04 File Offset: 0x0002D104
	public string sessionId { get; private set; }

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002EF10 File Offset: 0x0002D110
	// (set) Token: 0x060007EC RID: 2028 RVA: 0x0002EF18 File Offset: 0x0002D118
	public string version { get; private set; }

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x0002EF24 File Offset: 0x0002D124
	// (set) Token: 0x060007EE RID: 2030 RVA: 0x0002EF2C File Offset: 0x0002D12C
	public string seq { get; private set; }

	// Token: 0x060007EF RID: 2031 RVA: 0x0002EF38 File Offset: 0x0002D138
	public void Init()
	{
		ServerLoginState loginState = ServerInterface.LoginState;
		if (loginState.IsLoggedIn)
		{
			this.sessionId = loginState.sessionId;
		}
		else
		{
			this.sessionId = string.Empty;
		}
		this.version = CurrentBundleVersion.version;
		this.seq = (loginState.seqNum + 1UL).ToString();
	}
}
