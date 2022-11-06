using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000987 RID: 2439
	public class CharacterBarrier : MonoBehaviour
	{
		// Token: 0x06004008 RID: 16392 RVA: 0x0014CB98 File Offset: 0x0014AD98
		private void Start()
		{
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0014CB9C File Offset: 0x0014AD9C
		private void Update()
		{
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x0014CBA0 File Offset: 0x0014ADA0
		public void SetEnable()
		{
			base.gameObject.SetActive(true);
			this.m_effect = StateUtil.CreateEffect(this, (!this.m_bigSize) ? "ef_pl_barrier_lv1_s01" : "ef_pl_barrier_lv1_l01", false);
			if (this.m_effect != null)
			{
				StateUtil.SetObjectLocalPositionToCenter(this, this.m_effect);
			}
			SoundManager.SePlay("obj_item_barrier", "SE");
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0014CC10 File Offset: 0x0014AE10
		public void SetDisable()
		{
			if (this.m_effect != null)
			{
				UnityEngine.Object.Destroy(this.m_effect);
				this.m_effect = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x0014CC44 File Offset: 0x0014AE44
		private void Stop()
		{
			this.SetDisable();
			SoundManager.SePlay("obj_item_barrier_brk", "SE");
			GameObject gameObject = base.gameObject.transform.parent.gameObject;
			StateUtil.CreateEffect(this, gameObject, "ef_pl_barrier_cancel_s01", true, ResourceCategory.COMMON_EFFECT);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0014CC8C File Offset: 0x0014AE8C
		public void SetNotDraw(bool value)
		{
			if (this.m_effect != null)
			{
				this.m_effect.SetActive(!value);
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x0014CCBC File Offset: 0x0014AEBC
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x0014CCC4 File Offset: 0x0014AEC4
		public bool IsBigSize
		{
			get
			{
				return this.m_bigSize;
			}
			set
			{
				this.m_bigSize = value;
			}
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0014CCD0 File Offset: 0x0014AED0
		public void Damaged()
		{
			this.Stop();
		}

		// Token: 0x040036E9 RID: 14057
		private GameObject m_effect;

		// Token: 0x040036EA RID: 14058
		private bool m_bigSize;
	}
}
