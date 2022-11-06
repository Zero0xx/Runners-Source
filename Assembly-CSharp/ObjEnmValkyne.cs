using System;
using UnityEngine;

// Token: 0x02000944 RID: 2372
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmValkyne")]
public class ObjEnmValkyne : ObjEnemyConstant
{
	// Token: 0x06003DEE RID: 15854 RVA: 0x00142668 File Offset: 0x00140868
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003DEF RID: 15855 RVA: 0x0014266C File Offset: 0x0014086C
	protected override string[] GetModelFiles()
	{
		return ObjEnmValkyneData.GetModelFiles();
	}

	// Token: 0x06003DF0 RID: 15856 RVA: 0x00142674 File Offset: 0x00140874
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmValkyneData.GetEffectSize();
	}

	// Token: 0x06003DF1 RID: 15857 RVA: 0x0014267C File Offset: 0x0014087C
	protected override void OnSpawned()
	{
		base.OnSpawned();
	}
}
