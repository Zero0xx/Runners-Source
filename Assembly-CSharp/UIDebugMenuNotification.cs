using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class UIDebugMenuNotification : UIDebugMenuTask
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x0004A0B8 File Offset: 0x000482B8
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 3; i++)
		{
			string name = this.MenuObjName[i];
			GameObject x = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(x == null))
			{
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
			}
		}
		this.m_RecieverGuid = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_RecieverGuid.Setup(new Rect(200f, 350f, 350f, 50f), "RecieverID", string.Empty);
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0004A1B0 File Offset: 0x000483B0
	protected override void OnTransitionTo()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(false);
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
		if (this.m_RecieverGuid != null)
		{
			this.m_RecieverGuid.SetActive(false);
		}
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0004A214 File Offset: 0x00048414
	protected override void OnTransitionFrom()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(true);
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_RecieverGuid != null)
		{
			this.m_RecieverGuid.SetActive(true);
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0004A278 File Offset: 0x00048478
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name.Contains("UnRegist"))
		{
			PnoteNotification.RequestUnregister();
		}
		else if (name.Contains("Regist"))
		{
			PnoteNotification.RequestRegister();
		}
		else if (name.Contains("SendMessage"))
		{
			PnoteNotification.SendMessage("Debug Send By UIDebug", this.m_RecieverGuid.text, PnoteNotification.LaunchOption.None);
		}
	}

	// Token: 0x04000A45 RID: 2629
	private List<string> MenuObjName = new List<string>
	{
		"Regist",
		"UnRegist",
		"SendMessage"
	};

	// Token: 0x04000A46 RID: 2630
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 150f, 50f),
		new Rect(400f, 200f, 150f, 50f),
		new Rect(600f, 200f, 150f, 50f)
	};

	// Token: 0x04000A47 RID: 2631
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000A48 RID: 2632
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A49 RID: 2633
	private UIDebugMenuTextField m_RecieverGuid;

	// Token: 0x020001CA RID: 458
	private enum MenuType
	{
		// Token: 0x04000A4B RID: 2635
		REGIST,
		// Token: 0x04000A4C RID: 2636
		UNREGIST,
		// Token: 0x04000A4D RID: 2637
		SENDMESSAGE,
		// Token: 0x04000A4E RID: 2638
		NUM
	}
}
