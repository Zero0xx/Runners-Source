using System;
using UnityEngine;

namespace Player
{
	// Token: 0x02000988 RID: 2440
	public class CharacterCheckTrickJump : MonoBehaviour
	{
		// Token: 0x06004012 RID: 16402 RVA: 0x0014CCE0 File Offset: 0x0014AEE0
		private void Update()
		{
			CharacterInput component = base.GetComponent<CharacterInput>();
			if (component != null && component.IsTouched())
			{
				this.m_touched = true;
			}
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x0014CD14 File Offset: 0x0014AF14
		public void Reset()
		{
			this.m_touched = false;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x0014CD20 File Offset: 0x0014AF20
		public bool IsTouched
		{
			get
			{
				return this.m_touched;
			}
		}

		// Token: 0x06004015 RID: 16405 RVA: 0x0014CD28 File Offset: 0x0014AF28
		private void OnEnable()
		{
			this.Reset();
		}

		// Token: 0x040036EB RID: 14059
		private bool m_touched;
	}
}
