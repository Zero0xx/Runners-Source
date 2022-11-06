using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class MotorAnimalJump : MonoBehaviour
{
	// Token: 0x06000A63 RID: 2659 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
	public void Setup(ref MotorAnimalJump.JumpParam param)
	{
		this.m_param = param;
		this.m_add_x = param.m_add_x;
		this.m_add_y = this.m_param.m_bound_add_y;
		this.m_jump = true;
		this.m_time = 0f;
		this.m_setup = true;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0003E108 File Offset: 0x0003C308
	public void SetEnd()
	{
		this.m_setup = false;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0003E114 File Offset: 0x0003C314
	private void Update()
	{
		if (this.m_setup)
		{
			this.UpdateThrow(Time.deltaTime, this.m_param.m_obj);
		}
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0003E138 File Offset: 0x0003C338
	private void UpdateThrow(float delta, GameObject obj)
	{
		if (obj)
		{
			float num = delta * this.m_param.m_speed;
			float d;
			float d2;
			if (this.m_jump)
			{
				this.m_time += num;
				d = num * this.m_add_x;
				float num2 = this.m_add_y - this.m_time * -this.m_param.m_gravity * 0.15f;
				if (num2 < 0f)
				{
					num2 = 0f;
					this.m_time = 0f;
					this.m_jump = false;
				}
				d2 = delta * num2 * 3f;
			}
			else
			{
				this.m_time += num;
				float num3 = this.m_add_x - this.m_time * 0.1f;
				if (num3 < 0f)
				{
					num3 = 0f;
				}
				d = delta * num3 * 3f;
				d2 = this.m_time * this.m_param.m_gravity * delta;
			}
			Vector3 vector = obj.transform.position + this.m_param.m_up * d2 + this.m_param.m_forward * d;
			if (this.m_param.m_bound)
			{
				Vector3 zero = Vector3.zero;
				if (ObjUtil.CheckGroundHit(vector, this.m_param.m_up, 1f, 1f, out zero))
				{
					vector.y = zero.y;
					this.m_add_y = Mathf.Max(this.m_add_y - this.m_add_y * this.m_param.m_bound_down_y, 0f);
					this.m_add_x = Mathf.Max(this.m_add_x - this.m_add_x * this.m_param.m_bound_down_x, 0f);
					this.m_jump = true;
					this.m_time = 0f;
				}
				else
				{
					this.m_add_x = Mathf.Max(this.m_add_x - delta * this.m_add_x * 0.01f, 0f);
					this.m_add_y = Mathf.Max(this.m_add_y - delta * this.m_add_y * 0.01f, 0f);
				}
			}
			obj.transform.position = vector;
		}
	}

	// Token: 0x04000816 RID: 2070
	private const float HitLength = 1f;

	// Token: 0x04000817 RID: 2071
	private float m_time;

	// Token: 0x04000818 RID: 2072
	private bool m_jump = true;

	// Token: 0x04000819 RID: 2073
	private float m_add_x;

	// Token: 0x0400081A RID: 2074
	private float m_add_y;

	// Token: 0x0400081B RID: 2075
	private bool m_setup;

	// Token: 0x0400081C RID: 2076
	private MotorAnimalJump.JumpParam m_param;

	// Token: 0x02000169 RID: 361
	public struct JumpParam
	{
		// Token: 0x0400081D RID: 2077
		public GameObject m_obj;

		// Token: 0x0400081E RID: 2078
		public float m_speed;

		// Token: 0x0400081F RID: 2079
		public float m_gravity;

		// Token: 0x04000820 RID: 2080
		public float m_add_x;

		// Token: 0x04000821 RID: 2081
		public Vector3 m_up;

		// Token: 0x04000822 RID: 2082
		public Vector3 m_forward;

		// Token: 0x04000823 RID: 2083
		public bool m_bound;

		// Token: 0x04000824 RID: 2084
		public float m_bound_add_y;

		// Token: 0x04000825 RID: 2085
		public float m_bound_down_x;

		// Token: 0x04000826 RID: 2086
		public float m_bound_down_y;
	}
}
