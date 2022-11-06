using System;
using UnityEngine;

namespace Player
{
	// Token: 0x020009C4 RID: 2500
	public class CharacterBlinkTimer : MonoBehaviour
	{
		// Token: 0x060041A3 RID: 16803 RVA: 0x001559CC File Offset: 0x00153BCC
		private void Start()
		{
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x001559D0 File Offset: 0x00153BD0
		private void OnDestroy()
		{
			this.End();
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x001559D8 File Offset: 0x00153BD8
		public void Setup(CharacterState ctx, float damageTime)
		{
			this.m_context = ctx;
			this.m_timer = damageTime;
			this.m_context.SetStatus(Status.Damaged, true);
			base.enabled = true;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x00155A08 File Offset: 0x00153C08
		public void End()
		{
			if (this.m_context)
			{
				this.m_context.SetVisibleBlink(false);
				this.m_context.SetStatus(Status.Damaged, false);
			}
			base.enabled = false;
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00155A48 File Offset: 0x00153C48
		private void FixedUpdate()
		{
			if (this.m_context == null)
			{
				return;
			}
			this.m_timer -= Time.deltaTime;
			if (this.m_timer <= 0f)
			{
				this.End();
				return;
			}
			if (Mathf.FloorToInt(this.m_timer * 100f) % 20 > 10)
			{
				this.m_context.SetVisibleBlink(true);
			}
			else
			{
				this.m_context.SetVisibleBlink(false);
			}
		}

		// Token: 0x040037FF RID: 14335
		private float m_timer;

		// Token: 0x04003800 RID: 14336
		private CharacterState m_context;
	}
}
