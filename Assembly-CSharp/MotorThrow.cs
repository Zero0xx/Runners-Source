using System;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class MotorThrow : MonoBehaviour
{
	// Token: 0x06000A7F RID: 2687 RVA: 0x0003ECB0 File Offset: 0x0003CEB0
	public void Setup(MotorThrow.ThrowParam param)
	{
		this.m_param = param;
		this.m_rot_speed = param.m_rot_speed;
		this.m_add_x = param.m_add_x;
		this.m_add_y = param.m_add_y;
		this.m_time = 0f;
		this.m_bound = false;
		this.m_jump = true;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0003ED04 File Offset: 0x0003CF04
	public void SetEnd()
	{
		this.m_param = null;
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0003ED10 File Offset: 0x0003CF10
	private void Update()
	{
		if (this.m_param != null)
		{
			this.UpdateThrow(Time.deltaTime, this.m_param.m_obj);
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0003ED34 File Offset: 0x0003CF34
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
			Vector3 position = obj.transform.position + this.m_param.m_up * d2 + this.m_param.m_forward * d;
			if (this.m_param.m_bound)
			{
				if (position.y < this.m_param.m_bound_pos_y)
				{
					position.y = this.m_param.m_bound_pos_y;
					if (!this.m_bound)
					{
						this.m_add_y = this.m_param.m_bound_add_y;
					}
					else
					{
						this.m_add_y = Mathf.Max(this.m_add_y - this.m_add_y * this.m_param.m_bound_down_y, 0f);
					}
					this.m_add_x = Mathf.Max(this.m_add_x - this.m_add_x * this.m_param.m_bound_down_x, 0f);
					this.m_bound = true;
					this.m_jump = true;
					this.m_time = 0f;
				}
				else if (this.m_bound)
				{
					this.m_add_x = Mathf.Max(this.m_add_x - delta * this.m_add_x * 0.01f, 0f);
					this.m_add_y = Mathf.Max(this.m_add_y - delta * this.m_add_y * 0.01f, 0f);
				}
			}
			obj.transform.position = position;
			if (this.m_rot_speed > 0f)
			{
				float d3 = 60f * delta * this.m_rot_speed;
				obj.transform.rotation = Quaternion.Euler(d3 * this.m_param.m_rot_angle) * obj.transform.rotation;
				this.m_rot_speed = Mathf.Max(this.m_rot_speed - delta * this.m_param.m_rot_downspeed, 0f);
			}
		}
	}

	// Token: 0x0400085C RID: 2140
	private float m_time;

	// Token: 0x0400085D RID: 2141
	private float m_rot_speed;

	// Token: 0x0400085E RID: 2142
	private bool m_jump = true;

	// Token: 0x0400085F RID: 2143
	private float m_add_x;

	// Token: 0x04000860 RID: 2144
	private float m_add_y;

	// Token: 0x04000861 RID: 2145
	private bool m_bound;

	// Token: 0x04000862 RID: 2146
	private MotorThrow.ThrowParam m_param;

	// Token: 0x02000171 RID: 369
	public class ThrowParam
	{
		// Token: 0x04000863 RID: 2147
		public GameObject m_obj;

		// Token: 0x04000864 RID: 2148
		public float m_speed;

		// Token: 0x04000865 RID: 2149
		public float m_gravity;

		// Token: 0x04000866 RID: 2150
		public float m_add_x;

		// Token: 0x04000867 RID: 2151
		public float m_add_y;

		// Token: 0x04000868 RID: 2152
		public float m_rot_speed;

		// Token: 0x04000869 RID: 2153
		public float m_rot_downspeed;

		// Token: 0x0400086A RID: 2154
		public Vector3 m_up = Vector3.zero;

		// Token: 0x0400086B RID: 2155
		public Vector3 m_forward = Vector3.zero;

		// Token: 0x0400086C RID: 2156
		public Vector3 m_rot_angle = Vector3.zero;

		// Token: 0x0400086D RID: 2157
		public bool m_bound;

		// Token: 0x0400086E RID: 2158
		public float m_bound_pos_y;

		// Token: 0x0400086F RID: 2159
		public float m_bound_add_y;

		// Token: 0x04000870 RID: 2160
		public float m_bound_down_x;

		// Token: 0x04000871 RID: 2161
		public float m_bound_down_y;
	}
}
