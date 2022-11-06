using System;

// Token: 0x02000838 RID: 2104
public class ObjAnimalJump : ObjAnimalBase
{
	// Token: 0x06003902 RID: 14594 RVA: 0x0012E18C File Offset: 0x0012C38C
	protected override float GetCheckGroundHitLength()
	{
		return 1f;
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x0012E194 File Offset: 0x0012C394
	protected override void StartNextComponent()
	{
		MotorAnimalJump component = base.GetComponent<MotorAnimalJump>();
		if (component)
		{
			component.enabled = true;
			MotorAnimalJump.JumpParam jumpParam = default(MotorAnimalJump.JumpParam);
			jumpParam.m_obj = base.gameObject;
			jumpParam.m_speed = 6f;
			jumpParam.m_gravity = -6.1f;
			jumpParam.m_add_x = 1f + base.GetMoveSpeed();
			jumpParam.m_up = base.transform.up;
			jumpParam.m_forward = base.transform.right;
			jumpParam.m_bound = true;
			jumpParam.m_bound_add_y = 3f;
			jumpParam.m_bound_down_x = 0.2f;
			jumpParam.m_bound_down_y = 0f;
			component.Setup(ref jumpParam);
		}
	}

	// Token: 0x06003904 RID: 14596 RVA: 0x0012E254 File Offset: 0x0012C454
	protected override void EndNextComponent()
	{
		MotorAnimalJump component = base.GetComponent<MotorAnimalJump>();
		if (component)
		{
			component.enabled = false;
			component.SetEnd();
		}
	}

	// Token: 0x04002FC2 RID: 12226
	private const float JUMP_SPEED = 6f;

	// Token: 0x04002FC3 RID: 12227
	private const float JUMP_GRAVITY = -6.1f;

	// Token: 0x04002FC4 RID: 12228
	private const float JUMP_ADD_X = 1f;

	// Token: 0x04002FC5 RID: 12229
	private const float BOUND_ADD_X = 3f;

	// Token: 0x04002FC6 RID: 12230
	private const float BOUND_DOWN_X = 0.2f;

	// Token: 0x04002FC7 RID: 12231
	private const float BOUND_DOWN_Y = 0f;

	// Token: 0x04002FC8 RID: 12232
	private const float HIT_CHECK_DISTANCE = 1f;
}
