using System;

// Token: 0x02000934 RID: 2356
public class ObjEnemySwing : ObjEnemyBase
{
	// Token: 0x06003DC0 RID: 15808 RVA: 0x0014236C File Offset: 0x0014056C
	protected override void OnSpawned()
	{
		base.OnSpawned();
	}

	// Token: 0x06003DC1 RID: 15809 RVA: 0x00142374 File Offset: 0x00140574
	public void SetObjEnemySwingParameter(ObjEnemySwingParameter param)
	{
		if (param != null)
		{
			base.SetupEnemy((uint)param.tblID, 0f);
			MotorSwing component = base.GetComponent<MotorSwing>();
			if (component)
			{
				component.SetParam(param.moveSpeed, param.moveDistanceX, param.moveDistanceY, base.transform.forward);
			}
		}
	}
}
