using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000156 RID: 342
	public class ChaoMoveStopEnd : ChaoMoveBase
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
		public override void Enter(ChaoMovement context)
		{
			this.m_chao_pos = context.TargetPosition + context.Hovering + context.OffsetPosition;
			Camera main = Camera.main;
			if (main != null)
			{
				Vector3 position = main.WorldToScreenPoint(context.Position);
				position.x = 0f;
				Vector3 position2 = main.ScreenToWorldPoint(position);
				if (position2.x > context.Position.x)
				{
					context.Position = position2;
					this.m_distance = this.m_chao_pos.x - position2.x;
				}
				else
				{
					this.m_distance = this.m_chao_pos.x - context.Position.x;
				}
			}
			else
			{
				this.m_distance = 10f;
				Vector3 chao_pos = this.m_chao_pos;
				chao_pos.x -= this.m_distance;
				context.Position = chao_pos;
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0003C6D8 File Offset: 0x0003A8D8
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public override void Step(ChaoMovement context, float deltaTime)
		{
			this.m_chao_pos = context.TargetPosition + context.Hovering + context.OffsetPosition;
			this.m_distance -= context.ComeInSpeed * deltaTime;
			if (this.m_distance < 0f)
			{
				this.m_distance = 0f;
				context.NextState = true;
			}
			this.m_chao_pos.x = this.m_chao_pos.x - this.m_distance;
			context.Position = this.m_chao_pos;
		}

		// Token: 0x040007AE RID: 1966
		private Vector3 m_chao_pos = new Vector3(0f, 0f, 0f);

		// Token: 0x040007AF RID: 1967
		private float m_distance;
	}
}
