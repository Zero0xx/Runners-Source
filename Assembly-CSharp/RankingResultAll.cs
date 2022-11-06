using System;
using Text;
using UnityEngine;

// Token: 0x020004EF RID: 1263
public class RankingResultAll : MonoBehaviour
{
	// Token: 0x0600259E RID: 9630 RVA: 0x000E4A9C File Offset: 0x000E2C9C
	public void Setup(string serverMessageInfo)
	{
		string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "Ranking", "ranking_result_all_caption").text;
		this.m_rankingData = new RankingServerInfoConverter(serverMessageInfo);
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = "RankingResultAll",
			caption = text,
			message = this.m_rankingData.rankingResultAllText,
			buttonType = GeneralWindow.ButtonType.Close
		});
		this.m_mode = RankingResultAll.Mode.Wait;
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x000E4B10 File Offset: 0x000E2D10
	public bool IsEnd()
	{
		return this.m_mode != RankingResultAll.Mode.Wait;
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x000E4B24 File Offset: 0x000E2D24
	private void Update()
	{
		RankingResultAll.Mode mode = this.m_mode;
		if (mode == RankingResultAll.Mode.Wait)
		{
			if (GeneralWindow.IsCreated("RankingResultAll") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.m_mode = RankingResultAll.Mode.End;
			}
		}
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x000E4B70 File Offset: 0x000E2D70
	public static RankingResultAll Create(string serverMessageInfo)
	{
		GameObject gameObject = GameObject.Find("RankingResultAll");
		RankingResultAll rankingResultAll;
		if (gameObject == null)
		{
			gameObject = new GameObject("RankingResultAll");
			rankingResultAll = gameObject.AddComponent<RankingResultAll>();
		}
		else
		{
			rankingResultAll = gameObject.GetComponent<RankingResultAll>();
		}
		if (gameObject != null && rankingResultAll != null)
		{
			rankingResultAll.Setup(serverMessageInfo);
		}
		return rankingResultAll;
	}

	// Token: 0x040021E7 RID: 8679
	private RankingResultAll.Mode m_mode;

	// Token: 0x040021E8 RID: 8680
	private RankingServerInfoConverter m_rankingData;

	// Token: 0x020004F0 RID: 1264
	private enum Mode
	{
		// Token: 0x040021EA RID: 8682
		Idle,
		// Token: 0x040021EB RID: 8683
		Wait,
		// Token: 0x040021EC RID: 8684
		End
	}
}
