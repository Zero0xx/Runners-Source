using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class EventCommonDataTable : MonoBehaviour
{
	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000B7C RID: 2940 RVA: 0x000434F0 File Offset: 0x000416F0
	public static EventCommonDataTable Instance
	{
		get
		{
			return EventCommonDataTable.s_instance;
		}
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x000434F8 File Offset: 0x000416F8
	private void Awake()
	{
		if (EventCommonDataTable.s_instance == null)
		{
			EventCommonDataTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0004352C File Offset: 0x0004172C
	private void OnDestroy()
	{
		if (EventCommonDataTable.s_instance == this)
		{
			EventCommonDataTable.s_instance = null;
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00043544 File Offset: 0x00041744
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0004354C File Offset: 0x0004174C
	public void Setup()
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new string[8];
		}
		if (this.m_xml_data)
		{
			string xml = AESCrypt.Decrypt(this.m_xml_data.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			EventCommonDataTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x000435BC File Offset: 0x000417BC
	public static void CreateTable(XmlDocument doc, string[] data, out int tbl_count)
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EventCommonDataTable");
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
				for (int i = 0; i < 8; i++)
				{
					string itemName = EventCommonDataTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					string text = string.Empty;
					if (xmlAttribute != null)
					{
						text = xmlNode2.Attributes[itemName].Value;
					}
					int num2 = num * 8 + i;
					data[num2] = text;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00043734 File Offset: 0x00041934
	public string GetData(EventCommonDataItem item_index)
	{
		int num = 0;
		if (this.m_tblInfo != null && (ulong)num < (ulong)((long)this.m_tblCount))
		{
			int num2 = (int)(num * 8 + item_index);
			if (num2 < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num2];
			}
		}
		return string.Empty;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00043780 File Offset: 0x00041980
	public bool IsRouletteEventChao(int chaoId)
	{
		string data = this.GetData(EventCommonDataItem.RouletteChao_Number);
		if (data != null && data != string.Empty)
		{
			string[] array = data.Split(new char[]
			{
				','
			});
			foreach (string s in array)
			{
				int num = int.Parse(s, NumberStyles.AllowLeadingSign);
				if (num == chaoId)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x000437F0 File Offset: 0x000419F0
	public static string GetItemName(uint index)
	{
		if (index < 8U && (ulong)index < (ulong)((long)EventCommonDataTable.ITEM_NAMES.Length))
		{
			return EventCommonDataTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00043824 File Offset: 0x00041A24
	public static void LoadSetup()
	{
		GameObject gameObject = GameObject.Find("EventResourceCommon");
		if (gameObject != null)
		{
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
				if (gameObject2.name == "EventCommonDataTable" && !gameObject2.activeSelf)
				{
					gameObject2.SetActive(true);
				}
			}
		}
	}

	// Token: 0x04000922 RID: 2338
	public const int ITEM_COUNT_MAX = 8;

	// Token: 0x04000923 RID: 2339
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x04000924 RID: 2340
	[SerializeField]
	private TextAsset m_xml_data;

	// Token: 0x04000925 RID: 2341
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"SeFileName",
		"MenuBgmFileName",
		"Roulette_BgmName",
		"RouletteS_BgmName",
		"EventTop_BgmName",
		"RouletteDecide_SeCueName",
		"RouletteChange_SeCueName",
		"RouletteChao_Number"
	};

	// Token: 0x04000926 RID: 2342
	private string[] m_tblInfo;

	// Token: 0x04000927 RID: 2343
	private int m_tblCount;

	// Token: 0x04000928 RID: 2344
	private static EventCommonDataTable s_instance = null;
}
