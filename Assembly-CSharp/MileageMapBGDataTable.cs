using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class MileageMapBGDataTable : MonoBehaviour
{
	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x000444F0 File Offset: 0x000426F0
	public static MileageMapBGDataTable Instance
	{
		get
		{
			return MileageMapBGDataTable.instance;
		}
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000444F8 File Offset: 0x000426F8
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00044500 File Offset: 0x00042700
	private void Start()
	{
		this.SetData();
		base.enabled = false;
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00044510 File Offset: 0x00042710
	private void OnDestroy()
	{
		if (MileageMapBGDataTable.instance == this)
		{
			MileageMapBGDataTable.instance = null;
		}
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00044528 File Offset: 0x00042728
	private void SetInstance()
	{
		if (MileageMapBGDataTable.instance == null)
		{
			MileageMapBGDataTable.instance = this;
		}
		else if (this != MileageMapBGDataTable.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0004456C File Offset: 0x0004276C
	private void SetData()
	{
		if (this.m_xml_data != null)
		{
			string s = AESCrypt.Decrypt(this.m_xml_data.text);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(MileageMapBGData[]));
			StringReader textReader = new StringReader(s);
			this.m_bg_data = (MileageMapBGData[])xmlSerializer.Deserialize(textReader);
			if (this.m_bg_data != null)
			{
				Array.Sort<MileageMapBGData>(this.m_bg_data);
			}
		}
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x000445DC File Offset: 0x000427DC
	public string GetTextureName(int id)
	{
		if (this.m_bg_data != null)
		{
			int num = this.m_bg_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_bg_data[i].id == id)
				{
					return this.m_bg_data[i].texture_name;
				}
			}
		}
		return null;
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00044634 File Offset: 0x00042834
	public string GetWindowTextureName(int id)
	{
		if (this.m_bg_data != null)
		{
			int num = this.m_bg_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_bg_data[i].id == id)
				{
					return this.m_bg_data[i].window_texture_name;
				}
			}
		}
		return null;
	}

	// Token: 0x04000966 RID: 2406
	[SerializeField]
	private TextAsset m_xml_data;

	// Token: 0x04000967 RID: 2407
	private MileageMapBGData[] m_bg_data;

	// Token: 0x04000968 RID: 2408
	private static MileageMapBGDataTable instance;
}
