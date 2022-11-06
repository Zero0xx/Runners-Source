using System;
using Text;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class SpEggGetPartsRare : SpEggGetPartsBase
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x0009567C File Offset: 0x0009387C
	public SpEggGetPartsRare(int chaoId, int rarity, int acquiredSpEggCount)
	{
		this.m_chaoId = chaoId;
		this.m_rarity = rarity;
		this.m_acquiredSpEggCount = acquiredSpEggCount;
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x0009569C File Offset: 0x0009389C
	public override void Setup(GameObject spEggGetObjectRoot)
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(spEggGetObjectRoot, "img_chao_1");
		if (uitexture != null)
		{
			int idFromServerId = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
			ChaoTextureManager.Instance.GetTexture(idFromServerId, info);
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(spEggGetObjectRoot, "img_egg_0");
		if (uisprite != null)
		{
			string spriteName = "ui_roulette_egg_" + (2 * this.m_rarity).ToString();
			uisprite.spriteName = spriteName;
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(spEggGetObjectRoot, "img_egg_1");
		if (uisprite2 != null)
		{
			string spriteName2 = "ui_roulette_egg_" + (2 * this.m_rarity + 1).ToString();
			uisprite2.spriteName = spriteName2;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(spEggGetObjectRoot, "Lbl_item_name");
		if (uilabel != null)
		{
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "sp_egg_name").text;
			uilabel.text = text;
		}
		UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(spEggGetObjectRoot, "Lbl_item_number");
		if (uilabel2 != null)
		{
			TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Score", "number_of_pieces");
			text2.ReplaceTag("{NUM}", this.m_acquiredSpEggCount.ToString());
			uilabel2.text = text2.text;
		}
		UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(spEggGetObjectRoot, "Lbl_info");
		if (uilabel3 != null)
		{
			TextObject text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoRoulette", "sp_egg_info");
			string text4 = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, "Chao", ChaoWindowUtility.GetChaoLabelName(this.m_chaoId)).text;
			text3.ReplaceTag("{CHAO_NAME}", text4);
			uilabel3.text = text3.text;
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x00095854 File Offset: 0x00093A54
	public override void PlaySE(string seType)
	{
		if (seType == ChaoWindowUtility.SeHatch)
		{
			SoundManager.SePlay("sys_chao_hatch", "SE");
		}
		else if (seType == ChaoWindowUtility.SeSpEgg)
		{
			SoundManager.SePlay("sys_specialegg", "SE");
		}
		else if (seType == ChaoWindowUtility.SeBreak)
		{
			SoundManager.SePlay("sys_chao_birth", "SE");
		}
	}

	// Token: 0x040016F1 RID: 5873
	private int m_rarity;

	// Token: 0x040016F2 RID: 5874
	private int m_acquiredSpEggCount;
}
