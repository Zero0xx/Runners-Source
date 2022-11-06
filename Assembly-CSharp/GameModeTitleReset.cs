using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class GameModeTitleReset : MonoBehaviour
{
	// Token: 0x0600185F RID: 6239 RVA: 0x0008CE3C File Offset: 0x0008B03C
	private void Start()
	{
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x0008CE40 File Offset: 0x0008B040
	private void Update()
	{
		if (!this.m_isLoadedLevel)
		{
			Application.LoadLevel(TitleDefine.TitleSceneName);
			this.m_isLoadedLevel = true;
		}
	}

	// Token: 0x040015EE RID: 5614
	private bool m_isLoadedLevel;
}
