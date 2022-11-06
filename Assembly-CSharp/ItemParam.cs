using System;
using SaveData;

// Token: 0x0200025B RID: 603
public class ItemParam
{
	// Token: 0x0600105A RID: 4186 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
	public ItemParam(string name, string objName, SystemData.ItemTutorialFlagStatus flagStatus, HudTutorial.Id tutorialID)
	{
		this.m_name = name;
		this.m_objName = objName;
		this.m_flagStatus = flagStatus;
		this.m_tutorialID = tutorialID;
	}

	// Token: 0x04000ED4 RID: 3796
	public string m_name;

	// Token: 0x04000ED5 RID: 3797
	public string m_objName;

	// Token: 0x04000ED6 RID: 3798
	public SystemData.ItemTutorialFlagStatus m_flagStatus;

	// Token: 0x04000ED7 RID: 3799
	public HudTutorial.Id m_tutorialID;
}
