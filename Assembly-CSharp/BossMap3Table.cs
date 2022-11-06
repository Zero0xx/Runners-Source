using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class BossMap3Table
{
	// Token: 0x0600156F RID: 5487 RVA: 0x00076860 File Offset: 0x00074A60
	private void Start()
	{
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x00076864 File Offset: 0x00074A64
	public static string GetItemName(uint index)
	{
		uint itemIndex = (uint)BossMap3Table.GetItemIndex((int)index);
		if ((ulong)itemIndex < (ulong)((long)BossMap3Table.ITEM_NAMES.Length))
		{
			return BossMap3Table.ITEM_NAMES[(int)((UIntPtr)itemIndex)];
		}
		return string.Empty;
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x00076898 File Offset: 0x00074A98
	public static int GetItemIndex(int index)
	{
		int attackItemTableCount = BossMap3Table.GetAttackItemTableCount(index);
		if (attackItemTableCount > 0)
		{
			index -= attackItemTableCount * 5;
		}
		return index;
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000768BC File Offset: 0x00074ABC
	public static int GetAttackItemTableCount(int index)
	{
		int result = 0;
		if (index >= 5)
		{
			result = index / 5;
		}
		return result;
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x000768D8 File Offset: 0x00074AD8
	public Map3AttackData GetMap3AttackData(int tbl_index, int attack_index)
	{
		int num = tbl_index * 80 + 5 * attack_index;
		BossAttackType data = (BossAttackType)this.GetData(num);
		int data2 = this.GetData(++num);
		BossAttackPos data3 = (BossAttackPos)this.GetData(++num);
		int data4 = this.GetData(++num);
		BossAttackPos data5 = (BossAttackPos)this.GetData(num + 1);
		return new Map3AttackData(data, data2, data3, data4, data5);
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x00076934 File Offset: 0x00074B34
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[1920];
		}
		if (terrainData != null)
		{
			TextAsset bossMap3TableData = terrainData.BossMap3TableData;
			if (bossMap3TableData)
			{
				string xml = AESCrypt.Decrypt(bossMap3TableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				BossMap3Table.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
				if (this.m_tblCount == 0)
				{
				}
			}
		}
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000769B0 File Offset: 0x00074BB0
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("BossMap3Table");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			for (int i = 0; i < 16; i++)
			{
				XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Attack_" + i.ToString());
				foreach (object obj2 in xmlNodeList2)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					for (int j = 0; j < 5; j++)
					{
						string itemName = BossMap3Table.GetItemName((uint)j);
						XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
						int num2 = 0;
						if (xmlAttribute != null)
						{
							num2 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
						}
						int num3 = num * 80 + j + 5 * i;
						data[num3] = num2;
					}
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x00076B50 File Offset: 0x00074D50
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x00076B60 File Offset: 0x00074D60
	private int GetData(int tbl_index, int item_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = tbl_index * 80 + item_index;
			if (num < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num];
			}
		}
		return 0;
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x00076BA8 File Offset: 0x00074DA8
	private int GetData(int index)
	{
		if (this.m_tblInfo != null && index < this.m_tblInfo.Length)
		{
			return this.m_tblInfo[index];
		}
		return 0;
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x00076BD0 File Offset: 0x00074DD0
	public static int BossAttackTypeMinMax(int val)
	{
		int num = val;
		if (num < 0)
		{
			num = 0;
		}
		else if (num >= 11)
		{
			num = 10;
		}
		return num;
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x00076BFC File Offset: 0x00074DFC
	public static int GetBossAttackCount(BossAttackType type)
	{
		switch (type)
		{
		case BossAttackType.NONE:
			return 0;
		case BossAttackType.W:
			return 1;
		case BossAttackType.H:
			return 1;
		default:
			return 2;
		}
	}

	// Token: 0x040012F5 RID: 4853
	public const int ATTACK_COUNT_MAX = 16;

	// Token: 0x040012F6 RID: 4854
	public const int ITEM_COUNT_MAX = 80;

	// Token: 0x040012F7 RID: 4855
	public const int TBL_COUNT_MAX = 24;

	// Token: 0x040012F8 RID: 4856
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"Map3AttackType",
		"BallRand_A",
		"Position_A",
		"BallRand_B",
		"Position_B"
	};

	// Token: 0x040012F9 RID: 4857
	private int[] m_tblInfo;

	// Token: 0x040012FA RID: 4858
	private int m_tblCount;
}
