using System;
using LitJson;

// Token: 0x020006EA RID: 1770
public class NetServerGetEventUserRaidBossState : NetBase
{
	// Token: 0x06002F9B RID: 12187 RVA: 0x001136B4 File Offset: 0x001118B4
	public NetServerGetEventUserRaidBossState(int eventId)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x001136C4 File Offset: 0x001118C4
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventUserRaidboss");
		base.WriteActionParamValue("eventId", this.m_eventId);
	}

	// Token: 0x06002F9D RID: 12189 RVA: 0x001136E8 File Offset: 0x001118E8
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_userRaidBossState = NetUtil.AnalyzeEventUserRaidBossState(jdata);
	}

	// Token: 0x06002F9E RID: 12190 RVA: 0x001136F8 File Offset: 0x001118F8
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x06002F9F RID: 12191 RVA: 0x001136FC File Offset: 0x001118FC
	public ServerEventUserRaidBossState UserRaidBossState
	{
		get
		{
			return this.m_userRaidBossState;
		}
	}

	// Token: 0x04002A94 RID: 10900
	private int m_eventId;

	// Token: 0x04002A95 RID: 10901
	private ServerEventUserRaidBossState m_userRaidBossState;
}
