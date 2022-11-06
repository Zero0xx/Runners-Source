using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E9 RID: 1769
public class NetServerGetEventUserRaidBossList : NetBase
{
	// Token: 0x06002F95 RID: 12181 RVA: 0x00113650 File Offset: 0x00111850
	public NetServerGetEventUserRaidBossList(int eventId)
	{
		this.m_eventId = eventId;
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x00113660 File Offset: 0x00111860
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventUserRaidbossList");
		base.WriteActionParamValue("eventId", this.m_eventId);
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x00113684 File Offset: 0x00111884
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_userRaidBossList = NetUtil.AnalyzeRaidBossStateList(jdata);
		this.m_userRaidBossState = NetUtil.AnalyzeEventUserRaidBossState(jdata);
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x001136A0 File Offset: 0x001118A0
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x06002F99 RID: 12185 RVA: 0x001136A4 File Offset: 0x001118A4
	public List<ServerEventRaidBossState> UserRaidBossList
	{
		get
		{
			return this.m_userRaidBossList;
		}
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x06002F9A RID: 12186 RVA: 0x001136AC File Offset: 0x001118AC
	public ServerEventUserRaidBossState UserRaidBossState
	{
		get
		{
			return this.m_userRaidBossState;
		}
	}

	// Token: 0x04002A91 RID: 10897
	private int m_eventId;

	// Token: 0x04002A92 RID: 10898
	private List<ServerEventRaidBossState> m_userRaidBossList;

	// Token: 0x04002A93 RID: 10899
	private ServerEventUserRaidBossState m_userRaidBossState;
}
