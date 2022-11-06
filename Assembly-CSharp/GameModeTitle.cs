using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using App;
using DataTable;
using Message;
using NoahUnity;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class GameModeTitle : MonoBehaviour
{
	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x060017FB RID: 6139 RVA: 0x0008872C File Offset: 0x0008692C
	// (set) Token: 0x060017FC RID: 6140 RVA: 0x00088734 File Offset: 0x00086934
	public static bool Logined
	{
		get
		{
			return GameModeTitle.m_isLogined;
		}
		set
		{
			GameModeTitle.m_isLogined = value;
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x060017FD RID: 6141 RVA: 0x0008873C File Offset: 0x0008693C
	// (set) Token: 0x060017FE RID: 6142 RVA: 0x00088744 File Offset: 0x00086944
	public static bool FirstTutorialReturned
	{
		get
		{
			return GameModeTitle.m_isReturnFirstTutorial;
		}
		set
		{
			GameModeTitle.m_isReturnFirstTutorial = value;
		}
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x0008874C File Offset: 0x0008694C
	private void Awake()
	{
		Application.targetFrameRate = 60;
		base.gameObject.AddComponent<HudNetworkConnect>();
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x00088764 File Offset: 0x00086964
	private void Start()
	{
		HudUtility.SetInvalidNGUIMitiTouch();
		SystemSettings.ChangeQualityLevelBySaveData();
		SystemData systemSaveData = SystemSaveManager.GetSystemSaveData();
		bool flag = false;
		if (systemSaveData != null)
		{
			flag = systemSaveData.highTexture;
		}
		if (flag)
		{
			Caching.maximumAvailableDiskSpace = 524288000L;
		}
		else
		{
			Caching.maximumAvailableDiskSpace = 314572800L;
		}
		Caching.expirationDelay = 2592000;
		GameObject gameObject = GameObject.Find("StageInfo");
		if (gameObject == null)
		{
			gameObject = new GameObject();
			if (gameObject != null)
			{
				gameObject.name = "StageInfo";
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				gameObject.AddComponent("StageInfo");
			}
		}
		if (gameObject != null)
		{
			this.m_stageInfo = gameObject.GetComponent<StageInfo>();
			if (this.m_stageInfo == null)
			{
				return;
			}
		}
		SoundManager.AddTitleCueSheet();
		GameObject gameObject2 = GameObject.Find("UI Root (2D)");
		if (gameObject2 == null)
		{
			return;
		}
		this.m_touchScreenObject = GameObjectUtil.FindChildGameObject(gameObject2, "Lbl_mainmenu");
		if (this.m_touchScreenObject != null)
		{
			this.m_touchScreenObject.SetActive(false);
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "sega_logo");
		if (gameObject3 != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject3, "sega_logo_img");
			if (uisprite != null)
			{
				if (Env.language == Env.Language.JAPANESE || Env.language == Env.Language.KOREAN || Env.language == Env.Language.CHINESE_ZH || Env.language == Env.Language.CHINESE_ZHJ)
				{
					uisprite.spriteName = "ui_title_img_segalogo_jp";
				}
				else
				{
					uisprite.spriteName = "ui_title_img_segalogo_en";
				}
			}
			gameObject3.SetActive(GameModeTitle.s_first);
		}
		this.m_fsm = (base.gameObject.AddComponent(typeof(TinyFsmBehavior)) as TinyFsmBehavior);
		if (this.m_fsm == null)
		{
			return;
		}
		TinyFsmBehavior.Description description = new TinyFsmBehavior.Description(this);
		RouletteManager.Remove();
		if (GameModeTitle.m_isReturnFirstTutorial)
		{
			if (SystemSaveManager.Instance != null)
			{
				SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
				if (systemdata != null)
				{
					systemdata.SetFlagStatus(SystemData.FlagStatus.FIRST_LAUNCH_TUTORIAL_END, true);
					SystemSaveManager.Instance.SaveSystemData();
				}
			}
			GameObject gameObject4 = GameObject.Find("UI Root (2D)");
			if (gameObject4 != null)
			{
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject4, "img_blinder");
				if (gameObject5 != null)
				{
					gameObject5.SetActive(true);
				}
			}
			this.NoahBannerDisplay(false);
			this.m_nextSceneName = "MainMenu";
			description.initState = new TinyFsmState(new EventFunction(this.StateSnsInitialize));
		}
		else if (GameModeTitle.m_isLogined)
		{
			description.initState = new TinyFsmState(new EventFunction(this.StateFadeOut));
		}
		else
		{
			description.initState = new TinyFsmState(new EventFunction(this.StateLoadFont));
		}
		description.onFixedUpdate = true;
		this.m_fsm.SetUp(description);
		UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_ver");
		if (uilabel == null)
		{
			return;
		}
		string str = string.Empty;
		if (systemSaveData != null && systemSaveData.highTexture)
		{
			str = "t";
		}
		string text = this.VersionStr + str + NetBaseUtil.ServerTypeString;
		if (!string.IsNullOrEmpty(NetBaseUtil.ServerTypeString))
		{
			text = "[ff0000]" + text;
		}
		uilabel.text = text;
		this.m_loginLabel = GameObjectUtil.FindChildGameObject(gameObject2, "Lbl_login");
		if (this.m_loginLabel != null)
		{
			this.m_loginLabel.SetActive(false);
		}
		this.m_loadingWindow = GameObjectUtil.FindChildGameObjectComponent<HudLoadingWindow>(gameObject2, "DownloadWindow");
		this.m_userIdLabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject2, "Lbl_userid");
		if (this.m_userIdLabel != null)
		{
			if (TitleUtil.IsExistSaveDataGameId())
			{
				this.m_initUser = true;
				this.m_userIdLabel.gameObject.SetActive(true);
				this.m_userIdLabel.text = this.GetViewUserID();
				GameObject gameObject6 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_policy");
				if (gameObject6 != null)
				{
					gameObject6.SetActive(false);
				}
				GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_mainmenu");
				if (gameObject7 != null)
				{
					gameObject7.SetActive(true);
				}
				this.m_startButton = gameObject7;
			}
			else
			{
				this.m_initUser = false;
				this.m_userIdLabel.gameObject.SetActive(false);
				GameObject gameObject8 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_policy");
				if (gameObject8 != null)
				{
					gameObject8.SetActive(true);
				}
				GameObject gameObject9 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_mainmenu");
				if (gameObject9 != null)
				{
					gameObject9.SetActive(false);
				}
				this.m_startButton = gameObject8;
			}
			BackKeyManager.AddEventCallBack(base.gameObject);
			BackKeyManager.StartScene();
			BackKeyManager.InvalidFlag = true;
		}
		this.m_snsLogin = base.gameObject.AddComponent<SettingPartsSnsLogin>();
		this.m_snsLogin.SetCancelWindowUseFlag(false);
		this.m_snsLogin.Setup("Camera/TitleScreen/Anchor_5_MC");
		global::Debug.Log("GetLang:" + Language.GetLocalLanguage());
		GameObject gameObject10 = GameObjectUtil.FindChildGameObject(gameObject2, "Btn_fb");
		if (gameObject10 != null)
		{
			gameObject10.SetActive(false);
		}
		if (this.m_userIdLabel != null)
		{
			this.m_userIdLabel.gameObject.SetActive(true);
			this.m_userIdLabel.text = this.GetViewUserID();
		}
		CameraFade.StartAlphaFade(Color.black, true, 2f, 0f, new Action(this.FinishFadeCallback));
		this.m_movetButton = null;
		this.m_cacheButton = null;
		UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject2, "Btn_move");
		if (uibuttonMessage != null)
		{
			this.m_movetButton = uibuttonMessage.gameObject;
			this.m_movetButton.SetActive(false);
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "OnClickMigrationButton";
		}
		UIButtonMessage uibuttonMessage2 = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(gameObject2, "Btn_cache");
		if (uibuttonMessage2 != null)
		{
			this.m_cacheButton = uibuttonMessage2.gameObject;
			this.m_cacheButton.SetActive(false);
			uibuttonMessage2.target = base.gameObject;
			uibuttonMessage2.functionName = "OnClickCacheClearButton";
		}
		GameObject gameObject11 = GameObjectUtil.FindChildGameObject(gameObject2, "logo_jp");
		if (gameObject11 != null)
		{
			if (Env.language == Env.Language.JAPANESE)
			{
				gameObject11.SetActive(true);
			}
			else
			{
				gameObject11.SetActive(false);
			}
		}
		this.m_loadingConnect = base.gameObject.GetComponent<HudNetworkConnect>();
		if (!GameModeTitle.m_isReturnFirstTutorial)
		{
			GameObject gameObject12 = GameObjectUtil.FindChildGameObject(gameObject2, "img_titlelogo");
			if (gameObject12 != null)
			{
				gameObject12.SetActive(false);
			}
		}
		this.m_progressBar = GameObjectUtil.FindChildGameObjectComponent<HudProgressBar>(gameObject2, "Pgb_loading");
		if (this.m_progressBar != null)
		{
			this.m_progressBar.SetUp(25);
		}
		GameObject gameObject13 = GameObject.Find("UI Root 2(2D)");
		if (gameObject13 != null)
		{
			GameObject gameObject14 = GameObjectUtil.FindChildGameObject(gameObject13, "col_noah");
			if (gameObject14 != null)
			{
				Vector2 vector = new Vector2(960f, 640f);
				Resolution currentResolution = Screen.currentResolution;
				Vector2 vector2 = new Vector2((float)(currentResolution.width / Screen.width), (float)(currentResolution.height / Screen.height));
				vector2.x *= 2f;
				vector2.y *= 2f;
				BoxCollider component = gameObject14.GetComponent<BoxCollider>();
				if (component != null)
				{
					Vector2 vector3 = component.size;
					component.size = new Vector3(vector3.x * vector2.x, vector3.y * vector2.y, 1f);
					global::Debug.Log("NoahColliderSize = " + component.size.x.ToString() + " , " + component.size.y.ToString());
				}
				UISprite component2 = gameObject14.GetComponent<UISprite>();
				if (component2 != null)
				{
					Vector2 vector4 = new Vector2((float)component2.width, (float)component2.height);
					component2.width = (int)(vector4.x * vector2.x);
					component2.height = (int)(vector4.y * vector2.y);
					global::Debug.Log("NoahSpriteSize = " + component2.width.ToString() + " , " + component2.height.ToString());
				}
			}
		}
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x00088FC8 File Offset: 0x000871C8
	private TinyFsmState StateLoadFont(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (FontManager.Instance != null && FontManager.Instance.IsNecessaryLoadFont())
			{
				FontManager.Instance.LoadResourceData();
				FontManager.Instance.ReplaceFont();
			}
			return TinyFsmState.End();
		case 4:
			if (GameModeTitle.s_first)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeOut)));
			}
			else
			{
				this.SegaLogoAnimationSkip();
				if (SystemSaveManager.Instance != null && SystemSaveManager.Instance.ErrorOnStart())
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSaveDataError)));
					return TinyFsmState.End();
				}
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsInitialize)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000890D8 File Offset: 0x000872D8
	private TinyFsmState StateFadeOut(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_touchScreenObject != null)
			{
				this.m_touchScreenObject.SetActive(true);
				TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_mainmenu");
				UILabel component = this.m_touchScreenObject.GetComponent<UILabel>();
				if (component != null)
				{
					component.text = text.text;
				}
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_touchScreenObject, "Lbl_mainmenu_sh");
				if (uilabel != null)
				{
					uilabel.text = text.text;
				}
			}
			return TinyFsmState.End();
		default:
		{
			if (signal != 109)
			{
				return TinyFsmState.End();
			}
			GameObject parent = GameObject.Find("UI Root (2D)");
			GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, "img_titlelogo");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(parent, "TitleScreen");
			if (animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_title_intro_logo_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.SegaLogoAnimationFinishCallback), false);
			}
			GameModeTitle.s_first = false;
			if (SystemSaveManager.Instance != null && SystemSaveManager.Instance.ErrorOnStart())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSaveDataError)));
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsInitialize)));
			return TinyFsmState.End();
		}
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00089284 File Offset: 0x00087484
	private TinyFsmState StateSaveDataError(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			this.CreateSaveErrorWindow(false);
			this.m_subState = 0;
			BackKeyManager.InvalidFlag = false;
			return TinyFsmState.End();
		default:
			if (signal != 109)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsInitialize)));
			return TinyFsmState.End();
		case 4:
		{
			int subState = this.m_subState;
			if (subState != 0)
			{
				if (subState == 1)
				{
					if (GeneralWindow.IsButtonPressed)
					{
						GeneralWindow.Close();
						this.CreateSaveErrorWindow(false);
						this.m_subState = 0;
					}
				}
			}
			else if (GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				if (SystemSaveManager.Instance.SaveForStartingError())
				{
					if (GameModeTitle.m_isLogined)
					{
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsInitialize)));
					}
					else
					{
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsInitialize)));
					}
				}
				else
				{
					this.CreateSaveErrorWindow(true);
					this.m_subState = 1;
				}
			}
			return TinyFsmState.End();
		}
		}
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000893D4 File Offset: 0x000875D4
	private void CreateSaveErrorWindow(bool error)
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", (!error) ? "savedata_recreate" : "savedata_error").text,
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.Ok
		});
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x00089430 File Offset: 0x00087630
	private TinyFsmState StateSnsInitialize(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null)
			{
				socialInterface.Initialize(base.gameObject);
			}
			return TinyFsmState.End();
		}
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateNoahConnect)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000894BC File Offset: 0x000876BC
	private TinyFsmState StateNoahConnect(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			this.TryConnectNoah(false);
			return TinyFsmState.End();
		case 4:
			if (GameModeTitle.m_isReturnFirstTutorial)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGameServerPreLogin)));
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitTouchScreen)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x00089558 File Offset: 0x00087758
	private TinyFsmState StateWaitTouchScreen(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			BackKeyManager.InvalidFlag = false;
			if (this.m_startButton != null)
			{
				this.m_startButton.SetActive(true);
			}
			if (this.m_movetButton != null)
			{
				this.m_movetButton.SetActive(true);
			}
			if (this.m_cacheButton != null)
			{
				this.m_cacheButton.SetActive(true);
			}
			return TinyFsmState.End();
		default:
			if (signal == 100)
			{
				this.NoahBannerDisplay(false);
				if (this.m_startButton != null)
				{
					this.m_startButton.SetActive(false);
				}
				if (this.m_movetButton != null)
				{
					this.m_movetButton.SetActive(false);
				}
				if (this.m_cacheButton != null)
				{
					this.m_cacheButton.SetActive(false);
				}
				if (GameModeTitle.m_isLogined)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeIn)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGameServerPreLogin)));
				}
				GameObject parent = GameObject.Find("UI Root (2D)");
				UIObjectContainer uiobjectContainer = GameObjectUtil.FindChildGameObjectComponent<UIObjectContainer>(parent, "TitleScreen");
				if (uiobjectContainer != null)
				{
					GameObject[] objects = uiobjectContainer.Objects;
					if (objects != null)
					{
						foreach (GameObject gameObject in objects)
						{
							if (gameObject != null)
							{
								gameObject.SetActive(false);
							}
						}
					}
				}
				return TinyFsmState.End();
			}
			if (signal != 111)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTakeoverFunction)));
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("quit_app"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						Application.Quit();
					}
					else if (GeneralWindow.IsNoButtonPressed)
					{
						this.NoahBannerDisplay(true);
					}
					GeneralWindow.Close();
					this.SetUIEffect(true);
				}
			}
			else if (GeneralWindow.IsCreated("cache_clear"))
			{
				if (GeneralWindow.IsButtonPressed)
				{
					if (GeneralWindow.IsYesButtonPressed)
					{
						GeneralUtil.CleanAllCache();
						GeneralWindow.Close();
						GeneralWindow.Create(new GeneralWindow.CInfo
						{
							name = "cache_clear_end",
							caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_confirmation_bar"),
							message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_confirmation_title"),
							anchor_path = "Camera/Anchor_5_MC",
							buttonType = GeneralWindow.ButtonType.Ok
						});
					}
					else
					{
						GeneralWindow.Close();
						this.NoahBannerDisplay(true);
					}
				}
			}
			else if (GeneralWindow.IsCreated("cache_clear_end") && GeneralWindow.IsButtonPressed)
			{
				this.NoahBannerDisplay(true);
				GeneralWindow.Close();
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x0008986C File Offset: 0x00087A6C
	private TinyFsmState StateTakeoverFunction(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			BackKeyManager.InvalidFlag = false;
			this.m_subState = 0;
			this.CreateTakeoverCautionWindow();
			this.NoahBannerDisplay(false);
			return TinyFsmState.End();
		case 4:
		{
			int subState = this.m_subState;
			if (subState != 0)
			{
				if (subState == 1)
				{
					if (this.m_takeoverInput != null && this.m_takeoverInput.IsEndPlay())
					{
						if (this.m_takeoverInput.IsDicide)
						{
							string inputIdText = this.m_takeoverInput.InputIdText;
							string inputPassText = this.m_takeoverInput.InputPassText;
							global::Debug.Log("Input Finished! Input ID is " + inputIdText);
							global::Debug.Log("Input Finished! Input PASS is " + inputPassText);
							if (inputIdText.Length == 0 || inputPassText.Length == 0)
							{
								this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTakeoverError)));
								this.m_subState = 2;
							}
							else
							{
								this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTakeoverExecute)));
								this.m_subState = 2;
							}
						}
						if (this.m_takeoverInput.IsCanceled)
						{
							this.m_subState = 2;
							this.BacktoTouchScreenFromTakeover();
							this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitTouchScreen)));
						}
						this.SetUIEffect(true);
					}
				}
			}
			else if (GeneralWindow.IsCreated("TakeoverCaution") && GeneralWindow.IsButtonPressed)
			{
				bool flag = false;
				if (GeneralWindow.IsYesButtonPressed)
				{
					this.m_subState = 1;
					if (this.m_takeoverInput == null)
					{
						this.m_takeoverInput = base.gameObject.AddComponent<SettingTakeoverInput>();
						this.m_takeoverInput.Setup(this.ANCHOR_PATH);
					}
					if (this.m_takeoverInput != null)
					{
						this.m_takeoverInput.PlayStart();
					}
					flag = true;
				}
				else if (GeneralWindow.IsNoButtonPressed)
				{
					this.BacktoTouchScreenFromTakeover();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitTouchScreen)));
				}
				GeneralWindow.Close();
				if (flag)
				{
					this.SetUIEffect(false);
				}
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00089AD0 File Offset: 0x00087CD0
	private TinyFsmState StateTakeoverError(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			BackKeyManager.InvalidFlag = false;
			this.CreateTakeoverErrorWindow();
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("TakeoverError") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.BacktoTouchScreenFromTakeover();
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitTouchScreen)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x00089B70 File Offset: 0x00087D70
	private TinyFsmState StateTakeoverExecute(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
		{
			BackKeyManager.InvalidFlag = false;
			this.m_isTakeoverLogin = false;
			string inputIdText = this.m_takeoverInput.InputIdText;
			string inputPassText = this.m_takeoverInput.InputPassText;
			ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
			if (serverInterface != null)
			{
				string migrationPassword = NetUtil.CalcMD5String(inputPassText);
				serverInterface.RequestServerMigration(inputIdText, migrationPassword, base.gameObject);
			}
			this.CreateTakeoverExecWindow();
			this.m_timer = 0f;
			return TinyFsmState.End();
		}
		default:
			if (signal != 112)
			{
				if (signal == 113)
				{
					if (GeneralWindow.IsCreated("TakeoverExec"))
					{
						GeneralWindow.Close();
						this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTakeoverError)));
					}
				}
			}
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("TakeoverExec"))
			{
				this.m_timer += Time.deltaTime;
				if (this.m_timer >= GameModeTitle.TAKEOVER_WAIT_TIME)
				{
					this.m_timer = GameModeTitle.TAKEOVER_WAIT_TIME;
				}
				if (this.m_isTakeoverLogin && this.m_timer >= GameModeTitle.TAKEOVER_WAIT_TIME)
				{
					global::Debug.Log("Takeover Finished! Result Success! ");
					GeneralWindow.Close();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateTakeoverFinished)));
				}
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x00089CF8 File Offset: 0x00087EF8
	private TinyFsmState StateTakeoverFinished(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			BackKeyManager.InvalidFlag = false;
			this.CreateTakeoverFinishedWindow();
			global::Debug.Log("Takeover Finished!");
			return TinyFsmState.End();
		case 4:
			if (GeneralWindow.IsCreated("TakeoverFinished") && GeneralWindow.IsButtonPressed)
			{
				this.OnMsgGotoHead();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x00089D80 File Offset: 0x00087F80
	private void CreateTakeoverCautionWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "TakeoverCaution",
			buttonType = GeneralWindow.ButtonType.YesNo,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_text").text
		});
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x00089DE8 File Offset: 0x00087FE8
	private void CreateTakeoverErrorWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "TakeoverError",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_error_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_error_text").text
		});
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x00089E50 File Offset: 0x00088050
	private void CreateTakeoverExecWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "TakeoverExec",
			buttonType = GeneralWindow.ButtonType.None,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_exec_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_exec_text").text
		});
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x00089EB8 File Offset: 0x000880B8
	private void CreateTakeoverFinishedWindow()
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "TakeoverFinished",
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_finished_caption").text,
			message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "takeover_finished_text").text
		});
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x00089F20 File Offset: 0x00088120
	private void BacktoTouchScreenFromTakeover()
	{
		this.NoahBannerDisplay(true);
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x00089F2C File Offset: 0x0008812C
	private TinyFsmState StateGamePushNotification(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			BackKeyManager.InvalidFlag = true;
			return TinyFsmState.End();
		case 1:
			BackKeyManager.InvalidFlag = false;
			this.m_pushNotice = base.gameObject.AddComponent<SettingPartsPushNotice>();
			this.m_pushNotice.Setup(this.ANCHOR_PATH);
			this.m_pushNotice.PlayStart();
			this.m_pushNotice.SetCloseButtonEnabled(false);
			return TinyFsmState.End();
		default:
			if (signal != 100)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGameServerPreLogin)));
			return TinyFsmState.End();
		case 4:
			if (this.m_pushNotice != null && this.m_pushNotice.IsEndPlay())
			{
				if (this.m_pushNotice.IsOverwrite && SystemSaveManager.Instance != null)
				{
					SystemSaveManager.Instance.SaveSystemData();
				}
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGameServerPreLogin)));
				global::Debug.Log("m_fsm.ChangeState(new TinyFsmState(this.StateGameServerPreLogin));");
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x0008A05C File Offset: 0x0008825C
	private TinyFsmState StateGameServerPreLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
			if (serverSessionWatcher != null)
			{
				this.m_isSessionValid = false;
				serverSessionWatcher.ValidateSession(ServerSessionWatcher.ValidateType.PRELOGIN, new ServerSessionWatcher.ValidateSessionEndCallback(this.ValidateSessionCallback));
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isSessionValid)
			{
				global::Debug.Log("GameModeTitle.StateGameServerPreLogin:Finished");
				if (this.m_userIdLabel != null)
				{
					this.m_userIdLabel.gameObject.SetActive(true);
					this.m_userIdLabel.text = this.GetViewUserID();
				}
				bool flag = true;
				if (SystemSaveManager.Instance != null)
				{
					string countryCode = SystemSaveManager.GetCountryCode();
					if (string.IsNullOrEmpty(countryCode))
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAssetBundleInitialize)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetCountryCodeRetry)));
					global::Debug.Log("GameModeTitle.StateGameServerPreLogin:LostCountryCode!!");
				}
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x0008A19C File Offset: 0x0008839C
	private TinyFsmState StateGetCountryCodeRetry(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			this.m_isGetCountry = false;
			ServerInterface serverInterface = GameObjectUtil.FindGameObjectComponent<ServerInterface>("ServerInterface");
			if (serverInterface != null)
			{
				serverInterface.RequestServerGetCountry(base.gameObject);
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isGetCountry)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAssetBundleInitialize)));
				global::Debug.Log("GameModeTitle.StateGetCountryCodeRetry:Finished");
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x0008A244 File Offset: 0x00088444
	private TinyFsmState StateAssetBundleInitialize(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			Screen.sleepTimeout = -2;
			return TinyFsmState.End();
		case 1:
			NetBaseUtil.SetAssetServerURL();
			Screen.sleepTimeout = -1;
			if (Env.useAssetBundle)
			{
				if (AssetBundleLoader.Instance == null)
				{
					AssetBundleLoader.Create();
				}
				if (!AssetBundleLoader.Instance.IsEnableDownlad())
				{
					AssetBundleLoader.Instance.Initialize();
				}
			}
			return TinyFsmState.End();
		case 4:
			if (!Env.useAssetBundle || AssetBundleLoader.Instance.IsEnableDownlad())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateStreamingLoaderInitialize)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x0008A310 File Offset: 0x00088510
	private TinyFsmState StateStreamingLoaderInitialize(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			Screen.sleepTimeout = -2;
			return TinyFsmState.End();
		case 1:
			Screen.sleepTimeout = -1;
			if (StreamingDataLoader.Instance == null)
			{
				StreamingDataKeyRetryProcess process = new StreamingDataKeyRetryProcess(base.gameObject, this);
				NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
				StreamingDataLoader.Create();
				StreamingDataLoader.Instance.Initialize(base.gameObject);
			}
			return TinyFsmState.End();
		case 4:
			if (StreamingDataLoader.Instance.IsEnableDownlad())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckExistDownloadData)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x0008A3D8 File Offset: 0x000885D8
	private TinyFsmState StateCheckExistDownloadData(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (this.m_loader == null)
			{
				GameObject gameObject = new GameObject();
				this.m_loader = gameObject.AddComponent<TitleDataLoader>();
				if (StreamingDataLoader.Instance != null)
				{
					if (SystemSaveManager.Instance != null)
					{
						SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
						if (systemdata != null && !systemdata.IsFlagStatus(SystemData.FlagStatus.FIRST_LAUNCH_TUTORIAL_END) && !GameModeTitle.m_isReturnFirstTutorial)
						{
							if (systemdata.IsFlagStatus(SystemData.FlagStatus.TUTORIAL_BOSS_MAP_1))
							{
								systemdata.SetFlagStatus(SystemData.FlagStatus.FIRST_LAUNCH_TUTORIAL_END, true);
								SystemSaveManager.Instance.SaveSystemData();
							}
							else
							{
								this.m_isFirstTutorial = true;
							}
						}
					}
					List<string> list = new List<string>();
					if (this.m_isFirstTutorial)
					{
						this.m_loader.AddStreamingSoundData("BGM_z01.acb");
						this.m_loader.AddStreamingSoundData("BGM_z01_streamfiles.awb");
						this.m_loader.AddStreamingSoundData("BGM_jingle.acb");
						this.m_loader.AddStreamingSoundData("BGM_jingle_streamfiles.awb");
						this.m_loader.AddStreamingSoundData("se_runners.acb");
					}
					else
					{
						StreamingDataLoader.Instance.GetLoadList(ref list);
						foreach (string text in list)
						{
							bool flag = text.Contains("BGM_z");
							bool flag2 = text.Contains("BGM_boss");
							if (!flag && !flag2)
							{
								this.m_loader.AddStreamingSoundData(text);
							}
						}
					}
				}
				this.m_loader.Setup(this.m_isFirstTutorial);
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_loader.EndCheckExistingDownloadData)
			{
				if (this.m_loader.RequestedDownloadCount > 0)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAskDataDownload)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitDataLoad)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x0008A628 File Offset: 0x00088828
	private TinyFsmState StateAskDataDownload(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			GeneralWindow.CInfo info = default(GeneralWindow.CInfo);
			if (this.m_isFirstTutorial)
			{
				info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_caption").text;
				info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_ask_FirstTutorial").text;
			}
			else if (GameModeTitle.m_isReturnFirstTutorial)
			{
				info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_caption").text;
				info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_ask_FirstTutorialReturn").text;
			}
			else
			{
				info.caption = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_caption").text;
				if (TitleUtil.initUser)
				{
					info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_ask").text;
				}
				else
				{
					info.message = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_load_ask_2").text;
				}
			}
			info.anchor_path = "Camera/Anchor_5_MC";
			info.buttonType = GeneralWindow.ButtonType.YesNo;
			GeneralWindow.Create(info);
			return TinyFsmState.End();
		}
		case 4:
			if (GeneralWindow.IsYesButtonPressed)
			{
				if (GameModeTitle.m_isReturnFirstTutorial)
				{
					GameModeTitle.m_isReturnFirstTutorial = false;
				}
				GeneralWindow.Close();
				SoundManager.BgmPlay("bgm_sys_load", "BGM", false);
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitDataDownload)));
			}
			else if (GeneralWindow.IsNoButtonPressed)
			{
				GeneralWindow.Close();
				GameModeTitle.m_isLogined = false;
				if (GameModeTitle.m_isReturnFirstTutorial)
				{
					GameModeTitle.m_isReturnFirstTutorial = false;
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadTitleResetScene)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitTouchScreen)));
				}
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x0008A840 File Offset: 0x00088A40
	private TinyFsmState StateWaitDataDownload(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_loader != null)
			{
				UnityEngine.Object.Destroy(this.m_loader.gameObject);
				this.m_loader = null;
			}
			Screen.sleepTimeout = -2;
			return TinyFsmState.End();
		case 1:
			Screen.sleepTimeout = -1;
			if (this.m_loadingWindow != null)
			{
				this.m_loadingWindow.PlayStart();
			}
			if (this.m_loader != null)
			{
				this.m_loader.StartLoad();
			}
			this.SetUIEffect(false);
			return TinyFsmState.End();
		case 4:
			if (this.m_loader == null)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndDataLoad)));
			}
			else if (this.m_loader.LoadEnd)
			{
				TextManager.NotLoadSetupCommonText();
				TextManager.NotLoadSetupChaoText();
				TextManager.NotLoadSetupEventText();
				MissionTable.LoadSetup();
				CharacterDataNameInfo.LoadSetup();
				StageAbilityManager.SetupAbilityDataTable();
				OverlapBonusTable overlapBonusTable = GameObjectUtil.FindGameObjectComponent<OverlapBonusTable>("OverlapBonusTable");
				if (overlapBonusTable != null)
				{
					overlapBonusTable.Setup();
				}
				this.SetUIEffect(true);
				if (this.m_loadingWindow != null)
				{
					this.m_loadingWindow.PlayEnd();
				}
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndDataLoad)));
			}
			else if (this.m_loader != null && this.m_loadingWindow != null)
			{
				float num = (float)this.m_loader.RequestedLoadCount;
				float num2 = (float)this.m_loader.LoadEndCount;
				if (num == 0f)
				{
					num = 1f;
				}
				float loadingPercentage = num2 * 100f / num;
				this.m_loadingWindow.SetLoadingPercentage(loadingPercentage);
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x0008AA28 File Offset: 0x00088C28
	private TinyFsmState StateWaitDataLoad(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_loader != null)
			{
				UnityEngine.Object.Destroy(this.m_loader.gameObject);
				this.m_loader = null;
			}
			Screen.sleepTimeout = -2;
			return TinyFsmState.End();
		case 1:
			Screen.sleepTimeout = -1;
			if (this.m_loadingConnect != null)
			{
				this.m_loadingConnect.Setup();
				this.m_loadingConnect.PlayStart(HudNetworkConnect.DisplayType.NO_BG);
			}
			if (this.m_loader != null)
			{
				this.m_loader.StartLoad();
			}
			return TinyFsmState.End();
		case 4:
			if (this.m_loader == null)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndDataLoad)));
			}
			else if (this.m_loader.LoadEnd)
			{
				TextManager.NotLoadSetupCommonText();
				TextManager.NotLoadSetupChaoText();
				TextManager.NotLoadSetupEventText();
				MissionTable.LoadSetup();
				CharacterDataNameInfo.LoadSetup();
				StageAbilityManager.SetupAbilityDataTable();
				OverlapBonusTable overlapBonusTable = GameObjectUtil.FindGameObjectComponent<OverlapBonusTable>("OverlapBonusTable");
				if (overlapBonusTable != null)
				{
					overlapBonusTable.Setup();
				}
				if (this.m_loadingConnect != null)
				{
					this.m_loadingConnect.PlayEnd();
				}
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateEndDataLoad)));
			}
			else if (this.m_loader != null && this.m_loadingWindow != null)
			{
				float num = (float)this.m_loader.RequestedLoadCount;
				float num2 = (float)this.m_loader.LoadEndCount;
				if (num == 0f)
				{
					num = 1f;
				}
				float loadingPercentage = num2 * 100f / num;
				this.m_loadingWindow.SetLoadingPercentage(loadingPercentage);
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x0008AC10 File Offset: 0x00088E10
	private TinyFsmState StateEndDataLoad(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(0);
			}
			return TinyFsmState.End();
		case 1:
			return TinyFsmState.End();
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGameServerLogin)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x0008AC98 File Offset: 0x00088E98
	private TinyFsmState StateGameServerLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(1);
			}
			return TinyFsmState.End();
		case 1:
		{
			ServerSessionWatcher serverSessionWatcher = GameObjectUtil.FindGameObjectComponent<ServerSessionWatcher>("NetMonitor");
			if (serverSessionWatcher != null)
			{
				this.m_isSessionValid = false;
				serverSessionWatcher.ValidateSession(ServerSessionWatcher.ValidateType.LOGIN_OR_RELOGIN, new ServerSessionWatcher.ValidateSessionEndCallback(this.ValidateSessionCallback));
			}
			return TinyFsmState.End();
		}
		case 4:
			if (this.m_isSessionValid)
			{
				GameModeTitle.m_isLogined = true;
				global::Debug.Log("GameModeTitle.StateGameServerLogin:Finished");
				if (this.m_isFirstTutorial)
				{
					this.m_nextSceneName = "s_playingstage";
					this.SetFirstTutorialInfo();
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeIn)));
				}
				else
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetServerContinueParameter)));
				}
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x0008ADB0 File Offset: 0x00088FB0
	private TinyFsmState StateGetServerContinueParameter(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(2);
			}
			return TinyFsmState.End();
		case 1:
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetVariousParameter(base.gameObject);
			}
			return TinyFsmState.End();
		}
		default:
			if (signal != 102)
			{
				return TinyFsmState.End();
			}
			if (RegionManager.Instance.IsUseHardlightAds())
			{
				GameObject gameObject = GameObject.Find("HardlightAds");
				if (gameObject == null)
				{
					gameObject = new GameObject();
					if (gameObject != null)
					{
						gameObject.name = "HardlightAds";
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
						gameObject.AddComponent("HardlightAds");
					}
				}
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckAtom)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x0008AEB8 File Offset: 0x000890B8
	private TinyFsmState StateCheckAtom(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(3);
			}
			this.m_atomInfo = null;
			return TinyFsmState.End();
		case 1:
			if (Binding.Instance != null)
			{
				string urlSchemeStr = Binding.Instance.GetUrlSchemeStr();
				Binding.Instance.ClearUrlSchemeStr();
				if (!string.IsNullOrEmpty(urlSchemeStr))
				{
					string empty = string.Empty;
					string empty2 = string.Empty;
					if (ServerAtomSerial.GetSerialFromScheme(urlSchemeStr, ref empty, ref empty2))
					{
						this.m_atomInfo = new GameModeTitle.AtomDataInfo();
						this.m_atomInfo.campain = empty;
						this.m_atomInfo.serial = empty2;
						GeneralWindow.Create(new GeneralWindow.CInfo
						{
							message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_check"),
							caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "atom_check_caption"),
							anchor_path = "Camera/Anchor_5_MC",
							buttonType = GeneralWindow.ButtonType.Ok
						});
						this.m_subState = 0;
					}
					else
					{
						this.m_subState = 3;
					}
				}
				else
				{
					this.m_subState = 3;
				}
			}
			else
			{
				this.m_subState = 3;
			}
			return TinyFsmState.End();
		case 4:
			switch (this.m_subState)
			{
			case 0:
				if (GeneralWindow.IsButtonPressed)
				{
					bool new_user = true;
					SystemSaveManager instance = SystemSaveManager.Instance;
					if (instance != null)
					{
						SystemData systemdata = instance.GetSystemdata();
						if (systemdata != null)
						{
							new_user = systemdata.IsNewUser();
						}
					}
					ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
					loggedInServerInterface.RequestServerAtomSerial(this.m_atomInfo.campain, this.m_atomInfo.serial, new_user, base.gameObject);
					GeneralWindow.Close();
					this.m_subState = 1;
				}
				break;
			case 1:
				global::Debug.Log("Wait Server");
				return TinyFsmState.End();
			case 2:
				if (GeneralWindow.IsButtonPressed)
				{
					global::Debug.Log("EndText end:");
					GeneralWindow.Close();
					this.m_subState = 3;
				}
				return TinyFsmState.End();
			case 3:
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateCheckNoLoginIncentive)));
				return TinyFsmState.End();
			}
			return TinyFsmState.End();
		case 5:
		{
			int id = e.GetMessage.ID;
			TextManager.TextType type = TextManager.TextType.TEXTTYPE_FIXATION_TEXT;
			int num = id;
			if (num != 61495)
			{
				if (num == 61517)
				{
					MsgServerConnctFailed msgServerConnctFailed = e.GetMessage as MsgServerConnctFailed;
					string cellID = "atom_invalid_serial";
					if (msgServerConnctFailed != null && msgServerConnctFailed.m_status == ServerInterface.StatusCode.UsedSerialCode)
					{
						cellID = "atom_used_serial";
					}
					GeneralWindow.Create(new GeneralWindow.CInfo
					{
						message = TextUtility.GetText(type, "Title", cellID),
						caption = TextUtility.GetText(type, "Title", "atom_failure_caption"),
						anchor_path = "Camera/Anchor_5_MC",
						buttonType = GeneralWindow.ButtonType.Ok
					});
					this.m_subState = 2;
				}
			}
			else
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					message = TextUtility.GetText(type, "Title", "atom_present_get"),
					caption = TextUtility.GetText(type, "Title", "atom_success_caption"),
					anchor_path = "Camera/Anchor_5_MC",
					buttonType = GeneralWindow.ButtonType.Ok
				});
				this.m_subState = 2;
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x0008B230 File Offset: 0x00089430
	private TinyFsmState StateCheckNoLoginIncentive(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(4);
			}
			this.m_atomInfo = null;
			return TinyFsmState.End();
		case 1:
			if (PnoteNotification.CheckEnableGetNoLoginIncentive())
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerGetFacebookIncentive(4, 1, base.gameObject);
					this.m_subState = 0;
				}
			}
			else
			{
				this.m_subState = 2;
			}
			return TinyFsmState.End();
		case 4:
			switch (this.m_subState)
			{
			case 0:
				global::Debug.Log("Wait Server");
				return TinyFsmState.End();
			case 1:
				if (GeneralWindow.IsButtonPressed)
				{
					global::Debug.Log("EndText end:");
					GeneralWindow.Close();
					this.m_subState = 3;
				}
				return TinyFsmState.End();
			case 2:
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateSnsAdditionalData)));
				return TinyFsmState.End();
			default:
				return TinyFsmState.End();
			}
			break;
		case 5:
		{
			int id = e.GetMessage.ID;
			int num = id;
			if (num != 61490)
			{
				if (num == 61517)
				{
					this.m_subState = 2;
				}
			}
			else
			{
				this.m_subState = 2;
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x0008B3A8 File Offset: 0x000895A8
	private TinyFsmState StateSnsAdditionalData(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(5);
			}
			return TinyFsmState.End();
		case 1:
		{
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface != null && socialInterface.IsLoggedIn)
			{
				socialInterface.RequestFriendRankingInfoSet(null, null, SettingPartsSnsAdditional.Mode.BACK_GROUND_LOAD);
			}
			return TinyFsmState.End();
		}
		case 4:
		{
			SocialInterface socialInterface2 = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface2 != null && socialInterface2.IsLoggedIn)
			{
				if (socialInterface2.IsEnableFriendInfo)
				{
					this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StatePushNoticeCheck)));
				}
			}
			else
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StatePushNoticeCheck)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x0008B4A8 File Offset: 0x000896A8
	private TinyFsmState StatePushNoticeCheck(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			if (SystemSaveManager.Instance != null)
			{
				SystemData systemSaveData = SystemSaveManager.GetSystemSaveData();
				if (systemSaveData != null)
				{
					if (systemSaveData.pushNotice)
					{
						PnoteNotification.RequestRegister();
					}
					else
					{
						PnoteNotification.RequestUnregister();
					}
				}
			}
			return TinyFsmState.End();
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateWaitToGetMenuData)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x0008B548 File Offset: 0x00089748
	private TinyFsmState StateWaitToGetMenuData(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(16);
			}
			return TinyFsmState.End();
		case 1:
		{
			NoahHandler.Instance.SetGUID(TitleUtil.GetSystemSaveDataGameId());
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerRetrievePlayerState(base.gameObject);
			}
			return TinyFsmState.End();
		}
		default:
			if (signal != 103)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateAchievementLogin)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x0008B60C File Offset: 0x0008980C
	private TinyFsmState StateAchievementLogin(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(17);
			}
			return TinyFsmState.End();
		case 1:
		{
			AchievementManager.Setup();
			SystemSaveManager instance = SystemSaveManager.Instance;
			if (instance != null)
			{
				SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
				if (systemdata != null && systemdata.achievementCancelCount >= this.ACHIEVEMENT_HIDE_COUNT)
				{
					AchievementManager.RequestSkipAuthenticate();
					return TinyFsmState.End();
				}
			}
			AchievementManager.RequestUpdate();
			return TinyFsmState.End();
		}
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetCostList)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x0008B6DC File Offset: 0x000898DC
	private TinyFsmState StateGetCostList(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(19);
			}
			return TinyFsmState.End();
		case 1:
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetCostList(base.gameObject);
			}
			return TinyFsmState.End();
		}
		default:
			if (signal != 106)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetEventList)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x0008B790 File Offset: 0x00089990
	private TinyFsmState StateGetEventList(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(20);
			}
			return TinyFsmState.End();
		case 1:
		{
			this.m_isSkip = true;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerGetEventList(base.gameObject);
				this.m_isSkip = false;
			}
			return TinyFsmState.End();
		}
		default:
			if (signal != 108)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetMileageMap)));
			return TinyFsmState.End();
		case 4:
			if (this.m_isSkip)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateGetMileageMap)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x0008B87C File Offset: 0x00089A7C
	private TinyFsmState StateGetMileageMap(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(21);
			}
			return TinyFsmState.End();
		case 1:
		{
			this.m_isSkip = true;
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				List<string> list = new List<string>();
				if (list != null)
				{
					SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
					if (socialInterface != null)
					{
						list = SocialInterface.GetGameIdList(socialInterface.FriendList);
					}
				}
				if (list != null && list.Count > 0)
				{
					loggedInServerInterface.RequestServerGetMileageData(list.ToArray(), base.gameObject);
					this.m_isSkip = false;
				}
				else
				{
					loggedInServerInterface.RequestServerGetMileageData(null, base.gameObject);
					this.m_isSkip = false;
				}
			}
			return TinyFsmState.End();
		}
		default:
			if (signal != 107)
			{
				return TinyFsmState.End();
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIapInitialize)));
			return TinyFsmState.End();
		case 4:
			if (this.m_isSkip)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateIapInitialize)));
			}
			return TinyFsmState.End();
		}
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x0008B9C8 File Offset: 0x00089BC8
	private TinyFsmState StateIapInitialize(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(22);
			}
			return TinyFsmState.End();
		case 1:
			this.IapInitializeEndCallback(NativeObserver.IAPResult.ProductsRequestCompleted);
			return TinyFsmState.End();
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadEventResource)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x0008BA58 File Offset: 0x00089C58
	private TinyFsmState StateLoadEventResource(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(23);
			}
			return TinyFsmState.End();
		case 1:
			if (EventManager.Instance != null && EventManager.Instance.IsInEvent())
			{
				this.m_sceneLoader = new GameObject("SceneLoader");
				if (this.m_sceneLoader != null)
				{
					ResourceSceneLoader resourceSceneLoader = this.m_sceneLoader.AddComponent<ResourceSceneLoader>();
					this.m_loadInfoForEvent.m_scenename = "EventResourceCommon" + EventManager.GetResourceName();
					resourceSceneLoader.AddLoadAndResourceManager(this.m_loadInfoForEvent);
				}
				AtlasManager.Instance.StartLoadAtlasForTitle();
			}
			return TinyFsmState.End();
		case 4:
		{
			bool flag = true;
			if (this.m_sceneLoader != null)
			{
				if (this.m_sceneLoader.GetComponent<ResourceSceneLoader>().Loaded && AtlasManager.Instance.IsLoadAtlas())
				{
					flag = true;
					UnityEngine.Object.Destroy(this.m_sceneLoader);
					this.m_sceneLoader = null;
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadingUIData)));
			}
			return TinyFsmState.End();
		}
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x0008BBB4 File Offset: 0x00089DB4
	private TinyFsmState StateLoadingUIData(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			if (this.m_progressBar != null)
			{
				this.m_progressBar.SetState(24);
			}
			return TinyFsmState.End();
		case 1:
			ChaoTextureManager.Instance.RequestTitleLoadChaoTexture();
			return TinyFsmState.End();
		case 4:
			if (ChaoTextureManager.Instance.IsLoaded())
			{
				this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateJailBreakCheck)));
			}
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x0008BC54 File Offset: 0x00089E54
	private TinyFsmState StateJailBreakCheck(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
		{
			CPlusPlusLink instance = CPlusPlusLink.Instance;
			if (instance != null)
			{
				global::Debug.Log("GameModeTitle.StateJailBreakCheck");
				instance.BootGameCheatCheck();
			}
			return TinyFsmState.End();
		}
		case 4:
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateFadeIn)));
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x0008BCE0 File Offset: 0x00089EE0
	private TinyFsmState StateFadeIn(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			CameraFade.StartAlphaFade(Color.black, false, 1f, 0f, new Action(this.FinishFadeCallback));
			SoundManager.BgmFadeOut(0.5f);
			return TinyFsmState.End();
		default:
			if (signal != 109)
			{
				return TinyFsmState.End();
			}
			CameraFade.StartAlphaFade(Color.black, false, -1f);
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadLevel)));
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x0008BD94 File Offset: 0x00089F94
	private TinyFsmState StateLoadLevel(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			BackKeyManager.EndScene();
			this.m_stageInfo.FromTitle = true;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			TimeProfiler.StartCountTime("Title-NextScene");
			NoahHandler.Instance.SetCallback(null);
			NativeObserver.Instance.CheckCurrentTransaction();
			Application.LoadLevel(this.m_nextSceneName);
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
		return TinyFsmState.End();
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x0008BE28 File Offset: 0x0008A028
	private TinyFsmState StateLoadTitleResetScene(TinyFsmEvent e)
	{
		int signal = e.Signal;
		switch (signal + 4)
		{
		case 0:
			return TinyFsmState.End();
		case 1:
			CameraFade.StartAlphaFade(Color.black, false, 2f, 0f, new Action(this.FinishFadeCallback));
			return TinyFsmState.End();
		default:
			if (signal != 109)
			{
				return TinyFsmState.End();
			}
			Application.LoadLevel("s_title_reset");
			return TinyFsmState.End();
		case 4:
			return TinyFsmState.End();
		}
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x0008BEB0 File Offset: 0x0008A0B0
	private void TrySnsLoginButtonActive()
	{
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x0008BEB4 File Offset: 0x0008A0B4
	private void TryConnectNoah(bool isResume)
	{
		bool flag = false;
		if (this.m_initUser)
		{
			RegionManager instance = RegionManager.Instance;
			if (instance != null && instance.IsJapan())
			{
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			if (!isResume)
			{
				NoahHandler.Instance.SetCallback(base.gameObject);
			}
			else
			{
				NoahHandler.Instance.SetCallback(null);
			}
			Noah.Instance.Connect(NoahHandler.consumer_key, NoahHandler.secret_key, NoahHandler.action_id);
		}
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x0008BF3C File Offset: 0x0008A13C
	public void OnApplicationPause(bool flag)
	{
		if (!flag)
		{
			this.TryConnectNoah(true);
		}
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x0008BF4C File Offset: 0x0008A14C
	private void FinishFadeCallback()
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(109);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x0008BF80 File Offset: 0x0008A180
	private void InitEndCallback(MsgSocialNormalResponse msg)
	{
		global::Debug.Log("InitEndCallback");
		this.TrySnsLoginButtonActive();
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x0008BF94 File Offset: 0x0008A194
	private void ValidateSessionCallback(bool isSuccess)
	{
		global::Debug.Log("GameModeTitle.ValidateSessionCallback");
		this.m_isSessionValid = true;
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x0008BFA8 File Offset: 0x0008A1A8
	private void ServerGetVersion_Succeeded(MsgGetVersionSucceed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(101);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x0008BFDC File Offset: 0x0008A1DC
	private void ServerGetVariousParameter_Succeeded(MsgGetVariousParameterSucceed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(102);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x0008C010 File Offset: 0x0008A210
	private void ServerRetrievePlayerState_Succeeded(MsgGetPlayerStateSucceed msg)
	{
		this.m_progressBar.SetState(6);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetCharacterState(base.gameObject);
		}
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x0008C048 File Offset: 0x0008A248
	private void ServerGetCharacterState_Succeeded(MsgGetCharacterStateSucceed msg)
	{
		this.m_progressBar.SetState(7);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetChaoState(base.gameObject);
		}
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x0008C080 File Offset: 0x0008A280
	private void ServerGetChaoState_Succeeded(MsgGetChaoStateSucceed msg)
	{
		this.m_progressBar.SetState(8);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetWheelOptions(base.gameObject);
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x0008C0B8 File Offset: 0x0008A2B8
	private void ServerGetWheelOptions_Succeeded(MsgGetWheelOptionsSucceed msg)
	{
		this.m_progressBar.SetState(9);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetDailyMissionData(base.gameObject);
		}
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x0008C0F0 File Offset: 0x0008A2F0
	private void ServerGetDailyMissionData_Succeeded(MsgGetDailyMissionDataSucceed msg)
	{
		this.m_progressBar.SetState(10);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerGetMessageList(base.gameObject);
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x0008C128 File Offset: 0x0008A328
	private void ServerGetMessageList_Succeeded(MsgGetMessageListSucceed msg)
	{
		this.m_progressBar.SetState(11);
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			this.m_exchangeType = GameModeTitle.RedStarExchangeType.RSRING;
			loggedInServerInterface.RequestServerGetRedStarExchangeList((int)this.m_exchangeType, base.gameObject);
		}
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x0008C170 File Offset: 0x0008A370
	private void ServerGetRedStarExchangeList_Succeeded(MsgGetRedStarExchangeListSucceed msg)
	{
		switch (this.m_exchangeType)
		{
		case GameModeTitle.RedStarExchangeType.RSRING:
			this.m_progressBar.SetState(12);
			break;
		case GameModeTitle.RedStarExchangeType.RING:
			this.m_progressBar.SetState(13);
			break;
		case GameModeTitle.RedStarExchangeType.CHALLENGE:
			this.m_progressBar.SetState(14);
			break;
		case GameModeTitle.RedStarExchangeType.RAIDBOSS_ENERGY:
			this.m_progressBar.SetState(15);
			break;
		}
		bool flag = false;
		this.m_exchangeType++;
		if (this.m_exchangeType >= GameModeTitle.RedStarExchangeType.Count)
		{
			flag = true;
		}
		if (flag)
		{
			ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
			tickerInfo.Init(0);
			if (this.m_fsm != null)
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(103);
				this.m_fsm.Dispatch(signal);
			}
			ServerLoginState loginState = ServerInterface.LoginState;
			if (loginState != null)
			{
				loginState.IsChangeDataVersion = false;
				loginState.IsChangeAssetsVersion = false;
			}
		}
		else
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				if (this.m_exchangeType == GameModeTitle.RedStarExchangeType.RAIDBOSS_ENERGY)
				{
					loggedInServerInterface.RequestServerGetRedStarExchangeList(4, base.gameObject);
				}
				else
				{
					loggedInServerInterface.RequestServerGetRedStarExchangeList((int)this.m_exchangeType, base.gameObject);
				}
			}
		}
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x0008C2A4 File Offset: 0x0008A4A4
	private void ServerGetLeaderboardEntries_Succeeded(MsgGetLeaderboardEntriesSucceed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(104);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x0008C2D8 File Offset: 0x0008A4D8
	private void ServerGetLeagueData_Succeeded(MsgGetLeagueDataSucceed msg)
	{
		RankingLeagueTable.SetupRankingLeagueTable();
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(105);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x0008C310 File Offset: 0x0008A510
	private void GetCostList_Succeeded(MsgGetCostListSucceed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(106);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x0008C344 File Offset: 0x0008A544
	private void ServerAtomSerial_Succeeded(MsgSendAtomSerialSucceed msg)
	{
		this.DispatchMessage(msg);
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x0008C350 File Offset: 0x0008A550
	private void ServerAtomSerial_Failed(MsgServerConnctFailed msg)
	{
		this.DispatchMessage(msg);
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x0008C35C File Offset: 0x0008A55C
	private void ServerGetFacebookIncentive_Succeeded(MsgGetNormalIncentiveSucceed msg)
	{
		this.DispatchMessage(msg);
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x0008C368 File Offset: 0x0008A568
	private void ServerGetFacebookIncentive_Failed(MsgServerConnctFailed msg)
	{
		this.DispatchMessage(msg);
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x0008C374 File Offset: 0x0008A574
	private void ServerMigration_Succeeded(MsgLoginSucceed msg)
	{
		this.m_isTakeoverLogin = true;
		if (SystemSaveManager.Instance != null)
		{
			SystemSaveManager.Instance.DeleteSystemFile();
		}
		if (InformationSaveManager.Instance != null)
		{
			InformationSaveManager.Instance.DeleteInformationFile();
		}
		if (SystemSaveManager.Instance != null)
		{
			SystemSaveManager.SetGameID(msg.m_userId);
			SystemSaveManager.SetGamePassword(msg.m_password);
			SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.SetFlagStatus(SystemData.FlagStatus.FIRST_LAUNCH_TUTORIAL_END, true);
				if (!string.IsNullOrEmpty(msg.m_countryCode))
				{
					SystemSaveManager.SetCountryCode(msg.m_countryCode);
					SystemSaveManager.CheckIAPMessage();
				}
				SystemSaveManager.Instance.CheckLightMode();
				SystemSaveManager.Instance.SaveSystemData();
			}
		}
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x0008C434 File Offset: 0x0008A634
	private void ServerMigration_Failed(MsgServerConnctFailed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal;
			if (msg.m_status == ServerInterface.StatusCode.PassWordError)
			{
				signal = TinyFsmEvent.CreateUserEvent(113);
			}
			else
			{
				signal = TinyFsmEvent.CreateUserEvent(112);
			}
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x0008C484 File Offset: 0x0008A684
	private void ServerGetCountry_Succeeded(MsgGetCountrySucceed msg)
	{
		this.m_isGetCountry = true;
		if (SystemSaveManager.Instance != null)
		{
			SystemSaveManager.SetCountryCode(msg.m_countryCode);
			SystemSaveManager.CheckIAPMessage();
		}
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x0008C4BC File Offset: 0x0008A6BC
	private void DispatchMessage(MessageBase message)
	{
		if (this.m_fsm != null && message != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateMessage(message);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x0008C4F4 File Offset: 0x0008A6F4
	private void ServerGetMileageData_Succeeded()
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(107);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x0008C528 File Offset: 0x0008A728
	private void ServerGetEventList_Succeeded(MsgGetEventListSucceed msg)
	{
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(108);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x0008C55C File Offset: 0x0008A75C
	private void OnConnectNoah(string result)
	{
		bool flag = false;
		switch (Noah.ConvertResultState(int.Parse(result)))
		{
		case Noah.ResultState.Success:
			flag = true;
			break;
		}
		global::Debug.Log("Title: Noah OnConnectNoah called");
		if (!flag)
		{
			return;
		}
		this.NoahBannerDisplay(true);
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x0008C5BC File Offset: 0x0008A7BC
	private void IapInitializeEndCallback(NativeObserver.IAPResult result)
	{
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x0008C5C0 File Offset: 0x0008A7C0
	public void OnTouchedScreen()
	{
		if (this.m_initUser)
		{
			this.m_nextSceneName = "MainMenu";
			SoundManager.SePlay("sys_menu_decide", "SE");
			if (this.m_fsm != null)
			{
				TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
				this.m_fsm.Dispatch(signal);
			}
		}
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x0008C618 File Offset: 0x0008A818
	public void OnTouchedAcknowledgment()
	{
		this.m_nextSceneName = "MainMenu";
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(100);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x0008C668 File Offset: 0x0008A868
	public void OnTouchedAgreement()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		Application.OpenURL(NetBaseUtil.RedirectTrmsOfServicePageUrlForTitle);
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x0008C684 File Offset: 0x0008A884
	private IEnumerator OpenAgreementWindow()
	{
		this.m_agreementText = "test";
		if (this.m_agreementText == null || this.m_agreementText == string.Empty)
		{
			string url = NetUtil.GetWebPageURL(InformationDataTable.Type.TERMS_OF_SERVICE);
			GameObject htmlParserGameObject = HtmlParserFactory.Create(url, HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
			if (htmlParserGameObject == null)
			{
				yield return null;
			}
			HtmlParser htmlParser = htmlParserGameObject.GetComponent<HtmlParser>();
			if (htmlParser == null)
			{
				yield return null;
			}
			if (htmlParser != null)
			{
				while (!htmlParser.IsEndParse)
				{
					yield return null;
				}
				this.m_agreementText = htmlParser.ParsedString;
				UnityEngine.Object.Destroy(htmlParserGameObject);
			}
		}
		if (this.m_agreementText != null && this.m_agreementText != string.Empty)
		{
			TextObject title = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "gw_legal_caption");
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "AgreementLegal",
				anchor_path = "Camera/Anchor_5_MC",
				buttonType = GeneralWindow.ButtonType.Ok,
				caption = title.text,
				message = this.m_agreementText
			});
		}
		yield break;
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x0008C6A0 File Offset: 0x0008A8A0
	public void OnClickMigrationButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		global::Debug.Log("GameModeTitle:Migration button pressed");
		if (this.m_fsm != null)
		{
			TinyFsmEvent signal = TinyFsmEvent.CreateUserEvent(111);
			this.m_fsm.Dispatch(signal);
		}
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x0008C6EC File Offset: 0x0008A8EC
	public void OnClickCacheClearButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		global::Debug.Log("GameModeTitle:cache clear button pressed");
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "cache_clear",
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_bar"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Option", "cash_cashclear_explanation_title"),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.YesNo
		});
		this.NoahBannerDisplay(false);
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x0008C778 File Offset: 0x0008A978
	public void SegaLogoAnimationSkip()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject == null)
		{
			return;
		}
		SoundManager.BgmPlay("bgm_sys_title", "BGM", false);
		if (this.m_touchScreenObject != null)
		{
			this.m_touchScreenObject.SetActive(true);
			TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "ui_Lbl_mainmenu");
			UILabel component = this.m_touchScreenObject.GetComponent<UILabel>();
			if (component != null)
			{
				component.text = text.text;
			}
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_touchScreenObject, "Lbl_mainmenu_sh");
			if (uilabel != null)
			{
				uilabel.text = text.text;
			}
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "img_titlelogo");
		if (gameObject2 != null)
		{
			gameObject2.SetActive(true);
		}
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "title_logo");
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
			animation.enabled = true;
			animation.Play("ui_title_loop_Anim");
		}
		Animation animation2 = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "TitleScreen");
		if (animation2 != null)
		{
			foreach (object obj2 in animation2)
			{
				AnimationState animationState2 = (AnimationState)obj2;
				if (!(animationState2 == null))
				{
					animationState2.time = animationState2.length * 0.99f;
				}
			}
			ActiveAnimation.Play(animation2, "ui_title_intro_all_Anim", Direction.Forward);
		}
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x0008C9A8 File Offset: 0x0008ABA8
	public void SegaLogoAnimationFinishCallback()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject == null)
		{
			return;
		}
		SoundManager.BgmPlay("bgm_sys_title", "BGM", false);
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "title_logo");
		if (animation != null)
		{
			ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, "ui_title_intro_Anim", Direction.Forward);
			EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.TitleLogoAnimationFinishCallback), false);
		}
		Animation animation2 = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "TitleScreen");
		if (animation2 != null)
		{
			ActiveAnimation.Play(animation2, "ui_title_intro_all_Anim", Direction.Forward);
		}
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x0008CA40 File Offset: 0x0008AC40
	public void TitleLogoAnimationFinishCallback()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject == null)
		{
			return;
		}
		Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(gameObject, "title_logo");
		if (animation != null)
		{
			animation.enabled = true;
			animation.Play("ui_title_loop_Anim");
		}
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x0008CA90 File Offset: 0x0008AC90
	public void OnMsgGotoHead()
	{
		if (this.m_fsm != null)
		{
			if (this.m_loader != null)
			{
				UnityEngine.Object.Destroy(this.m_loader);
				this.m_loader = null;
			}
			if (this.m_loadingWindow != null)
			{
				UnityEngine.Object.Destroy(this.m_loadingWindow);
				this.m_loadingWindow = null;
				GameObject gameObject = GameObject.Find("UI Root (2D)");
				if (gameObject != null)
				{
					this.m_loadingWindow = GameObjectUtil.FindChildGameObjectComponent<HudLoadingWindow>(gameObject, "DownloadWindow");
				}
			}
			this.m_fsm.ChangeState(new TinyFsmState(new EventFunction(this.StateLoadTitleResetScene)));
		}
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x0008CB3C File Offset: 0x0008AD3C
	public void StreamingKeyDataRetry()
	{
		StreamingDataKeyRetryProcess process = new StreamingDataKeyRetryProcess(base.gameObject, this);
		NetMonitor.Instance.StartMonitor(process, 0f, HudNetworkConnect.DisplayType.ALL);
		StreamingDataLoader.Instance.LoadServerKey(base.gameObject);
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x0008CB78 File Offset: 0x0008AD78
	private void OnClickPlatformBackButtonEvent()
	{
		global::Debug.Log("GameModeTitle::Platform Back button pressed");
		this.SetUIEffect(false);
		this.NoahBannerDisplay(false);
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "quit_app",
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "gw_quit_app_caption"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "Title", "gw_quit_app_text"),
			anchor_path = "Camera/Anchor_5_MC",
			buttonType = GeneralWindow.ButtonType.YesNo
		});
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x0008CBFC File Offset: 0x0008ADFC
	private void NoahBannerDisplay(bool display)
	{
		if (display)
		{
			Noah.Instance.SetBannerEffect(Noah.BannerEffect.EffectUp);
			Noah.BannerSize size = Noah.BannerSize.SizeWideFillParentWidth;
			Vector2 bannerSize = Noah.Instance.GetBannerSize(size);
			Noah.Instance.ShowBannerView(size, 0f, (float)Screen.height - bannerSize.y);
		}
		else
		{
			Noah.Instance.CloseBanner();
			NoahHandler.Instance.SetCallback(null);
		}
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x0008CC64 File Offset: 0x0008AE64
	private void SetUIEffect(bool flag)
	{
		if (UIEffectManager.Instance != null)
		{
			UIEffectManager.Instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, flag);
		}
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x0008CC84 File Offset: 0x0008AE84
	private string GetViewUserID()
	{
		string text = TitleUtil.GetSystemSaveDataGameId();
		if (text.Length > 7)
		{
			text = text.Insert(6, " ");
			text = text.Insert(3, " ");
		}
		return text;
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x0008CCC0 File Offset: 0x0008AEC0
	private void SetFirstTutorialInfo()
	{
		GameObject gameObject = GameObject.Find("StageInfo");
		if (gameObject != null)
		{
			StageInfo component = gameObject.GetComponent<StageInfo>();
			if (component != null)
			{
				StageInfo.MileageMapInfo mileageMapInfo = new StageInfo.MileageMapInfo();
				mileageMapInfo.m_mapState.m_episode = 1;
				mileageMapInfo.m_mapState.m_chapter = 1;
				mileageMapInfo.m_mapState.m_point = 0;
				mileageMapInfo.m_mapState.m_score = 0L;
				component.SelectedStageName = StageInfo.GetStageNameByIndex(1);
				component.TenseType = TenseType.AFTERNOON;
				component.ExistBoss = true;
				component.BossStage = false;
				component.TutorialStage = false;
				component.FromTitle = false;
				component.FirstTutorial = true;
				component.MileageInfo = mileageMapInfo;
			}
		}
		LoadingInfo loadingInfo = LoadingInfo.CreateLoadingInfo();
		if (loadingInfo != null)
		{
			LoadingInfo.LoadingData info = loadingInfo.GetInfo();
			if (info != null)
			{
				string cellID = CharaName.Name[0];
				string commonText = TextUtility.GetCommonText("CharaName", cellID);
				info.m_titleText = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FirstLoading", "ui_Lbl_title_text").text;
				info.m_mainText = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FirstLoading", "ui_Lbl_main_text").text;
				info.m_optionTutorial = true;
				info.m_texture = null;
			}
		}
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x0008CDEC File Offset: 0x0008AFEC
	private void StreamingDataLoad_Succeed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseSucceed(null, null), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x0008CE0C File Offset: 0x0008B00C
	private void StreamingDataLoad_Failed()
	{
		NetMonitor.Instance.EndMonitorForward(new MsgAssetBundleResponseFailedMonitor(), null, null);
		NetMonitor.Instance.EndMonitorBackward();
	}

	// Token: 0x04001584 RID: 5508
	private const string GameSceneName = "s_playingstage";

	// Token: 0x04001585 RID: 5509
	private const string MainMenuSceneName = "MainMenu";

	// Token: 0x04001586 RID: 5510
	private HudProgressBar m_progressBar;

	// Token: 0x04001587 RID: 5511
	private static bool s_first = true;

	// Token: 0x04001588 RID: 5512
	private readonly string ANCHOR_PATH = "Camera/menu_Anim/MainMenuUI4/Anchor_5_MC";

	// Token: 0x04001589 RID: 5513
	private readonly string VersionStr = "Ver:" + CurrentBundleVersion.version.ToString();

	// Token: 0x0400158A RID: 5514
	private string m_nextSceneName;

	// Token: 0x0400158B RID: 5515
	private SettingPartsPushNotice m_pushNotice;

	// Token: 0x0400158C RID: 5516
	private SettingTakeoverInput m_takeoverInput;

	// Token: 0x0400158D RID: 5517
	private bool m_isTakeoverLogin;

	// Token: 0x0400158E RID: 5518
	private float m_timer;

	// Token: 0x0400158F RID: 5519
	private bool m_isGetCountry;

	// Token: 0x04001590 RID: 5520
	private static readonly float TAKEOVER_WAIT_TIME = 2f;

	// Token: 0x04001591 RID: 5521
	private ResourceSceneLoader.ResourceInfo m_loadInfoForEvent = new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, "EventResourceCommon", true, false, true, "EventResourceCommon", false);

	// Token: 0x04001592 RID: 5522
	private TinyFsmBehavior m_fsm;

	// Token: 0x04001593 RID: 5523
	private StageInfo m_stageInfo;

	// Token: 0x04001594 RID: 5524
	private TitleDataLoader m_loader;

	// Token: 0x04001595 RID: 5525
	private static bool m_isLogined;

	// Token: 0x04001596 RID: 5526
	private bool m_isSessionValid;

	// Token: 0x04001597 RID: 5527
	private bool m_isFirstTutorial;

	// Token: 0x04001598 RID: 5528
	private static bool m_isReturnFirstTutorial;

	// Token: 0x04001599 RID: 5529
	private GameObject m_loginLabel;

	// Token: 0x0400159A RID: 5530
	private GameObject m_touchScreenObject;

	// Token: 0x0400159B RID: 5531
	private GameObject m_startButton;

	// Token: 0x0400159C RID: 5532
	private GameObject m_movetButton;

	// Token: 0x0400159D RID: 5533
	private GameObject m_cacheButton;

	// Token: 0x0400159E RID: 5534
	private GameObject m_sceneLoader;

	// Token: 0x0400159F RID: 5535
	private UILabel m_userIdLabel;

	// Token: 0x040015A0 RID: 5536
	private SettingPartsSnsLogin m_snsLogin;

	// Token: 0x040015A1 RID: 5537
	private HudLoadingWindow m_loadingWindow;

	// Token: 0x040015A2 RID: 5538
	private HudNetworkConnect m_loadingConnect;

	// Token: 0x040015A3 RID: 5539
	private GameModeTitle.AtomDataInfo m_atomInfo;

	// Token: 0x040015A4 RID: 5540
	private readonly int ACHIEVEMENT_HIDE_COUNT = 3;

	// Token: 0x040015A5 RID: 5541
	private bool m_isSendNoahId;

	// Token: 0x040015A6 RID: 5542
	private bool m_isSkip;

	// Token: 0x040015A7 RID: 5543
	private int m_subState;

	// Token: 0x040015A8 RID: 5544
	private bool m_initUser;

	// Token: 0x040015A9 RID: 5545
	private string m_agreementText;

	// Token: 0x040015AA RID: 5546
	private GameModeTitle.RedStarExchangeType m_exchangeType;

	// Token: 0x0200032B RID: 811
	public enum ProgressBarLeaveState
	{
		// Token: 0x040015AC RID: 5548
		IDLE = -1,
		// Token: 0x040015AD RID: 5549
		StateEndDataLoad,
		// Token: 0x040015AE RID: 5550
		StateGameServerLogin,
		// Token: 0x040015AF RID: 5551
		StateGetServerContinueParameter,
		// Token: 0x040015B0 RID: 5552
		StateCheckAtom,
		// Token: 0x040015B1 RID: 5553
		StateCheckNoLoginIncentive,
		// Token: 0x040015B2 RID: 5554
		StateSnsAdditionalData,
		// Token: 0x040015B3 RID: 5555
		StateWaitToGetMenuData_PlayerState,
		// Token: 0x040015B4 RID: 5556
		StateWaitToGetMenuData_CharacterState,
		// Token: 0x040015B5 RID: 5557
		StateWaitToGetMenuData_ChaoState,
		// Token: 0x040015B6 RID: 5558
		StateWaitToGetMenuData_WheelOptions,
		// Token: 0x040015B7 RID: 5559
		StateWaitToGetMenuData_DailyMissionData,
		// Token: 0x040015B8 RID: 5560
		StateWaitToGetMenuData_MessageList,
		// Token: 0x040015B9 RID: 5561
		StateWaitToGetMenuData_RedStarExchangeList,
		// Token: 0x040015BA RID: 5562
		StateWaitToGetMenuData_RingExchangeList,
		// Token: 0x040015BB RID: 5563
		StateWaitToGetMenuData_ChallengeExchangeList,
		// Token: 0x040015BC RID: 5564
		StateWaitToGetMenuData_RaidBossEnergyExchangeList,
		// Token: 0x040015BD RID: 5565
		StateWaitToGetMenuData,
		// Token: 0x040015BE RID: 5566
		StateAchievementLogin,
		// Token: 0x040015BF RID: 5567
		StateGetLeagueData,
		// Token: 0x040015C0 RID: 5568
		StateGetCostList,
		// Token: 0x040015C1 RID: 5569
		StateGetEventList,
		// Token: 0x040015C2 RID: 5570
		StateGetMileageMap,
		// Token: 0x040015C3 RID: 5571
		StateIapInitialize,
		// Token: 0x040015C4 RID: 5572
		StateLoadEventResource,
		// Token: 0x040015C5 RID: 5573
		StateLoadingUIData,
		// Token: 0x040015C6 RID: 5574
		NUM
	}

	// Token: 0x0200032C RID: 812
	private enum EventSignal
	{
		// Token: 0x040015C8 RID: 5576
		SCENE_CHANGE_REQUESTED = 100,
		// Token: 0x040015C9 RID: 5577
		SERVER_GETVERSION_END,
		// Token: 0x040015CA RID: 5578
		SERVER_GET_CONTINUE_PARAMETER_END,
		// Token: 0x040015CB RID: 5579
		SERVER_GETMENUDATA_END,
		// Token: 0x040015CC RID: 5580
		SERVER_GET_RANKING_END,
		// Token: 0x040015CD RID: 5581
		SERVER_GET_LEAGUE_DATA_END,
		// Token: 0x040015CE RID: 5582
		SERVER_GET_COSTLIST_END,
		// Token: 0x040015CF RID: 5583
		SERVER_GET_MILEAGEMAP_END,
		// Token: 0x040015D0 RID: 5584
		SERVER_GET_EVENT_LIST_END,
		// Token: 0x040015D1 RID: 5585
		FADE_END,
		// Token: 0x040015D2 RID: 5586
		SCREEN_TOUCHED,
		// Token: 0x040015D3 RID: 5587
		TAKEOVER_REQUESTED,
		// Token: 0x040015D4 RID: 5588
		TAKEOVER_ERROR,
		// Token: 0x040015D5 RID: 5589
		TAKEOVER_PASSERROR
	}

	// Token: 0x0200032D RID: 813
	private class AtomDataInfo
	{
		// Token: 0x040015D6 RID: 5590
		public string campain;

		// Token: 0x040015D7 RID: 5591
		public string serial;
	}

	// Token: 0x0200032E RID: 814
	private enum RedStarExchangeType
	{
		// Token: 0x040015D9 RID: 5593
		RSRING,
		// Token: 0x040015DA RID: 5594
		RING,
		// Token: 0x040015DB RID: 5595
		CHALLENGE,
		// Token: 0x040015DC RID: 5596
		RAIDBOSS_ENERGY,
		// Token: 0x040015DD RID: 5597
		Count
	}

	// Token: 0x0200032F RID: 815
	private enum SubStateSaveError
	{
		// Token: 0x040015DF RID: 5599
		ShowMessage,
		// Token: 0x040015E0 RID: 5600
		Error
	}

	// Token: 0x02000330 RID: 816
	private enum SubStateTakeover
	{
		// Token: 0x040015E2 RID: 5602
		CautionWindow,
		// Token: 0x040015E3 RID: 5603
		InputIdAndPass,
		// Token: 0x040015E4 RID: 5604
		End
	}

	// Token: 0x02000331 RID: 817
	private enum SubStateCheckAtom
	{
		// Token: 0x040015E6 RID: 5606
		StartText,
		// Token: 0x040015E7 RID: 5607
		WaitServer,
		// Token: 0x040015E8 RID: 5608
		EndText,
		// Token: 0x040015E9 RID: 5609
		End
	}

	// Token: 0x02000332 RID: 818
	private enum SubStateCheckNoLogin
	{
		// Token: 0x040015EB RID: 5611
		WaitServer,
		// Token: 0x040015EC RID: 5612
		EndText,
		// Token: 0x040015ED RID: 5613
		End
	}
}
