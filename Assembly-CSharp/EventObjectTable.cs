using System;
using System.Globalization;
using System.Xml;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class EventObjectTable : MonoBehaviour
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0004392C File Offset: 0x00041B2C
	public static EventObjectTable Instance
	{
		get
		{
			return EventObjectTable.s_instance;
		}
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00043934 File Offset: 0x00041B34
	private void Awake()
	{
		if (EventObjectTable.s_instance == null)
		{
			EventObjectTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00043968 File Offset: 0x00041B68
	private void OnDestroy()
	{
		if (EventObjectTable.s_instance == this)
		{
			EventObjectTable.s_instance = null;
		}
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00043980 File Offset: 0x00041B80
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00043988 File Offset: 0x00041B88
	public static string GetItemName(uint index)
	{
		if (index < 8U && (ulong)index < (ulong)((long)EventObjectTable.ITEM_NAMES.Length))
		{
			return EventObjectTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x000439BC File Offset: 0x00041BBC
	public static bool IsEventCrystalBig(EventObjectTableItem index)
	{
		switch (index)
		{
		case EventObjectTableItem.CrystalA:
		case EventObjectTableItem.CrystalB:
		case EventObjectTableItem.CrystalC:
			return false;
		default:
			return true;
		}
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x000439E8 File Offset: 0x00041BE8
	private void Setup()
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new int[24];
		}
		TextAsset dataTabel = this.m_dataTabel;
		if (dataTabel)
		{
			string xml = AESCrypt.Decrypt(dataTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			EventObjectTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x00043A58 File Offset: 0x00041C58
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EventObjectTable");
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
					string itemName = EventObjectTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					int num2 = 0;
					if (xmlAttribute != null)
					{
						num2 = int.Parse(xmlNode2.Attributes[itemName].Value, NumberStyles.AllowLeadingSign);
					}
					int num3 = num * 8 + i;
					data[num3] = num2;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00043BD0 File Offset: 0x00041DD0
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00043BE0 File Offset: 0x00041DE0
	public int GetData(int tbl_index, EventObjectTableItem item_index)
	{
		if (this.m_tblInfo != null && (ulong)tbl_index < (ulong)((long)this.m_tblCount))
		{
			int num = (int)(tbl_index * 8 + item_index);
			if (num < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num];
			}
		}
		return 0;
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00043C24 File Offset: 0x00041E24
	private int GetData(int index)
	{
		if (this.m_tblInfo != null && index < this.m_tblInfo.Length)
		{
			return this.m_tblInfo[index];
		}
		return 0;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00043C4C File Offset: 0x00041E4C
	public static void LoadSetup()
	{
		EventObjectTable instance = EventObjectTable.Instance;
		if (instance == null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventObjectTable");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00043C90 File Offset: 0x00041E90
	public static int GetItemData(int tbl_index, EventObjectTableItem item_index)
	{
		EventObjectTable instance = EventObjectTable.Instance;
		if (instance != null)
		{
			return instance.GetData(tbl_index, item_index);
		}
		return 0;
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00043CBC File Offset: 0x00041EBC
	public static EventObjectTableItem GetEventObjectTableItem(string objName)
	{
		int num = Array.IndexOf<string>(EventObjectTable.ITEM_NAMES, objName);
		if (num >= 0)
		{
			return (EventObjectTableItem)num;
		}
		return EventObjectTableItem.NONE;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00043CE0 File Offset: 0x00041EE0
	public static bool IsCyrstal(EventObjectTableItem item)
	{
		return Array.IndexOf<EventObjectTableItem>(EventObjectTable.CRYSTAL_ITEMS, item) >= 0;
	}

	// Token: 0x04000934 RID: 2356
	public const int ITEM_COUNT_MAX = 8;

	// Token: 0x04000935 RID: 2357
	public const int TBL_COUNT_MAX = 3;

	// Token: 0x04000936 RID: 2358
	[SerializeField]
	private TextAsset m_dataTabel;

	// Token: 0x04000937 RID: 2359
	private static readonly EventObjectTableItem[] CRYSTAL_ITEMS = new EventObjectTableItem[]
	{
		EventObjectTableItem.CrystalA,
		EventObjectTableItem.CrystalB,
		EventObjectTableItem.CrystalC,
		EventObjectTableItem.Crystal10A,
		EventObjectTableItem.Crystal10B,
		EventObjectTableItem.Crystal10C
	};

	// Token: 0x04000938 RID: 2360
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"ObjRing",
		"ObjSuperRing",
		"ObjCrystal_A",
		"ObjCrystal_B",
		"ObjCrystal_C",
		"ObjCrystal10_A",
		"ObjCrystal10_B",
		"ObjCrystal10_C"
	};

	// Token: 0x04000939 RID: 2361
	private int[] m_tblInfo;

	// Token: 0x0400093A RID: 2362
	private int m_tblCount;

	// Token: 0x0400093B RID: 2363
	private static EventObjectTable s_instance = null;
}
