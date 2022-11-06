using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class MileageBonusInfo : MonoBehaviour
{
	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06001557 RID: 5463 RVA: 0x00076534 File Offset: 0x00074734
	// (set) Token: 0x06001558 RID: 5464 RVA: 0x0007653C File Offset: 0x0007473C
	public MileageBonusData BonusData
	{
		get
		{
			return this.m_bonus_data;
		}
		set
		{
			this.m_bonus_data = value;
		}
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x00076548 File Offset: 0x00074748
	public static MileageBonusInfo CreateMileageBonusInfo()
	{
		MileageBonusInfo mileageBonusInfo = GameObjectUtil.FindGameObjectComponent<MileageBonusInfo>("MileageBonusInfo");
		if (mileageBonusInfo)
		{
			UnityEngine.Object.Destroy(mileageBonusInfo.gameObject);
		}
		MileageBonusInfo mileageBonusInfo2 = null;
		GameObject gameObject = new GameObject("MileageBonusInfo");
		if (gameObject != null)
		{
			mileageBonusInfo2 = gameObject.AddComponent<MileageBonusInfo>();
			mileageBonusInfo2.ResetData();
		}
		return mileageBonusInfo2;
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000765A0 File Offset: 0x000747A0
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000765AC File Offset: 0x000747AC
	public void ResetData()
	{
		this.m_bonus_data.type = MileageBonus.NULL_EFFECT;
		this.m_bonus_data.value = 0f;
	}

	// Token: 0x040012BF RID: 4799
	public const string COMPONENT_NAME = "MileageBonusInfo";

	// Token: 0x040012C0 RID: 4800
	private MileageBonusData m_bonus_data;
}
