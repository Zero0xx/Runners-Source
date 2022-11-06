using System;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class FirstLaunchInviteFriend : MonoBehaviour
{
	// Token: 0x06001ECE RID: 7886 RVA: 0x000B74FC File Offset: 0x000B56FC
	public void Setup(string anchorPath)
	{
		this.m_anchorPath = anchorPath;
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x000B7508 File Offset: 0x000B5708
	public void PlayStart()
	{
		TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
		if (this.m_fsm != null)
		{
			this.m_fsm.Dispatch(signal);
		}
		this.m_isEndPlay = false;
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000B7544 File Offset: 0x000B5744
	// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x000B754C File Offset: 0x000B574C
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

	// Token: 0x06001ED2 RID: 7890 RVA: 0x000B7550 File Offset: 0x000B5750
	private void Start()
	{
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		description.initState = new TinyFsmState(new EventFunction(this.StateIdle));
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x000B75B0 File Offset: 0x000B57B0
	private void Update()
	{
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x000B75B4 File Offset: 0x000B57B4
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
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateInviteFriendWindow)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x000B762C File Offset: 0x000B582C
	private TinyFsmState StateInviteFriendWindow(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			info.caption = TextUtility.GetCommonText("FaceBook", "ui_Lbl_facebook_invite_friend_caption");
			int num = 5;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				ServerSettingState settingState = ServerInterface.SettingState;
				if (settingState != null)
				{
					num = settingState.m_invitBaseIncentive.m_num;
				}
			}
			string commonText = TextUtility.GetCommonText("FaceBook", "ui_Lbl_facebook_invite_friend_text", "{RED_STAR_RING_NUM}", num.ToString());
			info.message = commonText;
			info.anchor_path = this.m_anchorPath;
			info.buttonType = GeneralWindow.ButtonType.Ok;
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateDisplayTutorialCursor)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x000B7734 File Offset: 0x000B5934
	private TinyFsmState StateDisplayTutorialCursor(TinyFsmEvent e)
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
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEnd)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x000B77E0 File Offset: 0x000B59E0
	private TinyFsmState StateEnd(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.INVITE_FRIEND_SEQUENCE_END);
			this.m_isEndPlay = true;
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x04001C3D RID: 7229
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001C3E RID: 7230
	private float m_timer;

	// Token: 0x04001C3F RID: 7231
	private bool m_isEndPlay;

	// Token: 0x04001C40 RID: 7232
	private string m_anchorPath;

	// Token: 0x0200040A RID: 1034
	private enum EventSignal
	{
		// Token: 0x04001C42 RID: 7234
		PLAY_START = 100
	}
}
