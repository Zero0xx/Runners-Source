using System;
using UnityEngine;

// Token: 0x02000462 RID: 1122
public class HudMainMenuRankingButton
{
	// Token: 0x060021B6 RID: 8630 RVA: 0x000CAFFC File Offset: 0x000C91FC
	public void Intialize(GameObject mainMenuObject, bool isQuickMode)
	{
		if (mainMenuObject == null)
		{
			return;
		}
		this.m_mainMenuObject = mainMenuObject;
		this.m_isQuickMode = isQuickMode;
		if (isQuickMode)
		{
			this.m_buttonParentObject = null;
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
			if (gameObject == null)
			{
				return;
			}
			this.m_buttonParentObject = GameObjectUtil.FindChildGameObject(gameObject, "1_Quick");
			if (this.m_buttonParentObject == null)
			{
				return;
			}
		}
		else
		{
			this.m_buttonParentObject = null;
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(this.m_mainMenuObject, "Anchor_5_MC");
			if (gameObject2 == null)
			{
				return;
			}
			this.m_buttonParentObject = GameObjectUtil.FindChildGameObject(gameObject2, "0_Endless");
			if (this.m_buttonParentObject == null)
			{
				return;
			}
		}
		this.m_nextUpdateTime = HudMainMenuRankingButton.UPDATE_TIME;
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x000CB0CC File Offset: 0x000C92CC
	public void Update()
	{
		if (this.m_buttonParentObject == null)
		{
			return;
		}
		HudMainMenuRankingButton.State state = this.m_state;
		if (state != HudMainMenuRankingButton.State.INIT)
		{
			if (state == HudMainMenuRankingButton.State.UPDATE)
			{
				this.m_nextUpdateTime -= Time.deltaTime;
				if (this.m_nextUpdateTime <= 0f)
				{
					if (this.m_isQuickMode)
					{
						GeneralUtil.SetQuickRankingTime(this.m_buttonParentObject, "Btn_1_ranking");
					}
					else
					{
						GeneralUtil.SetEndlessRankingTime(this.m_buttonParentObject, "Btn_1_ranking");
					}
					this.m_nextUpdateTime = HudMainMenuRankingButton.UPDATE_TIME;
				}
			}
		}
		else
		{
			bool flag;
			if (this.m_isQuickMode)
			{
				flag = GeneralUtil.SetQuickRankingBtnIcon(this.m_buttonParentObject, "Btn_1_ranking");
			}
			else
			{
				flag = GeneralUtil.SetEndlessRankingBtnIcon(this.m_buttonParentObject, "Btn_1_ranking");
			}
			if (flag)
			{
				this.m_state = HudMainMenuRankingButton.State.UPDATE;
			}
		}
	}

	// Token: 0x04001E6A RID: 7786
	private GameObject m_mainMenuObject;

	// Token: 0x04001E6B RID: 7787
	private bool m_isQuickMode;

	// Token: 0x04001E6C RID: 7788
	private GameObject m_buttonParentObject;

	// Token: 0x04001E6D RID: 7789
	private static readonly float UPDATE_TIME = 60f;

	// Token: 0x04001E6E RID: 7790
	private float m_nextUpdateTime;

	// Token: 0x04001E6F RID: 7791
	private HudMainMenuRankingButton.State m_state;

	// Token: 0x02000463 RID: 1123
	private enum State
	{
		// Token: 0x04001E71 RID: 7793
		INIT,
		// Token: 0x04001E72 RID: 7794
		UPDATE,
		// Token: 0x04001E73 RID: 7795
		NUM
	}
}
