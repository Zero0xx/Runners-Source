using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000147 RID: 327
	public class ChaoMoveComeIn : ChaoMoveBase
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0003ACFC File Offset: 0x00038EFC
		public override void Enter(ChaoMovement context)
		{
			this.m_chao_pos = context.TargetPosition;
			Camera main = Camera.main;
			if (main != null)
			{
				Vector3 position = main.WorldToScreenPoint(context.Position);
				position.x = 0f;
				Vector3 position2 = main.ScreenToWorldPoint(position);
				context.Position = position2;
				this.m_distance = this.m_chao_pos.x - position2.x;
			}
			else
			{
				this.m_distance = 10f;
				Vector3 chao_pos = this.m_chao_pos;
				chao_pos.x -= this.m_distance;
				context.Position = chao_pos;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0003AD9C File Offset: 0x00038F9C
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		public override void Step(ChaoMovement context, float deltaTime)
		{
			this.m_chao_pos = context.TargetPosition + context.Hovering + context.OffsetPosition;
			this.m_distance -= context.ComeInSpeed * deltaTime;
			this.m_chao_pos.x = this.m_chao_pos.x - this.m_distance;
			context.Position = this.m_chao_pos;
			if (this.m_distance < 0f)
			{
				this.m_distance = 0f;
				context.NextState = true;
			}
		}

		// Token: 0x04000764 RID: 1892
		private Vector3 m_chao_pos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000765 RID: 1893
		private float m_distance;
	}
}
