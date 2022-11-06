using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class LoginBonusWindowUI : WindowBase
{
	// Token: 0x06002093 RID: 8339 RVA: 0x000C3924 File Offset: 0x000C1B24
	private void Start()
	{
		base.enabled = false;
		this.m_isEnd = false;
		this.m_isOpened = false;
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x000C393C File Offset: 0x000C1B3C
	private void OnDestroy()
	{
		base.Destroy();
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06002095 RID: 8341 RVA: 0x000C3944 File Offset: 0x000C1B44
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x000C394C File Offset: 0x000C1B4C
	private IEnumerator Setup(LoginBonusWindowUI.LoginBonusType type)
	{
		this.m_type = type;
		ServerInterface serverInterface = ServerInterface.LoggedInServerInterface;
		if (serverInterface != null)
		{
			this.m_days = new List<LoginDayObject>();
			GameObject loginBonusObj = GameObjectUtil.FindChildGameObject(base.gameObject, "login_bonus");
			this.m_BonusData = ServerInterface.LoginBonusData;
			if (this.m_BonusData != null && loginBonusObj != null)
			{
				int nowBonusCount = 0;
				if (type == LoginBonusWindowUI.LoginBonusType.NORMAL)
				{
					this.m_RewardList = this.m_BonusData.m_loginBonusRewardList;
					nowBonusCount = this.m_BonusData.m_loginBonusState.m_numBonus;
				}
				else
				{
					this.m_RewardList = this.m_BonusData.m_firstLoginBonusRewardList;
					nowBonusCount = this.m_BonusData.m_loginBonusState.m_numLogin;
				}
				if (this.m_RewardList != null)
				{
					int dayCount = this.m_RewardList.Count;
					for (int i = 0; i < dayCount; i++)
					{
						string objName = "ui_login_day" + (i + 1);
						if (i == dayCount - 1)
						{
							objName = "ui_login_big";
						}
						GameObject loginDayObj = GameObjectUtil.FindChildGameObject(loginBonusObj, objName);
						if (loginDayObj != null)
						{
							LoginDayObject dayObj = new LoginDayObject(loginDayObj, i + 1);
							ServerLoginBonusReward reward = this.m_RewardList[i];
							if (reward != null)
							{
								if (reward.m_itemList != null)
								{
									dayObj.SetItem(reward.m_itemList[0].m_itemId);
									dayObj.count = reward.m_itemList[0].m_num;
								}
								dayObj.SetAlready(i < nowBonusCount);
							}
							this.m_days.Add(dayObj);
						}
					}
					UILabel Label_days = GameObjectUtil.FindChildGameObjectComponent<UILabel>(loginBonusObj, "Lbl_days");
					if (Label_days != null)
					{
						int nowDayCount = this.m_BonusData.CalcTodayCount();
						int totalDays = this.m_BonusData.getTotalDays();
						if (this.m_type == LoginBonusWindowUI.LoginBonusType.FIRST)
						{
							nowDayCount = this.m_BonusData.m_loginBonusState.m_numLogin;
							totalDays = dayCount;
						}
						if (nowDayCount > -1)
						{
							int lastDayCount = totalDays - nowDayCount;
							if (lastDayCount < 0)
							{
								lastDayCount = 0;
							}
							if (this.m_type == LoginBonusWindowUI.LoginBonusType.FIRST)
							{
								Label_days.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "LoginBonus", "count").text, new Dictionary<string, string>
								{
									{
										"{COUNT}",
										lastDayCount.ToString()
									}
								});
							}
							else
							{
								Label_days.text = TextUtility.Replaces(TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "LoginBonus", "day").text, new Dictionary<string, string>
								{
									{
										"{DAY}",
										lastDayCount.ToString()
									}
								});
							}
						}
					}
					UILabel Label_BonusItems = GameObjectUtil.FindChildGameObjectComponent<UILabel>(loginBonusObj, "Lbl_loginbonus_feature");
					if (Label_BonusItems != null && this.m_BonusData.m_loginBonusState != null)
					{
						if (type == LoginBonusWindowUI.LoginBonusType.NORMAL)
						{
							this.m_TodayReward = this.m_BonusData.m_lastBonusReward;
						}
						else
						{
							this.m_TodayReward = this.m_BonusData.m_firstLastBonusReward;
						}
						string rewardText = string.Empty;
						string lineBreakText = "\n";
						if (this.m_TodayReward != null)
						{
							int itemCount = this.m_TodayReward.m_itemList.Count;
							for (int j = 0; j < itemCount; j++)
							{
								if (this.m_TodayReward.m_itemList[j] != null)
								{
									string itemName = new ServerItem((ServerItem.Id)this.m_TodayReward.m_itemList[j].m_itemId).serverItemName;
									int numItemCount = this.m_TodayReward.m_itemList[j].m_num;
									if (!string.IsNullOrEmpty(itemName))
									{
										string text = rewardText;
										rewardText = string.Concat(new string[]
										{
											text,
											itemName,
											"×",
											numItemCount.ToString(),
											lineBreakText
										});
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(rewardText))
						{
							Label_BonusItems.text = rewardText;
						}
					}
				}
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x000C3978 File Offset: 0x000C1B78
	public void PlayStart(LoginBonusWindowUI.LoginBonusType type)
	{
		base.enabled = true;
		this.m_TodayReward = null;
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "login_window");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
			base.StartCoroutine(this.Setup(type));
			if (this.m_RewardList == null || this.m_TodayReward == null)
			{
				this.OnClosedWindowAnim();
				global::Debug.Log("LoginBonusWindowUI::PlayStart >> NotLoginBonusReward! = " + type.ToString());
				return;
			}
			gameObject.SetActive(true);
			this.m_animation = gameObject.GetComponent<Animation>();
			if (this.m_animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Forward);
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnOpenWindowAnim), true);
			}
			UIPlayAnimation uiplayAnimation = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(gameObject, "Btn_next");
			if (uiplayAnimation != null)
			{
				uiplayAnimation.enabled = false;
			}
			UIPlayAnimation uiplayAnimation2 = GameObjectUtil.FindChildGameObjectComponent<UIPlayAnimation>(gameObject, "blinder");
			if (uiplayAnimation2 != null)
			{
				uiplayAnimation2.enabled = false;
			}
		}
		this.m_isEnd = false;
		this.m_isClickClose = false;
		this.m_isOpened = false;
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000C3AA0 File Offset: 0x000C1CA0
	private void Update()
	{
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x000C3AA4 File Offset: 0x000C1CA4
	private void OnClickNextButton()
	{
		if (!this.m_isClickClose)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnClosedWindowAnim), true);
			}
			this.m_isClickClose = true;
		}
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x000C3B0C File Offset: 0x000C1D0C
	public void OnOpenWindowAnim()
	{
		if (!this.m_isOpened)
		{
			this.m_isOpened = true;
			if (this.m_days != null && this.m_BonusData != null && this.m_BonusData.m_loginBonusState != null)
			{
				int num;
				int count;
				if (this.m_type == LoginBonusWindowUI.LoginBonusType.NORMAL)
				{
					num = this.m_BonusData.m_loginBonusState.m_numBonus - 1;
					count = this.m_BonusData.m_loginBonusRewardList.Count;
				}
				else
				{
					num = this.m_BonusData.m_loginBonusState.m_numLogin - 1;
					count = this.m_BonusData.m_firstLoginBonusRewardList.Count;
				}
				if (count < num)
				{
					num = count - 1;
				}
				if (num > -1)
				{
					this.m_days[num].PlayGetAnimation();
				}
			}
		}
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x000C3BD0 File Offset: 0x000C1DD0
	public void OnClosedWindowAnim()
	{
		base.gameObject.SetActive(false);
		base.enabled = false;
		this.m_isEnd = true;
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x000C3BEC File Offset: 0x000C1DEC
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
		if (!this.m_isClickClose)
		{
			SoundManager.SePlay("sys_menu_decide", "SE");
			ActiveAnimation activeAnimation = ActiveAnimation.Play(this.m_animation, "ui_cmn_window_Anim", Direction.Reverse);
			if (activeAnimation != null)
			{
				EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnClosedWindowAnim), true);
			}
			this.m_isClickClose = true;
		}
	}

	// Token: 0x04001D26 RID: 7462
	private bool m_isClickClose;

	// Token: 0x04001D27 RID: 7463
	private bool m_isEnd;

	// Token: 0x04001D28 RID: 7464
	private bool m_isOpened;

	// Token: 0x04001D29 RID: 7465
	private Animation m_animation;

	// Token: 0x04001D2A RID: 7466
	private List<LoginDayObject> m_days;

	// Token: 0x04001D2B RID: 7467
	private ServerLoginBonusData m_BonusData;

	// Token: 0x04001D2C RID: 7468
	private List<ServerLoginBonusReward> m_RewardList;

	// Token: 0x04001D2D RID: 7469
	private ServerLoginBonusReward m_TodayReward;

	// Token: 0x04001D2E RID: 7470
	private LoginBonusWindowUI.LoginBonusType m_type;

	// Token: 0x02000431 RID: 1073
	public enum LoginBonusType
	{
		// Token: 0x04001D30 RID: 7472
		NORMAL,
		// Token: 0x04001D31 RID: 7473
		FIRST,
		// Token: 0x04001D32 RID: 7474
		MAX
	}
}
