using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class ButtonEventBackButton : MonoBehaviour
{
	// Token: 0x060020BC RID: 8380 RVA: 0x000C4304 File Offset: 0x000C2504
	public void Initialize(ButtonEventBackButton.ButtonClickedCallback callback)
	{
		this.m_callback = callback;
		this.m_menu_anim_obj = HudMenuUtility.GetMenuAnimUIObject();
		this.m_btn_obj_list = new List<ButtonEventBackButton.BtnData>();
		if (this.m_menu_anim_obj != null)
		{
			for (uint num = 0U; num < 49U; num += 1U)
			{
				this.SetupBackButton((ButtonInfoTable.ButtonType)num);
			}
		}
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x000C435C File Offset: 0x000C255C
	public void SetupBackButton(ButtonInfoTable.ButtonType buttonType)
	{
		for (int i = 0; i < this.m_btn_obj_list.Count; i++)
		{
			if (this.m_btn_obj_list[i].btn_type == buttonType)
			{
				return;
			}
		}
		if (string.IsNullOrEmpty(this.m_info_table.m_button_info[(int)buttonType].clickButtonPath))
		{
			return;
		}
		Transform transform = this.m_menu_anim_obj.transform.Find(this.m_info_table.m_button_info[(int)buttonType].clickButtonPath);
		if (transform == null)
		{
			return;
		}
		GameObject gameObject = transform.gameObject;
		if (gameObject == null)
		{
			return;
		}
		ButtonEventBackButton.BtnData item = new ButtonEventBackButton.BtnData(gameObject, buttonType);
		this.m_btn_obj_list.Add(item);
		UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
		if (component == null)
		{
			gameObject.AddComponent<UIButtonMessage>();
			component = gameObject.GetComponent<UIButtonMessage>();
		}
		if (component != null)
		{
			component.enabled = true;
			component.trigger = UIButtonMessage.Trigger.OnClick;
			component.target = base.gameObject;
			component.functionName = "OnButtonClicked";
		}
		UIPlayAnimation[] components = gameObject.GetComponents<UIPlayAnimation>();
		if (components != null)
		{
			foreach (UIPlayAnimation uiplayAnimation in components)
			{
				uiplayAnimation.target = null;
			}
		}
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x000C44B0 File Offset: 0x000C26B0
	private void OnButtonClicked(GameObject obj)
	{
		if (obj == null)
		{
			return;
		}
		if (this.m_callback == null)
		{
			return;
		}
		if (this.m_btn_obj_list == null)
		{
			return;
		}
		int count = this.m_btn_obj_list.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(obj != this.m_btn_obj_list[i].obj))
			{
				ButtonInfoTable.ButtonType btn_type = this.m_btn_obj_list[i].btn_type;
				this.m_callback(btn_type);
				this.m_info_table.PlaySE(btn_type);
			}
		}
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x000C4554 File Offset: 0x000C2754
	private void Start()
	{
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x000C4558 File Offset: 0x000C2758
	private void Update()
	{
	}

	// Token: 0x04001D3F RID: 7487
	private ButtonInfoTable m_info_table = new ButtonInfoTable();

	// Token: 0x04001D40 RID: 7488
	private GameObject m_menu_anim_obj;

	// Token: 0x04001D41 RID: 7489
	private List<ButtonEventBackButton.BtnData> m_btn_obj_list;

	// Token: 0x04001D42 RID: 7490
	private ButtonEventBackButton.ButtonClickedCallback m_callback;

	// Token: 0x02000435 RID: 1077
	public struct BtnData
	{
		// Token: 0x060020C1 RID: 8385 RVA: 0x000C455C File Offset: 0x000C275C
		public BtnData(GameObject obj, ButtonInfoTable.ButtonType btn_type)
		{
			this.obj = obj;
			this.btn_type = btn_type;
		}

		// Token: 0x04001D43 RID: 7491
		public GameObject obj;

		// Token: 0x04001D44 RID: 7492
		public ButtonInfoTable.ButtonType btn_type;
	}

	// Token: 0x02000A8F RID: 2703
	// (Invoke) Token: 0x06004876 RID: 18550
	public delegate void ButtonClickedCallback(ButtonInfoTable.ButtonType buttonType);
}
