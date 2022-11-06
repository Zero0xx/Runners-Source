using System;
using Message;
using UnityEngine;

// Token: 0x02000166 RID: 358
[AddComponentMenu("Scripts/Runners/Component/MagnetControl")]
public class MagnetControl : MonoBehaviour
{
	// Token: 0x06000A58 RID: 2648 RVA: 0x0003DD24 File Offset: 0x0003BF24
	private void Start()
	{
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0003DD34 File Offset: 0x0003BF34
	private void OnEnable()
	{
		base.enabled = true;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0003DD40 File Offset: 0x0003BF40
	private void OnDisable()
	{
		this.Reset();
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0003DD48 File Offset: 0x0003BF48
	private void Update()
	{
		if (!this.m_active)
		{
			return;
		}
		if (this.m_object)
		{
			float num = this.m_speed;
			if (this.m_waitTime > 0f)
			{
				this.m_waitTime -= Time.deltaTime;
				num = this.m_speed * 0.1f;
			}
			this.m_time += Time.deltaTime;
			float num2 = 0.1f - this.m_time * num;
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			Vector3 zero = Vector3.zero;
			Vector3 target = (!(this.m_target != null)) ? this.m_playerInfo.Position : this.m_target.transform.position;
			this.m_object.transform.position = Vector3.SmoothDamp(this.m_object.transform.position, target, ref zero, num2);
		}
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0003DE3C File Offset: 0x0003C03C
	public void OnUseMagnet(MsgUseMagnet msg)
	{
		if (msg.m_obj)
		{
			this.m_object = msg.m_obj;
			this.m_target = msg.m_target;
			this.m_waitTime = msg.m_time;
			this.m_active = true;
			float num = ObjUtil.GetPlayerAddSpeed();
			if (num < 0f)
			{
				num = 0f;
			}
			this.m_speed = 0.2f + 0.02f * num;
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0003DEB0 File Offset: 0x0003C0B0
	public void Reset()
	{
		this.m_speed = 0f;
		this.m_object = null;
		this.m_target = null;
		this.m_waitTime = 0f;
		this.m_time = 0f;
		this.m_active = false;
		base.enabled = true;
	}

	// Token: 0x04000804 RID: 2052
	private const float MAGNET_SPEED = 0.2f;

	// Token: 0x04000805 RID: 2053
	private PlayerInformation m_playerInfo;

	// Token: 0x04000806 RID: 2054
	private GameObject m_object;

	// Token: 0x04000807 RID: 2055
	private GameObject m_target;

	// Token: 0x04000808 RID: 2056
	private float m_time;

	// Token: 0x04000809 RID: 2057
	private float m_waitTime;

	// Token: 0x0400080A RID: 2058
	private float m_speed;

	// Token: 0x0400080B RID: 2059
	private bool m_active;
}
