using System;
using UnityEngine;

// Token: 0x020004E2 RID: 1250
public class player_set_window : WindowBase
{
	// Token: 0x0600253C RID: 9532 RVA: 0x000E0738 File Offset: 0x000DE938
	private void Start()
	{
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x000E073C File Offset: 0x000DE93C
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_window_close");
		if (gameObject != null)
		{
			UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}
}
