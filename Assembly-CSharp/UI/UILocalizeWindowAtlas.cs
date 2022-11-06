using System;
using UnityEngine;

namespace UI
{
	// Token: 0x02000571 RID: 1393
	[AddComponentMenu("Scripts/UI/UILocalizeWindowAtlas")]
	public class UILocalizeWindowAtlas : MonoBehaviour
	{
		// Token: 0x06002ACE RID: 10958 RVA: 0x00108A88 File Offset: 0x00106C88
		private void Start()
		{
			base.enabled = false;
		}

		// Token: 0x0400263D RID: 9789
		[SerializeField]
		public string m_atlasName = "ui_mm_contents_word_Atlas_ja";
	}
}
