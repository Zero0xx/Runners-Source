using System;

namespace Player
{
	// Token: 0x020009B9 RID: 2489
	public class RunLoopPathParameter : StateEnteringParameter
	{
		// Token: 0x0600410F RID: 16655 RVA: 0x00152688 File Offset: 0x00150888
		public override void Reset()
		{
			this.m_pathComponent = null;
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x00152694 File Offset: 0x00150894
		public void Set(PathComponent component)
		{
			this.m_pathComponent = component;
		}

		// Token: 0x040037BE RID: 14270
		public PathComponent m_pathComponent;
	}
}
