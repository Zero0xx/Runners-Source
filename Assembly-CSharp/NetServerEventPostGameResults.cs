using System;
using LitJson;

// Token: 0x020006E1 RID: 1761
public class NetServerEventPostGameResults : NetBase
{
	// Token: 0x06002F40 RID: 12096 RVA: 0x00112B1C File Offset: 0x00110D1C
	public NetServerEventPostGameResults(int eventId, int numRaidBossRings)
	{
		this.m_eventId = eventId;
		this.m_numRaidBossRings = numRaidBossRings;
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x00112B3C File Offset: 0x00110D3C
	protected override void DoRequest()
	{
		base.SetAction("Event/eventPostGameResults");
		this.SetParameter_EventId();
		this.SetParameter_RaidBossRings();
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x00112B58 File Offset: 0x00110D58
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.GetResponse_EventUserRaidBossState(jdata);
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x00112B64 File Offset: 0x00110D64
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x00112B68 File Offset: 0x00110D68
	private void SetParameter_EventId()
	{
		base.WriteActionParamValue("eventId", this.m_eventId);
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x00112B80 File Offset: 0x00110D80
	private void SetParameter_RaidBossRings()
	{
		base.WriteActionParamValue("numRaidbossRings", this.m_numRaidBossRings);
	}

	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x06002F46 RID: 12102 RVA: 0x00112B98 File Offset: 0x00110D98
	public ServerEventUserRaidBossState UserRaidBossState
	{
		get
		{
			return this.m_userRaidBossState;
		}
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x00112BA0 File Offset: 0x00110DA0
	private void GetResponse_EventUserRaidBossState(JsonData jdata)
	{
		this.m_userRaidBossState = NetUtil.AnalyzeEventUserRaidBossState(jdata);
	}

	// Token: 0x04002A6D RID: 10861
	private int m_eventId = -1;

	// Token: 0x04002A6E RID: 10862
	private int m_numRaidBossRings;

	// Token: 0x04002A6F RID: 10863
	private ServerEventUserRaidBossState m_userRaidBossState;
}
