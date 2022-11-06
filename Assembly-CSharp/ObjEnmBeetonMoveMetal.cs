using System;
using UnityEngine;

// Token: 0x02000920 RID: 2336
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmBeetonMoveMetal")]
public class ObjEnmBeetonMoveMetal : ObjEnmBeetonMove
{
	// Token: 0x06003D64 RID: 15716 RVA: 0x00141830 File Offset: 0x0013FA30
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
