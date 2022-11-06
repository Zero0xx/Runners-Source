using System;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class SpecialStageEventDataTable : MonoBehaviour
{
	// Token: 0x06000C1C RID: 3100 RVA: 0x00045F8C File Offset: 0x0004418C
	private void Start()
	{
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			instance.SetEventMenuData(this.m_xml_data);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x00045FC4 File Offset: 0x000441C4
	private void OnDestroy()
	{
	}

	// Token: 0x04000996 RID: 2454
	[SerializeField]
	private TextAsset m_xml_data;
}
