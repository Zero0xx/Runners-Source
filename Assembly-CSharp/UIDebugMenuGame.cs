using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class UIDebugMenuGame : UIDebugMenuTask
{
	// Token: 0x06000CD9 RID: 3289 RVA: 0x00049B2C File Offset: 0x00047D2C
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		GameObject original = GameObjectUtil.FindChildGameObject(base.gameObject, "stage_object");
		for (int i = 0; i < 12; i++)
		{
			string name = this.MenuObjName[i];
			GameObject gameObject = UnityEngine.Object.Instantiate(original, Vector3.zero, Quaternion.identity) as GameObject;
			if (!(gameObject == null))
			{
				gameObject.name = name;
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
				this.m_mainCharacter = base.gameObject.AddComponent<UIDebugMenuTextField>();
				this.m_mainCharacter.Setup(new Rect(400f, 100f, 150f, 50f), "MainChara", string.Empty);
				this.m_subCharacter = base.gameObject.AddComponent<UIDebugMenuTextField>();
				this.m_subCharacter.Setup(new Rect(600f, 100f, 150f, 50f), "SubChara", string.Empty);
				if (CharacterDataNameInfo.Instance == null)
				{
					GameObject gameObject2 = new GameObject("ResourceSceneLoader");
					ResourceSceneLoader resourceSceneLoader = gameObject2.AddComponent<ResourceSceneLoader>();
					resourceSceneLoader.AddLoad("CharacterDataNameInfo", true, false);
				}
			}
		}
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00049CB8 File Offset: 0x00047EB8
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
		if (this.m_mainCharacter != null)
		{
			this.m_mainCharacter.SetActive(false);
		}
		if (this.m_subCharacter != null)
		{
			this.m_subCharacter.SetActive(false);
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00049D3C File Offset: 0x00047F3C
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
		if (this.m_mainCharacter != null)
		{
			this.m_mainCharacter.SetActive(true);
		}
		if (this.m_subCharacter != null)
		{
			this.m_subCharacter.SetActive(true);
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00049DC0 File Offset: 0x00047FC0
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else
		{
			if (CharacterDataNameInfo.Instance != null && SaveDataManager.Instance != null)
			{
				CharacterDataNameInfo.Info dataByName = CharacterDataNameInfo.Instance.GetDataByName(this.m_mainCharacter.text);
				if (dataByName != null)
				{
					SaveDataManager.Instance.PlayerData.MainChara = dataByName.m_ID;
				}
				dataByName = CharacterDataNameInfo.Instance.GetDataByName(this.m_subCharacter.text);
				if (dataByName != null)
				{
					SaveDataManager.Instance.PlayerData.SubChara = dataByName.m_ID;
				}
			}
			StageInfo stageInfo = GameObjectUtil.FindGameObjectComponent<StageInfo>("StageInfo");
			if (stageInfo != null)
			{
				if (name.Contains("w01"))
				{
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(1);
					stageInfo.BossType = BossType.MAP1;
				}
				if (name.Contains("w02"))
				{
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(2);
					stageInfo.BossType = BossType.MAP2;
				}
				if (name.Contains("w03"))
				{
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(3);
					stageInfo.BossType = BossType.MAP3;
				}
				if (name.Contains("w04"))
				{
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(4);
				}
				if (name.Contains("w05"))
				{
					EventManager.Instance.Id = 100020000;
					EventManager.Instance.SetDebugParameter();
					EventManager.Instance.EventStage = true;
					stageInfo.EventStage = true;
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(5);
				}
				if (name.Contains("w07"))
				{
					EventManager.Instance.Id = 100010000;
					EventManager.Instance.SetDebugParameter();
					EventManager.Instance.EventStage = true;
					stageInfo.EventStage = true;
					stageInfo.SelectedStageName = StageInfo.GetStageNameByIndex(7);
				}
				if (name.Contains("afternoon") || name.Contains("boss"))
				{
					stageInfo.TenseType = TenseType.AFTERNOON;
				}
				else if (name.Contains("night"))
				{
					stageInfo.TenseType = TenseType.NIGHT;
				}
				if (name.Contains("boss"))
				{
					stageInfo.BossStage = true;
				}
				else
				{
					stageInfo.BossStage = false;
				}
				stageInfo.FromTitle = true;
				Application.LoadLevel("s_playingstage");
			}
		}
	}

	// Token: 0x04000A30 RID: 2608
	private static int BUTTON_W = 30;

	// Token: 0x04000A31 RID: 2609
	private List<string> MenuObjName = new List<string>
	{
		"w01_afternoon",
		"w01_night",
		"w01_boss",
		"w02_afternoon",
		"w02_night",
		"w02_boss",
		"w03_afternoon",
		"w03_night",
		"w03_boss",
		"w04_afternoon",
		"w05_afternoon",
		"w07_afternoon"
	};

	// Token: 0x04000A32 RID: 2610
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 180f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(400f, 180f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(600f, 180f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(200f, 220f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(400f, 220f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(600f, 220f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(200f, 260f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(400f, 260f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(600f, 260f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(200f, 300f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(400f, 300f, 150f, (float)UIDebugMenuGame.BUTTON_W),
		new Rect(600f, 300f, 150f, (float)UIDebugMenuGame.BUTTON_W)
	};

	// Token: 0x04000A33 RID: 2611
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000A34 RID: 2612
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A35 RID: 2613
	private UIDebugMenuTextField m_mainCharacter;

	// Token: 0x04000A36 RID: 2614
	private UIDebugMenuTextField m_subCharacter;

	// Token: 0x020001C8 RID: 456
	private enum MenuType
	{
		// Token: 0x04000A38 RID: 2616
		W01_AFTERNOON,
		// Token: 0x04000A39 RID: 2617
		W01_NIGTH,
		// Token: 0x04000A3A RID: 2618
		W01_BOSS,
		// Token: 0x04000A3B RID: 2619
		W02_AFTERNOON,
		// Token: 0x04000A3C RID: 2620
		W02_NIGTH,
		// Token: 0x04000A3D RID: 2621
		W02_BOSS,
		// Token: 0x04000A3E RID: 2622
		W03_AFTERNOON,
		// Token: 0x04000A3F RID: 2623
		W03_NIGTH,
		// Token: 0x04000A40 RID: 2624
		W03_BOSS,
		// Token: 0x04000A41 RID: 2625
		W04_AFTERNOON,
		// Token: 0x04000A42 RID: 2626
		W05_AFTERNOON,
		// Token: 0x04000A43 RID: 2627
		W07_AFTERNOON,
		// Token: 0x04000A44 RID: 2628
		NUM
	}
}
