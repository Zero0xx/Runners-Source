using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class ItemTable
{
	// Token: 0x06001636 RID: 5686 RVA: 0x0007EE40 File Offset: 0x0007D040
	public ItemType GetItemType(int tbl_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = tbl_index * 12;
			int num2 = num + 8;
			bool flag = !(StageModeManager.Instance != null) || !StageModeManager.Instance.IsQuickMode() || !ObjTimerUtil.IsEnableCreateTimer();
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				int num4 = num + i;
				num3 += this.m_tblInfo[num4];
			}
			for (int j = 0; j < 4; j++)
			{
				if (!flag || (j != 1 && j != 2 && j != 3))
				{
					int num5 = num2 + j;
					num3 += this.m_tblInfo[num5];
				}
			}
			int randomRange = ObjUtil.GetRandomRange(num3);
			int num6 = 0;
			for (int k = 0; k < 8; k++)
			{
				int num7 = num + k;
				int num8 = this.m_tblInfo[num7];
				if (num6 <= randomRange && randomRange < num6 + num8)
				{
					return (ItemType)k;
				}
				num6 += num8;
			}
			for (int l = 0; l < 4; l++)
			{
				if (!flag || (l != 1 && l != 2 && l != 3))
				{
					int num9 = num2 + l;
					int num10 = this.m_tblInfo[num9];
					if (num6 <= randomRange && randomRange < num6 + num10)
					{
						switch (l)
						{
						case 0:
							return ItemType.REDSTAR_RING;
						case 1:
							return ItemType.TIMER_BRONZE;
						case 2:
							return ItemType.TIMER_SILVER;
						case 3:
							return ItemType.TIMER_GOLD;
						}
					}
					num6 += num10;
				}
			}
		}
		return ItemType.TRAMPOLINE;
	}

	// Token: 0x06001637 RID: 5687 RVA: 0x0007EFFC File Offset: 0x0007D1FC
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[512];
		}
		if (this.m_tblInfo.Length < 128)
		{
			return;
		}
		if (terrainData != null)
		{
			TextAsset itemTableData = terrainData.ItemTableData;
			if (itemTableData)
			{
				string xml = AESCrypt.Decrypt(itemTableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				ItemTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
				if (this.m_tblCount == 0)
				{
				}
			}
		}
	}

	// Token: 0x06001638 RID: 5688 RVA: 0x0007F08C File Offset: 0x0007D28C
	public static void CreateTable(XmlDocument doc, int[] data, out int tbl_count)
	{
		tbl_count = 0;
		if (doc == null)
		{
			return;
		}
		if (doc.DocumentElement == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("ItemTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			int num2 = num * 4;
			XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Item");
			foreach (object obj2 in xmlNodeList2)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				for (int i = 0; i < 8; i++)
				{
					string itemTypeName = ItemTypeName.GetItemTypeName((ItemType)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemTypeName];
					int num3 = 0;
					if (xmlAttribute != null)
					{
						num3 = int.Parse(xmlNode2.Attributes[itemTypeName].Value, NumberStyles.AllowLeadingSign);
					}
					int num4 = num * 8 + i + num2;
					data[num4] = num3;
				}
				for (int j = 0; j < 4; j++)
				{
					string otherItemTypeName = ItemTypeName.GetOtherItemTypeName((OtherItemType)j);
					XmlAttribute xmlAttribute2 = xmlNode2.Attributes[otherItemTypeName];
					int num5 = 0;
					if (xmlAttribute2 != null)
					{
						num5 = int.Parse(xmlNode2.Attributes[otherItemTypeName].Value, NumberStyles.AllowLeadingSign);
					}
					int num6 = num * 8 + 8 + j + num2;
					data[num6] = num5;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x040013B8 RID: 5048
	public const int ITEM_COUNT_MAX = 32;

	// Token: 0x040013B9 RID: 5049
	public const int TBL_COUNT_MAX = 16;

	// Token: 0x040013BA RID: 5050
	private int[] m_tblInfo;

	// Token: 0x040013BB RID: 5051
	private int m_tblCount;
}
