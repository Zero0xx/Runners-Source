using System;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098A RID: 2442
	public class CharacterInvincible : MonoBehaviour
	{
		// Token: 0x06004021 RID: 16417 RVA: 0x0014CF48 File Offset: 0x0014B148
		private void Start()
		{
			this.m_effect = StateUtil.CreateEffect(this, (!this.m_bigSize) ? "ef_pl_invincible_s01" : "ef_pl_invincible_l01", false);
			if (base.transform.parent)
			{
				CapsuleCollider component = base.transform.parent.GetComponent<CapsuleCollider>();
				if (component)
				{
					base.transform.localPosition = component.center;
				}
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x0014CFC0 File Offset: 0x0014B1C0
		private void OnDestroy()
		{
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0014CFC4 File Offset: 0x0014B1C4
		private void Update()
		{
			if (this.m_time > 0f)
			{
				this.m_time -= Time.deltaTime;
				if (this.m_time <= 0f)
				{
					this.SetDisable();
				}
			}
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x0014D00C File Offset: 0x0014B20C
		public void SetEnable()
		{
			this.m_time = -1f;
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x0014D01C File Offset: 0x0014B21C
		public void SetActive(float time)
		{
			base.gameObject.SetActive(true);
			if (this.m_effect != null && !this.m_effect.activeInHierarchy)
			{
				this.m_effect.SetActive(true);
			}
			this.SetTime(time);
			MsgInvincible value = new MsgInvincible(MsgInvincible.Mode.Start);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgInvincible", value, SendMessageOptions.DontRequireReceiver);
			if (StageAbilityManager.Instance != null)
			{
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.ITEM_TIME);
			}
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0014D0A0 File Offset: 0x0014B2A0
		public void SetDisable()
		{
			StateUtil.SendMessageToTerminateItem(ItemType.INVINCIBLE);
			base.gameObject.SetActive(false);
			GameObject gameObject = base.gameObject.transform.parent.gameObject;
			StateUtil.CreateEffect(this, gameObject, "ef_pl_invincible_cancel_s01", true, ResourceCategory.COMMON_EFFECT);
			MsgInvincible value = new MsgInvincible(MsgInvincible.Mode.End);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgInvincible", value, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0014D100 File Offset: 0x0014B300
		public void SetNotDraw(bool value)
		{
			if (this.m_effect != null)
			{
				bool flag = !value;
				if (this.m_effect.activeInHierarchy != flag)
				{
					this.m_effect.SetActive(flag);
				}
			}
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x0014D140 File Offset: 0x0014B340
		public void SetTime(float time)
		{
			this.m_time = time;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06004029 RID: 16425 RVA: 0x0014D14C File Offset: 0x0014B34C
		// (set) Token: 0x0600402A RID: 16426 RVA: 0x0014D154 File Offset: 0x0014B354
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

		// Token: 0x040036F1 RID: 14065
		private const string EffectNameS = "ef_pl_invincible_s01";

		// Token: 0x040036F2 RID: 14066
		private const string EffectNameL = "ef_pl_invincible_l01";

		// Token: 0x040036F3 RID: 14067
		private GameObject m_effect;

		// Token: 0x040036F4 RID: 14068
		private float m_time;

		// Token: 0x040036F5 RID: 14069
		private bool m_bigSize;
	}
}
