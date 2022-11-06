using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class ChaoSetupParameterData
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0003A618 File Offset: 0x00038818
		public Vector3 MainOffset
		{
			get
			{
				return this.m_mainOffset;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0003A620 File Offset: 0x00038820
		public Vector3 SubOffset
		{
			get
			{
				return this.m_subOffset;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0003A628 File Offset: 0x00038828
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0003A630 File Offset: 0x00038830
		public float ColliRadius
		{
			get
			{
				return this.m_colliRadius;
			}
			private set
			{
				this.m_colliRadius = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0003A63C File Offset: 0x0003883C
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0003A644 File Offset: 0x00038844
		public Vector3 ColliCenter
		{
			get
			{
				return this.m_colliCenter;
			}
			private set
			{
				this.m_colliCenter = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0003A650 File Offset: 0x00038850
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0003A658 File Offset: 0x00038858
		public ChaoMovementType MoveType
		{
			get
			{
				return this.m_movementType;
			}
			private set
			{
				this.m_movementType = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0003A664 File Offset: 0x00038864
		public ChaoHoverType HoverType
		{
			get
			{
				return this.m_hoverType;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0003A66C File Offset: 0x0003886C
		public bool UseCustomHoverParam
		{
			get
			{
				return this.m_useCustomHoverParam;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0003A674 File Offset: 0x00038874
		public float HoverSpeed
		{
			get
			{
				if (this.m_useCustomHoverParam)
				{
					return this.m_hoverSpeedDegree;
				}
				return 180f;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0003A690 File Offset: 0x00038890
		public float HoverHeight
		{
			get
			{
				if (this.m_useCustomHoverParam)
				{
					return this.m_hoverHeight;
				}
				return 0.2f;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0003A6AC File Offset: 0x000388AC
		public float HoverStartDegreeMain
		{
			get
			{
				if (this.m_useCustomHoverParam)
				{
					return this.m_hoverStartDegreeMain;
				}
				return 0f;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0003A6C8 File Offset: 0x000388C8
		public float HoverStartDegreeSub
		{
			get
			{
				if (this.m_useCustomHoverParam)
				{
					return this.m_hoverStartDegreeSub;
				}
				return 160f;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0003A6E4 File Offset: 0x000388E4
		public ShaderType ShaderOffset
		{
			get
			{
				return this.m_shaderType;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0003A6EC File Offset: 0x000388EC
		public void CopyTo(ChaoSetupParameterData dst)
		{
			dst.ColliRadius = this.ColliRadius;
			dst.ColliCenter = this.ColliCenter;
			dst.MoveType = this.MoveType;
			dst.m_mainOffset = this.m_mainOffset;
			dst.m_subOffset = this.m_subOffset;
			dst.m_hoverType = this.m_hoverType;
			dst.m_useCustomHoverParam = this.m_useCustomHoverParam;
			dst.m_hoverSpeedDegree = this.m_hoverSpeedDegree;
			dst.m_hoverHeight = this.m_hoverHeight;
			dst.m_hoverStartDegreeMain = this.m_hoverStartDegreeMain;
			dst.m_hoverStartDegreeSub = this.m_hoverStartDegreeSub;
			dst.m_shaderType = this.m_shaderType;
		}

		// Token: 0x04000741 RID: 1857
		private const float DefaultHoverSpeedDegree = 180f;

		// Token: 0x04000742 RID: 1858
		private const float DefaultHoverHeight = 0.2f;

		// Token: 0x04000743 RID: 1859
		private const float DefaultHoverStartDegreeMain = 0f;

		// Token: 0x04000744 RID: 1860
		private const float DefaultHoverStartDegreeSub = 160f;

		// Token: 0x04000745 RID: 1861
		[SerializeField]
		private Vector3 m_mainOffset = new Vector3(-1.1f, 1.4f, 0f);

		// Token: 0x04000746 RID: 1862
		[SerializeField]
		private Vector3 m_subOffset = new Vector3(-1.9f, 1f, 0f);

		// Token: 0x04000747 RID: 1863
		[SerializeField]
		private float m_colliRadius = 0.3f;

		// Token: 0x04000748 RID: 1864
		[SerializeField]
		private Vector3 m_colliCenter = new Vector3(0f, 0.13f, -0.05f);

		// Token: 0x04000749 RID: 1865
		[SerializeField]
		private ChaoMovementType m_movementType;

		// Token: 0x0400074A RID: 1866
		[SerializeField]
		private ChaoHoverType m_hoverType = ChaoHoverType.CHAO;

		// Token: 0x0400074B RID: 1867
		[SerializeField]
		private bool m_useCustomHoverParam;

		// Token: 0x0400074C RID: 1868
		[SerializeField]
		private float m_hoverSpeedDegree = 180f;

		// Token: 0x0400074D RID: 1869
		[SerializeField]
		private float m_hoverHeight = 0.2f;

		// Token: 0x0400074E RID: 1870
		[SerializeField]
		private float m_hoverStartDegreeMain;

		// Token: 0x0400074F RID: 1871
		[SerializeField]
		private float m_hoverStartDegreeSub = 160f;

		// Token: 0x04000750 RID: 1872
		[SerializeField]
		private ShaderType m_shaderType;
	}
}
