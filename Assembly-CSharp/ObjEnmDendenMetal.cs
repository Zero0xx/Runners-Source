using System;
using UnityEngine;

// Token: 0x02000923 RID: 2339
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmDendenMetal")]
public class ObjEnmDendenMetal : ObjEnmDenden
{
	// Token: 0x06003D6E RID: 15726 RVA: 0x00141894 File Offset: 0x0013FA94
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
