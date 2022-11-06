using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class ObjectPartTable
{
	// Token: 0x0600163B RID: 5691 RVA: 0x0007F33C File Offset: 0x0007D53C
	private void Start()
	{
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x0007F340 File Offset: 0x0007D540
	public static string GetItemName(uint index)
	{
		if ((ulong)index < (ulong)((long)ObjectPartTable.ITEM_NAMES.Length))
		{
			return ObjectPartTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x0007F360 File Offset: 0x0007D560
	public BrokenBonusType GetBrokenBonusType()
	{
		int data = this.GetData(ObjectPartType.BROKENBONUS_RATIO);
		int randomRange = ObjUtil.GetRandomRange100();
		if (randomRange < data)
		{
			bool flag = false;
			if (StageModeManager.Instance != null)
			{
				flag = StageModeManager.Instance.FirstTutorial;
			}
			int randomRange2 = ObjUtil.GetRandomRange100();
			int num = 0;
			for (int i = 1; i < 4; i++)
			{
				int num2 = this.GetData((ObjectPartType)i);
				if (flag && i == 2)
				{
					num2 = 0;
				}
				if (num <= randomRange2 && randomRange2 < num + num2)
				{
					switch (i)
					{
					case 1:
						return BrokenBonusType.SUPER_RING;
					case 2:
						return BrokenBonusType.REDSTAR_RING;
					case 3:
						return BrokenBonusType.CRYSTAL10;
					}
				}
				num += num2;
			}
		}
		return BrokenBonusType.NONE;
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x0007F418 File Offset: 0x0007D618
	public BrokenBonusType GetBrokenBonusTypeForChaoAbility()
	{
		int data = this.GetData(ObjectPartType.BROKENBONUS_RATIO);
		int randomRange = ObjUtil.GetRandomRange100();
		if (randomRange >= data)
		{
			return BrokenBonusType.NONE;
		}
		int randomRange2 = ObjUtil.GetRandomRange100();
		int num = 0;
		if (StageAbilityManager.Instance != null)
		{
			num = (int)StageAbilityManager.Instance.GetChaoAbilityExtraValue(ChaoAbility.COMBO_STEP_DESTROY_GET_10_RING);
		}
		if (randomRange2 <= 1)
		{
			return BrokenBonusType.REDSTAR_RING;
		}
		if (randomRange2 <= num)
		{
			return BrokenBonusType.SUPER_RING;
		}
		return BrokenBonusType.CRYSTAL10;
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x0007F478 File Offset: 0x0007D678
	public int GetComboBonusComboNum(int index)
	{
		return this.GetData(ObjectPartType.COMBOBONUS_COMBO1 + index);
	}

	// Token: 0x06001640 RID: 5696 RVA: 0x0007F484 File Offset: 0x0007D684
	public int GetComboBonusBonusNum(int index)
	{
		return this.GetData(ObjectPartType.COMBOBONUS_BONUS1 + index);
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x0007F490 File Offset: 0x0007D690
	public void Setup(TerrainXmlData terrainData)
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[19];
		}
		if (terrainData != null)
		{
			TextAsset objectPartTableData = terrainData.ObjectPartTableData;
			if (objectPartTableData)
			{
				string xml = AESCrypt.Decrypt(objectPartTableData.text);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				ObjectPartTable.CreateTable(xmlDocument, this.m_tblInfo);
			}
		}
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0007F4F8 File Offset: 0x0007D6F8
	public static void CreateTable(XmlDocument doc, int[] data)
	{
		if (doc == null)
		{
			return;
		}
		if (doc.DocumentElement == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("ObjectPartTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < 19; i++)
		{
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string itemName = ObjectPartTable.GetItemName((uint)i);
				XmlNodeList xmlNodeList2 = xmlNode.SelectNodes(itemName);
				foreach (object obj2 in xmlNodeList2)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (xmlNode2.InnerText != null)
					{
						num = int.Parse(xmlNode2.InnerText, NumberStyles.AllowLeadingSign);
					}
				}
			}
			data[i] = num;
		}
	}

	// Token: 0x06001643 RID: 5699 RVA: 0x0007F638 File Offset: 0x0007D838
	private int GetData(ObjectPartType item_index)
	{
		if (this.m_tblInfo != null && item_index < (ObjectPartType)this.m_tblInfo.Length)
		{
			return this.m_tblInfo[(int)item_index];
		}
		return 0;
	}

	// Token: 0x040013D5 RID: 5077
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"BrokenBonusRatio",
		"BrokenBonusSuperRing",
		"BrokenBonusRedStarRing",
		"BrokenBonusCrystal10",
		"ComboBonusCombo1",
		"ComboBonusCombo2",
		"ComboBonusCombo3",
		"ComboBonusCombo4",
		"ComboBonusCombo5",
		"ComboBonusCombo6",
		"ComboBonusCombo7",
		"ComboBonusBonus1",
		"ComboBonusBonus2",
		"ComboBonusBonus3",
		"ComboBonusBonus4",
		"ComboBonusBonus5",
		"ComboBonusBonus6",
		"ComboBonusBonus7",
		"ComboBonusBonus8"
	};

	// Token: 0x040013D6 RID: 5078
	private int[] m_tblInfo;
}
