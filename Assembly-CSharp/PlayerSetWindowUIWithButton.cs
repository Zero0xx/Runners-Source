using System;
using UnityEngine;

// Token: 0x020004E1 RID: 1249
public class PlayerSetWindowUIWithButton : PlayerSetWindowUI
{
	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x06002532 RID: 9522 RVA: 0x000E05B8 File Offset: 0x000DE7B8
	public bool isClickCancel
	{
		get
		{
			return this.m_isClickCancel;
		}
	}

	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x06002533 RID: 9523 RVA: 0x000E05C0 File Offset: 0x000DE7C0
	public bool isClickMain
	{
		get
		{
			return this.m_isClickMain;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x06002534 RID: 9524 RVA: 0x000E05C8 File Offset: 0x000DE7C8
	public bool isClickSub
	{
		get
		{
			return this.m_isClickSub;
		}
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x000E05D0 File Offset: 0x000DE7D0
	public void SetupWithButton(CharaType charaType)
	{
		base.Setup(charaType, null, PlayerSetWindowUI.WINDOW_MODE.DEFAULT);
		this.m_btnMain = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_main");
		this.m_btnSub = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_sub");
		PlayerSetWindowUIWithButton.SetupButton(this.m_btnMain, base.gameObject, "OnClickMainButton");
		PlayerSetWindowUIWithButton.SetupButton(this.m_btnSub, base.gameObject, "OnClickSubButton");
		this.m_isClickCancel = false;
		this.m_isClickMain = false;
		this.m_isClickSub = false;
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x000E0654 File Offset: 0x000DE854
	private void OnClickBtn()
	{
		this.m_isClickCancel = true;
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x000E0660 File Offset: 0x000DE860
	private void OnClickMainButton()
	{
		this.m_isClickMain = true;
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x000E066C File Offset: 0x000DE86C
	private void OnClickSubButton()
	{
		this.m_isClickSub = true;
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000E0678 File Offset: 0x000DE878
	public static PlayerSetWindowUIWithButton Create(CharaType charaType)
	{
		GameObject cameraUIObject = HudMenuUtility.GetCameraUIObject();
		if (cameraUIObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(cameraUIObject, "PlayerSetWindowUI");
			PlayerSetWindowUIWithButton playerSetWindowUIWithButton = null;
			if (gameObject != null)
			{
				playerSetWindowUIWithButton = gameObject.GetComponent<PlayerSetWindowUIWithButton>();
				if (playerSetWindowUIWithButton == null)
				{
					playerSetWindowUIWithButton = gameObject.AddComponent<PlayerSetWindowUIWithButton>();
				}
				if (playerSetWindowUIWithButton != null)
				{
					playerSetWindowUIWithButton.SetupWithButton(charaType);
				}
			}
			return playerSetWindowUIWithButton;
		}
		return null;
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x000E06E4 File Offset: 0x000DE8E4
	private static void SetupButton(GameObject buttonObject, GameObject targetObject, string functionName)
	{
		if (buttonObject == null)
		{
			return;
		}
		buttonObject.SetActive(true);
		UIButtonMessage uibuttonMessage = buttonObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage == null)
		{
			uibuttonMessage = buttonObject.AddComponent<UIButtonMessage>();
			uibuttonMessage.target = targetObject;
			uibuttonMessage.functionName = functionName;
		}
	}

	// Token: 0x0400215E RID: 8542
	private GameObject m_btnMain;

	// Token: 0x0400215F RID: 8543
	private GameObject m_btnSub;

	// Token: 0x04002160 RID: 8544
	private bool m_isClickCancel;

	// Token: 0x04002161 RID: 8545
	private bool m_isClickMain;

	// Token: 0x04002162 RID: 8546
	private bool m_isClickSub;
}
