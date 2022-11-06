using System;
using UnityEngine;

// Token: 0x02000941 RID: 2369
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmSpinaMetal")]
public class ObjEnmSpinaMetal : ObjEnmSpina
{
	// Token: 0x06003DE6 RID: 15846 RVA: 0x00142634 File Offset: 0x00140834
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
