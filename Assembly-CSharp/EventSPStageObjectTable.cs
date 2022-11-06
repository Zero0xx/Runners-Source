using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class EventSPStageObjectTable : MonoBehaviour
{
	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00043DC4 File Offset: 0x00041FC4
	public static EventSPStageObjectTable Instance
	{
		get
		{
			return EventSPStageObjectTable.s_instance;
		}
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00043DCC File Offset: 0x00041FCC
	private void Awake()
	{
		if (EventSPStageObjectTable.s_instance == null)
		{
			EventSPStageObjectTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00043E00 File Offset: 0x00042000
	private void OnDestroy()
	{
		if (EventSPStageObjectTable.s_instance == this)
		{
			EventSPStageObjectTable.s_instance = null;
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00043E18 File Offset: 0x00042018
	private void Start()
	{
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00043E1C File Offset: 0x0004201C
	public static string GetItemName(uint index)
	{
		if (index < 8U && (ulong)index < (ulong)((long)EventSPStageObjectTable.ITEM_NAMES.Length))
		{
			return EventSPStageObjectTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00043E50 File Offset: 0x00042050
	private void Setup()
	{
		if (this.IsSetupEnd())
		{
			return;
		}
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new string[8];
		}
		TextAsset dataTabel = this.m_dataTabel;
		if (dataTabel)
		{
			string xml = AESCrypt.Decrypt(dataTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			EventSPStageObjectTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
			if (this.m_tblCount != 0)
			{
				for (int i = 0; i < EventSPStageObjectTable.SPCRYSTAL_MODEL_TBL.Length; i++)
				{
					string itemData = EventSPStageObjectTable.GetItemData(EventSPStageObjectTable.SPCRYSTAL_MODEL_TBL[i]);
					if (itemData != null && itemData != string.Empty)
					{
						this.m_spCrystalModelList.Add(itemData);
					}
				}
			}
		}
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00043F18 File Offset: 0x00042118
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EventSPStageObjectTable");
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
					string itemName = EventSPStageObjectTable.GetItemName((uint)i);
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

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00044090 File Offset: 0x00042290
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x000440A0 File Offset: 0x000422A0
	public string GetData(EventSPStageObjectTableItem item_index)
	{
		if (!this.IsSetupEnd())
		{
			this.Setup();
		}
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

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000440FC File Offset: 0x000422FC
	private string GetData(int index)
	{
		return this.GetData((EventSPStageObjectTableItem)index);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00044108 File Offset: 0x00042308
	public List<string> GetSPCrystalModelList()
	{
		if (!this.IsSetupEnd())
		{
			this.Setup();
		}
		return this.m_spCrystalModelList;
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00044124 File Offset: 0x00042324
	public static void LoadSetup()
	{
		EventSPStageObjectTable instance = EventSPStageObjectTable.Instance;
		if (instance == null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventSPStageObjectTable");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00044168 File Offset: 0x00042368
	public static string GetItemData(EventSPStageObjectTableItem item_index)
	{
		EventSPStageObjectTable instance = EventSPStageObjectTable.Instance;
		if (instance != null)
		{
			return instance.GetData(item_index);
		}
		return string.Empty;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00044194 File Offset: 0x00042394
	public static string GetSPCrystalModel()
	{
		EventSPStageObjectTable instance = EventSPStageObjectTable.Instance;
		if (instance != null)
		{
			List<string> spcrystalModelList = instance.GetSPCrystalModelList();
			if (spcrystalModelList != null && spcrystalModelList.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, spcrystalModelList.Count);
				return spcrystalModelList[index];
			}
		}
		return string.Empty;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x000441E8 File Offset: 0x000423E8
	public static EventSPStageObjectTableItem GetEventSPStageObjectTableItem(string objName)
	{
		int num = Array.IndexOf<string>(EventSPStageObjectTable.ITEM_NAMES, objName);
		if (num >= 0)
		{
			return (EventSPStageObjectTableItem)num;
		}
		return EventSPStageObjectTableItem.NONE;
	}

	// Token: 0x04000948 RID: 2376
	public const int ITEM_COUNT_MAX = 8;

	// Token: 0x04000949 RID: 2377
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x0400094A RID: 2378
	[SerializeField]
	private TextAsset m_dataTabel;

	// Token: 0x0400094B RID: 2379
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"SPCrystalModelA",
		"SPCrystalModelB",
		"SPCrystalModelC",
		"SPCrystal10Model",
		"SPCrystalEffect",
		"SPCrystal10Effect",
		"SPCrystalSE",
		"SPCrystal10SE"
	};

	// Token: 0x0400094C RID: 2380
	private static readonly EventSPStageObjectTableItem[] SPCRYSTAL_MODEL_TBL = new EventSPStageObjectTableItem[]
	{
		EventSPStageObjectTableItem.SPCrystalModelA,
		EventSPStageObjectTableItem.SPCrystalModelB,
		EventSPStageObjectTableItem.SPCrystalModelC
	};

	// Token: 0x0400094D RID: 2381
	private List<string> m_spCrystalModelList = new List<string>();

	// Token: 0x0400094E RID: 2382
	private string[] m_tblInfo;

	// Token: 0x0400094F RID: 2383
	private int m_tblCount;

	// Token: 0x04000950 RID: 2384
	private static EventSPStageObjectTable s_instance = null;
}
