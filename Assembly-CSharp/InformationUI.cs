using System;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000413 RID: 1043
public class InformationUI : MonoBehaviour
{
	// Token: 0x06001F4D RID: 8013 RVA: 0x000B9BA4 File Offset: 0x000B7DA4
	private void Start()
	{
		base.enabled = false;
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x000B9BB0 File Offset: 0x000B7DB0
	private void PlayAnimation(bool inAnim)
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component != null)
		{
			Direction playDirection = (!inAnim) ? Direction.Reverse : Direction.Forward;
			ActiveAnimation activeAnimation = ActiveAnimation.Play(component, "ui_daily_challenge_infomation_intro_Anim", playDirection);
			if (activeAnimation != null)
			{
				if (inAnim)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedInAnimationCallback), true);
				}
				else
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedOutAnimationCallback), true);
				}
			}
		}
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x000B9C38 File Offset: 0x000B7E38
	private void OnFinishedInAnimationCallback()
	{
		HudMenuUtility.SendUIPageStart();
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x000B9C40 File Offset: 0x000B7E40
	private void OnFinishedOutAnimationCallback()
	{
		HudMenuUtility.SendUIPageEnd();
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x000B9C48 File Offset: 0x000B7E48
	private void OnClickBackButton()
	{
		HudMenuUtility.SendMenuButtonClicked(ButtonInfoTable.ButtonType.INFOMATION_BACK, false);
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x000B9C54 File Offset: 0x000B7E54
	private void OnStartInformation()
	{
		if (this.m_newsSet != null)
		{
			ui_mm_news_page component = this.m_newsSet.GetComponent<ui_mm_news_page>();
			if (component != null)
			{
				component.StartInformation();
			}
		}
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x000B9C90 File Offset: 0x000B7E90
	private void OnEndInformation()
	{
		HudMenuUtility.SendMsgInformationDisplay();
		ServerInterface.NoticeInfo.SaveInformation();
		if (InformationImageManager.Instance != null)
		{
			InformationImageManager.Instance.ClearWinowImage();
		}
	}

	// Token: 0x04001C6B RID: 7275
	[SerializeField]
	public GameObject m_newsSet;
}
