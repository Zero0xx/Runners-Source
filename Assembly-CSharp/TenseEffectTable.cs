using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class TenseEffectTable : MonoBehaviour
{
	// Token: 0x17000355 RID: 853
	// (get) Token: 0x06001428 RID: 5160 RVA: 0x0006C7F8 File Offset: 0x0006A9F8
	public static TenseEffectTable Instance
	{
		get
		{
			return TenseEffectTable.s_instance;
		}
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x0006C800 File Offset: 0x0006AA00
	private void Awake()
	{
		if (TenseEffectTable.s_instance == null)
		{
			TenseEffectTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x0006C834 File Offset: 0x0006AA34
	private void OnDestroy()
	{
		if (TenseEffectTable.s_instance == this)
		{
			TenseEffectTable.s_instance = null;
		}
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x0006C84C File Offset: 0x0006AA4C
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0006C854 File Offset: 0x0006AA54
	private void Setup()
	{
		TextAsset dataTabel = this.m_dataTabel;
		if (dataTabel)
		{
			string xml = AESCrypt.Decrypt(dataTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			TenseEffectTable.CreateTable(xmlDocument, out this.m_editParameters);
			if (this.m_editParameters.Count == 0)
			{
			}
		}
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x0006C8A8 File Offset: 0x0006AAA8
	public static void CreateTable(XmlDocument doc, out List<TenseParameter> outdata)
	{
		outdata = new List<TenseParameter>();
		outdata.Clear();
		if (doc == null)
		{
			return;
		}
		if (doc.DocumentElement == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("TenseEffectTable");
		if (xmlNodeList == null || xmlNodeList.Count == 0)
		{
			return;
		}
		foreach (object obj in xmlNodeList)
		{
			XmlNode xmlNode = (XmlNode)obj;
			string name_ = string.Empty;
			XmlAttribute xmlAttribute = xmlNode.Attributes["item"];
			if (xmlAttribute != null)
			{
				name_ = xmlAttribute.Value;
			}
			float r = 0f;
			XmlAttribute xmlAttribute2 = xmlNode.Attributes["r"];
			if (xmlAttribute2 != null)
			{
				r = float.Parse(xmlAttribute2.Value);
			}
			float g = 0f;
			XmlAttribute xmlAttribute3 = xmlNode.Attributes["g"];
			if (xmlAttribute3 != null)
			{
				g = float.Parse(xmlAttribute3.Value);
			}
			float b = 0f;
			XmlAttribute xmlAttribute4 = xmlNode.Attributes["b"];
			if (xmlAttribute4 != null)
			{
				b = float.Parse(xmlAttribute4.Value);
			}
			float a = 0f;
			XmlAttribute xmlAttribute5 = xmlNode.Attributes["a"];
			if (xmlAttribute5 != null)
			{
				a = float.Parse(xmlAttribute5.Value);
			}
			TenseParameter item = new TenseParameter(name_, new Color(r, g, b, a));
			outdata.Add(item);
		}
	}

	// Token: 0x0600142E RID: 5166 RVA: 0x0006CA68 File Offset: 0x0006AC68
	public bool IsSetupEnd()
	{
		return this.m_editParameters.Count != 0;
	}

	// Token: 0x0600142F RID: 5167 RVA: 0x0006CA80 File Offset: 0x0006AC80
	public Color GetData(string itemName)
	{
		if (this.IsSetupEnd())
		{
			foreach (TenseParameter tenseParameter in this.m_editParameters)
			{
				if (tenseParameter.name == itemName)
				{
					return tenseParameter.color;
				}
			}
		}
		return Color.white;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x0006CB10 File Offset: 0x0006AD10
	public static Color GetItemData(string itemName)
	{
		TenseEffectTable instance = TenseEffectTable.Instance;
		if (instance != null)
		{
			return instance.GetData(itemName);
		}
		return Color.white;
	}

	// Token: 0x04001191 RID: 4497
	[SerializeField]
	private TextAsset m_dataTabel;

	// Token: 0x04001192 RID: 4498
	private List<TenseParameter> m_editParameters = new List<TenseParameter>();

	// Token: 0x04001193 RID: 4499
	private static TenseEffectTable s_instance;
}
