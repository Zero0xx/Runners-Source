using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000148 RID: 328
	public class ChaoMoveExit : ChaoMoveBase
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x0003AE78 File Offset: 0x00039078
		public override void Enter(ChaoMovement context)
		{
			this.m_init_pos = context.TargetPosition;
			this.m_chao_pos = this.m_init_pos + context.Hovering + context.OffsetPosition;
			this.m_move_distance = 0f;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0003AEC0 File Offset: 0x000390C0
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0003AEC4 File Offset: 0x000390C4
		public override void Step(ChaoMovement context, float deltaTime)
		{
			if (!context.NextState)
			{
				this.m_chao_pos = this.m_init_pos + context.Hovering + context.OffsetPosition;
				if (context.PlayerInfo != null)
				{
					float x = context.PlayerInfo.HorizonVelocity.x;
					float num = x - 2f * context.ComeInSpeed;
					this.m_move_distance += num * deltaTime;
					this.m_chao_pos.x = this.m_chao_pos.x + this.m_move_distance;
					if (this.IsOffscreen())
					{
						context.NextState = true;
					}
				}
				context.Position = this.m_chao_pos;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003AF80 File Offset: 0x00039180
		private bool IsOffscreen()
		{
			return Camera.main.WorldToScreenPoint(this.m_chao_pos).x < 0f;
		}

		// Token: 0x04000766 RID: 1894
		private Vector3 m_chao_pos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000767 RID: 1895
		private Vector3 m_init_pos = new Vector3(0f, 0f, 0f);

		// Token: 0x04000768 RID: 1896
		private float m_move_distance;
	}
}
