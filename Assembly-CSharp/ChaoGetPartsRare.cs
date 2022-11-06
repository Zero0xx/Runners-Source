using System;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class ChaoGetPartsRare : ChaoGetPartsBase
{
	// Token: 0x06001920 RID: 6432 RVA: 0x00091340 File Offset: 0x0008F540
	public void Init(int chaoId, int rarity)
	{
		this.m_chaoId = chaoId;
		this.m_rarity = rarity;
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x00091350 File Offset: 0x0008F550
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
			ChaoWindowUtility.ChangeRaritySpriteFromServerChaoId(uisprite, this.m_chaoId);
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
		UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_0");
		if (uisprite3 != null)
		{
			string spriteName2 = "ui_roulette_egg_" + (2 * this.m_rarity).ToString();
			uisprite3.spriteName = spriteName2;
		}
		UISprite uisprite4 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_egg_1");
		if (uisprite4 != null)
		{
			string spriteName3 = "ui_roulette_egg_" + (2 * this.m_rarity + 1).ToString();
			uisprite4.spriteName = spriteName3;
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
		UISprite uisprite5 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(chaoGetObjectRoot, "img_bonus_icon");
		if (uisprite5 != null)
		{
			uisprite5.enabled = false;
		}
		UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(chaoGetObjectRoot, "Lbl_bonus");
		if (uilabel4 != null)
		{
			int idFromServerId4 = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			uilabel4.text = HudUtility.GetChaoAbilityText(idFromServerId4, -1);
		}
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x000915D8 File Offset: 0x0008F7D8
	public override void PlayGetAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_menu_chao_rare_get_Window_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.GetAnimationFinishCallback), true);
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x00091618 File Offset: 0x0008F818
	public override ChaoGetPartsBase.BtnType GetButtonType()
	{
		return ChaoGetPartsBase.BtnType.EQUIP_OK;
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x0009161C File Offset: 0x0008F81C
	public override void PlayEndAnimation(Animation anim)
	{
		if (anim == null)
		{
			return;
		}
		ActiveAnimation activeAnimation = ActiveAnimation.Play(anim, "ui_menu_chao_rare_get_Window_outro_Anim", Direction.Forward);
		EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.EndAnimationFinishCallback), true);
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x0009165C File Offset: 0x0008F85C
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

	// Token: 0x06001926 RID: 6438 RVA: 0x000916B0 File Offset: 0x0008F8B0
	public override EasySnsFeed CreateEasySnsFeed(GameObject rootObject)
	{
		string anchorPath = "Camera/menu_Anim/RouletteUI/Anchor_5_MC";
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_caption").text;
		TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "feed_chao_get_text");
		string text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", RouletteUtility.GetChaoCellName(this.m_chaoId)).text;
		text2.ReplaceTag("{CHAO}", text3);
		return new EasySnsFeed(rootObject, anchorPath, text, text2.text, null);
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x00091728 File Offset: 0x0008F928
	private void GetAnimationFinishCallback()
	{
		this.m_callback(ChaoGetPartsBase.AnimType.GET_ANIM_FINISH);
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x00091738 File Offset: 0x0008F938
	private void EndAnimationFinishCallback()
	{
		this.m_callback(ChaoGetPartsBase.AnimType.OUT_ANIM);
	}

	// Token: 0x04001699 RID: 5785
	private int m_rarity;
}
