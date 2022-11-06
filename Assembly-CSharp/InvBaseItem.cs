using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000054 RID: 84
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000139 RID: 313
	public int id16;

	// Token: 0x0400013A RID: 314
	public string name;

	// Token: 0x0400013B RID: 315
	public string description;

	// Token: 0x0400013C RID: 316
	public InvBaseItem.Slot slot;

	// Token: 0x0400013D RID: 317
	public int minItemLevel = 1;

	// Token: 0x0400013E RID: 318
	public int maxItemLevel = 50;

	// Token: 0x0400013F RID: 319
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x04000140 RID: 320
	public GameObject attachment;

	// Token: 0x04000141 RID: 321
	public Color color = Color.white;

	// Token: 0x04000142 RID: 322
	public UIAtlas iconAtlas;

	// Token: 0x04000143 RID: 323
	public string iconName = string.Empty;

	// Token: 0x02000055 RID: 85
	public enum Slot
	{
		// Token: 0x04000145 RID: 325
		None,
		// Token: 0x04000146 RID: 326
		Weapon,
		// Token: 0x04000147 RID: 327
		Shield,
		// Token: 0x04000148 RID: 328
		Body,
		// Token: 0x04000149 RID: 329
		Shoulders,
		// Token: 0x0400014A RID: 330
		Bracers,
		// Token: 0x0400014B RID: 331
		Boots,
		// Token: 0x0400014C RID: 332
		Trinket,
		// Token: 0x0400014D RID: 333
		_LastDoNotUse
	}
}
