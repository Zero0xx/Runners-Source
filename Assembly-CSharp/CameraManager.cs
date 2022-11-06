using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class CameraManager : MonoBehaviour
{
	// Token: 0x060008AF RID: 2223 RVA: 0x00030EEC File Offset: 0x0002F0EC
	private void Start()
	{
		Camera camera = base.camera;
		float num = camera.fieldOfView;
		ScreenType screenType = ScreenUtil.GetScreenType();
		float num2 = 1.5f;
		if (screenType == ScreenType.iPhone5)
		{
			float num3 = 1.7777778f;
			num /= num3 / num2;
			camera.fieldOfView = num;
		}
		else if (screenType == ScreenType.iPad)
		{
			float num4 = 1.3333334f;
			num /= num4 / num2;
			camera.fieldOfView = num;
		}
		this.m_playerInfo = GameObject.Find("PlayerInformation").GetComponent<PlayerInformation>();
		this.m_playerPosition = this.m_playerInfo.Position;
		base.transform.position = this.m_startCameraPos;
		this.m_param.m_target = base.transform.position;
		this.m_param.m_target.z = this.m_playerPosition.z;
		this.m_param.m_position = base.transform.position;
		this.m_param.m_upDirection = Vector3.up;
		this.m_defaultTargetOffset = base.transform.position - this.m_playerPosition;
		this.m_defaultTargetOffset.z = 0f;
		base.transform.position = this.m_param.m_position;
		base.transform.LookAt(this.m_param.m_target, this.m_param.m_upDirection);
		this.m_targetToCamera = this.m_param.m_position - this.m_param.m_target;
		GameFollowCamera state = new GameFollowCamera();
		this.PushCamera(state);
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00031070 File Offset: 0x0002F270
	private void LateUpdate()
	{
		if (this.m_playerInfo == null)
		{
			return;
		}
		if (this.m_playerInfo.IsDead())
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (deltaTime == 0f)
		{
			return;
		}
		foreach (CameraState cameraState in this.m_cameraList)
		{
			cameraState.Update(this, deltaTime);
		}
		int num = this.m_cameraList.Count - 1;
		if (this.IsNowInterpolate())
		{
			this.m_interpolateRate += this.m_ratePerSec * deltaTime;
			if (this.m_interpolateTime > 0f)
			{
				this.m_interpolateTime -= deltaTime;
				if (this.m_interpolateTime <= 0f)
				{
					if (this.m_interpolateRate < 0.5f)
					{
						CameraState state = this.m_cameraList[num];
						this.PopCamera(state);
					}
					this.m_interpolateTime = 0f;
				}
			}
			if (this.m_interpolateRate > 1f)
			{
				this.m_interpolateRate = 1f;
			}
			else if (this.m_interpolateRate < 0f)
			{
				this.m_interpolateRate = 0f;
			}
		}
		num = this.m_cameraList.Count - 1;
		if (this.IsNowInterpolate())
		{
			if (this.m_ratePerSec < 0f)
			{
				CameraState cameraState2 = this.m_cameraList[num - 1];
				cameraState2.GetCameraParameter(ref this.m_lowerParam);
				if (this.m_differenceApproachFlag)
				{
					CameraState cameraState3 = this.m_cameraList[num];
					cameraState3.GetCameraParameter(ref this.m_topParam);
					this.m_differencePos = this.m_lowerParam.m_position - this.m_topParam.m_position;
					this.m_differenceTarget = this.m_lowerParam.m_target - this.m_topParam.m_target;
					this.m_differenceApproachFlag = false;
				}
				float t = Mathf.Max(1f - this.m_interpolateRate, 0f);
				this.m_param.m_position = this.m_lowerParam.m_position - Vector3.Lerp(this.m_differencePos, Vector3.zero, t);
				this.m_param.m_target = this.m_lowerParam.m_target - Vector3.Lerp(this.m_differenceTarget, Vector3.zero, t);
				this.m_param.m_upDirection = Vector3.Lerp(this.m_param.m_upDirection, this.m_lowerParam.m_upDirection, t);
			}
			else
			{
				CameraState cameraState4 = this.m_cameraList[num];
				cameraState4.GetCameraParameter(ref this.m_topParam);
				this.m_param.m_position = Vector3.Lerp(this.m_param.m_position, this.m_topParam.m_position, this.m_interpolateRate);
				this.m_param.m_target = Vector3.Lerp(this.m_param.m_target, this.m_topParam.m_target, this.m_interpolateRate);
				this.m_param.m_upDirection = Vector3.Lerp(this.m_param.m_upDirection, this.m_topParam.m_upDirection, this.m_interpolateRate);
			}
		}
		else
		{
			CameraState cameraState5 = this.m_cameraList[num];
			cameraState5.GetCameraParameter(ref this.m_param);
		}
		base.transform.position = this.m_param.m_position;
		base.transform.LookAt(this.m_param.m_target, this.m_param.m_upDirection);
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x00031420 File Offset: 0x0002F620
	private void PushNewCamera(CameraType camType, UnityEngine.Object parameter, float interpolateTime)
	{
		CameraState cameraState = this.CreateNewCamera(camType, parameter);
		if (cameraState != null)
		{
			this.PushCamera(cameraState);
			this.StartInterpolate(true, interpolateTime);
			CameraState prevGimmickCamera = this.GetPrevGimmickCamera();
			if (prevGimmickCamera != null)
			{
				this.PopCamera(prevGimmickCamera);
			}
		}
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00031460 File Offset: 0x0002F660
	private void PopCamera(CameraType camType, float interpolateTime)
	{
		CameraState cameraByType = this.GetCameraByType(camType);
		if (cameraByType != null)
		{
			if (cameraByType == this.GetTopCamera())
			{
				CameraState prevGimmickCamera = this.GetPrevGimmickCamera();
				if (prevGimmickCamera != null)
				{
					this.PopCamera(prevGimmickCamera);
				}
				if (!this.IsNowInterpolate() || this.m_ratePerSec >= 0f)
				{
					this.StartInterpolate(false, interpolateTime);
				}
			}
			else
			{
				this.PopCamera(cameraByType);
			}
		}
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x000314D0 File Offset: 0x0002F6D0
	private CameraState CreateNewCamera(CameraType camType, UnityEngine.Object parameter)
	{
		CameraState result = null;
		switch (camType)
		{
		case CameraType.DEFAULT:
			result = new GameFollowCamera();
			break;
		case CameraType.LASER:
			result = new LaserCamera();
			break;
		case CameraType.JUMPBOARD:
			result = new JumpBoardCamera();
			break;
		case CameraType.CANNON:
			result = new CannonCamera();
			break;
		case CameraType.LOOP_TERRAIN:
			result = new LoopTerrainCamera();
			break;
		case CameraType.START_ACT:
			result = new StartCamera();
			break;
		}
		return result;
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00031548 File Offset: 0x0002F748
	public CameraState GetCameraByType(CameraType type)
	{
		foreach (CameraState cameraState in this.m_cameraList)
		{
			if (cameraState.GetCameraType() == type)
			{
				return cameraState;
			}
		}
		return null;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x000315C0 File Offset: 0x0002F7C0
	private CameraState GetTopCamera()
	{
		return this.m_cameraList[this.m_cameraList.Count - 1];
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x000315DC File Offset: 0x0002F7DC
	private void PushCamera(CameraState state)
	{
		state.OnEnter(this);
		this.m_cameraList.Add(state);
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x000315F4 File Offset: 0x0002F7F4
	private void PopCamera(CameraState state)
	{
		state.OnLeave(this);
		this.m_cameraList.Remove(state);
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0003160C File Offset: 0x0002F80C
	private void StartInterpolate(bool push, float time)
	{
		this.m_interpolateTime = time;
		if (push)
		{
			this.m_interpolateRate = 0f;
			this.m_ratePerSec = 1f / time;
		}
		else
		{
			this.m_interpolateRate = 1f;
			this.m_ratePerSec = -(1f / time);
			this.m_differenceApproachFlag = true;
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00031664 File Offset: 0x0002F864
	private void OnDrawGizmos()
	{
		foreach (CameraState cameraState in this.m_cameraList)
		{
			cameraState.OnDrawGizmos(this);
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x000316CC File Offset: 0x0002F8CC
	private void OnPushCamera(MsgPushCamera msg)
	{
		this.PushNewCamera(msg.m_type, msg.m_parameter, msg.m_interpolateTime);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x000316E8 File Offset: 0x0002F8E8
	private void OnPopCamera(MsgPopCamera msg)
	{
		this.PopCamera(msg.m_type, msg.m_interpolateTime);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x000316FC File Offset: 0x0002F8FC
	private CameraState GetPrevGimmickCamera()
	{
		CameraState cameraState = null;
		foreach (CameraState cameraState2 in this.m_cameraList)
		{
			CameraType cameraType = cameraState2.GetCameraType();
			if (CameraTypeData.IsGimmickCamera(cameraType))
			{
				if (cameraState != null)
				{
					return cameraState;
				}
				cameraState = cameraState2;
			}
		}
		return null;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00031788 File Offset: 0x0002F988
	public PlayerInformation GetPlayerInformation()
	{
		return this.m_playerInfo;
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00031790 File Offset: 0x0002F990
	public CameraParameter GetParameter()
	{
		return this.m_param;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00031798 File Offset: 0x0002F998
	public Vector3 GetTargetOffset()
	{
		return this.m_defaultTargetOffset;
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x000317A0 File Offset: 0x0002F9A0
	public Vector3 GetTargetToCamera()
	{
		return this.m_targetToCamera;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x000317A8 File Offset: 0x0002F9A8
	private bool IsNowInterpolate()
	{
		return this.m_interpolateTime > 0f && this.m_cameraList.Count > 1;
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000317CC File Offset: 0x0002F9CC
	public CameraManager.CameraEditParameter EditParameter
	{
		get
		{
			return this.m_editParameter;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000317D4 File Offset: 0x0002F9D4
	public CameraManager.LaserEditParameter LaserParameter
	{
		get
		{
			return this.m_laserParameter;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000317DC File Offset: 0x0002F9DC
	public CameraManager.JumpBoardEditParameter JumpBoardParameter
	{
		get
		{
			return this.m_jumpBoardParameter;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000317E4 File Offset: 0x0002F9E4
	public CameraManager.CannonEditParameter CannonParameter
	{
		get
		{
			return this.m_cannonParameter;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000317EC File Offset: 0x0002F9EC
	public CameraManager.LoopTerrainEditParameter LoopTerrainParameter
	{
		get
		{
			return this.m_loopTerrainParameter;
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000317F4 File Offset: 0x0002F9F4
	public CameraManager.StartActEditParameter StartActParameter
	{
		get
		{
			return this.m_startActParameter;
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x000317FC File Offset: 0x0002F9FC
	public void OnMsgTutorialResetForRetry(MsgTutorialResetForRetry msg)
	{
		while (this.GetTopCamera().GetCameraType() != CameraType.DEFAULT)
		{
			this.PopCamera(this.GetTopCamera());
		}
		GameFollowCamera gameFollowCamera = this.GetCameraByType(CameraType.DEFAULT) as GameFollowCamera;
		if (gameFollowCamera != null)
		{
			gameFollowCamera.OnMsgTutorialResetForRetry(this, msg);
		}
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x00031848 File Offset: 0x0002FA48
	private void WindowFunction(int windowID)
	{
		string text = string.Empty;
		CameraParameter param = this.m_param;
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"POS  :",
			param.m_position.x.ToString("F2"),
			" ",
			param.m_position.y.ToString("F2"),
			" ",
			param.m_position.z.ToString("F2"),
			" \n"
		});
		text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"TARGET:",
			param.m_target.x.ToString("F2"),
			" ",
			param.m_target.y.ToString("F2"),
			" ",
			param.m_target.z.ToString("F2"),
			" \n"
		});
		text += "\n";
		for (int i = this.m_cameraList.Count - 1; i >= 0; i--)
		{
			CameraState cameraState = this.m_cameraList[i];
			text = text + cameraState.ToString() + ":\n";
			CameraParameter cameraParameter = default(CameraParameter);
			cameraState.GetCameraParameter(ref cameraParameter);
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"POS  :",
				cameraParameter.m_position.x.ToString("F2"),
				" ",
				cameraParameter.m_position.y.ToString("F2"),
				" ",
				cameraParameter.m_position.z.ToString("F2"),
				" \n"
			});
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"TARGET:",
				cameraParameter.m_target.x.ToString("F2"),
				" ",
				cameraParameter.m_target.y.ToString("F2"),
				" ",
				cameraParameter.m_target.z.ToString("F2"),
				" \n"
			});
		}
		if (this.IsNowInterpolate())
		{
			text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				"Interpolate time:",
				this.m_interpolateTime.ToString("F2"),
				" rate:",
				this.m_interpolateRate.ToString("F2")
			});
		}
		GUIContent guicontent = new GUIContent();
		guicontent.text = text;
		Rect position = new Rect(10f, 20f, 270f, 280f);
		GUI.Label(position, guicontent);
	}

	// Token: 0x04000637 RID: 1591
	private PlayerInformation m_playerInfo;

	// Token: 0x04000638 RID: 1592
	private Vector3 m_defaultTargetOffset;

	// Token: 0x04000639 RID: 1593
	private Vector3 m_playerPosition;

	// Token: 0x0400063A RID: 1594
	private float m_prevDistanceGround;

	// Token: 0x0400063B RID: 1595
	private Vector3 m_targetToCamera;

	// Token: 0x0400063C RID: 1596
	private Vector3 m_differencePos;

	// Token: 0x0400063D RID: 1597
	private Vector3 m_differenceTarget;

	// Token: 0x0400063E RID: 1598
	private bool m_differenceApproachFlag;

	// Token: 0x0400063F RID: 1599
	private List<CameraState> m_cameraList = new List<CameraState>();

	// Token: 0x04000640 RID: 1600
	private float m_interpolateTime;

	// Token: 0x04000641 RID: 1601
	private float m_interpolateRate;

	// Token: 0x04000642 RID: 1602
	private float m_ratePerSec;

	// Token: 0x04000643 RID: 1603
	private CameraParameter m_param = default(CameraParameter);

	// Token: 0x04000644 RID: 1604
	private CameraParameter m_topParam = default(CameraParameter);

	// Token: 0x04000645 RID: 1605
	private CameraParameter m_lowerParam = default(CameraParameter);

	// Token: 0x04000646 RID: 1606
	public Vector3 m_startCameraPos = new Vector3(4.5f, 2f, -18f);

	// Token: 0x04000647 RID: 1607
	public CameraManager.CameraEditParameter m_editParameter = new CameraManager.CameraEditParameter();

	// Token: 0x04000648 RID: 1608
	public CameraManager.LaserEditParameter m_laserParameter = new CameraManager.LaserEditParameter();

	// Token: 0x04000649 RID: 1609
	public CameraManager.JumpBoardEditParameter m_jumpBoardParameter = new CameraManager.JumpBoardEditParameter();

	// Token: 0x0400064A RID: 1610
	public CameraManager.CannonEditParameter m_cannonParameter = new CameraManager.CannonEditParameter();

	// Token: 0x0400064B RID: 1611
	public CameraManager.LoopTerrainEditParameter m_loopTerrainParameter = new CameraManager.LoopTerrainEditParameter();

	// Token: 0x0400064C RID: 1612
	public CameraManager.StartActEditParameter m_startActParameter = new CameraManager.StartActEditParameter();

	// Token: 0x0400064D RID: 1613
	[SerializeField]
	private bool m_drawInfo;

	// Token: 0x0400064E RID: 1614
	[SerializeField]
	private bool m_debugInterpolate;

	// Token: 0x0400064F RID: 1615
	[SerializeField]
	private float m_debugPushInterpolateTime = 0.5f;

	// Token: 0x04000650 RID: 1616
	[SerializeField]
	private float m_debugPopInterpolateTime = 0.5f;

	// Token: 0x04000651 RID: 1617
	private Rect m_window;

	// Token: 0x02000116 RID: 278
	[Serializable]
	public class CameraEditParameter
	{
		// Token: 0x04000652 RID: 1618
		public float m_upScrollLine = 3f;

		// Token: 0x04000653 RID: 1619
		public float m_downScrollLine = -1f;

		// Token: 0x04000654 RID: 1620
		public float m_upScrollLimit = 10f;

		// Token: 0x04000655 RID: 1621
		public float m_downScrollLimit = -5f;

		// Token: 0x04000656 RID: 1622
		public float m_downScrollLineOnDown = 2f;
	}

	// Token: 0x02000117 RID: 279
	[Serializable]
	public class LaserEditParameter
	{
		// Token: 0x04000657 RID: 1623
		public float m_upScrollViewPort = 0.8f;

		// Token: 0x04000658 RID: 1624
		public float m_downScrollViewPort = 0.2f;

		// Token: 0x04000659 RID: 1625
		public float m_rightScrollViewPort = 0.758f;

		// Token: 0x0400065A RID: 1626
		public float m_leftScrollViewPort = 0.1f;

		// Token: 0x0400065B RID: 1627
		public float m_fastScrollTime = 0.2f;

		// Token: 0x0400065C RID: 1628
		public float m_slowScrollSpeed = 0.5f;
	}

	// Token: 0x02000118 RID: 280
	[Serializable]
	public class JumpBoardEditParameter
	{
		// Token: 0x0400065D RID: 1629
		public float m_waitTime;

		// Token: 0x0400065E RID: 1630
		public float m_upScrollViewPort = 0.6f;

		// Token: 0x0400065F RID: 1631
		public float m_leftScrollViewPort = 0.2f;

		// Token: 0x04000660 RID: 1632
		public float m_depthScrollViewPort = 26f;

		// Token: 0x04000661 RID: 1633
		public float m_scrollTime = 0.5f;
	}

	// Token: 0x02000119 RID: 281
	[Serializable]
	public class CannonEditParameter
	{
		// Token: 0x04000662 RID: 1634
		public float m_waitTime;

		// Token: 0x04000663 RID: 1635
		public float m_upScrollViewPort = 0.3f;

		// Token: 0x04000664 RID: 1636
		public float m_leftScrollViewPort = 0.2f;

		// Token: 0x04000665 RID: 1637
		public float m_depthScrollViewPort = 26f;

		// Token: 0x04000666 RID: 1638
		public float m_scrollTime = 0.5f;
	}

	// Token: 0x0200011A RID: 282
	[Serializable]
	public class LoopTerrainEditParameter
	{
		// Token: 0x04000667 RID: 1639
		public float m_waitTime;

		// Token: 0x04000668 RID: 1640
		public float m_upScrollViewPort = 0.3f;

		// Token: 0x04000669 RID: 1641
		public float m_leftScrollViewPort = 0.1f;

		// Token: 0x0400066A RID: 1642
		public float m_depthScrollViewPort = 18f;

		// Token: 0x0400066B RID: 1643
		public float m_scrollTime;
	}

	// Token: 0x0200011B RID: 283
	[Serializable]
	public class StartActEditParameter
	{
		// Token: 0x0400066C RID: 1644
		public Vector3 m_cameraOffset = new Vector3(0f, 0f, -10f);

		// Token: 0x0400066D RID: 1645
		public Vector3 m_targetOffset = new Vector3(0f, 0.5f, 0f);

		// Token: 0x0400066E RID: 1646
		public float m_nearStayTime = 2.5f;

		// Token: 0x0400066F RID: 1647
		public float m_nearToFarTime = 1f;
	}
}
