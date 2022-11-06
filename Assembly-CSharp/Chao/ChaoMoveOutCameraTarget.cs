using System;
using App;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200014B RID: 331
	public class ChaoMoveOutCameraTarget : ChaoMoveBase
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0003B3C8 File Offset: 0x000395C8
		public override void Enter(ChaoMovement context)
		{
			this.m_cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003B3DC File Offset: 0x000395DC
		public override void Leave(ChaoMovement context)
		{
			this.m_cameraObject = null;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0003B3E8 File Offset: 0x000395E8
		public override void Step(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			if (!context.IsPlyayerMoved)
			{
				return;
			}
			this.UpdateTargetPosition(context);
			Vector3 vector = this.m_targetPos - context.Position;
			Vector3 normalized = vector.normalized;
			float num = this.m_speedRate * context.PlayerInfo.DefaultSpeed;
			context.Velocity = normalized * num;
			Vector3 vector2 = context.Position;
			if (vector.sqrMagnitude < App.Math.Sqr(num * deltaTime))
			{
				vector2 = this.m_targetPos;
			}
			else
			{
				vector2 += context.Velocity * deltaTime;
			}
			context.Position = vector2;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0003B498 File Offset: 0x00039698
		public void SetParameter(Vector3 screenOffsetRate, float speedRate)
		{
			this.m_screenOffsetRate = screenOffsetRate;
			this.m_speedRate = speedRate;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0003B4A8 File Offset: 0x000396A8
		private bool UpdateTargetPosition(ChaoMovement context)
		{
			if (this.m_cameraObject == null)
			{
				return false;
			}
			Camera component = this.m_cameraObject.GetComponent<Camera>();
			if (component == null)
			{
				return false;
			}
			Vector3 position = component.WorldToScreenPoint(context.Position);
			position.x = -150f;
			position.y = component.pixelHeight * this.m_screenOffsetRate.y;
			this.m_targetPos = component.ScreenToWorldPoint(position);
			return true;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0003B524 File Offset: 0x00039724
		private bool IsOffscreen(Vector3 pos)
		{
			return Camera.main.WorldToScreenPoint(pos).x < 0f;
		}

		// Token: 0x04000775 RID: 1909
		private Vector3 m_screenOffsetRate;

		// Token: 0x04000776 RID: 1910
		private Vector3 m_targetPos;

		// Token: 0x04000777 RID: 1911
		private GameObject m_cameraObject;

		// Token: 0x04000778 RID: 1912
		private float m_speedRate;
	}
}
