using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class HudHeaderPresentBox : MonoBehaviour
{
	// Token: 0x0600214A RID: 8522 RVA: 0x000C8584 File Offset: 0x000C6784
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x000C8598 File Offset: 0x000C6798
	private void Initialize()
	{
		if (this.m_initEnd)
		{
			return;
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject != null)
		{
			GameObject gameObject = mainMenuUIObject.transform.FindChild("Anchor_7_BL/Btn_2_presentbox").gameObject;
			if (gameObject != null)
			{
				this.m_present_box_badge = gameObject.transform.FindChild("badge").gameObject;
				if (this.m_present_box_badge != null)
				{
					GameObject gameObject2 = this.m_present_box_badge.transform.FindChild("Lbl_present_volume").gameObject;
					if (gameObject2 != null)
					{
						this.m_volume_label = gameObject2.GetComponent<UILabel>();
					}
				}
				this.m_collider = gameObject.GetComponent<BoxCollider>();
				this.m_image_button = gameObject.GetComponent<UIImageButton>();
			}
		}
		this.m_initEnd = true;
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x000C8664 File Offset: 0x000C6864
	public void OnUpdateSaveDataDisplay()
	{
		this.Initialize();
		int num = 0;
		if (ServerInterface.MessageList != null)
		{
			List<ServerMessageEntry> messageList = ServerInterface.MessageList;
			foreach (ServerMessageEntry serverMessageEntry in messageList)
			{
				if (PresentBoxUtility.IsWithinTimeLimit(serverMessageEntry.m_expireTiem))
				{
					num++;
				}
			}
		}
		if (ServerInterface.OperatorMessageList != null)
		{
			List<ServerOperatorMessageEntry> operatorMessageList = ServerInterface.OperatorMessageList;
			foreach (ServerOperatorMessageEntry serverOperatorMessageEntry in operatorMessageList)
			{
				if (PresentBoxUtility.IsWithinTimeLimit(serverOperatorMessageEntry.m_expireTiem))
				{
					num++;
				}
			}
		}
		if (this.m_volume_label != null)
		{
			this.m_volume_label.text = num.ToString();
		}
		if (num == 0)
		{
			if (this.m_present_box_badge != null)
			{
				this.m_present_box_badge.SetActive(false);
			}
			if (this.m_collider != null)
			{
				this.m_collider.isTrigger = true;
			}
			if (this.m_image_button != null)
			{
				this.m_image_button.isEnabled = false;
			}
		}
		else
		{
			if (this.m_present_box_badge != null)
			{
				this.m_present_box_badge.SetActive(true);
			}
			if (this.m_collider != null)
			{
				this.m_collider.isTrigger = false;
			}
			if (this.m_image_button != null)
			{
				this.m_image_button.isEnabled = true;
			}
		}
	}

	// Token: 0x04001E14 RID: 7700
	private const string m_present_box_path = "Anchor_7_BL/Btn_2_presentbox";

	// Token: 0x04001E15 RID: 7701
	private GameObject m_present_box_badge;

	// Token: 0x04001E16 RID: 7702
	private UILabel m_volume_label;

	// Token: 0x04001E17 RID: 7703
	private BoxCollider m_collider;

	// Token: 0x04001E18 RID: 7704
	private UIImageButton m_image_button;

	// Token: 0x04001E19 RID: 7705
	private bool m_initEnd;

	// Token: 0x02000451 RID: 1105
	private enum DataType
	{
		// Token: 0x04001E1B RID: 7707
		PRESEN_BOX,
		// Token: 0x04001E1C RID: 7708
		VOLUME_LABEL
	}
}
