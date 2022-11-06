using System;
using UnityEngine;

// Token: 0x0200093E RID: 2366
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmPotosMoveMetal")]
public class ObjEnmPotosMoveMetal : ObjEnmPotosMove
{
	// Token: 0x06003DDC RID: 15836 RVA: 0x001425D0 File Offset: 0x001407D0
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
