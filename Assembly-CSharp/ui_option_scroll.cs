using System;
using Text;
using UnityEngine;

// Token: 0x020004B8 RID: 1208
public class ui_option_scroll : MonoBehaviour
{
	// Token: 0x060023D2 RID: 9170 RVA: 0x000D71AC File Offset: 0x000D53AC
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000D71B8 File Offset: 0x000D53B8
	public OptionUI.OptionInfo OptionInfo
	{
		get
		{
			return this.m_optionInfo;
		}
	}

	// Token: 0x060023D4 RID: 9172 RVA: 0x000D71C0 File Offset: 0x000D53C0
	public void UpdateView(OptionUI.OptionInfo info, OptionUI optionUI)
	{
		this.m_optionUI = optionUI;
		this.m_optionInfo = info;
		if (this.m_optionInfo != null)
		{
			if (this.m_imgSprite != null)
			{
				this.m_imgSprite.spriteName = this.m_optionInfo.icon;
			}
			if (this.m_textLabel != null)
			{
				TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Option", this.m_optionInfo.label);
				if (text != null)
				{
					this.m_textLabel.text = text.text;
				}
			}
		}
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x000D724C File Offset: 0x000D544C
	public void SetEnableImageButton(bool flag)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_option_top");
		if (gameObject != null)
		{
			UIImageButton component = gameObject.GetComponent<UIImageButton>();
			if (component != null)
			{
				component.isEnabled = flag;
			}
		}
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x000D7290 File Offset: 0x000D5490
	private void SetButtonTrigger(bool flag)
	{
		if (this.m_optionUI != null)
		{
			this.m_optionUI.SetButtonTrigger(flag);
		}
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x000D72B0 File Offset: 0x000D54B0
	private void OnClickOptionScroll()
	{
		if (BackKeyManager.MenuSequenceTransitionFlag)
		{
			return;
		}
		SoundManager.SePlay("sys_menu_decide", "SE");
		this.SetButtonTrigger(true);
		if (this.m_optionInfo != null)
		{
			switch (this.m_optionInfo.type)
			{
			case OptionType.USER_RESULT:
			{
				OptionUserResult optionUserResult = base.gameObject.GetComponent<OptionUserResult>();
				if (optionUserResult == null)
				{
					optionUserResult = base.gameObject.AddComponent<OptionUserResult>();
				}
				if (optionUserResult != null)
				{
					optionUserResult.Setup(this);
				}
				break;
			}
			case OptionType.BUYING_HISTORY:
			{
				BuyHistory buyHistory = base.gameObject.GetComponent<BuyHistory>();
				if (buyHistory == null)
				{
					buyHistory = base.gameObject.AddComponent<BuyHistory>();
				}
				if (buyHistory != null)
				{
					buyHistory.Setup(this);
				}
				break;
			}
			case OptionType.PUSH_NOTIFICATION:
			{
				OptionPushNotification optionPushNotification = base.gameObject.GetComponent<OptionPushNotification>();
				if (optionPushNotification == null)
				{
					optionPushNotification = base.gameObject.AddComponent<OptionPushNotification>();
				}
				if (optionPushNotification != null)
				{
					optionPushNotification.Setup(this);
				}
				break;
			}
			case OptionType.WEIGHT_SAVING:
			{
				WeightSaving weightSaving = base.gameObject.GetComponent<WeightSaving>();
				if (weightSaving == null)
				{
					weightSaving = base.gameObject.AddComponent<WeightSaving>();
				}
				if (weightSaving != null)
				{
					weightSaving.Setup(this);
				}
				break;
			}
			case OptionType.ID_CHECK:
			{
				CheckID checkID = base.gameObject.GetComponent<CheckID>();
				if (checkID == null)
				{
					checkID = base.gameObject.AddComponent<CheckID>();
				}
				if (checkID != null)
				{
					checkID.Setup(this);
				}
				break;
			}
			case OptionType.TUTORIAL:
			{
				OptionTutorial optionTutorial = base.gameObject.GetComponent<OptionTutorial>();
				if (optionTutorial == null)
				{
					optionTutorial = base.gameObject.AddComponent<OptionTutorial>();
				}
				if (optionTutorial != null)
				{
					optionTutorial.Setup(this);
				}
				break;
			}
			case OptionType.STAFF_CREDIT:
			{
				StaffRoll staffRoll = base.gameObject.GetComponent<StaffRoll>();
				if (staffRoll == null)
				{
					staffRoll = base.gameObject.AddComponent<StaffRoll>();
				}
				if (staffRoll != null)
				{
					staffRoll.SetTextType(StaffRoll.TextType.STAFF_ROLL);
					staffRoll.Setup(this);
				}
				break;
			}
			case OptionType.TERMS_OF_SERVICE:
			{
				OptionWebJump optionWebJump = base.gameObject.GetComponent<OptionWebJump>();
				if (optionWebJump == null)
				{
					optionWebJump = base.gameObject.AddComponent<OptionWebJump>();
				}
				if (optionWebJump != null)
				{
					optionWebJump.Setup(this, OptionWebJump.WebType.TERMS_OF_SERVICE);
				}
				break;
			}
			case OptionType.PAST_RESULTS:
				AchievementManager.RequestShowAchievementsUI();
				this.SetButtonTrigger(false);
				break;
			case OptionType.USER_NAME:
			{
				OptionUserName optionUserName = base.gameObject.GetComponent<OptionUserName>();
				if (optionUserName == null)
				{
					optionUserName = base.gameObject.AddComponent<OptionUserName>();
				}
				if (optionUserName != null)
				{
					optionUserName.Setup(this);
				}
				break;
			}
			case OptionType.SOUND_CONFIG:
			{
				SoundSetting soundSetting = base.gameObject.GetComponent<SoundSetting>();
				if (soundSetting == null)
				{
					soundSetting = base.gameObject.AddComponent<SoundSetting>();
				}
				if (soundSetting != null)
				{
					soundSetting.Setup(this);
				}
				break;
			}
			case OptionType.INVITE_FRIEND:
			{
				OptionFriendInvitation optionFriendInvitation = base.gameObject.GetComponent<OptionFriendInvitation>();
				if (optionFriendInvitation == null)
				{
					optionFriendInvitation = base.gameObject.AddComponent<OptionFriendInvitation>();
				}
				if (optionFriendInvitation != null)
				{
					optionFriendInvitation.Setup(this);
				}
				break;
			}
			case OptionType.ACCEPT_INVITE:
			{
				OptionAcceptInvite optionAcceptInvite = base.gameObject.GetComponent<OptionAcceptInvite>();
				if (optionAcceptInvite == null)
				{
					optionAcceptInvite = base.gameObject.AddComponent<OptionAcceptInvite>();
				}
				if (optionAcceptInvite != null)
				{
					optionAcceptInvite.Setup(this);
				}
				break;
			}
			case OptionType.FACEBOOK_ACCESS:
			{
				OptionFacebookAccess optionFacebookAccess = base.gameObject.GetComponent<OptionFacebookAccess>();
				if (optionFacebookAccess == null)
				{
					optionFacebookAccess = base.gameObject.AddComponent<OptionFacebookAccess>();
				}
				if (optionFacebookAccess != null)
				{
					optionFacebookAccess.Setup(this);
				}
				break;
			}
			case OptionType.HELP:
			{
				OptionWebJump optionWebJump2 = base.gameObject.GetComponent<OptionWebJump>();
				if (optionWebJump2 == null)
				{
					optionWebJump2 = base.gameObject.AddComponent<OptionWebJump>();
				}
				if (optionWebJump2 != null)
				{
					optionWebJump2.Setup(this, OptionWebJump.WebType.HELP);
				}
				break;
			}
			case OptionType.COPYRIGHT:
			{
				StaffRoll staffRoll2 = base.gameObject.GetComponent<StaffRoll>();
				if (staffRoll2 == null)
				{
					staffRoll2 = base.gameObject.AddComponent<StaffRoll>();
				}
				if (staffRoll2 != null)
				{
					staffRoll2.SetTextType(StaffRoll.TextType.COPYRIGHT);
					staffRoll2.Setup(this);
				}
				break;
			}
			case OptionType.CACHE_CLEAR:
			{
				OptionCacheClear optionCacheClear = base.gameObject.GetComponent<OptionCacheClear>();
				if (optionCacheClear == null)
				{
					optionCacheClear = base.gameObject.AddComponent<OptionCacheClear>();
				}
				if (optionCacheClear != null)
				{
					optionCacheClear.Setup(this);
				}
				break;
			}
			case OptionType.BACK_TITLE:
			{
				OptionBackTitle optionBackTitle = base.gameObject.GetComponent<OptionBackTitle>();
				if (optionBackTitle == null)
				{
					optionBackTitle = base.gameObject.AddComponent<OptionBackTitle>();
				}
				if (optionBackTitle != null)
				{
					optionBackTitle.Setup(this);
				}
				break;
			}
			}
		}
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x000D77AC File Offset: 0x000D59AC
	public void OnEndChildPage()
	{
		this.SetButtonTrigger(false);
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x000D77B8 File Offset: 0x000D59B8
	public void SetSystemSaveFlag()
	{
		if (this.m_optionUI != null)
		{
			this.m_optionUI.SystemSaveFlag = true;
		}
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x000D77D8 File Offset: 0x000D59D8
	public void ResetSystemSaveFlag()
	{
		if (this.m_optionUI != null)
		{
			this.m_optionUI.SystemSaveFlag = false;
		}
	}

	// Token: 0x04002088 RID: 8328
	[SerializeField]
	private UISprite m_imgSprite;

	// Token: 0x04002089 RID: 8329
	[SerializeField]
	private UILabel m_textLabel;

	// Token: 0x0400208A RID: 8330
	private OptionUI.OptionInfo m_optionInfo;

	// Token: 0x0400208B RID: 8331
	private OptionUI m_optionUI;
}
