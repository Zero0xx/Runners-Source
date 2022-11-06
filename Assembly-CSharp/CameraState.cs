using System;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class CameraState
{
	// Token: 0x060008D0 RID: 2256 RVA: 0x00031CE4 File Offset: 0x0002FEE4
	public CameraState(CameraType type)
	{
		this.m_param = default(CameraParameter);
		this.m_param.m_upDirection = Vector3.up;
		this.m_type = type;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00031D20 File Offset: 0x0002FF20
	public virtual void OnEnter(CameraManager manager)
	{
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x00031D24 File Offset: 0x0002FF24
	public virtual void OnLeave(CameraManager manager)
	{
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x00031D28 File Offset: 0x0002FF28
	public virtual void Update(CameraManager manager, float deltaTime)
	{
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00031D2C File Offset: 0x0002FF2C
	public void GetCameraParameter(ref CameraParameter parameter)
	{
		parameter.m_target = this.m_param.m_target;
		parameter.m_position = this.m_param.m_position;
		parameter.m_upDirection = this.m_param.m_upDirection;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00031D64 File Offset: 0x0002FF64
	public void SetCameraParameter(CameraParameter param)
	{
		this.m_param.m_target = param.m_target;
		this.m_param.m_position = param.m_position;
		this.m_param.m_upDirection = param.m_upDirection;
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00031DA8 File Offset: 0x0002FFA8
	public CameraType GetCameraType()
	{
		return this.m_type;
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00031DB0 File Offset: 0x0002FFB0
	public virtual void OnDrawGizmos(CameraManager manager)
	{
	}

	// Token: 0x04000670 RID: 1648
	protected CameraParameter m_param;

	// Token: 0x04000671 RID: 1649
	private CameraType m_type;
}
