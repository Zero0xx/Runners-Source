using System;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098B RID: 2443
	public class CharacterLoopEffect : MonoBehaviour
	{
		// Token: 0x0600402C RID: 16428 RVA: 0x0014D170 File Offset: 0x0014B370
		private void Start()
		{
			this.m_effect = StateUtil.CreateEffect(this, this.m_effectname, false, this.m_category);
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x0014D18C File Offset: 0x0014B38C
		private void OnEnable()
		{
			if (this.m_sename != null)
			{
				this.m_seID = SoundManager.SePlay(this.m_sename, "SE");
			}
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x0014D1B0 File Offset: 0x0014B3B0
		private void OnDisable()
		{
			if (this.m_sename != null)
			{
				SoundManager.SeStop(this.m_seID);
				this.m_seID = SoundManager.PlayId.NONE;
			}
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x0014D1D0 File Offset: 0x0014B3D0
		private void Update()
		{
			if (!this.m_valid)
			{
				if (this.m_stopTimer > 0f)
				{
					this.m_stopTimer -= Time.deltaTime;
				}
				else
				{
					base.gameObject.SetActive(false);
					this.m_stopTimer = 0f;
				}
			}
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x0014D228 File Offset: 0x0014B428
		public void SetValid(bool valid)
		{
			if (valid && !this.m_valid)
			{
				if (!base.gameObject.activeSelf)
				{
					base.gameObject.SetActive(true);
				}
				StateUtil.PlayParticle(this.m_effect);
			}
			else if (!valid && this.m_valid)
			{
				base.gameObject.SetActive(false);
			}
			this.m_valid = valid;
			this.m_stopTimer = 0f;
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x0014D2A4 File Offset: 0x0014B4A4
		public void Stop(float stopTime)
		{
			if (this.m_effect != null && stopTime > 0f)
			{
				StateUtil.StopParticle(this.m_effect);
				this.m_stopTimer = stopTime;
			}
			this.m_valid = false;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x0014D2DC File Offset: 0x0014B4DC
		public void Setup(string effectname, ResourceCategory category)
		{
			this.m_effectname = effectname;
			this.m_category = category;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0014D2EC File Offset: 0x0014B4EC
		public void SetSE(string sename)
		{
			this.m_sename = sename;
		}

		// Token: 0x040036F6 RID: 14070
		private string m_effectname;

		// Token: 0x040036F7 RID: 14071
		private string m_sename;

		// Token: 0x040036F8 RID: 14072
		private SoundManager.PlayId m_seID;

		// Token: 0x040036F9 RID: 14073
		private float m_stopTimer;

		// Token: 0x040036FA RID: 14074
		private GameObject m_effect;

		// Token: 0x040036FB RID: 14075
		private bool m_valid;

		// Token: 0x040036FC RID: 14076
		private ResourceCategory m_category = ResourceCategory.COMMON_EFFECT;
	}
}
