using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000154 RID: 340
	public class ChaoMoveStay : ChaoMoveBase
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x0003C500 File Offset: 0x0003A700
		public override void Enter(ChaoMovement context)
		{
			this.m_stayPosition = context.Position - context.Hovering;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0003C51C File Offset: 0x0003A71C
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0003C520 File Offset: 0x0003A720
		public override void Step(ChaoMovement context, float deltaTime)
		{
			context.Position = this.m_stayPosition + context.Hovering;
		}

		// Token: 0x040007AC RID: 1964
		private Vector3 m_stayPosition;
	}
}
