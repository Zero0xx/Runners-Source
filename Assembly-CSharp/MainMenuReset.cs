using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class MainMenuReset : MonoBehaviour
{
	// Token: 0x06001482 RID: 5250 RVA: 0x0006DAA0 File Offset: 0x0006BCA0
	private void Start()
	{
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
	private void Update()
	{
		if (!this.m_isLoadedScene)
		{
			Application.LoadLevel("MainMenu");
		}
	}

	// Token: 0x040011E1 RID: 4577
	private bool m_isLoadedScene;
}
