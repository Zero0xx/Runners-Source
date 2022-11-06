using System;
using System.Text.RegularExpressions;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class SettingTakeoverPassword : SettingBase
{
	// Token: 0x0600295B RID: 10587 RVA: 0x000FF4E4 File Offset: 0x000FD6E4
	public void SetCancelButtonUseFlag(bool useFlag)
	{
		this.m_calcelButtonUseFlag = useFlag;
	}

	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x0600295C RID: 10588 RVA: 0x000FF4F0 File Offset: 0x000FD6F0
	public bool isCancel
	{
		get
		{
			return this.m_isCancelEnd;
		}
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x000FF4F8 File Offset: 0x000FD6F8
	private void Start()
	{
		SettingPartsTakeoverPassword settingPartsTakeoverPassword = base.gameObject.AddComponent<SettingPartsTakeoverPassword>();
		settingPartsTakeoverPassword.SetCancelButtonUseFlag(this.m_calcelButtonUseFlag);
		settingPartsTakeoverPassword.Setup(this.m_anthorPath);
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
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x000FF5B4 File Offset: 0x000FD7B4
	private void OnDestroy()
	{
		SettingPartsTakeoverPassword component = base.gameObject.GetComponent<SettingPartsTakeoverPassword>();
		if (component != null)
		{
			component.SetOkButtonEnabled(true);
			UnityEngine.Object.Destroy(component);
		}
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x000FF5E8 File Offset: 0x000FD7E8
	protected override void OnSetup(string anthorPath)
	{
		this.m_anthorPath = anthorPath;
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x000FF5F4 File Offset: 0x000FD7F4
	protected override void OnPlayStart()
	{
		if (this.m_fsm != null)
		{
			if (this.m_isPlayStarted)
			{
				this.m_isEnd = false;
				this.m_isCancelEnd = false;
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

	// Token: 0x06002961 RID: 10593 RVA: 0x000FF67C File Offset: 0x000FD87C
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x000FF684 File Offset: 0x000FD884
	protected override void OnUpdate()
	{
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x000FF688 File Offset: 0x000FD888
	private TinyFsmState StateWaitStart(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_isEnd = false;
			this.m_isCancelEnd = false;
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateUserNameInput)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000FF710 File Offset: 0x000FD910
	private TinyFsmState StateUserNameInput(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			SettingPartsTakeoverPassword component = base.gameObject.GetComponent<SettingPartsTakeoverPassword>();
			if (component != null)
			{
				component.PlayStart();
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			SettingPartsTakeoverPassword component2 = base.gameObject.GetComponent<SettingPartsTakeoverPassword>();
			if (component2 != null)
			{
				if (component2.IsEndPlay())
				{
					if (component2.IsDecided)
					{
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckError)));
					}
					else if (component2.IsCanceled)
					{
						this.m_isCancelEnd = true;
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
					}
				}
				else
				{
					bool okButtonEnabled = this.CheckPassword(component2.InputText);
					component2.SetOkButtonEnabled(okButtonEnabled);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x000FF814 File Offset: 0x000FDA14
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
		{
			SettingPartsTakeoverPassword component = base.gameObject.GetComponent<SettingPartsTakeoverPassword>();
			string inputText = component.InputText;
			if (inputText.Length > this.MaxInputLength)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorOverFlow)));
			}
			else if (inputText.Length == 0)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorNoInput)));
			}
			else if (inputText.Length < this.MinInputLength)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInputErrorOverFlow)));
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateRegisterPassword)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x000FF924 File Offset: 0x000FDB24
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
				caption = TextUtility.GetCommonText("Option", "take_over_password_input_error"),
				message = TextUtility.GetCommonText("Option", "take_over_password_input_error_info"),
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

	// Token: 0x06002967 RID: 10599 RVA: 0x000FF9E0 File Offset: 0x000FDBE0
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
				caption = TextUtility.GetCommonText("Option", "take_over_password_input_error"),
				message = TextUtility.GetCommonText("Option", "take_over_password_input_error_info"),
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

	// Token: 0x06002968 RID: 10600 RVA: 0x000FFA9C File Offset: 0x000FDC9C
	private TinyFsmState StateRegisterPassword(TinyFsmEvent e)
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
				SettingPartsTakeoverPassword component = base.gameObject.GetComponent<SettingPartsTakeoverPassword>();
				string inputText = component.InputText;
				string userPassword = NetUtil.CalcMD5String(inputText);
				loggedInServerInterface.RequestServerGetMigrationPassword(userPassword, base.gameObject);
				SystemSaveManager.SetTakeoverPassword(inputText);
			}
			else
			{
				this.ServerGetMigrationPassword_Succeeded(null);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isEndConnect)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x000FFB68 File Offset: 0x000FDD68
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.m_isEnd = true;
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x000FFBBC File Offset: 0x000FDDBC
	private bool CheckPassword(string password)
	{
		bool result;
		if (password.Length > this.MaxInputLength)
		{
			result = false;
		}
		else if (password.Length == 0)
		{
			result = false;
		}
		else if (password.Length < this.MinInputLength)
		{
			result = false;
		}
		else
		{
			result = false;
			string text = "[a-zA-Z0-9]";
			for (int i = 1; i < password.Length; i++)
			{
				text += "[a-zA-Z0-9]";
			}
			if (Regex.IsMatch(password, text))
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x000FFC48 File Offset: 0x000FDE48
	private void ServerGetMigrationPassword_Succeeded(MsgGetMigrationPasswordSucceed msg)
	{
		this.m_isEndConnect = true;
		if (msg != null)
		{
			SystemSaveManager.SetTakeoverID(msg.m_migrationPassword);
			SystemSaveManager.Instance.SaveSystemData();
		}
	}

	// Token: 0x040024AF RID: 9391
	private readonly int MaxInputLength = 10;

	// Token: 0x040024B0 RID: 9392
	private readonly int MinInputLength = 6;

	// Token: 0x040024B1 RID: 9393
	private TinyFsmBehavior m_fsm;

	// Token: 0x040024B2 RID: 9394
	private bool m_isEndConnect;

	// Token: 0x040024B3 RID: 9395
	private bool m_isEnd;

	// Token: 0x040024B4 RID: 9396
	private bool m_isCancelEnd;

	// Token: 0x040024B5 RID: 9397
	private string m_anthorPath = string.Empty;

	// Token: 0x040024B6 RID: 9398
	private bool m_requestStart;

	// Token: 0x040024B7 RID: 9399
	private bool m_calcelButtonUseFlag = true;

	// Token: 0x040024B8 RID: 9400
	private bool m_isPlayStarted;

	// Token: 0x0200053E RID: 1342
	protected enum EventSignal
	{
		// Token: 0x040024BA RID: 9402
		PLAY_START = 100
	}
}
