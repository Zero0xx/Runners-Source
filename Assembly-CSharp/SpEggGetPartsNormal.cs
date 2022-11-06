using System;
using Text;
using UnityEngine;

// Token: 0x02000363 RID: 867
public class SpEggGetPartsNormal : SpEggGetPartsBase
{
	// Token: 0x060019A6 RID: 6566 RVA: 0x000954B8 File Offset: 0x000936B8
	public SpEggGetPartsNormal(int chaoId, int acquiredSpEggCount)
	{
		this.m_chaoId = chaoId;
		this.m_acquiredSpEggCount = acquiredSpEggCount;
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000954D0 File Offset: 0x000936D0
	public override void Setup(GameObject spEggGetObjectRoot)
	{
		UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(spEggGetObjectRoot, "img_chao_1");
		if (uitexture != null)
		{
			int idFromServerId = ChaoWindowUtility.GetIdFromServerId(this.m_chaoId);
			ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
			ChaoTextureManager.Instance.GetTexture(idFromServerId, info);
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

	// Token: 0x060019A8 RID: 6568 RVA: 0x00095604 File Offset: 0x00093804
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

	// Token: 0x040016F0 RID: 5872
	private int m_acquiredSpEggCount;
}
