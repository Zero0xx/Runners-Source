using System;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class PnoteRegistration : MonoBehaviour
{
	// Token: 0x060038D4 RID: 14548 RVA: 0x0012D790 File Offset: 0x0012B990
	private void Start()
	{
		this.m_enable = false;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060038D5 RID: 14549 RVA: 0x0012D7A4 File Offset: 0x0012B9A4
	private void Update()
	{
		if (this.m_enable)
		{
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002F90 RID: 12176
	private bool m_enable = true;
}
