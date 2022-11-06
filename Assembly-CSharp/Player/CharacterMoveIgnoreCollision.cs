using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000978 RID: 2424
	public class CharacterMoveIgnoreCollision : CharacterMoveBase
	{
		// Token: 0x06003F97 RID: 16279 RVA: 0x0014A854 File Offset: 0x00148A54
		public override void Enter(CharacterMovement context)
		{
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x0014A858 File Offset: 0x00148A58
		public override void Leave(CharacterMovement context)
		{
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x0014A85C File Offset: 0x00148A5C
		public override void Step(CharacterMovement context, float deltaTime)
		{
			Vector3 position = context.transform.position + context.Velocity * deltaTime;
			context.transform.position = position;
		}
	}
}
