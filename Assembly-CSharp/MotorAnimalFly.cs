using System;
using UnityEngine;

// Token: 0x02000167 RID: 359
[AddComponentMenu("Scripts/Runners/Component/MotorAnimalFly")]
public class MotorAnimalFly : MonoBehaviour
{
	// Token: 0x06000A5F RID: 2655 RVA: 0x0003DF10 File Offset: 0x0003C110
	private void Update()
	{
		if (this.m_setup)
		{
			float deltaTime = Time.deltaTime;
			this.m_time += deltaTime;
			float num = Mathf.Sin(6.2831855f * this.m_time * this.m_moveSpeed);
			float d = this.m_moveDistance * num;
			float d2 = this.m_addSpeedX * this.m_time;
			base.transform.position = this.m_center_position + base.transform.up * d + this.m_angleX * d2;
			if (this.m_hitCheck)
			{
				Vector3 zero = Vector3.zero;
				if (ObjUtil.CheckGroundHit(this.m_center_position, base.transform.up, 1f, this.m_moveDistance + this.m_groundDistance, out zero))
				{
					this.m_center_position += Vector3.up * deltaTime * 2f;
				}
			}
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003E00C File Offset: 0x0003C20C
	public void SetupParam(float speed, float distance, float add_speed_x, Vector3 angle_x, float ground_distance, bool hitCheck)
	{
		this.m_moveSpeed = speed;
		this.m_moveDistance = distance;
		this.m_addSpeedX = add_speed_x;
		this.m_angleX = angle_x;
		float d = 0f;
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component != null)
		{
			d = component.radius;
		}
		this.m_center_position = base.transform.position + Vector3.up * d;
		this.m_groundDistance = ground_distance;
		this.m_hitCheck = hitCheck;
		this.m_setup = true;
		this.m_time = 0f;
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0003E09C File Offset: 0x0003C29C
	public void SetEnd()
	{
		this.m_setup = false;
	}

	// Token: 0x0400080C RID: 2060
	private const float UP_SPEED = 2f;

	// Token: 0x0400080D RID: 2061
	private float m_moveSpeed;

	// Token: 0x0400080E RID: 2062
	private float m_moveDistance;

	// Token: 0x0400080F RID: 2063
	private float m_groundDistance;

	// Token: 0x04000810 RID: 2064
	private float m_addSpeedX;

	// Token: 0x04000811 RID: 2065
	private Vector3 m_angleX = Vector3.zero;

	// Token: 0x04000812 RID: 2066
	private Vector3 m_center_position = Vector3.zero;

	// Token: 0x04000813 RID: 2067
	private float m_time;

	// Token: 0x04000814 RID: 2068
	private bool m_hitCheck;

	// Token: 0x04000815 RID: 2069
	private bool m_setup;
}
