using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class UIDebugMenuAchievement : UIDebugMenuTask
{
	// Token: 0x06000CB0 RID: 3248 RVA: 0x000483B4 File Offset: 0x000465B4
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 100f, 50f), UIDebugMenuAchievement.STR_BACK, base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 4; i++)
		{
			string name = this.MenuObjName[i];
			GameObject x = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(x == null))
			{
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
			}
		}
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0004846C File Offset: 0x0004666C
	protected override void OnGuiFromTask()
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00048470 File Offset: 0x00046670
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
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x000484B8 File Offset: 0x000466B8
	protected override void OnTransitionFrom()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(true);
		}
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00048500 File Offset: 0x00046700
	private void OnClicked(string name)
	{
		if (name == UIDebugMenuAchievement.STR_BACK)
		{
			base.TransitionToParent();
		}
		else if (name == this.MenuObjName[1])
		{
			AchievementManager.RequestDebugAllOpen(true);
		}
		else if (name == this.MenuObjName[0])
		{
			AchievementManager.RequestReset();
		}
		else if (name == this.MenuObjName[2])
		{
			AchievementManager.RequestDebugInfo(true);
		}
		else if (name == this.MenuObjName[3])
		{
			AchievementManager.RequestShowAchievementsUI();
		}
	}

	// Token: 0x040009F8 RID: 2552
	private List<string> MenuObjName = new List<string>
	{
		"AllReset",
		"FlagOn:AllOpen",
		"FlagOn:DebugInfo",
		"Show"
	};

	// Token: 0x040009F9 RID: 2553
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 150f, 50f),
		new Rect(200f, 300f, 150f, 50f),
		new Rect(400f, 300f, 150f, 50f),
		new Rect(200f, 400f, 150f, 50f)
	};

	// Token: 0x040009FA RID: 2554
	private static string STR_BACK = "Back";

	// Token: 0x040009FB RID: 2555
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x040009FC RID: 2556
	private UIDebugMenuButton m_backButton;

	// Token: 0x020001BD RID: 445
	private enum MenuType
	{
		// Token: 0x040009FE RID: 2558
		ALL_RESET,
		// Token: 0x040009FF RID: 2559
		ALL_OPEN,
		// Token: 0x04000A00 RID: 2560
		OUTPUT_INFO,
		// Token: 0x04000A01 RID: 2561
		SHOW,
		// Token: 0x04000A02 RID: 2562
		NUM
	}
}
