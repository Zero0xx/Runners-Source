using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x0200014E RID: 334
	public class ChaoMoveGoRingBanking : ChaoMoveBase
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x0003B7FC File Offset: 0x000399FC
		public override void Enter(ChaoMovement context)
		{
			this.m_cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
			context.Velocity = context.HorzVelocity;
			this.m_posZ = context.Position.z;
			if (this.m_cameraObject != null)
			{
				Camera component = this.m_cameraObject.GetComponent<Camera>();
				if (component != null)
				{
					Vector3 vector = component.WorldToScreenPoint(context.Position);
					if (vector.x < 0f)
					{
						vector.x = -0.5f;
						context.Position = component.ScreenToWorldPoint(vector);
					}
					this.m_currentScreenPos = vector;
					vector.y = component.pixelHeight;
					vector.x = component.pixelWidth * 0.85f;
					Vector3 vector2 = component.ScreenToWorldPoint(vector);
					vector2 += ChaoMovement.VertDir * 1.5f;
					this.m_targetScreenPos = component.WorldToScreenPoint(vector2);
					this.m_targetScreenPos.z = this.m_currentScreenPos.z;
					this.m_distance = Vector3.Distance(this.m_targetScreenPos, this.m_currentScreenPos);
				}
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0003B918 File Offset: 0x00039B18
		public override void Leave(ChaoMovement context)
		{
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003B91C File Offset: 0x00039B1C
		public override void Step(ChaoMovement context, float deltaTime)
		{
			if (context.PlayerInfo == null)
			{
				return;
			}
			if (!context.IsPlyayerMoved)
			{
				return;
			}
			this.m_currentScreenPos = Vector3.MoveTowards(this.m_currentScreenPos, this.m_targetScreenPos, this.m_distance * 0.6f * Time.deltaTime);
			if (this.m_cameraObject != null)
			{
				Camera component = this.m_cameraObject.GetComponent<Camera>();
				if (component != null)
				{
					Vector3 position = component.ScreenToWorldPoint(this.m_currentScreenPos);
					position.z = this.m_posZ;
					context.Position = position;
				}
			}
		}

		// Token: 0x04000782 RID: 1922
		private const float ScreenOffsetWidth = 0.85f;

		// Token: 0x04000783 RID: 1923
		private const float UpperOffsetFromHud = 1.5f;

		// Token: 0x04000784 RID: 1924
		private const float SpeedRate = 0.6f;

		// Token: 0x04000785 RID: 1925
		private GameObject m_cameraObject;

		// Token: 0x04000786 RID: 1926
		private float m_posZ;

		// Token: 0x04000787 RID: 1927
		private Vector3 m_targetScreenPos = Vector3.zero;

		// Token: 0x04000788 RID: 1928
		private Vector3 m_currentScreenPos = Vector3.zero;

		// Token: 0x04000789 RID: 1929
		private float m_distance;
	}
}
