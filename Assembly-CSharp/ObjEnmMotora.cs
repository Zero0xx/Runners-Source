using System;
using UnityEngine;

// Token: 0x0200092A RID: 2346
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmMotora")]
public class ObjEnmMotora : ObjEnemyConstant
{
	// Token: 0x06003D86 RID: 15750 RVA: 0x00141988 File Offset: 0x0013FB88
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x0014198C File Offset: 0x0013FB8C
	protected override string[] GetModelFiles()
	{
		return ObjEnmMotoraData.GetModelFiles();
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x00141994 File Offset: 0x0013FB94
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmMotoraData.GetEffectSize();
	}
}
