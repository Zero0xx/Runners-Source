using System;
using UnityEngine;

// Token: 0x02000927 RID: 2343
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmGanigani")]
public class ObjEnmGanigani : ObjEnemyConstant
{
	// Token: 0x06003D7A RID: 15738 RVA: 0x00141904 File Offset: 0x0013FB04
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.NORMAL;
	}

	// Token: 0x06003D7B RID: 15739 RVA: 0x00141908 File Offset: 0x0013FB08
	protected override string[] GetModelFiles()
	{
		return ObjEnmGaniganiData.GetModelFiles();
	}

	// Token: 0x06003D7C RID: 15740 RVA: 0x00141910 File Offset: 0x0013FB10
	protected override ObjEnemyUtil.EnemyEffectSize GetEffectSize()
	{
		return ObjEnmGaniganiData.GetEffectSize();
	}

	// Token: 0x06003D7D RID: 15741 RVA: 0x00141918 File Offset: 0x0013FB18
	protected override Vector3 GetConstantAngle()
	{
		return base.transform.right;
	}

	// Token: 0x06003D7E RID: 15742 RVA: 0x00141928 File Offset: 0x0013FB28
	protected override bool IsNormalMotion(float speed)
	{
		return speed >= 0f;
	}
}
