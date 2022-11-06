using System;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class ButtonEventTimer : MonoBehaviour
{
	// Token: 0x060020EE RID: 8430 RVA: 0x000C53AC File Offset: 0x000C35AC
	public void SetWaitTime(float waitTime)
	{
		this.m_waitButtonTime = waitTime;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000C53B8 File Offset: 0x000C35B8
	public void SetWaitTimeDefault()
	{
		this.SetWaitTime(0.4f);
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000C53C8 File Offset: 0x000C35C8
	public bool IsWaiting()
	{
		return this.m_waitButtonTime > 0f;
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000C53E0 File Offset: 0x000C35E0
	private void Start()
	{
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x000C53E4 File Offset: 0x000C35E4
	private void Update()
	{
		if (this.m_waitButtonTime > 0f)
		{
			this.m_waitButtonTime -= RealTime.deltaTime;
			if (this.m_waitButtonTime <= 0f)
			{
				this.m_waitButtonTime = 0f;
			}
		}
	}

	// Token: 0x04001D59 RID: 7513
	private float m_waitButtonTime;
}
