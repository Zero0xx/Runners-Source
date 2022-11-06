using System;
using System.Collections.Generic;

// Token: 0x02000370 RID: 880
public class HudContinueUtility
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x00098C78 File Offset: 0x00096E78
	public static int GetContinueCost()
	{
		int result = 1;
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerCampaignData continueCostCampaignData = HudContinueUtility.GetContinueCostCampaignData();
			if (continueCostCampaignData != null)
			{
				result = continueCostCampaignData.iContent;
			}
			else
			{
				List<ServerConsumedCostData> costList = ServerInterface.CostList;
				if (costList != null)
				{
					foreach (ServerConsumedCostData serverConsumedCostData in costList)
					{
						if (serverConsumedCostData != null)
						{
							if (serverConsumedCostData.consumedItemId == 950000)
							{
								result = serverConsumedCostData.numItem;
								break;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x00098D38 File Offset: 0x00096F38
	public static bool IsInContinueCostCampaign()
	{
		return HudContinueUtility.GetContinueCostCampaignData() != null;
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x00098D54 File Offset: 0x00096F54
	public static ServerCampaignData GetContinueCostCampaignData()
	{
		if (ServerInterface.CampaignState != null)
		{
			return ServerInterface.CampaignState.GetCampaignInSession(Constants.Campaign.emType.ContinueCost);
		}
		return null;
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x00098D7C File Offset: 0x00096F7C
	public static string GetContinueCostString()
	{
		return HudContinueUtility.GetContinueCost().ToString();
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x00098D98 File Offset: 0x00096F98
	public static int GetRedStarRingCount()
	{
		return (int)SaveDataManager.Instance.ItemData.RedRingCount;
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x00098DB8 File Offset: 0x00096FB8
	public static string GetRedStarRingCountString()
	{
		return HudContinueUtility.GetRedStarRingCount().ToString();
	}
}
