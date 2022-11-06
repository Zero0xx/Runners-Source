using System;
using UnityEngine;

// Token: 0x020004D2 RID: 1234
public class MenuPlayerSetGripL : MenuPlayerSetPartsBase
{
	// Token: 0x06002482 RID: 9346 RVA: 0x000DB3A4 File Offset: 0x000D95A4
	public MenuPlayerSetGripL() : base("player_set_grip_L")
	{
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x000DB3B4 File Offset: 0x000D95B4
	public void SetCallback(MenuPlayerSetGripL.ButtonClickCallback callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x000DB3C0 File Offset: 0x000D95C0
	public void SetDisplayFlag(bool flag)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_grip_L");
		if (gameObject != null)
		{
			gameObject.SetActive(flag);
		}
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x000DB3F4 File Offset: 0x000D95F4
	protected override void OnSetup()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "player_set_grip_L");
		if (gameObject != null)
		{
			gameObject.SetActive(false);
			UIButtonMessage uibuttonMessage = gameObject.GetComponent<UIButtonMessage>();
			if (uibuttonMessage == null)
			{
				uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
			}
			uibuttonMessage.target = base.gameObject;
			uibuttonMessage.functionName = "GripLClickCallback";
		}
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x000DB458 File Offset: 0x000D9658
	protected override void OnPlayStart()
	{
	}

	// Token: 0x06002487 RID: 9351 RVA: 0x000DB45C File Offset: 0x000D965C
	protected override void OnPlayEnd()
	{
	}

	// Token: 0x06002488 RID: 9352 RVA: 0x000DB460 File Offset: 0x000D9660
	protected override void OnUpdate(float deltaTime)
	{
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x000DB464 File Offset: 0x000D9664
	private void GripLClickCallback()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
	}

	// Token: 0x040020EC RID: 8428
	private MenuPlayerSetGripL.ButtonClickCallback m_callback;

	// Token: 0x02000A97 RID: 2711
	// (Invoke) Token: 0x06004896 RID: 18582
	public delegate void ButtonClickCallback();
}
