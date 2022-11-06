using System;
using UnityEngine;

// Token: 0x02000573 RID: 1395
[AddComponentMenu("NGUI/UI/RootSizeControl")]
public class UIRootSizeControl : MonoBehaviour
{
	// Token: 0x06002AD9 RID: 10969 RVA: 0x00108C50 File Offset: 0x00106E50
	private void Start()
	{
		base.enabled = false;
		float num = (float)Screen.width / (float)Screen.height;
		if (num < 1.335f && Screen.height < 768)
		{
			UIRoot component = base.gameObject.GetComponent<UIRoot>();
			if (component != null)
			{
				component.minimumHeight = 768;
			}
		}
	}

	// Token: 0x04002641 RID: 9793
	private const float ASPECT_VALUE = 1.335f;

	// Token: 0x04002642 RID: 9794
	private const int MIMUN_HEIGHT = 768;
}
