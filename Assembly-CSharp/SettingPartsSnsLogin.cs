using System;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200052E RID: 1326
public class SettingPartsSnsLogin : MonoBehaviour
{
	// Token: 0x060028F5 RID: 10485 RVA: 0x000FD594 File Offset: 0x000FB794
	public void Setup(string anchorPath)
	{
		this.m_anchorPath = anchorPath;
	}

	// Token: 0x060028F6 RID: 10486 RVA: 0x000FD5A0 File Offset: 0x000FB7A0
	public void PlayStart()
	{
		this.m_isEndPlay = false;
		this.m_isCalceled = false;
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.UniqueWindow, false);
		}
		this.m_isPlayStart = true;
		HudMenuUtility.SetConnectAlertSimpleUI(true);
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000FD5E4 File Offset: 0x000FB7E4
	// (set) Token: 0x060028F8 RID: 10488 RVA: 0x000FD5EC File Offset: 0x000FB7EC
	public bool IsEnd
	{
		get
		{
			return this.m_isEndPlay;
		}
		private set
		{
		}
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000FD5F0 File Offset: 0x000FB7F0
	public bool IsCalceled
	{
		get
		{
			return this.m_isCalceled;
		}
	}

	// Token: 0x060028FA RID: 10490 RVA: 0x000FD5F8 File Offset: 0x000FB7F8
	public bool IsEnableCreateCustomData()
	{
		string gameID = SystemSaveManager.GetGameID();
		return gameID != null && gameID != "0";
	}

	// Token: 0x060028FB RID: 10491 RVA: 0x000FD624 File Offset: 0x000FB824
	public void SetCancelWindowUseFlag(bool flag)
	{
		this.m_cancelWindowUseFlag = flag;
	}

	// Token: 0x060028FC RID: 10492 RVA: 0x000FD630 File Offset: 0x000FB830
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
	}

	// Token: 0x060028FD RID: 10493 RVA: 0x000FD690 File Offset: 0x000FB890
	private void Update()
	{
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x000FD694 File Offset: 0x000FB894
	private TinyFsmState StateIdle(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (this.m_isPlayStart)
			{
				SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
				if (socialInterface != null)
				{
					if (socialInterface.IsLoggedIn)
					{
						global::Debug.Log("SettingPartsSnsLoging: Logging in");
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
					}
					else
					{
						global::Debug.Log("SettingPartsSnsLoging: Not Logging in");
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskSnsLogin)));
					}
				}
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000FD768 File Offset: 0x000FB968
	private TinyFsmState StateAskSnsLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateAskSnsLogin");
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_facebook_login").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_facebook_login_info").text,
				anchor_path = this.m_anchorPath,
				buttonType = GeneralWindow.ButtonType.YesNo,
				name = "FacebookLogin"
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				global::Debug.Log("SettingPartsSnsLoging:AskSnsLogin.YesButton");
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsLogin)));
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				global::Debug.Log("SettingPartsSnsLoging:AskSnsLogin.NoButton");
				GeneralWindow.Close();
				if (this.m_cancelWindowUseFlag)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsLoginCanceled)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000FD8C8 File Offset: 0x000FBAC8
	private TinyFsmState StateSnsLoginCanceled(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateSnsLoginCanceled");
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_facebook_login_method").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_facebook_login_method_info").text,
				anchor_path = this.m_anchorPath,
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_isCalceled = true;
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000FD9B0 File Offset: 0x000FBBB0
	private TinyFsmState StateSnsLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			global::Debug.Log("SettingPartsSnsLoging:StateSnsLogin");
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null)
			{
				socialInterface.Login(base.gameObject);
			}
			NetMonitor instance = NetMonitor.Instance;
			if (instance != null)
			{
				instance.StartMonitor(null);
			}
			return TinyFsmState.End();
		}
		default:
		{
			if (signal == 101)
			{
				NetMonitor instance2 = NetMonitor.Instance;
				if (instance2 != null)
				{
					instance2.EndMonitorForward(null, null, null);
					instance2.EndMonitorBackward();
				}
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					this.m_windowQueue = base.gameObject.AddComponent<IncentiveWindowQueue>();
					loggedInServerInterface.RequestServerGetFacebookIncentive(0, 0, base.gameObject);
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIncentiveConnectWait)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetSNSInformations)));
				}
				return TinyFsmState.End();
			}
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			NetMonitor instance3 = NetMonitor.Instance;
			if (instance3 != null)
			{
				instance3.EndMonitorForward(null, null, null);
				instance3.EndMonitorBackward();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsLoginFailed)));
			return TinyFsmState.End();
		}
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x000FDB38 File Offset: 0x000FBD38
	private TinyFsmState StateIncentiveConnectWait(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateIncentiveConnectWait");
			return TinyFsmState.End();
		default:
			if (signal != 103)
			{
				return TinyFsmState.End();
			}
			global::Debug.Log("SettingPartsSnsLoging:StateIncentiveConnectWait INCENTIVE_CONNECT_END");
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSetupIncentiveQueue)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000FDBC4 File Offset: 0x000FBDC4
	private TinyFsmState StateSetupIncentiveQueue(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateSetupIncentiveQueue");
			return TinyFsmState.End();
		case 4:
			if (this.m_windowQueue && this.m_windowQueue.SetUpped)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIncentiveDisplaying)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x000FDC58 File Offset: 0x000FBE58
	private TinyFsmState StateIncentiveDisplaying(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateIncentiveDisplaying");
			if (this.m_windowQueue != null)
			{
				global::Debug.Log("SettingPartsSnsLoging:StateIncentiveDisplaying  m_windowQueue.PlayStart()");
				this.m_windowQueue.PlayStart();
			}
			return TinyFsmState.End();
		case 4:
			global::Debug.Log("SettingPartsSnsLoging:StateIncentiveDisplaying  UPDATE(1)");
			if (this.m_windowQueue != null)
			{
				global::Debug.Log("SettingPartsSnsLoging:StateIncentiveDisplaying  UPDATE(2)");
				if (this.m_windowQueue.IsEmpty())
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetSNSInformations)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000FDD28 File Offset: 0x000FBF28
	private TinyFsmState StateSnsLoginFailed(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			global::Debug.Log("SettingPartsSnsLoging:StateGetSNSInformations");
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_network_error").text,
				message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_network_error_info").text,
				anchor_path = this.m_anchorPath,
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskSnsLogin)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000FDE08 File Offset: 0x000FC008
	private TinyFsmState StateGetSNSInformations(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			global::Debug.Log("SettingPartsSnsLoging:StateGetSNSInformations");
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null && socialInterface.IsLoggedIn)
			{
				socialInterface.RequestFriendRankingInfoSet(null, null, SettingPartsSnsAdditional.Mode.BACK_GROUND_LOAD);
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			SocialInterface socialInterface2 = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface2 != null && socialInterface2.IsLoggedIn)
			{
				if (socialInterface2.IsEnableFriendInfo)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x000FDEFC File Offset: 0x000FC0FC
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			UIEffectManager instance = UIEffectManager.Instance;
			if (instance != null)
			{
				instance.SetActiveEffect(HudMenuUtility.EffectPriority.UniqueWindow, true);
			}
			global::Debug.Log("SettingPartsSnsLoging:StateEnd");
			this.m_isPlayStart = false;
			this.m_isEndPlay = true;
			HudMenuUtility.SetConnectAlertSimpleUI(false);
			return TinyFsmState.End();
		}
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIdle)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x000FDFA0 File Offset: 0x000FC1A0
	private void ServerGetFacebookIncentive_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		global::Debug.Log("SettingPartsSnsLoging:ServerGetFacebookIncentive_Succeeded ");
		foreach (ServerPresentState serverPresentState in msg.m_incentive)
		{
			global::Debug.Log("SettingPartsSnsLoging:ServerGetFacebookIncentive_Succeeded m_incentive");
			IncentiveWindow window = new IncentiveWindow(serverPresentState.m_itemId, serverPresentState.m_numItem, this.m_anchorPath);
			this.m_windowQueue.AddWindow(window);
		}
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(103);
		if (this.m_fsm)
		{
			global::Debug.Log("SettingPartsSnsLoging:ServerGetFacebookIncentive_Succeeded INCENTIVE_CONNECT_END");
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x000FE068 File Offset: 0x000FC268
	private void LoginEndCallback(MsgSocialNormalResponse msg)
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			return;
		}
		TinyFsmEvent signal;
		if (socialInterface.IsLoggedIn)
		{
			HudMenuUtility.SetUpdateRankingFlag();
			signal = TinyFsmEvent.CreateUserEvent(101);
		}
		else
		{
			signal = TinyFsmEvent.CreateUserEvent(102);
		}
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0400245C RID: 9308
	private TinyFsmBehavior m_fsm;

	// Token: 0x0400245D RID: 9309
	private string m_anchorPath;

	// Token: 0x0400245E RID: 9310
	private bool m_isEndPlay;

	// Token: 0x0400245F RID: 9311
	private bool m_isCalceled;

	// Token: 0x04002460 RID: 9312
	private IncentiveWindowQueue m_windowQueue;

	// Token: 0x04002461 RID: 9313
	private SettingPartsSnsAdditional m_snsAdditional;

	// Token: 0x04002462 RID: 9314
	private bool m_cancelWindowUseFlag = true;

	// Token: 0x04002463 RID: 9315
	private bool m_isPlayStart;

	// Token: 0x0200052F RID: 1327
	private enum EventSignal
	{
		// Token: 0x04002465 RID: 9317
		PLAY_START = 100,
		// Token: 0x04002466 RID: 9318
		SNS_LOGIN_END,
		// Token: 0x04002467 RID: 9319
		SNS_LOGIN_FAILED,
		// Token: 0x04002468 RID: 9320
		INCENTIVE_CONNECT_END
	}
}
