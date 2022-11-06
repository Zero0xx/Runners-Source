using System;
using System.Collections.Generic;
using Facebook;
using LitJson;

// Token: 0x020009F5 RID: 2549
public class FacebookTaskCreateObject : SocialTaskBase
{
	// Token: 0x0600434A RID: 17226 RVA: 0x0015D8AC File Offset: 0x0015BAAC
	public FacebookTaskCreateObject()
	{
		this.m_isEndProcess = false;
	}

	// Token: 0x0600434B RID: 17227 RVA: 0x0015D8C8 File Offset: 0x0015BAC8
	public void Request(string objectName, string jSonString, FacebookTaskCreateObject.TaskFinishedCallback callback)
	{
		this.m_objectName = objectName;
		this.m_jSonString = jSonString;
		this.m_callback = callback;
	}

	// Token: 0x0600434C RID: 17228 RVA: 0x0015D8E0 File Offset: 0x0015BAE0
	protected override void OnStartProcess()
	{
		string query = FacebookUtil.FBVersionString + "me/objects/testrunners:" + this.m_objectName;
		Dictionary<string, string> formData = new Dictionary<string, string>
		{
			{
				"object",
				this.m_jSonString
			}
		};
		FB.API(query, HttpMethod.POST, new FacebookDelegate(this.CreateObjectEndCallback), formData);
	}

	// Token: 0x0600434D RID: 17229 RVA: 0x0015D934 File Offset: 0x0015BB34
	protected override void OnUpdate()
	{
	}

	// Token: 0x0600434E RID: 17230 RVA: 0x0015D938 File Offset: 0x0015BB38
	protected override bool OnIsEndProcess()
	{
		return this.m_isEndProcess;
	}

	// Token: 0x0600434F RID: 17231 RVA: 0x0015D940 File Offset: 0x0015BB40
	protected override string OnGetTaskName()
	{
		return this.TaskName;
	}

	// Token: 0x06004350 RID: 17232 RVA: 0x0015D948 File Offset: 0x0015BB48
	private void CreateObjectEndCallback(FBResult fbResult)
	{
		this.m_isEndProcess = true;
		if (fbResult == null)
		{
			this.m_callback(null);
			return;
		}
		if (fbResult.Text == null)
		{
			this.m_callback(null);
			return;
		}
		Debug.Log("Facebook.CreateObject:" + fbResult.Text);
		string text = fbResult.Text;
		if (text == null)
		{
			return;
		}
		Debug.Log("Facebook.CreateObject:" + text);
		JsonData jsonData = JsonMapper.ToObject(text);
		if (jsonData == null)
		{
			Debug.Log("Failed transform plainText to Json");
			return;
		}
		string jsonString = NetUtil.GetJsonString(jsonData, "id");
		this.m_callback(jsonString);
	}

	// Token: 0x0400390D RID: 14605
	private readonly string TaskName = "FacebookTaskCreateObject";

	// Token: 0x0400390E RID: 14606
	private bool m_isEndProcess;

	// Token: 0x0400390F RID: 14607
	private string m_objectName;

	// Token: 0x04003910 RID: 14608
	private string m_jSonString;

	// Token: 0x04003911 RID: 14609
	private FacebookTaskCreateObject.TaskFinishedCallback m_callback;

	// Token: 0x02000AA8 RID: 2728
	// (Invoke) Token: 0x060048DA RID: 18650
	public delegate void TaskFinishedCallback(string objectId);
}
