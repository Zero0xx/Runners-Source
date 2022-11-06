using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A1A RID: 2586
public class DelayedMessageManager : MonoBehaviour
{
	// Token: 0x06004496 RID: 17558 RVA: 0x0016149C File Offset: 0x0015F69C
	protected void Awake()
	{
		this.CheckInstance();
	}

	// Token: 0x06004497 RID: 17559 RVA: 0x001614A8 File Offset: 0x0015F6A8
	private void Start()
	{
		this.m_datas = new List<DelayedMessageManager.DelayMessageData>();
		this.m_sendToTagDatas = new List<DelayedMessageManager.DelayMessageData>();
	}

	// Token: 0x06004498 RID: 17560 RVA: 0x001614C0 File Offset: 0x0015F6C0
	private void Update()
	{
		if (this.m_sendToTagDatas.Count > 0)
		{
			foreach (DelayedMessageManager.DelayMessageData delayMessageData in this.m_sendToTagDatas)
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag(delayMessageData.m_objectName);
				foreach (GameObject gameObject in array)
				{
					this.AddDelayedMessage(gameObject, delayMessageData.m_methodName, delayMessageData.m_option);
				}
			}
			this.m_sendToTagDatas.Clear();
		}
		if (this.m_datas.Count > 0)
		{
			for (int j = 0; j < this.m_datas.Count; j++)
			{
				DelayedMessageManager.DelayMessageData delayMessageData2 = this.m_datas[j];
				if (delayMessageData2.m_object == null && delayMessageData2.m_objectName != null)
				{
					delayMessageData2.m_object = GameObject.Find(delayMessageData2.m_objectName);
				}
				if (delayMessageData2.m_object != null)
				{
					delayMessageData2.m_object.SendMessage(delayMessageData2.m_methodName, delayMessageData2.m_option, SendMessageOptions.DontRequireReceiver);
				}
			}
			this.m_datas.Clear();
		}
	}

	// Token: 0x06004499 RID: 17561 RVA: 0x00161628 File Offset: 0x0015F828
	public void AddDelayedMessage(string objectName, string methodName, object option)
	{
		DelayedMessageManager.DelayMessageData item = new DelayedMessageManager.DelayMessageData(objectName, methodName, option);
		this.m_datas.Add(item);
	}

	// Token: 0x0600449A RID: 17562 RVA: 0x0016164C File Offset: 0x0015F84C
	public void AddDelayedMessage(GameObject gameObject, string methodName, object option)
	{
		DelayedMessageManager.DelayMessageData item = new DelayedMessageManager.DelayMessageData(gameObject, methodName, option);
		this.m_datas.Add(item);
	}

	// Token: 0x0600449B RID: 17563 RVA: 0x00161670 File Offset: 0x0015F870
	public void AddDelayedMessageToTag(string objectName, string methodName, object option)
	{
		DelayedMessageManager.DelayMessageData item = new DelayedMessageManager.DelayMessageData(objectName, methodName, option);
		this.m_sendToTagDatas.Add(item);
	}

	// Token: 0x1700094E RID: 2382
	// (get) Token: 0x0600449C RID: 17564 RVA: 0x00161694 File Offset: 0x0015F894
	public static DelayedMessageManager Instance
	{
		get
		{
			if (DelayedMessageManager.instance == null)
			{
				DelayedMessageManager.instance = GameObjectUtil.FindGameObjectComponent<DelayedMessageManager>("DelayedMessageManager");
			}
			return DelayedMessageManager.instance;
		}
	}

	// Token: 0x0600449D RID: 17565 RVA: 0x001616C8 File Offset: 0x0015F8C8
	private void OnDestroy()
	{
		if (DelayedMessageManager.instance == this)
		{
			DelayedMessageManager.instance = null;
		}
	}

	// Token: 0x0600449E RID: 17566 RVA: 0x001616E0 File Offset: 0x0015F8E0
	protected bool CheckInstance()
	{
		if (DelayedMessageManager.instance == null)
		{
			DelayedMessageManager.instance = this;
			return true;
		}
		if (this == DelayedMessageManager.Instance)
		{
			return true;
		}
		UnityEngine.Object.Destroy(this);
		return false;
	}

	// Token: 0x040039A6 RID: 14758
	private List<DelayedMessageManager.DelayMessageData> m_datas;

	// Token: 0x040039A7 RID: 14759
	private List<DelayedMessageManager.DelayMessageData> m_sendToTagDatas;

	// Token: 0x040039A8 RID: 14760
	private static DelayedMessageManager instance;

	// Token: 0x02000A1B RID: 2587
	private class DelayMessageData
	{
		// Token: 0x0600449F RID: 17567 RVA: 0x00161714 File Offset: 0x0015F914
		public DelayMessageData(string objectName, string methodName, object option)
		{
			this.m_objectName = objectName;
			this.m_methodName = methodName;
			this.m_option = option;
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x00161734 File Offset: 0x0015F934
		public DelayMessageData(GameObject gameObject, string methodName, object option)
		{
			this.m_object = gameObject;
			this.m_methodName = methodName;
			this.m_option = option;
		}

		// Token: 0x040039A9 RID: 14761
		public string m_objectName;

		// Token: 0x040039AA RID: 14762
		public string m_methodName;

		// Token: 0x040039AB RID: 14763
		public GameObject m_object;

		// Token: 0x040039AC RID: 14764
		public object m_option;
	}
}
