using System;
using System.Collections;
using AnimationOrTween;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class ButtonEventAnimation : MonoBehaviour
{
	// Token: 0x060020AF RID: 8367 RVA: 0x000C4030 File Offset: 0x000C2230
	public void Initialize()
	{
		this.m_menu_anim_obj = HudMenuUtility.GetMenuAnimUIObject();
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x000C4040 File Offset: 0x000C2240
	public void PageOutAnimation(ButtonInfoTable.PageType currentPageType, ButtonInfoTable.PageType nextPageType, ButtonEventAnimation.AnimationEndCallback animEndCallback)
	{
		this.m_currentPageType = currentPageType;
		this.m_outAnimEndCallback = animEndCallback;
		ButtonInfoTable.AnimInfo pageAnimInfo = this.m_info_table.GetPageAnimInfo(currentPageType);
		if (pageAnimInfo == null)
		{
			this.OnFinishedOutAnimationCallback();
			return;
		}
		if (nextPageType == ButtonInfoTable.PageType.STAGE)
		{
			this.SetOutAnimation(new ButtonInfoTable.AnimInfo("ItemSet_3_UI", "ui_itemset_3_outro_Anim"), false);
		}
		else
		{
			this.SetOutAnimation(pageAnimInfo, true);
		}
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x000C40A0 File Offset: 0x000C22A0
	public void PageInAnimation(ButtonInfoTable.PageType nextPageType, ButtonEventAnimation.AnimationEndCallback animEndCallback)
	{
		this.m_inAnimEndCallback = animEndCallback;
		ButtonInfoTable.AnimInfo pageAnimInfo = this.m_info_table.GetPageAnimInfo(nextPageType);
		base.StartCoroutine(this.SetInAnimationCoroutine(pageAnimInfo, false));
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x000C40D0 File Offset: 0x000C22D0
	private void SetOutAnimation(ButtonInfoTable.AnimInfo animInfo, bool reverseFlag)
	{
		if (animInfo != null && animInfo.animName != null)
		{
			Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_menu_anim_obj, animInfo.targetName);
			if (animation != null)
			{
				if (animInfo.animName == "ui_mm_Anim")
				{
					reverseFlag = !reverseFlag;
				}
				Direction playDirection = (!reverseFlag) ? Direction.Forward : Direction.Reverse;
				ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, animInfo.animName, playDirection);
				if (activeAnimation != null)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinishedOutAnimationCallback), true);
				}
			}
			else
			{
				this.OnFinishedOutAnimationCallback();
			}
		}
		else if (animInfo.targetName == "RouletteTopUI")
		{
			base.StartCoroutine(this.WaitRouletteClose());
		}
		else
		{
			this.OnFinishedOutAnimationCallback();
		}
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x000C41A4 File Offset: 0x000C23A4
	private IEnumerator SetInAnimationCoroutine(ButtonInfoTable.AnimInfo animInfo, bool reverseFlag)
	{
		this.InitInAnimation(animInfo);
		yield return null;
		yield return null;
		if (animInfo != null)
		{
			yield return base.StartCoroutine(this.DelayPlayAnimation(animInfo, reverseFlag));
		}
		else
		{
			this.OnFinishedInAnimationCallback();
		}
		yield break;
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x000C41DC File Offset: 0x000C23DC
	private void InitInAnimation(ButtonInfoTable.AnimInfo animInfo)
	{
		if (animInfo != null)
		{
			bool flag = false;
			Animation animation = GameObjectUtil.FindChildGameObjectComponent<Animation>(this.m_menu_anim_obj, animInfo.targetName);
			if (animation != null)
			{
				if (animInfo.animName == "ui_mm_Anim")
				{
					flag = !flag;
				}
				Direction playDirection = (!flag) ? Direction.Forward : Direction.Reverse;
				ActiveAnimation.Play(animation, animInfo.animName, playDirection);
				animation.Stop(animInfo.animName);
			}
		}
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x000C4254 File Offset: 0x000C2454
	public IEnumerator DelayPlayAnimation(ButtonInfoTable.AnimInfo animInfo, bool reverseFlag)
	{
		int waite_frame = 2;
		while (waite_frame > 0)
		{
			waite_frame--;
			yield return null;
		}
		if (animInfo != null)
		{
			GameObject obj = GameObjectUtil.FindChildGameObject(this.m_menu_anim_obj, animInfo.targetName);
			if (obj != null)
			{
				ShopUI shop = obj.GetComponent<ShopUI>();
				if (shop != null)
				{
					while (!shop.IsInitShop)
					{
						yield return null;
					}
				}
				if (obj.name == "RouletteTopUI")
				{
					RouletteManager.Instance.gameObject.SetActive(true);
					while (!RouletteManager.IsRouletteEnabled())
					{
						yield return null;
					}
				}
				if (obj.name == "DailyChallengeInformationUI")
				{
					daily_challenge dailyChallenge = GameObjectUtil.FindChildGameObjectComponent<daily_challenge>(obj, "daily_challenge");
					if (dailyChallenge != null)
					{
						while (!dailyChallenge.IsEndSetup)
						{
							yield return null;
						}
					}
				}
				ChaoSetUI chao = obj.GetComponent<ChaoSetUI>();
				if (chao != null)
				{
					while (!chao.IsEndSetup)
					{
						yield return null;
					}
				}
				ItemSetMenu item = obj.GetComponent<ItemSetMenu>();
				if (item != null)
				{
					while (!item.IsEndSetup)
					{
						yield return null;
					}
				}
				MenuPlayerSet player = obj.GetComponent<MenuPlayerSet>();
				if (player != null)
				{
					while (!player.SetUpped)
					{
						yield return null;
					}
				}
				OptionUI option = obj.GetComponent<OptionUI>();
				if (option != null)
				{
					while (!option.IsEndSetup)
					{
						yield return null;
					}
				}
				PresentBoxUI presentBox = obj.GetComponent<PresentBoxUI>();
				if (presentBox != null)
				{
					while (!presentBox.IsEndSetup)
					{
						yield return null;
					}
				}
				DailyInfo dailyInfo = obj.GetComponent<DailyInfo>();
				if (dailyInfo != null)
				{
					yield return null;
				}
				if (animInfo.animName != null)
				{
					Animation anim = obj.GetComponent<Animation>();
					if (anim != null)
					{
						if (animInfo.animName == "ui_mm_Anim")
						{
							reverseFlag = !reverseFlag;
						}
						Direction dire = (!reverseFlag) ? Direction.Forward : Direction.Reverse;
						ActiveAnimation acviteAnim = ActiveAnimation.Play(anim, animInfo.animName, dire);
						if (acviteAnim != null)
						{
							EventDelegate.Add(acviteAnim.onFinished, new EventDelegate.Callback(this.OnFinishedInAnimationCallback), true);
						}
					}
				}
				else
				{
					this.OnFinishedInAnimationCallback();
				}
			}
		}
		yield break;
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x000C428C File Offset: 0x000C248C
	public IEnumerator WaitRouletteClose()
	{
		while (!RouletteManager.IsRouletteClose())
		{
			yield return null;
		}
		this.OnFinishedOutAnimationCallback();
		yield break;
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x000C42A8 File Offset: 0x000C24A8
	private void OnFinishedOutAnimationCallback()
	{
		if (this.m_outAnimEndCallback != null)
		{
			this.m_outAnimEndCallback();
			this.m_outAnimEndCallback = null;
		}
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000C42C8 File Offset: 0x000C24C8
	private void OnFinishedInAnimationCallback()
	{
		if (this.m_inAnimEndCallback != null)
		{
			this.m_inAnimEndCallback();
			this.m_inAnimEndCallback = null;
		}
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000C42E8 File Offset: 0x000C24E8
	private void Start()
	{
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x000C42EC File Offset: 0x000C24EC
	private void Update()
	{
	}

	// Token: 0x04001D3A RID: 7482
	private GameObject m_menu_anim_obj;

	// Token: 0x04001D3B RID: 7483
	private ButtonInfoTable m_info_table = new ButtonInfoTable();

	// Token: 0x04001D3C RID: 7484
	private ButtonEventAnimation.AnimationEndCallback m_inAnimEndCallback;

	// Token: 0x04001D3D RID: 7485
	private ButtonEventAnimation.AnimationEndCallback m_outAnimEndCallback;

	// Token: 0x04001D3E RID: 7486
	private ButtonInfoTable.PageType m_currentPageType;

	// Token: 0x02000A8E RID: 2702
	// (Invoke) Token: 0x06004872 RID: 18546
	public delegate void AnimationEndCallback();
}
