using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000150 RID: 336
	public class ChaoMoveLastChanceEnd : ChaoMoveBase
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x0003BD14 File Offset: 0x00039F14
		public override void Enter(ChaoMovement context)
		{
			if (context.gameObject != null && context.gameObject.transform != null)
			{
				Vector3 localEulerAngles = context.gameObject.transform.localEulerAngles;
				localEulerAngles.y = 90f;
				context.gameObject.transform.localEulerAngles = localEulerAngles;
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003BD78 File Offset: 0x00039F78
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003BD7C File Offset: 0x00039F7C
		public override void Step(ChaoMovement context, float deltaTime)
		{
			context.Position = context.TargetPosition;
		}
	}
}
