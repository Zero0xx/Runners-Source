using System;
using System.Diagnostics;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000549 RID: 1353
public class GeneralWindow : WindowBase
{
	// Token: 0x060029F4 RID: 10740 RVA: 0x001036A8 File Offset: 0x001018A8
	private void Start()
	{
		GeneralWindow.m_generalWindowObject = base.gameObject;
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		Transform parent;
		if (GeneralWindow.m_info.parentGameObject != null)
		{
			parent = GameObjectUtil.FindChildGameObject(GeneralWindow.m_info.parentGameObject, "Anchor_5_MC").transform;
		}
		else if (GeneralWindow.m_info.anchor_path != null)
		{
			parent = gameObject.transform.Find(GeneralWindow.m_info.anchor_path);
		}
		else
		{
			parent = gameObject.transform.Find("Camera/Anchor_5_MC");
		}
		Vector3 localPosition = base.transform.localPosition;
		Vector3 localScale = base.transform.localScale;
		base.transform.parent = parent;
		base.transform.localPosition = localPosition;
		base.transform.localScale = localScale;
		GeneralWindow.SetDisableButton(GeneralWindow.m_disableButton);
		GameObject gameObject2 = GeneralWindow.m_generalWindowObject.transform.Find("window/Lbl_caption").gameObject;
		GeneralWindow.m_captionLabel = gameObject2.GetComponent<UILabel>();
		GeneralWindow.m_captionLabel.text = GeneralWindow.m_info.caption;
		GeneralWindow.m_imageCountLabel = this.m_imgCount;
		GameObject gameObject3 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/textView/ScrollView/Lbl_body").gameObject;
		string str = "window/pattern_btn_use/textView/";
		Transform transform = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_less");
		Transform transform2 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use");
		Transform transform3 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_0");
		Transform transform4 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_1");
		Transform transform5 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_2");
		Transform transform6 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_3");
		Transform transform7 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_4");
		Transform transform8 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_5");
		Transform transform9 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_6");
		Transform transform10 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_use/pattern_7");
		transform.gameObject.SetActive(false);
		transform2.gameObject.SetActive(false);
		transform3.gameObject.SetActive(false);
		transform4.gameObject.SetActive(false);
		transform5.gameObject.SetActive(false);
		transform6.gameObject.SetActive(false);
		transform7.gameObject.SetActive(false);
		transform8.gameObject.SetActive(false);
		transform9.gameObject.SetActive(false);
		transform10.gameObject.SetActive(false);
		bool flag = false;
		if (RegionManager.Instance != null)
		{
			flag = RegionManager.Instance.IsUseSNS();
		}
		switch (GeneralWindow.m_info.buttonType)
		{
		case GeneralWindow.ButtonType.None:
			transform.gameObject.SetActive(true);
			gameObject3 = GeneralWindow.m_generalWindowObject.transform.Find("window/pattern_btn_less/bl_textView/bl_ScrollView/bl_Lbl_body").gameObject;
			str = "window/pattern_btn_less/bl_textView/bl_";
			break;
		case GeneralWindow.ButtonType.Ok:
			transform2.gameObject.SetActive(true);
			transform3.gameObject.SetActive(true);
			break;
		case GeneralWindow.ButtonType.YesNo:
			transform2.gameObject.SetActive(true);
			transform4.gameObject.SetActive(true);
			if (GeneralWindow.m_info.name == "FacebookLogin" && !flag)
			{
				UIImageButton uiimageButton = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(transform4.gameObject, "Btn_yes");
				if (uiimageButton != null)
				{
					uiimageButton.isEnabled = false;
				}
			}
			break;
		case GeneralWindow.ButtonType.ShopCancel:
			transform2.gameObject.SetActive(true);
			transform5.gameObject.SetActive(true);
			break;
		case GeneralWindow.ButtonType.TweetCancel:
			transform2.gameObject.SetActive(true);
			transform6.gameObject.SetActive(true);
			if (!flag)
			{
				UIImageButton uiimageButton2 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(transform6.gameObject, "Btn_post");
				if (uiimageButton2 != null)
				{
					uiimageButton2.isEnabled = false;
				}
			}
			break;
		case GeneralWindow.ButtonType.Close:
			transform2.gameObject.SetActive(true);
			transform7.gameObject.SetActive(true);
			break;
		case GeneralWindow.ButtonType.TweetOk:
			transform2.gameObject.SetActive(true);
			transform8.gameObject.SetActive(true);
			if (!flag)
			{
				UIImageButton uiimageButton3 = GameObjectUtil.FindChildGameObjectComponent<UIImageButton>(transform8.gameObject, "Btn_post");
				if (uiimageButton3 != null)
				{
					uiimageButton3.isEnabled = false;
				}
			}
			break;
		case GeneralWindow.ButtonType.OkNextSkip:
			transform2.gameObject.SetActive(true);
			transform9.gameObject.SetActive(true);
			break;
		case GeneralWindow.ButtonType.OkNextSkipAllSkip:
			transform2.gameObject.SetActive(true);
			transform10.gameObject.SetActive(true);
			break;
		}
		int num = (GeneralWindow.m_info.buttonType != GeneralWindow.ButtonType.None) ? 1 : 0;
		GeneralWindow.m_isChangedBgm = false;
		GeneralWindow.m_playedCloseSE = false;
		GeneralWindow.m_eventCount = 0;
		GeneralWindow.m_messages = null;
		GeneralWindow.m_messageCount = 0;
		GeneralWindow.m_messageLabel = null;
		this.m_textViews[num].SetActive(GeneralWindow.m_info.message != null);
		if (GeneralWindow.m_info.message != null)
		{
			GeneralWindow.m_messages = GeneralWindow.m_info.message.Split(new char[]
			{
				'\f'
			});
			UILabel component = gameObject3.GetComponent<UILabel>();
			component.text = ((GeneralWindow.m_messages.Length < 1) ? null : GeneralWindow.m_messages[GeneralWindow.m_messageCount++]);
			GeneralWindow.m_messageLabel = component;
			GameObject gameObject4 = GeneralWindow.m_generalWindowObject.transform.Find(str + "ScrollView").gameObject;
			UIPanel component2 = gameObject4.GetComponent<UIPanel>();
			float w = component2.clipRange.w;
			float num2 = component.printedSize.y * component.transform.localScale.y;
			if (num2 <= w)
			{
				BoxCollider component3 = GeneralWindow.m_generalWindowObject.transform.Find(str + "ScrollOutline").GetComponent<BoxCollider>();
				component3.enabled = false;
			}
		}
		bool isActiveImageView = GeneralWindow.m_info.isActiveImageView;
		this.m_imgView.SetActive(isActiveImageView);
		if (isActiveImageView)
		{
			this.m_imgItem.SetActive(false);
			this.m_imgChao.SetActive(false);
			this.m_imgDecoEff.SetActive(false);
			this.m_imgName.text = GeneralWindow.m_info.imageName;
			this.m_imgCount.text = GeneralWindow.m_info.imageCount;
		}
		bool flag2 = GeneralWindow.m_info.events != null;
		this.m_eventViews[num].SetActive(flag2);
		if (flag2)
		{
			GeneralWindow.m_messageLabel = this.m_eventTexts[num];
			this.NextEvent();
			this.SetOkNextButton();
		}
		bool isImgView = GeneralWindow.m_info.isImgView;
		this.m_imgView.SetActive(isImgView);
		if (isImgView)
		{
			this.m_imgItem.SetActive(false);
			this.m_imgChao.SetActive(false);
			this.m_imgDecoEff.SetActive(false);
			this.m_textViews[num].SetActive(false);
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(this.m_imgView, "Lbl_explan");
			if (uilabel != null)
			{
				uilabel.text = GeneralWindow.m_info.message;
			}
			UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(this.m_imgView, "img_tutorial_other_character");
			if (uitexture != null)
			{
				GameObject gameObject5 = GameObject.Find(GeneralWindow.m_info.imgTextureName);
				if (gameObject5 != null)
				{
					AssetBundleTexture component4 = gameObject5.GetComponent<AssetBundleTexture>();
					if (component4 != null)
					{
						uitexture.mainTexture = component4.m_tex;
					}
				}
			}
		}
		if (GeneralWindow.m_info.isPlayErrorSe)
		{
			SoundManager.SePlay("sys_error", "SE");
		}
	}

