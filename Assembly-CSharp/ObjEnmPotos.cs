using System;
using UnityEngine;

// Token: 0x0200093A RID: 2362
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmPotos")]
public class ObjEnmPotos : ObjEnemySwing
{
	// Token: 0x06003DCE RID: 15822 RVA: 0x00142550 File Offset: 0x00140750
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DCF RID: 15823 RVA: 0x00142554 File Offset: 0x00140754
	protected override string[] GetModelFiles()
	{
		return ObjEnmPotosData.GetModelFiles();
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x0014255C File Offset: 0x0014075C
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmPotosData.GetEffectSize();
	}
}
