using System;
using UnityEngine;

// Token: 0x020003C0 RID: 960
public class HudProgressBar : MonoBehaviour
{
	// Token: 0x06001BF5 RID: 7157 RVA: 0x000A6484 File Offset: 0x000A4684
	public void SetUp(int stateNum)
	{
		this.m_stateNum = (float)stateNum;
		this.m_state = -1f;
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000A649C File Offset: 0x000A469C
	public void SetState(int state)
	{
		this.m_state = (float)state;
		if (this.m_state >= 0f)
		{
			base.gameObject.SetActive(true);
			if (this.m_slider != null)
			{
				this.m_slider.value = (this.m_state + 1f) / this.m_stateNum;
				if (this.m_parcentLabel != null)
				{
					int num = (int)(this.m_slider.value * 100f);
					this.m_parcentLabel.text = num.ToString() + "%";
				}
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040019B3 RID: 6579
	[SerializeField]
	private UISlider m_slider;

	// Token: 0x040019B4 RID: 6580
	[SerializeField]
	private UILabel m_parcentLabel;

	// Token: 0x040019B5 RID: 6581
	private float m_stateNum;

	// Token: 0x040019B6 RID: 6582
	private float m_state = -1f;
}
