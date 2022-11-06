using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D7 RID: 1239
public class MenuPlayerSetShortCutMenu : MonoBehaviour
{
	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x060024AC RID: 9388 RVA: 0x000DB88C File Offset: 0x000D9A8C
	// (set) Token: 0x060024AD RID: 9389 RVA: 0x000DB894 File Offset: 0x000D9A94
	public bool IsEndSetup
	{
		get
		{
			return this.m_isEndSetup;
		}
		private set
		{
		}
	}

	// Token: 0x060024AE RID: 9390 RVA: 0x000DB898 File Offset: 0x000D9A98
	private void OnEnable()
	{
		if (!this.m_isEndSetup)
		{
			return;
		}
		foreach (MenuPlayerSetShortCutButton menuPlayerSetShortCutButton in this.m_buttons)
		{
			if (!(menuPlayerSetShortCutButton == null))
			{
				CharaType chara = menuPlayerSetShortCutButton.Chara;
				if (SaveDataManager.Instance.CharaData.Status[(int)chara] != 0)
				{
					menuPlayerSetShortCutButton.SetIconLock(false);
				}
			}
		}
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x000DB948 File Offset: 0x000D9B48
	public void Setup(MenuPlayerSetShortCutMenu.ShortCutCallback callback)
	{
		this.m_callback = callback;
		base.StartCoroutine(this.OnSetupCoroutine());
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x000DB960 File Offset: 0x000D9B60
	private IEnumerator OnSetupCoroutine()
	{
		List<GameObject> buttonObjectList = null;
		int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
		for (;;)
		{
			buttonObjectList = GameObjectUtil.FindChildGameObjects(base.gameObject, "ui_player_set_pager_scroll(Clone)");
			if (buttonObjectList != null && buttonObjectList.Count >= openedCharaCount)
			{
				break;
			}
			yield return null;
		}
		int buttonIndex = 0;
		for (int charaIndex = 0; charaIndex < 29; charaIndex++)
		{
			if (buttonIndex >= buttonObjectList.Count)
			{
				break;
			}
			CharaType charaType = (CharaType)charaIndex;
			if (MenuPlayerSetUtil.IsOpenedCharacter(charaType))
			{
				GameObject buttonObject = buttonObjectList[buttonIndex];
				buttonIndex++;
				if (!(buttonObject == null))
				{
					MenuPlayerSetShortCutButton button = buttonObject.AddComponent<MenuPlayerSetShortCutButton>();
					bool isLocked = SaveDataManager.Instance.CharaData.Status[charaIndex] == 0;
					button.Setup(charaType, isLocked);
					button.SetCallback(new MenuPlayerSetShortCutButton.ButtonClickedCallback(this.ButtonClickedCallback));
					this.m_buttons.Add(button);
				}
			}
		}
		UIRectItemStorage itemStorage = GameObjectUtil.FindChildGameObjectComponent<UIRectItemStorage>(base.gameObject, "slot");
		if (itemStorage != null)
		{
			this.m_buttonSpace = (float)itemStorage.spacing_x;
			UIPanel panel = base.gameObject.GetComponent<UIPanel>();
			if (panel != null)
			{
				this.m_displayIconCount = (int)(panel.clipRange.z / this.m_buttonSpace);
			}
		}
		GameObject scrollButtonRoot = null;
		GameObject parentObject = base.gameObject.transform.parent.gameObject;
		scrollButtonRoot = GameObjectUtil.FindChildGameObject(parentObject, "player_set_scroll_other");
		GameObject leftButtonObject = GameObjectUtil.FindChildGameObject(scrollButtonRoot, "Btn_icon_arrow_lt");
		if (leftButtonObject != null)
		{
			UIButtonMessage buttonMessage = leftButtonObject.GetComponent<UIButtonMessage>();
			if (buttonMessage == null)
			{
				buttonMessage = leftButtonObject.AddComponent<UIButtonMessage>();
			}
			buttonMessage.target = base.gameObject;
			buttonMessage.functionName = "LeftButtonClickedCallback";
		}
		GameObject rightButtonObject = GameObjectUtil.FindChildGameObject(scrollButtonRoot, "Btn_icon_arrow_rt");
		if (rightButtonObject != null)
		{
			UIButtonMessage buttonMessage2 = rightButtonObject.GetComponent<UIButtonMessage>();
			if (buttonMessage2 == null)
			{
				buttonMessage2 = rightButtonObject.AddComponent<UIButtonMessage>();
			}
			buttonMessage2.target = base.gameObject;
			buttonMessage2.functionName = "RightButtonClickedCallback";
		}
		this.m_isEndSetup = true;
		yield break;
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x000DB97C File Offset: 0x000D9B7C
	public void SetActiveCharacter(CharaType charaType, bool isSetup)
	{
		base.StartCoroutine(this.OnSetActiveCharacter(charaType, isSetup));
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x000DB990 File Offset: 0x000D9B90
	public IEnumerator OnSetActiveCharacter(CharaType charaType, bool isSetup)
	{
		int pageIndex = MenuPlayerSetUtil.GetPageIndexFromCharaType(charaType);
		int maxPage = MenuPlayerSetUtil.GetOpenedCharaCount();
		yield return null;
		if (isSetup)
		{
			UIDraggablePanel draggablePanel = base.gameObject.GetComponent<UIDraggablePanel>();
			if (draggablePanel != null)
			{
				int moveDistance = -(int)this.m_buttonSpace * (this.m_displayIconCount / 2);
				draggablePanel.MoveRelative(new Vector3((float)(-(float)moveDistance), 0f, 0f));
			}
		}
		else
		{
			UIDraggablePanel draggablePanel2 = base.gameObject.GetComponent<UIDraggablePanel>();
			if (draggablePanel2 != null)
			{
				int moveDistance2 = -(int)this.m_buttonSpace * (this.m_displayIconCount / 2);
				moveDistance2 += (int)this.m_buttonSpace * pageIndex;
				draggablePanel2.ResetPosition();
				draggablePanel2.MoveRelative(new Vector3((float)(-(float)moveDistance2), 0f, 0f));
			}
		}
		GameObject scrollButtonRoot = null;
		GameObject parentObject = base.gameObject.transform.parent.gameObject;
		scrollButtonRoot = GameObjectUtil.FindChildGameObject(parentObject, "player_set_scroll_other");
		if (pageIndex == 0)
		{
			BoxCollider boxCollider = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(scrollButtonRoot, "Btn_icon_arrow_lt");
			if (boxCollider != null)
			{
				boxCollider.isTrigger = true;
			}
		}
		else if (pageIndex >= maxPage - 1)
		{
			BoxCollider boxCollider2 = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(scrollButtonRoot, "Btn_icon_arrow_rt");
			if (boxCollider2 != null)
			{
				boxCollider2.isTrigger = true;
			}
		}
		else
		{
			BoxCollider boxColliderL = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(scrollButtonRoot, "Btn_icon_arrow_lt");
			if (boxColliderL != null)
			{
				boxColliderL.isTrigger = false;
			}
			BoxCollider boxColliderR = GameObjectUtil.FindChildGameObjectComponent<BoxCollider>(scrollButtonRoot, "Btn_icon_arrow_rt");
			if (boxColliderR != null)
			{
				boxColliderR.isTrigger = false;
			}
		}
		if (this.m_prevActivePageIndex >= 0)
		{
			this.m_buttons[this.m_prevActivePageIndex].SetButtonActive(false);
		}
		this.m_buttons[pageIndex].SetButtonActive(true);
		this.m_prevActivePageIndex = pageIndex;
		yield break;
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x000DB9C8 File Offset: 0x000D9BC8
	public void UnclockCharacter(CharaType charaType)
	{
		int pageIndexFromCharaType = MenuPlayerSetUtil.GetPageIndexFromCharaType(charaType);
		MenuPlayerSetShortCutButton menuPlayerSetShortCutButton = this.m_buttons[pageIndexFromCharaType];
		if (menuPlayerSetShortCutButton == null)
		{
			return;
		}
		menuPlayerSetShortCutButton.SetIconLock(false);
		menuPlayerSetShortCutButton.SetButtonActive(true);
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x000DBA04 File Offset: 0x000D9C04
	private void Start()
	{
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x000DBA08 File Offset: 0x000D9C08
	private void Update()
	{
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x000DBA0C File Offset: 0x000D9C0C
	private void ButtonClickedCallback(CharaType charaType)
	{
		if (this.m_callback != null)
		{
			this.m_callback(charaType);
		}
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x000DBA28 File Offset: 0x000D9C28
	private void LeftButtonClickedCallback()
	{
		int num = this.m_prevActivePageIndex - 1;
		if (num <= 0)
		{
			num = 0;
		}
		CharaType charaTypeFromPageIndex = MenuPlayerSetUtil.GetCharaTypeFromPageIndex(num);
		this.ButtonClickedCallback(charaTypeFromPageIndex);
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x000DBA58 File Offset: 0x000D9C58
	private void RightButtonClickedCallback()
	{
		int num = this.m_prevActivePageIndex + 1;
		int openedCharaCount = MenuPlayerSetUtil.GetOpenedCharaCount();
		if (num >= openedCharaCount - 1)
		{
			num = openedCharaCount - 1;
		}
		CharaType charaTypeFromPageIndex = MenuPlayerSetUtil.GetCharaTypeFromPageIndex(num);
		this.ButtonClickedCallback(charaTypeFromPageIndex);
	}

	// Token: 0x040020F4 RID: 8436
	private List<MenuPlayerSetShortCutButton> m_buttons = new List<MenuPlayerSetShortCutButton>();

	// Token: 0x040020F5 RID: 8437
	private MenuPlayerSetShortCutMenu.ShortCutCallback m_callback;

	// Token: 0x040020F6 RID: 8438
	private bool m_isEndSetup;

	// Token: 0x040020F7 RID: 8439
	private int m_prevActivePageIndex = -1;

	// Token: 0x040020F8 RID: 8440
	private float m_buttonSpace = 1f;

	// Token: 0x040020F9 RID: 8441
	private int m_displayIconCount = 1;

	// Token: 0x02000A9A RID: 2714
	// (Invoke) Token: 0x060048A2 RID: 18594
	public delegate void ShortCutCallback(CharaType charaType);
}
