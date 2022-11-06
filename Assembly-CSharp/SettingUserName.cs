using System;
using Message;
using Text;
using UnityEngine;

// Token: 0x0200053F RID: 1343
public class SettingUserName : SettingBase
{
	// Token: 0x0600296D RID: 10605 RVA: 0x000FFC94 File Offset: 0x000FDE94
	public void SetCancelButtonUseFlag(bool useFlag)
	{
		this.m_calcelButtonUseFlag = useFlag;
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x000FFCA0 File Offset: 0x000FDEA0
	private void Start()
	{
		SettingPartsUserName settingPartsUserName = base.gameObject.AddComponent<SettingPartsUserName>();
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm != null)
		{
			TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
			description.initState = new TinyFsmState(new EventFunction(this.StateWaitStart));
			description.onFixedUpdate = true;
			this.m_fsm.SetUp(description);
			if (this.m_requestStart)
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
				this.m_fsm.Dispatch(signal);
				this.m_requestStart = false;
			}
		}
		this.m_sendApolloFlag = FirstLaunchUserName.IsFirstLaunch;
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x000FFD4C File Offset: 0x000FDF4C
	private void OnDestroy()
	{
		SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
		NGWordCheck.ResetData();
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x000FFD7C File Offset: 0x000FDF7C
	protected override void OnSetup(string anthorPath)
	{
		this.m_anthorPath = anthorPath;
		SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
		if (component != null)
		{
			component.SetCancelButtonUseFlag(this.m_calcelButtonUseFlag);
			component.Setup(this.m_anthorPath);
		}
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x000FFDC0 File Offset: 0x000FDFC0
	protected override void OnPlayStart()
	{
		base.gameObject.SetActive(true);
		if (this.m_fsm != null)
		{
			if (this.m_isPlayStarted)
			{
				this.m_isEnd = false;
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			else
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
				this.m_fsm.Dispatch(signal);
				this.m_isPlayStarted = true;
			}
		}
		else
		{
			this.m_isPlayStarted = true;
			this.m_requestStart = true;
		}
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000FFE4C File Offset: 0x000FE04C
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x000FFE54 File Offset: 0x000FE054
	protected override void OnUpdate()
	{
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x000FFE58 File Offset: 0x000FE058
	private TinyFsmState StateWaitStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_isEnd = false;
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			if (this.m_sendApolloFlag)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloStart)));
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x000FFF04 File Offset: 0x000FE104
	private TinyFsmState StateSendApolloStart(TinyFsmEvent e)
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
		{
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP2, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_START, value);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000FFFDC File Offset: 0x000FE1DC
	private TinyFsmState StateUserNameInput(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
			if (component != null)
			{
				component.PlayStart();
				NGWordCheck.Load();
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			SettingPartsUserName component2 = base.gameObject.GetComponent<SettingPartsUserName>();
			if (component2 != null && component2.IsEndPlay())
			{
				if (component2.IsDecided)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckError)));
				}
				else if (component2.IsCanceled)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x001000C4 File Offset: 0x000FE2C4
	private TinyFsmState StateCheckError(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			if (NGWordCheck.IsLoaded())
			{
				SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
				string inputText = component.InputText;
				if (inputText.Length > this.MaxInputLength)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorOverFlow)));
				}
				else if (inputText.Length == 0)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorNoInput)));
				}
				else if (NGWordCheck.Check(inputText, component.TextLabel) != null)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorNGWord)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskToDecideName)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x001001E0 File Offset: 0x000FE3E0
	private TinyFsmState StateInputErrorOverFlow(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("UserName", "input_error"),
				message = TextUtility.GetCommonText("UserName", "input_error_info_1"),
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x0010029C File Offset: 0x000FE49C
	private TinyFsmState StateInputErrorNoInput(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("UserName", "input_error"),
				message = TextUtility.GetCommonText("UserName", "input_error_info_2"),
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x00100358 File Offset: 0x000FE558
	private TinyFsmState StateInputErrorNGWord(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				caption = TextUtility.GetCommonText("UserName", "input_error"),
				message = TextUtility.GetCommonText("UserName", "input_error_info_ng_word"),
				buttonType = GeneralWindow.ButtonType.Ok
			});
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x00100414 File Offset: 0x000FE614
	private TinyFsmState StateAskToDecideName(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
			string inputText = component.InputText;
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.caption = TextUtility.GetCommonText("UserName", "entry_user");
			string tag = "{NAME}";
			info.message = TextUtility.GetCommonText("UserName", "entry_user_info", tag, inputText);
			info.buttonType = GeneralWindow.ButtonType.YesNo;
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateRegisterUser)));
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x00100520 File Offset: 0x000FE720
	private TinyFsmState StateRegisterUser(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_isEndConnect = false;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
				string inputText = component.InputText;
				if (ServerInterface.SettingState.m_userName != inputText)
				{
					loggedInServerInterface.RequestServerSetUserName(inputText, base.gameObject);
				}
				else
				{
					this.ServerSetUserName_Succeeded(null);
				}
			}
			else
			{
				this.ServerSetUserName_Succeeded(null);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isEndConnect)
			{
				if (this.m_sendApolloFlag)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSendApolloEnd)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFinishedRegisterUser)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x00100628 File Offset: 0x000FE828
	private TinyFsmState StateSendApolloEnd(TinyFsmEvent e)
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
		{
			string[] value = new string[1];
			SendApollo.GetTutorialValue(ApolloTutorialIndex.START_STEP2, ref value);
			this.m_sendApollo = SendApollo.CreateRequest(ApolloType.TUTORIAL_END, value);
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_sendApollo != null && this.m_sendApollo.IsEnd())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFinishedRegisterUser)));
			}
			return TinyFsmState.End();
		case 5:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297E RID: 10622 RVA: 0x00100704 File Offset: 0x000FE904
	private TinyFsmState StateFinishedRegisterUser(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			SettingPartsUserName component = base.gameObject.GetComponent<SettingPartsUserName>();
			string inputText = component.InputText;
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.caption = TextUtility.GetCommonText("UserName", "end_entry_user");
			string tag = "{NAME}";
			info.message = TextUtility.GetCommonText("UserName", "end_entry_user_info", tag, inputText);
			info.buttonType = GeneralWindow.ButtonType.Ok;
			GeneralWindow.Create(info);
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				ServerInterface.SettingState.m_userName = inputText;
			}
			HudMenuUtility.SendMsgUpdateSaveDataDisplay();
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600297F RID: 10623 RVA: 0x00100804 File Offset: 0x000FEA04
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_isEnd = true;
			base.gameObject.SetActive(false);
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002980 RID: 10624 RVA: 0x00100864 File Offset: 0x000FEA64
	private void ServerSetUserName_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		if (msg != null)
		{
			HudMenuUtility.SetUpdateRankingFlag();
		}
		this.m_isEndConnect = true;
	}

	// Token: 0x040024BB RID: 9403
	private readonly int MaxInputLength = 8;

	// Token: 0x040024BC RID: 9404
	private TinyFsmBehavior m_fsm;

	// Token: 0x040024BD RID: 9405
	private bool m_isEndConnect;

	// Token: 0x040024BE RID: 9406
	private bool m_isEnd;

	// Token: 0x040024BF RID: 9407
	private string m_anthorPath = string.Empty;

	// Token: 0x040024C0 RID: 9408
	private bool m_requestStart;

	// Token: 0x040024C1 RID: 9409
	private bool m_calcelButtonUseFlag = true;

	// Token: 0x040024C2 RID: 9410
	private bool m_isPlayStarted;

	// Token: 0x040024C3 RID: 9411
	private bool m_sendApolloFlag;

	// Token: 0x040024C4 RID: 9412
	private SendApollo m_sendApollo;

	// Token: 0x02000540 RID: 1344
	protected enum EventSignal
	{
		// Token: 0x040024C6 RID: 9414
		PLAY_START = 100
	}
}
