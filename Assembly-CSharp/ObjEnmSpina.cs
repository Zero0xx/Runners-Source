using System;
using UnityEngine;

// Token: 0x0200093F RID: 2367
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmSpina")]
public class ObjEnmSpina : ObjEnemySwing
{
	// Token: 0x06003DDE RID: 15838 RVA: 0x001425DC File Offset: 0x001407DC
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DDF RID: 15839 RVA: 0x001425E0 File Offset: 0x001407E0
	protected override string[] GetModelFiles()
	{
		return ObjEnmSpinaData.GetModelFiles();
	}

	// Token: 0x06003DE0 RID: 15840 RVA: 0x001425E8 File Offset: 0x001407E8
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmSpinaData.GetEffectSize();
	}
}
