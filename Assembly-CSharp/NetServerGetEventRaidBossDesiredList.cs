using System;
using System.Collections.Generic;
using LitJson;

// Token: 0x020006E5 RID: 1765
public class NetServerGetEventRaidBossDesiredList : NetBase
{
	// Token: 0x06002F7D RID: 12157 RVA: 0x00113328 File Offset: 0x00111528
	public NetServerGetEventRaidBossDesiredList(int eventId, long raidBossId, List<string> friendIdList)
	{
		this.m_eventId = eventId;
		this.m_raidBossId = raidBossId;
		if (friendIdList != null)
		{
			foreach (string item in friendIdList)
			{
				this.m_friendIdList.Add(item);
			}
		}
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x001133B4 File Offset: 0x001115B4
	protected override void DoRequest()
	{
		base.SetAction("Event/getEventRaidbossDesiredList");
		base.WriteActionParamValue("raidbossId", this.m_raidBossId);
		base.WriteActionParamValue("eventId", this.m_eventId);
		List<object> list = new List<object>();
		foreach (string text in this.m_friendIdList)
		{
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
		}
		base.WriteActionParamArray("friendIdList", list);
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x00113470 File Offset: 0x00111670
	protected override void DoDidSuccess(JsonData jdata)
	{
		this.m_desiredList = NetUtil.AnalyzeEventRaidbossDesiredList(jdata);
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x00113480 File Offset: 0x00111680
	protected override void DoDidSuccessEmulation()
	{
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x06002F81 RID: 12161 RVA: 0x00113484 File Offset: 0x00111684
	public List<ServerEventRaidBossDesiredState> DesiredList
	{
		get
		{
			return this.m_desiredList;
		}
	}

	// Token: 0x04002A84 RID: 10884
	private int m_eventId;

	// Token: 0x04002A85 RID: 10885
	private long m_raidBossId;

	// Token: 0x04002A86 RID: 10886
	private List<string> m_friendIdList = new List<string>();

	// Token: 0x04002A87 RID: 10887
	private List<ServerEventRaidBossDesiredState> m_desiredList;
}
