using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class HudDailyBattleButton
{
	// Token: 0x060021BA RID: 8634 RVA: 0x000CB1C4 File Offset: 0x000C93C4
	public void Initialize(GameObject mainMenuObject)
	{
		if (mainMenuObject == null)
		{
			return;
		}
		this.m_mainMenuObject = mainMenuObject;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		this.m_quickModeObject = GameObjectUtil.FindChildGameObject(gameObject, "1_Quick");
		if (this.m_quickModeObject == null)
		{
			return;
		}
		GeneralUtil.SetDailyBattleBtnIcon(this.m_quickModeObject, "Btn_2_battle");
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x000CB238 File Offset: 0x000C9438
	public void Update()
	{
		this.m_nextUpdateTime -= Time.deltaTime;
		if (this.m_nextUpdateTime <= 0f)
		{
			GeneralUtil.SetDailyBattleTime(this.m_quickModeObject, "Btn_2_battle");
			this.m_nextUpdateTime = HudDailyBattleButton.UPDATE_TIME;
		}
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000CB284 File Offset: 0x000C9484
	public void UpdateView()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
		if (gameObject == null)
		{
			return;
		}
		this.m_quickModeObject = GameObjectUtil.FindChildGameObject(gameObject, "1_Quick");
		if (this.m_quickModeObject == null)
		{
			return;
		}
		GeneralUtil.SetDailyBattleBtnIcon(this.m_quickModeObject, "Btn_2_battle");
	}

	// Token: 0x04001E74 RID: 7796
	private GameObject m_mainMenuObject;

	// Token: 0x04001E75 RID: 7797
	private GameObject m_quickModeObject;

	// Token: 0x04001E76 RID: 7798
	private static readonly float UPDATE_TIME = 1f;

	// Token: 0x04001E77 RID: 7799
	private float m_nextUpdateTime;
}
