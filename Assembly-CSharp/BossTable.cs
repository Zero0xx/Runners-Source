using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002EA RID: 746
public class BossTable
{
	// Token: 0x0600157D RID: 5501 RVA: 0x00076C6C File Offset: 0x00074E6C
	private void Start()
	{
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x00076C70 File Offset: 0x00074E70
	public static string GetItemName(uint index)
	{
		if (index < 5U && (ulong)index < (ulong)((long)BossTable.ITEM_NAMES.Length))
		{
			return BossTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x00076CA4 File Offset: 0x00074EA4
	public int GetItemData(int tbl_index, BossTableItem tbl_item)
	{
		return this.GetData(tbl_index, (int)tbl_item);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00076CB0 File Offset: 0x00074EB0
	public int GetSuperRing(int tbl_index)
	{
		return this.GetData(tbl_index, 0);
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x00076CBC File Offset: 0x00074EBC
	public int GetRedStarRing(int tbl_index)
	{
		return this.GetData(tbl_index, 1);
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x00076CC8 File Offset: 0x00074EC8
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[120];
		}
		if (terrainData != null)
		{
			TextAsset bossTableData = terrainData.BossTableData;
			if (bossTableData)
			{
				string xml = AESCrypt.Decrypt(bossTableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				BossTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
				if (this.m_tblCount == 0)
				{
				}
			}
		}
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x00076D44 File Offset: 0x00074F44
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("BossTable");
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
				for (int i = 0; i < 5; i++)
				{
					string itemName = BossTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					int num2 = 0;
					if (xmlAttribute != null)
					{
						num2 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
					}
					int num3 = num * 5 + i;
					data[num3] = num2;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x00076EBC File Offset: 0x000750BC
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00076ECC File Offset: 0x000750CC
	private int GetData(int tbl_index, int item_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = tbl_index * 5 + item_index;
			if (num < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num];
			}
		}
		return 0;
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x00076F10 File Offset: 0x00075110
	private int GetData(int index)
	{
		if (this.m_tblInfo != null && index < this.m_tblInfo.Length)
		{
			return this.m_tblInfo[index];
		}
		return 0;
	}

	// Token: 0x04001302 RID: 4866
	public const int ITEM_COUNT_MAX = 5;

	// Token: 0x04001303 RID: 4867
	public const int TBL_COUNT_MAX = 24;

	// Token: 0x04001304 RID: 4868
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"SuperRing",
		"RedStarRing",
		"BronzeWatch",
		"SilverWatch",
		"GoldWatch"
	};

	// Token: 0x04001305 RID: 4869
	private int[] m_tblInfo;

	// Token: 0x04001306 RID: 4870
	private int m_tblCount;
}
