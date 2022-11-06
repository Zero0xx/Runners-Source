using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000990 RID: 2448
	public class CharacterTrampoline : MonoBehaviour
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x0014DA50 File Offset: 0x0014BC50
		private void Start()
		{
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0014DA54 File Offset: 0x0014BC54
		private void OnEnable()
		{
			this.m_requestEnd = false;
			this.m_effect = StateUtil.CreateEffect(this, "ef_pl_trampoline_s01", false);
			StateUtil.SetObjectLocalPositionToCenter(this, this.m_effect);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0014DA7C File Offset: 0x0014BC7C
		private void OnDisable()
		{
			if (this.m_effect != null)
			{
				StateUtil.DestroyParticle(this.m_effect, 1f);
				this.m_effect = null;
			}
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0014DAB4 File Offset: 0x0014BCB4
		public void SetEnable()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0014DAC4 File Offset: 0x0014BCC4
		public void SetDisable()
		{
			StateUtil.SendMessageToTerminateItem(ItemType.TRAMPOLINE);
			if (this.m_effect != null)
			{
				StateUtil.DestroyParticle(this.m_effect, 1f);
				this.m_effect = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x0014DB0C File Offset: 0x0014BD0C
		private void Update()
		{
			if (this.m_requestEnd)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.m_time > 0f)
			{
				this.m_time -= Time.deltaTime;
				if (this.m_time <= 0f)
				{
					base.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x0014DB70 File Offset: 0x0014BD70
		public void SetTime(float time)
		{
			this.m_time = time;
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0014DB7C File Offset: 0x0014BD7C
		public void RequestEnd()
		{
			this.m_requestEnd = true;
		}

		// Token: 0x04003712 RID: 14098
		private bool m_requestEnd;

		// Token: 0x04003713 RID: 14099
		private GameObject m_effect;

		// Token: 0x04003714 RID: 14100
		private float m_time;
	}
}
