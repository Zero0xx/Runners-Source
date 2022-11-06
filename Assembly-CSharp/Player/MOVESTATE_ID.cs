using System;

namespace Player
{
	// Token: 0x0200097F RID: 2431
	public enum MOVESTATE_ID
	{
		// Token: 0x040036B8 RID: 14008
		Edit = 1,
		// Token: 0x040036B9 RID: 14009
		Run,
		// Token: 0x040036BA RID: 14010
		Air,
		// Token: 0x040036BB RID: 14011
		IgnoreCollision,
		// Token: 0x040036BC RID: 14012
		RunOnPath,
		// Token: 0x040036BD RID: 14013
		GoTarget,
		// Token: 0x040036BE RID: 14014
		RunOnPathPhantom,
		// Token: 0x040036BF RID: 14015
		GoTargetBoss,
		// Token: 0x040036C0 RID: 14016
		RunOnPathPhantomDrill,
		// Token: 0x040036C1 RID: 14017
		Asteroid
	}
}
