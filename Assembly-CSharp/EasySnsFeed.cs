using System;
using System.Diagnostics;
using Text;
using UnityEngine;

// Token: 0x020009EF RID: 2543
public class EasySnsFeed
{
	// Token: 0x0600431F RID: 17183 RVA: 0x0015C914 File Offset: 0x0015AB14
	public EasySnsFeed(GameObject gameObject, string anchorPath, string caption, string text, ui_mm_mileage_page mileage_page = null)
	{
		this.Init(gameObject, anchorPath, caption, text, mileage_page);
	}

	// Token: 0x06004320 RID: 17184 RVA: 0x0015C934 File Offset: 0x0015AB34
	public EasySnsFeed(GameObject gameObject, string anchorPath)
	{
		this.m_isLoginOnly = true;
		this.Init(gameObject, anchorPath, null, null, null);
	}

	// Token: 0x06004321 RID: 17185 RVA: 0x0015C95C File Offset: 0x0015AB5C
	private void Init(GameObject gameObject, string anchorPath, string caption = null, string text = null, ui_mm_mileage_page mileage_page = null)
	{
		this.m_gameObject = gameObject;
		this.m_caption = caption;
		this.m_text = text;
		this.m_mileage_page = mileage_page;
		this.m_easySnsFeedMonoBehaviour = gameObject.GetComponent<EasySnsFeedMonoBehaviour>();
		if (this.m_easySnsFeedMonoBehaviour == null)
		{
			this.m_easySnsFeedMonoBehaviour = gameObject.AddComponent<EasySnsFeedMonoBehaviour>();
		}
		this.m_easySnsFeedMonoBehaviour.Init();
		this.m_snsLoginGameObject = GameObject.Find("EasySnsFeed.SnsLogin");
		if (this.m_snsLoginGameObject == null)
		{
			this.m_snsLoginGameObject = new GameObject("EasySnsFeed.SnsLogin");
		}
		if (this.m_snsLoginGameObject != null)
		{
			this.m_snsLogin = this.m_snsLoginGameObject.GetComponent<SettingPartsSnsLogin>();
			if (this.m_snsLogin == null)
			{
				this.m_snsLogin = this.m_snsLoginGameObject.AddComponent<SettingPartsSnsLogin>();
			}
		}
		if (this.m_snsLogin != null)
		{
			this.m_snsLogin.Setup(anchorPath);
			HudMenuUtility.SetConnectAlertSimpleUI(true);
		}
		else
		{
			this.m_result = EasySnsFeed.Result.FAILED;
		}
	}

