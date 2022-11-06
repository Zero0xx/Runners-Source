using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E6 RID: 1766
public class NetServerGetEventRaidBossUserList : NetBase
{
	// Token: 0x06002F82 RID: 12162 RVA: 0x0011348C File Offset: 0x0011168C
	public NetServerGetEventRaidBossUserList(int eventId, long raidBossId)
	{
		this.m_eventId = eventId;
		this.m_raidBossId = raidBossId;
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x001134A4 File Offset: 0x001116A4
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventRaidbossUserList");
		base.WriteActionParamValue("raidbossId", this.m_raidBossId);
		base.WriteActionParamValue("eventId", this.m_eventId);
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x001134E0 File Offset: 0x001116E0
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_raidBossUserStateList = NetUtil.AnalyzeEventRaidBossUserStateList(jdata);
		this.m_raidBossBonus = NetUtil.AnalyzeEventRaidBossBonus(jdata);
		this.m_raidBossState = NetUtil.AnalyzeRaidBossState(jdata);
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x00113514 File Offset: 0x00111714
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x06002F86 RID: 12166 RVA: 0x00113518 File Offset: 0x00111718
	public List<ServerEventRaidBossUserState> RaidBossUserStateList
	{
		get
		{
			return this.m_raidBossUserStateList;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x06002F87 RID: 12167 RVA: 0x00113520 File Offset: 0x00111720
	public ServerEventRaidBossBonus RaidBossBonus
	{
		get
		{
			return this.m_raidBossBonus;
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00113528 File Offset: 0x00111728
	public ServerEventRaidBossState RaidBossState
	{
		get
		{
			return this.m_raidBossState;
		}
	}

	// Token: 0x04002A88 RID: 10888
	private int m_eventId;

	// Token: 0x04002A89 RID: 10889
	private long m_raidBossId;

	// Token: 0x04002A8A RID: 10890
	private List<ServerEventRaidBossUserState> m_raidBossUserStateList;

	// Token: 0x04002A8B RID: 10891
	private ServerEventRaidBossBonus m_raidBossBonus;

	// Token: 0x04002A8C RID: 10892
	private ServerEventRaidBossState m_raidBossState;
}
