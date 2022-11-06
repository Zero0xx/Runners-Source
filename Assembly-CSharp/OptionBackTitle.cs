using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class OptionBackTitle : MonoBehaviour
{
	// Token: 0x060022F7 RID: 8951 RVA: 0x000D1FA4 File Offset: 0x000D01A4
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		this.CreateBackTitleWindow();
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x000D1FD8 File Offset: 0x000D01D8
	private void CreateBackTitleWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "BackTitle",
			caption = TextUtility.GetCommonText("MainMenu", "back_title_caption"),
			message = TextUtility.GetCommonText("MainMenu", "back_title_text"),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.YesNo
		});
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x000D2040 File Offset: 0x000D0240
	public void Update()
	{
		bool flag = false;
		if (GeneralWindow.IsCreated("BackTitle") && GeneralWindow.IsButtonPressed)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TITLE);
			}
			else
			{
				flag = true;
				GeneralWindow.Close();
			}
		}
		if (flag)
		{
			if (this.m_ui_option_scroll != null)
			{
				this.m_ui_option_scroll.OnEndChildPage();
			}
			base.enabled = false;
		}
	}

	// Token: 0x04001F9F RID: 8095
	private ui_option_scroll m_ui_option_scroll;
}
