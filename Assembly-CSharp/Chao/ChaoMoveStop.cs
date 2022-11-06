using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000155 RID: 341
	public class ChaoMoveStop : ChaoMoveBase
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0003C56C File Offset: 0x0003A76C
		public override void Enter(ChaoMovement context)
		{
			this.m_init_pos = context.TargetPosition;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0003C57C File Offset: 0x0003A77C
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0003C580 File Offset: 0x0003A780
		public override void Step(ChaoMovement context, float deltaTime)
		{
			context.Position = this.m_init_pos + context.Hovering + context.OffsetPosition;
		}

		// Token: 0x040007AD RID: 1965
		private Vector3 m_init_pos = new Vector3(0f, 0f, 0f);
	}
}
