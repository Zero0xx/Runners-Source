using System;

// Token: 0x02000121 RID: 289
public class CannonCamera : GimmickCameraBase
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00031EC0 File Offset: 0x000300C0
	public CannonCamera() : base(CameraType.CANNON)
	{
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00031ECC File Offset: 0x000300CC
	protected override GimmickCameraBase.GimmickCameraParameter GetGimmickCameraParameter(CameraManager manager)
	{
		CameraManager.CannonEditParameter cannonParameter = manager.CannonParameter;
		return new GimmickCameraBase.GimmickCameraParameter
		{
			m_waitTime = cannonParameter.m_waitTime,
			m_upScrollViewPort = cannonParameter.m_upScrollViewPort,
			m_leftScrollViewPort = cannonParameter.m_leftScrollViewPort,
			m_depthScrollViewPort = cannonParameter.m_depthScrollViewPort,
			m_scrollTime = cannonParameter.m_scrollTime
		};
	}
}
