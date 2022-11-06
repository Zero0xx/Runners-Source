using System;

// Token: 0x02000526 RID: 1318
public class SettingPartsInviteFriend : SettingBase
{
	// Token: 0x060028BF RID: 10431 RVA: 0x000FC148 File Offset: 0x000FA348
	private void Start()
	{
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x000FC14C File Offset: 0x000FA34C
	protected override void OnSetup(string anthorPath)
	{
		this.m_anchorPath = this.ExcludePathName;
		if (this.m_inviteFriend == null)
		{
			this.m_inviteFriend = base.gameObject.AddComponent<SettingPartsInviteFriendUI>();
		}
		if (this.m_login == null)
		{
			this.m_login = base.gameObject.AddComponent<SettingPartsSnsLogin>();
		}
		if (this.m_login != null)
		{
			this.m_login.Setup(this.m_anchorPath);
		}
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x000FC1CC File Offset: 0x000FA3CC
	protected override void OnPlayStart()
	{
		this.m_state = SettingPartsInviteFriend.State.STATE_WAIT;
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x000FC1D8 File Offset: 0x000FA3D8
	protected override bool OnIsEndPlay()
	{
		return !(this.m_inviteFriend != null) || this.m_inviteFriend.IsEndPlay();
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x000FC20C File Offset: 0x000FA40C
	protected override void OnUpdate()
	{
		switch (this.m_state)
		{
		case SettingPartsInviteFriend.State.STATE_WAIT:
			this.m_state = SettingPartsInviteFriend.State.STATE_LOGIN_SETUP;
			break;
		case SettingPartsInviteFriend.State.STATE_LOGIN_SETUP:
			if (this.m_login != null)
			{
				this.m_login.PlayStart();
			}
			this.m_state = SettingPartsInviteFriend.State.STATE_LOGIN;
			break;
		case SettingPartsInviteFriend.State.STATE_LOGIN:
			if (this.m_login != null && this.m_login.IsEnd)
			{
				this.m_inviteFriend.Setup(this.m_anchorPath);
				this.m_inviteFriend.PlayStart();
				this.m_state = SettingPartsInviteFriend.State.STATE_INVITE_FRIEND;
			}
			break;
		case SettingPartsInviteFriend.State.STATE_INVITE_FRIEND:
			if (this.m_inviteFriend.IsEndPlay())
			{
				this.m_state = SettingPartsInviteFriend.State.STATE_END;
			}
			break;
		}
	}

	// Token: 0x0400242C RID: 9260
	private SettingPartsInviteFriendUI m_inviteFriend;

	// Token: 0x0400242D RID: 9261
	private SettingPartsSnsLogin m_login;

	// Token: 0x0400242E RID: 9262
	private string m_anchorPath;

	// Token: 0x0400242F RID: 9263
	private readonly string ExcludePathName = "Camera/Anchor_5_MC";

	// Token: 0x04002430 RID: 9264
	private SettingPartsInviteFriend.State m_state;

	// Token: 0x02000527 RID: 1319
	private enum State
	{
		// Token: 0x04002432 RID: 9266
		STATE_IDLE,
		// Token: 0x04002433 RID: 9267
		STATE_WAIT,
		// Token: 0x04002434 RID: 9268
		STATE_LOGIN_SETUP,
		// Token: 0x04002435 RID: 9269
		STATE_LOGIN,
		// Token: 0x04002436 RID: 9270
		STATE_INVITE_FRIEND,
		// Token: 0x04002437 RID: 9271
		STATE_END
	}
}
