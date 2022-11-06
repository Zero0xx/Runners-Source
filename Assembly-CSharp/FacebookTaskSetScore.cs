using System;
using System.Collections.Generic;
using Facebook;
using Message;
using UnityEngine;

// Token: 0x02000A07 RID: 2567
public class FacebookTaskSetScore : SocialTaskBase
{
	// Token: 0x060043C7 RID: 17351 RVA: 0x0015F5E8 File Offset: 0x0015D7E8
	public FacebookTaskSetScore()
	{
		this.m_callbackObject = null;
		this.m_isEndProcess = false;
	}

	// Token: 0x060043C8 RID: 17352 RVA: 0x0015F60C File Offset: 0x0015D80C
	public void Request(SocialDefine.ScoreType type, int score, GameObject callbackObject)
	{
		this.m_score = score;
		this.m_callbackObject = callbackObject;
	}

	// Token: 0x060043C9 RID: 17353 RVA: 0x0015F61C File Offset: 0x0015D81C
	protected override void OnStartProcess()
	{
		if (!FacebookUtil.IsLoggedIn())
		{
			this.m_isEndProcess = true;
			return;
		}
		string query = FacebookUtil.FBVersionString + "me/scores)";
		Dictionary<string, string> formData = new Dictionary<string, string>
		{
			{
				"score",
				this.m_score.ToString()
			}
		};
		FB.API(query, HttpMethod.POST, new FacebookDelegate(this.SetScoreEndCallback), formData);
	}

	// Token: 0x060043CA RID: 17354 RVA: 0x0015F684 File Offset: 0x0015D884
	protected override void OnUpdate()
	{
	}

	// Token: 0x060043CB RID: 17355 RVA: 0x0015F688 File Offset: 0x0015D888
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x060043CC RID: 17356 RVA: 0x0015F690 File Offset: 0x0015D890
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x060043CD RID: 17357 RVA: 0x0015F698 File Offset: 0x0015D898
	private void SetScoreEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		if (this.m_callbackObject == null)
		{
			return;
		}
		SocialResult socialResult = new SocialResult();
		socialResult.ResultId = 0;
		socialResult.Result = fbResult.Text;
		socialResult.IsError = false;
		MsgSocialNormalResponse msgSocialNormalResponse = new MsgSocialNormalResponse();
		msgSocialNormalResponse.m_result = socialResult;
		this.m_callbackObject.SendMessage("SetScoreEndCallback", msgSocialNormalResponse, SendMessageOptions.DontRequireReceiver);
		this.m_callbackObject = null;
	}

	// Token: 0x04003952 RID: 14674
	private readonly string TaskName = "FacebookTaskSetScore";

	// Token: 0x04003953 RID: 14675
	private GameObject m_callbackObject;

	// Token: 0x04003954 RID: 14676
	private bool m_isEndProcess;

	// Token: 0x04003955 RID: 14677
	private int m_score;
}
