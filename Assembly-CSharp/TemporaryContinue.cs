using System;
using Message;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class TemporaryContinue : MonoBehaviour
{
	// Token: 0x060017B7 RID: 6071 RVA: 0x00087E2C File Offset: 0x0008602C
	private void Start()
	{
	}

	// Token: 0x060017B8 RID: 6072 RVA: 0x00087E30 File Offset: 0x00086030
	private void Update()
	{
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x00087E34 File Offset: 0x00086034
	private void OnGUI()
	{
		GUI.TextArea(new Rect(400f, 100f, 200f, 50f), "Continue?");
		if (GUI.Button(new Rect(200f, 300f, 200f, 100f), "Yes"))
		{
			MsgContinueResult value = new MsgContinueResult(true);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value, SendMessageOptions.DontRequireReceiver);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (GUI.Button(new Rect(600f, 300f, 200f, 100f), "No"))
		{
			MsgContinueResult value2 = new MsgContinueResult(false);
			GameObjectUtil.SendMessageFindGameObject("GameModeStage", "OnSendToGameModeStage", value2, SendMessageOptions.DontRequireReceiver);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
