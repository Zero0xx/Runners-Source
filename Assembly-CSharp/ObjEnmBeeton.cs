using System;
using UnityEngine;

// Token: 0x0200091C RID: 2332
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmBeeton")]
public class ObjEnmBeeton : ObjEnemySwing
{
	// Token: 0x06003D56 RID: 15702 RVA: 0x001417B0 File Offset: 0x0013F9B0
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D57 RID: 15703 RVA: 0x001417B4 File Offset: 0x0013F9B4
	protected override string[] GetModelFiles()
	{
		return ObjEnmBeetonData.GetModelFiles();
	}

	// Token: 0x06003D58 RID: 15704 RVA: 0x001417BC File Offset: 0x0013F9BC
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmBeetonData.GetEffectSize();
	}
}
