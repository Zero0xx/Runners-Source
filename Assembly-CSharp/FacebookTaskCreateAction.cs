using System;
using System.Collections.Generic;
using Facebook;
using LitJson;

// Token: 0x020009F4 RID: 2548
public class FacebookTaskCreateAction : SocialTaskBase
{
	// Token: 0x06004343 RID: 17219 RVA: 0x0015D784 File Offset: 0x0015B984
	public FacebookTaskCreateAction()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x06004344 RID: 17220 RVA: 0x0015D7A0 File Offset: 0x0015B9A0
	public void Request(string actionName, string postObjectName, string postObjectId, FacebookTaskCreateAction.TaskFinishedCallback callback)
	{
		this.m_actionName = actionName;
		this.m_postObjectName = postObjectName;
		this.m_postObjectId = postObjectId;
		this.m_callback = callback;
	}

	// Token: 0x06004345 RID: 17221 RVA: 0x0015D7C0 File Offset: 0x0015B9C0
	protected override void OnStartProcess()
	{
		string query = FacebookUtil.FBVersionString + "me/testrunners:" + this.m_actionName;
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{
				this.m_postObjectName,
				this.m_postObjectId
			}
		};
		dictionary.Add("no_feed_story", "1");
		FB.API(query, HttpMethod.POST, new FacebookDelegate(this.CreateActionEndCallback), dictionary);
	}

	// Token: 0x06004346 RID: 17222 RVA: 0x0015D828 File Offset: 0x0015BA28
	protected override void OnUpdate()
	{
	}

	// Token: 0x06004347 RID: 17223 RVA: 0x0015D82C File Offset: 0x0015BA2C
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x06004348 RID: 17224 RVA: 0x0015D834 File Offset: 0x0015BA34
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004349 RID: 17225 RVA: 0x0015D83C File Offset: 0x0015BA3C
	private void CreateActionEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			return;
		}
		Debug.Log("Facebook.CreateAction:" + fbResult.Text);
		string text = fbResult.Text;
		if (text == null)
		{
			return;
		}
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return;
		}
		string jsonString = NetUtil.GetJsonString(jsonData, "id");
		this.m_callback(jsonString);
	}

	// Token: 0x04003907 RID: 14599
	private readonly string TaskName = "FacebookTaskCreateAction";

	// Token: 0x04003908 RID: 14600
	private bool m_isEndProcess;

	// Token: 0x04003909 RID: 14601
	private string m_actionName;

	// Token: 0x0400390A RID: 14602
	private string m_postObjectName;

	// Token: 0x0400390B RID: 14603
	private string m_postObjectId;

	// Token: 0x0400390C RID: 14604
	private FacebookTaskCreateAction.TaskFinishedCallback m_callback;

	// Token: 0x02000AA7 RID: 2727
	// (Invoke) Token: 0x060048D6 RID: 18646
	public delegate void TaskFinishedCallback(string actionId);
}
