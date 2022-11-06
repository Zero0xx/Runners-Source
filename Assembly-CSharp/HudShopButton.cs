using System;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class HudShopButton : MonoBehaviour
{
	// Token: 0x06002187 RID: 8583 RVA: 0x000C9DD8 File Offset: 0x000C7FD8
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x000C9DEC File Offset: 0x000C7FEC
	private void Initialize()
	{
		if (this.m_shoBtn != null)
		{
			return;
		}
		GameObject mainMenuCmnUIObject = HudMenuUtility.GetMainMenuCmnUIObject();
		if (mainMenuCmnUIObject != null)
		{
			this.m_shoBtn = GameObjectUtil.FindChildGameObject(mainMenuCmnUIObject, "Btn_shop");
		}
		this.SetBtn();
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x000C9E34 File Offset: 0x000C8034
	private void SetShopButton(bool flag)
	{
		if (this.m_shoBtn != null)
		{
			this.m_shoBtn.SetActive(flag && !this.m_forceDisable);
		}
		this.SetBtn();
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x000C9E78 File Offset: 0x000C8078
	public void OnEnableShopButton(bool enableFlag)
	{
		this.Initialize();
		this.SetShopButton(enableFlag);
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x000C9E88 File Offset: 0x000C8088
	public void OnForceDisableShopButton(bool disableFlag)
	{
		this.Initialize();
		this.m_forceDisable = disableFlag;
		this.SetShopButton(!disableFlag);
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x000C9EA4 File Offset: 0x000C80A4
	private void SetBtn()
	{
		if (this.m_shoBtn != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(this.m_shoBtn, "Btn_charge_rsring");
			if (gameObject != null)
			{
				gameObject.SetActive(ServerInterface.IsRSREnable());
			}
		}
	}

	// Token: 0x04001E56 RID: 7766
	private GameObject m_shoBtn;

	// Token: 0x04001E57 RID: 7767
	private bool m_forceDisable;
}
