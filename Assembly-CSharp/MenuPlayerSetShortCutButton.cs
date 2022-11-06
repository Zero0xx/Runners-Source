using System;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class MenuPlayerSetShortCutButton : MonoBehaviour
{
	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x060024A1 RID: 9377 RVA: 0x000DB60C File Offset: 0x000D980C
	// (set) Token: 0x060024A2 RID: 9378 RVA: 0x000DB614 File Offset: 0x000D9814
	public CharaType Chara
	{
		get
		{
			return this.m_charaType;
		}
		private set
		{
		}
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x000DB618 File Offset: 0x000D9818
	public void Setup(CharaType charaType, bool isLocked)
	{
		this.m_charaType = charaType;
		BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.isTrigger = false;
		}
		this.SetIconLock(isLocked);
		UIButtonMessage uibuttonMessage = base.gameObject.GetComponent<UIButtonMessage>();
		if (uibuttonMessage == null)
		{
			uibuttonMessage = base.gameObject.AddComponent<UIButtonMessage>();
		}
		uibuttonMessage.target = base.gameObject;
		uibuttonMessage.functionName = "ClickedCallback";
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x000DB690 File Offset: 0x000D9890
	public void SetCallback(MenuPlayerSetShortCutButton.ButtonClickedCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x000DB69C File Offset: 0x000D989C
	public void SetIconLock(bool isLock)
	{
		this.m_isLocked = isLock;
		string text = string.Empty;
		string text2 = string.Empty;
		if (this.m_isLocked)
		{
			text = "ui_tex_player_set_unlock";
			text2 = "ui_tex_player_set_act_unlock";
		}
		else
		{
			text = "ui_tex_player_set_";
			text += string.Format("{0:00}", (int)this.m_charaType);
			text += "_";
			text += CharaName.PrefixName[(int)this.m_charaType];
			text2 = "ui_tex_player_set_act_";
			text2 += string.Format("{0:00}", (int)this.m_charaType);
			text2 += "_";
			text2 += CharaName.PrefixName[(int)this.m_charaType];
		}
		UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_pager_bg_temp");
		if (uisprite != null)
		{
			uisprite.spriteName = text;
		}
		UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(base.gameObject, "img_pager_act_temp");
		if (uisprite2 != null)
		{
			uisprite2.spriteName = text2;
			this.m_spriteSdwObject = uisprite2.gameObject;
			this.m_spriteSdwObject.SetActive(false);
		}
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x000DB7BC File Offset: 0x000D99BC
	public void SetButtonActive(bool isActive)
	{
		if (this.m_spriteSdwObject != null)
		{
			this.m_spriteSdwObject.SetActive(isActive);
		}
	}

	// Token: 0x060024A7 RID: 9383 RVA: 0x000DB7DC File Offset: 0x000D99DC
	public bool IsVisible(UIPanel panel)
	{
		if (panel != null)
		{
			Transform transform = base.gameObject.transform;
			if (panel.IsVisible(transform.position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060024A8 RID: 9384 RVA: 0x000DB818 File Offset: 0x000D9A18
	private void Start()
	{
	}

	// Token: 0x060024A9 RID: 9385 RVA: 0x000DB81C File Offset: 0x000D9A1C
	private void Update()
	{
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x000DB820 File Offset: 0x000D9A20
	private void ClickedCallback()
	{
		global::Debug.Log("MenuPlayerSetShortCutButton.ButtonClickedCallback");
		if (this.m_callback != null)
		{
			this.m_callback(this.m_charaType);
		}
	}

	// Token: 0x040020F0 RID: 8432
	private CharaType m_charaType;

	// Token: 0x040020F1 RID: 8433
	private MenuPlayerSetShortCutButton.ButtonClickedCallback m_callback;

	// Token: 0x040020F2 RID: 8434
	private bool m_isLocked;

	// Token: 0x040020F3 RID: 8435
	private GameObject m_spriteSdwObject;

	// Token: 0x02000A99 RID: 2713
	// (Invoke) Token: 0x0600489E RID: 18590
	public delegate void ButtonClickedCallback(CharaType charaType);
}
