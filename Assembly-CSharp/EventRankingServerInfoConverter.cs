using System;
using System.Collections.Generic;
using Text;

// Token: 0x020004E7 RID: 1255
public class EventRankingServerInfoConverter
{
	// Token: 0x0600256A RID: 9578 RVA: 0x000E1D1C File Offset: 0x000DFF1C
	public EventRankingServerInfoConverter(string serverMessageInfo)
	{
		this.Setup(serverMessageInfo);
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x000E1D4C File Offset: 0x000DFF4C
	public void Setup(string serverMessageInfo)
	{
		if (serverMessageInfo == null)
		{
			return;
		}
		this.m_messageInfo = serverMessageInfo;
		string[] array = this.m_messageInfo.Split(new char[]
		{
			','
		});
		if (array != null && array.Length > 0)
		{
			if (array.Length > 0)
			{
				this.m_eventId = int.Parse(array[0]);
			}
			if (array.Length > 1)
			{
				this.m_playerRank = int.Parse(array[1]);
			}
			else
			{
				this.m_playerRank = 0;
			}
			if (array.Length > 2)
			{
				this.m_participationNum = int.Parse(array[2]);
			}
			else
			{
				this.m_participationNum = 0;
			}
			if (array.Length > 3)
			{
				this.m_isPresented = (int.Parse(array[3]) != 0);
			}
			else
			{
				this.m_isPresented = false;
			}
		}
		else
		{
			this.m_messageInfo = null;
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x0600256C RID: 9580 RVA: 0x000E1E28 File Offset: 0x000E0028
	public bool isInit
	{
		get
		{
			return this.m_messageInfo != null;
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x0600256D RID: 9581 RVA: 0x000E1E38 File Offset: 0x000E0038
	public int eventId
	{
		get
		{
			return this.m_eventId;
		}
	}

	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x0600256E RID: 9582 RVA: 0x000E1E40 File Offset: 0x000E0040
	public string Result
	{
		get
		{
			if (!this.isInit)
			{
				return null;
			}
			string commonText;
			if (this.m_isPresented)
			{
				commonText = TextUtility.GetCommonText("Ranking", "ranking_result_event_text_2");
			}
			else
			{
				commonText = TextUtility.GetCommonText("Ranking", "ranking_result_event_text_1");
			}
			string eventName = this.GetEventName();
			return TextUtility.Replaces(commonText, new Dictionary<string, string>
			{
				{
					"{PARAM1}",
					eventName
				},
				{
					"{PARAM2}",
					this.m_playerRank.ToString()
				},
				{
					"{PARAM3}",
					this.m_participationNum.ToString()
				}
			});
		}
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x000E1ED8 File Offset: 0x000E00D8
	public string GetEventName()
	{
		string result = string.Empty;
		switch (EventManager.GetType(this.m_eventId))
		{
		case EventManager.EventType.SPECIAL_STAGE:
			result = HudUtility.GetEventStageName(EventManager.GetSpecificId(this.m_eventId));
			break;
		case EventManager.EventType.RAID_BOSS:
			result = HudUtility.GetEventStageName(EventManager.GetSpecificId(this.m_eventId));
			break;
		case EventManager.EventType.COLLECT_OBJECT:
			switch (EventManager.GetCollectEventType(this.m_eventId))
			{
			case EventManager.CollectEventType.GET_ANIMALS:
				result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_animl_get_event");
				break;
			case EventManager.CollectEventType.GET_RING:
				result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_ring_get_event");
				break;
			case EventManager.CollectEventType.RUN_DISTANCE:
				result = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Menu", "ui_Lbl_word_run_distance_event");
				break;
			}
			break;
		}
		return result;
	}

	// Token: 0x04002194 RID: 8596
	private string m_messageInfo;

	// Token: 0x04002195 RID: 8597
	private int m_playerRank = -1;

	// Token: 0x04002196 RID: 8598
	private int m_participationNum = -1;

	// Token: 0x04002197 RID: 8599
	private int m_eventId = -1;

	// Token: 0x04002198 RID: 8600
	private bool m_isPresented;

	// Token: 0x020004E8 RID: 1256
	public enum MSG_INFO
	{
		// Token: 0x0400219A RID: 8602
		EventId,
		// Token: 0x0400219B RID: 8603
		PlayerRank,
		// Token: 0x0400219C RID: 8604
		ParticipationNum,
		// Token: 0x0400219D RID: 8605
		IsPresented,
		// Token: 0x0400219E RID: 8606
		StartDt,
		// Token: 0x0400219F RID: 8607
		EndDt,
		// Token: 0x040021A0 RID: 8608
		NUM
	}
}
