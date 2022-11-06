using System;
using Mission;

// Token: 0x02000299 RID: 665
public class MissionTypeParam
{
	// Token: 0x0600122F RID: 4655 RVA: 0x00065E90 File Offset: 0x00064090
	public MissionTypeParam(EventID eventID, MissionCategory category)
	{
		this.m_eventID = eventID;
		this.m_category = category;
	}

	// Token: 0x04001060 RID: 4192
	public EventID m_eventID;

	// Token: 0x04001061 RID: 4193
	public MissionCategory m_category;
}
