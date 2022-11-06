using System;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class GimmickCameraBase : CameraState
{
	// Token: 0x060008E5 RID: 2277 RVA: 0x00032328 File Offset: 0x00030528
	public GimmickCameraBase(CameraType type) : base(type)
	{
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0003234C File Offset: 0x0003054C
	public override void OnEnter(CameraManager manager)
	{
		base.SetCameraParameter(manager.GetParameter());
		this.m_gimmickCameraParam = this.GetGimmickCameraParameter(manager);
		this.m_mode = GimmickCameraBase.Mode.Wait;
		this.m_time = 0f;
		this.m_speed = Vector3.zero;
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		this.m_playerPosition = playerInformation.Position;
		this.m_camera = manager.GetComponent<Camera>();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x000323B0 File Offset: 0x000305B0
	public override void Update(CameraManager manager, float deltaTime)
	{
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		Vector3 playerPosition = this.m_playerPosition;
		Vector3 position = playerInformation.Position;
		this.m_playerPosition = position;
		Vector3 target = this.m_param.m_target + (this.m_playerPosition - playerPosition);
		if (this.m_camera)
		{
			Vector3 viewPort = this.m_camera.WorldToViewportPoint(position);
			this.UpdateGimmickCamera(manager, deltaTime, viewPort, ref target);
			this.m_param.m_target = target;
			this.m_param.m_position = this.m_param.m_target + manager.GetTargetToCamera();
		}
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00032450 File Offset: 0x00030650
	public override void OnDrawGizmos(CameraManager manager)
	{
		Gizmos.DrawRay(this.m_playerPosition, this.m_param.m_upDirection * 0.5f);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00032480 File Offset: 0x00030680
	protected virtual GimmickCameraBase.GimmickCameraParameter GetGimmickCameraParameter(CameraManager manager)
	{
		return this.m_gimmickCameraParam;
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00032488 File Offset: 0x00030688
	private void UpdateGimmickCamera(CameraManager manager, float deltaTime, Vector3 viewPort, ref Vector3 nowTarget)
	{
		GimmickCameraBase.Mode mode = this.m_mode;
		if (mode != GimmickCameraBase.Mode.Wait)
		{
			if (mode == GimmickCameraBase.Mode.Move)
			{
				viewPort.x = GimmickCameraBase.UpdateScroll(viewPort.x, this.m_gimmickCameraParam.m_leftScrollViewPort, this.m_speed.x * deltaTime);
				viewPort.y = GimmickCameraBase.UpdateScroll(viewPort.y, this.m_gimmickCameraParam.m_upScrollViewPort, this.m_speed.y * deltaTime);
				viewPort.z = GimmickCameraBase.UpdateScroll(viewPort.z, this.m_gimmickCameraParam.m_depthScrollViewPort, this.m_speed.z * deltaTime);
				nowTarget = this.GetNowTarget(viewPort, nowTarget);
				this.m_time += deltaTime;
				if (this.m_time > this.m_gimmickCameraParam.m_scrollTime)
				{
					this.m_mode = GimmickCameraBase.Mode.Idle;
				}
			}
		}
		else
		{
			this.m_time += deltaTime;
			if (this.m_time > this.m_gimmickCameraParam.m_waitTime)
			{
				this.m_speed.x = GimmickCameraBase.GetSpeed(viewPort.x, this.m_gimmickCameraParam.m_leftScrollViewPort, this.m_gimmickCameraParam.m_scrollTime, 0.01f);
				this.m_speed.y = GimmickCameraBase.GetSpeed(viewPort.y, this.m_gimmickCameraParam.m_upScrollViewPort, this.m_gimmickCameraParam.m_scrollTime, 0.01f);
				this.m_speed.z = GimmickCameraBase.GetSpeed(viewPort.z, this.m_gimmickCameraParam.m_depthScrollViewPort, this.m_gimmickCameraParam.m_scrollTime, 0.01f);
				this.m_mode = GimmickCameraBase.Mode.Move;
			}
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0003263C File Offset: 0x0003083C
	private Vector3 GetNowTarget(Vector3 viewPort, Vector3 nowTarget)
	{
		if (this.m_camera)
		{
			Vector3 b = this.m_camera.ViewportToWorldPoint(viewPort);
			return nowTarget + (this.m_playerPosition - b);
		}
		return nowTarget;
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0003267C File Offset: 0x0003087C
	private static float GetSpeed(float value, float tgt_value, float time, float minmax)
	{
		float value2 = (value - tgt_value) / time;
		return GimmickCameraBase.GetMinMax(value2, minmax);
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00032698 File Offset: 0x00030898
	private static float GetMinMax(float value, float minmax)
	{
		float num = Mathf.Abs(value);
		float b = Mathf.Abs(minmax);
		num = Mathf.Max(num, b);
		return (value >= 0f) ? num : (-num);
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000326D0 File Offset: 0x000308D0
	private static float UpdateScroll(float value, float tgt_value, float speed)
	{
		float num = value - speed;
		if (speed < 0f)
		{
			if (num > tgt_value)
			{
				num = tgt_value;
			}
		}
		else if (num < tgt_value)
		{
			num = tgt_value;
		}
		return num;
	}

	// Token: 0x0400068A RID: 1674
	protected GimmickCameraBase.Mode m_mode;

	// Token: 0x0400068B RID: 1675
	private GimmickCameraBase.GimmickCameraParameter m_gimmickCameraParam = default(GimmickCameraBase.GimmickCameraParameter);

	// Token: 0x0400068C RID: 1676
	private float m_time;

	// Token: 0x0400068D RID: 1677
	private Vector3 m_speed;

	// Token: 0x0400068E RID: 1678
	private Vector3 m_playerPosition;

	// Token: 0x0400068F RID: 1679
	private Camera m_camera;

	// Token: 0x02000124 RID: 292
	protected enum Mode
	{
		// Token: 0x04000691 RID: 1681
		Idle,
		// Token: 0x04000692 RID: 1682
		Wait,
		// Token: 0x04000693 RID: 1683
		Move
	}

	// Token: 0x02000125 RID: 293
	protected struct GimmickCameraParameter
	{
		// Token: 0x04000694 RID: 1684
		public float m_waitTime;

		// Token: 0x04000695 RID: 1685
		public float m_upScrollViewPort;

		// Token: 0x04000696 RID: 1686
		public float m_leftScrollViewPort;

		// Token: 0x04000697 RID: 1687
		public float m_depthScrollViewPort;

		// Token: 0x04000698 RID: 1688
		public float m_scrollTime;
	}
}
