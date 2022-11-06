using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class MileageMapPointDataTable : MonoBehaviour
{
	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00044698 File Offset: 0x00042898
	public static MileageMapPointDataTable Instance
	{
		get
		{
			return MileageMapPointDataTable.instance;
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000446A0 File Offset: 0x000428A0
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x000446A8 File Offset: 0x000428A8
	private void Start()
	{
		this.SetData();
		base.enabled = false;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x000446B8 File Offset: 0x000428B8
	private void OnDestroy()
	{
		if (MileageMapPointDataTable.instance == this)
		{
			MileageMapPointDataTable.instance = null;
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x000446D0 File Offset: 0x000428D0
	private void SetInstance()
	{
		if (MileageMapPointDataTable.instance == null)
		{
			MileageMapPointDataTable.instance = this;
		}
		else if (this != MileageMapPointDataTable.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00044714 File Offset: 0x00042914
	private void SetData()
	{
		if (this.m_xml_data != null)
		{
			string s = AESCrypt.Decrypt(this.m_xml_data.text);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(MileageMapPointData[]));
			StringReader textReader = new StringReader(s);
			this.m_point_data = (MileageMapPointData[])xmlSerializer.Deserialize(textReader);
			if (this.m_point_data != null)
			{
				Array.Sort<MileageMapPointData>(this.m_point_data);
			}
		}
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00044784 File Offset: 0x00042984
	public string GetTextureName(int id)
	{
		if (this.m_point_data != null)
		{
			int num = this.m_point_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_point_data[i].id == id)
				{
					return this.m_point_data[i].texture_name;
				}
			}
		}
		return null;
	}

	// Token: 0x04000969 RID: 2409
	[SerializeField]
	private TextAsset m_xml_data;

	// Token: 0x0400096A RID: 2410
	private MileageMapPointData[] m_point_data;

	// Token: 0x0400096B RID: 2411
	private static MileageMapPointDataTable instance;
}
