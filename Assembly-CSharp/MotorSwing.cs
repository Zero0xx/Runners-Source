using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
[AddComponentMenu("Scripts/Runners/Component/MotorSwing")]
public class MotorSwing : MonoBehaviour
{
	// Token: 0x06000A7B RID: 2683 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
	private void Start()
	{
		this.m_center_position = base.transform.position;
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003EBEC File Offset: 0x0003CDEC
	private void Update()
	{
		if (this.m_moveSpeed > 0f)
		{
			this.m_time += Time.deltaTime;
			float num = Mathf.Sin(6.2831855f * this.m_time * this.m_moveSpeed);
			float d = this.m_moveDistanceX * num;
			float d2 = this.m_moveDistanceY * num;
			Vector3 b = base.transform.up * d2 + this.m_angle_x * d;
			base.transform.position = this.m_center_position + b;
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0003EC80 File Offset: 0x0003CE80
	public void SetParam(float speed, float x, float y, Vector3 agl)
	{
		this.m_moveSpeed = speed;
		this.m_moveDistanceX = x;
		this.m_moveDistanceY = y;
		this.m_angle_x = agl;
	}

	// Token: 0x04000856 RID: 2134
	private float m_moveSpeed;

	// Token: 0x04000857 RID: 2135
	private float m_moveDistanceX;

	// Token: 0x04000858 RID: 2136
	private float m_moveDistanceY;

	// Token: 0x04000859 RID: 2137
	private Vector3 m_center_position = Vector3.zero;

	// Token: 0x0400085A RID: 2138
	private Vector3 m_angle_x = Vector3.zero;

	// Token: 0x0400085B RID: 2139
	private float m_time;
}
