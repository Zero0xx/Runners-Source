using System;
using System.Collections;
using System.Collections.Generic;
using AnimationOrTween;
using Text;
using UnityEngine;

// Token: 0x020004EC RID: 1260
public class RankingFriendOptionWindow : MonoBehaviour
{
	// Token: 0x06002588 RID: 9608 RVA: 0x000E4104 File Offset: 0x000E2304
	private void Start()
	{
		GameObject parent = GameObjectUtil.FindChildGameObject(base.gameObject, "body");
		this.m_uiLabels[1] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(parent, "Lbl_body");
		this.m_uiLabels[0] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_caption");
		this.m_uiLabels[2] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_num_activefriend");
		this.m_uiLabels[3] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_page");
		this.m_uiLabels[4] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_0d");
		this.m_uiLabels[5] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_next");
		this.m_uiLabels[6] = GameObjectUtil.FindChildGameObjectComponent<UILabel>(base.gameObject, "Lbl_prev");
		this.m_uiLabels[5].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_page_next").text;
		this.m_uiLabels[6].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_page_back").text;
		this.m_sortOrderText[0] = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_sort_ascending").text;
		this.m_sortOrderText[1] = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_sort_descending").text;
		this.m_draggablePanel = GameObjectUtil.FindChildGameObjectComponent<UIDraggablePanel>(base.gameObject, "ScrollView");
		this.m_buttonPage = GameObjectUtil.FindChildGameObject(base.gameObject, "btn_page");
		this.m_panel = base.GetComponent<UIPanel>();
		if (this.m_panel != null)
		{
			this.m_panel.alpha = 0f;
		}
		this.m_scrollViewPanel = GameObjectUtil.FindChildGameObjectComponent<UIPanel>(base.gameObject, "ScrollView");
		if (this.m_scrollViewPanel != null)
		{
			this.m_scrollViewPanel.alpha = 0f;
		}
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x000E42D4 File Offset: 0x000E24D4
	public IEnumerator SetUp()
	{
		this.m_isAnimationEnd = false;
		yield return null;
		yield return null;
		this.m_panel.alpha = 1f;
		this.m_scrollViewPanel.alpha = 1f;
		if (base.animation != null)
		{
			ActiveAnimation m_activeAnim = ActiveAnimation.Play(base.animation, Direction.Forward);
			EventDelegate.Add(m_activeAnim.onFinished, new EventDelegate.Callback(this.OnFinishedActiveAnimation), true);
		}
		this.m_showConfirmWindow = false;
		this.m_page = 0;
		this.m_sortOrder = RankingFriendOptionWindow.SortOrder.Ascending;
		this.m_uiLabels[0].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_caption").text;
		this.m_uiLabels[1].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_body").text;
		this.m_buttonPage.SetActive(true);
		this.m_friendList.Clear();
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null && socialInterface.IsLoggedIn)
		{
			foreach (SocialUserData user in socialInterface.AllFriendList)
			{
				this.m_friendList.Add(user);
			}
		}
		this.m_pageMax = (this.m_friendList.Count - 1) / this.CHOSE_FRIEND_MAX;
		this.m_chosenFriend = new List<SocialUserData>(socialInterface.FriendList);
		this.m_simpleScrolls = GameObjectUtil.FindChildGameObjectsComponents<RankingSimpleScroll>(base.gameObject, "ui_rankingsimple_scroll(Clone)");
		this.ScrollListUpdate();
		this.Sort();
		this.EntryBackKeyCallBack();
		yield break;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x000E42F0 File Offset: 0x000E24F0
	private void Update()
	{
		if (this.m_draggablePanel != null && this.m_isAnimationEnd)
		{
			for (int i = 0; i < this.VISIBLE_IMAGE_MAX; i++)
			{
				float num = (float)(this.m_loadedFriendImageNum - this.VISIBLE_IMAGE_MAX) / (float)this.m_activeScrollCount;
				if (this.m_draggablePanel.verticalScrollBar.value <= num)
				{
					return;
				}
				this.NextImageLoad();
			}
		}
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x000E436C File Offset: 0x000E256C
	private void NextImageLoad()
	{
		for (int i = 0; i < this.LOAD_FRIEND_IMAGE_NUM; i++)
		{
			if (this.m_loadedFriendImageNum < this.m_simpleScrolls.Count)
			{
				if (!this.m_simpleScrolls[this.m_loadedFriendImageNum].gameObject.activeSelf)
				{
					return;
				}
				this.m_simpleScrolls[this.m_loadedFriendImageNum].LoadImage();
				this.m_loadedFriendImageNum++;
			}
		}
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x000E43EC File Offset: 0x000E25EC
	private void OnFinishedActiveAnimation()
	{
		this.m_isAnimationEnd = true;
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x000E43F8 File Offset: 0x000E25F8
	private void LabelUpdate()
	{
		this.m_uiLabels[3].text = 1 + this.m_page + "/" + (1 + this.m_pageMax);
		this.m_uiLabels[2].text = this.m_chosenFriend.Count + "/" + this.CHOSE_FRIEND_MAX;
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x000E4468 File Offset: 0x000E2668
	public void ScrollListUpdate()
	{
		this.m_loadedFriendImageNum = 0;
		this.m_activeScrollCount = 0;
		List<SocialUserData> list = (!this.m_showConfirmWindow) ? this.m_friendList : this.m_confirmList;
		int num = this.m_page * this.m_simpleScrolls.Count;
		foreach (RankingSimpleScroll rankingSimpleScroll in this.m_simpleScrolls)
		{
			if (list.Count > num)
			{
				rankingSimpleScroll.gameObject.SetActive(true);
				rankingSimpleScroll.SetUserData(list[num]);
				rankingSimpleScroll.m_toggle.value = false;
				foreach (SocialUserData socialUserData in this.m_chosenFriend)
				{
					if (socialUserData.Id == list[num].Id)
					{
						rankingSimpleScroll.m_toggle.value = true;
						break;
					}
				}
				this.m_activeScrollCount++;
			}
			else
			{
				rankingSimpleScroll.gameObject.SetActive(false);
			}
			num++;
		}
		this.m_draggablePanel.ResetPosition();
		this.LabelUpdate();
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x000E45EC File Offset: 0x000E27EC
	private void SetUpConfirmWindow()
	{
		this.m_showConfirmWindow = true;
		this.m_page = 0;
		this.m_confirmList = new List<SocialUserData>(this.m_chosenFriend);
		this.Sort();
		this.m_uiLabels[0].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_confirmation_caption").text;
		this.m_uiLabels[1].text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ui_friend_select_confirmation_body").text;
		this.m_buttonPage.SetActive(false);
		if (base.animation != null)
		{
			this.m_isAnimationEnd = false;
			this.m_activeAnim = ActiveAnimation.Play(base.animation, Direction.Forward);
			EventDelegate.Add(this.m_activeAnim.onFinished, new EventDelegate.Callback(this.OnFinishedActiveAnimation), true);
		}
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x000E46B8 File Offset: 0x000E28B8
	public void ChooseFriend(SocialUserData user, UIToggle toggle)
	{
		if (user != null)
		{
			if (toggle.value)
			{
				if (this.CHOSE_FRIEND_MAX > this.m_chosenFriend.Count)
				{
					SoundManager.SePlay("sys_menu_decide", "SE");
					this.m_chosenFriend.Add(user);
				}
				else
				{
					toggle.value = false;
				}
			}
			else
			{
				SoundManager.SePlay("sys_window_close", "SE");
				this.m_chosenFriend.RemoveAll((SocialUserData chooseFriend) => chooseFriend.Id == user.Id);
			}
		}
		this.LabelUpdate();
		base.StartCoroutine("UpdateDraggablePanel");
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x000E476C File Offset: 0x000E296C
	public IEnumerator UpdateDraggablePanel()
	{
		yield return null;
		yield return null;
		this.m_draggablePanel.SendMessage("OnVerticalBar");
		yield break;
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x000E4788 File Offset: 0x000E2988
	public void PageUp()
	{
		if (this.m_page > 0)
		{
			this.m_page--;
			this.ScrollListUpdate();
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x000E47C8 File Offset: 0x000E29C8
	public void PageDown()
	{
		if (this.m_page < this.m_pageMax)
		{
			this.m_page++;
			this.ScrollListUpdate();
			SoundManager.SePlay("sys_page_skip", "SE");
		}
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000E4800 File Offset: 0x000E2A00
	public void OnClickOkButton()
	{
		SoundManager.SePlay("sys_menu_decide", "SE");
		if (!this.m_showConfirmWindow)
		{
			this.SetUpConfirmWindow();
			return;
		}
		SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
		if (socialInterface != null)
		{
			socialInterface.FriendList = new List<SocialUserData>(this.m_chosenFriend);
			FacebookUtil.SaveFriendIdList(new List<SocialUserData>(this.m_chosenFriend));
		}
		this.m_showConfirmWindow = false;
		if (SingletonGameObject<RankingManager>.Instance != null)
		{
			SingletonGameObject<RankingManager>.Instance.Reset(RankingUtil.RankingMode.ENDLESS, RankingUtil.RankingRankerType.FRIEND);
			SingletonGameObject<RankingManager>.Instance.Reset(RankingUtil.RankingMode.QUICK, RankingUtil.RankingRankerType.FRIEND);
			if (EventManager.Instance.Type != EventManager.EventType.SPECIAL_STAGE || (SpecialStageWindow.Instance != null && !SpecialStageWindow.Instance.enabledAnchorObjects))
			{
				RankingUI rankingUI = GameObjectUtil.FindGameObjectComponent<RankingUI>("ui_mm_ranking_page(Clone)");
				if (rankingUI != null)
				{
					rankingUI.SendMessage("OnClickFriendOptionOk");
				}
			}
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000E48E8 File Offset: 0x000E2AE8
	public void OnClickNoButton()
	{
		SoundManager.SePlay("sys_window_close", "SE");
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x000E48FC File Offset: 0x000E2AFC
	public void OnFinishedAnimationCallback()
	{
		this.RemoveBackKeyCallBack();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000E4910 File Offset: 0x000E2B10
	public void OnPressSortName()
	{
		RankingFriendOptionWindow.SortOrder sortOrder = this.m_sortOrder;
		if (sortOrder != RankingFriendOptionWindow.SortOrder.Ascending)
		{
			if (sortOrder == RankingFriendOptionWindow.SortOrder.Descending)
			{
				this.m_sortOrder = RankingFriendOptionWindow.SortOrder.Ascending;
			}
		}
		else
		{
			this.m_sortOrder = RankingFriendOptionWindow.SortOrder.Descending;
		}
		this.Sort();
		SoundManager.SePlay("sys_menu_decide", "SE");
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x000E4964 File Offset: 0x000E2B64
	public void Sort()
	{
		this.m_uiLabels[4].text = this.m_sortOrderText[(int)this.m_sortOrder];
		List<SocialUserData> list = (!this.m_showConfirmWindow) ? this.m_friendList : this.m_confirmList;
		list.Sort(delegate(SocialUserData a, SocialUserData b)
		{
			RankingFriendOptionWindow.SortOrder sortOrder = this.m_sortOrder;
			if (sortOrder == RankingFriendOptionWindow.SortOrder.Ascending)
			{
				return string.Compare(a.Name, b.Name, true);
			}
			if (sortOrder != RankingFriendOptionWindow.SortOrder.Descending)
			{
				return string.Compare(a.Name, b.Name, true);
			}
			return string.Compare(b.Name, a.Name, true);
		});
		this.ScrollListUpdate();
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x000E49C0 File Offset: 0x000E2BC0
	private void EntryBackKeyCallBack()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x000E49D0 File Offset: 0x000E2BD0
	private void RemoveBackKeyCallBack()
	{
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x000E49E0 File Offset: 0x000E2BE0
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (msg != null)
		{
			msg.StaySequence();
		}
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "Btn_close");
		if (gameObject != null)
		{
			UIButtonMessage component = gameObject.GetComponent<UIButtonMessage>();
			if (component != null)
			{
				component.SendMessage("OnClick");
			}
		}
	}

	// Token: 0x040021C5 RID: 8645
	private UILabel[] m_uiLabels = new UILabel[7];

	// Token: 0x040021C6 RID: 8646
	private UIDraggablePanel m_draggablePanel;

	// Token: 0x040021C7 RID: 8647
	private GameObject m_buttonPage;

	// Token: 0x040021C8 RID: 8648
	private UIPanel m_panel;

	// Token: 0x040021C9 RID: 8649
	private UIPanel m_scrollViewPanel;

	// Token: 0x040021CA RID: 8650
	private List<RankingSimpleScroll> m_simpleScrolls = new List<RankingSimpleScroll>();

	// Token: 0x040021CB RID: 8651
	private List<SocialUserData> m_friendList = new List<SocialUserData>();

	// Token: 0x040021CC RID: 8652
	private List<SocialUserData> m_chosenFriend = new List<SocialUserData>();

	// Token: 0x040021CD RID: 8653
	private List<SocialUserData> m_confirmList = new List<SocialUserData>();

	// Token: 0x040021CE RID: 8654
	private readonly int CHOSE_FRIEND_MAX = 50;

	// Token: 0x040021CF RID: 8655
	private readonly int LOAD_FRIEND_IMAGE_NUM = 1;

	// Token: 0x040021D0 RID: 8656
	private readonly int VISIBLE_IMAGE_MAX = 6;

	// Token: 0x040021D1 RID: 8657
	private int m_loadedFriendImageNum;

	// Token: 0x040021D2 RID: 8658
	private int m_activeScrollCount;

	// Token: 0x040021D3 RID: 8659
	private int m_page;

	// Token: 0x040021D4 RID: 8660
	private int m_pageMax;

	// Token: 0x040021D5 RID: 8661
	private ActiveAnimation m_activeAnim;

	// Token: 0x040021D6 RID: 8662
	private bool m_isAnimationEnd;

	// Token: 0x040021D7 RID: 8663
	private bool m_showConfirmWindow;

	// Token: 0x040021D8 RID: 8664
	private string[] m_sortOrderText = new string[2];

	// Token: 0x040021D9 RID: 8665
	private RankingFriendOptionWindow.SortOrder m_sortOrder;

	// Token: 0x020004ED RID: 1261
	private enum LabelId
	{
		// Token: 0x040021DB RID: 8667
		CAPTION,
		// Token: 0x040021DC RID: 8668
		BODY,
		// Token: 0x040021DD RID: 8669
		ACTIVE_FRIEND,
		// Token: 0x040021DE RID: 8670
		PAGE,
		// Token: 0x040021DF RID: 8671
		SORT_ORDER,
		// Token: 0x040021E0 RID: 8672
		NEXT,
		// Token: 0x040021E1 RID: 8673
		BACK,
		// Token: 0x040021E2 RID: 8674
		NUM
	}

	// Token: 0x020004EE RID: 1262
	private enum SortOrder
	{
		// Token: 0x040021E4 RID: 8676
		Ascending,
		// Token: 0x040021E5 RID: 8677
		Descending,
		// Token: 0x040021E6 RID: 8678
		NUM
	}
}
