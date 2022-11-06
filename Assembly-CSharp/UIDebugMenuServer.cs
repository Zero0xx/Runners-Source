using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class UIDebugMenuServer : UIDebugMenuTask
{
	// Token: 0x06000D01 RID: 3329 RVA: 0x0004B6D0 File Offset: 0x000498D0
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 12; i++)
		{
			string name = this.MenuObjName[i];
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(gameObject == null))
			{
				string childName = this.MenuObjName[i];
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
				base.AddChild(childName, gameObject);
			}
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0004B7A0 File Offset: 0x000499A0
	protected override void OnTransitionTo()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(false);
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0004B7E8 File Offset: 0x000499E8
	protected override void OnTransitionFrom()
	{
		if (this.m_buttonList != null)
		{
			this.m_buttonList.SetActive(true);
		}
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0004B830 File Offset: 0x00049A30
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface != null)
			{
				loggedInServerInterface.RequestServerRetrievePlayerState(base.gameObject);
			}
			base.TransitionToParent();
		}
		else if (name == "getMigrationPassword")
		{
			ServerInterface loggedInServerInterface2 = ServerInterface.LoggedInServerInterface;
			if (loggedInServerInterface2 != null)
			{
				loggedInServerInterface2.RequestServerGetMigrationPassword(null, base.gameObject);
			}
		}
		else
		{
			base.TransitionToChild(name);
		}
	}

	// Token: 0x04000A7E RID: 2686
	private List<string> MenuObjName = new List<string>
	{
		"upgradeChao",
		"upGradeCharacter",
		"upPoint",
		"getMigrationPassword",
		"updateDailyMission",
		"addMessage",
		"addOpeMessage",
		"updateMileageMapData",
		"updateMileageMapDataProduction",
		"ringExchange",
		"forceDrawRaidboss",
		"updateUserData"
	};

	// Token: 0x04000A7F RID: 2687
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(300f, 200f, 150f, 50f),
		new Rect(100f, 300f, 150f, 50f),
		new Rect(300f, 300f, 150f, 50f),
		new Rect(100f, 400f, 150f, 50f),
		new Rect(300f, 400f, 150f, 50f),
		new Rect(500f, 200f, 150f, 50f),
		new Rect(500f, 300f, 150f, 50f),
		new Rect(500f, 400f, 150f, 50f),
		new Rect(700f, 400f, 220f, 50f),
		new Rect(900f, 500f, 150f, 50f),
		new Rect(100f, 500f, 150f, 50f),
		new Rect(500f, 500f, 150f, 50f)
	};

	// Token: 0x04000A80 RID: 2688
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000A81 RID: 2689
	private UIDebugMenuButton m_backButton;

	// Token: 0x020001D6 RID: 470
	private enum MenuType
	{
		// Token: 0x04000A83 RID: 2691
		UPGRADE_CHAO,
		// Token: 0x04000A84 RID: 2692
		UPGRADE_CHARACTER,
		// Token: 0x04000A85 RID: 2693
		UP_POINT,
		// Token: 0x04000A86 RID: 2694
		GET_MIGRATION_PASSWORD,
		// Token: 0x04000A87 RID: 2695
		UPDATE_DAILY_MISSION,
		// Token: 0x04000A88 RID: 2696
		ADD_MESSAGE,
		// Token: 0x04000A89 RID: 2697
		ADD_OPE_MESSAGE,
		// Token: 0x04000A8A RID: 2698
		UP_MILEAGE_MAP_DATA,
		// Token: 0x04000A8B RID: 2699
		UP_MILEAGEMAPPRODUCTION,
		// Token: 0x04000A8C RID: 2700
		RING_EXCHANGE,
		// Token: 0x04000A8D RID: 2701
		FORCE_DRAW_RAIDBOSS,
		// Token: 0x04000A8E RID: 2702
		UPDATE_USER_DATA,
		// Token: 0x04000A8F RID: 2703
		NUM
	}
}
