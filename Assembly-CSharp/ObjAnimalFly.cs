using System;

// Token: 0x02000837 RID: 2103
public class ObjAnimalFly : ObjAnimalBase
{
	// Token: 0x060038FE RID: 14590 RVA: 0x0012E0FC File Offset: 0x0012C2FC
	protected override float GetCheckGroundHitLength()
	{
		return 2f;
	}

	// Token: 0x060038FF RID: 14591 RVA: 0x0012E104 File Offset: 0x0012C304
	protected override void StartNextComponent()
	{
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.enabled = true;
			component.SetupParam(0.5f, 1f, 1f + base.GetMoveSpeed(), base.transform.right, 3f, true);
		}
	}

	// Token: 0x06003900 RID: 14592 RVA: 0x0012E158 File Offset: 0x0012C358
	protected override void EndNextComponent()
	{
		MotorAnimalFly component = base.GetComponent<MotorAnimalFly>();
		if (component)
		{
			component.enabled = false;
			component.SetEnd();
		}
	}

	// Token: 0x04002FBD RID: 12221
	private const float FLY_SPEED = 0.5f;

	// Token: 0x04002FBE RID: 12222
	private const float FLY_DISTANCE = 1f;

	// Token: 0x04002FBF RID: 12223
	private const float FLY_ADD_X = 1f;

	// Token: 0x04002FC0 RID: 12224
	private const float GROUND_DISTANCE = 3f;

	// Token: 0x04002FC1 RID: 12225
	private const float HIT_CHECK_DISTANCE = 2f;
}
