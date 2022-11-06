using System;
using UnityEngine;

// Token: 0x0200016A RID: 362
[AddComponentMenu("Scripts/Runners/Component/MotorConstant")]
public class MotorConstant : MonoBehaviour
{
	// Token: 0x06000A68 RID: 2664 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
	private void Start()
	{
		this.m_playerInfo = ObjUtil.GetPlayerInformation();
		this.m_animator = base.GetComponentInChildren<Animator>();
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0003E3C0 File Offset: 0x0003C5C0
	private void Update()
	{
		switch (this.m_state)
		{
		case MotorConstant.State.Wait:
			this.UpdateWait();
			break;
		case MotorConstant.State.Move:
			this.UpdateMove();
			break;
		}
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0003E408 File Offset: 0x0003C608
	private void OnDestroy()
	{
		this.SetMoveSE(false);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0003E414 File Offset: 0x0003C614
	public void SetParam(float speed, float dst, float start_dst, Vector3 agl, string se_category, string se_name)
	{
		this.m_moveSpeed = speed;
		this.m_moveDistance = dst;
		this.m_startMoveDistance = start_dst;
		this.m_angle = agl;
		this.m_move_SECatName = se_category;
		this.m_move_SEName = se_name;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0003E444 File Offset: 0x0003C644
	private void UpdateWait()
	{
		float playerDistance = this.GetPlayerDistance();
		if (!this.m_moveSpeed.Equals(0f) && this.m_moveDistance > 0f && playerDistance < this.m_startMoveDistance)
		{
			this.SetMoveAnimation(true);
			this.SetMoveSE(true);
			this.m_state = MotorConstant.State.Move;
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0003E4A0 File Offset: 0x0003C6A0
	private void UpdateMove()
	{
		float num = this.m_moveSpeed * Time.deltaTime;
		base.transform.position += this.m_angle * num;
		this.m_addDistance += Mathf.Abs(num);
		if (this.m_addDistance >= this.m_moveDistance)
		{
			this.SetMoveAnimation(false);
			this.SetMoveSE(false);
			this.m_state = MotorConstant.State.Idle;
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0003E518 File Offset: 0x0003C718
	private float GetPlayerDistance()
	{
		if (this.m_playerInfo)
		{
			Vector3 position = base.transform.position;
			return Mathf.Abs(Vector3.Distance(position, this.m_playerInfo.Position));
		}
		return 0f;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0003E560 File Offset: 0x0003C760
	private void SetMoveAnimation(bool flag)
	{
		if (this.m_animator)
		{
			this.m_animator.SetBool("move", flag);
		}
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0003E584 File Offset: 0x0003C784
	private void SetMoveSE(bool flag)
	{
		if (!this.m_move_SEName.Equals(string.Empty) && !this.m_move_SECatName.Equals(string.Empty))
		{
			if (flag)
			{
				if (this.m_move_SEID == 0U)
				{
					this.m_move_SEID = (uint)ObjUtil.LightPlaySE(this.m_move_SEName, this.m_move_SECatName);
				}
			}
			else if (this.m_move_SEID != 0U)
			{
				ObjUtil.StopSE((SoundManager.PlayId)this.m_move_SEID);
				this.m_move_SEID = 0U;
			}
		}
	}

	// Token: 0x04000827 RID: 2087
	private const string m_anim_name = "move";

	// Token: 0x04000828 RID: 2088
	private float m_moveSpeed;

	// Token: 0x04000829 RID: 2089
	private float m_moveDistance;

	// Token: 0x0400082A RID: 2090
	private float m_startMoveDistance;

	// Token: 0x0400082B RID: 2091
	private MotorConstant.State m_state;

	// Token: 0x0400082C RID: 2092
	private float m_addDistance;

	// Token: 0x0400082D RID: 2093
	private PlayerInformation m_playerInfo;

	// Token: 0x0400082E RID: 2094
	private Animator m_animator;

	// Token: 0x0400082F RID: 2095
	private Vector3 m_angle = Vector3.zero;

	// Token: 0x04000830 RID: 2096
	private string m_move_SEName = string.Empty;

	// Token: 0x04000831 RID: 2097
	private string m_move_SECatName = string.Empty;

	// Token: 0x04000832 RID: 2098
	private uint m_move_SEID;

	// Token: 0x0200016B RID: 363
	private enum State
	{
		// Token: 0x04000834 RID: 2100
		Wait,
		// Token: 0x04000835 RID: 2101
		Move,
		// Token: 0x04000836 RID: 2102
		Idle
	}
}
