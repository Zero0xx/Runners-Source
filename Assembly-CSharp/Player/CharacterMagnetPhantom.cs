using System;
using App;
using Message;
using UnityEngine;

namespace Player
{
	// Token: 0x0200098D RID: 2445
	public class CharacterMagnetPhantom : MonoBehaviour
	{
		// Token: 0x06004042 RID: 16450 RVA: 0x0014D5B0 File Offset: 0x0014B7B0
		private void Awake()
		{
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component)
			{
				this.m_defaultOffset = component.center;
				this.m_defaultRadius = component.radius;
			}
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x0014D5E8 File Offset: 0x0014B7E8
		private void Start()
		{
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x0014D5EC File Offset: 0x0014B7EC
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
					StageAbilityManager.Instance.RequestPlayChaoEffect(ChaoAbility.MAGNET_RANGE);
				}
			}
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0014D6AC File Offset: 0x0014B8AC
		public void SetDefaultRadiusAndOffset()
		{
			this.SetRadiusAndOffset(this.m_defaultRadius, this.m_defaultOffset);
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0014D6C0 File Offset: 0x0014B8C0
		public void SetOffDrawing(bool value)
		{
			this.m_offDrawing = value;
			SphereCollider component = base.GetComponent<SphereCollider>();
			if (component)
			{
				component.enabled = !this.m_offDrawing;
			}
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x0014D6F8 File Offset: 0x0014B8F8
		private void OnTriggerEnter(Collider other)
		{
			if (!this.m_offDrawing)
			{
				MsgOnDrawingRings value = new MsgOnDrawingRings();
				GameObjectUtil.SendDelayedMessageToGameObject(other.gameObject, "OnDrawingRings", value);
			}
		}

		// Token: 0x04003705 RID: 14085
		private Vector3 m_defaultOffset;

		// Token: 0x04003706 RID: 14086
		private float m_defaultRadius;

		// Token: 0x04003707 RID: 14087
		private bool m_offDrawing;
	}
}
