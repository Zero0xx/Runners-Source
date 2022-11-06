using System;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class PlaceObjectData
{
	// Token: 0x06001BCA RID: 7114 RVA: 0x000A5338 File Offset: 0x000A3538
	public PlaceObjectData(int _x, int _y, int _count, Bounds _b)
	{
		this.x = _x;
		this.y = _y;
		this.count = _count;
		this.bound = _b;
	}

	// Token: 0x0400197B RID: 6523
	public int x;

	// Token: 0x0400197C RID: 6524
	public int y;

	// Token: 0x0400197D RID: 6525
	public int count;

	// Token: 0x0400197E RID: 6526
	public Bounds bound;
}
