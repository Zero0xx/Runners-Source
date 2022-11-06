using System;
using UnityEngine;

// Token: 0x02000498 RID: 1176
public class OptionFriendInvitation : MonoBehaviour
{
	// Token: 0x06002306 RID: 8966 RVA: 0x000D2678 File Offset: 0x000D0878
	public void Setup(ui_option_scroll scroll)
	{
		if (scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		this.m_loginFlag = false;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			this.m_loginFlag = socialInterface.IsLoggedIn;
		}
		if (this.m_loginFlag)
		{
			this.PlayInvite();
		}
		else
		{
			this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/Anchor_5_MC");
		}
		base.enabled = true;
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x000D26F8 File Offset: 0x000D08F8
	private void SetInvite()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			this.m_loginFlag = socialInterface.IsLoggedIn;
		}
		if (this.m_loginFlag)
		{
			if (this.m_inviteFriend != null)
			{
				this.m_inviteFriend.PlayStart();
			}
			else
			{
				this.m_inviteFriend = base.gameObject.AddComponent<SettingPartsInviteFriend>();
				if (this.m_inviteFriend != null)
				{
					this.m_inviteFriend.Setup("Camera/menu_Anim/OptionUI/Anchor_5_MC");
					this.m_inviteFriend.PlayStart();
				}
			}
		}
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x000D2794 File Offset: 0x000D0994
	private void PlayInvite()
	{
		if (this.m_inviteFriend != null)
		{
			this.m_inviteFriend.PlayStart();
		}
		else
		{
			this.m_inviteFriend = base.gameObject.AddComponent<SettingPartsInviteFriend>();
			if (this.m_inviteFriend != null)
			{
				this.m_inviteFriend.Setup("Camera/menu_Anim/OptionUI/Anchor_5_MC");
				this.m_inviteFriend.PlayStart();
			}
		}
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x000D2800 File Offset: 0x000D0A00
	public void Update()
	{
		if (this.m_loginFlag)
		{
			if (this.m_inviteFriend != null && this.m_inviteFriend.IsEndPlay())
			{
				if (this.m_ui_option_scroll != null)
				{
					this.m_ui_option_scroll.OnEndChildPage();
				}
				base.enabled = false;
			}
		}
		else if (this.m_easySnsFeed != null)
		{
			EasySnsFeed.Result result = this.m_easySnsFeed.Update();
			if (result != EasySnsFeed.Result.COMPLETED)
			{
				if (result == EasySnsFeed.Result.FAILED)
				{
					this.m_easySnsFeed = null;
					if (this.m_ui_option_scroll != null)
					{
						this.m_ui_option_scroll.OnEndChildPage();
					}
					base.enabled = false;
				}
			}
			else
			{
				this.SetInvite();
				this.m_easySnsFeed = null;
				if (!this.m_loginFlag)
				{
					if (this.m_ui_option_scroll != null)
					{
						this.m_ui_option_scroll.OnEndChildPage();
					}
					base.enabled = false;
				}
			}
		}
	}

	// Token: 0x04001FAF RID: 8111
	private const string ATTACH_ANTHOR_NAME = "Camera/menu_Anim/OptionUI/Anchor_5_MC";

	// Token: 0x04001FB0 RID: 8112
	private SettingPartsInviteFriend m_inviteFriend;

	// Token: 0x04001FB1 RID: 8113
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x04001FB2 RID: 8114
	private bool m_loginFlag;

	// Token: 0x04001FB3 RID: 8115
	private ui_option_scroll m_ui_option_scroll;
}
