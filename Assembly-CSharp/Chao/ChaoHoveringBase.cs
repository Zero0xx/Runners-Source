using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000158 RID: 344
	public class ChaoHoveringBase : MonoBehaviour
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0003C864 File Offset: 0x0003AA64
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0003C86C File Offset: 0x0003AA6C
		public Vector3 Position
		{
			get
			{
				return this.m_position;
			}
			protected set
			{
				this.m_position = value;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003C878 File Offset: 0x0003AA78
		public void Setup(ChaoHoveringBase.CInfoBase cinfo)
		{
			this.m_movement = cinfo.movement;
			this.SetupImpl(cinfo);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0003C890 File Offset: 0x0003AA90
		protected virtual void SetupImpl(ChaoHoveringBase.CInfoBase info)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0003C894 File Offset: 0x0003AA94
		public virtual void Reset()
		{
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0003C898 File Offset: 0x0003AA98
		public ChaoMovement Movement
		{
			get
			{
				return this.m_movement;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private ChaoMovement m_movement;

		// Token: 0x040007B1 RID: 1969
		private Vector3 m_position;

		// Token: 0x02000159 RID: 345
		public class CInfoBase
		{
			// Token: 0x06000A09 RID: 2569 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
			protected CInfoBase(ChaoMovement movement_)
			{
				this.movement = movement_;
			}

			// Token: 0x040007B2 RID: 1970
			public ChaoMovement movement;
		}
	}
}
