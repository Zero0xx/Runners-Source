using System;
using UnityEngine;

// Token: 0x02000942 RID: 2370
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmSpinaMove")]
public class ObjEnmSpinaMove : ObjEnemyConstant
{
	// Token: 0x06003DE8 RID: 15848 RVA: 0x00142640 File Offset: 0x00140840
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DE9 RID: 15849 RVA: 0x00142644 File Offset: 0x00140844
	protected override string[] GetModelFiles()
	{
		return ObjEnmSpinaData.GetModelFiles();
	}

	// Token: 0x06003DEA RID: 15850 RVA: 0x0014264C File Offset: 0x0014084C
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmSpinaData.GetEffectSize();
	}
}
