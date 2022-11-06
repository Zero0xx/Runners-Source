using System;
using System.Collections.Generic;
using AnimationOrTween;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000522 RID: 1314
public class SettingPartsAcceptInvite : SettingBase
{
	// Token: 0x060028A5 RID: 10405 RVA: 0x000FB7F4 File Offset: 0x000F99F4
	private void Start()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x000FB804 File Offset: 0x000F9A04
	private void OnDestroy()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x000FB814 File Offset: 0x000F9A14
	protected override void OnSetup(string anthorPath)
	{
		this.m_anchorPath = this.ExcludePathName;
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x000FB824 File Offset: 0x000F9A24
	protected override void OnPlayStart()
	{
		this.m_isEnd = false;
		bool flag = true;
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface == null)
		{
			flag = false;
		}
		else if (!socialInterface.IsLoggedIn)
		{
			flag = false;
		}
		if (!flag)
		{
			this.m_isValid = false;
			this.m_isEnd = true;
			return;
		}
		socialInterface.RequestInvitedFriend(base.gameObject);
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x000FB888 File Offset: 0x000F9A88
	protected override bool OnIsEndPlay()
	{
		return this.m_isEnd;
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x000FB890 File Offset: 0x000F9A90
	protected override void OnUpdate()
	{
		if (!this.m_isValid)
		{
			return;
		}
		if (this.m_isEnd)
		{
			return;
		}
		switch (this.m_state)
		{
		case SettingPartsAcceptInvite.State.SETUP_BEFORE:
			if (this.m_isSetup)
			{
				this.m_state = SettingPartsAcceptInvite.State.WAIT_SETUP;
			}
			else
			{
				this.SetupWindowData();
				this.m_isSetup = true;
				this.m_state = SettingPartsAcceptInvite.State.WAIT_SETUP;
			}
			break;
		case SettingPartsAcceptInvite.State.WAIT_SETUP:
			this.m_state = SettingPartsAcceptInvite.State.SETUP;
			break;
		case SettingPartsAcceptInvite.State.SETUP:
		{
			if (this.m_itemStorage != null && this.m_msg != null)
			{
				this.m_itemStorage.maxRows = this.m_msg.m_friends.Count;
				this.m_itemStorage.Restart();
			}
			List<GameObject> list = GameObjectUtil.FindChildGameObjects(this.m_object, "ui_option_window_invite_scroll(Clone)");
			if (list == null)
			{
				return;
			}
			this.m_buttons.Clear();
			if (this.m_msg != null)
			{
				List<SocialUserData> friends = this.m_msg.m_friends;
				for (int i = 0; i < friends.Count; i++)
				{
					SocialUserData socialUserData = friends[i];
					if (socialUserData != null)
					{
						GameObject gameObject = list[i];
						if (!(gameObject == null))
						{
							SettingPartsInviteButton settingPartsInviteButton = gameObject.AddComponent<SettingPartsInviteButton>();
							settingPartsInviteButton.Setup(socialUserData, new SettingPartsInviteButton.ButtonPressedCallback(this.InviteButtonPressedCallback));
							this.m_buttons.Add(settingPartsInviteButton);
						}
					}
				}
			}
			if (this.m_object != null)
			{
				this.m_object.SetActive(true);
			}
			if (this.m_uiAnimation != null)
			{
				this.m_uiAnimation.Play(true);
			}
			SoundManager.SePlay("sys_window_open", "SE");
			this.m_state = SettingPartsAcceptInvite.State.UPDATE;
			break;
		}
		case SettingPartsAcceptInvite.State.DECIDE_FRIEND:
			if (GeneralWindow.IsYesButtonPressed)
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					string gameId = this.m_decidedFriendData.CustomData.GameId;
					loggedInServerInterface.RequestServerSetInviteCode(gameId, base.gameObject);
				}
				this.m_decidedFriendData = null;
				this.m_state = SettingPartsAcceptInvite.State.WAIT_SERVER_RESPONSE;
				GeneralWindow.Close();
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				this.m_decidedFriendData = null;
				this.m_state = SettingPartsAcceptInvite.State.UPDATE;
				GeneralWindow.Close();
			}
			break;
		case SettingPartsAcceptInvite.State.WAIT_END_WINDOW_CLOSE:
			if (GeneralWindow.IsCreated("CreateEndAcceptWindow") && GeneralWindow.IsOkButtonPressed)
			{
				GeneralWindow.Close();
				this.OnClickCancelButton();
				this.m_state = SettingPartsAcceptInvite.State.UPDATE;
			}
			break;
		}
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x000FBB2C File Offset: 0x000F9D2C
	protected void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_object != null && this.m_object.activeSelf && this.m_state == SettingPartsAcceptInvite.State.UPDATE)
		{
			if (msg != null)
			{
				msg.StaySequence();
			}
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
			if (gameObject != null)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x000FBBB0 File Offset: 0x000F9DB0
	private void OnClickCancelButton()
	{
		if (this.m_object != null)
		{
			Animation component = this.m_object.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(component, Direction.Reverse);
				if (activeAnimation != null)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), true);
				}
			}
		}
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x000FBC24 File Offset: 0x000F9E24
	private void InviteButtonPressedCallback(SocialUserData friendData)
	{
		GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
		info.buttonType = GeneralWindow.ButtonType.YesNo;
		TextManager.TextType type = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
		info.caption = TextUtility.GetText(type, "FaceBook", "ui_Lbl_verification");
		info.message = TextUtility.GetText(type, "FaceBook", "ui_Lbl_accept_invite_text", "{FRIEND_NAME}", friendData.Name);
		GeneralWindow.Create(info);
		this.m_decidedFriendData = friendData;
		this.m_state = SettingPartsAcceptInvite.State.DECIDE_FRIEND;
	}

	// Token: 0x060028AE RID: 10414 RVA: 0x000FBC94 File Offset: 0x000F9E94
	private void RequestInviteListEndCallback(MsgSocialFriendListResponse msg)
	{
		if (msg == null)
		{
			return;
		}
		if (msg.m_result.IsError)
		{
			this.m_isEnd = true;
			return;
		}
		this.m_msg = msg;
		this.m_state = SettingPartsAcceptInvite.State.SETUP_BEFORE;
	}

	// Token: 0x060028AF RID: 10415 RVA: 0x000FBCC4 File Offset: 0x000F9EC4
	private void ServerSetInviteCode_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		HudMenuUtility.SaveSystemDataFlagStatus(SystemData.FlagStatus.FRIEDN_ACCEPT_INVITE);
		this.CreateEndAcceptWindow();
		this.m_state = SettingPartsAcceptInvite.State.WAIT_END_WINDOW_CLOSE;
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x000FBCDC File Offset: 0x000F9EDC
	private void ServerSetInviteCode_Failed(MsgServerConnctFailed msg)
	{
		this.m_state = SettingPartsAcceptInvite.State.UPDATE;
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x000FBCE8 File Offset: 0x000F9EE8
	private void OnFinishedAnimationCallback()
	{
		this.m_isEnd = true;
		this.m_object.SetActive(false);
	}

	// Token: 0x060028B2 RID: 10418 RVA: 0x000FBD00 File Offset: 0x000F9F00
	private void SetupWindowData()
	{
		this.m_object = HudMenuUtility.GetLoadMenuChildObject("window_invite", true);
		if (this.m_object != null)
		{
			GameObject gameObject = GameObject.Find(this.m_anchorPath);
			if (gameObject != null)
			{
				Vector3 localPosition = this.m_object.transform.localPosition;
				Vector3 localScale = this.m_object.transform.localScale;
				this.m_object.transform.parent = gameObject.transform;
				this.m_object.transform.localPosition = localPosition;
				this.m_object.transform.localScale = localScale;
			}
			this.m_uiAnimation = base.gameObject.AddComponent<UIPlayAnimation>();
			if (this.m_uiAnimation != null)
			{
				Animation component = this.m_object.GetComponent<Animation>();
				this.m_uiAnimation.target = component;
				this.m_uiAnimation.clipName = "ui_menu_option_window_Anim";
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_object, "Btn_close");
			if (gameObject2 != null)
			{
				UIButtonMessage uibuttonMessage = gameObject2.AddComponent<UIButtonMessage>();
				uibuttonMessage.target = base.gameObject;
				uibuttonMessage.functionName = "OnClickCancelButton";
			}
			this.m_itemStorage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(this.m_object, "slot");
			if (this.m_itemStorage != null && this.m_msg != null)
			{
				this.m_itemStorage.maxRows = this.m_msg.m_friends.Count;
			}
			this.m_buttons = new List<SettingPartsInviteButton>();
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_invite");
			if (gameObject3 != null)
			{
				UILabel component2 = gameObject3.GetComponent<UILabel>();
				if (component2 != null)
				{
					TextUtility.SetCommonText(component2, "Option", "acceptance_of_invite");
				}
			}
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(this.m_object, "Lbl_invite_sub");
			if (gameObject4 != null)
			{
				UILabel component3 = gameObject4.GetComponent<UILabel>();
				if (component3 != null)
				{
					TextUtility.SetCommonText(component3, "Option", "acceptance_of_invite_info");
				}
			}
			UIPanel component4 = this.m_object.GetComponent<UIPanel>();
			if (component4 != null)
			{
				component4.alpha = 0f;
			}
			this.m_object.SetActive(true);
		}
	}

	// Token: 0x060028B3 RID: 10419 RVA: 0x000FBF44 File Offset: 0x000FA144
	private void CreateEndAcceptWindow()
	{
		GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
		info.buttonType = GeneralWindow.ButtonType.Ok;
		TextManager.TextType type = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
		info.caption = TextUtility.GetText(type, "FaceBook", "ui_Lbl_ask_accept_invite_caption");
		info.message = TextUtility.GetText(type, "FaceBook", "ui_Lbl_accept_invite_end_text");
		info.name = "CreateEndAcceptWindow";
		GeneralWindow.Create(info);
	}

	// Token: 0x04002413 RID: 9235
	private bool m_isValid = true;

	// Token: 0x04002414 RID: 9236
	private bool m_isEnd;

	// Token: 0x04002415 RID: 9237
	private bool m_isSetup;

	// Token: 0x04002416 RID: 9238
	private GameObject m_object;

	// Token: 0x04002417 RID: 9239
	private UIPlayAnimation m_uiAnimation;

	// Token: 0x04002418 RID: 9240
	private List<SettingPartsInviteButton> m_buttons;

	// Token: 0x04002419 RID: 9241
	private UIRectItemStorage m_itemStorage;

	// Token: 0x0400241A RID: 9242
	private string m_anchorPath;

	// Token: 0x0400241B RID: 9243
	private SettingPartsAcceptInvite.State m_state;

	// Token: 0x0400241C RID: 9244
	private SocialUserData m_decidedFriendData;

	// Token: 0x0400241D RID: 9245
	private MsgSocialFriendListResponse m_msg;

	// Token: 0x0400241E RID: 9246
	private readonly string ExcludePathName = "UI Root (2D)/Camera/Anchor_5_MC";

	// Token: 0x02000523 RID: 1315
	private enum State
	{
		// Token: 0x04002420 RID: 9248
		IDLE,
		// Token: 0x04002421 RID: 9249
		WAIT_SNS_RESPONSE,
		// Token: 0x04002422 RID: 9250
		SETUP_BEFORE,
		// Token: 0x04002423 RID: 9251
		WAIT_SETUP,
		// Token: 0x04002424 RID: 9252
		SETUP,
		// Token: 0x04002425 RID: 9253
		UPDATE,
		// Token: 0x04002426 RID: 9254
		DECIDE_FRIEND,
		// Token: 0x04002427 RID: 9255
		WAIT_SERVER_RESPONSE,
		// Token: 0x04002428 RID: 9256
		WAIT_END_WINDOW_CLOSE
	}
}
