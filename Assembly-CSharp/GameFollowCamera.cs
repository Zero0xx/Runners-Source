using System;
using Message;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class GameFollowCamera : CameraState
{
	// Token: 0x060008DF RID: 2271 RVA: 0x00031F2C File Offset: 0x0003012C
	public GameFollowCamera() : base(CameraType.DEFAULT)
	{
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00031F38 File Offset: 0x00030138
	public override void OnEnter(CameraManager manager)
	{
		base.SetCameraParameter(manager.GetParameter());
		this.m_defaultTargetOffset = manager.GetTargetOffset();
		this.m_downScrollLine = manager.EditParameter.m_downScrollLine;
		this.ResetParameters(manager);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00031F78 File Offset: 0x00030178
	public override void Update(CameraManager manager, float deltaTime)
	{
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		Vector3 vector = playerInformation.SideViewPathPos;
		Vector3 position = playerInformation.Position;
		float num = manager.m_startCameraPos.y + 1f;
		float y = vector.y;
		float y2 = position.y;
		vector = Vector3.Lerp(this.m_sideViewPos, vector, 6f * deltaTime);
		float num2 = y2 - y;
		float num3 = Vector3.Dot(playerInformation.VerticalVelocity, -playerInformation.GravityDir);
		float to = manager.EditParameter.m_downScrollLine;
		if (!playerInformation.IsOnGround() && num3 < -2f && num2 > num)
		{
			to = manager.EditParameter.m_downScrollLineOnDown;
		}
		this.m_downScrollLine = Mathf.Lerp(this.m_downScrollLine, to, 2f * deltaTime);
		float num4 = manager.EditParameter.m_upScrollLine + this.m_distPathToTarget;
		float num5 = this.m_downScrollLine + this.m_distPathToTarget;
		float upScrollLimit = manager.EditParameter.m_upScrollLimit;
		float downScrollLimit = manager.EditParameter.m_downScrollLimit;
		if (num2 > num4)
		{
			this.m_distPathToTarget += num2 - num4;
		}
		if (num2 < num5)
		{
			this.m_distPathToTarget += num2 - num5;
		}
		if (num2 < num && num2 > 0f)
		{
			this.m_distPathToTarget = Mathf.Lerp(this.m_distPathToTarget, manager.m_startCameraPos.y, 0.5f * deltaTime);
		}
		this.m_distPathToTarget = Mathf.Clamp(this.m_distPathToTarget, downScrollLimit, upScrollLimit);
		this.m_playerPosition.x = position.x;
		this.m_playerPosition.y = vector.y + this.m_distPathToTarget;
		this.m_playerPosition.z = 0f;
		this.m_param.m_target.x = this.m_playerPosition.x + this.m_defaultTargetOffset.x;
		this.m_param.m_target.y = this.m_playerPosition.y + this.m_defaultTargetOffset.y;
		this.m_param.m_target.z = 0f;
		this.m_param.m_position = this.m_param.m_target + manager.GetTargetToCamera();
		this.m_param.m_target.z = 0f;
		this.m_sideViewPos = vector;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000321E0 File Offset: 0x000303E0
	public override void OnDrawGizmos(CameraManager manager)
	{
		Gizmos.DrawRay(this.m_playerPosition, this.m_param.m_upDirection * 0.5f);
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(this.m_playerPosition, 0.5f);
		Vector3 center = new Vector3(this.m_playerPosition.x, this.m_sideViewPos.y + this.m_distPathToTarget, this.m_playerPosition.z);
		center.y = this.m_sideViewPos.y + this.m_distPathToTarget + manager.EditParameter.m_upScrollLine;
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(center, 0.5f);
		center.y = this.m_sideViewPos.y + this.m_distPathToTarget + this.m_downScrollLine;
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(center, 0.5f);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x000322C8 File Offset: 0x000304C8
	private void ResetParameters(CameraManager manager)
	{
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		this.m_playerPosition = playerInformation.Position;
		float y = playerInformation.Position.y;
		float y2 = playerInformation.SideViewPathPos.y;
		this.m_distPathToTarget = y - y2;
		this.m_sideViewPos = playerInformation.SideViewPathPos;
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0003231C File Offset: 0x0003051C
	public void OnMsgTutorialResetForRetry(CameraManager manager, MsgTutorialResetForRetry msg)
	{
		this.ResetParameters(manager);
	}

	// Token: 0x04000681 RID: 1665
	private const float ZOffset = 0f;

	// Token: 0x04000682 RID: 1666
	private const float PathPosSensitive = 6f;

	// Token: 0x04000683 RID: 1667
	private const float downScrollLineSensitive = 2f;

	// Token: 0x04000684 RID: 1668
	private const float pathDistanceSensitive = 0.5f;

	// Token: 0x04000685 RID: 1669
	private Vector3 m_defaultTargetOffset;

	// Token: 0x04000686 RID: 1670
	private Vector3 m_playerPosition;

	// Token: 0x04000687 RID: 1671
	private float m_distPathToTarget;

	// Token: 0x04000688 RID: 1672
	private Vector3 m_sideViewPos;

	// Token: 0x04000689 RID: 1673
	private float m_downScrollLine;
}
