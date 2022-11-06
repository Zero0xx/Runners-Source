using System;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class FirstLaunchUserSetting : MonoBehaviour
{
	// Token: 0x06001EF0 RID: 7920 RVA: 0x000B7F00 File Offset: 0x000B6100
	public void PlayStart()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		this.m_isEndPlay = false;
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x000B7F3C File Offset: 0x000B613C
	// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x000B7F44 File Offset: 0x000B6144
	public bool IsEndPlay
	{
		get
		{
			return this.m_isEndPlay;
		}
		private set
		{
		}
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x000B7F48 File Offset: 0x000B6148
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		this.m_snsLogin = base.gameObject.AddComponent<SettingPartsSnsLogin>();
		this.m_snsLogin.Setup(this.ANCHOR_PATH);
		this.m_acceptInvite = base.gameObject.AddComponent<SettingPartsAcceptInvite>();
		this.m_acceptInvite.Setup(this.ANCHOR_PATH);
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x000B7FEC File Offset: 0x000B61EC
	private void Update()
	{
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x000B7FF0 File Offset: 0x000B61F0
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateHelp)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x000B8068 File Offset: 0x000B6268
	private TinyFsmState StateSnsLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_snsLogin != null)
			{
				this.m_snsLogin.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_snsLogin.IsEnd)
			{
				if (this.m_snsLogin.IsCalceled)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateHelp)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskInputInviteCode)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x000B8130 File Offset: 0x000B6330
	private TinyFsmState StateAskInputInviteCode(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "StateAskInputInviteCode",
				buttonType = GeneralWindow.ButtonType.YesNo,
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_ask_accept_invite_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_ask_accept_invite_text").text,
				anchor_path = this.ANCHOR_PATH
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("StateAskInputInviteCode"))
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputInviteCode)));
				}
				else if (GeneralWindow.IsNoButtonPressed)
				{
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateHelp)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateHelp)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x000B8274 File Offset: 0x000B6474
	private TinyFsmState StateInputInviteCode(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_acceptInvite != null)
			{
				this.m_acceptInvite.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_acceptInvite != null && this.m_acceptInvite.IsEndPlay())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateHelp)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x000B831C File Offset: 0x000B651C
	private TinyFsmState StateHelp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "StateHelp",
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_about_help_menu_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_about_help_menu_text").text,
				anchor_path = this.ANCHOR_PATH,
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("StateHelp"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateOptionButton)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateOptionButton)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x000B8430 File Offset: 0x000B6630
	private TinyFsmState StateOptionButton(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.OPTION);
			this.m_timer = 3f;
			return TinyFsmState.End();
		case 4:
			this.m_timer -= Time.deltaTime;
			if (TutorialCursor.IsTouchScreen() || this.m_timer < 0f)
			{
				TutorialCursor.DestroyTutorialCursor();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetUserResult)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x000B84DC File Offset: 0x000B66DC
	private TinyFsmState StateGetUserResult(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_getUserResult = false;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerOptionUserResult(base.gameObject);
			}
			else
			{
				this.m_getUserResult = true;
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_getUserResult)
			{
				if (this.m_chaoRouletteNum == 0)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetSpecialEggNum)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndProcessSpEgg)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x000B85B0 File Offset: 0x000B67B0
	private void ServerGetOptionUserResult_Succeeded(MsgGetOptionUserResultSucceed msg)
	{
		if (msg != null && msg.m_serverOptionUserResult != null)
		{
			this.m_chaoRouletteNum = msg.m_serverOptionUserResult.m_numChaoRoulette;
		}
		this.m_getUserResult = true;
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x000B85DC File Offset: 0x000B67DC
	private void ServerGetOptionUserResult_Failed(MsgServerConnctFailed msg)
	{
		this.m_getUserResult = true;
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x000B85E8 File Offset: 0x000B67E8
	private TinyFsmState StateGetSpecialEggNum(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_getSpecialEggNum = false;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetChaoWheelOptions(base.gameObject);
			}
			else
			{
				this.m_getSpecialEggNum = true;
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_getSpecialEggNum)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndProcessSpEgg)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x000B8690 File Offset: 0x000B6890
	private void ServerGetChaoWheelOptions_Succeeded(MsgGetChaoWheelOptionsSucceed msg)
	{
		if (msg != null && msg.m_options != null)
		{
			this.m_specialEggNum = msg.m_options.NumSpecialEggs;
		}
		this.m_getSpecialEggNum = true;
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x000B86BC File Offset: 0x000B68BC
	private void ServerGetChaoWheelOptions_Failed(MsgServerConnctFailed msg)
	{
		this.m_getSpecialEggNum = true;
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x000B86C8 File Offset: 0x000B68C8
	private bool IsCheater()
	{
		return this.m_specialEggNum != 1 || this.m_chaoRouletteNum != 0;
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x000B86E4 File Offset: 0x000B68E4
	private TinyFsmState StateEndProcessSpEgg(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.SetFlagStatus(SystemData.FlagStatus.TUTORIAL_END, true);
					instance.SaveSystemData();
				}
			}
			FoxManager.SendLtvPoint(FoxLtvType.CompeleteTutorial);
			Resources.UnloadUnusedAssets();
			GC.Collect();
			return TinyFsmState.End();
		}
		case 1:
			if (this.IsCheater())
			{
				this.m_addSpecialEgg = true;
			}
			else
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerAddSpecialEgg(9, base.gameObject);
					this.m_addSpecialEgg = false;
				}
				else
				{
					this.m_addSpecialEgg = true;
				}
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_addSpecialEgg)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndProcessApollo)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x000B87E0 File Offset: 0x000B69E0
	private void ServerAddSpecialEgg_Succeeded(MsgAddSpecialEggSucceed msg)
	{
		this.m_addSpecialEgg = true;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x000B87EC File Offset: 0x000B69EC
	private void ServerAddSpecialEgg_Failed(MsgServerConnctFailed msg)
	{
		this.m_addSpecialEgg = true;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x000B87F8 File Offset: 0x000B69F8
	private TinyFsmState StateEndProcessApollo(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_sendApollo != null)
			{
				UnityEngine.Object.Destroy(this.m_sendApollo.gameObject);
				this.m_sendApollo = null;
			}
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTutorialEnd)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x000B888C File Offset: 0x000B6A8C
	private TinyFsmState StateTutorialEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "StateTutorialEnd",
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "end_of_tutorial_text").text,
				anchor_path = this.ANCHOR_PATH,
				buttonType = GeneralWindow.ButtonType.TweetCancel
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("StateTutorialEnd"))
			{
				if (GeneralWindow.IsYesButtonPressed)
				{
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MileageMap", "feed_highscore_caption").text;
					string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "tutorial_end_feed_text").text;
					this.m_feed = new EasySnsFeed(base.gameObject, this.ANCHOR_PATH, text, text2, null);
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitSnsFeedEnd)));
				}
				else if (GeneralWindow.IsNoButtonPressed)
				{
					SystemSaveManager instance = SystemSaveManager.Instance;
					if (instance != null)
					{
						SystemData systemdata = instance.GetSystemdata();
						if (systemdata != null)
						{
							systemdata.SetFacebookWindow(false);
							instance.SaveSystemData();
						}
					}
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSpEggHelp)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSpEggHelp)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x000B8A48 File Offset: 0x000B6C48
	private TinyFsmState StateWaitSnsFeedEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_feed != null)
			{
				EasySnsFeed.Result result = this.m_feed.Update();
				if (result == EasySnsFeed.Result.COMPLETED || result == EasySnsFeed.Result.FAILED)
				{
					global::Debug.Log("FirstLaunchUserSetting.EasySnsFeed.Result=" + result.ToString());
					this.m_feed = null;
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSpEggHelp)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x000B8AF8 File Offset: 0x000B6CF8
	private TinyFsmState StateSpEggHelp(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow != null)
			{
				itemGetWindow.Create(new ItemGetWindow.CInfo
				{
					caption = TextUtility.GetCommonText("MainMenu", "tutorial_sp_egg1_caption"),
					serverItemId = 220000,
					imageCount = TextUtility.GetCommonText("MainMenu", "tutorial_sp_egg1_text", "{COUNT}", 9.ToString())
				});
				SoundManager.SePlay("sys_specialegg", "SE");
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			ItemGetWindow itemGetWindow2 = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow2 != null && itemGetWindow2.IsEnd)
			{
				HudMenuUtility.SendMsgUpdateSaveDataDisplay();
				itemGetWindow2.Reset();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSpEggHelp2)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x000B8C00 File Offset: 0x000B6E00
	private TinyFsmState StateSpEggHelp2(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "StateSpEggHelp2",
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "tutorial_sp_egg2_caption").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "tutorial_sp_egg2_text").text,
				anchor_path = this.ANCHOR_PATH,
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("StateSpEggHelp2"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x000B8D14 File Offset: 0x000B6F14
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_isEndPlay = true;
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x04001C4D RID: 7245
	private readonly string ANCHOR_PATH = "Camera/menu_Anim/MainMenuUI4/Anchor_5_MC";

	// Token: 0x04001C4E RID: 7246
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001C4F RID: 7247
	private SettingPartsSnsLogin m_snsLogin;

	// Token: 0x04001C50 RID: 7248
	private SettingPartsAcceptInvite m_acceptInvite;

	// Token: 0x04001C51 RID: 7249
	private EasySnsFeed m_feed;

	// Token: 0x04001C52 RID: 7250
	private SendApollo m_sendApollo;

	// Token: 0x04001C53 RID: 7251
	private int m_specialEggNum = -1;

	// Token: 0x04001C54 RID: 7252
	private int m_chaoRouletteNum = -1;

	// Token: 0x04001C55 RID: 7253
	private bool m_isEndPlay;

	// Token: 0x04001C56 RID: 7254
	private float m_timer;

	// Token: 0x04001C57 RID: 7255
	private bool m_addSpecialEgg;

	// Token: 0x04001C58 RID: 7256
	private bool m_getSpecialEggNum;

	// Token: 0x04001C59 RID: 7257
	private bool m_getUserResult;

	// Token: 0x0200040F RID: 1039
	private enum EventSignal
	{
		// Token: 0x04001C5B RID: 7259
		PLAY_START = 100,
		// Token: 0x04001C5C RID: 7260
		SNS_LOGIN_END
	}
}
