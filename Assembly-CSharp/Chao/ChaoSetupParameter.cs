using System;
using UnityEngine;

namespace Chao
{
	// Token: 0x02000141 RID: 321
	public class ChaoSetupParameter : MonoBehaviour
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0003A7A0 File Offset: 0x000389A0
		public ChaoSetupParameterData Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003A7A8 File Offset: 0x000389A8
		private void Awake()
		{
			base.enabled = false;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003A7B4 File Offset: 0x000389B4
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Vector3 localPosition = base.transform.localPosition;
			base.transform.localPosition = this.m_data.ColliCenter;
			Gizmos.DrawWireSphere(base.transform.position, this.m_data.ColliRadius);
			base.transform.localPosition = localPosition;
		}

		// Token: 0x04000751 RID: 1873
		[SerializeField]
		private ChaoSetupParameterData m_data = new ChaoSetupParameterData();
	}
}
