using System;
using UnityEngine;

// Token: 0x02000946 RID: 2374
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmValkyneMetal")]
public class ObjEnmValkyneMetal : ObjEnmValkyne
{
	// Token: 0x06003DF7 RID: 15863 RVA: 0x001426C8 File Offset: 0x001408C8
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
