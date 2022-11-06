using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000224 RID: 548
public abstract class EventWindowBase : MonoBehaviour
{
	// Token: 0x06000E9A RID: 3738
	protected abstract void SetObject();

	// Token: 0x06000E9B RID: 3739 RVA: 0x00054D28 File Offset: 0x00052F28
	public void AnimationFinishi()
	{
		this.enabledAnchorObjects = false;
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00054D34 File Offset: 0x00052F34
	// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00054DB4 File Offset: 0x00052FB4
	public bool enabledAnchorObjects
	{
		get
		{
			bool result = false;
			if (this.anchorObjects != null)
			{
				foreach (GameObject gameObject in this.anchorObjects)
				{
					if (gameObject.activeSelf)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}
		set
		{
			if (this.anchorObjects != null)
			{
				foreach (GameObject gameObject in this.anchorObjects)
				{
					gameObject.SetActive(value);
				}
			}
		}
	}

	// Token: 0x04000C7B RID: 3195
	[SerializeField]
	protected List<GameObject> anchorObjects;

	// Token: 0x04000C7C RID: 3196
	protected Dictionary<string, UILabel> m_objectLabels;

	// Token: 0x04000C7D RID: 3197
	protected Dictionary<string, UISprite> m_objectSprites;

	// Token: 0x04000C7E RID: 3198
	protected Dictionary<string, UITexture> m_objectTextures;

	// Token: 0x04000C7F RID: 3199
	protected Dictionary<string, GameObject> m_objects;

	// Token: 0x04000C80 RID: 3200
	protected bool m_isSetObject;
}
