using System;
using UnityEngine;

// Token: 0x02000929 RID: 2345
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmGaniganiMetal")]
public class ObjEnmGaniganiMetal : ObjEnmGanigani
{
	// Token: 0x06003D84 RID: 15748 RVA: 0x0014197C File Offset: 0x0013FB7C
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
