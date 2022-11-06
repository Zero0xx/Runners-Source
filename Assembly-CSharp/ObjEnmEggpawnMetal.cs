using System;
using UnityEngine;

// Token: 0x02000926 RID: 2342
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmEggpawnMetal")]
public class ObjEnmEggpawnMetal : ObjEnmEggpawn
{
	// Token: 0x06003D78 RID: 15736 RVA: 0x001418F8 File Offset: 0x0013FAF8
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
