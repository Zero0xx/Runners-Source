using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Text
{
	// Token: 0x02000A27 RID: 2599
	internal class TextLoadImpl
	{
		// Token: 0x060044CF RID: 17615 RVA: 0x001623D8 File Offset: 0x001605D8
		public TextLoadImpl()
		{
			this.m_categoryList = new Dictionary<string, CellDataList>();
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x001623EC File Offset: 0x001605EC
		public TextLoadImpl(ResourceSceneLoader sceneLoader, string fileName, string suffixe)
		{
			this.m_categoryList = new Dictionary<string, CellDataList>();
			this.LoadScene(sceneLoader, fileName, suffixe);
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x00162408 File Offset: 0x00160608
		public string GetText(string categoryName, string cellName)
		{
			if (this.m_categoryList == null)
			{
				return null;
			}
			if (!this.m_categoryList.ContainsKey(categoryName))
			{
				global::Debug.LogWarning("Not Contains Key [GroupID]:" + categoryName + ", [CellID]:" + cellName);
				return null;
			}
			CellData cellData = this.m_categoryList[categoryName].Get(cellName);
			if (cellData == null)
			{
				global::Debug.LogWarning("Not Contains Key [GroupID]:" + categoryName + ", [CellID]:" + cellName);
				return null;
			}
			return cellData.m_cellString;
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x00162484 File Offset: 0x00160684
		public int GetCellCount(string categoryName)
		{
			if (!this.m_categoryList.ContainsKey(categoryName))
			{
				return -1;
			}
			return this.m_categoryList[categoryName].GetCount();
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x001624B8 File Offset: 0x001606B8
		public bool IsSetup()
		{
			return this.m_setup;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x001624C0 File Offset: 0x001606C0
		public void LoadScene(ResourceSceneLoader sceneLoader, string fileName, string suffixe)
		{
			if (sceneLoader != null)
			{
				bool onAssetBundle = true;
				string text = fileName + "_" + suffixe;
				GameObject x = GameObject.Find(text);
				if (x == null)
				{
					sceneLoader.AddLoad(text, onAssetBundle, false);
				}
			}
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x00162508 File Offset: 0x00160708
		public void LoadResourcesSetup(string fileName, string language)
		{
			TextAsset textAsset = Resources.Load(fileName) as TextAsset;
			if (textAsset == null)
			{
				return;
			}
			this.TextSetup(textAsset, language);
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x00162538 File Offset: 0x00160738
		public void LoadSceneSetup(string fileName, string language, string suffixe)
		{
			string name = fileName + "_" + suffixe;
			GameObject gameObject = GameObject.Find(name);
			if (gameObject == null)
			{
				return;
			}
			AssetBundleTextData component = gameObject.GetComponent<AssetBundleTextData>();
			if (component == null)
			{
				return;
			}
			this.TextSetup(component.m_TextAsset, language);
			UnityEngine.Object.Destroy(gameObject);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00162590 File Offset: 0x00160790
		private void TextSetup(TextAsset textData, string language)
		{
			string xml = AESCrypt.Decrypt(textData.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement == null)
			{
				return;
			}
			XmlNodeList xmlNodeList = documentElement.SelectNodes("LanguageData");
			if (xmlNodeList == null)
			{
				return;
			}
			XmlNode xmlNode = null;
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				XmlNode xmlNode2 = xmlNodeList[i];
				if (xmlNode2 != null)
				{
					XmlNode namedItem = xmlNode2.Attributes.GetNamedItem("type");
					if (namedItem != null)
					{
						string value = namedItem.Value;
						if (value != null)
						{
							if (value == language)
							{
								xmlNode = xmlNodeList[i];
								break;
							}
						}
					}
				}
			}
			if (xmlNode == null)
			{
				return;
			}
			XmlNode xmlNode3 = xmlNode.SelectSingleNode("CategoryList");
			if (xmlNode3 == null)
			{
				return;
			}
			XmlNodeList xmlNodeList2 = xmlNode3.SelectNodes("CategoryData");
			if (xmlNodeList2 == null)
			{
				return;
			}
			for (int j = 0; j < xmlNodeList2.Count; j++)
			{
				XmlNode xmlNode4 = xmlNodeList2[j];
				if (xmlNode4 != null)
				{
					XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("ID");
					if (namedItem2 != null)
					{
						string value2 = namedItem2.Value;
						this.CreateCategoryData(xmlNode4.SelectSingleNode("CellList"), value2);
					}
				}
			}
			this.m_setup = true;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x00162708 File Offset: 0x00160908
		private void CreateCategoryData(XmlNode parentNode, string categoryName)
		{
			if (parentNode == null)
			{
				return;
			}
			XmlNodeList xmlNodeList = parentNode.SelectNodes("Cell");
			if (xmlNodeList == null)
			{
				return;
			}
			CellDataList cellDataList = new CellDataList(categoryName);
			for (int i = 0; i < xmlNodeList.Count; i++)
			{
				XmlNode xmlNode = xmlNodeList[i];
				if (xmlNode != null)
				{
					XmlNode namedItem = xmlNode.Attributes.GetNamedItem("ID");
					if (namedItem != null)
					{
						XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("String");
						if (namedItem2 != null)
						{
							CellData cellData = new CellData(namedItem.Value, namedItem2.Value);
							cellDataList.Add(cellData);
						}
					}
				}
			}
			this.m_categoryList.Add(categoryName, cellDataList);
		}

		// Token: 0x040039BD RID: 14781
		private Dictionary<string, CellDataList> m_categoryList;

		// Token: 0x040039BE RID: 14782
		private bool m_setup;
	}
}
