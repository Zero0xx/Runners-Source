using System;
using UnityEngine;

// Token: 0x02000921 RID: 2337
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmDenden")]
public class ObjEnmDenden : ObjEnemyConstant
{
	// Token: 0x06003D66 RID: 15718 RVA: 0x0014183C File Offset: 0x0013FA3C
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D67 RID: 15719 RVA: 0x00141840 File Offset: 0x0013FA40
	protected override string[] GetModelFiles()
	{
		return ObjEnmDendenData.GetModelFiles();
	}

	// Token: 0x06003D68 RID: 15720 RVA: 0x00141848 File Offset: 0x0013FA48
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmDendenData.GetEffectSize();
	}
}