	// Token: 0x060029F5 RID: 10741 RVA: 0x00103E90 File Offset: 0x00102090
	private void Update()
	{
		if (GeneralWindow.m_createdTime < 2f)
		{
			float deltaTime = Time.deltaTime;
			if (GeneralWindow.m_createdTime < 1f && GeneralWindow.m_createdTime + deltaTime >= 1f)
			{
				UILabel[] componentsInChildren = base.gameObject.GetComponentsInChildren<UILabel>();
				if (componentsInChildren != null && componentsInChildren.Length > 0)
				{
					foreach (UILabel uilabel in componentsInChildren)
					{
						uilabel.gameObject.SendMessage("Start");
					}
					global::Debug.Log("GeneralWindow UILabel restart " + componentsInChildren.Length + " !");
				}
			}
			else if (GeneralWindow.m_createdTime < 2f && GeneralWindow.m_createdTime + deltaTime >= 2f)
			{
				UILabel[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<UILabel>();
				if (componentsInChildren2 != null && componentsInChildren2.Length > 0)
				{
					foreach (UILabel uilabel2 in componentsInChildren2)
					{
						uilabel2.gameObject.SendMessage("Start");
					}
					global::Debug.Log("GeneralWindow UILabel restart " + componentsInChildren2.Length + " !!");
				}
			}
			GeneralWindow.m_createdTime += deltaTime;
		}
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x00103FDC File Offset: 0x001021DC
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x060029F7 RID: 10743 RVA: 0x00103FE4 File Offset: 0x001021E4
	private void SendButtonMessage(string patternName, string btnName)
	{
		Transform transform = GeneralWindow.m_generalWindowObject.transform.Find(patternName);
		if (transform != null && transform.gameObject.activeSelf)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(transform.gameObject, btnName);
			if (uibuttonMessage != null)
			{
				uibuttonMessage.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x060029F8 RID: 10744 RVA: 0x00104044 File Offset: 0x00102244
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (GeneralWindow.m_isOpend)
		{
			switch (GeneralWindow.m_info.buttonType)
			{
			case GeneralWindow.ButtonType.Ok:
				this.SendButtonMessage("window/pattern_btn_use/pattern_0", "Btn_ok");
				break;
			case GeneralWindow.ButtonType.YesNo:
				this.SendButtonMessage("window/pattern_btn_use/pattern_1", "Btn_no");
				break;
			case GeneralWindow.ButtonType.ShopCancel:
				this.SendButtonMessage("window/pattern_btn_use/pattern_2", "Btn_cancel");
				break;
			case GeneralWindow.ButtonType.TweetCancel:
				this.SendButtonMessage("window/pattern_btn_use/pattern_3", "Btn_ok");
				break;
			case GeneralWindow.ButtonType.Close:
				this.SendButtonMessage("window/pattern_btn_use/pattern_4", "Btn_close");
				break;
			case GeneralWindow.ButtonType.TweetOk:
				this.SendButtonMessage("window/pattern_btn_use/pattern_5", "Btn_ok");
				break;
			case GeneralWindow.ButtonType.OkNextSkip:
				this.SendButtonMessage("window/pattern_btn_use/pattern_6", "Btn_skip");
				break;
			case GeneralWindow.ButtonType.OkNextSkipAllSkip:
				this.SendButtonMessage("window/pattern_btn_use/pattern_7", "Btn_skip");
				break;
			}
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x060029F9 RID: 10745 RVA: 0x00104148 File Offset: 0x00102348
	private bool NextEvent()
	{
		if (GeneralWindow.m_info.events != null && GeneralWindow.m_eventCount < GeneralWindow.m_info.events.Length)
		{
			GeneralWindow.CInfo.Event @event = GeneralWindow.m_info.events[GeneralWindow.m_eventCount++];
			if (!string.IsNullOrEmpty(@event.bgmCueName))
			{
				if (WindowBodyData.IsBgmStop(@event.bgmCueName))
				{
					SoundManager.BgmStop();
				}
				else if (@event.bgmCueName.Contains(","))
				{
					string[] array = @event.bgmCueName.Split(new char[]
					{
						','
					});
					if (array != null && array.Length > 1)
					{
						SoundManager.BgmPlay(array[0], array[1], false);
					}
				}
				else
				{
					SoundManager.BgmPlay(@event.bgmCueName, "BGM", false);
				}
				GeneralWindow.m_isChangedBgm = true;
			}
			if (!string.IsNullOrEmpty(@event.seCueName))
			{
				SoundManager.SePlay(@event.seCueName, "SE");
			}
			foreach (GeneralWindow.FaceWindowUI[] array3 in new GeneralWindow.FaceWindowUI[][]
			{
				this.m_singleFaceWindowUis,
				this.m_twinFaceWindowUis
			})
			{
				foreach (GeneralWindow.FaceWindowUI faceWindowUI in array3)
				{
					faceWindowUI.m_faceWindowGameObject.SetActive(false);
					faceWindowUI.m_namePlateGameObject.SetActive(false);
					faceWindowUI.m_balloonArrow.SetActive(false);
				}
			}
			Texture texture = (!GeneralWindow.m_info.isSpecialEvent) ? MileageMapUtility.GetWindowBGTexture() : EventUtility.GetBGTexture();
			if (texture != null)
			{
				this.m_eventWindowBgTexture.mainTexture = texture;
			}
			switch (@event.arrowType)
			{
			case ArrowType.MIDDLE:
				this.m_singleFaceWindowUis[0].m_balloonArrow.SetActive(true);
				break;
			case ArrowType.RIGHT:
				this.m_twinFaceWindowUis[1].m_balloonArrow.SetActive(true);
				break;
			case ArrowType.LEFT:
				this.m_twinFaceWindowUis[0].m_balloonArrow.SetActive(true);
				break;
			case ArrowType.TWO_SIDES:
				this.m_twinFaceWindowUis[0].m_balloonArrow.SetActive(true);
				this.m_twinFaceWindowUis[1].m_balloonArrow.SetActive(true);
				break;
			}
			if (@event.faceWindows != null)
			{
				GeneralWindow.FaceWindowUI[] array5 = (@event.faceWindows.Length != 1) ? this.m_twinFaceWindowUis : this.m_singleFaceWindowUis;
				for (int k = 0; k < @event.faceWindows.Length; k++)
				{
					GeneralWindow.CInfo.Event.FaceWindow faceWindow = @event.faceWindows[k];
					GeneralWindow.FaceWindowUI faceWindowUI2 = array5[k];
					foreach (Animation animation in faceWindowUI2.m_faceAnimations)
					{
						animation.gameObject.SetActive(false);
					}
					switch (faceWindow.showingType)
					{
					case ShowingType.NORMAL:
						faceWindowUI2.m_faceWindowGameObject.SetActive(true);
						faceWindowUI2.m_disableFilter.SetActive(false);
						break;
					case ShowingType.DARK:
						faceWindowUI2.m_faceWindowGameObject.SetActive(true);
						faceWindowUI2.m_disableFilter.SetActive(true);
						break;
					case ShowingType.HIDE:
						faceWindowUI2.m_faceWindowGameObject.SetActive(false);
						faceWindowUI2.m_disableFilter.SetActive(false);
						break;
					}
					faceWindowUI2.m_namePlateGameObject.SetActive(!string.IsNullOrEmpty(faceWindow.name));
					faceWindowUI2.m_nameLabel.text = faceWindow.name;
					faceWindowUI2.m_faceTexture.mainTexture = faceWindow.texture;
					Rect uvRect = faceWindowUI2.m_faceTexture.uvRect;
					ReverseType reverseType = faceWindow.reverseType;
					if (reverseType != ReverseType.MIRROR)
					{
						uvRect.width = Mathf.Abs(uvRect.width);
					}
					else
					{
						uvRect.width = -Mathf.Abs(uvRect.width);
					}
					faceWindowUI2.m_faceTexture.uvRect = uvRect;
					EffectType effectType = faceWindow.effectType;
					if (effectType == EffectType.BANG || effectType == EffectType.BLAST)
					{
						GameObject gameObject = faceWindowUI2.m_faceAnimations[faceWindow.effectType - EffectType.BANG].gameObject;
						gameObject.SetActive(false);
						gameObject.SetActive(true);
					}
					string str = "_vibe_skip_Anim";
					string str2 = "_intro_skip_Anim";
					switch (faceWindow.animType)
					{
					case AnimType.VIBRATION:
						str = "_vibe_Anim";
						break;
					case AnimType.FADE_IN:
						str2 = "_intro_Anim";
						break;
					case AnimType.FADE_OUT:
						str2 = "_outro_Anim";
						break;
					}
					ActiveAnimation.Play(faceWindowUI2.m_vibrateAnimation, "ui_gn_window_event_tex_" + faceWindowUI2.m_windowKey + str, Direction.Forward);
					ActiveAnimation.Play(faceWindowUI2.m_fadeAnimation, "ui_gn_window_event_tex_" + faceWindowUI2.m_windowKey + str2, Direction.Forward);
				}
			}
			GeneralWindow.m_messageCount = 0;
			GeneralWindow.m_messages = @event.message.Split(new char[]
			{
				'\f'
			});
			this.EventNextMessage();
			return true;
		}
		return false;
	}

	// Token: 0x060029FA RID: 10746 RVA: 0x0010466C File Offset: 0x0010286C
	private bool EventNextMessage()
	{
		if (GeneralWindow.m_messages != null && GeneralWindow.m_messageCount < GeneralWindow.m_messages.Length)
		{
			GeneralWindow.m_messageLabel.text = GeneralWindow.m_messages[GeneralWindow.m_messageCount++];
			this.m_eventScrollBar.value = 0f;
			return true;
		}
		return false;
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x001046C4 File Offset: 0x001028C4
	private void SetOkNextButton()
	{
		bool flag = (GeneralWindow.m_info.events != null && GeneralWindow.m_eventCount < GeneralWindow.m_info.events.Length) || (GeneralWindow.m_messages != null && GeneralWindow.m_messageCount < GeneralWindow.m_messages.Length);
		if (GeneralWindow.m_info.buttonType == GeneralWindow.ButtonType.OkNextSkip)
		{
			this.m_spEventOkButton.SetActive(!flag);
			this.m_spEventNextButton.SetActive(flag);
		}
		else if (GeneralWindow.m_info.buttonType == GeneralWindow.ButtonType.OkNextSkipAllSkip)
		{
			this.m_eventOkButton.SetActive(!flag);
			this.m_eventNextButton.SetActive(flag);
			this.m_eventAllSkipButton.SetActive(flag);
		}
	}

	// Token: 0x060029FC RID: 10748 RVA: 0x0010477C File Offset: 0x0010297C
	public static GameObject Create(GeneralWindow.CInfo info)
	{
		GeneralWindow.SetUIEffect(false);
		GeneralWindow.m_info = info;
		GeneralWindow.m_disableButton = info.disableButton;
		GeneralWindow.m_pressed.m_isButtonPressed = false;
		GeneralWindow.m_pressed.m_isOkButtonPressed = false;
		GeneralWindow.m_pressed.m_isYesButtonPressed = false;
		GeneralWindow.m_pressed.m_isNoButtonPressed = false;
		GeneralWindow.m_pressed.m_isAllSkipButtonPressed = false;
		GeneralWindow.m_resultPressed = GeneralWindow.m_pressed;
		GeneralWindow.m_created = true;
		GeneralWindow.m_isOpend = true;
		GeneralWindow.m_isChangedBgm = false;
		GeneralWindow.m_playedCloseSE = false;
		GeneralWindow.m_createdTime = 0f;
		if (GeneralWindow.m_generalWindowPrefab == null)
		{
			GeneralWindow.m_generalWindowPrefab = (Resources.Load("Prefabs/UI/GeneralWindow") as GameObject);
		}
		if (GeneralWindow.m_generalWindowObject != null)
		{
			return null;
		}
		GeneralWindow.m_generalWindowObject = (UnityEngine.Object.Instantiate(GeneralWindow.m_generalWindowPrefab, Vector3.zero, Quaternion.identity) as GameObject);
		GeneralWindow.m_generalWindowObject.SetActive(true);
		GeneralWindow.ResetScrollBar();
		SoundManager.SePlay("sys_window_open", "SE");
		Animation component = GeneralWindow.m_generalWindowObject.GetComponent<Animation>();
		if (component != null)
		{
			ActiveAnimation.Play(component, Direction.Forward);
		}
		return GeneralWindow.m_generalWindowObject;
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x0010489C File Offset: 0x00102A9C
	public static bool Close()
	{
		bool playedCloseSE = GeneralWindow.m_playedCloseSE;
		GeneralWindow.m_info = default(GeneralWindow.CInfo);
		GeneralWindow.m_pressed = default(GeneralWindow.Pressed);
		GeneralWindow.m_resultPressed = default(GeneralWindow.Pressed);
		GeneralWindow.m_created = false;
		GeneralWindow.m_isOpend = false;
		GeneralWindow.m_playedCloseSE = false;
		if (GeneralWindow.m_generalWindowObject != null)
		{
			if (!playedCloseSE)
			{
				SoundManager.SePlay("sys_window_close", "SE");
			}
			GeneralWindow.DestroyWindow();
			return true;
		}
		return false;
	}

	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x060029FE RID: 10750 RVA: 0x0010491C File Offset: 0x00102B1C
	public static GeneralWindow.CInfo Info
	{
		get
		{
			return GeneralWindow.m_info;
		}
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x060029FF RID: 10751 RVA: 0x00104924 File Offset: 0x00102B24
	public static bool Created
	{
		get
		{
			return GeneralWindow.m_created;
		}
	}

	// Token: 0x06002A00 RID: 10752 RVA: 0x0010492C File Offset: 0x00102B2C
	public static bool IsCreated(string name)
	{
		return GeneralWindow.Info.name == name;
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x06002A01 RID: 10753 RVA: 0x0010494C File Offset: 0x00102B4C
	public static bool IsButtonPressed
	{
		get
		{
			return GeneralWindow.m_resultPressed.m_isButtonPressed;
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x06002A02 RID: 10754 RVA: 0x00104958 File Offset: 0x00102B58
	public static bool IsOkButtonPressed
	{
		get
		{
			return GeneralWindow.m_resultPressed.m_isOkButtonPressed;
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06002A03 RID: 10755 RVA: 0x00104964 File Offset: 0x00102B64
	public static bool IsYesButtonPressed
	{
		get
		{
			return GeneralWindow.m_resultPressed.m_isYesButtonPressed;
		}
	}

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x06002A04 RID: 10756 RVA: 0x00104970 File Offset: 0x00102B70
	public static bool IsNoButtonPressed
	{
		get
		{
			return GeneralWindow.m_resultPressed.m_isNoButtonPressed;
		}
	}

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0010497C File Offset: 0x00102B7C
	public static bool IsAllSkipButtonPressed
	{
		get
		{
			return GeneralWindow.m_resultPressed.m_isAllSkipButtonPressed;
		}
	}

	// Token: 0x06002A06 RID: 10758 RVA: 0x00104988 File Offset: 0x00102B88
	private void OnClickOkButton()
	{
		if (!GeneralWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			GeneralWindow.m_pressed.m_isOkButtonPressed = true;
			GeneralWindow.m_pressed.m_isButtonPressed = true;
		}
		GeneralWindow.m_isOpend = false;
	}

	// Token: 0x06002A07 RID: 10759 RVA: 0x001049C8 File Offset: 0x00102BC8
	private void OnClickYesButton()
	{
		if (!GeneralWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			GeneralWindow.m_pressed.m_isYesButtonPressed = true;
			GeneralWindow.m_pressed.m_isButtonPressed = true;
		}
		GeneralWindow.m_isOpend = false;
	}

	// Token: 0x06002A08 RID: 10760 RVA: 0x00104A08 File Offset: 0x00102C08
	private void OnClickNoButton()
	{
		if (!GeneralWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_window_close", "SE");
			GeneralWindow.m_pressed.m_isNoButtonPressed = true;
			GeneralWindow.m_pressed.m_isButtonPressed = true;
			GeneralWindow.m_playedCloseSE = true;
		}
		GeneralWindow.m_isOpend = false;
	}

	// Token: 0x06002A09 RID: 10761 RVA: 0x00104A58 File Offset: 0x00102C58
	private void OnClickNextButton()
	{
		if (this.EventNextMessage() || this.NextEvent())
		{
			SoundManager.SePlay("sys_page_skip", "SE");
		}
		this.SetOkNextButton();
	}

	// Token: 0x06002A0A RID: 10762 RVA: 0x00104A94 File Offset: 0x00102C94
	private void OnClickSkipButton()
	{
		if (!GeneralWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_window_close", "SE");
			GeneralWindow.m_pressed.m_isNoButtonPressed = true;
			GeneralWindow.m_pressed.m_isButtonPressed = true;
			GeneralWindow.m_playedCloseSE = true;
		}
		GeneralWindow.m_isOpend = false;
	}

	// Token: 0x06002A0B RID: 10763 RVA: 0x00104AE4 File Offset: 0x00102CE4
	private void OnClickAllSkipButton()
	{
		if (!GeneralWindow.m_pressed.m_isButtonPressed)
		{
			SoundManager.SePlay("sys_window_close", "SE");
			GeneralWindow.m_pressed.m_isButtonPressed = true;
			GeneralWindow.m_pressed.m_isAllSkipButtonPressed = true;
			GeneralWindow.m_playedCloseSE = true;
		}
		GeneralWindow.m_isOpend = false;
	}

	// Token: 0x06002A0C RID: 10764 RVA: 0x00104B34 File Offset: 0x00102D34
	public void OnFinishedCloseAnim()
	{
		GeneralWindow.m_resultPressed = GeneralWindow.m_pressed;
		GeneralWindow.m_created = false;
		if (GeneralWindow.m_info.finishedCloseDelegate != null)
		{
			GeneralWindow.m_info.finishedCloseDelegate();
		}
		GeneralWindow.DestroyWindow();
		if (GeneralWindow.m_isChangedBgm)
		{
			if (GeneralWindow.m_info.isNotPlaybackDefaultBgm)
			{
				SoundManager.BgmStop();
			}
			else if (GeneralWindow.m_info.isSpecialEvent)
			{
				string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.EventTop_BgmName);
				if (string.IsNullOrEmpty(data))
				{
					HudMenuUtility.ChangeMainMenuBGM();
				}
				else
				{
					string cueSheetName = "BGM_" + EventManager.GetEventTypeName(EventManager.Instance.Type);
					SoundManager.BgmChange(data, cueSheetName);
				}
			}
			else
			{
				HudMenuUtility.ChangeMainMenuBGM();
			}
		}
	}

	// Token: 0x06002A0D RID: 10765 RVA: 0x00104BF4 File Offset: 0x00102DF4
	private static void DestroyWindow()
	{
		if (GeneralWindow.m_generalWindowObject != null)
		{
			UnityEngine.Object.Destroy(GeneralWindow.m_generalWindowObject);
			GeneralWindow.m_generalWindowObject = null;
		}
		GeneralWindow.SetUIEffect(true);
	}

	// Token: 0x06002A0E RID: 10766 RVA: 0x00104C28 File Offset: 0x00102E28
	public static void SetCaption(string caption)
	{
		GeneralWindow.m_captionLabel.text = caption;
	}

	// Token: 0x06002A0F RID: 10767 RVA: 0x00104C38 File Offset: 0x00102E38
	public static void SetImageCount(string text)
	{
		GeneralWindow.m_imageCountLabel.text = text;
	}

	// Token: 0x06002A10 RID: 10768 RVA: 0x00104C48 File Offset: 0x00102E48
	public static void SetDisableButton(bool disableButton)
	{
		GeneralWindow.m_disableButton = disableButton;
		if (GeneralWindow.m_generalWindowObject != null)
		{
			foreach (UIButton uibutton in GeneralWindow.m_generalWindowObject.GetComponentsInChildren<UIButton>(true))
			{
				uibutton.isEnabled = !GeneralWindow.m_disableButton;
			}
			foreach (UIImageButton uiimageButton in GeneralWindow.m_generalWindowObject.GetComponentsInChildren<UIImageButton>(true))
			{
				uiimageButton.isEnabled = !GeneralWindow.m_disableButton;
			}
		}
	}

	// Token: 0x06002A11 RID: 10769 RVA: 0x00104CD8 File Offset: 0x00102ED8
	private static void SetUIEffect(bool flag)
	{
		if (UIEffectManager.Instance != null)
		{
			UIEffectManager.Instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, flag);
		}
	}

	// Token: 0x06002A12 RID: 10770 RVA: 0x00104CF8 File Offset: 0x00102EF8
	private static void ResetScrollBar()
	{
		if (GeneralWindow.m_generalWindowObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(GeneralWindow.m_generalWindowObject, "textView");
			if (gameObject != null)
			{
				UIScrollBar uiscrollBar = GameObjectUtil.FindChildGameObjectComponent<UIScrollBar>(gameObject, "Scroll_Bar");
				if (uiscrollBar != null)
				{
					uiscrollBar.value = 0f;
				}
			}
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(GeneralWindow.m_generalWindowObject, "bl_textView");
			if (gameObject2 != null)
			{
				UIScrollBar uiscrollBar2 = GameObjectUtil.FindChildGameObjectComponent<UIScrollBar>(gameObject2, "bl_Scroll_Bar");
				if (uiscrollBar2 != null)
				{
					uiscrollBar2.value = 0f;
				}
			}
		}
	}

	// Token: 0x06002A13 RID: 10771 RVA: 0x00104D94 File Offset: 0x00102F94
	public static Texture2D GetDummyTexture(int index)
	{
		Color[] array = new Color[]
		{
			Color.red,
			Color.blue,
			Color.green,
			Color.yellow,
			Color.white,
			Color.cyan,
			Color.gray,
			Color.magenta
		};
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, false);
		texture2D.SetPixel(0, 0, Color.black);
		texture2D.SetPixel(1, 0, Color.black);
		checked
		{
			texture2D.SetPixel(0, 1, array[(int)((IntPtr)(unchecked((ulong)index % (ulong)((long)array.Length))))]);
			texture2D.SetPixel(1, 1, array[(int)((IntPtr)(unchecked((ulong)index % (ulong)((long)array.Length))))]);
			texture2D.Apply();
			return texture2D;
		}
	}

	// Token: 0x06002A14 RID: 10772 RVA: 0x00104E94 File Offset: 0x00103094
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x06002A15 RID: 10773 RVA: 0x00104EA8 File Offset: 0x001030A8
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x0400250E RID: 9486
	private const char FORM_FEED_CODE = '\f';

	// Token: 0x0400250F RID: 9487
	private static GameObject m_generalWindowPrefab;

	// Token: 0x04002510 RID: 9488
	private static GameObject m_generalWindowObject;

	// Token: 0x04002511 RID: 9489
	private static GeneralWindow.CInfo m_info;

	// Token: 0x04002512 RID: 9490
	private static bool m_disableButton;

	// Token: 0x04002513 RID: 9491
	private static UILabel m_captionLabel;

	// Token: 0x04002514 RID: 9492
	private static UILabel m_imageCountLabel;

	// Token: 0x04002515 RID: 9493
	private static GeneralWindow.Pressed m_pressed;

	// Token: 0x04002516 RID: 9494
	private static GeneralWindow.Pressed m_resultPressed;

	// Token: 0x04002517 RID: 9495
	private static bool m_isChangedBgm;

	// Token: 0x04002518 RID: 9496
	private static bool m_created;

	// Token: 0x04002519 RID: 9497
	private static bool m_isOpend;

	// Token: 0x0400251A RID: 9498
	private static bool m_playedCloseSE;

	// Token: 0x0400251B RID: 9499
	private static int m_eventCount;

	// Token: 0x0400251C RID: 9500
	private static string[] m_messages;

	// Token: 0x0400251D RID: 9501
	private static int m_messageCount;

	// Token: 0x0400251E RID: 9502
	private static UILabel m_messageLabel;

	// Token: 0x0400251F RID: 9503
	[SerializeField]
	private GameObject[] m_textViews = new GameObject[2];

	// Token: 0x04002520 RID: 9504
	[SerializeField]
	private GameObject m_imgView;

	// Token: 0x04002521 RID: 9505
	[SerializeField]
	private GameObject m_imgItem;

	// Token: 0x04002522 RID: 9506
	[SerializeField]
	private GameObject m_imgChao;

	// Token: 0x04002523 RID: 9507
	[SerializeField]
	private UILabel m_imgName;

	// Token: 0x04002524 RID: 9508
	[SerializeField]
	private UILabel m_imgCount;

	// Token: 0x04002525 RID: 9509
	[SerializeField]
	private GameObject m_imgDecoEff;

	// Token: 0x04002526 RID: 9510
	[SerializeField]
	private GameObject[] m_eventViews = new GameObject[2];

	// Token: 0x04002527 RID: 9511
	[SerializeField]
	private UILabel[] m_eventTexts = new UILabel[2];

	// Token: 0x04002528 RID: 9512
	[SerializeField]
	private GameObject m_eventOkButton;

	// Token: 0x04002529 RID: 9513
	[SerializeField]
	private GameObject m_eventNextButton;

	// Token: 0x0400252A RID: 9514
	[SerializeField]
	private GameObject m_eventAllSkipButton;

	// Token: 0x0400252B RID: 9515
	[SerializeField]
	private GameObject m_spEventOkButton;

	// Token: 0x0400252C RID: 9516
	[SerializeField]
	private GameObject m_spEventNextButton;

	// Token: 0x0400252D RID: 9517
	[SerializeField]
	private UIScrollBar m_eventScrollBar;

	// Token: 0x0400252E RID: 9518
	private static float m_createdTime;

	// Token: 0x0400252F RID: 9519
	[SerializeField]
	private GeneralWindow.FaceWindowUI[] m_singleFaceWindowUis = new GeneralWindow.FaceWindowUI[1];

	// Token: 0x04002530 RID: 9520
	[SerializeField]
	private GeneralWindow.FaceWindowUI[] m_twinFaceWindowUis = new GeneralWindow.FaceWindowUI[2];

	// Token: 0x04002531 RID: 9521
	[SerializeField]
	public UITexture m_eventWindowBgTexture;

	// Token: 0x0200054A RID: 1354
	public enum ButtonType
	{
		// Token: 0x04002533 RID: 9523
		None,
		// Token: 0x04002534 RID: 9524
		Ok,
		// Token: 0x04002535 RID: 9525
		YesNo,
		// Token: 0x04002536 RID: 9526
		ShopCancel,
		// Token: 0x04002537 RID: 9527
		TweetCancel,
		// Token: 0x04002538 RID: 9528
		Close,
		// Token: 0x04002539 RID: 9529
		TweetOk,
		// Token: 0x0400253A RID: 9530
		OkNextSkip,
		// Token: 0x0400253B RID: 9531
		OkNextSkipAllSkip
	}

	// Token: 0x0200054B RID: 1355
	public struct CInfo
	{
		// Token: 0x0400253C RID: 9532
		public bool isImgView;

		// Token: 0x0400253D RID: 9533
		public string imgTextureName;

		// Token: 0x0400253E RID: 9534
		public string name;

		// Token: 0x0400253F RID: 9535
		public GeneralWindow.ButtonType buttonType;

		// Token: 0x04002540 RID: 9536
		public string caption;

		// Token: 0x04002541 RID: 9537
		public string anchor_path;

		// Token: 0x04002542 RID: 9538
		public GameObject parentGameObject;

		// Token: 0x04002543 RID: 9539
		public bool disableButton;

		// Token: 0x04002544 RID: 9540
		public string message;

		// Token: 0x04002545 RID: 9541
		public string imageName;

		// Token: 0x04002546 RID: 9542
		public string imageCount;

		// Token: 0x04002547 RID: 9543
		public GeneralWindow.CInfo.Event[] events;

		// Token: 0x04002548 RID: 9544
		public GeneralWindow.CInfo.FinishedCloseDelegate finishedCloseDelegate;

		// Token: 0x04002549 RID: 9545
		public bool isPlayErrorSe;

		// Token: 0x0400254A RID: 9546
		public bool isNotPlaybackDefaultBgm;

		// Token: 0x0400254B RID: 9547
		public bool isActiveImageView;

		// Token: 0x0400254C RID: 9548
		public bool isSpecialEvent;

		// Token: 0x0200054C RID: 1356
		public struct Event
		{
			// Token: 0x0400254D RID: 9549
			public GeneralWindow.CInfo.Event.FaceWindow[] faceWindows;

			// Token: 0x0400254E RID: 9550
			public ArrowType arrowType;

			// Token: 0x0400254F RID: 9551
			public string bgmCueName;

			// Token: 0x04002550 RID: 9552
			public string seCueName;

			// Token: 0x04002551 RID: 9553
			public string message;

			// Token: 0x0200054D RID: 1357
			public class FaceWindow
			{
				// Token: 0x04002552 RID: 9554
				public Texture texture;

				// Token: 0x04002553 RID: 9555
				public string name;

				// Token: 0x04002554 RID: 9556
				public EffectType effectType;

				// Token: 0x04002555 RID: 9557
				public AnimType animType;

				// Token: 0x04002556 RID: 9558
				public ReverseType reverseType;

				// Token: 0x04002557 RID: 9559
				public ShowingType showingType;
			}
		}

		// Token: 0x02000A9F RID: 2719
		// (Invoke) Token: 0x060048B6 RID: 18614
		public delegate void FinishedCloseDelegate();
	}

	// Token: 0x0200054E RID: 1358
	private struct Pressed
	{
		// Token: 0x04002558 RID: 9560
		public bool m_isButtonPressed;

		// Token: 0x04002559 RID: 9561
		public bool m_isOkButtonPressed;

		// Token: 0x0400255A RID: 9562
		public bool m_isYesButtonPressed;

		// Token: 0x0400255B RID: 9563
		public bool m_isNoButtonPressed;

		// Token: 0x0400255C RID: 9564
		public bool m_isAllSkipButtonPressed;
	}

	// Token: 0x0200054F RID: 1359
	[Serializable]
	private class FaceWindowUI
	{
		// Token: 0x0400255D RID: 9565
		[SerializeField]
		public string m_windowKey;

		// Token: 0x0400255E RID: 9566
		[SerializeField]
		public GameObject m_faceWindowGameObject;

		// Token: 0x0400255F RID: 9567
		[SerializeField]
		public UITexture m_faceTexture;

		// Token: 0x04002560 RID: 9568
		[SerializeField]
		public GameObject m_namePlateGameObject;

		// Token: 0x04002561 RID: 9569
		[SerializeField]
		public UILabel m_nameLabel;

		// Token: 0x04002562 RID: 9570
		[SerializeField]
		public GameObject m_balloonArrow;

		// Token: 0x04002563 RID: 9571
		[SerializeField]
		public GameObject m_disableFilter;

		// Token: 0x04002564 RID: 9572
		[SerializeField]
		public Animation m_fadeAnimation;

		// Token: 0x04002565 RID: 9573
		[SerializeField]
		public Animation m_vibrateAnimation;

		// Token: 0x04002566 RID: 9574
		[SerializeField]
		public Animation[] m_faceAnimations = new Animation[2];
	}
}
