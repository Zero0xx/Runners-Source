using System;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class window_name_setting : WindowBase
{
	// Token: 0x0600294A RID: 10570 RVA: 0x000FF3B4 File Offset: 0x000FD5B4
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x000FF3C0 File Offset: 0x000FD5C0
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

	// Token: 0x0600294C RID: 10572 RVA: 0x000FF414 File Offset: 0x000FD614
	private void OnDestroy()
	{
		base.Destroy();
	}
}
