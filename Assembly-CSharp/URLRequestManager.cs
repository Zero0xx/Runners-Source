using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007C4 RID: 1988
public class URLRequestManager : MonoBehaviour
{
	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x06003474 RID: 13428 RVA: 0x0011D36C File Offset: 0x0011B56C
	public static URLRequestManager Instance
	{
		get
		{
			return URLRequestManager.mInstance;
		}
	}

	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x06003475 RID: 13429 RVA: 0x0011D374 File Offset: 0x0011B574
	// (set) Token: 0x06003476 RID: 13430 RVA: 0x0011D37C File Offset: 0x0011B57C
	public bool Emulation
	{
		get
		{
			return this.mEmulation;
		}
		set
		{
			this.mEmulation = value;
		}
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x0011D388 File Offset: 0x0011B588
	private void Awake()
	{
		if (URLRequestManager.mInstance == null)
		{
			URLRequestManager.mInstance = this;
			this.mEmulation = false;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.mRequestList = new List<URLRequest>();
			this.mExecuteList = new List<URLRequest>();
			this.mRemainingList = new List<URLRequest>();
			this.mExecuting = false;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x0011D3F0 File Offset: 0x0011B5F0
	private void Start()
	{
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x0011D3F4 File Offset: 0x0011B5F4
	private void Update()
	{
		if (this.mExecuting)
		{
			return;
		}
		if (0 < this.mRequestList.Count)
		{
			this.mExecuteList.Clear();
			this.mRemainingList.Clear();
			int count = this.mRequestList.Count;
			for (int i = 0; i < count; i++)
			{
				URLRequest item = this.mRequestList[i];
				this.mExecuteList.Add(item);
				this.mRemainingList.Add(item);
			}
			this.mRequestList.Clear();
			this.mExecuting = true;
			base.StartCoroutine("ExecuteRequest");
		}
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x0011D4A0 File Offset: 0x0011B6A0
	private IEnumerator ExecuteRequest()
	{
		int count = this.mExecuteList.Count;
		for (int i = 0; i < count; i++)
		{
			URLRequest request = this.mExecuteList[i];
			if (request != null)
			{
				bool cancel = false;
				bool exec = true;
				while (exec)
				{
					if (this.Emulation || request.Emulation)
					{
						request.PreBegin();
						WWWForm form = request.CreateWWWForm();
						if (form == null)
						{
							continue;
						}
						string param = Encoding.ASCII.GetString(form.data);
						global::Debug.Log(string.Concat(new string[]
						{
							"URLRequest Emulation : [",
							request.url,
							"] [",
							param,
							"]"
						}), DebugTraceManager.TraceType.SERVER);
						float startTime = Time.realtimeSinceStartup;
						do
						{
							yield return null;
						}
						while (Time.realtimeSinceStartup - startTime < 1f);
						request.DidReceiveSuccess(null);
					}
					else
					{
						this.mCurrentRequest = request;
						request.Begin();
						float spendTime = 0f;
						while (!request.IsDone())
						{
							float waitTime = request.UpdateElapsedTime(spendTime);
							if (request.IsTimeOut())
							{
								break;
							}
							float startTime2 = Time.realtimeSinceStartup;
							do
							{
								yield return null;
								spendTime = Time.realtimeSinceStartup - startTime2;
							}
							while (spendTime < waitTime);
						}
					}
					if (this.mCancelState == URLRequestManager.CancelState.CANCELING)
					{
						global::Debug.Log("-------------- AssetBundleRequest.ExecuteRequest Cancel --------------", DebugTraceManager.TraceType.SERVER);
						this.mCancelState = URLRequestManager.CancelState.CANCELED;
						this.mExecuting = false;
						exec = false;
						cancel = true;
					}
					else
					{
						if (!this.Emulation && !request.Emulation)
						{
							request.Result();
						}
						exec = false;
						this.mRemainingList.Remove(request);
					}
				}
				if (cancel)
				{
					break;
				}
			}
		}
		this.mExecuteList.Clear();
		this.mRemainingList.Clear();
		this.mCurrentRequest = null;
		this.mExecuting = false;
		this.ClearCancel();
		yield break;
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x0011D4BC File Offset: 0x0011B6BC
	public void Request(URLRequest request)
	{
		this.mRequestList.Add(request);
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x0011D4CC File Offset: 0x0011B6CC
	public void Cancel()
	{
		if (0 >= this.mExecuteList.Count)
		{
			this.mCancelState = URLRequestManager.CancelState.IDLE;
			return;
		}
		this.mCancelRequest = true;
		this.mCancelState = URLRequestManager.CancelState.CANCELING;
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x0011D4F8 File Offset: 0x0011B6F8
	public bool IsCanceled()
	{
		return this.mCancelState == URLRequestManager.CancelState.IDLE || URLRequestManager.CancelState.CANCELED == this.mCancelState;
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x0011D510 File Offset: 0x0011B710
	public bool IsCancelRequest()
	{
		return this.mCancelRequest;
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x0011D518 File Offset: 0x0011B718
	public void ClearCancel()
	{
		this.mCancelRequest = false;
		this.mCancelState = URLRequestManager.CancelState.IDLE;
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x0011D528 File Offset: 0x0011B728
	public int GetRemainingCount()
	{
		if (this.mRemainingList == null)
		{
			return 0;
		}
		return this.mRemainingList.Count;
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x0011D544 File Offset: 0x0011B744
	public URLRequest GetRemainingRequest(int index)
	{
		if (this.mRemainingList != null && 0 <= index && this.mRemainingList.Count > index)
		{
			return this.mRemainingList[index];
		}
		return null;
	}

	// Token: 0x04002C1F RID: 11295
	private const float EMULATION_WAIT_TIME = 1f;

	// Token: 0x04002C20 RID: 11296
	public bool mEmulation;

	// Token: 0x04002C21 RID: 11297
	private List<URLRequest> mRequestList;

	// Token: 0x04002C22 RID: 11298
	private List<URLRequest> mExecuteList;

	// Token: 0x04002C23 RID: 11299
	private List<URLRequest> mRemainingList;

	// Token: 0x04002C24 RID: 11300
	private bool mExecuting;

	// Token: 0x04002C25 RID: 11301
	private static URLRequestManager mInstance;

	// Token: 0x04002C26 RID: 11302
	private URLRequest mCurrentRequest;

	// Token: 0x04002C27 RID: 11303
	private URLRequestManager.CancelState mCancelState;

	// Token: 0x04002C28 RID: 11304
	private bool mCancelRequest;

	// Token: 0x020007C5 RID: 1989
	private enum CancelState
	{
		// Token: 0x04002C2A RID: 11306
		IDLE,
		// Token: 0x04002C2B RID: 11307
		CANCELING,
		// Token: 0x04002C2C RID: 11308
		CANCELED
	}
}
