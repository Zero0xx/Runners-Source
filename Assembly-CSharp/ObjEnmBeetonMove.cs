using System;
using UnityEngine;

// Token: 0x0200091F RID: 2335
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmBeetonMove")]
public class ObjEnmBeetonMove : ObjEnemyConstant
{
	// Token: 0x06003D60 RID: 15712 RVA: 0x00141814 File Offset: 0x0013FA14
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D61 RID: 15713 RVA: 0x00141818 File Offset: 0x0013FA18
	protected override string[] GetModelFiles()
	{
		return ObjEnmBeetonData.GetModelFiles();
	}

	// Token: 0x06003D62 RID: 15714 RVA: 0x00141820 File Offset: 0x0013FA20
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmBeetonData.GetEffectSize();
	}
}
