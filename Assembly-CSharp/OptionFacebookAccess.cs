using System;
using Text;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class OptionFacebookAccess : MonoBehaviour
{
	// Token: 0x060022FF RID: 8959 RVA: 0x000D225C File Offset: 0x000D045C
	public void Setup(ui_option_scroll scroll)
	{
		base.enabled = true;
		if (this.m_ui_option_scroll == null && scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (this.m_gameObject != null)
		{
			this.m_initFlag = true;
			this.m_gameObject.SetActive(true);
			if (this.m_eventSetting != null)
			{
				this.m_eventSetting.Setup(window_event_setting.TextType.FACEBOOK_ACCESS);
				this.m_eventSetting.PlayOpenWindow();
				this.m_State = OptionFacebookAccess.State.IDLE;
			}
		}
		else
		{
			this.m_initFlag = false;
			this.m_gameObject = HudMenuUtility.GetLoadMenuChildObject("window_event_setting", true);
		}
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x000D2304 File Offset: 0x000D0504
	private void SetEventSetting()
	{
		if (this.m_gameObject != null && this.m_eventSetting == null)
		{
			this.m_eventSetting = this.m_gameObject.GetComponent<window_event_setting>();
		}
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x000D233C File Offset: 0x000D053C
	public void Update()
	{
		if (!this.m_initFlag)
		{
			this.m_initFlag = true;
			this.SetEventSetting();
			if (this.m_eventSetting != null)
			{
				this.m_eventSetting.Setup(window_event_setting.TextType.FACEBOOK_ACCESS);
				this.m_eventSetting.PlayOpenWindow();
				this.m_State = OptionFacebookAccess.State.IDLE;
			}
		}
		else
		{
			switch (this.m_State)
			{
			case OptionFacebookAccess.State.IDLE:
				if (this.m_eventSetting != null && this.m_eventSetting.IsEnd)
				{
					switch (this.m_eventSetting.EndState)
					{
					case window_event_setting.State.PRESS_LOGIN:
						this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/Anchor_5_MC");
						this.m_State = OptionFacebookAccess.State.LOGIN;
						break;
					case window_event_setting.State.PRESS_LOGOUT:
						this.CreateLogoutWindow();
						this.m_State = OptionFacebookAccess.State.LOGOUT;
						break;
					case window_event_setting.State.CLOSE:
						this.CloseFunction();
						break;
					}
				}
				break;
			case OptionFacebookAccess.State.LOGIN:
				if (this.m_easySnsFeed != null)
				{
					EasySnsFeed.Result result = this.m_easySnsFeed.Update();
					if (result != EasySnsFeed.Result.COMPLETED)
					{
						if (result == EasySnsFeed.Result.FAILED)
						{
							this.m_easySnsFeed = null;
							this.CloseFunction();
						}
					}
					else
					{
						this.m_easySnsFeed = null;
						this.CloseFunction();
					}
				}
				break;
			case OptionFacebookAccess.State.LOGOUT:
				if (GeneralWindow.IsCreated("FacebookLogout") && GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
						if (socialInterface != null)
						{
							socialInterface.Logout();
							socialInterface.IsLoggedIn = false;
							PlayerImageManager instance = PlayerImageManager.Instance;
							if (instance != null)
							{
								instance.ClearAllPlayerImage();
							}
							HudMenuUtility.SetUpdateRankingFlag();
						}
						this.m_State = OptionFacebookAccess.State.LOGOUT_COMPLETE_SETTING;
					}
					else
					{
						this.CloseFunction();
					}
					GeneralWindow.Close();
				}
				break;
			case OptionFacebookAccess.State.LOGOUT_COMPLETE_SETTING:
				this.CreateLogoutCompleteWindow();
				this.m_State = OptionFacebookAccess.State.LOGOUT_COMPLETE;
				break;
			case OptionFacebookAccess.State.LOGOUT_COMPLETE:
				if (GeneralWindow.IsCreated("LogoutComplete") && GeneralWindow.IsButtonPressed)
				{
					this.CloseFunction();
					GeneralWindow.Close();
				}
				break;
			}
		}
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x000D2564 File Offset: 0x000D0764
	private void CreateLogoutWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "FacebookLogout",
			buttonType = GeneralWindow.ButtonType.YesNo,
			caption = TextUtility.GetCommonText("Option", "logout"),
			message = TextUtility.GetCommonText("Option", "logout_message")
		});
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x000D25C0 File Offset: 0x000D07C0
	private void CreateLogoutCompleteWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "LogoutComplete",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetCommonText("Option", "logout"),
			message = TextUtility.GetCommonText("Option", "logout_complete")
		});
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x000D261C File Offset: 0x000D081C
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
		this.m_State = OptionFacebookAccess.State.CLOSE;
	}

	// Token: 0x04001FA1 RID: 8097
	private window_event_setting m_eventSetting;

	// Token: 0x04001FA2 RID: 8098
	private GameObject m_gameObject;

	// Token: 0x04001FA3 RID: 8099
	private ui_option_scroll m_ui_option_scroll;

	// Token: 0x04001FA4 RID: 8100
	private bool m_initFlag;

	// Token: 0x04001FA5 RID: 8101
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x04001FA6 RID: 8102
	private OptionFacebookAccess.State m_State;

	// Token: 0x02000497 RID: 1175
	private enum State
	{
		// Token: 0x04001FA8 RID: 8104
		INIT,
		// Token: 0x04001FA9 RID: 8105
		IDLE,
		// Token: 0x04001FAA RID: 8106
		LOGIN,
		// Token: 0x04001FAB RID: 8107
		LOGOUT,
		// Token: 0x04001FAC RID: 8108
		LOGOUT_COMPLETE_SETTING,
		// Token: 0x04001FAD RID: 8109
		LOGOUT_COMPLETE,
		// Token: 0x04001FAE RID: 8110
		CLOSE
	}
}
