using System;
using UnityEngine;

// Token: 0x020003CF RID: 975
public class HudFlagWatcher
{
	// Token: 0x06001C4B RID: 7243 RVA: 0x000A89C4 File Offset: 0x000A6BC4
	public void Setup(GameObject watchObject, HudFlagWatcher.ValueChangeCallback callback)
	{
		this.m_watchObject = watchObject;
		this.m_callback = callback;
		if (this.m_watchObject != null)
		{
			this.m_watchObject.SetActive(true);
			this.m_value = this.m_watchObject.transform.localPosition.x;
		}
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x000A8A1C File Offset: 0x000A6C1C
	public void Update()
	{
		if (this.m_watchObject != null)
		{
			float x = this.m_watchObject.transform.localPosition.x;
			if (x != this.m_value)
			{
				if (this.m_callback != null)
				{
					this.m_callback(x, this.m_value);
				}
				this.m_value = x;
			}
		}
	}

	// Token: 0x04001A4E RID: 6734
	private GameObject m_watchObject;

	// Token: 0x04001A4F RID: 6735
	private float m_value;

	// Token: 0x04001A50 RID: 6736
	private HudFlagWatcher.ValueChangeCallback m_callback;

	// Token: 0x02000A88 RID: 2696
	// (Invoke) Token: 0x0600485A RID: 18522
	public delegate void ValueChangeCallback(float newValue, float prevValue);
}
