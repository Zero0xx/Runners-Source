using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class UIDebugMenuUpdateMileageData : UIDebugMenuTask
{
	// Token: 0x06000D37 RID: 3383 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 6; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i]);
			this.m_TextFields[i].text = this.textFieldDefault[i];
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0004D7BC File Offset: 0x0004B9BC
	protected override void OnTransitionTo()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(false);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(false);
		}
		for (int i = 0; i < 6; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0004D83C File Offset: 0x0004BA3C
	protected override void OnTransitionFrom()
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetActive(true);
		}
		if (this.m_decideButton != null)
		{
			this.m_decideButton.SetActive(true);
		}
		for (int i = 0; i < 6; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0004D8BC File Offset: 0x0004BABC
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			for (int i = 0; i < 6; i++)
			{
				UIDebugMenuTextField uidebugMenuTextField = this.m_TextFields[i];
				if (!(uidebugMenuTextField == null))
				{
					int num;
					if (!int.TryParse(uidebugMenuTextField.text, out num))
					{
						return;
					}
				}
			}
			this.m_updMileageData = new NetDebugUpdMileageData(new ServerMileageMapState
			{
				m_episode = int.Parse(this.m_TextFields[0].text),
				m_chapter = int.Parse(this.m_TextFields[1].text),
				m_point = int.Parse(this.m_TextFields[2].text),
				m_stageTotalScore = (long)int.Parse(this.m_TextFields[3].text),
				m_numBossAttack = int.Parse(this.m_TextFields[4].text),
				m_stageMaxScore = (long)int.Parse(this.m_TextFields[5].text)
			});
			this.m_updMileageData.Request();
		}
	}

	// Token: 0x04000AF0 RID: 2800
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000AF1 RID: 2801
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000AF2 RID: 2802
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[6];

	// Token: 0x04000AF3 RID: 2803
	private string[] DefaultTextList = new string[]
	{
		"Story/話",
		"Chapter/章",
		"Point/ポイント",
		"Score On Map.",
		"Boss' Remain Life",
		"Target Score In Chapter"
	};

	// Token: 0x04000AF4 RID: 2804
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(500f, 200f, 250f, 50f),
		new Rect(200f, 300f, 250f, 50f),
		new Rect(500f, 300f, 250f, 50f),
		new Rect(200f, 400f, 250f, 50f),
		new Rect(500f, 400f, 250f, 50f)
	};

	// Token: 0x04000AF5 RID: 2805
	private string[] textFieldDefault = new string[]
	{
		"2",
		"1",
		"0",
		"0",
		"3",
		"0"
	};

	// Token: 0x04000AF6 RID: 2806
	private NetDebugUpdMileageData m_updMileageData;

	// Token: 0x020001EB RID: 491
	private enum TextType
	{
		// Token: 0x04000AF8 RID: 2808
		EPISODE,
		// Token: 0x04000AF9 RID: 2809
		CHAPTER,
		// Token: 0x04000AFA RID: 2810
		POINT,
		// Token: 0x04000AFB RID: 2811
		MAP_DISTANCE,
		// Token: 0x04000AFC RID: 2812
		NUM_BOSS_ATTACK,
		// Token: 0x04000AFD RID: 2813
		STAGE_DISTANCE,
		// Token: 0x04000AFE RID: 2814
		NUM
	}
}
