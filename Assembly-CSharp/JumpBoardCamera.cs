using System;

// Token: 0x02000126 RID: 294
public class JumpBoardCamera : GimmickCameraBase
{
	// Token: 0x060008EF RID: 2287 RVA: 0x00032704 File Offset: 0x00030904
	public JumpBoardCamera() : base(CameraType.JUMPBOARD)
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00032710 File Offset: 0x00030910
	protected override GimmickCameraBase.GimmickCameraParameter GetGimmickCameraParameter(CameraManager manager)
	{
		CameraManager.JumpBoardEditParameter jumpBoardParameter = manager.JumpBoardParameter;
		return new GimmickCameraBase.GimmickCameraParameter
		{
			m_waitTime = jumpBoardParameter.m_waitTime,
			m_upScrollViewPort = jumpBoardParameter.m_upScrollViewPort,
			m_leftScrollViewPort = jumpBoardParameter.m_leftScrollViewPort,
			m_depthScrollViewPort = jumpBoardParameter.m_depthScrollViewPort,
			m_scrollTime = jumpBoardParameter.m_scrollTime
		};
	}
}
