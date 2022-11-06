using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000521 RID: 1313
public class window_odds : WindowBase
{
	// Token: 0x0600289E RID: 10398 RVA: 0x000FB5D0 File Offset: 0x000F97D0
	public void Init()
	{
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x000FB608 File Offset: 0x000F9808
	public void Open(List<string[]> oddsList, string note)
	{
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, false);
		}
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			Animation component = gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation.Play(component, Direction.Forward);
			}
		}
		this.m_oddsList = oddsList;
		this.m_note = note;
		base.StartCoroutine(this.OpenCoroutine());
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x000FB690 File Offset: 0x000F9890
	public void Open(ServerPrizeState prize, ServerWheelOptionsData data)
	{
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, false);
		}
		GameObject gameObject = base.gameObject.transform.parent.gameObject;
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			Animation component = gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation.Play(component, Direction.Forward);
			}
		}
		this.m_oddsList = prize.GetItemOdds(data);
		this.m_note = prize.GetPrizeText(data);
		base.StartCoroutine(this.OpenCoroutine());
		RouletteManager.OpenRouletteWindow();
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x000FB728 File Offset: 0x000F9928
	private IEnumerator OpenCoroutine()
	{
		while (!base.gameObject.activeInHierarchy)
		{
			yield return null;
		}
		yield return null;
		SoundManager.SePlay("sys_window_open", "SE");
		if (this.m_oddsList != null)
		{
			this.m_oddsItemStorage.maxItemCount = (this.m_oddsItemStorage.maxRows = this.m_oddsList.Count);
		}
		else
		{
			this.m_oddsItemStorage.maxItemCount = (this.m_oddsItemStorage.maxRows = 0);
		}
		this.m_oddsItemStorage.Restart();
		ui_roulette_window_odds_scroll[] ui_roulette_window_odds_scrolls = this.m_oddsItemStorage.GetComponentsInChildren<ui_roulette_window_odds_scroll>(true);
		if (this.m_oddsList != null)
		{
			for (int i = 0; i < this.m_oddsItemStorage.maxItemCount; i++)
			{
				ui_roulette_window_odds_scrolls[i].UpdateView(this.m_oddsList[i][0], this.m_oddsList[i][1]);
			}
		}
		this.m_noteLabel.text = this.m_note;
		yield break;
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x000FB744 File Offset: 0x000F9944
	private void OnClickCloseButton()
	{
		UIEffectManager instance = UIEffectManager.Instance;
		if (instance != null)
		{
			instance.SetActiveEffect(HudMenuUtility.EffectPriority.Menu, true);
		}
		SoundManager.SePlay("sys_window_close", "SE");
		RouletteManager.CloseRouletteWindow();
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x000FB784 File Offset: 0x000F9984
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (base.gameObject.activeSelf)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(base.gameObject, "Btn_close");
			if (uibuttonMessage != null)
			{
				uibuttonMessage.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x0400240F RID: 9231
	[SerializeField]
	private UIRectItemStorage m_oddsItemStorage;

	// Token: 0x04002410 RID: 9232
	[SerializeField]
	private UILabel m_noteLabel;

	// Token: 0x04002411 RID: 9233
	private List<string[]> m_oddsList;

	// Token: 0x04002412 RID: 9234
	private string m_note;
}
