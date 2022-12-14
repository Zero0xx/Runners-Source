using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class OverlapBonusTable : MonoBehaviour
{
	// Token: 0x06000C05 RID: 3077 RVA: 0x00044E94 File Offset: 0x00043094
	private void Start()
	{
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00044E98 File Offset: 0x00043098
	public void Setup()
	{
		if (this.m_overlapBonusTabel)
		{
			string xml = AESCrypt.Decrypt(this.m_overlapBonusTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			OverlapBonusTable.CreateTable(xmlDocument, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00044EEC File Offset: 0x000430EC
	public static string GetItemName(uint index)
	{
		if (index < 5U && (ulong)index < (ulong)((long)OverlapBonusTable.ITEM_NAMES.Length))
		{
			return OverlapBonusTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00044F20 File Offset: 0x00043120
	public static void CreateTable(XmlDocument doc, out int tbl_count)
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("BonusData");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		int num = 0;
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			XmlAttribute xmlAttribute = xmlNode.Attributes["charaID"];
			int serverID = 0;
			if (xmlAttribute != null)
			{
				serverID = int.Parse(xmlNode.Attributes["charaID"].Value, NumberStyles.AllowLeadingSign);
			}
			CharacterDataNameInfo instance = CharacterDataNameInfo.Instance;
			if (instance != null)
			{
				CharacterDataNameInfo.Info dataByServerID = instance.GetDataByServerID(serverID);
				if (dataByServerID != null)
				{
					CharaType id = dataByServerID.m_ID;
					XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Param");
					int num2 = 0;
					foreach (object obj2 in xmlNodeList2)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						int[] array = new int[5];
						for (int i = 0; i < 5; i++)
						{
							string itemName = OverlapBonusTable.GetItemName((uint)i);
							XmlAttribute xmlAttribute2 = xmlNode2.Attributes[itemName];
							int num3 = 0;
							if (xmlAttribute2 != null)
							{
								num3 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
							}
							array[i] = num3;
						}
						if (!OverlapBonusTable.m_OverlapBonus.ContainsKey(id))
						{
							OverlapBonusTable.m_OverlapBonus.Add(id, new List<int[]>());
						}
						OverlapBonusTable.m_OverlapBonus[id].Add(array);
						num2++;
					}
					num++;
				}
			}
		}
		tbl_count = num;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00045144 File Offset: 0x00043344
	public bool IsSetupEnd()
	{
		return this.m_tblCount > 0;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00045158 File Offset: 0x00043358
	public float GetStarBonusList(CharaType charaType, int star, OverlapBonusType bonusType)
	{
		if (this.IsSetupEnd())
		{
			CharaType key = charaType;
			if (!OverlapBonusTable.m_OverlapBonus.ContainsKey(key))
			{
				key = CharaType.SONIC;
			}
			if (star < OverlapBonusTable.m_OverlapBonus[key].Count && OverlapBonusType.SCORE <= bonusType && bonusType < OverlapBonusType.NUM)
			{
				return (float)OverlapBonusTable.m_OverlapBonus[key][star][(int)bonusType];
			}
		}
		return 0f;
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x000451C4 File Offset: 0x000433C4
	public Dictionary<BonusParam.BonusType, float> GetStarBonusList(CharaType charaType, int star)
	{
		Dictionary<BonusParam.BonusType, float> dictionary = new Dictionary<BonusParam.BonusType, float>();
		if (this.IsSetupEnd())
		{
			CharaType key = charaType;
			if (!OverlapBonusTable.m_OverlapBonus.ContainsKey(key))
			{
				key = CharaType.SONIC;
			}
			if (star < OverlapBonusTable.m_OverlapBonus[key].Count)
			{
				dictionary.Add(BonusParam.BonusType.SCORE, (float)OverlapBonusTable.m_OverlapBonus[key][star][0]);
				dictionary.Add(BonusParam.BonusType.RING, (float)OverlapBonusTable.m_OverlapBonus[key][star][1]);
				dictionary.Add(BonusParam.BonusType.ANIMAL, (float)OverlapBonusTable.m_OverlapBonus[key][star][2]);
				dictionary.Add(BonusParam.BonusType.DISTANCE, (float)OverlapBonusTable.m_OverlapBonus[key][star][3]);
				dictionary.Add(BonusParam.BonusType.ENEMY_OBJBREAK, (float)OverlapBonusTable.m_OverlapBonus[key][star][4]);
			}
		}
		return dictionary;
	}

	// Token: 0x06000C0C RID: 3084 RVA: 0x00045298 File Offset: 0x00043498
	public Dictionary<BonusParam.BonusType, float> GetStarBonusList(CharaType charaType)
	{
		Dictionary<BonusParam.BonusType, float> result = null;
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			ServerCharacterState serverCharacterState = playerState.CharacterState(charaType);
			if (serverCharacterState != null)
			{
				return this.GetStarBonusList(charaType, serverCharacterState.star);
			}
		}
		return result;
	}

	// Token: 0x0400098D RID: 2445
	[SerializeField]
	private TextAsset m_overlapBonusTabel;

	// Token: 0x0400098E RID: 2446
	private static Dictionary<CharaType, List<int[]>> m_OverlapBonus = new Dictionary<CharaType, List<int[]>>();

	// Token: 0x0400098F RID: 2447
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"Score",
		"Ring",
		"Animal",
		"Distance",
		"Enemy"
	};

	// Token: 0x04000990 RID: 2448
	private int m_tblCount;
}
