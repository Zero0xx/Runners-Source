using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class EventRaidBossAttackRateTable : MonoBehaviour
{
	// Token: 0x06000B98 RID: 2968 RVA: 0x00043D00 File Offset: 0x00041F00
	private void Start()
	{
		EventManager instance = EventManager.Instance;
		if (instance != null)
		{
			instance.SetRaidBossAttacRate(this.m_xml_data);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00043D38 File Offset: 0x00041F38
	private void OnDestroy()
	{
	}

	// Token: 0x0400093C RID: 2364
	[SerializeField]
	private TextAsset m_xml_data;
}
