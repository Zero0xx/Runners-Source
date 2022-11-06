using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class UIDebugMenuEvent : UIDebugMenuTask
{
	// Token: 0x06000CBF RID: 3263 RVA: 0x00048B4C File Offset: 0x00046D4C
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 3; i++)
		{
			string name = this.MenuObjName[i];
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (gameObject == null)
			{
				gameObject = new GameObject();
				gameObject.name = name;
			}
			this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
		}
		this.m_textField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_textField.Setup(new Rect(200f, 400f, 350f, 40f), "log");
		this.m_textField.text = string.Empty;
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00048C58 File Offset: 0x00046E58
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
		if (this.m_textField != null)
		{
			this.m_textField.SetActive(false);
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00048CBC File Offset: 0x00046EBC
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
		if (this.m_textField != null)
		{
			this.m_textField.SetActive(true);
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00048D20 File Offset: 0x00046F20
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00048D40 File Offset: 0x00046F40
	private bool CheckEvent()
	{
		return EventManager.Instance != null && EventManager.Instance.Type != EventManager.EventType.UNKNOWN;
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00048D70 File Offset: 0x00046F70
	private string GetEventType()
	{
		if (EventManager.Instance != null)
		{
			return EventManager.Instance.Type.ToString();
		}
		return string.Empty;
	}

	// Token: 0x04000A0C RID: 2572
	private List<string> MenuObjName = new List<string>
	{
		"special_stage_1 (w7 spring)",
		"special_stage_2 (w5 desert)",
		"animal_collect"
	};

	// Token: 0x04000A0D RID: 2573
	private List<int> MenuObjId = new List<int>
	{
		100010000,
		100020000,
		300010000
	};

	// Token: 0x04000A0E RID: 2574
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 40f),
		new Rect(200f, 250f, 250f, 40f),
		new Rect(200f, 300f, 250f, 40f)
	};

	// Token: 0x04000A0F RID: 2575
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000A10 RID: 2576
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A11 RID: 2577
	private UIDebugMenuTextField m_textField = new UIDebugMenuTextField();

	// Token: 0x020001C1 RID: 449
	private enum MenuType
	{
		// Token: 0x04000A13 RID: 2579
		SPECIAL_STAGE_01,
		// Token: 0x04000A14 RID: 2580
		SPECIAL_STAGE_02,
		// Token: 0x04000A15 RID: 2581
		ANIMAL_COLLECT,
		// Token: 0x04000A16 RID: 2582
		NUM
	}
}
