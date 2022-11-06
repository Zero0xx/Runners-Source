using System;
using DataTable;
using Text;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class HudDailyChallenge : MonoBehaviour
{
	// Token: 0x0600212F RID: 8495 RVA: 0x000C7A04 File Offset: 0x000C5C04
	public void OnUpdateSaveDataDisplay()
	{
		this.UpdateChallengePanel();
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x000C7A0C File Offset: 0x000C5C0C
	private void UpdateChallengePanel()
	{
		int num = 0;
		if (SaveDataManager.Instance != null)
		{
			int id = SaveDataManager.Instance.PlayerData.DailyMission.id;
			MissionData missionData = MissionTable.GetMissionData(id);
			if (missionData != null && missionData.quota > 0)
			{
				double num2 = (double)SaveDataManager.Instance.PlayerData.DailyMission.progress / (double)missionData.quota * 100.0;
				if (num2 > 100.0)
				{
					num2 = 100.0;
				}
				num = (int)num2;
			}
		}
		GameObject mainMenuUIObject = HudMenuUtility.GetMainMenuUIObject();
		if (mainMenuUIObject == null)
		{
			return;
		}
		Transform transform = mainMenuUIObject.transform.FindChild("Anchor_9_BR/Btn_1_challenge");
		if (transform == null)
		{
			return;
		}
		GameObject gameObject = transform.gameObject;
		if (gameObject != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_player_main_lv");
			if (uilabel != null)
			{
				uilabel.text = TextUtility.GetCommonText("ChaoSet", "bonus_percent", "{BONUS}", num.ToString());
			}
		}
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x000C7B28 File Offset: 0x000C5D28
	private void Start()
	{
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x000C7B2C File Offset: 0x000C5D2C
	private void Update()
	{
	}
}
