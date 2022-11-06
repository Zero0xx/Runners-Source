using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class EventRewardWindow : EventWindowBase
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x00053134 File Offset: 0x00051334
	protected override void SetObject()
	{
		GeneralUtil.SetRouletteBtnIcon(base.gameObject, "Btn_roulette");
		if (this.m_isSetObject)
		{
			return;
		}
		base.gameObject.transform.localPosition = default(Vector3);
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<string> list4 = new List<string>();
		list.Add("Lbl_caption");
		list.Add("Lbl_word_left_title");
		list.Add("Lbl_word_left_title_sh");
		list.Add("Lbl_chao_name");
		list.Add("ui_Lbl_word_object_total_main");
		list.Add("Lbl_object_total_main_num");
		list2.Add("img_chao_bg");
		list2.Add("img_object_total_main");
		list2.Add("img_type_icon");
		list3.Add("texture_chao");
		list4.Add("list");
		this.m_objectLabels = ObjectUtility.GetObjectLabel(base.gameObject, list);
		this.m_objectSprites = ObjectUtility.GetObjectSprite(base.gameObject, list2);
		this.m_objectTextures = ObjectUtility.GetObjectTexture(base.gameObject, list3);
		this.m_objects = ObjectUtility.GetGameObject(base.gameObject, list4);
		UIPlayAnimation uiplayAnimation = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(base.gameObject, "blinder");
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "blinder");
		if (uiplayAnimation != null && uibuttonMessage != null)
		{
			uiplayAnimation.enabled = false;
			uibuttonMessage.enabled = false;
		}
		this.m_isSetObject = true;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000532A8 File Offset: 0x000514A8
	private void Setup(SpecialStageInfo info)
	{
		this.mainPanel.alpha = 1f;
		if (info != null && info.GetRewardChao() != null)
		{
			this.m_targetChao = info.GetRewardChao().id;
		}
		else
		{
			this.m_targetChao = -1;
		}
		if (info != null)
		{
			this.SetupObject(info);
		}
		this.m_mode = EventRewardWindow.Mode.Wait;
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x0005330C File Offset: 0x0005150C
	private void Setup(RaidBossInfo info)
	{
		this.mainPanel.alpha = 1f;
		if (info != null && info.GetRewardChao() != null)
		{
			this.m_targetChao = info.GetRewardChao().id;
		}
		else
		{
			this.m_targetChao = -1;
		}
		if (info != null)
		{
			this.SetupObject(info);
		}
		this.m_mode = EventRewardWindow.Mode.Wait;
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x00053370 File Offset: 0x00051570
	private void Setup(EtcEventInfo info)
	{
		this.mainPanel.alpha = 1f;
		if (info != null && info.GetRewardChao() != null)
		{
			this.m_targetChao = info.GetRewardChao().id;
		}
		else
		{
			this.m_targetChao = -1;
		}
		if (info != null)
		{
			this.SetupObject(info);
		}
		this.m_mode = EventRewardWindow.Mode.Wait;
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x000533D4 File Offset: 0x000515D4
	private void SetupObject(EventBaseInfo info)
	{
		this.m_isGetEffectEnd = true;
		this.m_afterTime = 0.3f;
		this.SetObject();
		if (this.m_getSoundDelay != null)
		{
			this.m_getSoundDelay.Clear();
		}
		this.m_mode = EventRewardWindow.Mode.Idle;
		this.m_close = false;
		this.m_btnAct = EventRewardWindow.BUTTON_ACT.NONE;
		base.enabledAnchorObjects = true;
		if (this.animationData != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animationData, "ui_event_rewordlist_window_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
			SoundManager.SePlay("sys_window_open", "SE");
		}
		UIDraggablePanel uidraggablePanel = null;
		if (this.m_objects != null && this.m_objects.ContainsKey("list"))
		{
			uidraggablePanel = this.m_objects["list"].GetComponent<UIDraggablePanel>();
		}
		if (info != null)
		{
			if (this.m_objectLabels != null && this.m_objectLabels.Count > 0)
			{
				if (this.m_objectLabels.ContainsKey("Lbl_caption") && this.m_objectLabels["Lbl_caption"] != null)
				{
					this.m_objectLabels["Lbl_caption"].text = info.caption;
				}
				if (this.m_objectLabels.ContainsKey("Lbl_word_left_title") && this.m_objectLabels["Lbl_word_left_title"] != null)
				{
					this.m_objectLabels["Lbl_word_left_title"].text = info.leftTitle;
				}
				if (this.m_objectLabels.ContainsKey("Lbl_word_left_title_sh") && this.m_objectLabels["Lbl_word_left_title_sh"] != null)
				{
					this.m_objectLabels["Lbl_word_left_title_sh"].text = info.leftTitle;
				}
				if (this.m_objectLabels.ContainsKey("Lbl_chao_name") && this.m_objectLabels["Lbl_chao_name"] != null)
				{
					this.m_objectLabels["Lbl_chao_name"].text = info.leftName;
				}
				if (this.m_objectLabels.ContainsKey("ui_Lbl_word_object_total_main") && this.m_objectLabels["ui_Lbl_word_object_total_main"] != null)
				{
					this.m_objectLabels["ui_Lbl_word_object_total_main"].text = info.rightTitle;
				}
				if (this.m_objectLabels.ContainsKey("Lbl_object_total_main_num") && this.m_objectLabels["Lbl_object_total_main_num"] != null)
				{
					this.m_objectLabels["Lbl_object_total_main_num"].text = HudUtility.GetFormatNumString<long>(info.totalPoint);
				}
			}
			if (this.m_objectSprites != null && this.m_objectSprites.Count > 0)
			{
				if (this.m_objectSprites.ContainsKey("img_chao_bg"))
				{
					this.m_objectSprites["img_chao_bg"].spriteName = info.leftBg;
				}
				if (this.m_objectSprites.ContainsKey("img_object_total_main"))
				{
					this.m_objectSprites["img_object_total_main"].spriteName = info.rightTitleIcon;
				}
				if (this.m_objectSprites.ContainsKey("img_type_icon") && !string.IsNullOrEmpty(info.chaoTypeIcon))
				{
					this.m_objectSprites["img_type_icon"].spriteName = info.chaoTypeIcon;
				}
			}
			if (this.m_objectTextures != null && this.m_objectTextures.ContainsKey("texture_chao"))
			{
				ChaoData rewardChao = info.GetRewardChao();
				ChaoTextureManager.CallbackInfo info2 = new ChaoTextureManager.CallbackInfo(this.m_objectTextures["texture_chao"], null, true);
				ChaoTextureManager.Instance.GetTexture(rewardChao.id, info2);
			}
			if (uidraggablePanel != null)
			{
				List<EventMission> eventMission = info.eventMission;
				if (eventMission != null && eventMission.Count > 0)
				{
					int count = eventMission.Count;
					int num = count;
					if (this.m_listObject == null)
					{
						this.m_rewardTime = 0f;
						this.m_attainment = 0;
						this.m_currentAttainment = 0;
						if (this.m_first)
						{
							this.m_isGetEffectEnd = false;
							this.m_afterTime = 0f;
						}
						this.m_listObject = new List<GameObject>();
						for (int i = 0; i < count; i++)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate(this.orgEventScroll, Vector3.zero, Quaternion.identity) as GameObject;
							gameObject.gameObject.transform.parent = uidraggablePanel.gameObject.transform;
							gameObject.gameObject.transform.localPosition = new Vector3(0f, (float)(-100 * i), 0f);
							gameObject.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
							UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject.gameObject, "Lbl_itemname");
							UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject.gameObject, "Lbl_itemname_sh");
							UILabel uilabel3 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject.gameObject, "Lbl_word_event_object_total");
							UILabel uilabel4 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject.gameObject, "Lbl_event_object_total_num");
							GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject.gameObject, "get_icon_Anim");
							UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject.gameObject, "img_item_0");
							UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject.gameObject, "texture_chao_0");
							UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject.gameObject, "texture_chao_1");
							UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject.gameObject, "img_event_object_icon");
							if (uilabel3 != null && uilabel4 != null)
							{
								uilabel3.text = eventMission[i].name;
								uilabel4.text = HudUtility.GetFormatNumString<long>(eventMission[i].point);
							}
							if (gameObject2 != null)
							{
								gameObject2.SetActive(false);
								if (eventMission[i].IsAttainment(info.totalPoint))
								{
									this.m_attainment = i + 1;
								}
							}
							if (uisprite3 != null)
							{
								uisprite3.spriteName = info.rightTitleIcon;
							}
							if (uilabel != null && uilabel2 != null && uisprite != null && uisprite2 != null && uitexture != null)
							{
								ServerItem serverItem = new ServerItem((ServerItem.Id)eventMission[i].reward);
								string text = serverItem.serverItemName;
								int index = eventMission[i].index;
								if (index > 1)
								{
									text = text + " × " + index;
								}
								uilabel.text = text;
								uilabel2.text = text;
								if (eventMission[i].reward >= 400000 && eventMission[i].reward < 500000)
								{
									uisprite2.alpha = 0f;
									uisprite.alpha = 0f;
									uitexture.alpha = 0f;
									uitexture.gameObject.SetActive(true);
									ChaoTextureManager.CallbackInfo info3 = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
									ChaoTextureManager.Instance.GetTexture(eventMission[i].reward - 400000, info3);
									uitexture.alpha = 1f;
									ChaoData chaoData = ChaoTable.GetChaoData(serverItem.chaoId);
									if (chaoData != null)
									{
										uilabel.text = chaoData.name;
										uilabel2.text = chaoData.name;
									}
								}
								else
								{
									uisprite.spriteName = PresentBoxUtility.GetItemSpriteName(serverItem);
									uisprite.alpha = 1f;
									uisprite2.alpha = 0f;
									uitexture.alpha = 0f;
									uitexture.gameObject.SetActive(false);
								}
							}
							this.m_listObject.Add(gameObject);
						}
						uidraggablePanel.ResetPosition();
						if (this.m_attainment > 0)
						{
							for (int j = 0; j < this.m_attainment; j++)
							{
								Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_listObject[j], "get_icon_Anim");
								if (animation != null)
								{
									if (j < this.m_attainment - 5 || !this.m_first)
									{
										this.m_currentAttainment = j + 1;
										animation.enabled = false;
										animation.gameObject.SetActive(true);
									}
									else
									{
										animation.enabled = true;
									}
								}
							}
							this.m_initPoint = 0f;
							if (this.m_attainment > 3)
							{
								this.m_initPoint = 1f / (float)(num - 4) * (float)(this.m_attainment - 3);
							}
							base.StartCoroutine(this.ScrollInit(this.m_initPoint, uidraggablePanel));
						}
					}
					else
					{
						uidraggablePanel.ResetPosition();
						this.m_currentAttainment = this.m_attainment;
						this.m_isGetEffectEnd = true;
						this.m_afterTime = 0.3f;
						base.StartCoroutine(this.ScrollInit(this.m_initPoint, uidraggablePanel));
					}
				}
			}
		}
		if (this.m_attainment <= 0 || this.m_currentAttainment >= this.m_attainment)
		{
			this.m_isGetEffectEnd = true;
			this.m_afterTime = 0.3f;
		}
		else
		{
			this.m_rewardTime = 0.75f;
		}
		base.enabled = true;
		this.m_first = false;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00053D28 File Offset: 0x00051F28
	private void Update()
	{
		if (!this.m_isGetEffectEnd)
		{
			if (this.m_rewardTime <= 0f)
			{
				if (this.m_listObject != null && this.m_currentAttainment >= 0 && this.m_currentAttainment < this.m_listObject.Count)
				{
					GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_listObject[this.m_currentAttainment].gameObject, "get_icon_Anim");
					if (gameObject != null)
					{
						if (!gameObject.activeSelf)
						{
							if (this.m_getSoundDelay == null)
							{
								this.m_getSoundDelay = new List<float>();
							}
							this.m_getSoundDelay.Add(0.25f);
						}
						gameObject.SetActive(true);
					}
					this.m_currentAttainment++;
					this.m_rewardTime = 0.7f;
					if (this.m_attainment <= this.m_currentAttainment)
					{
						this.m_isGetEffectEnd = true;
						this.m_rewardTime = 0f;
					}
				}
				else
				{
					this.m_isGetEffectEnd = true;
					this.m_rewardTime = 0f;
				}
			}
			this.m_rewardTime -= Time.deltaTime;
		}
		else
		{
			this.m_afterTime += Time.deltaTime;
			this.m_rewardTime = 0f;
			if (this.m_afterTime > 3f)
			{
				base.enabled = false;
			}
		}
		if (this.m_getSoundDelay != null && this.m_getSoundDelay.Count > 0)
		{
			float deltaTime = Time.deltaTime;
			int num = -1;
			for (int i = 0; i < this.m_getSoundDelay.Count; i++)
			{
				List<float> getSoundDelay;
				List<float> list = getSoundDelay = this.m_getSoundDelay;
				int index2;
				int index = index2 = i;
				float num2 = getSoundDelay[index2];
				list[index] = num2 - deltaTime;
				if (this.m_getSoundDelay[i] <= 0f)
				{
					num = i;
				}
			}
			if (num >= 0)
			{
				SoundManager.SePlay("sys_result_decide", "SE");
				this.m_getSoundDelay.RemoveAt(num);
			}
		}
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00053F1C File Offset: 0x0005211C
	private void OnSetChaoTexture(ChaoTextureManager.TextureData data)
	{
		if (data != null && data.tex != null && this.m_objectTextures != null && this.m_objectTextures.ContainsKey("texture_chao"))
		{
			this.m_objectTextures["texture_chao"].mainTexture = data.tex;
			this.m_objectTextures["texture_chao"].alpha = 1f;
		}
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00053F98 File Offset: 0x00052198
	private IEnumerator ScrollInit(float point, UIDraggablePanel list)
	{
		yield return new WaitForSeconds(0.025f);
		list.verticalScrollBar.value = point;
		yield break;
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x00053FC8 File Offset: 0x000521C8
	public bool IsEnd()
	{
		return this.m_mode != EventRewardWindow.Mode.Wait;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x00053FDC File Offset: 0x000521DC
	public void OnClickNoButton()
	{
		this.m_btnAct = EventRewardWindow.BUTTON_ACT.CLOSE;
		this.m_close = true;
		SoundManager.SePlay("sys_window_close", "SE");
		if (this.animationData != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animationData, "ui_event_rewordlist_window_Anim", Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
		}
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x00054044 File Offset: 0x00052244
	public void OnClickNoBgButton()
	{
		this.OnClickNoButton();
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0005404C File Offset: 0x0005224C
	public void OnClickTarget()
	{
		if (this.m_targetChao >= 0)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ChaoSetWindowUI window = ChaoSetWindowUI.GetWindow();
			if (window != null)
			{
				ChaoData chaoData = ChaoTable.GetChaoData(this.m_targetChao);
				if (chaoData != null)
				{
					ChaoSetWindowUI.ChaoInfo chaoInfo = new ChaoSetWindowUI.ChaoInfo(chaoData);
					chaoInfo.level = ChaoTable.ChaoMaxLevel();
					chaoInfo.detail = chaoData.GetDetailsLevel(chaoInfo.level);
					if (chaoInfo.level == ChaoTable.ChaoMaxLevel())
					{
						chaoInfo.detail = chaoInfo.detail + "\n" + TextUtility.GetChaoText("Chao", "level_max");
					}
					window.OpenWindow(chaoInfo, ChaoSetWindowUI.WindowType.WINDOW_ONLY);
				}
			}
		}
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x00054100 File Offset: 0x00052300
	public void OnClickRouletteButton()
	{
		this.m_btnAct = EventRewardWindow.BUTTON_ACT.ROULETTE;
		this.m_close = true;
		SoundManager.SePlay("sys_menu_decide", "SE");
		HudMenuUtility.SendChaoRouletteButtonClicked();
		if (this.animationData != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animationData, "ui_event_rewordlist_window_Anim", Direction.Reverse);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.WindowAnimationFinishCallback), true);
		}
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0005416C File Offset: 0x0005236C
	private void WindowAnimationFinishCallback()
	{
		if (this.m_close)
		{
			this.m_mode = EventRewardWindow.Mode.End;
			if (this.m_getSoundDelay != null)
			{
				this.m_getSoundDelay.Clear();
			}
			EventRewardWindow.BUTTON_ACT btnAct = this.m_btnAct;
			if (btnAct != EventRewardWindow.BUTTON_ACT.CLOSE)
			{
				if (btnAct != EventRewardWindow.BUTTON_ACT.ROULETTE)
				{
					base.enabledAnchorObjects = false;
				}
				else
				{
					base.enabledAnchorObjects = false;
				}
			}
			else
			{
				base.enabledAnchorObjects = false;
			}
		}
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x000541E0 File Offset: 0x000523E0
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!base.enabledAnchorObjects || this.m_btnAct != EventRewardWindow.BUTTON_ACT.NONE)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (this.m_isGetEffectEnd && this.m_afterTime > 0.5f)
		{
			base.SendMessage("OnClickNoButton");
		}
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x00054238 File Offset: 0x00052438
	public static EventRewardWindow Create(SpecialStageInfo info)
	{
		if (EventRewardWindow.s_instance != null)
		{
			EventRewardWindow.s_instance.gameObject.SetActive(true);
			EventRewardWindow.s_instance.Setup(info);
			return EventRewardWindow.s_instance;
		}
		return null;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x00054278 File Offset: 0x00052478
	public static EventRewardWindow Create(RaidBossInfo info)
	{
		if (EventRewardWindow.s_instance != null)
		{
			EventRewardWindow.s_instance.gameObject.SetActive(true);
			EventRewardWindow.s_instance.Setup(info);
			return EventRewardWindow.s_instance;
		}
		return null;
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x000542B8 File Offset: 0x000524B8
	public static EventRewardWindow Create(EtcEventInfo info)
	{
		if (EventRewardWindow.s_instance != null)
		{
			EventRewardWindow.s_instance.gameObject.SetActive(true);
			EventRewardWindow.s_instance.Setup(info);
			return EventRewardWindow.s_instance;
		}
		return null;
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x000542F8 File Offset: 0x000524F8
	public void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00054308 File Offset: 0x00052508
	public void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00054318 File Offset: 0x00052518
	private static EventRewardWindow Instance
	{
		get
		{
			return EventRewardWindow.s_instance;
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00054320 File Offset: 0x00052520
	private void Awake()
	{
		this.SetInstance();
		this.EntryBackKeyCallBack();
		base.enabledAnchorObjects = false;
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x00054338 File Offset: 0x00052538
	private void OnDestroy()
	{
		if (EventRewardWindow.s_instance == this)
		{
			this.RemoveBackKeyCallBack();
			EventRewardWindow.s_instance = null;
		}
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00054358 File Offset: 0x00052558
	private void SetInstance()
	{
		if (EventRewardWindow.s_instance == null)
		{
			EventRewardWindow.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000C54 RID: 3156
	private const int DRAW_LINE_MAX = 4;

	// Token: 0x04000C55 RID: 3157
	private const int DRAW_LINE_POS = 3;

	// Token: 0x04000C56 RID: 3158
	private const float GET_ICON_SOUND_DELAY = 0.25f;

	// Token: 0x04000C57 RID: 3159
	private const string LBL_CAPTION = "Lbl_caption";

	// Token: 0x04000C58 RID: 3160
	private const string LBL_LEFT_TITLE = "Lbl_word_left_title";

	// Token: 0x04000C59 RID: 3161
	private const string LBL_LEFT_TITLE_SH = "Lbl_word_left_title_sh";

	// Token: 0x04000C5A RID: 3162
	private const string LBL_LEFT_NAME = "Lbl_chao_name";

	// Token: 0x04000C5B RID: 3163
	private const string LBL_RIGHT_TITLE = "ui_Lbl_word_object_total_main";

	// Token: 0x04000C5C RID: 3164
	private const string LBL_RIGHT_NUM = "Lbl_object_total_main_num";

	// Token: 0x04000C5D RID: 3165
	private const string IMG_LEFT_BG = "img_chao_bg";

	// Token: 0x04000C5E RID: 3166
	private const string IMG_RIGHT_ICON = "img_object_total_main";

	// Token: 0x04000C5F RID: 3167
	private const string IMG_TYPE_ICON = "img_type_icon";

	// Token: 0x04000C60 RID: 3168
	private const string TEX_LEFT_TEXTURE = "texture_chao";

	// Token: 0x04000C61 RID: 3169
	private const string GO_LIST = "list";

	// Token: 0x04000C62 RID: 3170
	[SerializeField]
	private GameObject orgEventScroll;

	// Token: 0x04000C63 RID: 3171
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04000C64 RID: 3172
	private List<GameObject> m_listObject;

	// Token: 0x04000C65 RID: 3173
	private EventRewardWindow.Mode m_mode;

	// Token: 0x04000C66 RID: 3174
	private int m_targetChao = -1;

	// Token: 0x04000C67 RID: 3175
	[SerializeField]
	private Animation animationData;

	// Token: 0x04000C68 RID: 3176
	private bool m_close;

	// Token: 0x04000C69 RID: 3177
	private EventRewardWindow.BUTTON_ACT m_btnAct = EventRewardWindow.BUTTON_ACT.NONE;

	// Token: 0x04000C6A RID: 3178
	private bool m_first = true;

	// Token: 0x04000C6B RID: 3179
	private float m_afterTime;

	// Token: 0x04000C6C RID: 3180
	private float m_rewardTime;

	// Token: 0x04000C6D RID: 3181
	private int m_attainment;

	// Token: 0x04000C6E RID: 3182
	private int m_currentAttainment;

	// Token: 0x04000C6F RID: 3183
	private bool m_isGetEffectEnd;

	// Token: 0x04000C70 RID: 3184
	private float m_initPoint;

	// Token: 0x04000C71 RID: 3185
	private List<float> m_getSoundDelay;

	// Token: 0x04000C72 RID: 3186
	private static EventRewardWindow s_instance;

	// Token: 0x02000221 RID: 545
	private enum BUTTON_ACT
	{
		// Token: 0x04000C74 RID: 3188
		CLOSE,
		// Token: 0x04000C75 RID: 3189
		ROULETTE,
		// Token: 0x04000C76 RID: 3190
		NONE
	}

	// Token: 0x02000222 RID: 546
	private enum Mode
	{
		// Token: 0x04000C78 RID: 3192
		Idle,
		// Token: 0x04000C79 RID: 3193
		Wait,
		// Token: 0x04000C7A RID: 3194
		End
	}
}
