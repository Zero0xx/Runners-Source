using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000989 RID: 2441
	public class CharacterCombo : MonoBehaviour
	{
		// Token: 0x06004017 RID: 16407 RVA: 0x0014CD38 File Offset: 0x0014AF38
		private void Start()
		{
			this.m_movement = base.transform.parent.gameObject.GetComponent<CharacterMovement>();
			this.m_information = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x0014CD70 File Offset: 0x0014AF70
		private void OnEnable()
		{
			this.SetCombo(true);
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x0014CD7C File Offset: 0x0014AF7C
		private void OnDisable()
		{
			this.SetCombo(false);
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x0014CD88 File Offset: 0x0014AF88
		public void SetEnable()
		{
			base.gameObject.SetActive(true);
			this.m_time = -1f;
			this.m_effect = StateUtil.CreateEffect(this, "ef_pl_combobonus01", false);
			if (this.m_effect != null)
			{
				StateUtil.SetObjectLocalPositionToCenter(this, this.m_effect);
			}
			if (StageAbilityManager.Instance != null)
			{
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.COMBO_TIME);
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.ITEM_TIME);
			}
			SoundManager.SePlay("obj_combo_loop", "SE");
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x0014CE14 File Offset: 0x0014B014
		public void SetDisable()
		{
			StateUtil.SendMessageToTerminateItem(ItemType.COMBO);
			if (this.m_effect != null)
			{
				StateUtil.DestroyParticle(this.m_effect, 1f);
				this.m_effect = null;
			}
			base.gameObject.SetActive(false);
			this.m_requestEnd = false;
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x0014CE64 File Offset: 0x0014B064
		private void Update()
		{
			if (this.m_movement != null && this.m_movement.IsOnGround() && this.m_requestEnd)
			{
				this.SetDisable();
			}
			if (this.m_time > 0f)
			{
				this.m_time -= Time.deltaTime;
				if (this.m_time <= 0f)
				{
					this.RequestEnd();
				}
			}
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x0014CEDC File Offset: 0x0014B0DC
		public void SetTime(float time)
		{
			this.m_time = time;
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x0014CEE8 File Offset: 0x0014B0E8
		public void RequestEnd()
		{
			this.m_requestEnd = true;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0014CEF4 File Offset: 0x0014B0F4
		private void SetCombo(bool flag)
		{
			if (this.m_information == null)
			{
				this.m_information = GameObjectUtil.FindGameObjectComponent<PlayerInformation>("PlayerInformation");
			}
			if (this.m_information != null)
			{
				this.m_information.SetCombo(flag);
			}
		}

		// Token: 0x040036EC RID: 14060
		private CharacterMovement m_movement;

		// Token: 0x040036ED RID: 14061
		private GameObject m_effect;

		// Token: 0x040036EE RID: 14062
		private PlayerInformation m_information;

		// Token: 0x040036EF RID: 14063
		private float m_time;

		// Token: 0x040036F0 RID: 14064
		private bool m_requestEnd;
	}
}
