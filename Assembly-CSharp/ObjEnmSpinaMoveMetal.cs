using System;
using UnityEngine;

// Token: 0x02000943 RID: 2371
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmSpinaMoveMetal")]
public class ObjEnmSpinaMoveMetal : ObjEnmSpinaMove
{
	// Token: 0x06003DEC RID: 15852 RVA: 0x0014265C File Offset: 0x0014085C
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
