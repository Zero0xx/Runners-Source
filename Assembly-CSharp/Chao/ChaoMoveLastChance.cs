using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200014F RID: 335
	public class ChaoMoveLastChance : ChaoMoveBase
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x0003BA28 File Offset: 0x00039C28
		public override void Enter(ChaoMovement context)
		{
			this.m_distancePos = context.TargetPosition - context.Position;
			this.m_velocity = context.TargetAccessSpeed;
			this.m_prePositon = context.Position;
			this.m_stayPosition = context.Position - context.Hovering;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003BA7C File Offset: 0x00039C7C
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0003BA80 File Offset: 0x00039C80
		public override void Step(ChaoMovement context, float deltaTime)
		{
			Camera main = Camera.main;
			if (deltaTime != 0f && main != null)
			{
				float movingDistance = this.m_velocity * deltaTime;
				if ((double)main.WorldToViewportPoint(context.TargetPosition).y < -0.05)
				{
					this.m_distancePos.x = this.CalcAccessDistance(movingDistance, this.m_distancePos.x);
					this.m_stayPosition.x = context.TargetPosition.x - this.m_distancePos.x;
					context.Position = this.m_stayPosition + context.Hovering;
				}
				else
				{
					this.m_distancePos.x = this.CalcAccessDistance(movingDistance, this.m_distancePos.x);
					this.m_distancePos.y = this.CalcAccessDistance(movingDistance, this.m_distancePos.y);
					context.Position = context.TargetPosition - this.m_distancePos;
					if (this.m_distancePos.y < 0f)
					{
						float num = context.Position.y - this.m_prePositon.y;
						if (num > 0f)
						{
							this.m_distancePos.y = this.m_distancePos.y + num;
							if (this.m_distancePos.y > 0f)
							{
								this.m_distancePos.y = 0f;
							}
							context.Position = context.TargetPosition - this.m_distancePos;
						}
					}
					else if (this.m_distancePos.y > 0f)
					{
						float num2 = context.Position.y - this.m_prePositon.y;
						if (num2 < 0f)
						{
							this.m_distancePos.y = this.m_distancePos.y - num2;
							if (this.m_distancePos.y < 0f)
							{
								this.m_distancePos.y = 0f;
							}
							context.Position = context.TargetPosition - this.m_distancePos;
						}
					}
				}
				this.m_prePositon = context.Position;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0003BCB0 File Offset: 0x00039EB0
		private float CalcAccessDistance(float movingDistance, float targetDistance)
		{
			if (targetDistance != 0f)
			{
				if (targetDistance > 0f)
				{
					if (targetDistance > movingDistance)
					{
						targetDistance -= movingDistance;
					}
					else
					{
						targetDistance = 0f;
					}
				}
				else if (targetDistance < -movingDistance)
				{
					targetDistance += movingDistance;
				}
				else
				{
					targetDistance = 0f;
				}
			}
			return targetDistance;
		}

		// Token: 0x0400078A RID: 1930
		private float m_velocity = 5f;

		// Token: 0x0400078B RID: 1931
		private Vector3 m_distancePos = new Vector3(0f, 0f, 0f);

		// Token: 0x0400078C RID: 1932
		private Vector3 m_prePositon = new Vector3(0f, 0f, 0f);

		// Token: 0x0400078D RID: 1933
		private Vector3 m_stayPosition = new Vector3(0f, 0f, 0f);
	}
}
