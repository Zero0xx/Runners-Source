using System;
using Message;
using UnityEngine;

// Token: 0x0200046A RID: 1130
public class WorkingWindow : MonoBehaviour
{
	// Token: 0x060021D5 RID: 8661 RVA: 0x000CBB08 File Offset: 0x000C9D08
	private void Start()
	{
	}

	// Token: 0x060021D6 RID: 8662 RVA: 0x000CBB0C File Offset: 0x000C9D0C
	public void CreateWindow(string caption)
	{
		this.m_window_obj = GeneralWindow.Create(new GeneralWindow.CInfo
		{
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = caption,
			message = "         CLOSED \n DUE TO CONSTRUCTION",
			anchor_path = "Camera/menu_Anim/MainMenuUI4/Anchor_5_MC"
		});
		this.m_update_flag = true;
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x000CBB5C File Offset: 0x000C9D5C
	public void Update()
	{
		if (this.m_update_flag && this.m_window_obj != null && GeneralWindow.IsButtonPressed)
		{
			GeneralWindow.Close();
			this.m_window_obj = null;
			this.m_update_flag = false;
			this.SendMessage();
		}
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x000CBBAC File Offset: 0x000C9DAC
	public void SendMessage()
	{
		MsgMenuSequence msgMenuSequence = new MsgMenuSequence(MsgMenuSequence.SequeneceType.MAIN);
		if (msgMenuSequence != null)
		{
			GameObjectUtil.SendMessageFindGameObject("MainMenu", "OnMsgReceive", msgMenuSequence, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04001E95 RID: 7829
	private GameObject m_window_obj;

	// Token: 0x04001E96 RID: 7830
	private bool m_update_flag;
}
