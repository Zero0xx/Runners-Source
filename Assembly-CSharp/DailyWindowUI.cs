using System;
using System.Collections;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class DailyWindowUI : WindowBase
{
	// Token: 0x06001E83 RID: 7811 RVA: 0x000B5044 File Offset: 0x000B3244
	private void Start()
	{
		base.enabled = false;
		this.m_isDisplay = false;
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x000B5054 File Offset: 0x000B3254
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x06001E85 RID: 7813 RVA: 0x000B505C File Offset: 0x000B325C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x000B5064 File Offset: 0x000B3264
	public void PlayStart()
	{
		base.gameObject.SetActive(true);
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "daily_window");
		if (gameObject != null)
		{
			gameObject.SetActive(true);
			Animation component = gameObject.GetComponent<Animation>();
			if (component != null)
			{
				ActiveAnimation.Play(component, "ui_cmn_window_Anim", Direction.Forward);
			}
			base.StartCoroutine(this.DisplayProgressBar());
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x000B50DC File Offset: 0x000B32DC
	private IEnumerator DisplayProgressBar()
	{
		yield return null;
		GameObject dailyChallengeObj = GameObjectUtil.FindChildGameObject(base.gameObject, "daily_challenge");
		if (dailyChallengeObj != null)
		{
			long progress = -1L;
			if (SaveDataManager.Instance != null && !this.m_isDisplay)
			{
				DailyMissionData nowData = SaveDataManager.Instance.PlayerData.DailyMission;
				DailyMissionData beforeData = SaveDataManager.Instance.PlayerData.BeforeDailyMissionData;
				if (nowData.date == beforeData.date && nowData.id == beforeData.id)
				{
					progress = beforeData.progress;
					this.m_isDisplay = true;
				}
			}
			dailyChallengeObj.SendMessage("OnStartDailyMissionInMileageMap", progress, SendMessageOptions.DontRequireReceiver);
		}
		yield break;
	}

	// Token: 0x06001E88 RID: 7816 RVA: 0x000B50F8 File Offset: 0x000B32F8
	private void OnClickNextButton()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "daily_challenge");
		if (gameObject != null)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			gameObject.SendMessage("OnClickNextButton", base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
		this.m_isClickClose = true;
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x000B514C File Offset: 0x000B334C
	public void OnClosedWindowAnim()
	{
		SoundManager.SeStop("sys_gauge", "SE");
		base.gameObject.SetActive(false);
		this.m_isEnd = true;
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x000B517C File Offset: 0x000B337C
	public override void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (this.m_isEnd)
		{
			return;
		}
		if (msg != null)
		{
			msg.StaySequence();
		}
		if (!this.m_isClickClose && !daily_challenge.isUpdateEffect)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_next");
			if (gameObject != null)
			{
				UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
				if (component != null)
				{
					component.SendMessage("OnClick");
				}
			}
		}
	}

	// Token: 0x04001BEB RID: 7147
	[SerializeField]
	public bool m_isDebug;

	// Token: 0x04001BEC RID: 7148
	public bool m_isClickClose;

	// Token: 0x04001BED RID: 7149
	public bool m_isEnd;

	// Token: 0x04001BEE RID: 7150
	private bool m_isDisplay;
}
