using System;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class MenuPlayerSetGripR : MenuPlayerSetPartsBase
{
	// Token: 0x0600248A RID: 9354 RVA: 0x000DB47C File Offset: 0x000D967C
	public MenuPlayerSetGripR() : base("player_set_grip_R")
	{
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000DB48C File Offset: 0x000D968C
	public void SetCallback(MenuPlayerSetGripR.ButtonClickCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x0600248C RID: 9356 RVA: 0x000DB498 File Offset: 0x000D9698
	public void SetDisplayFlag(bool flag)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_grip_R");
		if (gameObject != null)
		{
			if (!gameObject.activeSelf && flag)
			{
				gameObject.SetActive(true);
			}
			else if (gameObject.activeSelf && !flag)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600248D RID: 9357 RVA: 0x000DB4F8 File Offset: 0x000D96F8
	protected override void OnSetup()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_grip_R");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
			UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			}
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "GripRClickCallback";
		}
	}

	// Token: 0x0600248E RID: 9358 RVA: 0x000DB55C File Offset: 0x000D975C
	protected override void OnPlayStart()
	{
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x000DB560 File Offset: 0x000D9760
	protected override void OnPlayEnd()
	{
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x000DB564 File Offset: 0x000D9764
	protected override void OnUpdate(float deltaTime)
	{
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x000DB568 File Offset: 0x000D9768
	private void GripRClickCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x040020ED RID: 8429
	private MenuPlayerSetGripR.ButtonClickCallback m_callback;

	// Token: 0x02000A98 RID: 2712
	// (Invoke) Token: 0x0600489A RID: 18586
	public delegate void ButtonClickCallback();
}
