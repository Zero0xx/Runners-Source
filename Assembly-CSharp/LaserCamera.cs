using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class LaserCamera : CameraState
{
	// Token: 0x060008F1 RID: 2289 RVA: 0x00032770 File Offset: 0x00030970
	public LaserCamera() : base(CameraType.LASER)
	{
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0003277C File Offset: 0x0003097C
	public override void OnEnter(CameraManager manager)
	{
		base.SetCameraParameter(manager.GetParameter());
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		this.m_playerPosition = playerInformation.Position;
		this.m_mode = LaserCamera.Mode.Stay;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x000327B0 File Offset: 0x000309B0
	public override void Update(CameraManager manager, float deltaTime)
	{
		Camera component = manager.GetComponent<Camera>();
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		Vector3 playerPosition = this.m_playerPosition;
		Vector3 position = playerInformation.Position;
		this.m_playerPosition = position;
		Vector3 position2 = component.WorldToViewportPoint(position);
		CameraManager.LaserEditParameter laserParameter = manager.LaserParameter;
		float upScrollViewPort = laserParameter.m_upScrollViewPort;
		float downScrollViewPort = laserParameter.m_downScrollViewPort;
		if (position2.y > upScrollViewPort)
		{
			position2.y = upScrollViewPort;
		}
		if (position2.y < downScrollViewPort)
		{
			position2.y = downScrollViewPort;
		}
		switch (this.m_mode)
		{
		case LaserCamera.Mode.Stay:
			if (position2.x > laserParameter.m_rightScrollViewPort)
			{
				this.m_mode = LaserCamera.Mode.MoveFast;
			}
			return;
		case LaserCamera.Mode.MoveFast:
		{
			float num = (position2.x - laserParameter.m_leftScrollViewPort) / laserParameter.m_fastScrollTime;
			position2.x -= num * deltaTime;
			if (position2.x < laserParameter.m_leftScrollViewPort)
			{
				position2.x = laserParameter.m_leftScrollViewPort;
				this.m_mode = LaserCamera.Mode.MoveConst;
			}
			break;
		}
		case LaserCamera.Mode.MoveSlow:
		{
			float fastScrollTime = laserParameter.m_fastScrollTime;
			position2.x += fastScrollTime * deltaTime;
			if (position2.x > laserParameter.m_rightScrollViewPort)
			{
				this.m_mode = LaserCamera.Mode.MoveFast;
			}
			break;
		}
		case LaserCamera.Mode.MoveConst:
			position2.x = laserParameter.m_leftScrollViewPort;
			break;
		}
		Vector3 b = component.ViewportToWorldPoint(position2);
		Vector3 b2 = (this.m_mode != LaserCamera.Mode.MoveConst) ? (this.m_playerPosition - b) : Vector3.zero;
		Vector3 target = this.m_param.m_target + (this.m_playerPosition - playerPosition) + b2;
		this.m_param.m_target = target;
		this.m_param.m_position = this.m_param.m_target + manager.GetTargetToCamera();
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00032998 File Offset: 0x00030B98
	public override void OnDrawGizmos(CameraManager manager)
	{
		Gizmos.DrawRay(this.m_playerPosition, this.m_param.m_upDirection * 0.5f);
	}

	// Token: 0x04000699 RID: 1689
	private LaserCamera.Mode m_mode;

	// Token: 0x0400069A RID: 1690
	private Vector3 m_playerPosition;

	// Token: 0x02000128 RID: 296
	private enum Mode
	{
		// Token: 0x0400069C RID: 1692
		Stay,
		// Token: 0x0400069D RID: 1693
		MoveFast,
		// Token: 0x0400069E RID: 1694
		MoveSlow,
		// Token: 0x0400069F RID: 1695
		MoveConst
	}
}
