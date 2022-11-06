using System;
using UnityEngine;

// Token: 0x0200055A RID: 1370
public class ItemGetWindowUtil
{
	// Token: 0x06002A50 RID: 10832 RVA: 0x00106524 File Offset: 0x00104724
	public static ItemGetWindow GetItemGetWindow()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "item_get_Window");
			if (gameObject2 != null)
			{
				return gameObject2.GetComponent<ItemGetWindow>();
			}
		}
		return null;
	}
}
