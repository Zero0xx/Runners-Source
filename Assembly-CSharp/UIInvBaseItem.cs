using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AC RID: 940
[Serializable]
public class UIInvBaseItem
{
	// Token: 0x04001903 RID: 6403
	public int id16;

	// Token: 0x04001904 RID: 6404
	public string name;

	// Token: 0x04001905 RID: 6405
	public string description;

	// Token: 0x04001906 RID: 6406
	public UIInvBaseItem.Slot slot;

	// Token: 0x04001907 RID: 6407
	public int minItemLevel = 1;

	// Token: 0x04001908 RID: 6408
	public int maxItemLevel = 50;

	// Token: 0x04001909 RID: 6409
	public List<UIInvStat> stats = new List<UIInvStat>();

	// Token: 0x0400190A RID: 6410
	public GameObject attachment;

	// Token: 0x0400190B RID: 6411
	public Color color = Color.white;

	// Token: 0x0400190C RID: 6412
	public UIAtlas iconAtlas;

	// Token: 0x0400190D RID: 6413
	public string iconName = string.Empty;

	// Token: 0x020003AD RID: 941
	public enum Slot
	{
		// Token: 0x0400190F RID: 6415
		None,
		// Token: 0x04001910 RID: 6416
		Weapon,
		// Token: 0x04001911 RID: 6417
		Shield,
		// Token: 0x04001912 RID: 6418
		Body,
		// Token: 0x04001913 RID: 6419
		Shoulders,
		// Token: 0x04001914 RID: 6420
		Bracers,
		// Token: 0x04001915 RID: 6421
		Boots,
		// Token: 0x04001916 RID: 6422
		Trinket,
		// Token: 0x04001917 RID: 6423
		_LastDoNotUse
	}
}
