using System;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class ChaoSetWindowUI : MonoBehaviour
{
	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06001E0A RID: 7690 RVA: 0x000B081C File Offset: 0x000AEA1C
	public static bool isActive
	{
		get
		{
			return ChaoSetWindowUI.m_isActive;
		}
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x000B0824 File Offset: 0x000AEA24
	public static ChaoSetWindowUI GetWindow()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject != null)
		{
			return GameObjectUtil.FindChildGameObjectComponent<ChaoSetWindowUI>(gameObject, "ChaoSetWindowUI");
		}
		return null;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x000B0858 File Offset: 0x000AEA58
	public void OpenWindow(ChaoSetWindowUI.ChaoInfo chaoInfo, ChaoSetWindowUI.WindowType windowType)
	{
		ChaoSetWindowUI.m_isActive = true;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(base.gameObject, "chao_set_window");
		if (animation != null)
		{
			animation.gameObject.SetActive(true);
			ActiveAnimation.Play(animation, Direction.Forward);
		}
		SoundManager.SePlay("sys_window_open", "SE");
		this.OnSetChao(chaoInfo);
		string text = string.Empty;
		if (windowType != ChaoSetWindowUI.WindowType.WITH_BUTTON)
		{
			if (windowType == ChaoSetWindowUI.WindowType.WINDOW_ONLY)
			{
				this.OnSetActiveButton(false);
				text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "ui_Lbl_caption_detail").text;
			}
		}
		else
		{
			this.OnSetActiveButton(true);
			text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "ui_Lbl_caption").text;
		}
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		if (uilabel != null)
		{
			uilabel.text = text;
		}
		this.m_tutorial = ChaoSetUI.IsChaoTutorial();
		if (this.m_tutorial)
		{
			TutorialCursor.StartTutorialCursor(TutorialCursor.Type.CHAOSELECT_MAIN);
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x000B0970 File Offset: 0x000AEB70
	private void Start()
	{
		this.SetAnimationCallBack("Btn_set_main");
		this.SetAnimationCallBack("Btn_set_sub");
		this.SetAnimationCallBack("Btn_window_close");
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x000B0994 File Offset: 0x000AEB94
	private void SetAnimationCallBack(string objName)
	{
		UIPlayAnimation uiplayAnimation = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, objName);
		if (uiplayAnimation != null)
		{
			EventDelegate.Add(uiplayAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimationCallback), false);
		}
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x000B09D4 File Offset: 0x000AEBD4
	private void OnDestroy()
	{
		ChaoSetWindowUI.m_isActive = false;
		if (this.m_chaoTexture != null && this.m_chaoTexture.mainTexture != null)
		{
			this.m_chaoTexture.mainTexture = null;
		}
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x000B0A10 File Offset: 0x000AEC10
	private void OnSetChao(ChaoSetWindowUI.ChaoInfo chaoInfo)
	{
		this.m_chaoInfo = chaoInfo;
		ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(this.m_chaoTexture, null, true);
		ChaoTextureManager.Instance.GetTexture(this.m_chaoInfo.id, info);
		this.m_chaoTexture.enabled = true;
		if (this.m_chaoRankSprite != null)
		{
			this.m_chaoRankSprite.spriteName = "ui_chao_set_bg_ll_" + (int)this.m_chaoInfo.rarity;
		}
		if (this.m_chaoNameLabel != null)
		{
			this.m_chaoNameLabel.text = this.m_chaoInfo.name;
		}
		if (this.m_chaoLevelLabel != null)
		{
			this.m_chaoLevelLabel.text = TextUtility.GetTextLevel(this.m_chaoInfo.level.ToString());
		}
		string text = this.m_chaoInfo.charaAttribute.ToString().ToLower();
		if (this.m_chaoTypeSprite != null)
		{
			this.m_chaoTypeSprite.spriteName = "ui_chao_set_type_icon_" + text;
		}
		if (this.m_chaoTypeLabel != null)
		{
			this.m_chaoTypeLabel.text = TextUtility.GetCommonText("CharaAtribute", text);
		}
		if (this.m_chaoTexture != null)
		{
			if (this.m_chaoInfo.onMask)
			{
				this.m_chaoTexture.color = Color.black;
			}
			else
			{
				this.m_chaoTexture.color = Color.white;
			}
		}
		if (this.m_bonusLabel != null)
		{
			this.m_bonusLabel.text = this.m_chaoInfo.detail;
		}
		if (this.m_draggablePanel == null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "ScrollView");
			if (gameObject != null)
			{
				this.m_draggablePanel = gameObject.GetComponent<UIDraggablePanel>();
			}
		}
		if (this.m_draggablePanel != null)
		{
			this.m_draggablePanel.ResetPosition();
		}
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x000B0C10 File Offset: 0x000AEE10
	private void OnSetActiveButton(bool isActive)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_set_main");
		if (gameObject != null)
		{
			gameObject.SetActive(isActive);
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_set_sub");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(isActive);
		}
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x000B0C68 File Offset: 0x000AEE68
	private void OnClickMain()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		ChaoSetUI chaoSetUI = GameObjectUtil.FindGameObjectComponent<ChaoSetUI>("ChaoSetUI");
		if (chaoSetUI != null)
		{
			chaoSetUI.RegistChao(0, this.m_chaoInfo.id);
		}
		if (this.m_tutorial)
		{
			this.CreateTutorialWindow();
			TutorialCursor.EndTutorialCursor(TutorialCursor.Type.CHAOSELECT_MAIN);
		}
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x000B0CC8 File Offset: 0x000AEEC8
	private void OnClickSub()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		ChaoSetUI chaoSetUI = GameObjectUtil.FindGameObjectComponent<ChaoSetUI>("ChaoSetUI");
		if (chaoSetUI != null)
		{
			chaoSetUI.RegistChao(1, this.m_chaoInfo.id);
		}
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x000B0D10 File Offset: 0x000AEF10
	private void OnClickClose()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x000B0D24 File Offset: 0x000AEF24
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		if (this.m_chaoInfo.id == data.chao_id && this.m_chaoTexture != null)
		{
			this.m_chaoTexture.mainTexture = data.tex;
			this.m_chaoTexture.enabled = true;
		}
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x000B0D78 File Offset: 0x000AEF78
	private void OnFinishedAnimationCallback()
	{
		if (this.m_chaoTexture != null && this.m_chaoTexture.mainTexture != null)
		{
			this.m_chaoTexture.mainTexture = null;
			this.m_chaoTexture.enabled = false;
		}
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
		ChaoSetWindowUI.m_isActive = false;
		this.m_chaoInfo = default(ChaoSetWindowUI.ChaoInfo);
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x000B0DE4 File Offset: 0x000AEFE4
	private void CreateTutorialWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "ChaoTutorial",
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "tutorial_ready_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ChaoSet", "tutorial_ready_message").text,
			buttonType = GeneralWindow.ButtonType.Ok
		});
	}

	// Token: 0x04001B59 RID: 7001
	private static bool m_isActive;

	// Token: 0x04001B5A RID: 7002
	private ChaoSetWindowUI.ChaoInfo m_chaoInfo;

	// Token: 0x04001B5B RID: 7003
	[SerializeField]
	private UISprite m_chaoSprite;

	// Token: 0x04001B5C RID: 7004
	[SerializeField]
	private UITexture m_chaoTexture;

	// Token: 0x04001B5D RID: 7005
	[SerializeField]
	private UISprite m_chaoRankSprite;

	// Token: 0x04001B5E RID: 7006
	[SerializeField]
	private UILabel m_chaoNameLabel;

	// Token: 0x04001B5F RID: 7007
	[SerializeField]
	private UILabel m_chaoLevelLabel;

	// Token: 0x04001B60 RID: 7008
	[SerializeField]
	private UISprite m_chaoTypeSprite;

	// Token: 0x04001B61 RID: 7009
	[SerializeField]
	private UILabel m_chaoTypeLabel;

	// Token: 0x04001B62 RID: 7010
	[SerializeField]
	private UISprite m_bonusTypeSprite;

	// Token: 0x04001B63 RID: 7011
	[SerializeField]
	private UILabel m_bonusLabel;

	// Token: 0x04001B64 RID: 7012
	private UIDraggablePanel m_draggablePanel;

	// Token: 0x04001B65 RID: 7013
	private bool m_tutorial;

	// Token: 0x020003F4 RID: 1012
	public struct ChaoInfo
	{
		// Token: 0x06001E18 RID: 7704 RVA: 0x000B0E4C File Offset: 0x000AF04C
		public ChaoInfo(ChaoData chaoData)
		{
			this.id = chaoData.id;
			this.level = chaoData.level;
			this.rarity = chaoData.rarity;
			this.charaAttribute = chaoData.charaAtribute;
			this.name = chaoData.name;
			this.detail = chaoData.GetDetailLevelPlusSP(this.level);
			this.onMask = false;
		}

		// Token: 0x04001B66 RID: 7014
		public int id;

		// Token: 0x04001B67 RID: 7015
		public int level;

		// Token: 0x04001B68 RID: 7016
		public ChaoData.Rarity rarity;

		// Token: 0x04001B69 RID: 7017
		public CharacterAttribute charaAttribute;

		// Token: 0x04001B6A RID: 7018
		public string name;

		// Token: 0x04001B6B RID: 7019
		public string detail;

		// Token: 0x04001B6C RID: 7020
		public bool onMask;
	}

	// Token: 0x020003F5 RID: 1013
	public enum WindowType
	{
		// Token: 0x04001B6E RID: 7022
		WITH_BUTTON,
		// Token: 0x04001B6F RID: 7023
		WINDOW_ONLY
	}
}
