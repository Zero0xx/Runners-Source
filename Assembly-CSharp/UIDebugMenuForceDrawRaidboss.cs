using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class UIDebugMenuForceDrawRaidboss : UIDebugMenuTask
{
	// Token: 0x06000CE8 RID: 3304 RVA: 0x0004A658 File Offset: 0x00048858
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_decideButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_decideButton.Setup(new Rect(200f, 450f, 150f, 50f), "Decide", base.gameObject);
		for (int i = 0; i < 2; i++)
		{
			this.m_TextFields[i] = base.gameObject.AddComponent<UIDebugMenuTextField>();
			this.m_TextFields[i].Setup(this.RectList[i], this.DefaultTextList[i]);
		}
		this.m_TextFields[0].text = UIDebugMenuServerDefine.DefaultRaidbossId;
		this.m_textBox = base.gameObject.AddComponent<UIDebugMenuTextBox>();
		this.m_textBox.Setup(new Rect(500f, 100f, 400f, 500f), string.Empty);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0004A778 File Offset: 0x00048978
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
		for (int i = 0; i < 2; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(false);
			}
		}
		if (this.m_textBox != null)
		{
			this.m_textBox.SetActive(false);
		}
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0004A814 File Offset: 0x00048A14
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
		for (int i = 0; i < 2; i++)
		{
			if (!(this.m_TextFields[i] == null))
			{
				this.m_TextFields[i].SetActive(true);
			}
		}
		if (this.m_textBox != null)
		{
			this.m_textBox.SetActive(true);
		}
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0004A8B0 File Offset: 0x00048AB0
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "Decide")
		{
			for (int i = 0; i < 2; i++)
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
			this.m_networkRequest = new NetDebugForceDrawRaidboss(int.Parse(this.m_TextFields[0].text), long.Parse(this.m_TextFields[1].text));
			base.StartCoroutine(this.NetworkRequest(this.m_networkRequest, new UIDebugMenuForceDrawRaidboss.NetworkRequestSuccessCallback(this.ForceDrawRaidbossEndCallback), new UIDebugMenuForceDrawRaidboss.NetworkRequestFailedCallback(this.NetworkFailedCallback)));
		}
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0004A984 File Offset: 0x00048B84
	private IEnumerator NetworkRequest(NetBase request, UIDebugMenuForceDrawRaidboss.NetworkRequestSuccessCallback successCallback, UIDebugMenuForceDrawRaidboss.NetworkRequestFailedCallback failedCallback)
	{
		request.Request();
		while (request.IsExecuting())
		{
			yield return null;
		}
		if (request.IsSucceeded())
		{
			if (successCallback != null)
			{
				successCallback();
			}
		}
		else if (failedCallback != null)
		{
			failedCallback(request.resultStCd);
		}
		yield break;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0004A9C4 File Offset: 0x00048BC4
	private void ForceDrawRaidbossEndCallback()
	{
		this.m_networkRequest = new NetServerGetEventUserRaidBossList(int.Parse(this.m_TextFields[0].text));
		base.StartCoroutine(this.NetworkRequest(this.m_networkRequest, new UIDebugMenuForceDrawRaidboss.NetworkRequestSuccessCallback(this.GetEventUserRaidBossListEndCallback), new UIDebugMenuForceDrawRaidboss.NetworkRequestFailedCallback(this.NetworkFailedCallback)));
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0004AA1C File Offset: 0x00048C1C
	private void GetEventUserRaidBossListEndCallback()
	{
		NetServerGetEventUserRaidBossList netServerGetEventUserRaidBossList = this.m_networkRequest as NetServerGetEventUserRaidBossList;
		if (netServerGetEventUserRaidBossList == null)
		{
			return;
		}
		List<ServerEventRaidBossState> userRaidBossList = netServerGetEventUserRaidBossList.UserRaidBossList;
		if (userRaidBossList == null)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (ServerEventRaidBossState serverEventRaidBossState in userRaidBossList)
		{
			if (serverEventRaidBossState != null)
			{
				stringBuilder.Append("BossId: " + serverEventRaidBossState.Id.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  Level: " + serverEventRaidBossState.Level.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  Rarity: " + serverEventRaidBossState.Rarity.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  HitPoint: " + serverEventRaidBossState.HitPoint.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  MaxHitPoint: " + serverEventRaidBossState.MaxHitPoint.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  Status: " + serverEventRaidBossState.GetStatusType().ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  EscapeAt: " + serverEventRaidBossState.EscapeAt.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  EncounterName: " + serverEventRaidBossState.EncounterName);
				stringBuilder.AppendLine();
				stringBuilder.Append("  Encounter: " + serverEventRaidBossState.Encounter.ToString());
				stringBuilder.AppendLine();
				stringBuilder.Append("  Crowded: " + serverEventRaidBossState.Crowded.ToString());
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
			}
		}
		this.m_textBox.text = stringBuilder.ToString();
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0004AC40 File Offset: 0x00048E40
	private void NetworkFailedCallback(ServerInterface.StatusCode statusCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Error!!!!!!!!!");
		stringBuilder.AppendLine();
		stringBuilder.Append("StatusCode = " + statusCode.ToString());
		stringBuilder.AppendLine();
		this.m_textBox.text = stringBuilder.ToString();
	}

	// Token: 0x04000A58 RID: 2648
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A59 RID: 2649
	private UIDebugMenuButton m_decideButton;

	// Token: 0x04000A5A RID: 2650
	private UIDebugMenuTextField[] m_TextFields = new UIDebugMenuTextField[2];

	// Token: 0x04000A5B RID: 2651
	private UIDebugMenuTextBox m_textBox;

	// Token: 0x04000A5C RID: 2652
	private string[] DefaultTextList = new string[]
	{
		"イベントID",
		"スコア"
	};

	// Token: 0x04000A5D RID: 2653
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 250f, 50f),
		new Rect(200f, 300f, 250f, 50f)
	};

	// Token: 0x04000A5E RID: 2654
	private NetBase m_networkRequest;

	// Token: 0x020001CE RID: 462
	private enum TextType
	{
		// Token: 0x04000A60 RID: 2656
		EVENTID,
		// Token: 0x04000A61 RID: 2657
		SCORE,
		// Token: 0x04000A62 RID: 2658
		NUM
	}

	// Token: 0x02000A7A RID: 2682
	// (Invoke) Token: 0x06004822 RID: 18466
	private delegate void NetworkRequestSuccessCallback();

	// Token: 0x02000A7B RID: 2683
	// (Invoke) Token: 0x06004826 RID: 18470
	private delegate void NetworkRequestFailedCallback(ServerInterface.StatusCode statusCode);
}
