using System;
using UnityEngine;

// Token: 0x0200092F RID: 2351
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmNarlMetal")]
public class ObjEnmNarlMetal : ObjEnmNarl
{
	// Token: 0x06003D98 RID: 15768 RVA: 0x00141A44 File Offset: 0x0013FC44
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
