using System;
using UnityEngine;

// Token: 0x02000A20 RID: 2592
public class HtmlWindow : MonoBehaviour
{
	// Token: 0x060044B3 RID: 17587 RVA: 0x00161DA4 File Offset: 0x0015FFA4
	private void Start()
	{
	}

	// Token: 0x060044B4 RID: 17588 RVA: 0x00161DA8 File Offset: 0x0015FFA8
	private void Update()
	{
		if (this.m_isSetup)
		{
			HtmlParser component = this.m_parserObject.GetComponent<HtmlParser>();
			if (component == null)
			{
				return;
			}
			if (component.IsEndParse)
			{
				GeneralWindow.Create(new GeneralWindow.CInfo
				{
					buttonType = GeneralWindow.ButtonType.Ok,
					caption = "利用規約",
					message = component.ParsedString
				});
				this.m_isSetup = false;
			}
		}
	}

	// Token: 0x060044B5 RID: 17589 RVA: 0x00161E1C File Offset: 0x0016001C
	private void PlayStartSync()
	{
		string urlString = HtmlParserFactory.GetUrlString("http://puyopuyoquest.sega-net.com/rule/index.html");
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = "利用規約",
			message = urlString
		});
		this.m_isSetup = false;
	}

	// Token: 0x060044B6 RID: 17590 RVA: 0x00161E68 File Offset: 0x00160068
	private void PlayStartASync()
	{
		this.m_parserObject = HtmlParserFactory.Create("http://puyopuyoquest.sega-net.com/rule/index.html", HtmlParser.SyncType.TYPE_ASYNC, HtmlParser.SyncType.TYPE_ASYNC);
		this.m_isSetup = true;
	}

	// Token: 0x040039AF RID: 14767
	private GameObject m_parserObject;

	// Token: 0x040039B0 RID: 14768
	private bool m_isSetup;

	// Token: 0x02000A21 RID: 2593
	private enum EventSignal
	{
		// Token: 0x040039B2 RID: 14770
		SIG_PLAYSTART = 100
	}
}
