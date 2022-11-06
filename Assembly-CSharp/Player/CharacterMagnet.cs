using System;
using App;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098C RID: 2444
	public class CharacterMagnet : MonoBehaviour
	{
		// Token: 0x06004035 RID: 16437 RVA: 0x0014D300 File Offset: 0x0014B500
		private void Awake()
		{
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component)
			{
				this.m_defaultOffset = component.center;
				this.m_defaultRadius = component.radius;
			}
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0014D338 File Offset: 0x0014B538
		private void Start()
		{
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0014D33C File Offset: 0x0014B53C
		public void SetEnable()
		{
			base.gameObject.SetActive(true);
			this.m_time = -1f;
			if (this.m_forChaoAbility)
			{
				return;
			}
			this.m_effect = StateUtil.CreateEffect(this, (!this.m_bigSize) ? "ef_pl_magnet_s01" : "ef_pl_magnet_l01", false);
			if (this.m_effect != null)
			{
				StateUtil.SetObjectLocalPositionToCenter(this, this.m_effect);
			}
			if (StageAbilityManager.Instance != null)
			{
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.MAGNET_TIME);
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.ITEM_TIME);
				StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.MAGNET_RANGE);
			}
			SoundManager.SePlay("obj_magnet", "SE");
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0014D3F4 File Offset: 0x0014B5F4
		public void SetDisable()
		{
			if (!this.m_forChaoAbility)
			{
				StateUtil.SendMessageToTerminateItem(ItemType.MAGNET);
			}
			if (this.m_effect != null)
			{
				StateUtil.DestroyParticle(this.m_effect, 1f);
				this.m_effect = null;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x0014D448 File Offset: 0x0014B648
		// (set) Token: 0x0600403A RID: 16442 RVA: 0x0014D450 File Offset: 0x0014B650
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

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0014D45C File Offset: 0x0014B65C
		public bool ForChaoAbility
		{
			get
			{
				return this.m_forChaoAbility;
			}
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x0014D464 File Offset: 0x0014B664
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

		// Token: 0x0600403D RID: 16445 RVA: 0x0014D4AC File Offset: 0x0014B6AC
		private void SetRadiusAndOffset(float radius, Vector3 offset)
		{
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component)
			{
				if (!App.Math.NearEqual(radius, component.radius, 1E-06f))
				{
					component.radius = radius;
				}
				if (!App.Math.NearZero((offset - component.center).sqrMagnitude, 1E-06f))
				{
					component.center = offset;
				}
				this.m_defaultOffset = component.center;
				this.m_defaultRadius = component.radius;
				if (StageAbilityManager.Instance != null)
				{
					float num = StageAbilityManager.Instance.GetChaoAbliltyValue(ChaoAbility.MAGNET_RANGE, 100f) / 100f;
					component.radius = this.m_defaultRadius * num;
				}
			}
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0014D560 File Offset: 0x0014B760
		public void SetDefaultRadiusAndOffset()
		{
			this.SetRadiusAndOffset(this.m_defaultRadius, this.m_defaultOffset);
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x0014D574 File Offset: 0x0014B774
		public void SetTime(float time)
		{
			this.m_time = time;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x0014D580 File Offset: 0x0014B780
		private void OnTriggerEnter(Collider other)
		{
			MsgOnDrawingRings value = new MsgOnDrawingRings();
			other.gameObject.SendMessage("OnDrawingRings", value, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x040036FD RID: 14077
		private const string EffectNameS = "ef_pl_magnet_s01";

		// Token: 0x040036FE RID: 14078
		private const string EffectNameL = "ef_pl_magnet_l01";

		// Token: 0x040036FF RID: 14079
		private GameObject m_effect;

		// Token: 0x04003700 RID: 14080
		private Vector3 m_defaultOffset;

		// Token: 0x04003701 RID: 14081
		private float m_defaultRadius;

		// Token: 0x04003702 RID: 14082
		private float m_time;

		// Token: 0x04003703 RID: 14083
		private bool m_bigSize;

		// Token: 0x04003704 RID: 14084
		[SerializeField]
		private bool m_forChaoAbility;
	}
}
