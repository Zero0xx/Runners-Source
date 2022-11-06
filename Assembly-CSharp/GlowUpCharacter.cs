using System;
using System.Collections;
using Text;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class GlowUpCharacter : MonoBehaviour
{
	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000A18E0 File Offset: 0x0009FAE0
	public bool IsEndSetup
	{
		get
		{
			return this.m_isEndSetup;
		}
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000A18E8 File Offset: 0x0009FAE8
	public void Setup(GlowUpCharaBaseInfo baseInfo)
	{
		this.m_baseInfo = baseInfo;
		if (!this.m_baseInfo.IsActive)
		{
			this.m_isEndSetup = true;
			this.m_isEnd = true;
			base.gameObject.SetActive(false);
			return;
		}
		base.StartCoroutine(this.OnSetup(baseInfo));
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x000A1938 File Offset: 0x0009FB38
	private IEnumerator OnSetup(GlowUpCharaBaseInfo baseInfo)
	{
		yield return null;
		CharaType charaType = this.m_baseInfo.charaType;
		UISprite charaIcon = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player");
		if (charaIcon != null)
		{
			string spriteName = HudUtility.MakeCharaTextureName(charaType, HudUtility.TextureType.TYPE_S);
			charaIcon.spriteName = spriteName;
		}
		UILabel levelLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		if (levelLabel != null)
		{
			levelLabel.text = TextUtility.GetTextLevel(this.m_baseInfo.level.ToString());
		}
		UILabel charaNameLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_name");
		if (charaNameLabel != null)
		{
			string charaName = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", CharaName.Name[(int)charaType]).text;
			charaNameLabel.text = charaName;
		}
		UISprite typeSprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_speacies");
		if (typeSprite != null)
		{
			typeSprite.spriteName = HudUtility.GetCharaAttributeSpriteName(charaType);
		}
		UISprite teamTypeSprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_player_genus");
		if (teamTypeSprite != null)
		{
			teamTypeSprite.spriteName = HudUtility.GetTeamAttributeSpriteName(charaType);
		}
		if (this.m_expBar == null)
		{
			this.m_expBar = base.gameObject.AddComponent<GlowUpExpBar>();
			UISlider baseSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_b4exp");
			UISlider glowUpSlider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_exp");
			UILabel expLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_price_number");
			if (this.m_expBar != null)
			{
				this.m_expBar.SetBaseSlider(baseSlider);
				this.m_expBar.SetGlowUpSlider(glowUpSlider);
				this.m_expBar.SetExpLabel(expLabel);
				this.m_expBar.SetCallback(new GlowUpExpBar.LevelUpCallback(this.ExpBarLevelUpCallback), new GlowUpExpBar.EndCallback(this.ExpBarEndCallback));
			}
		}
		if (this.m_expBar != null)
		{
			GlowUpExpBar.ExpInfo startInfo = new GlowUpExpBar.ExpInfo();
			startInfo.level = this.m_baseInfo.level;
			startInfo.cost = this.m_baseInfo.levelUpCost;
			startInfo.exp = this.m_baseInfo.currentExp;
			this.m_expBar.SetStartExp(startInfo);
		}
		if (this.m_abilityPanel == null)
		{
			GameObject abilityRootObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ui_player_set_item_2_cell(Clone)");
			if (abilityRootObject != null)
			{
				this.m_abilityPanel = abilityRootObject.AddComponent<MenuPlayerSetAbilityButton>();
			}
		}
		this.m_isEndSetup = true;
		yield break;
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x000A1954 File Offset: 0x0009FB54
	public void PlayStart(GlowUpCharaAfterInfo afterInfo)
	{
		if (this.m_isEnd)
		{
			return;
		}
		base.StartCoroutine(this.OnPlayStart(afterInfo));
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000A1970 File Offset: 0x0009FB70
	public void PlaySkip()
	{
		if (this.m_expBar != null)
		{
			this.m_expBar.PlaySkip();
		}
		if (this.m_abilityPanel != null)
		{
			this.m_abilityPanel.SkipLevelUp();
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000A19B8 File Offset: 0x0009FBB8
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x000A19C0 File Offset: 0x0009FBC0
	private IEnumerator OnPlayStart(GlowUpCharaAfterInfo afterInfo)
	{
		while (!this.m_isEndSetup)
		{
			yield return null;
		}
		this.m_afterInfo = afterInfo;
		this.m_isEnd = false;
		if (this.m_expBar != null)
		{
			GlowUpExpBar.ExpInfo endInfo = new GlowUpExpBar.ExpInfo();
			endInfo.level = this.m_afterInfo.level;
			endInfo.cost = this.m_afterInfo.levelUpCost;
			endInfo.exp = this.m_afterInfo.exp;
			this.m_expBar.SetEndExp(endInfo);
			this.m_expBar.PlayStart();
			this.m_expBar.SetLevelUpCostList(this.m_afterInfo.abilityListExp);
		}
		yield break;
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x000A19EC File Offset: 0x0009FBEC
	private void ExpBarLevelUpCallback(int level)
	{
		SoundManager.SePlay("sys_buy", "SE");
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_player_lv");
		if (uilabel != null)
		{
			uilabel.text = TextUtility.GetTextLevel(level.ToString());
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_slot_mask");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		if (this.m_abilityPanel != null && this.m_afterInfo.abilityList.Count > 0)
		{
			AbilityType abilityType = this.m_afterInfo.abilityList[0];
			this.m_abilityPanel.Setup(this.m_baseInfo.charaType, abilityType);
			this.m_abilityPanel.LevelUp(new MenuPlayerSetAbilityButton.AnimEndCallback(this.LevelUpAnimationEndCallback));
			this.m_afterInfo.abilityList.Remove(abilityType);
		}
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000A1AD8 File Offset: 0x0009FCD8
	private void LevelUpAnimationEndCallback()
	{
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000A1ADC File Offset: 0x0009FCDC
	private void ExpBarEndCallback()
	{
		this.m_isEnd = true;
	}

	// Token: 0x040018DA RID: 6362
	private GlowUpCharaBaseInfo m_baseInfo;

	// Token: 0x040018DB RID: 6363
	private GlowUpCharaAfterInfo m_afterInfo;

	// Token: 0x040018DC RID: 6364
	private GlowUpExpBar m_expBar;

	// Token: 0x040018DD RID: 6365
	private MenuPlayerSetAbilityButton m_abilityPanel;

	// Token: 0x040018DE RID: 6366
	private bool m_isEndSetup;

	// Token: 0x040018DF RID: 6367
	private bool m_isEnd;

	// Token: 0x02000A85 RID: 2693
	// (Invoke) Token: 0x0600484E RID: 18510
	public delegate void GlowUpEndCallback();
}