	// Token: 0x06004322 RID: 17186 RVA: 0x0015CA60 File Offset: 0x0015AC60
	public EasySnsFeed.Result Update()
	{
		if (this.m_result == EasySnsFeed.Result.NONE)
		{
			switch (this.m_phase)
			{
			case EasySnsFeed.Phase.START:
			case EasySnsFeed.Phase.WAIT_PRELOGIN:
				this.m_phase = EasySnsFeed.Phase.LOGIN;
				break;
			case EasySnsFeed.Phase.LOGIN:
				HudMenuUtility.SetConnectAlertSimpleUI(false);
				this.m_snsLogin.SetCancelWindowUseFlag(!this.m_isLoginOnly);
				this.m_snsLogin.PlayStart();
				this.m_phase = EasySnsFeed.Phase.WAIT_LOGIN;
				break;
			case EasySnsFeed.Phase.WAIT_LOGIN:
				if (this.m_snsLogin.IsEnd)
				{
					this.m_phase = ((!this.m_snsLogin.IsCalceled) ? ((!this.m_isLoginOnly) ? EasySnsFeed.Phase.FEED : EasySnsFeed.Phase.COMPLETED) : EasySnsFeed.Phase.FAILED);
				}
				break;
			case EasySnsFeed.Phase.FEED:
			{
				SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
				if (socialInterface != null)
				{
					socialInterface.Feed(this.m_caption, this.m_text, this.m_gameObject);
				}
				this.m_phase = ((!(socialInterface != null)) ? EasySnsFeed.Phase.FAILED : EasySnsFeed.Phase.WAIT_FEED);
				break;
			}
			case EasySnsFeed.Phase.WAIT_FEED:
			{
				bool? isFeeded = this.m_easySnsFeedMonoBehaviour.m_isFeeded;
				if (isFeeded != null)
				{
					this.m_phase = ((!(this.m_easySnsFeedMonoBehaviour.m_isFeeded == true)) ? EasySnsFeed.Phase.FAILED : EasySnsFeed.Phase.COMPLETED);
				}
				break;
			}
			case EasySnsFeed.Phase.GET_INCENTIVE:
			{
				ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
				if (loggedInServerInterface != null)
				{
					loggedInServerInterface.RequestServerGetFacebookIncentive(2, 0, this.m_gameObject);
				}
				this.m_phase = ((!(loggedInServerInterface != null)) ? EasySnsFeed.Phase.FAILED : EasySnsFeed.Phase.WAIT_GET_INCENTIVE);
				break;
			}
			case EasySnsFeed.Phase.WAIT_GET_INCENTIVE:
				if (this.m_easySnsFeedMonoBehaviour.m_feedIncentiveList != null)
				{
					this.m_phase = EasySnsFeed.Phase.VIEW_INCENTIVE;
				}
				break;
			case EasySnsFeed.Phase.VIEW_INCENTIVE:
				if (this.m_easySnsFeedMonoBehaviour.m_feedIncentiveList.Count > 0)
				{
					ServerPresentState serverPresentState = this.m_easySnsFeedMonoBehaviour.m_feedIncentiveList[0];
					this.m_easySnsFeedMonoBehaviour.m_feedIncentiveList.RemoveAt(0);
					ItemGetWindow itemGetWindow = ItemGetWindowUtil.GetItemGetWindow();
					if (itemGetWindow != null)
					{
						itemGetWindow.Create(new ItemGetWindow.CInfo
						{
							caption = TextUtility.GetCommonText("SnsFeed", "gw_feed_incentive_caption"),
							serverItemId = serverPresentState.m_itemId,
							imageCount = TextUtility.GetCommonText("SnsFeed", "gw_feed_incentive_text", "{COUNT}", HudUtility.GetFormatNumString<int>(serverPresentState.m_numItem))
						});
					}
					HudMenuUtility.SendMsgUpdateSaveDataDisplay();
					this.m_phase = EasySnsFeed.Phase.WAIT_VIEW_INCENTIVE;
				}
				else
				{
					HudMenuUtility.SendMsgUpdateSaveDataDisplay();
					this.m_phase = EasySnsFeed.Phase.COMPLETED;
				}
				break;
			case EasySnsFeed.Phase.WAIT_VIEW_INCENTIVE:
			{
				ItemGetWindow itemGetWindow2 = ItemGetWindowUtil.GetItemGetWindow();
				if (itemGetWindow2 != null && itemGetWindow2.IsEnd)
				{
					itemGetWindow2.Reset();
					this.m_phase = EasySnsFeed.Phase.VIEW_INCENTIVE;
				}
				break;
			}
			case EasySnsFeed.Phase.COMPLETED:
				this.m_result = EasySnsFeed.Result.COMPLETED;
				break;
			case EasySnsFeed.Phase.FAILED:
				this.m_result = EasySnsFeed.Result.FAILED;
				break;
			}
		}
		return this.m_result;
	}

	// Token: 0x06004323 RID: 17187 RVA: 0x0015CD58 File Offset: 0x0015AF58
	[Conditional("DEBUG_INFO")]
	public static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x06004324 RID: 17188 RVA: 0x0015CD6C File Offset: 0x0015AF6C
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x040038E4 RID: 14564
	private GameObject m_snsLoginGameObject;

	// Token: 0x040038E5 RID: 14565
	private SettingPartsSnsLogin m_snsLogin;

	// Token: 0x040038E6 RID: 14566
	private EasySnsFeed.Phase m_phase;

	// Token: 0x040038E7 RID: 14567
	private EasySnsFeed.Result m_result;

	// Token: 0x040038E8 RID: 14568
	private GameObject m_gameObject;

	// Token: 0x040038E9 RID: 14569
	private string m_caption;

	// Token: 0x040038EA RID: 14570
	private string m_text;

	// Token: 0x040038EB RID: 14571
	private ui_mm_mileage_page m_mileage_page;

	// Token: 0x040038EC RID: 14572
	private EasySnsFeedMonoBehaviour m_easySnsFeedMonoBehaviour;

	// Token: 0x040038ED RID: 14573
	private bool m_isLoginOnly;

	// Token: 0x020009F0 RID: 2544
	private enum Phase
	{
		// Token: 0x040038EF RID: 14575
		START,
		// Token: 0x040038F0 RID: 14576
		WAIT_PRELOGIN,
		// Token: 0x040038F1 RID: 14577
		LOGIN,
		// Token: 0x040038F2 RID: 14578
		WAIT_LOGIN,
		// Token: 0x040038F3 RID: 14579
		FEED,
		// Token: 0x040038F4 RID: 14580
		WAIT_FEED,
		// Token: 0x040038F5 RID: 14581
		GET_INCENTIVE,
		// Token: 0x040038F6 RID: 14582
		WAIT_GET_INCENTIVE,
		// Token: 0x040038F7 RID: 14583
		VIEW_INCENTIVE,
		// Token: 0x040038F8 RID: 14584
		WAIT_VIEW_INCENTIVE,
		// Token: 0x040038F9 RID: 14585
		COMPLETED,
		// Token: 0x040038FA RID: 14586
		FAILED
	}

	// Token: 0x020009F1 RID: 2545
	public enum Result
	{
		// Token: 0x040038FC RID: 14588
		NONE,
		// Token: 0x040038FD RID: 14589
		COMPLETED,
		// Token: 0x040038FE RID: 14590
		FAILED
	}
}
