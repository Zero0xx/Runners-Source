using System;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class OptionAcceptInvite : MonoBehaviour
{
	// Token: 0x060022F2 RID: 8946 RVA: 0x000D1CBC File Offset: 0x000CFEBC
	public void Setup(ui_option_scroll scroll)
	{
		if (scroll != null)
		{
			this.m_ui_option_scroll = scroll;
		}
		if (HudMenuUtility.IsSystemDataFlagStatus(SystemData.FlagStatus.FRIEDN_ACCEPT_INVITE))
		{
			this.m_acceptedFlag = true;
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "AcceptedInvite",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = TextUtility.GetCommonText("Option", "accepted_invite_caption"),
				message = TextUtility.GetCommonText("Option", "accepted_invite_text")
			});
		}
		else
		{
			this.m_loginFlag = false;
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null)
			{
				this.m_loginFlag = socialInterface.IsLoggedIn;
			}
			if (this.m_loginFlag)
			{
				this.PlayAcceptInvite();
			}
			else
			{
				this.m_easySnsFeed = new EasySnsFeed(base.gameObject, "Camera/Anchor_5_MC");
			}
		}
		base.enabled = true;
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x000D1DA0 File Offset: 0x000CFFA0
	private void SetAcceptInvite()
	{
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			this.m_loginFlag = socialInterface.IsLoggedIn;
		}
		if (this.m_loginFlag)
		{
			this.PlayAcceptInvite();
		}
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x000D1DE4 File Offset: 0x000CFFE4
	private void PlayAcceptInvite()
	{
		this.m_acceptInvite = base.gameObject.GetComponent<SettingPartsAcceptInvite>();
		if (this.m_acceptInvite == null)
		{
			this.m_acceptInvite = base.gameObject.AddComponent<SettingPartsAcceptInvite>();
			this.m_acceptInvite.Setup("UI Root (2D)/Camera/menu_Anim/OptionUI/Anchor_5_MC");
			this.m_acceptInvite.PlayStart();
		}
		else
		{
			this.m_acceptInvite.PlayStart();
		}
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x000D1E50 File Offset: 0x000D0050
	public void Update()
	{
		if (this.m_acceptedFlag)
		{
			if (GeneralWindow.IsCreated("AcceptedInvite") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				if (this.m_ui_option_scroll != null)
				{
					this.m_ui_option_scroll.OnEndChildPage();
				}
				base.enabled = false;
			}
		}
		else if (this.m_loginFlag)
		{
			if (this.m_acceptInvite != null && this.m_acceptInvite.IsEndPlay())
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
				this.SetAcceptInvite();
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

	// Token: 0x04001F99 RID: 8089
	private const string ATTACH_ANTHOR_NAME = "UI Root (2D)/Camera/menu_Anim/OptionUI/Anchor_5_MC";

	// Token: 0x04001F9A RID: 8090
	private SettingPartsAcceptInvite m_acceptInvite;

	// Token: 0x04001F9B RID: 8091
	private EasySnsFeed m_easySnsFeed;

	// Token: 0x04001F9C RID: 8092
	private bool m_loginFlag;

	// Token: 0x04001F9D RID: 8093
	private bool m_acceptedFlag;

	// Token: 0x04001F9E RID: 8094
	private ui_option_scroll m_ui_option_scroll;
}
