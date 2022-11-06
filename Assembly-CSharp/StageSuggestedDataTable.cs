using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class StageSuggestedDataTable : MonoBehaviour
{
	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00045FD4 File Offset: 0x000441D4
	public static StageSuggestedDataTable Instance
	{
		get
		{
			return StageSuggestedDataTable.instance;
		}
	}

	// Token: 0x06000C21 RID: 3105 RVA: 0x00045FDC File Offset: 0x000441DC
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x00045FE4 File Offset: 0x000441E4
	private void Start()
	{
		this.SetData();
		base.enabled = false;
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00045FF4 File Offset: 0x000441F4
	private void OnDestroy()
	{
		if (StageSuggestedDataTable.instance == this)
		{
			StageSuggestedDataTable.instance = null;
		}
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x0004600C File Offset: 0x0004420C
	private void SetInstance()
	{
		if (StageSuggestedDataTable.instance == null)
		{
			StageSuggestedDataTable.instance = this;
		}
		else if (this != StageSuggestedDataTable.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00046050 File Offset: 0x00044250
	private void SetData()
	{
		if (this.m_xml_data != null)
		{
			string s = AESCrypt.Decrypt(this.m_xml_data.text);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(StageSuggestedData[]));
			StringReader textReader = new StringReader(s);
			this.m_data = (StageSuggestedData[])xmlSerializer.Deserialize(textReader);
			if (this.m_data != null)
			{
				Array.Sort<StageSuggestedData>(this.m_data);
			}
		}
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000460C0 File Offset: 0x000442C0
	public CharacterAttribute[] GetStageSuggestedData(int stageIndex)
	{
		if (this.m_data != null)
		{
			int num = this.m_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_data[i].id == stageIndex)
				{
					return this.m_data[i].charaAttribute;
				}
			}
		}
		return null;
	}

	// Token: 0x04000997 RID: 2455
	[SerializeField]
	private TextAsset m_xml_data;

	// Token: 0x04000998 RID: 2456
	private StageSuggestedData[] m_data;

	// Token: 0x04000999 RID: 2457
	private static StageSuggestedDataTable instance;
}
