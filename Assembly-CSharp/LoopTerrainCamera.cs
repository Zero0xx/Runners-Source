using System;

// Token: 0x02000129 RID: 297
public class LoopTerrainCamera : GimmickCameraBase
{
	// Token: 0x060008F5 RID: 2293 RVA: 0x000329C8 File Offset: 0x00030BC8
	public LoopTerrainCamera() : base(CameraType.LOOP_TERRAIN)
	{
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000329D4 File Offset: 0x00030BD4
	protected override GimmickCameraBase.GimmickCameraParameter GetGimmickCameraParameter(CameraManager manager)
	{
		CameraManager.LoopTerrainEditParameter loopTerrainParameter = manager.LoopTerrainParameter;
		return new GimmickCameraBase.GimmickCameraParameter
		{
			m_waitTime = loopTerrainParameter.m_waitTime,
			m_upScrollViewPort = loopTerrainParameter.m_upScrollViewPort,
			m_leftScrollViewPort = loopTerrainParameter.m_leftScrollViewPort,
			m_depthScrollViewPort = loopTerrainParameter.m_depthScrollViewPort,
			m_scrollTime = loopTerrainParameter.m_scrollTime
		};
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00032A34 File Offset: 0x00030C34
	public override void Update(CameraManager manager, float deltaTime)
	{
		if (this.m_mode != GimmickCameraBase.Mode.Idle)
		{
			base.Update(manager, deltaTime);
		}
	}
}
