using System;
using UnityEngine;

// Token: 0x0200053B RID: 1339
public class window_pushinfo_setting : WindowBase
{
	// Token: 0x0600294E RID: 10574 RVA: 0x000FF424 File Offset: 0x000FD624
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000FF430 File Offset: 0x000FD630
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		if (gameObject != null)
		{
			UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x000FF484 File Offset: 0x000FD684
	private void OnDestroy()
	{
		base.Destroy();
	}
}
