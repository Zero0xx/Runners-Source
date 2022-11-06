using System;
using System.Collections.Generic;

// Token: 0x02000438 RID: 1080
public class ButtonEventPageHistory
{
	// Token: 0x060020D9 RID: 8409 RVA: 0x000C4D18 File Offset: 0x000C2F18
	public void Push(ButtonInfoTable.PageType pageType)
	{
		if (pageType == ButtonInfoTable.PageType.NON)
		{
			return;
		}
		if (pageType >= ButtonInfoTable.PageType.NUM)
		{
			return;
		}
		if (pageType == ButtonInfoTable.PageType.MAIN)
		{
			this.Clear();
		}
		this.m_pageList.Add(pageType);
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x000C4D44 File Offset: 0x000C2F44
	public ButtonInfoTable.PageType Pop()
	{
		if (this.m_pageList.Count <= 0)
		{
			return ButtonInfoTable.PageType.MAIN;
		}
		int index = this.m_pageList.Count - 1;
		ButtonInfoTable.PageType result = this.m_pageList[index];
		this.m_pageList.RemoveAt(index);
		return result;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x000C4D8C File Offset: 0x000C2F8C
	public ButtonInfoTable.PageType Peek()
	{
		if (this.m_pageList.Count <= 0)
		{
			return ButtonInfoTable.PageType.MAIN;
		}
		int index = this.m_pageList.Count - 1;
		return this.m_pageList[index];
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x000C4DC8 File Offset: 0x000C2FC8
	public void Clear()
	{
		this.m_pageList.Clear();
		this.m_pageList.Add(ButtonInfoTable.PageType.MAIN);
	}

	// Token: 0x04001D55 RID: 7509
	private List<ButtonInfoTable.PageType> m_pageList = new List<ButtonInfoTable.PageType>();
}
