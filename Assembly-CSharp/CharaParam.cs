using System;
using SaveData;

// Token: 0x02000255 RID: 597
public class CharaParam
{
	// Token: 0x06001052 RID: 4178 RVA: 0x0005FB5C File Offset: 0x0005DD5C
	public CharaParam(SystemData.CharaTutorialFlagStatus flagStatus, HudTutorial.Id tutorialID)
	{
		this.m_flagStatus = flagStatus;
		this.m_tutorialID = tutorialID;
	}

	// Token: 0x04000EAF RID: 3759
	public SystemData.CharaTutorialFlagStatus m_flagStatus;

	// Token: 0x04000EB0 RID: 3760
	public HudTutorial.Id m_tutorialID;
}
