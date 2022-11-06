using System;
using Text;

// Token: 0x0200081A RID: 2074
public class ServerPrizeData
{
	// Token: 0x060037AE RID: 14254 RVA: 0x00125C4C File Offset: 0x00123E4C
	public ServerPrizeData()
	{
		this.itemId = 0;
		this.weight = 0;
		this.num = 1;
		this.spinId = 0;
		this.priority = -100;
	}

	// Token: 0x1700083D RID: 2109
	// (get) Token: 0x060037AF RID: 14255 RVA: 0x00125C84 File Offset: 0x00123E84
	// (set) Token: 0x060037B0 RID: 14256 RVA: 0x00125C8C File Offset: 0x00123E8C
	public int itemId
	{
		get
		{
			return this.m_itemId;
		}
		set
		{
			this.m_itemId = value;
			if (this.m_itemId >= 300000 && this.m_itemId < 400000)
			{
				this.priority = 100;
			}
			else if (this.m_itemId >= 400000 && this.m_itemId < 500000)
			{
				if (this.m_itemId >= 402000)
				{
					this.priority = 2;
				}
				else if (this.m_itemId >= 401000)
				{
					this.priority = 1;
				}
				else
				{
					this.priority = 0;
				}
			}
			else if (this.m_itemId == 200002)
			{
				this.priority = -1;
			}
			else
			{
				this.priority = -100;
			}
		}
	}

	// Token: 0x060037B1 RID: 14257 RVA: 0x00125D58 File Offset: 0x00123F58
	public string GetItemName()
	{
		ServerItem serverItem = new ServerItem((ServerItem.Id)this.itemId);
		string result;
		if (serverItem.idType == ServerItem.IdType.CHARA)
		{
			result = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, RouletteUtility.GetChaoGroupName(this.itemId), RouletteUtility.GetChaoCellName(this.itemId)).text;
		}
		else if (serverItem.idType == ServerItem.IdType.CHAO)
		{
			result = TextManager.GetText(TextManager.TextType.TEXTTYPE_CHAO_TEXT, RouletteUtility.GetChaoGroupName(this.itemId), RouletteUtility.GetChaoCellName(this.itemId)).text;
		}
		else
		{
			result = serverItem.serverItemName;
		}
		return result;
	}

	// Token: 0x060037B2 RID: 14258 RVA: 0x00125DE8 File Offset: 0x00123FE8
	public void CopyTo(ServerPrizeData to)
	{
		to.itemId = this.itemId;
		to.num = this.num;
		to.weight = this.weight;
		to.spinId = this.spinId;
	}

	// Token: 0x04002F10 RID: 12048
	private int m_itemId;

	// Token: 0x04002F11 RID: 12049
	public int weight;

	// Token: 0x04002F12 RID: 12050
	public int num;

	// Token: 0x04002F13 RID: 12051
	public int spinId;

	// Token: 0x04002F14 RID: 12052
	public int priority;
}
