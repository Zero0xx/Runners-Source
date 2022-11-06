using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200096E RID: 2414
	[Serializable]
	public class CharacterParameter : MonoBehaviour
	{
		// Token: 0x06003F0D RID: 16141 RVA: 0x00147D98 File Offset: 0x00145F98
		private void Start()
		{
			base.enabled = false;
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x00147DA4 File Offset: 0x00145FA4
		public CharacterParameterData GetData()
		{
			return this.m_data;
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x00147DAC File Offset: 0x00145FAC
		public void CopyData(CharacterParameterData data)
		{
			this.m_data = data;
		}

		// Token: 0x04003636 RID: 13878
		public CharacterParameterData m_data;
	}
}
