using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000149 RID: 329
	public class ChaoMoveGoCameraTarget : ChaoMoveBase
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x0003AFE0 File Offset: 0x000391E0
		public override void Enter(ChaoMovement context)
		{
			this.m_posZ = context.Position.z;
			this.m_cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
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
				}
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0003B078 File Offset: 0x00039278
		public override void Leave(ChaoMovement context)
		{
			this.m_cameraObject = null;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0003B084 File Offset: 0x00039284
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
			this.UpdateTargetPosition(context);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0003B0AC File Offset: 0x000392AC
		public void SetParameter(Vector3 screenOffsetRate, float speedRate)
		{
			this.m_screenOffsetRate = screenOffsetRate;
			this.m_speedRate = speedRate;
			if (this.m_cameraObject != null)
			{
				Camera component = this.m_cameraObject.GetComponent<Camera>();
				if (component != null)
				{
					this.m_targetScreenPos.x = component.pixelWidth * this.m_screenOffsetRate.x;
					this.m_targetScreenPos.y = component.pixelHeight * this.m_screenOffsetRate.y;
					this.m_targetScreenPos.z = this.m_currentScreenPos.z;
				}
			}
			this.m_distance = Vector3.Distance(this.m_targetScreenPos, this.m_currentScreenPos);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0003B158 File Offset: 0x00039358
		private void UpdateTargetPosition(ChaoMovement context)
		{
			this.m_currentScreenPos = Vector3.MoveTowards(this.m_currentScreenPos, this.m_targetScreenPos, this.m_distance * this.m_speedRate * Time.deltaTime);
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

		// Token: 0x04000769 RID: 1897
		private Vector3 m_screenOffsetRate = Vector3.zero;

		// Token: 0x0400076A RID: 1898
		private Vector3 m_targetScreenPos = Vector3.zero;

		// Token: 0x0400076B RID: 1899
		private Vector3 m_currentScreenPos = Vector3.zero;

		// Token: 0x0400076C RID: 1900
		private GameObject m_cameraObject;

		// Token: 0x0400076D RID: 1901
		private float m_speedRate;

		// Token: 0x0400076E RID: 1902
		private float m_distance;

		// Token: 0x0400076F RID: 1903
		private float m_speed;

		// Token: 0x04000770 RID: 1904
		private float m_posZ;
	}
}
