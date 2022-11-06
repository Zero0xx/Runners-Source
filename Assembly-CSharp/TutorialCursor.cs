using System;
using UnityEngine;

// Token: 0x02000568 RID: 1384
public class TutorialCursor : MonoBehaviour
{
	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x06002A8E RID: 10894 RVA: 0x00107CE8 File Offset: 0x00105EE8
	public static TutorialCursor Instance
	{
		get
		{
			return TutorialCursor.m_instance;
		}
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x00107CF0 File Offset: 0x00105EF0
	private void Awake()
	{
		if (TutorialCursor.m_instance == null)
		{
			TutorialCursor.m_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x00107D24 File Offset: 0x00105F24
	private void Start()
	{
		this.EntryBackKeyCallBack();
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x00107D2C File Offset: 0x00105F2C
	private void OnDestroy()
	{
		if (TutorialCursor.m_instance == this)
		{
			TutorialCursor.m_instance.RemoveBackKeyCallBack();
			TutorialCursor.m_instance = null;
		}
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x00107D5C File Offset: 0x00105F5C
	public void Setup()
	{
		for (int i = 0; i < 18; i++)
		{
			Transform transform = base.gameObject.transform.FindChild(TutorialCursor.ParamTable[i].m_name);
			if (transform != null)
			{
				this.m_cursorObj[i] = transform.gameObject;
				if (this.m_cursorObj[i] != null)
				{
					this.m_cursorObj[i].SetActive(false);
				}
			}
		}
		this.SetUIButtonMessage(TutorialCursor.Type.OPTION);
		this.SetUIButtonMessage(TutorialCursor.Type.ITEMSELECT_SUBCHARA);
		this.m_draggablePanel = HudMenuUtility.GetMainMenuDraggablePanel();
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x00107DF0 File Offset: 0x00105FF0
	private void SetUIButtonMessage(TutorialCursor.Type type)
	{
		if (this.m_cursorObj[(int)type] != null)
		{
			Transform transform = this.m_cursorObj[(int)type].transform.FindChild("blinder/0_all");
			if (transform != null)
			{
				GameObject gameObject = transform.gameObject;
				UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
				if (uibuttonMessage == null)
				{
					uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				}
				if (uibuttonMessage != null)
				{
					uibuttonMessage.enabled = true;
					uibuttonMessage.trigger = UIButtonMessage.Trigger.OnClick;
					uibuttonMessage.target = base.gameObject;
					uibuttonMessage.functionName = "OnOptionTouchScreen";
				}
			}
		}
	}

	// Token: 0x06002A94 RID: 10900 RVA: 0x00107E88 File Offset: 0x00106088
	public void OnStartTutorialCursor(TutorialCursor.Type type)
	{
		this.m_optionTouch = false;
		this.m_created = true;
		BackKeyManager.AddWindowCallBack(base.gameObject);
		this.SetTutorialCursor(type, true);
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x00107EAC File Offset: 0x001060AC
	public void OnEndTutorialCursor(TutorialCursor.Type type)
	{
		this.m_optionTouch = false;
		this.m_created = false;
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
		this.SetTutorialCursor(type, false);
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x00107ED0 File Offset: 0x001060D0
	public bool IsOptionTouchScreen()
	{
		return this.m_optionTouch;
	}

	// Token: 0x06002A97 RID: 10903 RVA: 0x00107ED8 File Offset: 0x001060D8
	public bool IsCreated()
	{
		return this.m_created;
	}

	// Token: 0x06002A98 RID: 10904 RVA: 0x00107EE0 File Offset: 0x001060E0
	private void SetTutorialCursor(TutorialCursor.Type type, bool active)
	{
		if (type < TutorialCursor.Type.NUM)
		{
			if (active)
			{
				for (int i = 0; i < 18; i++)
				{
					if (this.m_cursorObj[i] != null)
					{
						if (type == (TutorialCursor.Type)i)
						{
							this.m_cursorObj[i].SetActive(true);
						}
						else
						{
							this.m_cursorObj[i].SetActive(false);
						}
					}
				}
				this.m_type = type;
			}
			else
			{
				if (this.m_cursorObj[(int)type] != null)
				{
					this.m_cursorObj[(int)type].SetActive(false);
				}
				this.m_type = TutorialCursor.Type.NONE;
			}
			this.SetDraggablePanel(!active);
		}
	}

	// Token: 0x06002A99 RID: 10905 RVA: 0x00107F88 File Offset: 0x00106188
	private void SetDraggablePanel(bool on)
	{
		if (this.m_draggablePanel != null)
		{
			this.m_draggablePanel.enabled = on;
		}
	}

	// Token: 0x06002A9A RID: 10906 RVA: 0x00107FA8 File Offset: 0x001061A8
	private void OnOptionTouchScreen()
	{
		this.m_optionTouch = true;
	}

	// Token: 0x06002A9B RID: 10907 RVA: 0x00107FB4 File Offset: 0x001061B4
	private void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x00107FC4 File Offset: 0x001061C4
	private void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x06002A9D RID: 10909 RVA: 0x00107FD4 File Offset: 0x001061D4
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_created)
		{
			bool flag = false;
			switch (this.m_type)
			{
			case TutorialCursor.Type.OPTION:
				flag = true;
				break;
			case TutorialCursor.Type.CHARASELECT_LEVEL_UP:
				flag = true;
				break;
			case TutorialCursor.Type.ITEMSELECT_SUBCHARA:
				flag = true;
				break;
			}
			if (flag)
			{
				this.OnOptionTouchScreen();
				msg.StaySequence();
			}
		}
	}

	// Token: 0x06002A9E RID: 10910 RVA: 0x00108040 File Offset: 0x00106240
	public static TutorialCursor GetTutorialCursor()
	{
		TutorialCursor instance = TutorialCursor.Instance;
		if (instance == null)
		{
			GameObject gameObject = Resources.Load("Prefabs/UI/tutorial_sign") as GameObject;
			GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
			if (gameObject != null && cameraUIObject != null)
			{
				UnityEngine.Object.Instantiate(gameObject, Vector3.zero, Quaternion.identity);
				instance = TutorialCursor.Instance;
				if (instance != null)
				{
					instance.Setup();
					Vector3 localPosition = instance.gameObject.transform.localPosition;
					Vector3 localScale = instance.gameObject.transform.localScale;
					instance.gameObject.transform.parent = cameraUIObject.transform;
					instance.gameObject.transform.localPosition = localPosition;
					instance.gameObject.transform.localScale = localScale;
				}
			}
		}
		return instance;
	}

	// Token: 0x06002A9F RID: 10911 RVA: 0x00108114 File Offset: 0x00106314
	public static void StartTutorialCursor(TutorialCursor.Type type)
	{
		TutorialCursor tutorialCursor = TutorialCursor.GetTutorialCursor();
		if (tutorialCursor != null)
		{
			tutorialCursor.OnStartTutorialCursor(type);
		}
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x0010813C File Offset: 0x0010633C
	public static void EndTutorialCursor(TutorialCursor.Type type)
	{
		TutorialCursor tutorialCursor = TutorialCursor.GetTutorialCursor();
		if (tutorialCursor != null)
		{
			tutorialCursor.OnEndTutorialCursor(type);
		}
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x00108164 File Offset: 0x00106364
	public static bool IsTouchScreen()
	{
		TutorialCursor tutorialCursor = TutorialCursor.GetTutorialCursor();
		return tutorialCursor != null && tutorialCursor.IsOptionTouchScreen();
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x0010818C File Offset: 0x0010638C
	public static void DestroyTutorialCursor()
	{
		TutorialCursor instance = TutorialCursor.Instance;
		if (instance != null)
		{
			instance.m_created = false;
			instance.SetDraggablePanel(true);
			UnityEngine.Object.Destroy(instance.gameObject);
		}
	}

	// Token: 0x04002604 RID: 9732
	private static readonly TutorialCursorParam[] ParamTable = new TutorialCursorParam[]
	{
		new TutorialCursorParam("Anchor_5_MC/pattern_ch_chao"),
		new TutorialCursorParam("Anchor_5_MC/pattern_ch_main"),
		new TutorialCursorParam("Anchor_5_MC/pattern_it_laser"),
		new TutorialCursorParam("Anchor_5_MC/pattern_mm_chao"),
		new TutorialCursorParam("Anchor_9_BR/pattern_mm_play"),
		new TutorialCursorParam("Anchor_8_BC/pattern_mm_page3"),
		new TutorialCursorParam("Anchor_8_BC/pattern_mm_roulette"),
		new TutorialCursorParam("Anchor_5_MC/pattern_mm_boss_play"),
		new TutorialCursorParam("Anchor_6_MR/pattern_ro_spin"),
		new TutorialCursorParam("Anchor_5_MC/pattern_ro_ok"),
		new TutorialCursorParam("Anchor_7_BL/pattern_back"),
		new TutorialCursorParam("Anchor_5_MC/pattern_option"),
		new TutorialCursorParam("Anchor_5_MC/pattern_pl_main"),
		new TutorialCursorParam("Anchor_5_MC/pattern_pl_mainsub"),
		new TutorialCursorParam("Anchor_5_MC/pattern_pl_levelup"),
		new TutorialCursorParam("Anchor_5_MC/pattern_pl_change"),
		new TutorialCursorParam("Anchor_5_MC/pattern_ro_premium"),
		new TutorialCursorParam("Anchor_8_BC/pattern_st_item")
	};

	// Token: 0x04002605 RID: 9733
	private GameObject[] m_cursorObj = new GameObject[18];

	// Token: 0x04002606 RID: 9734
	private UIDraggablePanel m_draggablePanel;

	// Token: 0x04002607 RID: 9735
	private TutorialCursor.Type m_type = TutorialCursor.Type.NONE;

	// Token: 0x04002608 RID: 9736
	private bool m_optionTouch;

	// Token: 0x04002609 RID: 9737
	private bool m_created;

	// Token: 0x0400260A RID: 9738
	private static TutorialCursor m_instance = null;

	// Token: 0x02000569 RID: 1385
	public enum Type
	{
		// Token: 0x0400260C RID: 9740
		CHAOSELECT_CHAO,
		// Token: 0x0400260D RID: 9741
		CHAOSELECT_MAIN,
		// Token: 0x0400260E RID: 9742
		ITEMSELECT_LASER,
		// Token: 0x0400260F RID: 9743
		MAINMENU_CHAO,
		// Token: 0x04002610 RID: 9744
		MAINMENU_PLAY,
		// Token: 0x04002611 RID: 9745
		MAINMENU_PAGE,
		// Token: 0x04002612 RID: 9746
		MAINMENU_ROULETTE,
		// Token: 0x04002613 RID: 9747
		MAINMENU_BOSS_PLAY,
		// Token: 0x04002614 RID: 9748
		ROULETTE_SPIN,
		// Token: 0x04002615 RID: 9749
		ROULETTE_OK,
		// Token: 0x04002616 RID: 9750
		BACK,
		// Token: 0x04002617 RID: 9751
		OPTION,
		// Token: 0x04002618 RID: 9752
		CHARASELECT_CHARA,
		// Token: 0x04002619 RID: 9753
		CHARASELECT_MAIN,
		// Token: 0x0400261A RID: 9754
		CHARASELECT_LEVEL_UP,
		// Token: 0x0400261B RID: 9755
		ITEMSELECT_SUBCHARA,
		// Token: 0x0400261C RID: 9756
		ROULETTE_TOP_PAGE,
		// Token: 0x0400261D RID: 9757
		STAGE_ITEM,
		// Token: 0x0400261E RID: 9758
		NUM,
		// Token: 0x0400261F RID: 9759
		NONE
	}
}
