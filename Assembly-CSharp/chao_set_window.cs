using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class chao_set_window : WindowBase
{
	// Token: 0x06001E1A RID: 7706 RVA: 0x000B0EB8 File Offset: 0x000AF0B8
	private void Start()
	{
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x000B0EBC File Offset: 0x000AF0BC
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!ChaoSetUI.IsChaoTutorial())
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
}
