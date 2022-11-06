using System;
using System.Collections.Generic;
using System.Xml;
using App.Utility;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class CharacterDataNameInfo : MonoBehaviour
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06001030 RID: 4144 RVA: 0x0005F03C File Offset: 0x0005D23C
	public static string[] PrefixNameList
	{
		get
		{
			return CharacterDataNameInfo.m_prefixNameList;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06001031 RID: 4145 RVA: 0x0005F044 File Offset: 0x0005D244
	public static string[] CharaNameList
	{
		get
		{
			return CharacterDataNameInfo.m_charaNameList;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06001032 RID: 4146 RVA: 0x0005F04C File Offset: 0x0005D24C
	public static string[] CharaNameLowerList
	{
		get
		{
			return CharacterDataNameInfo.m_charaNameLowerList;
		}
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0005F054 File Offset: 0x0005D254
	private void Awake()
	{
		if (CharacterDataNameInfo.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			CharacterDataNameInfo.instance = this;
			this.Setup();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0005F098 File Offset: 0x0005D298
	private void OnDestroy()
	{
		if (CharacterDataNameInfo.instance == this)
		{
			CharacterDataNameInfo.instance = null;
		}
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0005F0B0 File Offset: 0x0005D2B0
	private void Start()
	{
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0005F0B4 File Offset: 0x0005D2B4
	private void Setup()
	{
		if (this.m_list == null)
		{
			this.m_list = new List<CharacterDataNameInfo.Info>();
			string xml = AESCrypt.Decrypt(this.m_text.text);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			this.m_list = new List<CharacterDataNameInfo.Info>();
			CharacterDataNameInfo.CreateTable(xmlDocument, this.m_list);
			this.SetNameList();
		}
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x0005F114 File Offset: 0x0005D314
	public CharacterDataNameInfo.Info GetDataByIndex(int index)
	{
		if (index < 0 || index >= this.m_list.Count)
		{
			return null;
		}
		return this.m_list[index];
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x0005F148 File Offset: 0x0005D348
	public CharacterDataNameInfo.Info GetDataByName(string name)
	{
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_name == name)
			{
				return info;
			}
		}
		return null;
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
	public CharacterDataNameInfo.Info GetDataByID(CharaType id)
	{
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_ID == id)
			{
				return info;
			}
		}
		return null;
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x0005F23C File Offset: 0x0005D43C
	public string GetNameByID(CharaType id)
	{
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_ID == id)
			{
				return info.m_name;
			}
		}
		return null;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0005F2B8 File Offset: 0x0005D4B8
	public CharacterDataNameInfo.Info GetDataByServerID(int serverID)
	{
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_serverID == serverID)
			{
				return info;
			}
		}
		return null;
	}

	// Token: 0x0600103C RID: 4156 RVA: 0x0005F330 File Offset: 0x0005D530
	public string GetNameByServerID(int serverID)
	{
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_serverID == serverID)
			{
				return info.m_name;
			}
		}
		return null;
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0005F3AC File Offset: 0x0005D5AC
	public static void CreateTable(XmlDocument doc, List<CharacterDataNameInfo.Info> list)
	{
		if (doc == null)
		{
			return;
		}
		if (doc.DocumentElement == null)
		{
			return;
		}
		XmlNodeList xmlNodeList = doc.SelectNodes("SonicRunnersCharacterInfo");
		if (xmlNodeList == null)
		{
			return;
		}
		if (xmlNodeList.Count == 0)
		{
			return;
		}
		if (xmlNodeList[0] == null)
		{
			return;
		}
		XmlNodeList xmlNodeList2 = xmlNodeList[0].SelectNodes("Character");
		foreach (object obj in xmlNodeList2)
		{
			XmlNode xmlNode = (XmlNode)obj;
			CharacterDataNameInfo.Info info = new CharacterDataNameInfo.Info();
			info.m_name = xmlNode.Attributes["Name"].Value;
			string value = xmlNode.Attributes["ID"].Value;
			if (Enum.IsDefined(typeof(CharaType), value))
			{
				info.m_ID = (CharaType)((int)Enum.Parse(typeof(CharaType), value, true));
			}
			info.m_hud_suffix = xmlNode.Attributes["Suffix"].Value;
			string value2 = xmlNode.Attributes["Attribute"].Value;
			if (Enum.IsDefined(typeof(CharacterAttribute), value2))
			{
				info.m_attribute = (CharacterAttribute)((int)Enum.Parse(typeof(CharacterAttribute), value2, true));
			}
			string value3 = xmlNode.Attributes["Team"].Value;
			if (Enum.IsDefined(typeof(TeamAttribute), value3))
			{
				info.m_teamAttribute = (TeamAttribute)((int)Enum.Parse(typeof(TeamAttribute), value3, true));
			}
			string value4 = xmlNode.Attributes["MainBonus"].Value;
			if (Enum.IsDefined(typeof(TeamAttributeBonusType), value4))
			{
				info.m_mainAttributeBonus = (TeamAttributeBonusType)((int)Enum.Parse(typeof(TeamAttributeBonusType), value4, true));
			}
			string value5 = xmlNode.Attributes["SubBonus"].Value;
			if (Enum.IsDefined(typeof(TeamAttributeBonusType), value5))
			{
				info.m_subAttributeBonus = (TeamAttributeBonusType)((int)Enum.Parse(typeof(TeamAttributeBonusType), value5, true));
			}
			float teamAttributeValue = 0f;
			if (float.TryParse(xmlNode.Attributes["TeamAttributeValue"].Value, out teamAttributeValue))
			{
				info.TeamAttributeValue = teamAttributeValue;
			}
			float teamAttributeSubValue = 0f;
			if (float.TryParse(xmlNode.Attributes["TeamAttributeSubValue"].Value, out teamAttributeSubValue))
			{
				info.TeamAttributeSubValue = teamAttributeSubValue;
			}
			string value6 = xmlNode.Attributes["Category"].Value;
			if (Enum.IsDefined(typeof(TeamAttributeCategory), value6))
			{
				info.m_teamAttributeCategory = (TeamAttributeCategory)((int)Enum.Parse(typeof(TeamAttributeCategory), value6, true));
			}
			else
			{
				info.m_teamAttributeCategory = TeamAttributeCategory.NUM;
			}
			int serverID;
			if (int.TryParse(xmlNode.Attributes["ServerID"].Value, out serverID))
			{
				info.m_serverID = serverID;
			}
			if (xmlNode.Attributes["OptBig"] != null)
			{
				string value7 = xmlNode.Attributes["OptBig"].Value;
				if (value7.Equals("true"))
				{
					info.m_flag.Set(0, true);
				}
			}
			if (xmlNode.Attributes["OptHighSpeed"] != null)
			{
				string value8 = xmlNode.Attributes["OptHighSpeed"].Value;
				if (value8.Equals("true"))
				{
					info.m_flag.Set(1, true);
				}
			}
			if (xmlNode.Attributes["OpThirdJump"] != null)
			{
				string value9 = xmlNode.Attributes["OpThirdJump"].Value;
				if (value9.Equals("true"))
				{
					info.m_flag.Set(2, true);
				}
			}
			if (info.m_ID != CharaType.UNKNOWN && info.m_attribute != CharacterAttribute.UNKNOWN && info.m_teamAttribute != TeamAttribute.UNKNOWN)
			{
				list.Add(info);
			}
		}
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x0005F810 File Offset: 0x0005DA10
	private void SetNameList()
	{
		if (this.m_list == null)
		{
			return;
		}
		CharacterDataNameInfo.m_prefixNameList = new string[this.m_list.Count];
		CharacterDataNameInfo.m_charaNameList = new string[this.m_list.Count];
		CharacterDataNameInfo.m_charaNameLowerList = new string[this.m_list.Count];
		foreach (CharacterDataNameInfo.Info info in this.m_list)
		{
			if (info.m_ID < (CharaType)this.m_list.Count)
			{
				CharacterDataNameInfo.m_prefixNameList[(int)info.m_ID] = info.m_hud_suffix;
				CharacterDataNameInfo.m_charaNameList[(int)info.m_ID] = info.m_name;
				CharacterDataNameInfo.m_charaNameLowerList[(int)info.m_ID] = info.m_name.ToLower();
			}
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x0600103F RID: 4159 RVA: 0x0005F90C File Offset: 0x0005DB0C
	public static CharacterDataNameInfo Instance
	{
		get
		{
			if (CharacterDataNameInfo.instance == null)
			{
				CharacterDataNameInfo.instance = GameObjectUtil.FindGameObjectComponent<CharacterDataNameInfo>("CharacterDataNameInfo");
			}
			return CharacterDataNameInfo.instance;
		}
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0005F940 File Offset: 0x0005DB40
	public static void LoadSetup()
	{
		GameObject gameObject = GameObject.Find("CharacterDataNameInfo");
		if (gameObject != null && gameObject.transform.parent != null && gameObject.transform.parent.name == "ETC")
		{
			gameObject.transform.parent = null;
		}
	}

	// Token: 0x04000DC7 RID: 3527
	private List<CharacterDataNameInfo.Info> m_list;

	// Token: 0x04000DC8 RID: 3528
	private static string[] m_prefixNameList;

	// Token: 0x04000DC9 RID: 3529
	private static string[] m_charaNameList;

	// Token: 0x04000DCA RID: 3530
	private static string[] m_charaNameLowerList;

	// Token: 0x04000DCB RID: 3531
	[SerializeField]
	private TextAsset m_text;

	// Token: 0x04000DCC RID: 3532
	private static CharacterDataNameInfo instance;

	// Token: 0x02000246 RID: 582
	public class Info
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0005F9E8 File Offset: 0x0005DBE8
		public float GetTeamAttributeValue(TeamAttributeBonusType type)
		{
			if (this.m_mainAttributeBonus == type)
			{
				return this.m_teamAttributeValue;
			}
			if (this.m_subAttributeBonus == type)
			{
				return this.m_teamAttributeSubValue;
			}
			return 0f;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0005FA24 File Offset: 0x0005DC24
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x0005FA18 File Offset: 0x0005DC18
		public float TeamAttributeValue
		{
			get
			{
				return this.m_teamAttributeValue;
			}
			set
			{
				this.m_teamAttributeValue = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x0005FA38 File Offset: 0x0005DC38
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x0005FA2C File Offset: 0x0005DC2C
		public float TeamAttributeSubValue
		{
			get
			{
				return this.m_teamAttributeSubValue;
			}
			set
			{
				this.m_teamAttributeSubValue = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0005FA50 File Offset: 0x0005DC50
		// (set) Token: 0x06001047 RID: 4167 RVA: 0x0005FA40 File Offset: 0x0005DC40
		public bool BigSize
		{
			get
			{
				return this.m_flag.Test(0);
			}
			set
			{
				this.m_flag.Set(0, value);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x0005FA70 File Offset: 0x0005DC70
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x0005FA60 File Offset: 0x0005DC60
		public bool HighSpeedEffect
		{
			get
			{
				return this.m_flag.Test(1);
			}
			set
			{
				this.m_flag.Set(1, value);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x0005FA90 File Offset: 0x0005DC90
		// (set) Token: 0x0600104B RID: 4171 RVA: 0x0005FA80 File Offset: 0x0005DC80
		public bool ThirdJump
		{
			get
			{
				return this.m_flag.Test(2);
			}
			set
			{
				this.m_flag.Set(2, value);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0005FAA0 File Offset: 0x0005DCA0
		public string characterSpriteName
		{
			get
			{
				string result = null;
				int id = (int)this.m_ID;
				if (id >= 0)
				{
					result = string.Format("ui_tex_player_{0:00}_{1}", id, CharacterDataNameInfo.m_prefixNameList[id]);
				}
				return result;
			}
		}

		// Token: 0x04000DCD RID: 3533
		public string m_name;

		// Token: 0x04000DCE RID: 3534
		public CharaType m_ID = CharaType.UNKNOWN;

		// Token: 0x04000DCF RID: 3535
		public string m_hud_suffix;

		// Token: 0x04000DD0 RID: 3536
		public CharacterAttribute m_attribute = CharacterAttribute.UNKNOWN;

		// Token: 0x04000DD1 RID: 3537
		public TeamAttribute m_teamAttribute = TeamAttribute.UNKNOWN;

		// Token: 0x04000DD2 RID: 3538
		public TeamAttributeCategory m_teamAttributeCategory = TeamAttributeCategory.NONE;

		// Token: 0x04000DD3 RID: 3539
		public TeamAttributeBonusType m_mainAttributeBonus = TeamAttributeBonusType.NONE;

		// Token: 0x04000DD4 RID: 3540
		public TeamAttributeBonusType m_subAttributeBonus = TeamAttributeBonusType.NONE;

		// Token: 0x04000DD5 RID: 3541
		private float m_teamAttributeValue;

		// Token: 0x04000DD6 RID: 3542
		private float m_teamAttributeSubValue;

		// Token: 0x04000DD7 RID: 3543
		public int m_serverID;

		// Token: 0x04000DD8 RID: 3544
		public Bitset32 m_flag;

		// Token: 0x02000247 RID: 583
		public enum Option
		{
			// Token: 0x04000DDA RID: 3546
			Big,
			// Token: 0x04000DDB RID: 3547
			HighSpeed,
			// Token: 0x04000DDC RID: 3548
			ThirdJump
		}
	}
}
