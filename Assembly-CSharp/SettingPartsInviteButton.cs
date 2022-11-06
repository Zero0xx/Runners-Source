using System;
using Message;
using UnityEngine;

// Token: 0x02000524 RID: 1316
public class SettingPartsInviteButton : MonoBehaviour
{
	// Token: 0x060028B5 RID: 10421 RVA: 0x000FBFAC File Offset: 0x000FA1AC
	public void Setup(SocialUserData friendData, SettingPartsInviteButton.ButtonPressedCallback callback)
	{
		if (friendData == null)
		{
			return;
		}
		this.m_friendData = friendData;
		this.m_callback = callback;
		UITexture texture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_icon_friends");
		if (texture != null)
		{
			PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
			texture.mainTexture = playerImageManager.GetPlayerImage(this.m_friendData.Id, string.Empty, delegate(Texture2D _faceTexture)
			{
				texture.mainTexture = _faceTexture;
			});
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_friend_name");
		if (uilabel != null)
		{
			uilabel.text = this.m_friendData.Name;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_invite");
		if (gameObject != null)
		{
			UIButtonMessage uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickButton";
		}
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x000FC0A4 File Offset: 0x000FA2A4
	private void OnClickButton()
	{
		if (this.m_callback != null)
		{
			this.m_callback(this.m_friendData);
		}
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x000FC0C4 File Offset: 0x000FA2C4
	private void InviteFriendEndCallback(MsgSocialNormalResponse msg)
	{
	}

	// Token: 0x04002429 RID: 9257
	private SocialUserData m_friendData;

	// Token: 0x0400242A RID: 9258
	private SettingPartsInviteButton.ButtonPressedCallback m_callback;

	// Token: 0x02000A9D RID: 2717
	// (Invoke) Token: 0x060048AE RID: 18606
	public delegate void ButtonPressedCallback(SocialUserData friendData);
}
