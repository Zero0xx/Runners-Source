using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class StartCamera : CameraState
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x00032A4C File Offset: 0x00030C4C
	public StartCamera() : base(CameraType.START_ACT)
	{
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00032A58 File Offset: 0x00030C58
	public override void OnEnter(CameraManager manager)
	{
		base.SetCameraParameter(manager.GetParameter());
		this.m_nearTargetOffset = manager.StartActParameter.m_targetOffset;
		this.m_nearCameraOffset = manager.StartActParameter.m_cameraOffset;
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		this.m_playerPosition = playerInformation.Position;
		this.m_type = StartCamera.Type.NEAR;
		this.m_rate = 0f;
		this.m_perRate = 0f;
		this.m_timer = manager.StartActParameter.m_nearStayTime;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00032AD4 File Offset: 0x00030CD4
	public override void Update(CameraManager manager, float deltaTime)
	{
		PlayerInformation playerInformation = manager.GetPlayerInformation();
		Vector3 position = playerInformation.Position;
		this.m_playerPosition.x = position.x;
		this.m_playerPosition.y = position.y;
		this.m_playerPosition.z = 0f;
		Vector3 vector = this.m_playerPosition + this.m_nearTargetOffset;
		Vector3 vector2 = vector + this.m_nearCameraOffset;
		vector2.z -= vector.z;
		Vector3 vector3 = vector;
		Vector3 vector4 = vector2;
		CameraState cameraByType = manager.GetCameraByType(CameraType.DEFAULT);
		if (cameraByType != null)
		{
			CameraParameter param = this.m_param;
			cameraByType.GetCameraParameter(ref param);
			vector3 = param.m_target;
			vector4 = param.m_position;
		}
		switch (this.m_type)
		{
		case StartCamera.Type.NEAR:
			this.m_param.m_target = vector;
			this.m_param.m_position = vector2;
			this.m_timer -= deltaTime;
			if (this.m_timer < 0f)
			{
				if (manager.StartActParameter.m_nearToFarTime > 0f)
				{
					this.m_rate = 0f;
					this.m_perRate = 1f / manager.StartActParameter.m_nearToFarTime;
					this.m_type = StartCamera.Type.BACK;
				}
				else
				{
					this.m_type = StartCamera.Type.FAR;
				}
			}
			break;
		case StartCamera.Type.BACK:
			this.m_rate += this.m_perRate * deltaTime;
			if (this.m_rate > 1f)
			{
				this.m_param.m_target = vector3;
				this.m_param.m_position = vector4;
				this.m_type = StartCamera.Type.FAR;
			}
			else
			{
				this.m_param.m_target = Vector3.Lerp(vector, vector3, this.m_rate);
				this.m_param.m_position = Vector3.Lerp(vector2, vector4, this.m_rate);
			}
			break;
		case StartCamera.Type.FAR:
			this.m_param.m_target = vector3;
			this.m_param.m_position = vector4;
			break;
		}
	}

	// Token: 0x040006A0 RID: 1696
	private StartCamera.Type m_type;

	// Token: 0x040006A1 RID: 1697
	private Vector3 m_nearTargetOffset;

	// Token: 0x040006A2 RID: 1698
	private Vector3 m_nearCameraOffset;

	// Token: 0x040006A3 RID: 1699
	private Vector3 m_playerPosition;

	// Token: 0x040006A4 RID: 1700
	private float m_timer;

	// Token: 0x040006A5 RID: 1701
	private float m_rate;

	// Token: 0x040006A6 RID: 1702
	private float m_perRate;

	// Token: 0x0200012B RID: 299
	private enum Type
	{
		// Token: 0x040006A8 RID: 1704
		NEAR,
		// Token: 0x040006A9 RID: 1705
		BACK,
		// Token: 0x040006AA RID: 1706
		FAR
	}
}
