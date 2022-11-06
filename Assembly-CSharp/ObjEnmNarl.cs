using System;
using UnityEngine;

// Token: 0x0200092D RID: 2349
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmNarl")]
public class ObjEnmNarl : ObjEnemyConstant
{
	// Token: 0x06003D90 RID: 15760 RVA: 0x001419EC File Offset: 0x0013FBEC
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D91 RID: 15761 RVA: 0x001419F0 File Offset: 0x0013FBF0
	protected override string[] GetModelFiles()
	{
		return ObjEnmNarlData.GetModelFiles();
	}

	// Token: 0x06003D92 RID: 15762 RVA: 0x001419F8 File Offset: 0x0013FBF8
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmNarlData.GetEffectSize();
	}
}
