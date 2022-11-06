using System;
using UnityEngine;

// Token: 0x020004E6 RID: 1254
public class ui_presentbox_scroll : MonoBehaviour
{
	// Token: 0x0600255E RID: 9566 RVA: 0x000E16D4 File Offset: 0x000DF8D4
	private void Start()
	{
		base.enabled = false;
		if (this.m_toggle != null)
		{
			EventDelegate.Add(this.m_toggle.onChange, new EventDelegate.Callback(this.OnChangeToggle));
		}
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x000E1718 File Offset: 0x000DF918
	private void OnDestroy()
	{
		this.ResetTextureData();
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x000E1720 File Offset: 0x000DF920
	public void UpdateView(PresentBoxUI.PresentInfo info)
	{
		this.m_persentInfo = info;
		this.m_friendFBId = string.Empty;
		if (this.m_persentInfo != null)
		{
			if (this.CheckTextureDisplay())
			{
				this.SetUITexture();
			}
			else
			{
				this.SetUISprite();
			}
			if (this.m_itemNameLabel != null)
			{
				string itemName = PresentBoxUtility.GetItemName(this.m_persentInfo.serverItem);
				this.m_itemNameLabel.text = itemName + " × " + this.m_persentInfo.itemNum.ToString();
			}
			if (this.m_infoLabel != null)
			{
				if (this.m_persentInfo.operatorFlag)
				{
					this.m_infoLabel.text = this.m_persentInfo.infoText;
				}
				else
				{
					this.m_infoLabel.text = PresentBoxUtility.GetItemInfo(this.m_persentInfo);
				}
			}
			if (this.m_receivedTimeLabel != null)
			{
				this.m_receivedTimeLabel.text = PresentBoxUtility.GetReceivedTime(this.m_persentInfo.expireTime);
			}
			this.SetCheckFlag(this.m_persentInfo.checkFlag);
		}
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x000E1840 File Offset: 0x000DFA40
	public void ResetTextureData()
	{
		if (this.m_imgTex != null)
		{
			UITexture component = this.m_imgTex.GetComponent<UITexture>();
			if (component != null && component.mainTexture != null)
			{
				component.mainTexture = null;
			}
		}
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x000E1890 File Offset: 0x000DFA90
	public bool IsCheck()
	{
		return this.m_check_flag;
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x000E1898 File Offset: 0x000DFA98
	private void SetCheckFlag(bool check_flag)
	{
		if (this.m_toggle != null)
		{
			this.m_toggle.value = check_flag;
		}
		if (this.m_persentInfo != null)
		{
			this.m_persentInfo.checkFlag = check_flag;
		}
		this.m_check_flag = check_flag;
		this.m_se_skip_flag = true;
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x000E18E8 File Offset: 0x000DFAE8
	private void SetUITexture()
	{
		if (this.m_persentInfo != null)
		{
			this.SetUISprite(this.m_imgChara, false, string.Empty);
			this.SetUISprite(this.m_imgItem, false, string.Empty);
			if (this.m_imgChao != null)
			{
				this.m_imgChao.SetActive(false);
			}
			if (this.m_imgTex != null)
			{
				this.m_imgTex.SetActive(true);
				UITexture uiTex = this.m_imgTex.GetComponent<UITexture>();
				if (uiTex != null)
				{
					uiTex.enabled = true;
					PlayerImageManager playerImageManager = GameObjectUtil.FindGameObjectComponent<PlayerImageManager>("PlayerImageManager");
					if (playerImageManager != null)
					{
						uiTex.mainTexture = playerImageManager.GetPlayerImage(this.m_friendFBId, string.Empty, delegate(Texture2D _faceTexture)
						{
							uiTex.mainTexture = _faceTexture;
						});
					}
				}
			}
		}
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x000E19D4 File Offset: 0x000DFBD4
	private void SetUISprite()
	{
		if (this.m_imgTex != null)
		{
			this.m_imgTex.SetActive(false);
		}
		if (this.m_persentInfo != null)
		{
			if (this.m_persentInfo.serverItem.idType == ServerItem.IdType.CHARA)
			{
				CharaType charaType = this.m_persentInfo.serverItem.charaType;
				string str = "ui_tex_player_set_";
				int num = (int)charaType;
				string spriteName = str + num.ToString("00") + "_" + CharaName.PrefixName[(int)charaType];
				this.SetUISprite(this.m_imgChara, true, spriteName);
				this.SetUISprite(this.m_imgItem, false, string.Empty);
				if (this.m_imgChao != null)
				{
					this.m_imgChao.SetActive(false);
				}
			}
			else if (this.m_persentInfo.serverItem.idType == ServerItem.IdType.CHAO)
			{
				this.SetUISprite(this.m_imgChara, false, string.Empty);
				this.SetUISprite(this.m_imgChao, true, "ui_tex_chao_" + this.m_persentInfo.serverItem.chaoId.ToString("D4"));
				this.SetUISprite(this.m_imgItem, false, string.Empty);
				if (this.m_imgChao != null)
				{
					this.m_imgChao.SetActive(true);
					UITexture component = this.m_imgChao.GetComponent<UITexture>();
					int chaoId = this.m_persentInfo.serverItem.chaoId;
					ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(component, null, true);
					ChaoTextureManager.Instance.GetTexture(chaoId, info);
				}
			}
			else
			{
				this.SetUISprite(this.m_imgChara, false, string.Empty);
				this.SetUISprite(this.m_imgItem, true, PresentBoxUtility.GetItemSpriteName(this.m_persentInfo.serverItem));
				if (this.m_imgChao != null)
				{
					this.m_imgChao.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x000E1BAC File Offset: 0x000DFDAC
	private void SetUISprite(GameObject obj, bool on, string spriteName = "")
	{
		if (obj != null)
		{
			obj.SetActive(on);
			if (on)
			{
				UISprite component = obj.GetComponent<UISprite>();
				if (component != null)
				{
					component.spriteName = spriteName;
				}
			}
		}
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x000E1BEC File Offset: 0x000DFDEC
	private void SetSocialInterface()
	{
		if (this.m_socialInterface == null)
		{
			this.m_socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		}
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x000E1C10 File Offset: 0x000DFE10
	private bool CheckTextureDisplay()
	{
		if (this.m_persentInfo != null && this.m_persentInfo.messageType == ServerMessageEntry.MessageType.SendEnergy)
		{
			this.SetSocialInterface();
			if (this.m_socialInterface != null)
			{
				SocialUserData socialUserDataFromGameId = SocialInterface.GetSocialUserDataFromGameId(this.m_socialInterface.FriendList, this.m_persentInfo.fromId);
				if (socialUserDataFromGameId != null)
				{
					this.m_friendFBId = socialUserDataFromGameId.Id;
					return !socialUserDataFromGameId.IsSilhouette;
				}
			}
		}
		return false;
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000E1C8C File Offset: 0x000DFE8C
	private void OnChangeToggle()
	{
		if (this.m_toggle != null)
		{
			this.m_check_flag = this.m_toggle.value;
			if (this.m_persentInfo != null)
			{
				this.m_persentInfo.checkFlag = this.m_check_flag;
			}
		}
		if (!this.m_se_skip_flag)
		{
			if (this.m_check_flag)
			{
				SoundManager.SePlay("sys_menu_decide", "SE");
			}
			else
			{
				SoundManager.SePlay("sys_window_close", "SE");
			}
		}
		this.m_se_skip_flag = false;
	}

	// Token: 0x04002187 RID: 8583
	[SerializeField]
	private UIToggle m_toggle;

	// Token: 0x04002188 RID: 8584
	[SerializeField]
	private GameObject m_imgChara;

	// Token: 0x04002189 RID: 8585
	[SerializeField]
	private GameObject m_imgChao;

	// Token: 0x0400218A RID: 8586
	[SerializeField]
	private GameObject m_imgItem;

	// Token: 0x0400218B RID: 8587
	[SerializeField]
	private GameObject m_imgTex;

	// Token: 0x0400218C RID: 8588
	[SerializeField]
	private UILabel m_infoLabel;

	// Token: 0x0400218D RID: 8589
	[SerializeField]
	private UILabel m_itemNameLabel;

	// Token: 0x0400218E RID: 8590
	[SerializeField]
	private UILabel m_receivedTimeLabel;

	// Token: 0x0400218F RID: 8591
	private PresentBoxUI.PresentInfo m_persentInfo;

	// Token: 0x04002190 RID: 8592
	private SocialInterface m_socialInterface;

	// Token: 0x04002191 RID: 8593
	private string m_friendFBId = string.Empty;

	// Token: 0x04002192 RID: 8594
	private bool m_check_flag;

	// Token: 0x04002193 RID: 8595
	private bool m_se_skip_flag;
}
