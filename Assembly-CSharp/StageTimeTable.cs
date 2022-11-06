using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class StageTimeTable : MonoBehaviour
{
	// Token: 0x060017AF RID: 6063 RVA: 0x00087B70 File Offset: 0x00085D70
	private void Start()
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[7];
		}
		if (this.m_stageTimeTable != null && this.m_stageTimeTable)
		{
			string xml = AESCrypt.Decrypt(this.m_stageTimeTable.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			StageTimeTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x00087BF0 File Offset: 0x00085DF0
	public static string GetItemName(uint index)
	{
		if (index < 7U && (ulong)index < (ulong)((long)StageTimeTable.ITEM_NAMES.Length))
		{
			return StageTimeTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x00087C24 File Offset: 0x00085E24
	public int GetTableItemData(StageTimeTableItem item)
	{
		return this.GetData((int)item);
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x00087C30 File Offset: 0x00085E30
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("StageTimeTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Item");
			foreach (object obj2 in xmlNodeList2)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				for (int i = 0; i < 7; i++)
				{
					string itemName = StageTimeTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					int num2 = 0;
					if (xmlAttribute != null)
					{
						num2 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
					}
					int num3 = num * 7 + i;
					data[num3] = num2;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x00087DA8 File Offset: 0x00085FA8
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x00087DB8 File Offset: 0x00085FB8
	private int GetData(int tbl_index, int item_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = tbl_index * 7 + item_index;
			if (num < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num];
			}
		}
		return 0;
	}

	// Token: 0x060017B5 RID: 6069 RVA: 0x00087DFC File Offset: 0x00085FFC
	private int GetData(int index)
	{
		if (this.m_tblInfo != null && index < this.m_tblInfo.Length)
		{
			return this.m_tblInfo[index];
		}
		return 0;
	}

	// Token: 0x04001537 RID: 5431
	public const int ITEM_COUNT_MAX = 7;

	// Token: 0x04001538 RID: 5432
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x04001539 RID: 5433
	[SerializeField]
	private TextAsset m_stageTimeTable;

	// Token: 0x0400153A RID: 5434
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"StartTime",
		"OverlapBonus",
		"ItemExtendedLimit",
		"BronzeWatch",
		"SilverWatch",
		"GoldWatch",
		"Continue"
	};

	// Token: 0x0400153B RID: 5435
	private int[] m_tblInfo;

	// Token: 0x0400153C RID: 5436
	private int m_tblCount;
}
