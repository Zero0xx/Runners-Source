using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098F RID: 2447
	public class CharacterPhantomCollision : MonoBehaviour
	{
		// Token: 0x06004051 RID: 16465 RVA: 0x0014D9C0 File Offset: 0x0014BBC0
		private void Start()
		{
			this.m_parent = base.transform.parent.gameObject;
			if (this.m_parent != null)
			{
				this.m_state = this.m_parent.GetComponent<CharacterState>();
			}
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0014DA08 File Offset: 0x0014BC08
		private void OnAddRings(int numRing)
		{
			if (this.m_state != null)
			{
				this.m_state.OnAddRings(numRing);
			}
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0014DA28 File Offset: 0x0014BC28
		private void OnFallingDead()
		{
			if (this.m_state != null)
			{
				this.m_state.OnFallingDead();
			}
		}

		// Token: 0x04003710 RID: 14096
		private GameObject m_parent;

		// Token: 0x04003711 RID: 14097
		private CharacterState m_state;
	}
}
