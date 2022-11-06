using System;
using System.Xml;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class EventBossParamTable : MonoBehaviour
{
	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00042E9C File Offset: 0x0004109C
	public static EventBossParamTable Instance
	{
		get
		{
			return EventBossParamTable.s_instance;
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00042EA4 File Offset: 0x000410A4
	private void Awake()
	{
		if (EventBossParamTable.s_instance == null)
		{
			EventBossParamTable.s_instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00042ED8 File Offset: 0x000410D8
	private void OnDestroy()
	{
		if (EventBossParamTable.s_instance == this)
		{
			EventBossParamTable.s_instance = null;
		}
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x00042EF0 File Offset: 0x000410F0
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00042EF8 File Offset: 0x000410F8
	public static string GetItemName(uint index)
	{
		if (index < 12U && (ulong)index < (ulong)((long)EventBossParamTable.ITEM_NAMES.Length))
		{
			return EventBossParamTable.ITEM_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00042F20 File Offset: 0x00041120
	public static string GetRingDropName(uint index)
	{
		if (index < 10U && (ulong)index < (ulong)((long)EventBossParamTable.RING_DROP_LEVEL_NAMES.Length))
		{
			return EventBossParamTable.RING_DROP_LEVEL_NAMES[(int)((UIntPtr)index)];
		}
		return string.Empty;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00042F48 File Offset: 0x00041148
	private void Setup()
	{
		if (this.m_tblInfo == null)
		{
			this.m_tblInfo = new float[12];
		}
		if (this.m_newTblInfo == null)
		{
			this.m_newTblInfo = new EventBossParamTable.BossParam();
		}
		TextAsset dataTabel = this.m_dataTabel;
		if (dataTabel)
		{
			string xml = AESCrypt.Decrypt(dataTabel.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			EventBossParamTable.CreateTable(xmlDocument, this.m_tblInfo, this.m_newTblInfo, out this.m_tblCount);
			if (this.m_tblCount == 0)
			{
			}
		}
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00042FD4 File Offset: 0x000411D4
	public static void CreateTable(XmlDocument doc, float[] data, EventBossParamTable.BossParam param, out int tbl_count)
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
		XmlNodeList xmlNodeList = doc.DocumentElement.SelectNodes("EventBossParamTable");
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
				for (int i = 0; i < 12; i++)
				{
					string itemName = EventBossParamTable.GetItemName((uint)i);
					XmlAttribute xmlAttribute = xmlNode2.Attributes[itemName];
					float num2 = 0f;
					if (xmlAttribute != null)
					{
						num2 = float.Parse(xmlNode2.Attributes[itemName].Value);
					}
					int num3 = num * 12 + i;
					data[num3] = num2;
				}
			}
			XmlNodeList xmlNodeList3 = xmlNode.SelectNodes("Ring");
			int num4 = 0;
			foreach (object obj3 in xmlNodeList3)
			{
				XmlNode xmlNode3 = (XmlNode)obj3;
				if (num4 < 10)
				{
					int rate = 0;
					if (int.TryParse(xmlNode3.Attributes["SurperRingRate"].Value, out rate))
					{
						param.m_rings[num4].rate = rate;
					}
					int normal = 0;
					if (int.TryParse(xmlNode3.Attributes["Normal"].Value, out normal))
					{
						param.m_rings[num4].normal = normal;
					}
					int rare = 0;
					if (int.TryParse(xmlNode3.Attributes["Rare"].Value, out rare))
					{
						param.m_rings[num4].rare = rare;
					}
					int srare = 0;
					if (int.TryParse(xmlNode3.Attributes["SRare"].Value, out srare))
					{
						param.m_rings[num4].srare = srare;
					}
					num4++;
				}
			}
			num++;
		}
		tbl_count = num;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x000432B4 File Offset: 0x000414B4
	public bool IsSetupEnd()
	{
		return this.m_tblInfo != null;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x000432C4 File Offset: 0x000414C4
	public float GetData(EventBossParamTableItem item_index)
	{
		int num = 0;
		if (this.m_tblInfo != null && (ulong)num < (ulong)((long)this.m_tblCount))
		{
			int num2 = (int)(num * 12 + item_index);
			if (num2 < this.m_tblInfo.Length)
			{
				return this.m_tblInfo[num2];
			}
		}
		return 0f;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00043310 File Offset: 0x00041510
	public int GetData(BossType bossType, int playerAggressivity)
	{
		if (this.m_newTblInfo != null)
		{
			for (int i = 0; i < 10; i++)
			{
				int num = -1;
				switch (bossType)
				{
				case BossType.EVENT1:
					num = this.m_newTblInfo.m_rings[i].normal;
					break;
				case BossType.EVENT2:
					num = this.m_newTblInfo.m_rings[i].rare;
					break;
				case BossType.EVENT3:
					num = this.m_newTblInfo.m_rings[i].srare;
					break;
				}
				if (num > 0 && playerAggressivity <= num)
				{
					return this.m_newTblInfo.m_rings[i].rate;
				}
			}
		}
		return 0;
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x000433D4 File Offset: 0x000415D4
	private float GetData(int index)
	{
		return this.GetData((EventBossParamTableItem)index);
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x000433E0 File Offset: 0x000415E0
	public static void LoadSetup()
	{
		EventBossParamTable instance = EventBossParamTable.Instance;
		if (instance == null)
		{
			GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, "EventBossParamTable");
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00043424 File Offset: 0x00041624
	public static float GetItemData(EventBossParamTableItem item_index)
	{
		EventBossParamTable instance = EventBossParamTable.Instance;
		if (instance != null)
		{
			return instance.GetData(item_index);
		}
		return 0f;
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00043450 File Offset: 0x00041650
	public static int GetSuperRingDropData(BossType bossType, int playerAggressivity)
	{
		if (EventBossParamTable.Instance != null)
		{
			return EventBossParamTable.Instance.GetData(bossType, playerAggressivity);
		}
		return 0;
	}

	// Token: 0x04000908 RID: 2312
	public const int ITEM_COUNT_MAX = 12;

	// Token: 0x04000909 RID: 2313
	public const int RING_LEVEL_COUNT_MAX = 10;

	// Token: 0x0400090A RID: 2314
	public const int TBL_COUNT_MAX = 1;

	// Token: 0x0400090B RID: 2315
	[SerializeField]
	private TextAsset m_dataTabel;

	// Token: 0x0400090C RID: 2316
	private static readonly string[] ITEM_NAMES = new string[]
	{
		"Level1",
		"Level2",
		"Level3",
		"Level4",
		"WispRatio",
		"WispRatioDown",
		"BoostAttack1",
		"BoostAttack2",
		"BoostAttack3",
		"BoostSpeed1",
		"BoostSpeed2",
		"BoostSpeed3"
	};

	// Token: 0x0400090D RID: 2317
	private static readonly string[] RING_DROP_LEVEL_NAMES = new string[]
	{
		"RingDropLevel1",
		"RingDropLevel2",
		"RingDropLevel3",
		"RingDropLevel4",
		"RingDropLevel5",
		"RingDropLevel6",
		"RingDropLevel7",
		"RingDropLevel8",
		"RingDropLevel9",
		"RingDropLevel10"
	};

	// Token: 0x0400090E RID: 2318
	private float[] m_tblInfo;

	// Token: 0x0400090F RID: 2319
	private EventBossParamTable.BossParam m_newTblInfo;

	// Token: 0x04000910 RID: 2320
	private int m_tblCount;

	// Token: 0x04000911 RID: 2321
	private static EventBossParamTable s_instance = null;

	// Token: 0x0200018D RID: 397
	public struct DropRingParam
	{
		// Token: 0x04000912 RID: 2322
		public int rate;

		// Token: 0x04000913 RID: 2323
		public int normal;

		// Token: 0x04000914 RID: 2324
		public int rare;

		// Token: 0x04000915 RID: 2325
		public int srare;
	}

	// Token: 0x0200018E RID: 398
	public class BossParam
	{
		// Token: 0x04000916 RID: 2326
		public EventBossParamTable.DropRingParam[] m_rings = new EventBossParamTable.DropRingParam[10];
	}
}
