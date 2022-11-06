using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class RareEnemyTable
{
	// Token: 0x06001645 RID: 5701 RVA: 0x0007F668 File Offset: 0x0007D868
	public bool IsRareEnemy(uint tbl_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount) && (ulong)tbl_index < (ulong)((long)this.m_tblInfo.Length))
		{
			int randomRange = ObjUtil.GetRandomRange100();
			int num = this.m_tblInfo[(int)((UIntPtr)tbl_index)];
			num = ObjUtil.GetChaoAbliltyValue(ChaoAbility.RARE_ENEMY_UP, num);
			if (num > randomRange)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001646 RID: 5702 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[8];
		}
		if (terrainData != null)
		{
			TextAsset rareEnemyTableData = terrainData.RareEnemyTableData;
			if (rareEnemyTableData)
			{
				string xml = AESCrypt.Decrypt(rareEnemyTableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				RareEnemyTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
				if (this.m_tblCount == 0)
				{
				}
			}
		}
	}

	// Token: 0x06001647 RID: 5703 RVA: 0x0007F73C File Offset: 0x0007D93C
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("RareEnemyTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Table");
			foreach (object obj2 in xmlNodeList2)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				int num2 = int.Parse(xmlNode2.Attributes["Param"].Value, NumberStyles.AllowLeadingSign);
				data[num] = num2;
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x040013D7 RID: 5079
	public const int TBL_COUNT_MAX = 8;

	// Token: 0x040013D8 RID: 5080
	private int[] m_tblInfo;

	// Token: 0x040013D9 RID: 5081
	private int m_tblCount;
}
