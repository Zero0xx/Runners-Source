using System;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using Text;
using UI;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class ConnectAlertMaskUI : MonoBehaviour
{
	// Token: 0x06001E29 RID: 7721 RVA: 0x000B199C File Offset: 0x000AFB9C
	private void Awake()
	{
		ConnectAlertMaskUI.s_instance = this;
		this.CheckChaoObject();
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x000B19AC File Offset: 0x000AFBAC
	private void OnDestroy()
	{
		if (ConnectAlertMaskUI.s_instance != null)
		{
			ConnectAlertMaskUI.s_instance.RemoveAtlasList();
			ConnectAlertMaskUI.s_instance.DeleteTexture();
		}
		ConnectAlertMaskUI.s_instance = null;
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x000B19E4 File Offset: 0x000AFBE4
	public void SetChaoInfo()
	{
		this.CheckChaoObject();
		if (this.m_chaoObject != null)
		{
			if (this.m_tipsObject != null)
			{
				this.m_tipsObject.SetActive(false);
			}
			this.SetChaoInfoData();
		}
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x000B1A2C File Offset: 0x000AFC2C
	private void CheckChaoObject()
	{
		if (this.m_chaoObject == null)
		{
			this.m_chaoObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_chao");
			if (this.m_chaoObject != null)
			{
				this.m_lblName = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_chaoObject, "Lbl_chao_name");
				this.m_lblBonus = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_chaoObject, "Lbl_chao_bonus");
				this.m_imgBg = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_chaoObject, "img_chao_rank_bg");
				this.m_imgIcon = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_chaoObject, "img_chao_type");
				this.m_imgChao = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_chaoObject, "img_chao");
			}
		}
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x000B1AE0 File Offset: 0x000AFCE0
	private bool SetChaoInfoData()
	{
		int loadingChaoId = ChaoTextureManager.Instance.LoadingChaoId;
		ChaoData chaoData = ChaoTable.GetChaoData(loadingChaoId);
		global::Debug.Log("ConnectAlertMaskUI:SetChaoInfoData  DisplayChaoID = " + loadingChaoId.ToString());
		if (chaoData != null)
		{
			this.SetEventBanner();
			if (this.m_chaoObject != null)
			{
				this.m_chaoObject.SetActive(true);
			}
			this.m_lblName.text = chaoData.name;
			int chaoLevel = ChaoTable.ChaoMaxLevel();
			this.m_lblBonus.text = chaoData.GetLoadingPageDetailLevelPlusSP(chaoLevel);
			if (!string.IsNullOrEmpty(this.m_lblBonus.text))
			{
				UILabel lblBonus = this.m_lblBonus;
				lblBonus.text = lblBonus.text + "\n" + TextUtility.GetChaoText("Chao", "level_max");
			}
			switch (chaoData.rarity)
			{
			case ChaoData.Rarity.NORMAL:
				this.m_imgBg.spriteName = "ui_chao_set_bg_load_0";
				break;
			case ChaoData.Rarity.RARE:
				this.m_imgBg.spriteName = "ui_chao_set_bg_load_1";
				break;
			case ChaoData.Rarity.SRARE:
				this.m_imgBg.spriteName = "ui_chao_set_bg_load_2";
				break;
			}
			switch (chaoData.charaAtribute)
			{
			case CharacterAttribute.SPEED:
				this.m_imgIcon.spriteName = "ui_chao_set_type_icon_speed";
				break;
			case CharacterAttribute.FLY:
				this.m_imgIcon.spriteName = "ui_chao_set_type_icon_fly";
				break;
			case CharacterAttribute.POWER:
				this.m_imgIcon.spriteName = "ui_chao_set_type_icon_power";
				break;
			}
			if (this.m_imgChao != null)
			{
				this.m_chaoId = ChaoWindowUtility.GetIdFromServerId(chaoData.id + 400000);
				ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(this.m_imgChao, null, true);
				ChaoTextureManager.Instance.GetTexture(this.m_chaoId, info);
				this.m_imgChao.enabled = true;
			}
			return true;
		}
		global::Debug.Log("ConnectAlertMaskUI:SetChaoInfoData  ChaoInfoData is Null!!");
		if (this.m_chaoObject != null)
		{
			this.m_chaoObject.SetActive(false);
		}
		return false;
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x000B1CE8 File Offset: 0x000AFEE8
	public void SetTipsInfo()
	{
		this.CheckTipsObject();
		if (this.m_tipsObject != null)
		{
			if (this.m_chaoObject != null)
			{
				this.m_chaoObject.SetActive(false);
			}
			this.SetTipsInfoData();
		}
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x000B1D30 File Offset: 0x000AFF30
	private void CheckTipsObject()
	{
		this.m_tipsObject = GameObjectUtil.FindChildGameObject(base.gameObject, "info_tips");
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x000B1D48 File Offset: 0x000AFF48
	private void SetTipsInfoData()
	{
		this.SetEventBanner();
		this.m_tipsObject.SetActive(true);
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_tipsObject, "Lbl_tips_body");
		if (uilabel != null)
		{
			int categoryCellCount = TextManager.GetCategoryCellCount(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tips");
			string text = string.Empty;
			if (categoryCellCount > 0)
			{
				int num = UnityEngine.Random.Range(1, categoryCellCount);
				string cellID = "tips_message_" + num;
				text = TextUtility.GetCommonText("Tips", cellID);
			}
			if (text != null)
			{
				uilabel.text = text;
			}
		}
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x000B1DD4 File Offset: 0x000AFFD4
	public void SetTipsCategory(ConnectAlertMaskUI.dispCategory category)
	{
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_tips_category");
		if (uilabel != null)
		{
			if (category == ConnectAlertMaskUI.dispCategory.CHAO_INFO)
			{
				uilabel.text = TextUtility.GetCommonText("MainMenu", "loading_chaoInfo_caption");
				this.SetChaoInfo();
			}
			else
			{
				uilabel.text = TextUtility.GetCommonText("MainMenu", "loading_tipsInfo_caption");
				this.SetTipsInfo();
			}
		}
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x000B1E40 File Offset: 0x000B0040
	public void PlayReverse()
	{
		ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_screenAnimation, Direction.Reverse);
		if (activeAnimation != null)
		{
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedAnimation), true);
		}
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x000B1E80 File Offset: 0x000B0080
	private void SetEventBanner()
	{
		bool eventObject = false;
		if (EventManager.Instance != null && EventManager.Instance.Type != EventManager.EventType.UNKNOWN)
		{
			eventObject = EventManager.Instance.IsInEvent();
			if (this.m_eventObject != null && EventManager.Instance.Type == EventManager.EventType.ADVERT)
			{
				string advertEventTitleText = this.GetAdvertEventTitleText();
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_eventObject, "ui_Lbl_event_caption");
				if (uilabel != null)
				{
					uilabel.text = advertEventTitleText;
				}
				UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_eventObject, "ui_Lbl_event_caption_sh");
				if (uilabel2 != null)
				{
					uilabel2.text = advertEventTitleText;
				}
				UILocalizeText uilocalizeText = GameObjectUtil.FindChildGameObjectComponent<UILocalizeText>(this.m_eventObject, "ui_Lbl_event_caption");
				if (uilocalizeText != null)
				{
					uilocalizeText.enabled = false;
				}
				UILocalizeText uilocalizeText2 = GameObjectUtil.FindChildGameObjectComponent<UILocalizeText>(this.m_eventObject, "ui_Lbl_event_caption_sh");
				if (uilocalizeText2 != null)
				{
					uilocalizeText2.enabled = false;
				}
			}
		}
		this.SetEventObject(eventObject);
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x000B1F80 File Offset: 0x000B0180
	private string GetAdvertEventTitleText()
	{
		string result = string.Empty;
		if (EventManager.Instance != null)
		{
			EventManager.AdvertEventType advertType = EventManager.Instance.AdvertType;
			if (advertType != EventManager.AdvertEventType.UNKNOWN)
			{
				switch (advertType)
				{
				case EventManager.AdvertEventType.ROULETTE:
				{
					EyeCatcherCharaData[] eyeCatcherCharaDatas = EventManager.Instance.GetEyeCatcherCharaDatas();
					if (eyeCatcherCharaDatas != null)
					{
						bool flag = false;
						foreach (EyeCatcherCharaData eyeCatcherCharaData in eyeCatcherCharaDatas)
						{
							if (eyeCatcherCharaData.id >= -1 && eyeCatcherCharaData.id != 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "ui_Lbl_word_header_event_roulette_c");
						}
						else
						{
							result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "ui_Lbl_word_header_event_roulette_o");
						}
					}
					else
					{
						result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "ui_Lbl_word_header_event_roulette_o");
					}
					break;
				}
				case EventManager.AdvertEventType.CHARACTER:
					result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "ui_Lbl_word_header_event_character");
					break;
				case EventManager.AdvertEventType.SHOP:
					result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "ui_Lbl_word_header_event_shop");
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000B209C File Offset: 0x000B029C
	public void SetEventObject(bool enabled)
	{
		if (this.m_eventObject != null)
		{
			if (enabled && AtlasManager.Instance != null)
			{
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_eventObject, "bg_deco");
				if (uisprite != null)
				{
					this.m_atlas.Add(uisprite.atlas);
				}
				if (this.m_eventLogImg != null)
				{
					this.m_atlas.Add(this.m_eventLogImg.atlas);
				}
				AtlasManager.Instance.ReplaceAtlasForMenuLoading(this.m_atlas.ToArray());
			}
			this.m_eventObject.SetActive(enabled);
		}
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x000B2148 File Offset: 0x000B0348
	private void OnFinishedAnimation()
	{
		if (this.m_onFinishedFadeOutCallbackAction != null)
		{
			this.m_onFinishedFadeOutCallbackAction();
		}
		this.DeleteTexture();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x000B2174 File Offset: 0x000B0374
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		if (this.m_chaoId == data.chao_id && this.m_imgChao != null)
		{
			this.m_imgChao.enabled = true;
			this.m_imgChao.mainTexture = data.tex;
		}
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x000B21C0 File Offset: 0x000B03C0
	private void RemoveAtlasList()
	{
		if (this.m_eventObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(this.m_eventObject, "bg_deco");
			if (uisprite != null)
			{
				uisprite.atlas = null;
			}
		}
		if (this.m_eventLogImg != null)
		{
			this.m_eventLogImg.atlas = null;
		}
		this.m_atlas.Clear();
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x000B222C File Offset: 0x000B042C
	private void DeleteTexture()
	{
		if (this.m_imgChao != null && this.m_imgChao.mainTexture != null)
		{
			this.m_imgChao.mainTexture = null;
		}
		if (this.m_eventLogImg != null)
		{
		}
		if (this.m_chaoId > 0)
		{
			ChaoTextureManager.Instance.RemoveChaoTexture(this.m_chaoId);
		}
		this.m_chaoId = -1;
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x000B22A0 File Offset: 0x000B04A0
	public static void StartScreen()
	{
		if (ConnectAlertMaskUI.s_instance != null)
		{
			ConnectAlertMaskUI.s_instance.m_alertGameObject.SetActive(true);
			ConnectAlertMaskUI.dispCategory dispCategory = ConnectAlertMaskUI.dispCategory.CHAO_INFO;
			ConnectAlertMaskUI.s_instance.SetTipsCategory(dispCategory);
			global::Debug.Log("ConnectAlertMaskUI:StartScreen  dispCategory = " + dispCategory.ToString());
		}
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x000B22F4 File Offset: 0x000B04F4
	public static void EndScreen(Action onFinishedFadeOutCallbackAction = null)
	{
		if (ConnectAlertMaskUI.s_instance != null)
		{
			ConnectAlertMaskUI.s_instance.SetEventObject(false);
			ConnectAlertMaskUI.s_instance.m_onFinishedFadeOutCallbackAction = onFinishedFadeOutCallbackAction;
			ConnectAlertMaskUI.s_instance.PlayReverse();
		}
	}

	// Token: 0x04001B88 RID: 7048
	[SerializeField]
	private GameObject m_alertGameObject;

	// Token: 0x04001B89 RID: 7049
	[SerializeField]
	private GameObject m_eventObject;

	// Token: 0x04001B8A RID: 7050
	[SerializeField]
	private Animation m_screenAnimation;

	// Token: 0x04001B8B RID: 7051
	[SerializeField]
	private UISprite m_eventLogImg;

	// Token: 0x04001B8C RID: 7052
	private static ConnectAlertMaskUI s_instance;

	// Token: 0x04001B8D RID: 7053
	private GameObject m_chaoObject;

	// Token: 0x04001B8E RID: 7054
	private GameObject m_tipsObject;

	// Token: 0x04001B8F RID: 7055
	private UILabel m_lblName;

	// Token: 0x04001B90 RID: 7056
	private UILabel m_lblBonus;

	// Token: 0x04001B91 RID: 7057
	private UISprite m_imgBg;

	// Token: 0x04001B92 RID: 7058
	private UISprite m_imgIcon;

	// Token: 0x04001B93 RID: 7059
	private UITexture m_imgChao;

	// Token: 0x04001B94 RID: 7060
	private int m_chaoId = -1;

	// Token: 0x04001B95 RID: 7061
	private List<UIAtlas> m_atlas = new List<UIAtlas>();

	// Token: 0x04001B96 RID: 7062
	private Action m_onFinishedFadeOutCallbackAction;

	// Token: 0x020003F9 RID: 1017
	public enum dispCategory
	{
		// Token: 0x04001B98 RID: 7064
		CHAO_INFO,
		// Token: 0x04001B99 RID: 7065
		TIPS_INFO,
		// Token: 0x04001B9A RID: 7066
		END
	}
}
