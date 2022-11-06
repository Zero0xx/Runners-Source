using System;
using System.IO;
using System.Xml.Serialization;
using Text;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class MileageMapRouteDataTable : MonoBehaviour
{
	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000447E8 File Offset: 0x000429E8
	public static MileageMapRouteDataTable Instance
	{
		get
		{
			return MileageMapRouteDataTable.instance;
		}
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x000447F0 File Offset: 0x000429F0
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000447F8 File Offset: 0x000429F8
	private void Start()
	{
		this.SetData();
		base.enabled = false;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00044808 File Offset: 0x00042A08
	private void OnDestroy()
	{
		if (MileageMapRouteDataTable.instance == this)
		{
			MileageMapRouteDataTable.instance = null;
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00044820 File Offset: 0x00042A20
	private void SetInstance()
	{
		if (MileageMapRouteDataTable.instance == null)
		{
			MileageMapRouteDataTable.instance = this;
		}
		else if (this != MileageMapRouteDataTable.Instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00044864 File Offset: 0x00042A64
	private void SetData()
	{
		if (this.m_xml_data != null)
		{
			string s = AESCrypt.Decrypt(this.m_xml_data.text);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(MileageMapRouteData[]));
			StringReader textReader = new StringReader(s);
			this.m_route_data = (MileageMapRouteData[])xmlSerializer.Deserialize(textReader);
			if (this.m_route_data != null)
			{
				Array.Sort<MileageMapRouteData>(this.m_route_data);
			}
		}
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x000448D4 File Offset: 0x00042AD4
	public MileageMapRouteData GetMileageMapRouteData(int id)
	{
		if (this.m_route_data != null)
		{
			int num = this.m_route_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_route_data[i].id == id)
				{
					return this.m_route_data[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00044924 File Offset: 0x00042B24
	public MileageBonus GetBonusType(int id)
	{
		MileageBonus result = MileageBonus.UNKNOWN;
		if (this.m_route_data != null)
		{
			int num = this.m_route_data.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.m_route_data[i].id == id)
				{
					result = this.m_route_data[i].ability_type;
					break;
				}
			}
		}
		return result;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00044980 File Offset: 0x00042B80
	public string GetBonusTypeText(int id)
	{
		string bonusTypeTextWithoutColor = this.GetBonusTypeTextWithoutColor(id);
		if (bonusTypeTextWithoutColor != null)
		{
			string str = "[00d2ff]";
			return str + bonusTypeTextWithoutColor;
		}
		return null;
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x000449AC File Offset: 0x00042BAC
	public string GetBonusTypeTextWithoutColor(int id)
	{
		if (this.m_route_data != null)
		{
			MileageBonus bonusType = this.GetBonusType(id);
			TextObject textObject = null;
			TextManager.TextType textType = TextManager.TextType.TEXTTYPE_COMMON_TEXT;
			switch (bonusType)
			{
			case MileageBonus.SCORE:
				textObject = TextManager.GetText(textType, "Score", "score");
				break;
			case MileageBonus.ANIMAL:
				textObject = TextManager.GetText(textType, "Score", "animal");
				break;
			case MileageBonus.RING:
				textObject = TextManager.GetText(textType, "Item", "get_ring");
				break;
			case MileageBonus.DISTANCE:
				textObject = TextManager.GetText(textType, "Score", "distance");
				break;
			case MileageBonus.FINAL_SCORE:
				textObject = TextManager.GetText(textType, "Score", "final_score");
				break;
			}
			if (textObject != null)
			{
				return textObject.text;
			}
		}
		return null;
	}

	// Token: 0x0400096C RID: 2412
	[SerializeField]
	private TextAsset m_xml_data;

	// Token: 0x0400096D RID: 2413
	private MileageMapRouteData[] m_route_data;

	// Token: 0x0400096E RID: 2414
	private static MileageMapRouteDataTable instance;
}
