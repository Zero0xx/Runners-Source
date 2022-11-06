using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public class IncentiveWindowQueue : MonoBehaviour
{
	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x06002A1E RID: 10782 RVA: 0x00105020 File Offset: 0x00103220
	public bool SetUpped
	{
		get
		{
			return !(this.m_resourceLoader != null) || this.m_resourceLoader.IsLoaded;
		}
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x00105054 File Offset: 0x00103254
	public void AddWindow(IncentiveWindow window)
	{
		this.m_queue.Add(window);
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x00105064 File Offset: 0x00103264
	public void PlayStart()
	{
		if (this.IsEmpty())
		{
			return;
		}
		if (this.m_queue == null)
		{
			return;
		}
		this.m_queue[0].PlayStart();
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x00105090 File Offset: 0x00103290
	public bool IsEmpty()
	{
		return this.m_queue.Count <= 0;
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x001050A8 File Offset: 0x001032A8
	private void Start()
	{
		if (this.m_resourceLoader == null)
		{
			this.m_resourceLoader = base.gameObject.AddComponent<ButtonEventResourceLoader>();
		}
		this.m_resourceLoader.LoadResourceIfNotLoadedAsync("item_get_Window", delegate()
		{
			if (FontManager.Instance != null)
			{
				FontManager.Instance.ReplaceFont();
			}
			if (AtlasManager.Instance != null)
			{
				AtlasManager.Instance.ReplaceAtlasForMenu(true);
			}
		});
	}

	// Token: 0x06002A23 RID: 10787 RVA: 0x00105104 File Offset: 0x00103304
	private void Update()
	{
		if (this.IsEmpty())
		{
			return;
		}
		if (!this.SetUpped)
		{
			return;
		}
		IncentiveWindow incentiveWindow = this.m_queue[0];
		if (incentiveWindow == null)
		{
			return;
		}
		incentiveWindow.Update();
		if (incentiveWindow.IsEnd)
		{
			this.m_queue.Remove(incentiveWindow);
			if (!this.IsEmpty())
			{
				this.m_queue[0].PlayStart();
			}
		}
	}

	// Token: 0x0400256B RID: 9579
	private List<IncentiveWindow> m_queue = new List<IncentiveWindow>();

	// Token: 0x0400256C RID: 9580
	private ButtonEventResourceLoader m_resourceLoader;
}
