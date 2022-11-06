using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200014C RID: 332
	public class ChaoMoveGoKillOut : ChaoMoveBase
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x0003B55C File Offset: 0x0003975C
		public override void Enter(ChaoMovement context)
		{
			this.m_mode = ChaoMoveGoKillOut.Mode.Up;
			this.m_cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			this.UpdateScreenPoint(context);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003B580 File Offset: 0x00039780
		public override void Leave(ChaoMovement context)
		{
			this.m_cameraObject = null;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0003B58C File Offset: 0x0003978C
		public override void Step(ChaoMovement context, float deltaTime)
		{
			this.UpdateScreenPoint(context);
			if (context.PlayerInfo == null)
			{
				return;
			}
			if (!context.IsPlyayerMoved)
			{
				return;
			}
			ChaoMoveGoKillOut.Mode mode = this.m_mode;
			if (mode != ChaoMoveGoKillOut.Mode.Up)
			{
				if (mode == ChaoMoveGoKillOut.Mode.Forward)
				{
					float speed = context.PlayerInfo.DefaultSpeed * 5f;
					this.MoveForward(context, speed, deltaTime);
				}
			}
			else
			{
				this.MoveUp(context, deltaTime);
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003B608 File Offset: 0x00039808
		public void ChangeMode(ChaoMoveGoKillOut.Mode mode)
		{
			this.m_mode = mode;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0003B614 File Offset: 0x00039814
		private void MoveUp(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			Vector3 a = context.PlayerPosition + context.OffsetPosition;
			float num = Vector3.Dot(a - context.Position, ChaoMovement.HorzDir);
			Vector3 a2 = (num >= 0f) ? (ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed) : (ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed * 0.5f);
			Vector3 b = (this.m_screenRate.y >= 0.8f) ? Vector3.zero : (ChaoMovement.VertDir * 4.5f);
			context.Velocity = a2 + b;
			context.Position += context.Velocity * deltaTime;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0003B6FC File Offset: 0x000398FC
		private void MoveForward(ChaoMovement context, float speed, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			context.Velocity = ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed * 5f;
			context.Position += context.Velocity * deltaTime;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0003B760 File Offset: 0x00039960
		private bool UpdateScreenPoint(ChaoMovement context)
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
			Vector3 vector = component.WorldToScreenPoint(context.Position);
			this.m_screenRate.x = vector.x / component.pixelWidth;
			this.m_screenRate.y = vector.y / component.pixelHeight;
			return true;
		}

		// Token: 0x04000779 RID: 1913
		private const float UpVelocity = 4.5f;

		// Token: 0x0400077A RID: 1914
		private const float SpeedRate = 5f;

		// Token: 0x0400077B RID: 1915
		private const float UpScrenRate = 0.8f;

		// Token: 0x0400077C RID: 1916
		private ChaoMoveGoKillOut.Mode m_mode;

		// Token: 0x0400077D RID: 1917
		private Vector3 m_screenRate;

		// Token: 0x0400077E RID: 1918
		private GameObject m_cameraObject;

		// Token: 0x0200014D RID: 333
		public enum Mode
		{
			// Token: 0x04000780 RID: 1920
			Up,
			// Token: 0x04000781 RID: 1921
			Forward
		}
	}
}
