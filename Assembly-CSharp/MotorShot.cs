using System;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class MotorShot : MonoBehaviour
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x0003E610 File Offset: 0x0003C810
	public void Setup(MotorShot.ShotParam param)
	{
		this.m_param = param;
		this.m_time = 0f;
		this.m_rot_speed = param.m_rot_speed;
		this.m_add_x = param.m_after_add_x;
		this.m_add_y = 0f;
		this.m_shot_speed = param.m_shot_speed;
		this.m_jump = false;
		this.m_bound = false;
		if (this.m_param.m_shot_time > 0f)
		{
			this.m_state = MotorShot.State.Shot;
		}
		else
		{
			this.m_state = MotorShot.State.After;
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0003E694 File Offset: 0x0003C894
	public void SetEnd()
	{
		this.m_param = null;
		this.m_state = MotorShot.State.Idle;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003E6A4 File Offset: 0x0003C8A4
	private void Update()
	{
		if (this.m_param != null)
		{
			float deltaTime = Time.deltaTime;
			MotorShot.State state = this.m_state;
			if (state != MotorShot.State.Shot)
			{
				if (state == MotorShot.State.After)
				{
					this.UpdateAfter(deltaTime, this.m_param.m_obj);
					this.UpdateRot(deltaTime, this.m_param.m_obj);
				}
			}
			else
			{
				this.UpdateShot(deltaTime, this.m_param.m_obj);
				this.UpdateRot(deltaTime, this.m_param.m_obj);
			}
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0003E730 File Offset: 0x0003C930
	private void UpdateShot(float delta, GameObject obj)
	{
		if (obj)
		{
			this.m_time += delta;
			if (this.m_time > this.m_param.m_shot_time)
			{
				this.m_time = 0f;
				this.m_jump = false;
				this.m_bound = false;
				this.m_state = MotorShot.State.After;
			}
			else
			{
				Vector3 a = this.m_param.m_shot_rotation * Vector3.up * this.m_shot_speed;
				Vector3 b = a * delta;
				Vector3 vector = obj.transform.position + b;
				float num = this.m_shot_speed * delta * this.m_param.m_shot_downspeed;
				this.m_shot_speed -= num;
				if (num < 0f)
				{
					this.m_shot_speed = 0f;
					this.m_param.m_shot_time = 0f;
				}
				if (this.m_param.m_bound)
				{
					vector = this.SetBound(delta, vector);
				}
				obj.transform.position = vector;
			}
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0003E83C File Offset: 0x0003CA3C
	private void UpdateAfter(float delta, GameObject obj)
	{
		if (obj)
		{
			float num = delta * this.m_param.m_after_speed;
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
			Vector3 vector = obj.transform.position + this.m_param.m_after_up * d2 + this.m_param.m_after_forward * d;
			if (this.m_param.m_bound)
			{
				vector = this.SetBound(delta, vector);
			}
			obj.transform.position = vector;
		}
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0003E99C File Offset: 0x0003CB9C
	private Vector3 SetBound(float delta, Vector3 pos)
	{
		Vector3 result = pos;
		if (result.y < this.m_param.m_bound_pos_y)
		{
			result.y = this.m_param.m_bound_pos_y;
			if (this.m_param.m_bound_add_y > 0f)
			{
				if (!this.m_bound)
				{
					this.m_add_y = this.m_param.m_bound_add_y;
				}
				else
				{
					this.m_add_y = Mathf.Max(this.m_add_y - this.m_add_y * this.m_param.m_bound_down_y, 0f);
				}
				this.m_add_x = Mathf.Max(this.m_add_x - this.m_add_x * this.m_param.m_bound_down_x, 0f);
				this.m_time = 0f;
				this.m_bound = true;
				this.m_jump = true;
				if (this.m_state == MotorShot.State.Shot)
				{
					this.m_state = MotorShot.State.After;
				}
			}
			else if (this.m_state == MotorShot.State.Shot)
			{
				this.m_state = MotorShot.State.Idle;
			}
		}
		else if (this.m_bound)
		{
			this.m_add_x = Mathf.Max(this.m_add_x - delta * this.m_add_x * 0.01f, 0f);
			this.m_add_y = Mathf.Max(this.m_add_y - delta * this.m_add_y * 0.01f, 0f);
		}
		return result;
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0003EAFC File Offset: 0x0003CCFC
	private void UpdateRot(float delta, GameObject obj)
	{
		if (obj)
		{
			float d = 60f * delta * this.m_rot_speed;
			obj.transform.rotation = Quaternion.Euler(d * this.m_param.m_rot_angle) * obj.transform.rotation;
			this.m_rot_speed = Mathf.Max(this.m_rot_speed - delta * this.m_param.m_rot_downspeed, 0f);
		}
	}

	// Token: 0x04000837 RID: 2103
	private float m_time;

	// Token: 0x04000838 RID: 2104
	private float m_rot_speed;

	// Token: 0x04000839 RID: 2105
	private float m_add_x;

	// Token: 0x0400083A RID: 2106
	private float m_add_y;

	// Token: 0x0400083B RID: 2107
	private float m_shot_speed;

	// Token: 0x0400083C RID: 2108
	private bool m_jump;

	// Token: 0x0400083D RID: 2109
	private bool m_bound;

	// Token: 0x0400083E RID: 2110
	private MotorShot.State m_state;

	// Token: 0x0400083F RID: 2111
	private MotorShot.ShotParam m_param;

	// Token: 0x0200016D RID: 365
	private enum State
	{
		// Token: 0x04000841 RID: 2113
		Idle,
		// Token: 0x04000842 RID: 2114
		Shot,
		// Token: 0x04000843 RID: 2115
		After
	}

	// Token: 0x0200016E RID: 366
	public class ShotParam
	{
		// Token: 0x04000844 RID: 2116
		public GameObject m_obj;

		// Token: 0x04000845 RID: 2117
		public float m_gravity;

		// Token: 0x04000846 RID: 2118
		public float m_rot_speed;

		// Token: 0x04000847 RID: 2119
		public float m_rot_downspeed;

		// Token: 0x04000848 RID: 2120
		public Vector3 m_rot_angle = Vector3.zero;

		// Token: 0x04000849 RID: 2121
		public Quaternion m_shot_rotation = Quaternion.identity;

		// Token: 0x0400084A RID: 2122
		public float m_shot_time;

		// Token: 0x0400084B RID: 2123
		public float m_shot_speed;

		// Token: 0x0400084C RID: 2124
		public float m_shot_downspeed;

		// Token: 0x0400084D RID: 2125
		public bool m_bound;

		// Token: 0x0400084E RID: 2126
		public float m_bound_pos_y;

		// Token: 0x0400084F RID: 2127
		public float m_bound_add_y;

		// Token: 0x04000850 RID: 2128
		public float m_bound_down_x;

		// Token: 0x04000851 RID: 2129
		public float m_bound_down_y;

		// Token: 0x04000852 RID: 2130
		public float m_after_speed;

		// Token: 0x04000853 RID: 2131
		public float m_after_add_x;

		// Token: 0x04000854 RID: 2132
		public Vector3 m_after_up = Vector3.zero;

		// Token: 0x04000855 RID: 2133
		public Vector3 m_after_forward = Vector3.zero;
	}
}
