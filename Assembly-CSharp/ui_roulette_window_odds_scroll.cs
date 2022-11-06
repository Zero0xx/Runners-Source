using System;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class ui_roulette_window_odds_scroll : MonoBehaviour
{
	// Token: 0x0600289C RID: 10396 RVA: 0x000FB5AC File Offset: 0x000F97AC
	public void UpdateView(string prizeNmae, string oddsValue)
	{
		this.m_prizeName.text = prizeNmae;
		this.m_oddsValue.text = oddsValue;
	}

	// Token: 0x0400240D RID: 9229
	[SerializeField]
	private UILabel m_prizeName;

	// Token: 0x0400240E RID: 9230
	[SerializeField]
	private UILabel m_oddsValue;
}
