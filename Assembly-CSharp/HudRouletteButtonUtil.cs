using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
public class HudRouletteButtonUtil
{
	// Token: 0x06002181 RID: 8577 RVA: 0x000C9C58 File Offset: 0x000C7E58
	public static void SetSpecialEggIcon(GameObject eggObj)
	{
		if (eggObj != null)
		{
			bool active = false;
			if (RouletteManager.Instance != null && RouletteManager.Instance.specialEgg >= 10)
			{
				active = true;
			}
			eggObj.SetActive(active);
		}
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x000C9CA0 File Offset: 0x000C7EA0
	public static void SetFreeSpin(GameObject badgeSpinObj, UILabel uiLable, bool counterStop = false)
	{
		int num = 0;
		bool active = false;
		ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
		if (wheelOptions != null)
		{
			num = wheelOptions.m_numRemaining;
			if (num > 0)
			{
				active = true;
			}
			if (counterStop && num > 999)
			{
				num = 999;
			}
		}
		if (badgeSpinObj != null)
		{
			badgeSpinObj.SetActive(active);
		}
		if (uiLable != null)
		{
			uiLable.text = num.ToString();
		}
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x000C9D10 File Offset: 0x000C7F10
	public static void SetChaoFreeSpin(GameObject badgeSpinObj, UILabel uiLable, bool counterStop = false)
	{
		int num = 0;
		bool active = false;
		ServerChaoWheelOptions chaoWheelOptions = ServerInterface.ChaoWheelOptions;
		if (chaoWheelOptions != null)
		{
			num = chaoWheelOptions.NumRouletteToken;
			if (num > 0)
			{
				active = true;
			}
			if (counterStop && num > 999)
			{
				num = 999;
			}
		}
		if (badgeSpinObj != null)
		{
			badgeSpinObj.SetActive(active);
		}
		if (uiLable != null)
		{
			uiLable.text = num.ToString();
		}
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000C9D80 File Offset: 0x000C7F80
	public static void SetSaleIcon(GameObject iconObj)
	{
		if (iconObj != null)
		{
			bool active = HudMenuUtility.IsSale(Constants.Campaign.emType.ChaoRouletteCost);
			iconObj.SetActive(active);
		}
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x000C9DA8 File Offset: 0x000C7FA8
	public static void SetEventIcon(GameObject eventObj)
	{
		if (eventObj != null)
		{
			bool active = EventUtility.IsEnableRouletteUI();
			eventObj.SetActive(active);
		}
	}
}
