using System;
using LitJson;

// Token: 0x02000715 RID: 1813
public class NetServerDrawRaidBoss : NetBase
{
	// Token: 0x0600302B RID: 12331 RVA: 0x001145EC File Offset: 0x001127EC
	public NetServerDrawRaidBoss(int eventId, long score)
	{
		this.m_eventId = eventId;
		this.m_score = score;
	}

	// Token: 0x0600302C RID: 12332 RVA: 0x00114604 File Offset: 0x00112804
	protected override void DoRequest()
	{
		base.SetAction("Game/drawRaidboss");
		base.WriteActionParamValue("eventId", this.m_eventId);
		base.WriteActionParamValue("score", this.m_score);
	}

	// Token: 0x0600302D RID: 12333 RVA: 0x00114640 File Offset: 0x00112840
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_raidBossState = NetUtil.AnalyzeRaidBossState(jdata);
	}

	// Token: 0x0600302E RID: 12334 RVA: 0x00114650 File Offset: 0x00112850
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x0600302F RID: 12335 RVA: 0x00114654 File Offset: 0x00112854
	public ServerEventRaidBossState RaidBossState
	{
		get
		{
			return this.m_raidBossState;
		}
	}

	// Token: 0x04002ACE RID: 10958
	private int m_eventId;

	// Token: 0x04002ACF RID: 10959
	private long m_score;

	// Token: 0x04002AD0 RID: 10960
	private ServerEventRaidBossState m_raidBossState;
}
