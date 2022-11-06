using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000145 RID: 325
	public class ChaoMoveAttackBoss : ChaoMoveBase
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0003A9B8 File Offset: 0x00038BB8
		public override void Enter(ChaoMovement context)
		{
			this.m_mode = ChaoMoveAttackBoss.Mode.Up;
			this.m_boss = null;
			context.Velocity = Vector3.zero;
			if (context.PlayerInfo != null)
			{
				this.m_prevPlrPos = context.PlayerInfo.Position;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0003AA00 File Offset: 0x00038C00
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0003AA04 File Offset: 0x00038C04
		public override void Step(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			if (this.m_boss == null)
			{
				context.Position += context.Velocity * deltaTime;
				this.m_prevPlrPos = context.PlayerInfo.Position;
				return;
			}
			Vector3 position = this.m_boss.transform.position;
			switch (this.m_mode)
			{
			case ChaoMoveAttackBoss.Mode.Up:
				this.MoveUp(context, deltaTime);
				break;
			case ChaoMoveAttackBoss.Mode.Homing:
			{
				float speed = context.PlayerInfo.DefaultSpeed * 4f;
				this.MoveHoming(context, position, speed, deltaTime);
				break;
			}
			case ChaoMoveAttackBoss.Mode.AfterAttack:
				this.MoveAfterAttack(context, deltaTime);
				break;
			}
			this.m_prevPlrPos = context.PlayerInfo.Position;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		public void SetTarget(GameObject boss)
		{
			this.m_boss = boss;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003AAEC File Offset: 0x00038CEC
		public void ChangeMode(ChaoMoveAttackBoss.Mode mode)
		{
			this.m_mode = mode;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0003AAF8 File Offset: 0x00038CF8
		private void MoveHoming(ChaoMovement context, Vector3 targetPosition, float speed, float deltaTime)
		{
			Vector3 vector = targetPosition - context.Position;
			float magnitude = vector.magnitude;
			Vector3 normalized = vector.normalized;
			if (magnitude < speed * deltaTime)
			{
				context.Position = targetPosition;
			}
			else
			{
				context.Velocity = normalized * speed;
				context.Position += context.Velocity * deltaTime;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0003AB64 File Offset: 0x00038D64
		private void MoveUp(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			if (Vector3.Distance(this.m_prevPlrPos, context.PlayerInfo.Position) < 1E-45f)
			{
				return;
			}
			context.Velocity = ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed + ChaoMovement.VertDir * 4.5f;
			context.Position += context.Velocity * deltaTime;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0003ABF0 File Offset: 0x00038DF0
		private void MoveAfterAttack(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			Vector3 position = context.PlayerInfo.Position;
			if (Vector3.Distance(this.m_prevPlrPos, position) < 1E-45f)
			{
				return;
			}
			Vector3 lhs = position - context.Position;
			float num = Vector3.Dot(lhs, ChaoMovement.HorzDir);
			Vector3 a;
			if (num > 0f)
			{
				a = ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed;
			}
			else
			{
				a = ChaoMovement.HorzDir * context.PlayerInfo.DefaultSpeed * 0.25f;
			}
			context.Velocity = a + ChaoMovement.VertDir * 7f;
			context.Position += context.Velocity * deltaTime;
		}

		// Token: 0x0400075A RID: 1882
		private const float UpVelocity = 4.5f;

		// Token: 0x0400075B RID: 1883
		private const float AfterAttackVelocity = 7f;

		// Token: 0x0400075C RID: 1884
		private const float SpeedRate = 4f;

		// Token: 0x0400075D RID: 1885
		private ChaoMoveAttackBoss.Mode m_mode;

		// Token: 0x0400075E RID: 1886
		private GameObject m_boss;

		// Token: 0x0400075F RID: 1887
		private Vector3 m_prevPlrPos;

		// Token: 0x02000146 RID: 326
		public enum Mode
		{
			// Token: 0x04000761 RID: 1889
			Up,
			// Token: 0x04000762 RID: 1890
			Homing,
			// Token: 0x04000763 RID: 1891
			AfterAttack
		}
	}
}
