using System;
using UnityEngine;

// Token: 0x0200093C RID: 2364
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmPotosMetal")]
public class ObjEnmPotosMetal : ObjEnmPotos
{
	// Token: 0x06003DD6 RID: 15830 RVA: 0x001425A8 File Offset: 0x001407A8
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
