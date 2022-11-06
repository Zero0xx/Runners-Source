using System;
using LitJson;

// Token: 0x020006D4 RID: 1748
public class NetDebugForceDrawRaidboss : NetBase
{
	// Token: 0x06002EB2 RID: 11954 RVA: 0x00112064 File Offset: 0x00110264
	public NetDebugForceDrawRaidboss() : this(0, 0L)
	{
	}

	// Token: 0x06002EB3 RID: 11955 RVA: 0x00112070 File Offset: 0x00110270
	public NetDebugForceDrawRaidboss(int eventId, long score)
	{
		this.paramEventId = eventId;
		this.paramScore = score;
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x00112088 File Offset: 0x00110288
	protected override void DoRequest()
	{
		base.SetAction("Debug/forceDrawRaidboss");
		this.SetParameter_User();
	}

	// Token: 0x06002EB5 RID: 11957 RVA: 0x0011209C File Offset: 0x0011029C
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_raidBossState = NetUtil.AnalyzeRaidBossState(jdata);
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x001120AC File Offset: 0x001102AC
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x001120B0 File Offset: 0x001102B0
	// (set) Token: 0x06002EB8 RID: 11960 RVA: 0x001120B8 File Offset: 0x001102B8
	public int paramEventId { get; set; }

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x001120C4 File Offset: 0x001102C4
	// (set) Token: 0x06002EBA RID: 11962 RVA: 0x001120CC File Offset: 0x001102CC
	public long paramScore { get; set; }

	// Token: 0x06002EBB RID: 11963 RVA: 0x001120D8 File Offset: 0x001102D8
	private void SetParameter_User()
	{
		base.WriteActionParamValue("eventId", this.paramEventId);
		base.WriteActionParamValue("score", this.paramScore);
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x06002EBC RID: 11964 RVA: 0x00112114 File Offset: 0x00110314
	public ServerEventRaidBossState RaidBossState
	{
		get
		{
			return this.m_raidBossState;
		}
	}

	// Token: 0x04002A4F RID: 10831
	private ServerEventRaidBossState m_raidBossState;
}
