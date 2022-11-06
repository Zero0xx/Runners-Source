using System;
using Text;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class HudQuestionButton : MonoBehaviour
{
	// Token: 0x060021BE RID: 8638 RVA: 0x000CB2EC File Offset: 0x000C94EC
	public void Initialize(bool isQuickMode)
	{
		this.m_isQuickMode = isQuickMode;
		if (isQuickMode)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_5_MC");
			if (gameObject == null)
			{
				return;
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "1_Quick");
			if (gameObject2 == null)
			{
				return;
			}
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject2, "Btn_0_help");
			if (uibuttonMessage == null)
			{
				return;
			}
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "QuickModeQuestionButtonClicked";
		}
		else
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "Anchor_5_MC");
			if (gameObject3 == null)
			{
				return;
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject3, "0_Endless");
			if (gameObject4 == null)
			{
				return;
			}
			UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject4, "Btn_0_help");
			if (uibuttonMessage2 == null)
			{
				return;
			}
			uibuttonMessage2.target = base.gameObject;
			uibuttonMessage2.functionName = "EndlessModeQuestionButtonClicked";
		}
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000CB3E4 File Offset: 0x000C95E4
	private void QuickModeQuestionButtonClicked()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "QuickModeQuestion",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "help_quick_mode_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "help_quick_mode_text").text
		});
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000CB44C File Offset: 0x000C964C
	private void EndlessModeQuestionButtonClicked()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "EndlessModeQuestion",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "help_episode_mode_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "help_episode_mode_text").text
		});
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x000CB4B4 File Offset: 0x000C96B4
	private void Start()
	{
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x000CB4B8 File Offset: 0x000C96B8
	private void Update()
	{
		bool flag = false;
		if (GeneralWindow.IsCreated("QuickModeQuestion"))
		{
			flag = true;
		}
		else if (GeneralWindow.IsCreated("QuickModeQuestion"))
		{
			flag = true;
		}
		if (flag && GeneralWindow.IsOkButtonPressed)
		{
			GeneralWindow.Close();
		}
	}

	// Token: 0x04001E78 RID: 7800
	private bool m_isQuickMode;
}
