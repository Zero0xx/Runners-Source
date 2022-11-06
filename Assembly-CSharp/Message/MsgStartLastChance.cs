using System;
using UnityEngine;

namespace Message
{
	// Token: 0x020005BB RID: 1467
	public class MsgStartLastChance : MessageBase
	{
		// Token: 0x06002B29 RID: 11049 RVA: 0x00109DB8 File Offset: 0x00107FB8
		public MsgStartLastChance(GameObject parentObject) : base(24587)
		{
			this.m_parentObject = parentObject;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x00109DCC File Offset: 0x00107FCC
		public GameObject ParentObject
		{
			get
			{
				return this.m_parentObject;
			}
		}

		// Token: 0x040027B3 RID: 10163
		private GameObject m_parentObject;
	}
}
