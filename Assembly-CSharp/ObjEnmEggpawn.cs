using System;
using UnityEngine;

// Token: 0x02000924 RID: 2340
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmEggpawn")]
public class ObjEnmEggpawn : ObjEnemyConstant
{
	// Token: 0x06003D70 RID: 15728 RVA: 0x001418A0 File Offset: 0x0013FAA0
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D71 RID: 15729 RVA: 0x001418A4 File Offset: 0x0013FAA4
	protected override string[] GetModelFiles()
	{
		return ObjEnmEggpawnData.GetModelFiles();
	}

	// Token: 0x06003D72 RID: 15730 RVA: 0x001418AC File Offset: 0x0013FAAC
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmEggpawnData.GetEffectSize();
	}
}
