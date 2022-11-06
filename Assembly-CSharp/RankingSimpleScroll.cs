using System;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public class RankingSimpleScroll : MonoBehaviour
{
	// Token: 0x060025EE RID: 9710 RVA: 0x000E6770 File Offset: 0x000E4970
	private void Start()
	{
		PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
		if (playerImageManager != null)
		{
			this.m_defaultImageHash = playerImageManager.GetDefaultImage().GetHashCode();
		}
		this.m_nameLable = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_username");
		this.m_toggle = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(base.gameObject, "Btn_ranking_top");
		this.m_texture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(base.gameObject, "img_tex_icon_friends");
	}

	// Token: 0x060025EF RID: 9711 RVA: 0x000E67E8 File Offset: 0x000E49E8
	public void SetUserData(SocialUserData user)
	{
		this.m_userData = user;
		this.m_id = user.Id;
		this.m_nameLable.text = user.Name;
	}

	// Token: 0x060025F0 RID: 9712 RVA: 0x000E681C File Offset: 0x000E4A1C
	public void LoadImage()
	{
		PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
		if (playerImageManager != null)
		{
			Texture2D playerImage = playerImageManager.GetPlayerImage(this.m_userData.Id, this.m_userData.Url, delegate(Texture2D _faceTexture)
			{
				if (_faceTexture.GetHashCode() != this.m_defaultImageHash && this.m_texture.mainTexture.GetHashCode() != _faceTexture.GetHashCode())
				{
					this.m_texture.mainTexture = _faceTexture;
				}
			});
			if (this.m_texture.mainTexture.GetHashCode() != playerImage.GetHashCode())
			{
				this.m_texture.mainTexture = playerImage;
			}
		}
	}

	// Token: 0x060025F1 RID: 9713 RVA: 0x000E6890 File Offset: 0x000E4A90
	public void OnClickRankingScroll()
	{
		RankingFriendOptionWindow rankingFriendOptionWindow = GameObjectUtil.FindGameObjectComponent<RankingFriendOptionWindow>("RankingFriendOptionWindow");
		if (rankingFriendOptionWindow != null)
		{
			rankingFriendOptionWindow.ChooseFriend(this.m_userData, this.m_toggle);
		}
	}

	// Token: 0x04002240 RID: 8768
	private UILabel m_nameLable;

	// Token: 0x04002241 RID: 8769
	private string m_id;

	// Token: 0x04002242 RID: 8770
	public UIToggle m_toggle;

	// Token: 0x04002243 RID: 8771
	private UITexture m_texture;

	// Token: 0x04002244 RID: 8772
	private SocialUserData m_userData;

	// Token: 0x04002245 RID: 8773
	private int m_defaultImageHash;
}
