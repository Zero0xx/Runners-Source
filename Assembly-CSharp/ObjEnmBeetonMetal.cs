using System;
using UnityEngine;

// Token: 0x0200091E RID: 2334
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmBeetonMetal")]
public class ObjEnmBeetonMetal : ObjEnmBeeton
{
	// Token: 0x06003D5E RID: 15710 RVA: 0x00141808 File Offset: 0x0013FA08
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
