using System;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public class CheckID : MonoBehaviour
{
	// Token: 0x060022EA RID: 8938 RVA: 0x000D1930 File Offset: 0x000CFB30
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_gameObject == null)
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_id_info", true);
		}
		this.m_State = CheckID.State.INIT;
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x000D1998 File Offset: 0x000CFB98
	private void SetIdInfo()
	{
		if (this.m_gameObject != null && this.m_idInfo == null)
		{
			this.m_idInfo = this.m_gameObject.GetComponent<window_id_info>();
		}
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x000D19D0 File Offset: 0x000CFBD0
	public void Update()
	{
		switch (this.m_State)
		{
		case CheckID.State.INIT:
			this.CreateFirstCautionWindow();
			break;
		case CheckID.State.FIRST_CAUTION:
			if (GeneralWindow.IsCreated("FirstCaution") && GeneralWindow.IsButtonPressed)
			{
				if (this.m_gameObject != null)
				{
					this.m_gameObject.SetActive(false);
				}
				GeneralWindow.Close();
				this.m_State = CheckID.State.CHECK_PASSWORD;
			}
			break;
		case CheckID.State.CHECK_PASSWORD:
		{
			string takeoverID = SystemSaveManager.GetTakeoverID();
			if (string.IsNullOrEmpty(takeoverID))
			{
				this.CreateUserPassWindow();
			}
			else
			{
				this.SetupInfoWindow();
			}
			break;
		}
		case CheckID.State.INPUT_USERPASS:
			if (this.m_settingPassword != null && this.m_settingPassword.IsEndPlay())
			{
				if (this.m_settingPassword.isCancel)
				{
					this.CloseFunction();
				}
				else
				{
					this.SetupInfoWindow();
				}
			}
			break;
		case CheckID.State.IDLE:
			if (this.m_idInfo != null && this.m_idInfo.IsEnd)
			{
				if (this.m_idInfo.IsPassResetEnd)
				{
					if (this.m_gameObject != null)
					{
						this.m_gameObject.SetActive(false);
					}
					this.CreateUserPassWindow();
				}
				else
				{
					this.CloseFunction();
				}
			}
			break;
		}
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x000D1B38 File Offset: 0x000CFD38
	private void CreateFirstCautionWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "FirstCaution",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetCommonText("Option", "take_over_attention"),
			message = TextUtility.GetCommonText("Option", "take_over_attention_text")
		});
		this.m_State = CheckID.State.FIRST_CAUTION;
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x000D1B9C File Offset: 0x000CFD9C
	private void CreateUserPassWindow()
	{
		if (this.m_settingPassword == null)
		{
			this.m_settingPassword = base.gameObject.AddComponent<SettingTakeoverPassword>();
		}
		if (this.m_settingPassword != null)
		{
			this.m_settingPassword.SetCancelButtonUseFlag(true);
			this.m_settingPassword.Setup("UI Root (2D)/Camera/Anchor_5_MC");
			this.m_settingPassword.PlayStart();
		}
		this.m_State = CheckID.State.INPUT_USERPASS;
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x000D1C0C File Offset: 0x000CFE0C
	private void SetupInfoWindow()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetIdInfo();
		}
		this.m_gameObject.SetActive(true);
		if (this.m_idInfo != null)
		{
			this.m_idInfo.PlayOpenWindow();
		}
		this.m_State = CheckID.State.IDLE;
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x000D1C60 File Offset: 0x000CFE60
	private void CloseFunction()
	{
		if (this.m_ui_option_scroll != null)
		{
			this.m_ui_option_scroll.OnEndChildPage();
		}
		base.enabled = false;
		if (this.m_gameObject != null)
		{
			this.m_gameObject.SetActive(false);
		}
		this.m_State = CheckID.State.CLOSE;
	}

	// Token: 0x04001F8A RID: 8074
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x04001F8B RID: 8075
	private window_id_info m_idInfo;

	// Token: 0x04001F8C RID: 8076
	private GameObject m_gameObject;

	// Token: 0x04001F8D RID: 8077
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001F8E RID: 8078
	private SettingTakeoverPassword m_settingPassword;

	// Token: 0x04001F8F RID: 8079
	private bool m_initFlag;

	// Token: 0x04001F90 RID: 8080
	private CheckID.State m_State;

	// Token: 0x02000492 RID: 1170
	private enum State
	{
		// Token: 0x04001F92 RID: 8082
		INIT,
		// Token: 0x04001F93 RID: 8083
		FIRST_CAUTION,
		// Token: 0x04001F94 RID: 8084
		CHECK_PASSWORD,
		// Token: 0x04001F95 RID: 8085
		INPUT_USERPASS,
		// Token: 0x04001F96 RID: 8086
		GET_MOVING_PASSWORD,
		// Token: 0x04001F97 RID: 8087
		IDLE,
		// Token: 0x04001F98 RID: 8088
		CLOSE
	}
}
