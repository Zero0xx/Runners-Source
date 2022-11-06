using System;
using UnityEngine;

// Token: 0x0200092C RID: 2348
[AddComponentMenu("Scripts/Runners/Object/Enemy/ObjEnmMotoraMetal")]
public class ObjEnmMotoraMetal : ObjEnmMotora
{
	// Token: 0x06003D8E RID: 15758 RVA: 0x001419E0 File Offset: 0x0013FBE0
	protected override ObjEnemyUtil.EnemyType GetOriginalType()
	{
		return ObjEnemyUtil.EnemyType.METAL;
	}
}
