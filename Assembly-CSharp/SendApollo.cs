using System;
using Message;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class SendApollo : MonoBehaviour
{
	// Token: 0x0600071A RID: 1818 RVA: 0x00029998 File Offset: 0x00027B98
	private bool IsDebugDraw()
	{
		return false;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0002999C File Offset: 0x00027B9C
	public SendApollo.State GetState()
	{
		return this.m_state;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x000299A4 File Offset: 0x00027BA4
	public bool IsEnd()
	{
		return this.m_state != SendApollo.State.Request;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x000299B8 File Offset: 0x00027BB8
	public void RequestServer(ApolloType type, string[] value)
	{
		if (this.IsDebugDraw())
		{
			global::Debug.Log("SendApollo RequestServer type=" + type.ToString());
			if (value != null)
			{
				foreach (string str in value)
				{
					global::Debug.Log("SendApollo RequestServer value=" + str);
				}
			}
		}
		ServerInterface loggedInServerInterface = ServerInterface.LoggedInServerInterface;
		if (loggedInServerInterface != null)
		{
			loggedInServerInterface.RequestServerSendApollo((int)type, value, base.gameObject);
			this.m_state = SendApollo.State.Request;
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00029A44 File Offset: 0x00027C44
	private void ServerSendApollo_Succeeded(MsgSendApolloSucceed msg)
	{
		if (this.IsDebugDraw())
		{
			global::Debug.Log("SendApollo ServerSendApollo_Succeeded");
		}
		this.m_state = SendApollo.State.Succeeded;
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00029A64 File Offset: 0x00027C64
	private void ServerSendApollo_Failed(MsgServerConnctFailed msg)
	{
		if (this.IsDebugDraw())
		{
			global::Debug.Log("SendApollo ServerSendApollo_Failed");
		}
		this.m_state = SendApollo.State.Failed;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00029A84 File Offset: 0x00027C84
	public static SendApollo Create()
	{
		GameObject gameObject = new GameObject("SendApollo");
		return gameObject.AddComponent<SendApollo>();
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00029AA4 File Offset: 0x00027CA4
	public static SendApollo CreateRequest(ApolloType type, string[] value)
	{
		SendApollo sendApollo = SendApollo.Create();
		if (sendApollo != null)
		{
			sendApollo.RequestServer(type, value);
		}
		return sendApollo;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00029ACC File Offset: 0x00027CCC
	public static void GetTutorialValue(ApolloTutorialIndex index, ref string[] value)
	{
		if (value != null)
		{
			string[] array = value;
			int num = 0;
			int num2 = (int)index;
			array[num] = num2.ToString();
		}
	}

	// Token: 0x0400057A RID: 1402
	private bool m_debugDraw = true;

	// Token: 0x0400057B RID: 1403
	private SendApollo.State m_state;

	// Token: 0x020000F0 RID: 240
	public enum State
	{
		// Token: 0x0400057D RID: 1405
		Idle,
		// Token: 0x0400057E RID: 1406
		Request,
		// Token: 0x0400057F RID: 1407
		Succeeded,
		// Token: 0x04000580 RID: 1408
		Failed
	}
}
