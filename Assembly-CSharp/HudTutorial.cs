using System;
using Message;
using SaveData;
using Text;
using Tutorial;
using UnityEngine;

// Token: 0x020003C5 RID: 965
public class HudTutorial : MonoBehaviour
{
	// Token: 0x06001C19 RID: 7193 RVA: 0x000A72B0 File Offset: 0x000A54B0
	public static HudTutorial.Kind GetKind(HudTutorial.Id id)
	{
		if (id == HudTutorial.Id.NONE)
		{
			return HudTutorial.Kind.NONE;
		}
		if (id < HudTutorial.Id.MISSION_END)
		{
			return HudTutorial.Kind.MISSION;
		}
		if (id == HudTutorial.Id.MISSION_END)
		{
			return HudTutorial.Kind.MISSION_END;
		}
		if (id < HudTutorial.Id.MAPBOSS_1)
		{
			return HudTutorial.Kind.FEVERBOSS;
		}
		if (id < HudTutorial.Id.EVENTBOSS_1)
		{
			return HudTutorial.Kind.MAPBOSS;
		}
		if (id < HudTutorial.Id.ITEM_1)
		{
			return HudTutorial.Kind.EVENTBOSS;
		}
		if (id < HudTutorial.Id.CHARA_0)
		{
			return HudTutorial.Kind.ITEM;
		}
		if (id < HudTutorial.Id.ACTION_1)
		{
			return HudTutorial.Kind.CHARA;
		}
		if (id < HudTutorial.Id.ITEM_BUTTON_1)
		{
			return HudTutorial.Kind.ACTION;
		}
		if (id < HudTutorial.Id.QUICK_1)
		{
			return HudTutorial.Kind.ITEM_BUTTON;
		}
		return HudTutorial.Kind.QUICK;
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000A7320 File Offset: 0x000A5520
	private HudTutorial.Kind kind
	{
		get
		{
			return HudTutorial.GetKind(this.m_id);
		}
	}

	// Token: 0x06001C1B RID: 7195 RVA: 0x000A7330 File Offset: 0x000A5530
	private void Start()
	{
		this.Initialize();
	}

	// Token: 0x06001C1C RID: 7196 RVA: 0x000A7338 File Offset: 0x000A5538
	private void SetTexture(string sceneName, int index)
	{
		this.m_explanPattern.m_texture.mainTexture = null;
		GameObject gameObject = GameObject.Find(sceneName);
		if (gameObject == null)
		{
			GameObject gameObject2 = GameObject.Find("EventResourceCommon");
			if (gameObject2 != null)
			{
				for (int i = 0; i < gameObject2.transform.childCount; i++)
				{
					GameObject gameObject3 = gameObject2.transform.GetChild(i).gameObject;
					if (gameObject3.name == sceneName)
					{
						gameObject = gameObject3;
						break;
					}
				}
			}
		}
		if (gameObject != null)
		{
			HudTutorialTexture component = gameObject.GetComponent<HudTutorialTexture>();
			if (component != null && index < component.m_texList.Length)
			{
				this.m_explanPattern.m_texture.mainTexture = component.m_texList[index];
			}
		}
		this.m_explanPattern.m_texture.enabled = true;
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x000A7420 File Offset: 0x000A5620
	private void SetTexture(int index)
	{
		HudTutorial.TutorialData tutorialData = HudTutorial.GetTutorialData(this.m_id);
		if (tutorialData == null)
		{
			return;
		}
		int index2;
		if (index != 0)
		{
			if (index != 1)
			{
				return;
			}
			index2 = tutorialData.m_textureNumber2;
		}
		else
		{
			index2 = tutorialData.m_textureNumber1;
		}
		switch (this.kind)
		{
		case HudTutorial.Kind.MISSION:
			this.SetTexture(HudTutorial.MISSION_SCENENAME, index2);
			break;
		case HudTutorial.Kind.FEVERBOSS:
			this.SetTexture(HudTutorial.FEVERBOSS_SCENENAME, index2);
			break;
		case HudTutorial.Kind.MAPBOSS:
			this.SetTexture(HudTutorial.MAPBOSS_SCENENAME + (this.m_id - HudTutorial.Id.MAPBOSS_1).ToString(), index2);
			break;
		case HudTutorial.Kind.EVENTBOSS:
			this.SetTexture(HudTutorial.EVENTBOSS_SCENENAME + (this.m_id - HudTutorial.Id.EVENTBOSS_1).ToString(), index2);
			break;
		case HudTutorial.Kind.ITEM:
			this.SetTexture(HudTutorial.ITEM_SCENENAME, index2);
			break;
		case HudTutorial.Kind.CHARA:
			this.SetTexture(HudTutorial.CHARA_SCENENAME + (this.m_id - HudTutorial.Id.CHARA_0).ToString(), index2);
			break;
		case HudTutorial.Kind.ACTION:
			this.SetTexture(HudTutorial.ACTION_SCENENAME + (this.m_id - HudTutorial.Id.ACTION_1 + 1).ToString(), index2);
			break;
		case HudTutorial.Kind.QUICK:
			this.SetTexture(HudTutorial.QUICK_SCENENAME + (this.m_id - HudTutorial.Id.QUICK_1 + 1).ToString(), index2);
			break;
		}
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x000A75A8 File Offset: 0x000A57A8
	private void Update()
	{
		if (this.m_id != HudTutorial.Id.NONE)
		{
			HudTutorial.Phase phase = this.m_phase;
			switch (phase + 1)
			{
			case HudTutorial.Phase.OPEN:
				switch (this.kind)
				{
				case HudTutorial.Kind.MISSION:
				case HudTutorial.Kind.FEVERBOSS:
				case HudTutorial.Kind.MAPBOSS:
				case HudTutorial.Kind.ITEM:
				case HudTutorial.Kind.CHARA:
				case HudTutorial.Kind.ACTION:
				case HudTutorial.Kind.QUICK:
					this.m_captionGameObject.SetActive(true);
					this.m_captionLabel.text = HudTutorial.GetCaptionText(this.m_id, 0);
					this.m_explanGameObject.SetActive(true);
					this.m_explanPattern = this.m_explanPatterns[2];
					this.m_explanPattern.m_gameObject.SetActive(true);
					this.m_explanPattern.m_label.text = HudTutorial.GetExplainText(this.m_id, 0);
					this.SetTexture(0);
					break;
				case HudTutorial.Kind.MISSION_END:
					this.m_explanGameObject.SetActive(true);
					this.m_explanPattern = this.m_explanPatterns[0];
					this.m_explanPattern.m_gameObject.SetActive(true);
					this.m_explanPattern.m_label.text = HudTutorial.GetExplainText(this.m_id, 0);
					break;
				case HudTutorial.Kind.EVENTBOSS:
					this.m_captionGameObject.SetActive(true);
					this.m_captionLabel.text = HudTutorial.GetEventBossCaptionText(this.m_id, this.m_bossType, 0);
					this.m_explanGameObject.SetActive(true);
					this.m_explanPattern = this.m_explanPatterns[2];
					this.m_explanPattern.m_gameObject.SetActive(true);
					this.m_explanPattern.m_label.text = HudTutorial.GetEventBossExplainText(this.m_id, this.m_bossType, 0);
					this.SetTexture(0);
					break;
				}
				if (this.kind == HudTutorial.Kind.ITEM_BUTTON)
				{
					this.m_phase = HudTutorial.Phase.CLOSE;
				}
				else
				{
					this.m_textureCount = HudTutorial.GetTexuterPageCount(this.m_id);
					this.m_currentTextureIndex = 0;
					this.m_timer = 1f;
					this.m_phase = HudTutorial.Phase.OPEN;
				}
				break;
			case HudTutorial.Phase.OPEN_WAIT:
				if (this.m_anchorObj != null)
				{
					this.m_anchorObj.SetActive(true);
				}
				this.m_phase = HudTutorial.Phase.OPEN_WAIT;
				break;
			case HudTutorial.Phase.WAIT:
				this.m_timer -= RealTime.deltaTime;
				if (this.m_timer <= 0f)
				{
					this.m_phase = HudTutorial.Phase.WAIT;
				}
				break;
			case HudTutorial.Phase.CLOSE:
				this.m_timer -= RealTime.deltaTime;
				if (this.m_timer <= 0f)
				{
					this.m_phase = HudTutorial.Phase.CLOSE;
				}
				break;
			case HudTutorial.Phase.PLAY:
				this.m_phase = ((this.kind != HudTutorial.Kind.MISSION) ? HudTutorial.Phase.END : HudTutorial.Phase.PLAY);
				if (this.kind == HudTutorial.Kind.ITEM_BUTTON)
				{
					GameObjectUtil.SendMessageFindGameObject("HudCockpit", "OnNextTutorial", null, SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					HudTutorial.TutorialData tutorialData = HudTutorial.GetTutorialData(this.m_id);
					if (tutorialData != null)
					{
						MsgTutorialPlayAction value = new MsgTutorialPlayAction(tutorialData.m_eventID);
						GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialPlayAction", value, SendMessageOptions.DontRequireReceiver);
					}
				}
				break;
			case HudTutorial.Phase.RETRY:
				if (this.m_id != HudTutorial.Id.MISSION_8)
				{
					SoundManager.SePlay("sys_clear", "SE");
					this.m_successGameObject.SetActive(true);
				}
				this.m_phase = HudTutorial.Phase.RESULT;
				break;
			case HudTutorial.Phase.RESULT:
				if (this.m_id != HudTutorial.Id.MISSION_8)
				{
					SoundManager.SePlay("sys_retry", "SE");
					this.m_retryGameObject.SetActive(true);
				}
				this.m_phase = HudTutorial.Phase.RESULT;
				break;
			case (HudTutorial.Phase)10:
				if (this.m_anchorObj != null)
				{
					this.m_anchorObj.SetActive(false);
				}
				this.m_phase = HudTutorial.Phase.NONE;
				this.m_id = HudTutorial.Id.NONE;
				break;
			}
		}
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x000A7964 File Offset: 0x000A5B64
	private void Initialize()
	{
		this.m_id = HudTutorial.Id.NONE;
		this.m_phase = HudTutorial.Phase.NONE;
		this.m_captionGameObject.SetActive(false);
		this.m_explanGameObject.SetActive(false);
		foreach (HudTutorial.ExplanPattern explanPattern in this.m_explanPatterns)
		{
			explanPattern.m_gameObject.SetActive(false);
			if (explanPattern.m_label != null)
			{
				explanPattern.m_label.text = null;
			}
			if (explanPattern.m_texture != null)
			{
				explanPattern.m_texture.enabled = false;
			}
		}
		this.m_successGameObject.SetActive(false);
		this.m_retryGameObject.SetActive(false);
		if (this.m_anchorObj != null)
		{
			this.m_anchorObj.SetActive(false);
		}
	}

	// Token: 0x06001C20 RID: 7200 RVA: 0x000A7A34 File Offset: 0x000A5C34
	private void OnClickScreen()
	{
		if (this.m_phase == HudTutorial.Phase.WAIT)
		{
			bool flag = false;
			if (this.m_textureCount > 1 && this.m_currentTextureIndex < this.m_textureCount - 1)
			{
				flag = true;
			}
			if (flag)
			{
				this.m_currentTextureIndex++;
				switch (this.kind)
				{
				case HudTutorial.Kind.MISSION:
					this.m_explanPattern.m_label.text = HudTutorial.GetExplainText(this.m_id, this.m_currentTextureIndex);
					break;
				case HudTutorial.Kind.MAPBOSS:
					this.m_explanPattern.m_label.text = HudTutorial.GetExplainText(this.m_id, this.m_currentTextureIndex);
					break;
				case HudTutorial.Kind.EVENTBOSS:
					this.m_explanPattern.m_label.text = HudTutorial.GetEventBossExplainText(this.m_id, this.m_bossType, this.m_currentTextureIndex);
					break;
				}
				this.SetTexture(this.m_currentTextureIndex);
				this.m_timer = 0.5f;
				this.m_phase = HudTutorial.Phase.OPEN_WAIT;
			}
			else
			{
				this.OnClose();
				if (this.kind == HudTutorial.Kind.ITEM || this.kind == HudTutorial.Kind.CHARA || this.kind == HudTutorial.Kind.ACTION || this.kind == HudTutorial.Kind.ITEM_BUTTON || this.kind == HudTutorial.Kind.QUICK)
				{
					this.m_timer = 0.3f;
				}
				else
				{
					this.m_timer = 0f;
				}
				this.m_phase = HudTutorial.Phase.CLOSE_WAIT;
			}
		}
	}

	// Token: 0x06001C21 RID: 7201 RVA: 0x000A7BAC File Offset: 0x000A5DAC
	private void OnClose()
	{
		switch (this.kind)
		{
		case HudTutorial.Kind.MISSION:
			if (this.m_id == HudTutorial.Id.MISSION_8)
			{
				this.m_captionGameObject.SetActive(false);
			}
			break;
		case HudTutorial.Kind.MAPBOSS:
		case HudTutorial.Kind.EVENTBOSS:
		case HudTutorial.Kind.ITEM:
		case HudTutorial.Kind.ITEM_BUTTON:
		case HudTutorial.Kind.CHARA:
		case HudTutorial.Kind.ACTION:
		case HudTutorial.Kind.QUICK:
			this.m_captionGameObject.SetActive(false);
			break;
		}
		this.m_explanGameObject.SetActive(false);
		this.m_explanPattern.m_gameObject.SetActive(false);
	}

	// Token: 0x06001C22 RID: 7202 RVA: 0x000A7C40 File Offset: 0x000A5E40
	private void OnStartTutorial(MsgTutorialHudStart msg)
	{
		this.Initialize();
		if (this.m_phase == HudTutorial.Phase.NONE)
		{
			this.m_id = msg.m_id;
			this.m_bossType = msg.m_bossType;
		}
	}

	// Token: 0x06001C23 RID: 7203 RVA: 0x000A7C78 File Offset: 0x000A5E78
	private void OnSuccessTutorial()
	{
		if (this.m_phase == HudTutorial.Phase.PLAY)
		{
			this.m_phase = HudTutorial.Phase.SUCCESS;
		}
	}

	// Token: 0x06001C24 RID: 7204 RVA: 0x000A7C90 File Offset: 0x000A5E90
	private void OnRetryTutorial()
	{
		if (this.m_phase == HudTutorial.Phase.PLAY)
		{
			this.m_phase = HudTutorial.Phase.RETRY;
		}
	}

	// Token: 0x06001C25 RID: 7205 RVA: 0x000A7CA8 File Offset: 0x000A5EA8
	private void OnEndTutorial()
	{
		this.Initialize();
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x000A7CB0 File Offset: 0x000A5EB0
	private void OnPushBackKey()
	{
		this.OnClickScreen();
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x000A7CB8 File Offset: 0x000A5EB8
	private static void SetUIEffect(bool flag)
	{
		if (UIEffectManager.Instance != null)
		{
			UIEffectManager.Instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, flag);
		}
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
	public static void StartTutorial(HudTutorial.Id id, BossType bossType)
	{
		HudTutorial.SetUIEffect(false);
		GameObjectUtil.SendMessageFindGameObject("HudTutorial", "OnStartTutorial", new MsgTutorialHudStart(id, bossType), SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x000A7CF8 File Offset: 0x000A5EF8
	public static void SuccessTutorial()
	{
		GameObjectUtil.SendMessageFindGameObject("HudTutorial", "OnSuccessTutorial", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x000A7D0C File Offset: 0x000A5F0C
	public static void RetryTutorial()
	{
		GameObjectUtil.SendMessageFindGameObject("HudTutorial", "OnRetryTutorial", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x000A7D20 File Offset: 0x000A5F20
	public static void EndTutorial()
	{
		HudTutorial.SetUIEffect(true);
		GameObjectUtil.SendMessageFindGameObject("HudTutorial", "OnEndTutorial", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x000A7D3C File Offset: 0x000A5F3C
	public static void PushBackKey()
	{
		GameObjectUtil.SendMessageFindGameObject("HudTutorial", "OnPushBackKey", null, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x000A7D50 File Offset: 0x000A5F50
	public static void Load(ResourceSceneLoader loaderComponent, bool turorial, bool bossStage, BossType tutorialBossType, CharaType mainChara, CharaType subChara)
	{
		bool onAssetBundle = true;
		if (loaderComponent != null)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (turorial)
			{
				flag = true;
				flag3 = true;
				loaderComponent.AddLoad(HudTutorial.FEVERBOSS_SCENENAME, onAssetBundle, false);
			}
			else if (tutorialBossType == BossType.MAP1 || tutorialBossType == BossType.MAP2 || tutorialBossType == BossType.MAP3)
			{
				flag2 = true;
				flag4 = true;
			}
			else if (bossStage)
			{
				flag4 = true;
			}
			else
			{
				flag3 = true;
				flag4 = true;
				if (StageModeManager.Instance != null && StageModeManager.Instance.IsQuickMode())
				{
					for (int i = 0; i < HudTutorial.QUICK_MODE_TUTORIAL_DATA_TBL.Length; i++)
					{
						if (HudTutorial.IsQuickModeTutorial(HudTutorial.Id.QUICK_1 + i))
						{
							loaderComponent.AddLoad(HudTutorial.QUICK_SCENENAME + (i + 1).ToString(), onAssetBundle, false);
						}
					}
				}
				for (int j = 0; j < HudTutorial.ACTION_TUTORIAL_DATA_TBL.Length; j++)
				{
					if (HudTutorial.IsActionTutorial(HudTutorial.Id.ACTION_1 + j))
					{
						loaderComponent.AddLoad(HudTutorial.ACTION_SCENENAME + (j + 1).ToString(), onAssetBundle, false);
					}
				}
				if (tutorialBossType == BossType.FEVER)
				{
					loaderComponent.AddLoad(HudTutorial.FEVERBOSS_SCENENAME, onAssetBundle, false);
				}
			}
			if (flag)
			{
				loaderComponent.AddLoad(HudTutorial.MISSION_SCENENAME, onAssetBundle, false);
			}
			if (flag2)
			{
				int num = tutorialBossType - BossType.MAP1;
				loaderComponent.AddLoad(HudTutorial.MAPBOSS_SCENENAME + num, onAssetBundle, false);
			}
			if (flag3 && HudTutorial.IsItemTutorial())
			{
				loaderComponent.AddLoad(HudTutorial.ITEM_SCENENAME, onAssetBundle, false);
			}
			if (flag4)
			{
				if (HudTutorial.IsCharaTutorial(mainChara))
				{
					int charaTutorialIndex = HudTutorial.GetCharaTutorialIndex(mainChara);
					if (charaTutorialIndex >= 0)
					{
						loaderComponent.AddLoad(HudTutorial.CHARA_SCENENAME + charaTutorialIndex, onAssetBundle, false);
					}
				}
				if (HudTutorial.IsCharaTutorial(subChara))
				{
					int charaTutorialIndex2 = HudTutorial.GetCharaTutorialIndex(subChara);
					if (charaTutorialIndex2 >= 0)
					{
						loaderComponent.AddLoad(HudTutorial.CHARA_SCENENAME + charaTutorialIndex2, onAssetBundle, false);
					}
				}
			}
		}
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x000A7F5C File Offset: 0x000A615C
	public static HudTutorial.TutorialData GetTutorialData(HudTutorial.Id id)
	{
		if ((ulong)id < (ulong)((long)HudTutorial.TUTORIAL_DATA_TBL.Length))
		{
			return HudTutorial.TUTORIAL_DATA_TBL[(int)id];
		}
		return null;
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x000A7F78 File Offset: 0x000A6178
	public static string GetLoadSceneName(BossType bossType)
	{
		if (bossType != BossType.NONE)
		{
			int num = bossType - BossType.MAP1;
			return HudTutorial.MAPBOSS_SCENENAME + num;
		}
		return string.Empty;
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x000A7FA8 File Offset: 0x000A61A8
	public static string GetLoadSceneName(CharaType charaType)
	{
		if (charaType != CharaType.UNKNOWN)
		{
			int charaTutorialIndex = HudTutorial.GetCharaTutorialIndex(charaType);
			return HudTutorial.CHARA_SCENENAME + charaTutorialIndex;
		}
		return string.Empty;
	}

	// Token: 0x06001C31 RID: 7217 RVA: 0x000A7FDC File Offset: 0x000A61DC
	public static string GetLoadQuickModeSceneName(HudTutorial.Id quickID)
	{
		int num = quickID - HudTutorial.Id.QUICK_1;
		if (0 <= num && num < HudTutorial.QUICK_MODE_TUTORIAL_DATA_TBL.Length)
		{
			return HudTutorial.QUICK_SCENENAME + (num + 1).ToString();
		}
		return string.Empty;
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x000A8020 File Offset: 0x000A6220
	public static int GetTexuterPageCount(HudTutorial.Id id)
	{
		HudTutorial.TutorialData tutorialData = HudTutorial.GetTutorialData(id);
		if (tutorialData != null)
		{
			return tutorialData.m_textureCount;
		}
		return 0;
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x000A8044 File Offset: 0x000A6244
	public static string GetCaptionText(HudTutorial.Id id, int page = 0)
	{
		string result = string.Empty;
		switch (HudTutorial.GetKind(id))
		{
		case HudTutorial.Kind.MISSION:
			result = TextUtility.GetCommonText("Tutorial", "caption" + (int)(id + 1));
			break;
		case HudTutorial.Kind.FEVERBOSS:
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_explan2_2");
			string commonText = TextUtility.GetCommonText("BossName", "ferver");
			text.ReplaceTag("{PARAM_NAME}", commonText);
			result = text.text;
			break;
		}
		case HudTutorial.Kind.MAPBOSS:
		{
			BossType type = BossType.MAP1;
			switch (id)
			{
			case HudTutorial.Id.MAPBOSS_1:
				type = BossType.MAP1;
				break;
			case HudTutorial.Id.MAPBOSS_2:
				type = BossType.MAP2;
				break;
			case HudTutorial.Id.MAPBOSS_3:
				type = BossType.MAP3;
				break;
			}
			TextObject text2 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_boss");
			text2.ReplaceTag("{PARAM_NAME}", BossTypeUtil.GetTextCommonBossName(type));
			result = text2.text;
			break;
		}
		case HudTutorial.Kind.ITEM:
		{
			string itemTutorialCaptionText = HudTutorial.GetItemTutorialCaptionText(id);
			TextObject text3 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", itemTutorialCaptionText);
			text3.ReplaceTag("{PARAM_NAME}", HudTutorial.GetItemTutorialText(id));
			result = text3.text;
			break;
		}
		case HudTutorial.Kind.ITEM_BUTTON:
		{
			TextObject text4 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_explan3");
			result = text4.text;
			break;
		}
		case HudTutorial.Kind.CHARA:
		{
			CharaType commonTextCharaName = HudTutorial.GetCommonTextCharaName(id);
			string text5 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "CharaName", CharaName.Name[(int)commonTextCharaName]).text;
			TextObject text6 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_explan1");
			text6.ReplaceTag("{PARAM_NAME}", text5);
			result = text6.text;
			break;
		}
		case HudTutorial.Kind.ACTION:
		{
			TextObject text7 = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_explan2_2");
			text7.ReplaceTag("{PARAM_NAME}", HudTutorial.GetActionTutorialText(id));
			result = text7.text;
			break;
		}
		case HudTutorial.Kind.QUICK:
		{
			string commonText2 = TextUtility.GetCommonText("Tutorial", "caption_quickmode_tutorial");
			result = TextUtility.GetCommonText("Tutorial", "caption_explan2", "{PARAM_NAME}", commonText2);
			break;
		}
		}
		return result;
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x000A8264 File Offset: 0x000A6464
	public static string GetEventBossCaptionText(HudTutorial.Id id, BossType bossType, int page = 0)
	{
		string result = string.Empty;
		HudTutorial.Kind kind = HudTutorial.GetKind(id);
		if (kind == HudTutorial.Kind.EVENTBOSS)
		{
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Tutorial", "caption_boss");
			text.ReplaceTag("{PARAM_NAME}", BossTypeUtil.GetTextCommonBossCharaName(bossType));
			result = text.text;
		}
		return result;
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x000A82BC File Offset: 0x000A64BC
	public static string GetExplainText(HudTutorial.Id id, int page = 0)
	{
		string result = string.Empty;
		switch (HudTutorial.GetKind(id))
		{
		case HudTutorial.Kind.MISSION:
			if (page == 0)
			{
				result = TextUtility.GetCommonText("Tutorial", "explan" + (int)(id + 1));
			}
			else
			{
				string arg = "_" + page.ToString();
				result = TextUtility.GetCommonText("Tutorial", "explan" + (int)(id + 1) + arg);
			}
			break;
		case HudTutorial.Kind.MISSION_END:
			result = TextUtility.GetCommonText("Tutorial", "end");
			break;
		case HudTutorial.Kind.FEVERBOSS:
			result = TextUtility.GetCommonText("Tutorial", "fever_boss");
			break;
		case HudTutorial.Kind.MAPBOSS:
			if (page == 0)
			{
				result = TextUtility.GetCommonText("Tutorial", "boss" + (id - HudTutorial.Id.MAPBOSS_1 + 1));
			}
			else
			{
				string arg2 = "_" + page.ToString();
				result = TextUtility.GetCommonText("Tutorial", "boss" + (id - HudTutorial.Id.MAPBOSS_1 + 1) + arg2);
			}
			break;
		case HudTutorial.Kind.ITEM:
			result = TextUtility.GetCommonText("Tutorial", "item_" + (id - HudTutorial.Id.ITEM_1 + 1));
			break;
		case HudTutorial.Kind.ITEM_BUTTON:
			result = TextUtility.GetCommonText("Tutorial", "item_btn");
			break;
		case HudTutorial.Kind.CHARA:
			result = TextUtility.GetCommonText("Tutorial", "chara" + (id - HudTutorial.Id.CHARA_1 + 1));
			break;
		case HudTutorial.Kind.ACTION:
			result = TextUtility.GetCommonText("Tutorial", "action" + (id - HudTutorial.Id.ACTION_1 + 1));
			break;
		case HudTutorial.Kind.QUICK:
			result = TextUtility.GetCommonText("Tutorial", "quick" + (id - HudTutorial.Id.QUICK_1 + 1));
			break;
		}
		return result;
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x000A84A4 File Offset: 0x000A66A4
	public static string GetEventBossExplainText(HudTutorial.Id id, BossType bossType, int page = 0)
	{
		string result = string.Empty;
		HudTutorial.Kind kind = HudTutorial.GetKind(id);
		if (kind == HudTutorial.Kind.EVENTBOSS)
		{
			if (page == 0)
			{
				result = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, "Tutorial", "boss_" + (id - HudTutorial.Id.EVENTBOSS_1 + 1)).text;
			}
			else
			{
				string text = "_" + page.ToString();
				result = TextManager.GetText(TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, "Tutorial", string.Concat(new object[]
				{
					"boss_",
					id - HudTutorial.Id.EVENTBOSS_1 + 1,
					"_",
					text
				})).text;
			}
		}
		return result;
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x000A8554 File Offset: 0x000A6754
	private static bool IsItemTutorial()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				for (int i = 0; i < 8; i++)
				{
					SystemData.ItemTutorialFlagStatus itemTutorialStatus = ItemTypeName.GetItemTutorialStatus((ItemType)i);
					if (itemTutorialStatus != SystemData.ItemTutorialFlagStatus.NONE && !systemdata.IsFlagStatus(itemTutorialStatus))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x000A85B0 File Offset: 0x000A67B0
	private static bool IsItemTutorial(ItemType type)
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SystemData.ItemTutorialFlagStatus itemTutorialStatus = ItemTypeName.GetItemTutorialStatus(type);
				if (itemTutorialStatus != SystemData.ItemTutorialFlagStatus.NONE && !systemdata.IsFlagStatus(itemTutorialStatus))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C39 RID: 7225 RVA: 0x000A85FC File Offset: 0x000A67FC
	public static bool IsCharaTutorial(CharaType type)
	{
		if (type == CharaType.SONIC)
		{
			ServerMileageMapState mileageMapState = ServerInterface.MileageMapState;
			if (mileageMapState != null && mileageMapState.m_episode > 1)
			{
				return false;
			}
		}
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SystemData.CharaTutorialFlagStatus characterSaveDataFlagStatus = CharaTypeUtil.GetCharacterSaveDataFlagStatus(type);
				if (characterSaveDataFlagStatus != SystemData.CharaTutorialFlagStatus.NONE && !systemdata.IsFlagStatus(characterSaveDataFlagStatus))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001C3A RID: 7226 RVA: 0x000A8668 File Offset: 0x000A6868
	private static int GetCharaTutorialIndex(CharaType type)
	{
		HudTutorial.Id characterTutorialID = CharaTypeUtil.GetCharacterTutorialID(type);
		if (characterTutorialID != HudTutorial.Id.NONE)
		{
			return characterTutorialID - HudTutorial.Id.CHARA_0;
		}
		return -1;
	}

	// Token: 0x06001C3B RID: 7227 RVA: 0x000A868C File Offset: 0x000A688C
	public static CharaType GetCommonTextCharaName(HudTutorial.Id in_id)
	{
		for (int i = 0; i < 29; i++)
		{
			HudTutorial.Id characterTutorialID = CharaTypeUtil.GetCharacterTutorialID((CharaType)i);
			if (characterTutorialID == in_id)
			{
				return (CharaType)i;
			}
		}
		return CharaType.UNKNOWN;
	}

	// Token: 0x06001C3C RID: 7228 RVA: 0x000A86C0 File Offset: 0x000A68C0
	private static bool IsActionTutorial(HudTutorial.Id actionID)
	{
		if (HudTutorial.GetKind(actionID) == HudTutorial.Kind.ACTION)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					SystemData.ActionTutorialFlagStatus actionTutorialSaveFlag = HudTutorial.GetActionTutorialSaveFlag(actionID);
					if (actionTutorialSaveFlag != SystemData.ActionTutorialFlagStatus.NONE && !systemdata.IsFlagStatus(actionTutorialSaveFlag))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001C3D RID: 7229 RVA: 0x000A8718 File Offset: 0x000A6918
	public static bool IsQuickModeTutorial(HudTutorial.Id actionID)
	{
		if (HudTutorial.GetKind(actionID) == HudTutorial.Kind.QUICK)
		{
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = instance.GetSystemdata();
				if (systemdata != null)
				{
					SystemData.QuickModeTutorialFlagStatus quickModeTutorialSaveFlag = HudTutorial.GetQuickModeTutorialSaveFlag(actionID);
					if (quickModeTutorialSaveFlag != SystemData.QuickModeTutorialFlagStatus.NONE && !systemdata.IsFlagStatus(quickModeTutorialSaveFlag))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06001C3E RID: 7230 RVA: 0x000A8770 File Offset: 0x000A6970
	public static void SendItemTutorial(ItemType itemType)
	{
		if (HudTutorial.IsItemTutorial(itemType))
		{
			HudTutorial.Id itemTutorialID = ItemTypeName.GetItemTutorialID(itemType);
			if (itemTutorialID != HudTutorial.Id.NONE)
			{
				GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialItem", new MsgTutorialItem(itemTutorialID), SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06001C3F RID: 7231 RVA: 0x000A87B0 File Offset: 0x000A69B0
	public static void SendActionTutorial(HudTutorial.Id actionID)
	{
		if (HudTutorial.GetKind(actionID) == HudTutorial.Kind.ACTION && HudTutorial.IsActionTutorial(actionID))
		{
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnMsgTutorialAction", new MsgTutorialAction(actionID), SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001C40 RID: 7232 RVA: 0x000A87EC File Offset: 0x000A69EC
	public static SystemData.ActionTutorialFlagStatus GetActionTutorialSaveFlag(HudTutorial.Id actionID)
	{
		if (HudTutorial.GetKind(actionID) == HudTutorial.Kind.ACTION)
		{
			int num = actionID - HudTutorial.Id.ACTION_1;
			if ((ulong)num < (ulong)((long)HudTutorial.ACTION_TUTORIAL_DATA_TBL.Length))
			{
				return HudTutorial.ACTION_TUTORIAL_DATA_TBL[num].m_flagStatus;
			}
		}
		return SystemData.ActionTutorialFlagStatus.NONE;
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x000A8828 File Offset: 0x000A6A28
	public static SystemData.QuickModeTutorialFlagStatus GetQuickModeTutorialSaveFlag(HudTutorial.Id quickID)
	{
		if (HudTutorial.GetKind(quickID) == HudTutorial.Kind.QUICK)
		{
			int num = quickID - HudTutorial.Id.QUICK_1;
			if ((ulong)num < (ulong)((long)HudTutorial.QUICK_MODE_TUTORIAL_DATA_TBL.Length))
			{
				return HudTutorial.QUICK_MODE_TUTORIAL_DATA_TBL[num].m_flagStatus;
			}
		}
		return SystemData.QuickModeTutorialFlagStatus.NONE;
	}

	// Token: 0x06001C42 RID: 7234 RVA: 0x000A8864 File Offset: 0x000A6A64
	public static string GetActionTutorialText(HudTutorial.Id actionID)
	{
		if (HudTutorial.GetKind(actionID) == HudTutorial.Kind.ACTION)
		{
			int num = actionID - HudTutorial.Id.ACTION_1;
			if ((ulong)num < (ulong)((long)HudTutorial.ACTION_TUTORIAL_DATA_TBL.Length))
			{
				return TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, HudTutorial.ACTION_TUTORIAL_DATA_TBL[num].m_textCategory, HudTutorial.ACTION_TUTORIAL_DATA_TBL[num].m_textCell).text;
			}
		}
		return string.Empty;
	}

	// Token: 0x06001C43 RID: 7235 RVA: 0x000A88BC File Offset: 0x000A6ABC
	public static string GetItemTutorialText(HudTutorial.Id itemID)
	{
		if (HudTutorial.GetKind(itemID) == HudTutorial.Kind.ITEM)
		{
			string text = "name" + (itemID - HudTutorial.Id.ITEM_1 + 1).ToString();
			if (itemID == HudTutorial.Id.ITEM_6)
			{
				text += "_2";
			}
			return TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "ShopItem", text).text;
		}
		return string.Empty;
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x000A891C File Offset: 0x000A6B1C
	public static string GetItemTutorialCaptionText(HudTutorial.Id itemID)
	{
		if (HudTutorial.GetKind(itemID) != HudTutorial.Kind.ITEM)
		{
			return string.Empty;
		}
		if (itemID == HudTutorial.Id.ITEM_6)
		{
			return "caption_explan2_2";
		}
		return "caption_explan2";
	}

	// Token: 0x040019D1 RID: 6609
	[SerializeField]
	private GameObject m_captionGameObject;

	// Token: 0x040019D2 RID: 6610
	[SerializeField]
	private UILabel m_captionLabel;

	// Token: 0x040019D3 RID: 6611
	[SerializeField]
	private GameObject m_explanGameObject;

	// Token: 0x040019D4 RID: 6612
	[SerializeField]
	private HudTutorial.ExplanPattern[] m_explanPatterns = new HudTutorial.ExplanPattern[3];

	// Token: 0x040019D5 RID: 6613
	[SerializeField]
	private GameObject m_successGameObject;

	// Token: 0x040019D6 RID: 6614
	[SerializeField]
	private GameObject m_retryGameObject;

	// Token: 0x040019D7 RID: 6615
	[SerializeField]
	private GameObject m_anchorObj;

	// Token: 0x040019D8 RID: 6616
	private static readonly HudTutorial.TutorialData[] TUTORIAL_DATA_TBL = new HudTutorial.TutorialData[]
	{
		new HudTutorial.TutorialData(EventID.JUMP, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.DOUBLE_JUMP, 1, 1, 0),
		new HudTutorial.TutorialData(EventID.RING_BONUS, 2, 2, 8),
		new HudTutorial.TutorialData(EventID.ENEMY, 1, 3, 0),
		new HudTutorial.TutorialData(EventID.DAMAGE, 2, 4, 9),
		new HudTutorial.TutorialData(EventID.MISS, 1, 5, 0),
		new HudTutorial.TutorialData(EventID.PARA_LOOP, 1, 6, 0),
		new HudTutorial.TutorialData(EventID.FEVER_BOSS, 1, 7, 0),
		new HudTutorial.TutorialData(EventID.COMPLETE, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 1),
		new HudTutorial.TutorialData(EventID.NUM, 2, 0, 1),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 1, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 2, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 3, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 4, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 5, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 6, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 7, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 0, 0, 0),
		new HudTutorial.TutorialData(EventID.NUM, 1, 0, 0)
	};

	// Token: 0x040019D9 RID: 6617
	private static readonly HudTutorial.ActionTutorialData[] ACTION_TUTORIAL_DATA_TBL = new HudTutorial.ActionTutorialData[]
	{
		new HudTutorial.ActionTutorialData(SystemData.ActionTutorialFlagStatus.ACTION_1, "Item", "crystal_2")
	};

	// Token: 0x040019DA RID: 6618
	private static readonly HudTutorial.QuickModeTutorialData[] QUICK_MODE_TUTORIAL_DATA_TBL = new HudTutorial.QuickModeTutorialData[]
	{
		new HudTutorial.QuickModeTutorialData(SystemData.QuickModeTutorialFlagStatus.QUICK_1)
	};

	// Token: 0x040019DB RID: 6619
	private static string MISSION_SCENENAME = "ui_tex_tutorial_mission";

	// Token: 0x040019DC RID: 6620
	private static string FEVERBOSS_SCENENAME = "ui_tex_tutorial_feverboss";

	// Token: 0x040019DD RID: 6621
	private static string MAPBOSS_SCENENAME = "ui_tex_tutorial_mapboss_";

	// Token: 0x040019DE RID: 6622
	private static string EVENTBOSS_SCENENAME = "ui_tex_tutorial_eventboss_";

	// Token: 0x040019DF RID: 6623
	private static string ITEM_SCENENAME = "ui_tex_tutorial_item";

	// Token: 0x040019E0 RID: 6624
	private static string CHARA_SCENENAME = "ui_tex_tutorial_chara_";

	// Token: 0x040019E1 RID: 6625
	private static string ACTION_SCENENAME = "ui_tex_tutorial_action_";

	// Token: 0x040019E2 RID: 6626
	private static string QUICK_SCENENAME = "ui_tex_tutorial_quick_";

	// Token: 0x040019E3 RID: 6627
	private HudTutorial.ExplanPattern m_explanPattern;

	// Token: 0x040019E4 RID: 6628
	private HudTutorial.Id m_id;

	// Token: 0x040019E5 RID: 6629
	private HudTutorial.Phase m_phase;

	// Token: 0x040019E6 RID: 6630
	private int m_textureCount;

	// Token: 0x040019E7 RID: 6631
	private int m_currentTextureIndex;

	// Token: 0x040019E8 RID: 6632
	private float m_timer;

	// Token: 0x040019E9 RID: 6633
	private BossType m_bossType;

	// Token: 0x020003C6 RID: 966
	private enum ExplanPatternType
	{
		// Token: 0x040019EB RID: 6635
		TEXT_ONLY,
		// Token: 0x040019EC RID: 6636
		IMAGE_ONLY,
		// Token: 0x040019ED RID: 6637
		TEXT_AND_IMAGE,
		// Token: 0x040019EE RID: 6638
		COUNT
	}

	// Token: 0x020003C7 RID: 967
	[Serializable]
	private class ExplanPattern
	{
		// Token: 0x040019EF RID: 6639
		[SerializeField]
		public GameObject m_gameObject;

		// Token: 0x040019F0 RID: 6640
		[SerializeField]
		public UILabel m_label;

		// Token: 0x040019F1 RID: 6641
		[SerializeField]
		public UITexture m_texture;
	}

	// Token: 0x020003C8 RID: 968
	public enum Id
	{
		// Token: 0x040019F3 RID: 6643
		NONE = -1,
		// Token: 0x040019F4 RID: 6644
		MISSION_1,
		// Token: 0x040019F5 RID: 6645
		MISSION_2,
		// Token: 0x040019F6 RID: 6646
		MISSION_3,
		// Token: 0x040019F7 RID: 6647
		MISSION_4,
		// Token: 0x040019F8 RID: 6648
		MISSION_5,
		// Token: 0x040019F9 RID: 6649
		MISSION_6,
		// Token: 0x040019FA RID: 6650
		MISSION_7,
		// Token: 0x040019FB RID: 6651
		MISSION_8,
		// Token: 0x040019FC RID: 6652
		MISSION_END,
		// Token: 0x040019FD RID: 6653
		FEVERBOSS,
		// Token: 0x040019FE RID: 6654
		MAPBOSS_1,
		// Token: 0x040019FF RID: 6655
		MAPBOSS_2,
		// Token: 0x04001A00 RID: 6656
		MAPBOSS_3,
		// Token: 0x04001A01 RID: 6657
		EVENTBOSS_1,
		// Token: 0x04001A02 RID: 6658
		EVENTBOSS_2,
		// Token: 0x04001A03 RID: 6659
		ITEM_1,
		// Token: 0x04001A04 RID: 6660
		ITEM_2,
		// Token: 0x04001A05 RID: 6661
		ITEM_3,
		// Token: 0x04001A06 RID: 6662
		ITEM_4,
		// Token: 0x04001A07 RID: 6663
		ITEM_5,
		// Token: 0x04001A08 RID: 6664
		ITEM_6,
		// Token: 0x04001A09 RID: 6665
		ITEM_7,
		// Token: 0x04001A0A RID: 6666
		ITEM_8,
		// Token: 0x04001A0B RID: 6667
		CHARA_0,
		// Token: 0x04001A0C RID: 6668
		CHARA_1,
		// Token: 0x04001A0D RID: 6669
		CHARA_2,
		// Token: 0x04001A0E RID: 6670
		CHARA_3,
		// Token: 0x04001A0F RID: 6671
		CHARA_4,
		// Token: 0x04001A10 RID: 6672
		CHARA_5,
		// Token: 0x04001A11 RID: 6673
		CHARA_6,
		// Token: 0x04001A12 RID: 6674
		CHARA_7,
		// Token: 0x04001A13 RID: 6675
		CHARA_8,
		// Token: 0x04001A14 RID: 6676
		CHARA_9,
		// Token: 0x04001A15 RID: 6677
		CHARA_10,
		// Token: 0x04001A16 RID: 6678
		CHARA_11,
		// Token: 0x04001A17 RID: 6679
		CHARA_12,
		// Token: 0x04001A18 RID: 6680
		CHARA_13,
		// Token: 0x04001A19 RID: 6681
		CHARA_14,
		// Token: 0x04001A1A RID: 6682
		CHARA_15,
		// Token: 0x04001A1B RID: 6683
		CHARA_16,
		// Token: 0x04001A1C RID: 6684
		CHARA_17,
		// Token: 0x04001A1D RID: 6685
		CHARA_18,
		// Token: 0x04001A1E RID: 6686
		CHARA_19,
		// Token: 0x04001A1F RID: 6687
		CHARA_20,
		// Token: 0x04001A20 RID: 6688
		CHARA_21,
		// Token: 0x04001A21 RID: 6689
		CHARA_22,
		// Token: 0x04001A22 RID: 6690
		CHARA_23,
		// Token: 0x04001A23 RID: 6691
		CHARA_24,
		// Token: 0x04001A24 RID: 6692
		CHARA_25,
		// Token: 0x04001A25 RID: 6693
		CHARA_26,
		// Token: 0x04001A26 RID: 6694
		CHARA_27,
		// Token: 0x04001A27 RID: 6695
		CHARA_28,
		// Token: 0x04001A28 RID: 6696
		ACTION_1,
		// Token: 0x04001A29 RID: 6697
		ITEM_BUTTON_1,
		// Token: 0x04001A2A RID: 6698
		QUICK_1,
		// Token: 0x04001A2B RID: 6699
		NUM
	}

	// Token: 0x020003C9 RID: 969
	public enum Kind
	{
		// Token: 0x04001A2D RID: 6701
		NONE = -1,
		// Token: 0x04001A2E RID: 6702
		MISSION,
		// Token: 0x04001A2F RID: 6703
		MISSION_END,
		// Token: 0x04001A30 RID: 6704
		FEVERBOSS,
		// Token: 0x04001A31 RID: 6705
		MAPBOSS,
		// Token: 0x04001A32 RID: 6706
		EVENTBOSS,
		// Token: 0x04001A33 RID: 6707
		ITEM,
		// Token: 0x04001A34 RID: 6708
		ITEM_BUTTON,
		// Token: 0x04001A35 RID: 6709
		CHARA,
		// Token: 0x04001A36 RID: 6710
		ACTION,
		// Token: 0x04001A37 RID: 6711
		QUICK
	}

	// Token: 0x020003CA RID: 970
	private enum Phase
	{
		// Token: 0x04001A39 RID: 6713
		NONE = -1,
		// Token: 0x04001A3A RID: 6714
		OPEN,
		// Token: 0x04001A3B RID: 6715
		OPEN_WAIT,
		// Token: 0x04001A3C RID: 6716
		WAIT,
		// Token: 0x04001A3D RID: 6717
		CLOSE_WAIT,
		// Token: 0x04001A3E RID: 6718
		CLOSE,
		// Token: 0x04001A3F RID: 6719
		PLAY,
		// Token: 0x04001A40 RID: 6720
		SUCCESS,
		// Token: 0x04001A41 RID: 6721
		RETRY,
		// Token: 0x04001A42 RID: 6722
		RESULT,
		// Token: 0x04001A43 RID: 6723
		END
	}

	// Token: 0x020003CB RID: 971
	public class TutorialData
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x000A894C File Offset: 0x000A6B4C
		public TutorialData(EventID eventID, int count, int num1, int num2)
		{
			this.m_eventID = eventID;
			this.m_textureCount = count;
			this.m_textureNumber1 = num1;
			this.m_textureNumber2 = num2;
		}

		// Token: 0x04001A44 RID: 6724
		public EventID m_eventID;

		// Token: 0x04001A45 RID: 6725
		public int m_textureCount;

		// Token: 0x04001A46 RID: 6726
		public int m_textureNumber1;

		// Token: 0x04001A47 RID: 6727
		public int m_textureNumber2;
	}

	// Token: 0x020003CC RID: 972
	public class ActionTutorialData
	{
		// Token: 0x06001C47 RID: 7239 RVA: 0x000A8974 File Offset: 0x000A6B74
		public ActionTutorialData(SystemData.ActionTutorialFlagStatus flagStatus, string textCategory, string textCell)
		{
			this.m_flagStatus = flagStatus;
			this.m_textCategory = textCategory;
			this.m_textCell = textCell;
		}

		// Token: 0x04001A48 RID: 6728
		public SystemData.ActionTutorialFlagStatus m_flagStatus;

		// Token: 0x04001A49 RID: 6729
		public string m_textCategory;

		// Token: 0x04001A4A RID: 6730
		public string m_textCell;
	}

	// Token: 0x020003CD RID: 973
	public class QuickModeTutorialData
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x000A8994 File Offset: 0x000A6B94
		public QuickModeTutorialData(SystemData.QuickModeTutorialFlagStatus flagStatus)
		{
			this.m_flagStatus = flagStatus;
		}

		// Token: 0x04001A4B RID: 6731
		public SystemData.QuickModeTutorialFlagStatus m_flagStatus;
	}
}
