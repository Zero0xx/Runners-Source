using System;
using System.Collections.Generic;

// Token: 0x020002E2 RID: 738
public class ResultData
{
	// Token: 0x040012C5 RID: 4805
	public bool m_validResult;

	// Token: 0x040012C6 RID: 4806
	public string m_stageName;

	// Token: 0x040012C7 RID: 4807
	public bool m_bossStage;

	// Token: 0x040012C8 RID: 4808
	public bool m_bossDestroy;

	// Token: 0x040012C9 RID: 4809
	public bool m_rivalHighScore;

	// Token: 0x040012CA RID: 4810
	public bool m_fromOptionTutorial;

	// Token: 0x040012CB RID: 4811
	public bool m_eventStage;

	// Token: 0x040012CC RID: 4812
	public bool m_quickMode;

	// Token: 0x040012CD RID: 4813
	public bool m_missionComplete;

	// Token: 0x040012CE RID: 4814
	public MileageMapState m_oldMapState;

	// Token: 0x040012CF RID: 4815
	public MileageMapState m_newMapState;

	// Token: 0x040012D0 RID: 4816
	public List<ServerMileageIncentive> m_mileageIncentiveList;

	// Token: 0x040012D1 RID: 4817
	public List<ServerItemState> m_dailyMissionIncentiveList;

	// Token: 0x040012D2 RID: 4818
	public long m_highScore;

	// Token: 0x040012D3 RID: 4819
	public long m_totalScore;
}
