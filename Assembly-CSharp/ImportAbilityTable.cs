using System;
using System.Xml;
using UnityEngine;

// Token: 0x020004BD RID: 1213
public class ImportAbilityTable : MonoBehaviour
{
	// Token: 0x060023F1 RID: 9201 RVA: 0x000D7B60 File Offset: 0x000D5D60
	private void Awake()
	{
		if (ImportAbilityTable.m_instance == null)
		{
			this.Initialize();
			ImportAbilityTable.m_instance = this;
		}
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x000D7B80 File Offset: 0x000D5D80
	private void OnDestroy()
	{
		if (ImportAbilityTable.m_instance == this)
		{
			ImportAbilityTable.m_instance = null;
		}
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x000D7B98 File Offset: 0x000D5D98
	private void Initialize()
	{
		if (this.m_textAsset == null)
		{
			return;
		}
		string xml = AESCrypt.Decrypt(this.m_textAsset.text);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);
		XmlNode documentElement = xmlDocument.DocumentElement;
		if (documentElement == null)
		{
			return;
		}
		XmlNodeList childNodes = documentElement.ChildNodes;
		if (childNodes == null)
		{
			return;
		}
		this.m_table = new AbilityUpParamTable();
		int num = 0;
		foreach (object obj in childNodes)
		{
			XmlNode xmlNode = (XmlNode)obj;
			if (xmlNode != null)
			{
				AbilityUpParamList abilityUpParamList = new AbilityUpParamList();
				XmlNodeList childNodes2 = xmlNode.ChildNodes;
				foreach (object obj2 in childNodes2)
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					if (xmlNode2 != null)
					{
						string value = xmlNode2.Attributes.GetNamedItem("ring_cost").Value;
						string value2 = xmlNode2.Attributes.GetNamedItem("potential").Value;
						abilityUpParamList.AddAbilityUpParam(new AbilityUpParam
						{
							Cost = float.Parse(value),
							Potential = float.Parse(value2)
						});
					}
				}
				if (abilityUpParamList.Count > 0)
				{
					AbilityType type = (AbilityType)num;
					this.m_table.AddList(type, abilityUpParamList);
					num++;
				}
			}
		}
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x000D7D64 File Offset: 0x000D5F64
	public float GetAbilityPotential(AbilityType type, int level)
	{
		if (this.m_table == null)
		{
			return 0f;
		}
		AbilityUpParamList list = this.m_table.GetList(type);
		if (list == null)
		{
			return 0f;
		}
		AbilityUpParam abilityUpParam = list.GetAbilityUpParam(level);
		if (abilityUpParam == null)
		{
			return 0f;
		}
		return abilityUpParam.Potential;
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x000D7DB8 File Offset: 0x000D5FB8
	public float GetAbilityCost(AbilityType type, int level)
	{
		if (this.m_table == null)
		{
			return 0f;
		}
		AbilityUpParamList list = this.m_table.GetList(type);
		if (list == null)
		{
			return 0f;
		}
		AbilityUpParam abilityUpParam = list.GetAbilityUpParam(level);
		if (abilityUpParam == null)
		{
			return 0f;
		}
		return abilityUpParam.Cost;
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x000D7E0C File Offset: 0x000D600C
	public int GetMaxLevel(AbilityType type)
	{
		if (this.m_table == null)
		{
			return 0;
		}
		AbilityUpParamList list = this.m_table.GetList(type);
		if (list == null)
		{
			return 0;
		}
		return list.GetMaxLevel();
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x000D7E44 File Offset: 0x000D6044
	public static ImportAbilityTable GetInstance()
	{
		return ImportAbilityTable.m_instance;
	}

	// Token: 0x04002098 RID: 8344
	[SerializeField]
	private TextAsset m_textAsset;

	// Token: 0x04002099 RID: 8345
	private static ImportAbilityTable m_instance;

	// Token: 0x0400209A RID: 8346
	private AbilityUpParamTable m_table;
}
