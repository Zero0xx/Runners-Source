using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class UIDebugMenuFacebook : UIDebugMenuTask
{
	// Token: 0x06000CD3 RID: 3283 RVA: 0x0004948C File Offset: 0x0004768C
	protected override void OnStartFromTask()
	{
		this.m_backButton = base.gameObject.AddComponent<UIDebugMenuButton>();
		this.m_backButton.Setup(new Rect(200f, 100f, 150f, 50f), "Back", base.gameObject);
		this.m_buttonList = base.gameObject.AddComponent<UIDebugMenuButtonList>();
		for (int i = 0; i < 5; i++)
		{
			string name = this.MenuObjName[i];
			GameObject x = GameObjectUtil.FindChildGameObject(base.gameObject, name);
			if (!(x == null))
			{
				this.m_buttonList.Add(this.RectList, this.MenuObjName, base.gameObject);
			}
		}
		this.m_testUserCountField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_testUserCountField.Setup(new Rect(200f, 370f, 350f, 50f), "作成するテストユーザー数");
		this.m_testUserCountField.text = "2";
		this.m_masterUserIdField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_masterUserIdField.Setup(new Rect(200f, 450f, 350f, 50f), "全ユーザーと友達にしたいユーザーID");
		this.m_masterUserIdField.text = string.Empty;
		this.m_masterUserAccessTokenField = base.gameObject.AddComponent<UIDebugMenuTextField>();
		this.m_masterUserAccessTokenField.Setup(new Rect(200f, 520f, 350f, 50f), "全ユーザーと友達にしたいユーザーのアクセストークン");
		this.m_masterUserAccessTokenField.text = string.Empty;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x00049624 File Offset: 0x00047824
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
		if (this.m_testUserCountField != null)
		{
			this.m_testUserCountField.SetActive(false);
		}
		if (this.m_masterUserIdField != null)
		{
			this.m_masterUserIdField.SetActive(false);
		}
		if (this.m_masterUserAccessTokenField != null)
		{
			this.m_masterUserAccessTokenField.SetActive(false);
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x000496C4 File Offset: 0x000478C4
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
		if (this.m_testUserCountField != null)
		{
			this.m_testUserCountField.SetActive(true);
		}
		if (this.m_masterUserIdField != null)
		{
			this.m_masterUserIdField.SetActive(true);
		}
		if (this.m_masterUserAccessTokenField != null)
		{
			this.m_masterUserAccessTokenField.SetActive(true);
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00049764 File Offset: 0x00047964
	private void OnClicked(string name)
	{
		if (name == "Back")
		{
			base.TransitionToParent();
		}
		else if (name == "CreateTestUser")
		{
			if (this.m_testUserCountField != null)
			{
				int createCount = int.Parse(this.m_testUserCountField.text);
				if (this.m_testUserCreate == null)
				{
					this.m_testUserCreate = base.gameObject.AddComponent<FacebookTestUserCreater>();
				}
				this.m_testUserCreate.Create(createCount);
			}
		}
		else if (name == "CreateMasterUser")
		{
			if (this.m_masterUserIdField != null && this.m_masterUserAccessTokenField != null)
			{
				if (this.m_masterUserCreate == null)
				{
					this.m_masterUserCreate = base.gameObject.AddComponent<FacebookMasterUserCreater>();
				}
				string text = this.m_masterUserIdField.text;
				string text2 = this.m_masterUserAccessTokenField.text;
				this.m_masterUserCreate.AttachFriend(text, text2);
			}
		}
		else
		{
			SocialInterface socialInterface = GameObjectUtil.FindGameObjectComponent<SocialInterface>("SocialInterface");
			if (socialInterface == null)
			{
				return;
			}
			if (name.Contains("Feed"))
			{
				socialInterface.Feed("デバッグ投稿", "デバッグ投稿です", base.gameObject);
			}
			else if (name.Contains("DeleteCustomData"))
			{
				socialInterface.Login(base.gameObject);
			}
			else if (name.Contains("Logout"))
			{
				socialInterface.Logout();
			}
		}
	}

	// Token: 0x04000A20 RID: 2592
	private List<string> MenuObjName = new List<string>
	{
		"Feed",
		"DeleteCustomData",
		"Logout",
		"CreateTestUser",
		"CreateMasterUser"
	};

	// Token: 0x04000A21 RID: 2593
	private List<Rect> RectList = new List<Rect>
	{
		new Rect(200f, 200f, 150f, 50f),
		new Rect(400f, 200f, 150f, 50f),
		new Rect(600f, 200f, 150f, 50f),
		new Rect(200f, 300f, 150f, 50f),
		new Rect(200f, 570f, 150f, 50f)
	};

	// Token: 0x04000A22 RID: 2594
	private UIDebugMenuButtonList m_buttonList;

	// Token: 0x04000A23 RID: 2595
	private UIDebugMenuButton m_backButton;

	// Token: 0x04000A24 RID: 2596
	private UIDebugMenuTextField m_testUserCountField;

	// Token: 0x04000A25 RID: 2597
	private UIDebugMenuTextField m_masterUserIdField;

	// Token: 0x04000A26 RID: 2598
	private UIDebugMenuTextField m_masterUserAccessTokenField;

	// Token: 0x04000A27 RID: 2599
	private FacebookTestUserCreater m_testUserCreate;

	// Token: 0x04000A28 RID: 2600
	private FacebookMasterUserCreater m_masterUserCreate;

	// Token: 0x020001C6 RID: 454
	private enum MenuType
	{
		// Token: 0x04000A2A RID: 2602
		FEED,
		// Token: 0x04000A2B RID: 2603
		DELETE_CUSTOM_DATA,
		// Token: 0x04000A2C RID: 2604
		LOGOUT,
		// Token: 0x04000A2D RID: 2605
		CREATE_TEST_USER,
		// Token: 0x04000A2E RID: 2606
		CREATE_MASTER_USER,
		// Token: 0x04000A2F RID: 2607
		NUM
	}
}
