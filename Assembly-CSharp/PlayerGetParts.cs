using System;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class PlayerGetParts : ChaoGetPartsBase
{
	// Token: 0x0600193F RID: 6463 RVA: 0x0009227C File Offset: 0x0009047C
	public void Init(int severId, int level)
	{
		this.m_severId = severId;
		this.m_level = level;
		this.m_info = CharacterDataNameInfo.Instance.GetDataByServerID(this.m_severId);
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000922B0 File Offset: 0x000904B0
	public override void Setup(GameObject chaoGetObjectRoot)
	{
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_player");
		if (uisprite != null && this.m_info != null)
		{
			uisprite.spriteName = this.m_info.characterSpriteName;
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_player_speacies");
		if (uisprite2 != null && this.m_info != null)
		{
			switch (this.m_info.m_attribute)
			{
			case CharacterAttribute.SPEED:
				uisprite2.spriteName = "ui_mm_player_species_0";
				break;
			case CharacterAttribute.FLY:
				uisprite2.spriteName = "ui_mm_player_species_1";
				break;
			case CharacterAttribute.POWER:
				uisprite2.spriteName = "ui_mm_player_species_2";
				break;
			}
		}
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_player_genus");
		if (uisprite3 != null && this.m_info != null)
		{
			uisprite3.spriteName = HudUtility.GetTeamAttributeSpriteName(this.m_info.m_ID);
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_name");
		if (uilabel != null && this.m_info != null)
		{
			uilabel.text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", this.m_info.m_name.ToLower()).text;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_lv");
		if (uilabel2 != null && this.m_info != null)
		{
			uilabel2.text = string.Format("Lv.{0:D3}", this.m_level);
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_player_attribute");
		if (uilabel3 != null && this.m_info != null)
		{
			ServerPlayerState playerState = ServerInterface.PlayerState;
			if (playerState != null)
			{
				ServerCharacterState serverCharacterState = playerState.CharacterState(this.m_info.m_ID);
				uilabel3.text = serverCharacterState.GetCharaAttributeText();
			}
		}
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x00092484 File Offset: 0x00090684
	public override void PlayGetAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_intro_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x000924C4 File Offset: 0x000906C4
	public override ChaoGetPartsBase.BtnType GetButtonType()
	{
		return ChaoGetPartsBase.BtnType.EQUIP_OK;
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x000924C8 File Offset: 0x000906C8
	public override void PlayEndAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_ro_PlayerGetWindowUI_outro_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.EndAnimationFinishCallback), true);
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x00092508 File Offset: 0x00090708
	public override void PlaySE(string seType)
	{
		if (!(seType == ChaoWindowUtility.SeHatch))
		{
			if (seType == ChaoWindowUtility.SeBreak)
			{
				SoundManager.SePlay("sys_chao_birth", "SE");
			}
		}
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x00092540 File Offset: 0x00090740
	public override EasySnsFeed CreateEasySnsFeed(GameObject rootObject)
	{
		string anchorPath = "Camera/menu_Anim/RouletteUI/Anchor_5_MC";
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_caption").text;
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_text");
		string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", this.m_info.m_name.ToLower()).text;
		text2.ReplaceTag("{CHAO}", text3);
		return new EasySnsFeed(rootObject, anchorPath, text, text2.text, null);
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x000925BC File Offset: 0x000907BC
	private void GetAnimationFinishCallback()
	{
		this.m_callback(ChaoGetPartsBase.AnimType.GET_ANIM_FINISH);
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x000925CC File Offset: 0x000907CC
	private void EndAnimationFinishCallback()
	{
		this.m_callback(ChaoGetPartsBase.AnimType.OUT_ANIM);
	}

	// Token: 0x040016AE RID: 5806
	private int m_severId;

	// Token: 0x040016AF RID: 5807
	private int m_level;

	// Token: 0x040016B0 RID: 5808
	private CharacterDataNameInfo.Info m_info;
}
