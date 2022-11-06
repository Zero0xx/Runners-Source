using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000143 RID: 323
	public class ChaoHovering : ChaoHoveringBase
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0003A8DC File Offset: 0x00038ADC
		protected override void SetupImpl(ChaoHoveringBase.CInfoBase info)
		{
			ChaoHovering.CInfo cinfo = info as ChaoHovering.CInfo;
			this.m_hovering_height = cinfo.height;
			this.m_hovering_speed = cinfo.speed * 0.017453292f;
			this.m_angle = cinfo.startAngle;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0003A91C File Offset: 0x00038B1C
		public override void Reset()
		{
			this.m_angle = 0f;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003A92C File Offset: 0x00038B2C
		private void Start()
		{
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003A930 File Offset: 0x00038B30
		private void Update()
		{
			float deltaTime = Time.deltaTime;
			this.m_angle += this.m_hovering_speed * deltaTime;
			if (this.m_angle > 6.2831855f)
			{
				this.m_angle -= 6.2831855f;
			}
			this.m_hovering_pos.y = this.m_hovering_height * Mathf.Sin(this.m_angle);
			base.Position = this.m_hovering_pos;
		}

		// Token: 0x04000752 RID: 1874
		public const float PI_2 = 6.2831855f;

		// Token: 0x04000753 RID: 1875
		private float m_angle;

		// Token: 0x04000754 RID: 1876
		[SerializeField]
		private float m_hovering_speed = 3.1415927f;

		// Token: 0x04000755 RID: 1877
		[SerializeField]
		private float m_hovering_height = 0.3f;

		// Token: 0x04000756 RID: 1878
		private Vector3 m_hovering_pos = Vector3.zero;

		// Token: 0x02000144 RID: 324
		public class CInfo : ChaoHoveringBase.CInfoBase
		{
			// Token: 0x060009AF RID: 2479 RVA: 0x0003A9A4 File Offset: 0x00038BA4
			public CInfo(ChaoMovement movement) : base(movement)
			{
			}

			// Token: 0x04000757 RID: 1879
			public float height;

			// Token: 0x04000758 RID: 1880
			public float speed;

			// Token: 0x04000759 RID: 1881
			public float startAngle;
		}
	}
}
