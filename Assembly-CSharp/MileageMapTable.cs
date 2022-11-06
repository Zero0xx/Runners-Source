using System;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class MileageMapTable : MonoBehaviour
{
	// Token: 0x06000BDF RID: 3039 RVA: 0x00044A8C File Offset: 0x00042C8C
	private void Start()
	{
		MileageMapDataManager instance = MileageMapDataManager.Instance;
		if (instance != null)
		{
			instance.SetData(this.m_xml_data);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00044AC4 File Offset: 0x00042CC4
	private void OnDestroy()
	{
	}

	// Token: 0x0400096F RID: 2415
	[SerializeField]
	private TextAsset m_xml_data;
}
