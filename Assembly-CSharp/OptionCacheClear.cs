using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class OptionCacheClear : MonoBehaviour
{
	// Token: 0x060022FB RID: 8955 RVA: 0x000D20BC File Offset: 0x000D02BC
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		this.CreateCacheClearWindow();
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x000D20F0 File Offset: 0x000D02F0
	private void CreateCacheClearWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "cache_clear",
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_bar"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_explanation"),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.YesNo
		});
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x000D215C File Offset: 0x000D035C
	public void Update()
	{
		bool flag = false;
		if (GeneralWindow.IsCreated("cache_clear") && GeneralWindow.IsButtonPressed)
		{
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralUtil.CleanAllCache();
				GeneralWindow.Close();
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					name = "cache_clear_end",
					caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_confirmation_bar"),
					message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_confirmation"),
					anchor_path = "Camera/Anchor_5_MC",
					buttonType = GeneralWindow.ButtonType.Ok
				});
			}
			else
			{
				flag = true;
				GeneralWindow.Close();
			}
		}
		if (GeneralWindow.IsCreated("cache_clear_end") && GeneralWindow.IsOkButtonPressed)
		{
			GeneralWindow.Close();
			HudMenuUtility.SendMsgMenuSequenceToMainMenu(MsgMenuSequence.SequeneceType.TITLE);
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

	// Token: 0x04001FA0 RID: 8096
	private ui_option_scroll m_ui_option_scroll;
}
