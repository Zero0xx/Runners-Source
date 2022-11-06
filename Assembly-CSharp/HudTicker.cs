using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200045B RID: 1115
public class HudTicker : MonoBehaviour
{
	// Token: 0x0600218E RID: 8590 RVA: 0x000C9EF4 File Offset: 0x000C80F4
	private void Start()
	{
		HudMenuUtility.SetTagHudSaveItem(base.gameObject);
		base.enabled = false;
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x000C9F08 File Offset: 0x000C8108
	public void OnUpdateTickerDisplay()
	{
		TickerWindow tickerWindow = this.GetTickerWindow();
		if (tickerWindow == null)
		{
			return;
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			ServerTickerInfo tickerInfo = ServerInterface.TickerInfo;
			if (tickerInfo != null && tickerInfo.Data != null)
			{
				List<ServerTickerData> list = new List<ServerTickerData>();
				for (int i = 0; i < tickerInfo.Data.Count; i++)
				{
					ServerTickerData serverTickerData = tickerInfo.Data[i];
					if (serverTickerData != null && NetUtil.IsServerTimeWithinPeriod(serverTickerData.Start, serverTickerData.End))
					{
						list.Add(serverTickerData);
					}
				}
				if (list.Count > 0)
				{
					this.SetTickerData(list);
					return;
				}
			}
			List<ServerTickerData> list2 = new List<ServerTickerData>();
			ServerTickerData serverTickerData2 = new ServerTickerData();
			serverTickerData2.Init(0L, 0L, 0L, string.Empty);
			list2.Add(serverTickerData2);
			this.SetTickerData(list2);
			return;
		}
		List<ServerTickerData> list3 = new List<ServerTickerData>();
		ServerTickerData serverTickerData3 = new ServerTickerData();
		ServerTickerData serverTickerData4 = new ServerTickerData();
		serverTickerData3.Init(0L, 0L, 0L, "ログインしていません");
		serverTickerData4.Init(0L, 0L, 0L, "ログインしたらおしらせがひょうじされます");
		list3.Add(serverTickerData3);
		list3.Add(serverTickerData4);
		this.SetTickerData(list3);
	}

	// Token: 0x06002190 RID: 8592 RVA: 0x000CA04C File Offset: 0x000C824C
	public void OnTickerReset()
	{
		TickerWindow tickerWindow = this.GetTickerWindow();
		if (tickerWindow != null)
		{
			tickerWindow.ResetWindow();
		}
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x000CA074 File Offset: 0x000C8274
	private GameObject GetLabelObject()
	{
		GameObject gameObject = GameObject.Find("UI Root (2D)");
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "mainmenu_info_user");
		if (gameObject2 == null)
		{
			return null;
		}
		return GameObjectUtil.FindChildGameObject(gameObject2, "ticker");
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x000CA0C0 File Offset: 0x000C82C0
	private TickerWindow GetTickerWindow()
	{
		GameObject labelObject = this.GetLabelObject();
		if (labelObject == null)
		{
			return null;
		}
		return labelObject.GetComponent<TickerWindow>();
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x000CA0E8 File Offset: 0x000C82E8
	private void SetTickerData(List<ServerTickerData> tickerData)
	{
		TickerWindow tickerWindow = this.GetTickerWindow();
		if (tickerWindow != null)
		{
			tickerWindow.Setup(new TickerWindow.CInfo
			{
				tickerList = tickerData,
				labelName = "Lbl_ticker",
				moveSpeed = 1f,
				moveSpeedUp = 20f
			});
		}
	}
}
