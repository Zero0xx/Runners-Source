using System;
using App;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200014A RID: 330
	public class ChaoMoveGoCameraTargetUsePlayerSpeed : ChaoMoveBase
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0003B1E4 File Offset: 0x000393E4
		public override void Enter(ChaoMovement context)
		{
			this.m_cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			if (this.m_cameraObject != null)
			{
				Camera component = this.m_cameraObject.GetComponent<Camera>();
				if (component != null)
				{
					Vector3 position = component.WorldToScreenPoint(context.Position);
					if (position.x < 0f)
					{
						position.x = -0.5f;
						context.Position = component.ScreenToWorldPoint(position);
					}
				}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0003B264 File Offset: 0x00039464
		public override void Leave(ChaoMovement context)
		{
			this.m_cameraObject = null;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0003B270 File Offset: 0x00039470
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
			float num = this.m_speedRate * context.PlayerInfo.FrontSpeed;
			context.Velocity = normalized * num;
			Vector3 vector2 = context.Position;
			if (vector.sqrMagnitude < App.Math.Sqr(num * deltaTime))
			{
				vector2 = this.m_targetPos;
				context.NextState = true;
			}
			else
			{
				vector2 += context.Velocity * deltaTime;
			}
			context.Position = vector2;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0003B328 File Offset: 0x00039528
		public void SetParameter(Vector3 screenOffsetRate, float speedRate)
		{
			this.m_screenOffsetRate = screenOffsetRate;
			this.m_speedRate = speedRate;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003B338 File Offset: 0x00039538
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
			position.x = component.pixelWidth * this.m_screenOffsetRate.x;
			position.y = component.pixelHeight * this.m_screenOffsetRate.y;
			this.m_targetPos = component.ScreenToWorldPoint(position);
			return true;
		}

		// Token: 0x04000771 RID: 1905
		private Vector3 m_screenOffsetRate;

		// Token: 0x04000772 RID: 1906
		private Vector3 m_targetPos;

		// Token: 0x04000773 RID: 1907
		private GameObject m_cameraObject;

		// Token: 0x04000774 RID: 1908
		private float m_speedRate;
	}
}
