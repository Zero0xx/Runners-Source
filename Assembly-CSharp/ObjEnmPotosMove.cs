using System;
using UnityEngine;

// Token: 0x0200093D RID: 2365
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmPotosMove")]
public class ObjEnmPotosMove : ObjEnemyConstant
{
	// Token: 0x06003DD8 RID: 15832 RVA: 0x001425B4 File Offset: 0x001407B4
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x001425B8 File Offset: 0x001407B8
	protected override string[] GetModelFiles()
	{
		return ObjEnmPotosData.GetModelFiles();
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x001425C0 File Offset: 0x001407C0
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmPotosData.GetEffectSize();
	}
}
