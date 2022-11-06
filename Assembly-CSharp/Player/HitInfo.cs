using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000983 RID: 2435
	public struct HitInfo
	{
		// Token: 0x06003FFA RID: 16378 RVA: 0x0014C384 File Offset: 0x0014A584
		public void Reset()
		{
			this.valid = false;
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x0014C390 File Offset: 0x0014A590
		public void Set(RaycastHit hit)
		{
			this.valid = true;
			this.info = hit;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x0014C3A0 File Offset: 0x0014A5A0
		public bool IsValid()
		{
			return this.valid;
		}

		// Token: 0x040036E2 RID: 14050
		public bool valid;

		// Token: 0x040036E3 RID: 14051
		public RaycastHit info;
	}
}
