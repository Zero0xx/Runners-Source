using System;
using App;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000689 RID: 1673
public class ErrorHandleServerNextVersion : ErrorHandleBase
{
	// Token: 0x06002C98 RID: 11416 RVA: 0x0010CF84 File Offset: 0x0010B184
	private bool IsRegionJapan()
	{
		return RegionManager.Instance != null && RegionManager.Instance.IsJapan();
	}

	// Token: 0x06002C99 RID: 11417 RVA: 0x0010CFA4 File Offset: 0x0010B1A4
	private string GetReplaceText(string srcText, string tag, string replace)
	{
		if (!string.IsNullOrEmpty(srcText) && !string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(replace))
		{
			return TextUtility.Replace(srcText, tag, replace);
		}
		return srcText;
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x0010CFD4 File Offset: 0x0010B1D4
	public override void Setup(GameObject callbackObject, string callbackFuncName, MessageBase msg)
	{
	}

	// Token: 0x06002C9B RID: 11419 RVA: 0x0010CFD8 File Offset: 0x0010B1D8
	public override void StartErrorHandle()
	{
		NetworkErrorWindow.CInfo info = default(NetworkErrorWindow.CInfo);
		info.anchor_path = string.Empty;
		this.m_titleBack = GameModeTitle.Logined;
		if (this.m_titleBack)
		{
			info.buttonType = NetworkErrorWindow.ButtonType.Ok;
			info.caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption");
			info.message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_reset");
			info.message += -19990.ToString();
		}
		else
		{
			this.m_userId = SystemSaveManager.GetGameID();
			info.caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_notification_caption");
			bool flag = Env.language == Env.Language.JAPANESE;
			string text = (!flag) ? ServerInterface.NextVersionState.m_eMsg : ServerInterface.NextVersionState.m_jMsg;
			if (this.m_userId != "0" && this.IsRegionJapan())
			{
				this.m_buyRSRNum = ServerInterface.NextVersionState.m_buyRSRNum;
				this.m_freeRSRNum = ServerInterface.NextVersionState.m_freeRSRNum;
				this.m_userName = ServerInterface.NextVersionState.m_userName;
				if (string.IsNullOrEmpty(this.m_userName))
				{
					ServerSettingState settingState = ServerInterface.SettingState;
					if (settingState != null)
					{
						this.m_userName = settingState.m_userName;
					}
					if (string.IsNullOrEmpty(this.m_userName))
					{
						this.m_userName = " ";
					}
				}
				info.buttonType = NetworkErrorWindow.ButtonType.Repayment;
				string text2 = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_user_info_text");
				text2 = this.GetReplaceText(text2, "{PARAM1}", this.m_userName);
				text2 = this.GetReplaceText(text2, "{PARAM2}", this.m_userId);
				text2 = this.GetReplaceText(text2, "{PARAM3}", this.m_buyRSRNum.ToString());
				text2 = this.GetReplaceText(text2, "{PARAM4}", this.m_freeRSRNum.ToString());
				text = text + "\n" + text2;
			}
			else
			{
				info.buttonType = NetworkErrorWindow.ButtonType.TextOnly;
			}
			info.message = text;
		}
		NetworkErrorWindow.Create(info);
		this.m_isEnd = false;
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x0010D1F4 File Offset: 0x0010B3F4
	public override void Update()
	{
		if (this.m_titleBack)
		{
			if (NetworkErrorWindow.IsOkButtonPressed)
			{
				NetworkErrorWindow.Close();
				HudMenuUtility.GoToTitleScene();
				this.m_isEnd = true;
			}
		}
		else if (NetworkErrorWindow.IsButtonPressed)
		{
			NetworkErrorWindow.ResetButton();
			string url = ServerInterface.NextVersionState.m_url;
			if (!string.IsNullOrEmpty(url))
			{
				Application.OpenURL(url);
			}
		}
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x0010D258 File Offset: 0x0010B458
	public override bool IsEnd()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002C9E RID: 11422 RVA: 0x0010D260 File Offset: 0x0010B460
	public override void EndErrorHandle()
	{
	}

	// Token: 0x0400296E RID: 10606
	private int m_buyRSRNum;

	// Token: 0x0400296F RID: 10607
	private int m_freeRSRNum;

	// Token: 0x04002970 RID: 10608
	private string m_userId = string.Empty;

	// Token: 0x04002971 RID: 10609
	private string m_userName = string.Empty;

	// Token: 0x04002972 RID: 10610
	private bool m_titleBack;

	// Token: 0x04002973 RID: 10611
	private bool m_isEnd;
}
