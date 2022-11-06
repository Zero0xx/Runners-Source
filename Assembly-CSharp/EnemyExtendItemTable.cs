using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class EnemyExtendItemTable
{
	// Token: 0x0600159E RID: 5534 RVA: 0x00077ACC File Offset: 0x00075CCC
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[3];
		}
		if (terrainData != null)
		{
			TextAsset enemyExtendItemTableData = terrainData.EnemyExtendItemTableData;
			if (enemyExtendItemTableData)
			{
				string xml = AESCrypt.Decrypt(enemyExtendItemTableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				EnemyExtendItemTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
				if (this.m_tblCount == 0)
				{
				}
			}
		}
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x00077B44 File Offset: 0x00075D44
	public static string GetItemName(uint index)
	{
		if (index < 3U && (ulong)index < (ulong)((long)EnemyExtendItemTable.ITEM_NAMES.Length))
		{
			return EnemyExtendItemTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x00077B78 File Offset: 0x00075D78
	public int GetTableItemData(EnemyExtendItemTableItem item)
	{
		return this.GetData((int)item);
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x00077B84 File Offset: 0x00075D84
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EnemyExtendItemTable");
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
				for (int i = 0; i < 3; i++)
				{
					string itemName = EnemyExtendItemTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					int num2 = 0;
					if (xmlAttribute != null)
					{
						num2 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
					}
					int num3 = num * 3 + i;
					data[num3] = num2;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x00077CFC File Offset: 0x00075EFC
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x00077D0C File Offset: 0x00075F0C
	private int GetData(int tbl_index, int item_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = tbl_index * 3 + item_index;
			if (num < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num];
			}
		}
		return 0;
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x00077D50 File Offset: 0x00075F50
	private int GetData(int index)
	{
		if (this.m_tblInfo != null && index < this.m_tblInfo.Length)
		{
			return this.m_tblInfo[index];
		}
		return 0;
	}

	// Token: 0x04001321 RID: 4897
	public const int ITEM_COUNT_MAX = 3;

	// Token: 0x04001322 RID: 4898
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x04001323 RID: 4899
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"BronzeTimer",
		"SilverTimer",
		"GoldTimer"
	};

	// Token: 0x04001324 RID: 4900
	private int[] m_tblInfo;

	// Token: 0x04001325 RID: 4901
	private int m_tblCount;
}
