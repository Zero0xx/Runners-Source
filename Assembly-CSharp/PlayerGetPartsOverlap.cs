using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class PlayerGetPartsOverlap : ChaoGetPartsBase
{
	// Token: 0x06001949 RID: 6473 RVA: 0x000925E4 File Offset: 0x000907E4
	public void Init(int severId, int rarity, int level, Dictionary<int, ServerItemState> itemList, PlayerGetPartsOverlap.IntroType introType = PlayerGetPartsOverlap.IntroType.NORMAL)
	{
		this.m_playSeCount = 0;
		this.m_severId = severId;
		this.m_rarity = rarity;
		this.m_level = level;
		this.m_introType = introType;
		this.m_itemListNum = null;
		this.m_itemList = null;
		if (itemList != null && itemList.Count > 0)
		{
			this.m_itemListNum = new List<int>();
			this.m_itemList = new List<ServerItem>();
			Dictionary<int, ServerItemState>.KeyCollection keys = itemList.Keys;
			foreach (int num in keys)
			{
				ServerItem item = new ServerItem((ServerItem.Id)num);
				this.m_itemList.Add(item);
				this.m_itemListNum.Add(itemList[num].m_num);
			}
		}
		this.m_currentAnimCount = 0;
		this.m_info = CharacterDataNameInfo.Instance.GetDataByServerID(this.m_severId);
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x000926EC File Offset: 0x000908EC
	public override void Setup(GameObject chaoGetObjectRoot)
	{
		this.m_rootGameObject = chaoGetObjectRoot;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(chaoGetObjectRoot, "eff_set");
		gameObject.SetActive(true);
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_0");
		if (uisprite != null)
		{
			string spriteName = "ui_roulette_egg_" + (2 * this.m_rarity).ToString();
			uisprite.spriteName = spriteName;
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_1");
		if (uisprite2 != null)
		{
			string spriteName2 = "ui_roulette_egg_" + (2 * this.m_rarity + 1).ToString();
			uisprite2.spriteName = spriteName2;
		}
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(chaoGetObjectRoot, "img_player");
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_item");
		if (uitexture != null && this.m_info != null)
		{
			uitexture.gameObject.SetActive(true);
			TextureRequestChara request = new TextureRequestChara(this.m_info.m_ID, uitexture);
			TextureAsyncLoadManager.Instance.Request(request);
		}
		if (uisprite3 != null)
		{
			uisprite3.gameObject.SetActive(false);
		}
		UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_player_speacies");
		if (uisprite4 != null && this.m_info != null)
		{
			uisprite4.gameObject.SetActive(true);
			switch (this.m_info.m_attribute)
			{
			case CharacterAttribute.SPEED:
				uisprite4.spriteName = "ui_mm_player_species_0";
				break;
			case CharacterAttribute.FLY:
				uisprite4.spriteName = "ui_mm_player_species_1";
				break;
			case CharacterAttribute.POWER:
				uisprite4.spriteName = "ui_mm_player_species_2";
				break;
			}
		}
		UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_player_genus");
		if (uisprite5 != null && this.m_info != null)
		{
			uisprite5.gameObject.SetActive(true);
			uisprite5.spriteName = HudUtility.GetTeamAttributeSpriteName(this.m_info.m_ID);
		}
		this.m_caption = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_caption");
		if (this.m_caption != null)
		{
			this.m_caption.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "PlayerSet", "ui_Lbt_get_captions").text;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_name");
		if (uilabel != null && this.m_info != null)
		{
			uilabel.gameObject.SetActive(true);
			uilabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", this.m_info.m_name.ToLower()).text;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_lv");
		if (uilabel2 != null && this.m_info != null)
		{
			uilabel2.gameObject.SetActive(true);
			uilabel2.text = string.Format("Lv.{0:D3}", this.m_level);
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_attribute");
		if (uilabel3 != null && this.m_info != null)
		{
			uilabel3.gameObject.SetActive(true);
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_info.m_ID);
				uilabel3.text = serverCharacterState.GetCharaAttributeText();
			}
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_item_name");
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_item_info");
		UILabel uilabel6 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_item_number");
		if (uilabel4 != null)
		{
			uilabel4.gameObject.SetActive(false);
		}
		if (uilabel5 != null)
		{
			uilabel5.gameObject.SetActive(false);
		}
		if (uilabel6 != null)
		{
			uilabel6.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x00092A9C File Offset: 0x00090C9C
	private void SetupItem()
	{
		ServerItem serverItem = this.m_itemList[this.m_currentAnimCount];
		int num = this.m_itemListNum[this.m_currentAnimCount];
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rootGameObject, "img_player");
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rootGameObject, "img_item");
		if (uisprite != null && this.m_info != null)
		{
			uisprite.gameObject.SetActive(false);
		}
		if (uisprite2 != null && AtlasManager.Instance != null)
		{
			uisprite2.atlas = AtlasManager.Instance.ItemAtlas;
			uisprite2.spriteName = serverItem.serverItemSpriteName;
			uisprite2.gameObject.SetActive(true);
		}
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rootGameObject, "img_player_speacies");
		if (uisprite3 != null && this.m_info != null)
		{
			uisprite3.gameObject.SetActive(false);
		}
		UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_rootGameObject, "img_player_genus");
		if (uisprite4 != null && this.m_info != null)
		{
			uisprite4.gameObject.SetActive(false);
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_player_name");
		if (uilabel != null && this.m_info != null)
		{
			uilabel.gameObject.SetActive(false);
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_player_lv");
		if (uilabel2 != null && this.m_info != null)
		{
			uilabel2.gameObject.SetActive(false);
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_player_attribute");
		if (uilabel3 != null && this.m_info != null)
		{
			uilabel3.gameObject.SetActive(false);
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_item_name");
		UILabel uilabel5 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_item_info");
		UILabel uilabel6 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_rootGameObject, "Lbl_item_number");
		if (uilabel4 != null)
		{
			uilabel4.gameObject.SetActive(true);
			uilabel4.text = serverItem.serverItemName;
			if (this.m_caption != null)
			{
				string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "overlap_get_item_caption").text;
				if (string.IsNullOrEmpty(text))
				{
					this.m_caption.text = serverItem.serverItemName;
				}
				else
				{
					this.m_caption.text = text.Replace("{ITEM}", serverItem.serverItemName);
				}
			}
		}
		if (uilabel5 != null)
		{
			uilabel5.gameObject.SetActive(true);
			uilabel5.text = serverItem.serverItemComment;
			UIDraggablePanel uidraggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(this.m_rootGameObject, "window_chaoset_alpha_clip");
			if (uidraggablePanel != null)
			{
				uidraggablePanel.ResetPosition();
			}
		}
		if (uilabel6 != null)
		{
			uilabel6.gameObject.SetActive(true);
			uilabel6.text = string.Format("× {0:0000}", num);
		}
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x00092DB0 File Offset: 0x00090FB0
	public override void PlayGetAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		if (this.m_currentAnimCount == 0)
		{
			this.m_animation = anim;
			if (this.m_itemList != null && this.m_itemList.Count > 0)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_1_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
				this.m_buttonType = ChaoGetPartsBase.BtnType.NEXT;
			}
			else
			{
				string clipName = "ui_ro_PlayerGetWindowUI_intro_Anim";
				switch (this.m_introType)
				{
				case PlayerGetPartsOverlap.IntroType.NO_EGG:
				case PlayerGetPartsOverlap.IntroType.NONE:
					clipName = "ui_ro_PlayerGetWindowUI_noegg_intro_Anim";
					break;
				}
				ActiveAnimation activeAnimation2 = ActiveAnimation.Play(anim, clipName, Direction.Forward);
				EventDelegate.Add(activeAnimation2.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
				this.m_buttonType = ChaoGetPartsBase.BtnType.EQUIP_OK;
			}
		}
		else
		{
			this.SetupItem();
			this.m_animation = null;
			ActiveAnimation activeAnimation3 = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_2_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation3.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
			if (this.m_currentAnimCount >= this.m_itemList.Count - 1)
			{
				this.m_buttonType = ChaoGetPartsBase.BtnType.OK;
			}
			else
			{
				this.m_buttonType = ChaoGetPartsBase.BtnType.NEXT;
			}
		}
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x00092EEC File Offset: 0x000910EC
	public override ChaoGetPartsBase.BtnType GetButtonType()
	{
		if (RouletteUtility.loginRoulette)
		{
			return ChaoGetPartsBase.BtnType.OK;
		}
		return this.m_buttonType;
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x00092F00 File Offset: 0x00091100
	public override void PlayEndAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		if (this.m_itemList != null && this.m_itemList.Count > 0)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_3_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.EndAnimationFinishCallback), true);
		}
		else
		{
			ActiveAnimation activeAnimation2 = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_outro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation2.onFinished, new EventDelegate.Callback(this.EndAnimationFinishCallback), true);
		}
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x00092F88 File Offset: 0x00091188
	public override void PlaySE(string seType)
	{
		if (seType == ChaoWindowUtility.SeHatch)
		{
			if (this.m_playSeCount > 0)
			{
				SoundManager.SePlay("sys_roulette_itemget", "SE");
			}
			else
			{
				SoundManager.SePlay("sys_chao_hatch", "SE");
			}
			this.m_playSeCount++;
		}
		else if (seType == ChaoWindowUtility.SeBreak)
		{
			SoundManager.SePlay("sys_chao_birthS", "SE");
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x0009300C File Offset: 0x0009120C
	public override EasySnsFeed CreateEasySnsFeed(GameObject rootObject)
	{
		string anchorPath = "Camera/menu_Anim/RouletteUI/Anchor_5_MC";
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_caption").text;
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_text");
		string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", this.m_info.m_name.ToLower()).text;
		text2.ReplaceTag("{CHAO}", text3);
		return new EasySnsFeed(rootObject, anchorPath, text, text2.text, null);
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x00093088 File Offset: 0x00091288
	private void GetAnimationFinishCallback()
	{
		if (this.m_currentAnimCount == 0 && this.m_itemList != null && this.m_itemList.Count > 0 && this.m_animation != null)
		{
			base.StartCoroutine(this.FirstOverlap());
		}
		else
		{
			this.m_currentAnimCount++;
			if (this.m_itemList == null || this.m_currentAnimCount >= this.m_itemList.Count)
			{
				this.m_callback(ChaoGetPartsBase.AnimType.GET_ANIM_FINISH);
			}
			else
			{
				this.m_callback(ChaoGetPartsBase.AnimType.GET_ANIM_CONTINUE);
			}
		}
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x0009312C File Offset: 0x0009132C
	private IEnumerator FirstOverlap()
	{
		yield return null;
		this.SetupItem();
		ActiveAnimation activeAnim = ActiveAnimation.Play(this.m_animation, "ui_ro_PlayerGetWindowUI_2_Anim", Direction.Forward);
		EventDelegate.Add(activeAnim.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
		this.m_animation = null;
		yield break;
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x00093148 File Offset: 0x00091348
	private void EndAnimationFinishCallback()
	{
		this.m_callback(ChaoGetPartsBase.AnimType.OUT_ANIM);
	}

	// Token: 0x040016B1 RID: 5809
	private int m_severId;

	// Token: 0x040016B2 RID: 5810
	private int m_rarity;

	// Token: 0x040016B3 RID: 5811
	private int m_level;

	// Token: 0x040016B4 RID: 5812
	private int m_currentAnimCount;

	// Token: 0x040016B5 RID: 5813
	private List<ServerItem> m_itemList;

	// Token: 0x040016B6 RID: 5814
	private List<int> m_itemListNum;

	// Token: 0x040016B7 RID: 5815
	private CharacterDataNameInfo.Info m_info;

	// Token: 0x040016B8 RID: 5816
	private Animation m_animation;

	// Token: 0x040016B9 RID: 5817
	private GameObject m_rootGameObject;

	// Token: 0x040016BA RID: 5818
	private int m_playSeCount;

	// Token: 0x040016BB RID: 5819
	private PlayerGetPartsOverlap.IntroType m_introType;

	// Token: 0x040016BC RID: 5820
	private ChaoGetPartsBase.BtnType m_buttonType;

	// Token: 0x040016BD RID: 5821
	private UILabel m_caption;

	// Token: 0x02000356 RID: 854
	public enum IntroType
	{
		// Token: 0x040016BF RID: 5823
		NORMAL,
		// Token: 0x040016C0 RID: 5824
		NO_EGG,
		// Token: 0x040016C1 RID: 5825
		NONE
	}
}
