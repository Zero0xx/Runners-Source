using System;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class QuotaInfo
{
	// Token: 0x06001AD7 RID: 6871 RVA: 0x0009E5C8 File Offset: 0x0009C7C8
	public QuotaInfo(string caption, string quotaString, int serverRewardId, string reward, bool isCleared)
	{
		this.m_caption = caption;
		this.m_quotaString = quotaString;
		this.m_serverRewardId = serverRewardId;
		this.m_reward = reward;
		this.m_isCleared = isCleared;
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0009E604 File Offset: 0x0009C804
	public GameObject QuotaPlate
	{
		get
		{
			return this.m_quotaPlate;
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0009E60C File Offset: 0x0009C80C
	public string AnimClipName
	{
		get
		{
			return this.m_animClipName;
		}
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0009E614 File Offset: 0x0009C814
	public void Setup(GameObject quotaPlate, Animation animation, string animClipName)
	{
		this.m_quotaPlate = quotaPlate;
		this.m_animation = animation;
		this.m_animClipName = animClipName;
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0009E62C File Offset: 0x0009C82C
	public void SetupDisplay()
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)this.m_serverRewardId);
		bool flag = false;
		if (serverItem.idType == ServerItem.IdType.CHAO)
		{
			flag = true;
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_quotaPlate, "img_item_0");
		if (uisprite != null)
		{
			uisprite.gameObject.SetActive(!flag);
			uisprite.spriteName = PresentBoxUtility.GetItemSpriteName(serverItem);
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_quotaPlate, "texture_chao_0");
		if (uisprite2 != null)
		{
			uisprite2.gameObject.SetActive(false);
		}
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_quotaPlate, "texture_chao_1");
		if (uitexture != null)
		{
			uitexture.gameObject.SetActive(flag);
			if (flag && ChaoTextureManager.Instance != null)
			{
				uitexture.alpha = 1f;
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
				ChaoTextureManager.Instance.GetTexture(serverItem.chaoId, info);
			}
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_quotaPlate, "Lbl_event_object_total_num");
		if (uilabel != null)
		{
			uilabel.text = this.m_quotaString;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_quotaPlate, "Lbl_itemname");
		if (uilabel2 != null)
		{
			uilabel2.text = this.m_reward;
			UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_quotaPlate, "Lbl_itemname_sh");
			if (uilabel3 != null)
			{
				uilabel3.text = this.m_reward;
			}
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_quotaPlate, "Lbl_word_event_object_total");
		if (uilabel4 != null)
		{
			uilabel4.text = this.m_caption;
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_quotaPlate, "get_icon_Anim");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0009E7FC File Offset: 0x0009C9FC
	public void PlayStart()
	{
		this.m_state = QuotaInfo.State.IN;
		this.m_isPlayEnd = false;
		this.m_timer = 0f;
		this.SetupDisplay();
		if (string.IsNullOrEmpty(this.m_animClipName))
		{
			return;
		}
		if (this.m_animation == null)
		{
			return;
		}
		ActiveAnimation component = this.m_animation.gameObject.GetComponent<ActiveAnimation>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
		this.m_animation.enabled = true;
		this.m_animation.Rewind();
		this.m_animation.Play(this.m_animClipName);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_quotaPlate, "get_icon_Anim");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0009E8BC File Offset: 0x0009CABC
	public bool IsPlayEnd()
	{
		return this.m_isPlayEnd;
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0009E8CC File Offset: 0x0009CACC
	public virtual void Update()
	{
		switch (this.m_state)
		{
		case QuotaInfo.State.IN:
			if (this.m_animation != null && !this.m_animation.IsPlaying(this.m_animClipName))
			{
				if (this.m_isCleared)
				{
					Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_quotaPlate, "get_icon_Anim");
					if (animation != null)
					{
						animation.gameObject.SetActive(true);
						animation.Rewind();
						animation.Play("ui_Event_mission_getin_Anim");
						SoundManager.SePlay("sys_result_decide", "SE");
					}
					this.m_state = QuotaInfo.State.BONUS;
				}
				else
				{
					this.m_state = QuotaInfo.State.WAIT;
				}
			}
			break;
		case QuotaInfo.State.BONUS:
		{
			Animation animation2 = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_quotaPlate, "get_icon_Anim");
			if (animation2 != null && !animation2.IsPlaying("ui_Event_mission_getin_Anim"))
			{
				this.m_state = QuotaInfo.State.WAIT;
			}
			break;
		}
		case QuotaInfo.State.WAIT:
			this.m_timer += Time.deltaTime;
			if (this.m_timer >= QuotaInfo.WAIT_TIME)
			{
				this.m_state = QuotaInfo.State.END;
			}
			break;
		case QuotaInfo.State.END:
			this.m_isPlayEnd = true;
			this.m_state = QuotaInfo.State.IDLE;
			break;
		}
	}

	// Token: 0x04001830 RID: 6192
	private string m_caption;

	// Token: 0x04001831 RID: 6193
	private string m_quotaString;

	// Token: 0x04001832 RID: 6194
	private int m_serverRewardId;

	// Token: 0x04001833 RID: 6195
	private string m_reward;

	// Token: 0x04001834 RID: 6196
	private bool m_isCleared;

	// Token: 0x04001835 RID: 6197
	private GameObject m_quotaPlate;

	// Token: 0x04001836 RID: 6198
	private Animation m_animation;

	// Token: 0x04001837 RID: 6199
	private string m_animClipName;

	// Token: 0x04001838 RID: 6200
	private bool m_isPlayEnd;

	// Token: 0x04001839 RID: 6201
	private static readonly float WAIT_TIME = 0.5f;

	// Token: 0x0400183A RID: 6202
	private float m_timer;

	// Token: 0x0400183B RID: 6203
	private QuotaInfo.State m_state;

	// Token: 0x0200038B RID: 907
	private enum State
	{
		// Token: 0x0400183D RID: 6205
		IDLE,
		// Token: 0x0400183E RID: 6206
		IN,
		// Token: 0x0400183F RID: 6207
		BONUS,
		// Token: 0x04001840 RID: 6208
		WAIT,
		// Token: 0x04001841 RID: 6209
		END
	}
}
