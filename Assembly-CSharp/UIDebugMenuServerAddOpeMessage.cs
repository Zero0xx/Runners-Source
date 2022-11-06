using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class UIDebugMenuServerAddOpeMessage : UIDebugMenuTask
{
	// Token: 0x06000D0C RID: 3340 RVA: 0x0004BD84 File Offset: 0x00049F84
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(100f, 20f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(250f, 20f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 7; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i], this.DefaultParamTextList[i]);
		}
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0004BE60 File Offset: 0x0004A060
	private string GetGameId()
	{
		return SystemSaveManager.GetGameID();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0004BE74 File Offset: 0x0004A074
	protected override void OnTransitionTo()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(false);
		}
		for (int i = 0; i < 7; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0004BEF4 File Offset: 0x0004A0F4
	protected override void OnTransitionFrom()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(true);
		}
		for (int i = 0; i < 7; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0004BF74 File Offset: 0x0004A174
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			this.m_addOpeMsg = new NetDebugAddOpeMessage(new NetDebugAddOpeMessage.OpeMsgInfo
			{
				userID = this.GetGameId(),
				messageKind = int.Parse(this.m_TextFields[0].text),
				infoId = int.Parse(this.m_TextFields[1].text),
				itemId = int.Parse(this.m_TextFields[2].text),
				numItem = int.Parse(this.m_TextFields[3].text),
				additionalInfo1 = int.Parse(this.m_TextFields[4].text),
				additionalInfo2 = int.Parse(this.m_TextFields[5].text),
				msgTitle = string.Empty,
				msgContent = this.m_TextFields[6].text,
				msgImageId = "0"
			});
			this.m_addOpeMsg.Request();
		}
	}

	// Token: 0x04000A9A RID: 2714
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A9B RID: 2715
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A9C RID: 2716
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[7];

	// Token: 0x04000A9D RID: 2717
	private string[] DefaultTextList = new string[]
	{
		"メッセージ種別( 0:全体 1:個別)",
		"お知らせID(全体へのプレゼント時の時に、入力可。個別時は0とする)",
		"運営配布のアイテムID",
		"運営配布のアイテム数",
		"アイテム追加情報_1(チャオであればLV)",
		"アイテム追加情報_2(チャオであればLVMAX時の取得スペシャルエッグ数)",
		"個別メッセージの内容"
	};

	// Token: 0x04000A9E RID: 2718
	private string[] DefaultParamTextList = new string[]
	{
		"1",
		"0",
		"120000",
		"1",
		"0",
		"0",
		string.Empty
	};

	// Token: 0x04000A9F RID: 2719
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(100f, 100f, 200f, 50f),
		new Rect(310f, 100f, 500f, 50f),
		new Rect(100f, 175f, 250f, 50f),
		new Rect(360f, 175f, 250f, 50f),
		new Rect(100f, 250f, 500f, 50f),
		new Rect(100f, 325f, 500f, 50f),
		new Rect(100f, 475f, 500f, 50f)
	};

	// Token: 0x04000AA0 RID: 2720
	private NetDebugAddOpeMessage m_addOpeMsg;

	// Token: 0x020001DA RID: 474
	private enum TextType
	{
		// Token: 0x04000AA2 RID: 2722
		MESSAGE_KIND,
		// Token: 0x04000AA3 RID: 2723
		INFO_ID,
		// Token: 0x04000AA4 RID: 2724
		ITEM_ID,
		// Token: 0x04000AA5 RID: 2725
		NUM_ITEM,
		// Token: 0x04000AA6 RID: 2726
		ADDITIONAL_INFO1,
		// Token: 0x04000AA7 RID: 2727
		ADDITIONAL_INFO2,
		// Token: 0x04000AA8 RID: 2728
		MSG_CONTENT,
		// Token: 0x04000AA9 RID: 2729
		NUM
	}
}
