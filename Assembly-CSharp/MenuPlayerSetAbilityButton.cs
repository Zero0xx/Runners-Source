using System;
using System.Collections;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class MenuPlayerSetAbilityButton : MonoBehaviour
{
	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x0600241A RID: 9242 RVA: 0x000D84D4 File Offset: 0x000D66D4
	// (set) Token: 0x0600241B RID: 9243 RVA: 0x000D84DC File Offset: 0x000D66DC
	public bool AnimEnd
	{
		get
		{
			return this.m_animEnd;
		}
		private set
		{
		}
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x000D84E0 File Offset: 0x000D66E0
	private void Start()
	{
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x000D84E4 File Offset: 0x000D66E4
	private void LateUpdate()
	{
		MenuPlayerSetAbilityButton.State state = this.m_state;
		if (state != MenuPlayerSetAbilityButton.State.IDLE)
		{
			if (state == MenuPlayerSetAbilityButton.State.LEVELUP)
			{
				ImportAbilityTable instance = ImportAbilityTable.GetInstance();
				int maxLevel = instance.GetMaxLevel(this.m_params.Ability);
				int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
				UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_item_lv");
				if (uislider != null)
				{
					this.m_levelUpAnimTime += Time.deltaTime;
					if (this.m_levelUpAnimTime >= this.LevelUpAnimTotalTime)
					{
						this.m_levelUpAnimTime = this.LevelUpAnimTotalTime;
						this.m_state = MenuPlayerSetAbilityButton.State.IDLE;
					}
					float num = (float)level - 1f + this.m_levelUpAnimTime / this.LevelUpAnimTotalTime;
					num /= (float)maxLevel;
					uislider.value = num;
				}
			}
		}
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x000D85C8 File Offset: 0x000D67C8
	public void Setup(CharaType charaType, AbilityType abilityType)
	{
		this.m_params = new AbilityButtonParams();
		this.m_params.Character = charaType;
		this.m_params.Ability = abilityType;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_cursor_eff_set");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		this.InitLabels();
		this.InitButtonState();
		UISlider uislider = GameObjectUtil.FindChildGameObjectComponent<UISlider>(base.gameObject, "Pgb_item_lv");
		if (uislider != null)
		{
			ImportAbilityTable instance = ImportAbilityTable.GetInstance();
			int maxLevel = instance.GetMaxLevel(this.m_params.Ability);
			int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
			uislider.value = (float)level / (float)maxLevel;
		}
		this.m_state = MenuPlayerSetAbilityButton.State.IDLE;
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x000D8690 File Offset: 0x000D6890
	public void SetActive(bool isActive)
	{
		GameObject x = GameObjectUtil.FindChildGameObject(base.gameObject, "img_cursor");
		if (x != null)
		{
		}
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x000D86BC File Offset: 0x000D68BC
	public void LevelUp(MenuPlayerSetAbilityButton.AnimEndCallback callback)
	{
		this.m_animEndCallback = callback;
		this.m_animEnd = false;
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		int maxLevel = instance.GetMaxLevel(this.m_params.Ability);
		int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
		if (level >= maxLevel)
		{
			UnityEngine.Object.Destroy(this.m_currentState);
			this.m_currentState = base.gameObject.AddComponent<MenuPlayerSetLevelStateMax>();
			this.m_currentState.Setup(this.m_params);
			this.AwakeLevelMax();
		}
		this.SetPotentialText();
		this.ChangeLevelLabels();
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "Btn_toggle");
		if (animation != null)
		{
			animation.Stop();
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.LevelUpAnimationEndCallback), true);
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_cursor_eff_set");
			if (gameObject != null)
			{
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_effect_b4");
				if (uilabel != null)
				{
					uilabel.text = instance.GetAbilityPotential(this.m_params.Ability, level - 1).ToString();
				}
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_effect_af");
				if (uilabel2 != null)
				{
					uilabel2.text = instance.GetAbilityPotential(this.m_params.Ability, level).ToString();
				}
				gameObject.SetActive(true);
			}
			this.SetActive(false);
		}
		this.m_levelUpAnimTime = 0f;
		this.m_state = MenuPlayerSetAbilityButton.State.LEVELUP;
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x000D8854 File Offset: 0x000D6A54
	public void SkipLevelUp()
	{
		if (this.m_animEndCallback == null)
		{
			return;
		}
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "Btn_toggle");
		if (animation != null)
		{
			foreach (object obj in animation)
			{
				AnimationState animationState = (AnimationState)obj;
				if (!(animationState == null))
				{
					animationState.time = animationState.length * 0.99f;
				}
			}
		}
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x000D8904 File Offset: 0x000D6B04
	private void InitLabels()
	{
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_icon");
		if (uisprite != null)
		{
			string spriteName = "ui_mm_player_icon_" + ((int)this.m_params.Ability).ToString();
			uisprite.spriteName = spriteName;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_word_item_effect");
		if (uilabel != null)
		{
			string cellName = "abilitycaption" + ((int)(this.m_params.Ability + 1)).ToString();
			string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", cellName).text;
			uilabel.text = text;
		}
		this.SetPotentialText();
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x000D89B4 File Offset: 0x000D6BB4
	private void InitButtonState()
	{
		if (this.m_currentState != null)
		{
			return;
		}
		ImportAbilityTable instance = ImportAbilityTable.GetInstance();
		int maxLevel = instance.GetMaxLevel(this.m_params.Ability);
		int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
		if (level >= maxLevel)
		{
			this.m_currentState = base.gameObject.AddComponent<MenuPlayerSetLevelStateMax>();
			this.AwakeLevelMax();
		}
		else
		{
			this.m_currentState = base.gameObject.AddComponent<MenuPlayerSetLevelStateNormal>();
		}
		this.m_currentState.Setup(this.m_params);
		this.ChangeLevelLabels();
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x000D8A54 File Offset: 0x000D6C54
	private void SetPotentialText()
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_effect");
		if (uilabel != null)
		{
			string cellName = "abilitypotential" + ((int)(this.m_params.Ability + 1)).ToString();
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaStatus", cellName);
			ImportAbilityTable instance = ImportAbilityTable.GetInstance();
			int level = MenuPlayerSetUtil.GetLevel(this.m_params.Character, this.m_params.Ability);
			text.ReplaceTag("{ABILITY_POTENTIAL}", instance.GetAbilityPotential(this.m_params.Ability, level).ToString());
			uilabel.text = text.text;
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_item_effect_sh");
			if (uilabel2 != null)
			{
				uilabel2.text = text.text;
			}
		}
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x000D8B30 File Offset: 0x000D6D30
	private void ChangeLevelLabels()
	{
		if (this.m_currentState != null)
		{
			this.m_currentState.ChangeLabels();
		}
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x000D8B50 File Offset: 0x000D6D50
	private void AwakeLevelMax()
	{
		UIImageButton component = base.gameObject.GetComponent<UIImageButton>();
		if (component != null)
		{
			component.isEnabled = false;
		}
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x000D8B7C File Offset: 0x000D6D7C
	private void LevelUpAnimationEndCallback()
	{
		this.m_animEnd = true;
		base.StartCoroutine(this.HideEffectObject());
		this.m_animEndCallback();
		this.m_animEndCallback = null;
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x000D8BB0 File Offset: 0x000D6DB0
	private IEnumerator HideEffectObject()
	{
		yield return null;
		GameObject effectObject = GameObjectUtil.FindChildGameObject(base.gameObject, "img_cursor_eff_set");
		if (effectObject != null)
		{
			effectObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x040020B0 RID: 8368
	private AbilityButtonParams m_params;

	// Token: 0x040020B1 RID: 8369
	private MenuPlayerSetLevelState m_currentState;

	// Token: 0x040020B2 RID: 8370
	private MenuPlayerSetAbilityButton.State m_state;

	// Token: 0x040020B3 RID: 8371
	private float m_levelUpAnimTime;

	// Token: 0x040020B4 RID: 8372
	private readonly float LevelUpAnimTotalTime = 1f;

	// Token: 0x040020B5 RID: 8373
	private MenuPlayerSetAbilityButton.AnimEndCallback m_animEndCallback;

	// Token: 0x040020B6 RID: 8374
	private bool m_animEnd = true;

	// Token: 0x020004C7 RID: 1223
	private enum State
	{
		// Token: 0x040020B8 RID: 8376
		IDLE,
		// Token: 0x040020B9 RID: 8377
		LEVELUP
	}

	// Token: 0x02000A93 RID: 2707
	// (Invoke) Token: 0x06004886 RID: 18566
	public delegate void AnimEndCallback();
}
