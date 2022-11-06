using System;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class ChaoGetPartsNormal : ChaoGetPartsBase
{
	// Token: 0x06001916 RID: 6422 RVA: 0x00090F20 File Offset: 0x0008F120
	public void Init(int chaoId, int rarity)
	{
		this.m_chaoId = chaoId;
		this.m_rarity = rarity;
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x00090F30 File Offset: 0x0008F130
	public override void Setup(GameObject chaoGetObjectRoot)
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(chaoGetObjectRoot, "img_chao_1");
		if (uitexture != null)
		{
			int idFromServerId = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
			ChaoTextureManager.Instance.GetTexture(idFromServerId, info);
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_chao_bg_rare");
		if (uisprite != null)
		{
			ChaoWindowUtility.ChangeRaritySpriteFromRarity(uisprite, this.m_rarity);
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_chao_name");
		if (uilabel != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", ChaoWindowUtility.GetChaoLabelName(this.m_chaoId)).text;
			uilabel.text = text;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_chao_lv");
		if (uilabel2 != null)
		{
			uilabel2.text = TextUtility.GetTextLevel("0");
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_type_icon");
		if (uisprite2 != null)
		{
			int idFromServerId2 = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			ChaoData chaoData = ChaoTable.GetChaoData(idFromServerId2);
			if (chaoData != null)
			{
				CharacterAttribute charaAtribute = chaoData.charaAtribute;
				string spriteName = "ui_chao_set_type_icon_" + charaAtribute.ToString().ToLower();
				uisprite2.spriteName = spriteName;
			}
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_type");
		if (uilabel3 != null)
		{
			int idFromServerId3 = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			ChaoData chaoData2 = ChaoTable.GetChaoData(idFromServerId3);
			if (chaoData2 != null)
			{
				CharacterAttribute charaAtribute2 = chaoData2.charaAtribute;
				string cellName = charaAtribute2.ToString().ToLower();
				string text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaAtribute", cellName).text;
				uilabel3.text = text2;
			}
		}
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_bonus_icon");
		if (uisprite3 != null)
		{
			uisprite3.enabled = false;
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_bonus");
		if (uilabel4 != null)
		{
			int idFromServerId4 = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			uilabel4.text = HudUtility.GetChaoAbilityText(idFromServerId4, -1);
		}
		UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_0");
		if (uisprite4 != null)
		{
			string spriteName2 = "ui_roulette_egg_" + (2 * this.m_rarity).ToString();
			uisprite4.spriteName = spriteName2;
		}
		UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_1");
		if (uisprite5 != null)
		{
			string spriteName3 = "ui_roulette_egg_" + (2 * this.m_rarity + 1).ToString();
			uisprite5.spriteName = spriteName3;
		}
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x000911B8 File Offset: 0x0008F3B8
	public override void PlayGetAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_menu_chao_get_Window_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x000911F8 File Offset: 0x0008F3F8
	public override ChaoGetPartsBase.BtnType GetButtonType()
	{
		return ChaoGetPartsBase.BtnType.EQUIP_OK;
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000911FC File Offset: 0x0008F3FC
	public override void PlayEndAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_menu_chao_get_Window_outro_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.EndAnimationFinishCallback), true);
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x0009123C File Offset: 0x0008F43C
	public override void PlaySE(string seType)
	{
		if (seType == ChaoWindowUtility.SeHatch)
		{
			SoundManager.SePlay("sys_chao_hatch", "SE");
		}
		else if (seType == ChaoWindowUtility.SeBreak)
		{
			ChaoWindowUtility.PlaySEChaoBtrth(this.m_chaoId, this.m_rarity);
		}
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x00091290 File Offset: 0x0008F490
	public override EasySnsFeed CreateEasySnsFeed(GameObject rootObject)
	{
		string anchorPath = "Camera/menu_Anim/RouletteUI/Anchor_5_MC";
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_caption").text;
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_text");
		string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", RouletteUtility.GetChaoCellName(this.m_chaoId)).text;
		text2.ReplaceTag("{CHAO}", text3);
		return new EasySnsFeed(rootObject, anchorPath, text, text2.text, null);
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x00091308 File Offset: 0x0008F508
	private void GetAnimationFinishCallback()
	{
		global::Debug.Log("ChaoGetPartsNormal.GetAnimationFinishCallback()");
		this.m_callback(ChaoGetPartsBase.AnimType.GET_ANIM_FINISH);
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x00091320 File Offset: 0x0008F520
	private void EndAnimationFinishCallback()
	{
		global::Debug.Log("ChaoGetPartsNormal.EndAnimationFinishCallback()");
		this.m_callback(ChaoGetPartsBase.AnimType.OUT_ANIM);
	}

	// Token: 0x04001698 RID: 5784
	private int m_rarity;
}
