using System;
using Text;

// Token: 0x02000550 RID: 1360
public class IncentiveWindow
{
	// Token: 0x06002A18 RID: 10776 RVA: 0x00104ED8 File Offset: 0x001030D8
	public IncentiveWindow(int serverItemId, int itemNum, string anchor)
	{
		this.m_serverItemId = serverItemId;
		this.m_itemNum = itemNum;
	}

	// Token: 0x06002A19 RID: 10777 RVA: 0x00104EF0 File Offset: 0x001030F0
	public void PlayStart()
	{
		string imageCount = string.Empty;
		TextObject text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "gw_item_text");
		if (text != null)
		{
			text.ReplaceTag("{COUNT}", HudUtility.GetFormatNumString<int>(this.m_itemNum));
			imageCount = text.text;
		}
		string caption = string.Empty;
		text = TextManager.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "FaceBook", "ui_Lbl_reward_caption");
		if (text != null)
		{
			caption = text.text;
		}
		ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
		if (itemGetWindow != null)
		{
			itemGetWindow.gameObject.SetActive(true);
			itemGetWindow.Create(new ItemGetWindow.CInfo
			{
				caption = caption,
				serverItemId = this.m_serverItemId,
				imageCount = imageCount
			});
		}
		this.m_isStart = true;
	}

	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06002A1A RID: 10778 RVA: 0x00104FB0 File Offset: 0x001031B0
	// (set) Token: 0x06002A1B RID: 10779 RVA: 0x00104FB8 File Offset: 0x001031B8
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
		private set
		{
		}
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x00104FBC File Offset: 0x001031BC
	public void Update()
	{
		if (this.m_isStart)
		{
			ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
			if (itemGetWindow != null && itemGetWindow.IsEnd)
			{
				itemGetWindow.Reset();
				itemGetWindow.gameObject.SetActive(false);
				this.m_isEnd = true;
			}
		}
	}

	// Token: 0x04002567 RID: 9575
	private int m_serverItemId;

	// Token: 0x04002568 RID: 9576
	private int m_itemNum;

	// Token: 0x04002569 RID: 9577
	private bool m_isStart;

	// Token: 0x0400256A RID: 9578
	private bool m_isEnd;
}
