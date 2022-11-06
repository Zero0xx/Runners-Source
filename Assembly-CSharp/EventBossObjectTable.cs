using System;
using System.Xml;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class EventBossObjectTable : MonoBehaviour
{
	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00042A5C File Offset: 0x00040C5C
	public static EventBossObjectTable Instance
	{
		get
		{
			return EventBossObjectTable.s_instance;
		}
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x00042A64 File Offset: 0x00040C64
	private void Awake()
	{
		if (EventBossObjectTable.s_instance == null)
		{
			EventBossObjectTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00042A98 File Offset: 0x00040C98
	private void OnDestroy()
	{
		if (EventBossObjectTable.s_instance == this)
		{
			EventBossObjectTable.s_instance = null;
		}
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00042AB0 File Offset: 0x00040CB0
	private void Start()
	{
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00042AB4 File Offset: 0x00040CB4
	public static string GetItemName(uint index)
	{
		if (index < 15U && (ulong)index < (ulong)((long)EventBossObjectTable.ITEM_NAMES.Length))
		{
			return EventBossObjectTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00042ADC File Offset: 0x00040CDC
	public void Setup()
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new string[15];
		}
		TextAsset dataTabel = this.m_dataTabel;
		if (dataTabel)
		{
			string xml = AESCrypt.Decrypt(dataTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			EventBossObjectTable.CreateTable(xmlDocument, this.m_tblInfo, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00042B4C File Offset: 0x00040D4C
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EventBossObjectTable");
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
				for (int i = 0; i < 15; i++)
				{
					string itemName = EventBossObjectTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					string text = string.Empty;
					if (xmlAttribute != null)
					{
						text = xmlNode2.Attributes[itemName].Value;
					}
					int num2 = num * 15 + i;
					data[num2] = text;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00042CC4 File Offset: 0x00040EC4
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00042CD4 File Offset: 0x00040ED4
	public string GetData(EventBossObjectTableItem item_index)
	{
		int num = 0;
		if (this.m_tblInfo != null && (ulong)num < (ulong)((long)this.m_tblCount))
		{
			int num2 = (int)(num * 15 + item_index);
			if (num2 < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num2];
			}
		}
		return string.Empty;
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00042D20 File Offset: 0x00040F20
	private string GetData(int index)
	{
		return this.GetData((EventBossObjectTableItem)index);
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x00042D2C File Offset: 0x00040F2C
	public static void LoadSetup()
	{
		EventBossObjectTable instance = EventBossObjectTable.Instance;
		if (instance == null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventBossObjectTable");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
			EventBossObjectTable instance2 = EventBossObjectTable.Instance;
			if (instance2 != null)
			{
				instance2.Setup();
			}
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00042D88 File Offset: 0x00040F88
	public static string GetItemData(EventBossObjectTableItem item_index)
	{
		EventBossObjectTable instance = EventBossObjectTable.Instance;
		if (instance != null)
		{
			return instance.GetData(item_index);
		}
		return string.Empty;
	}

	// Token: 0x040008E6 RID: 2278
	public const int ITEM_COUNT_MAX = 15;

	// Token: 0x040008E7 RID: 2279
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x040008E8 RID: 2280
	[SerializeField]
	private TextAsset m_dataTabel;

	// Token: 0x040008E9 RID: 2281
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"BgmFile",
		"BgmCueName",
		"RingModel",
		"Ring10Model",
		"RingEffect",
		"Ring10Effect",
		"RingSE",
		"Ring10SE",
		"EscapeEffect",
		"Obj1_ModelName",
		"Obj1_EffectName",
		"Obj1_LoopEffectName",
		"Obj1_SetSeName",
		"Obj2_ModelName",
		"Obj2_EffectName"
	};

	// Token: 0x040008EA RID: 2282
	private string[] m_tblInfo;

	// Token: 0x040008EB RID: 2283
	private int m_tblCount;

	// Token: 0x040008EC RID: 2284
	private static EventBossObjectTable s_instance = null;
}
