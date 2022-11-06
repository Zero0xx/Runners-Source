using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A35 RID: 2613
public class CustomGameObject : MonoBehaviour
{
	// Token: 0x1700095A RID: 2394
	// (get) Token: 0x06004551 RID: 17745 RVA: 0x00164374 File Offset: 0x00162574
	public float gameObjectTime
	{
		get
		{
			return this.m_gameObjTime;
		}
	}

	// Token: 0x1700095B RID: 2395
	// (get) Token: 0x06004552 RID: 17746 RVA: 0x0016437C File Offset: 0x0016257C
	// (set) Token: 0x06004553 RID: 17747 RVA: 0x00164384 File Offset: 0x00162584
	public float gameObjectTimeRate
	{
		get
		{
			return this.m_gameObjTimeRate;
		}
		protected set
		{
			this.m_gameObjTimeRate = value;
		}
	}

	// Token: 0x1700095C RID: 2396
	// (get) Token: 0x06004554 RID: 17748 RVA: 0x00164390 File Offset: 0x00162590
	// (set) Token: 0x06004555 RID: 17749 RVA: 0x00164398 File Offset: 0x00162598
	public float gameObjectSleepTime
	{
		get
		{
			return this.m_gameObjSleepTime;
		}
		protected set
		{
			this.m_gameObjSleepTime = value;
		}
	}

	// Token: 0x1700095D RID: 2397
	// (get) Token: 0x06004556 RID: 17750 RVA: 0x001643A4 File Offset: 0x001625A4
	public bool isSleep
	{
		get
		{
			return this.m_gameObjSleepTime > 0f;
		}
	}

	// Token: 0x1700095E RID: 2398
	// (get) Token: 0x06004557 RID: 17751 RVA: 0x001643B4 File Offset: 0x001625B4
	// (set) Token: 0x06004558 RID: 17752 RVA: 0x001643BC File Offset: 0x001625BC
	protected bool updateStdLast
	{
		get
		{
			return this.m_updateStdLast;
		}
		set
		{
			this.m_updateStdLast = value;
		}
	}

	// Token: 0x1700095F RID: 2399
	// (get) Token: 0x06004559 RID: 17753 RVA: 0x001643C8 File Offset: 0x001625C8
	// (set) Token: 0x0600455A RID: 17754 RVA: 0x001643D0 File Offset: 0x001625D0
	protected bool updateCusOnly
	{
		get
		{
			return this.m_updateCusOnly;
		}
		set
		{
			this.m_updateCusOnly = value;
		}
	}

	// Token: 0x0600455B RID: 17755 RVA: 0x001643DC File Offset: 0x001625DC
	private void Start()
	{
		this.m_gameObjTime = 0f;
	}

	// Token: 0x0600455C RID: 17756 RVA: 0x001643EC File Offset: 0x001625EC
	private void Update()
	{
		float num = Time.deltaTime;
		if (this.m_gameObjSleepTime <= 0f)
		{
			num *= this.m_gameObjTimeRate;
			if (!this.m_updateStdLast)
			{
				this.UpdateStd(num, this.m_gameObjTimeRate);
			}
			this.UpdateCustoms(num, this.m_gameObjTimeRate);
			if (this.m_updateStdLast)
			{
				this.UpdateStd(num, this.m_gameObjTimeRate);
			}
			this.m_gameObjTime += num;
		}
		else
		{
			this.m_gameObjSleepTime -= num;
			if (this.m_gameObjSleepTime <= 0f)
			{
				this.m_gameObjSleepTime = 0f;
			}
		}
	}

	// Token: 0x0600455D RID: 17757 RVA: 0x00164494 File Offset: 0x00162694
	private void UpdateCustoms(float deltaTime, float timeRate)
	{
		if (this.m_updateDelegateList != null)
		{
			int num = 0;
			Dictionary<string, CustomGameObject.UpdateCustom>.KeyCollection keys = this.m_updateDelegateList.Keys;
			foreach (string text in keys)
			{
				if (this.m_updateDelegateListCurrentTime[text] <= 0f)
				{
					if (!this.m_updateCusOnly || num == 0)
					{
						this.m_updateDelegateList[text](text, timeRate);
						this.m_updateDelegateListCurrentTime[text] = this.m_updateDelegateListTime[text];
						num++;
					}
				}
				else
				{
					Dictionary<string, float> updateDelegateListCurrentTime;
					Dictionary<string, float> dictionary = updateDelegateListCurrentTime = this.m_updateDelegateListCurrentTime;
					string key2;
					string key = key2 = text;
					float num2 = updateDelegateListCurrentTime[key2];
					dictionary[key] = num2 - deltaTime;
					if (this.m_updateDelegateListCurrentTime[text] <= 0f)
					{
						this.m_updateDelegateListCurrentTime[text] = 0f;
					}
				}
			}
		}
		if (this.m_callbackDelegateList != null)
		{
			Dictionary<string, CustomGameObject.Callback>.KeyCollection keys2 = this.m_callbackDelegateList.Keys;
			List<string> list = null;
			foreach (string text2 in keys2)
			{
				if (this.m_callbackDelegateListCurrentTime[text2] <= 0f)
				{
					this.m_callbackDelegateList[text2](text2);
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(text2);
				}
				else
				{
					Dictionary<string, float> callbackDelegateListCurrentTime;
					Dictionary<string, float> dictionary2 = callbackDelegateListCurrentTime = this.m_callbackDelegateListCurrentTime;
					string key2;
					string key3 = key2 = text2;
					float num2 = callbackDelegateListCurrentTime[key2];
					dictionary2[key3] = num2 - deltaTime;
					if (this.m_callbackDelegateListCurrentTime[text2] <= 0f)
					{
						this.m_callbackDelegateListCurrentTime[text2] = 0f;
					}
				}
			}
			if (list != null)
			{
				foreach (string callName in list)
				{
					this.RemoveCallback(callName);
				}
			}
		}
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x00164704 File Offset: 0x00162904
	protected virtual void UpdateStd(float deltaTime, float timeRate)
	{
	}

	// Token: 0x0600455F RID: 17759 RVA: 0x00164708 File Offset: 0x00162908
	protected bool AddUpdateCustom(CustomGameObject.UpdateCustom upd, string updName, float updTime)
	{
		bool result = false;
		if (!this.IsUpdateCustom(updName))
		{
			if (this.m_updateDelegateList == null)
			{
				this.m_updateDelegateList = new Dictionary<string, CustomGameObject.UpdateCustom>();
			}
			if (this.m_updateDelegateListTime == null)
			{
				this.m_updateDelegateListTime = new Dictionary<string, float>();
			}
			if (this.m_updateDelegateListCurrentTime == null)
			{
				this.m_updateDelegateListCurrentTime = new Dictionary<string, float>();
			}
			this.m_updateDelegateList.Add(updName, upd);
			this.m_updateDelegateListTime.Add(updName, updTime);
			this.m_updateDelegateListCurrentTime.Add(updName, updTime);
			result = true;
		}
		return result;
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x00164790 File Offset: 0x00162990
	protected bool SetUpdateCustom(string updName, float updTime)
	{
		bool result = false;
		if (this.IsUpdateCustom(updName))
		{
			this.m_updateDelegateListTime[updName] = updTime;
			this.m_updateDelegateListCurrentTime[updName] = updTime;
			result = true;
		}
		return result;
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x001647C8 File Offset: 0x001629C8
	protected bool RemoveUpdateCustom(string updName = null)
	{
		bool result = false;
		if (string.IsNullOrEmpty(updName))
		{
			if (this.m_updateDelegateList != null)
			{
				this.m_updateDelegateList.Clear();
			}
			if (this.m_updateDelegateListTime != null)
			{
				this.m_updateDelegateListTime.Clear();
			}
			if (this.m_updateDelegateListCurrentTime != null)
			{
				this.m_updateDelegateListCurrentTime.Clear();
			}
			this.m_updateDelegateList = null;
			this.m_updateDelegateListTime = null;
			this.m_updateDelegateListCurrentTime = null;
			result = true;
		}
		else if (this.IsUpdateCustom(updName))
		{
			this.m_updateDelegateList.Remove(updName);
			this.m_updateDelegateListTime.Remove(updName);
			this.m_updateDelegateListCurrentTime.Remove(updName);
			if (this.m_updateDelegateList.Count <= 0)
			{
				this.m_updateDelegateList = null;
				this.m_updateDelegateListTime = null;
				this.m_updateDelegateListCurrentTime = null;
			}
			result = true;
		}
		return result;
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x0016489C File Offset: 0x00162A9C
	protected bool IsUpdateCustom(string updName)
	{
		bool result = false;
		if (this.m_updateDelegateList != null)
		{
			result = this.m_updateDelegateList.ContainsKey(updName);
		}
		return result;
	}

	// Token: 0x06004563 RID: 17763 RVA: 0x001648C4 File Offset: 0x00162AC4
	protected bool AddCallback(CustomGameObject.Callback call, string callName, float callTime)
	{
		bool result = false;
		if (!this.IsCallback(callName))
		{
			if (this.m_callbackDelegateList == null)
			{
				this.m_callbackDelegateList = new Dictionary<string, CustomGameObject.Callback>();
			}
			if (this.m_callbackDelegateListCurrentTime == null)
			{
				this.m_callbackDelegateListCurrentTime = new Dictionary<string, float>();
			}
			this.m_callbackDelegateList.Add(callName, call);
			this.m_callbackDelegateListCurrentTime.Add(callName, callTime);
			result = true;
		}
		return result;
	}

	// Token: 0x06004564 RID: 17764 RVA: 0x00164928 File Offset: 0x00162B28
	protected bool RemoveCallback(string callName = null)
	{
		bool result = false;
		if (string.IsNullOrEmpty(callName))
		{
			if (this.m_callbackDelegateList != null)
			{
				this.m_callbackDelegateList.Clear();
			}
			if (this.m_callbackDelegateListCurrentTime != null)
			{
				this.m_callbackDelegateListCurrentTime.Clear();
			}
			this.m_callbackDelegateList = null;
			this.m_callbackDelegateListCurrentTime = null;
			result = true;
		}
		else if (this.IsCallback(callName))
		{
			this.m_callbackDelegateList.Remove(callName);
			this.m_callbackDelegateListCurrentTime.Remove(callName);
			result = true;
		}
		return result;
	}

	// Token: 0x06004565 RID: 17765 RVA: 0x001649AC File Offset: 0x00162BAC
	protected int RemoveCallbackPartialMatch(string callName = null)
	{
		int num = 0;
		if (this.m_callbackDelegateList != null && this.m_callbackDelegateList.Count > 0)
		{
			Dictionary<string, CustomGameObject.Callback>.KeyCollection keys = this.m_callbackDelegateList.Keys;
			List<string> list = new List<string>();
			foreach (string text in keys)
			{
				if (text.IndexOf(callName) != -1 && this.IsCallback(text))
				{
					list.Add(text);
				}
			}
			foreach (string key in list)
			{
				this.m_callbackDelegateList.Remove(key);
				this.m_callbackDelegateListCurrentTime.Remove(key);
				num++;
			}
		}
		return num;
	}

	// Token: 0x06004566 RID: 17766 RVA: 0x00164AC8 File Offset: 0x00162CC8
	protected bool IsCallback(string callName)
	{
		bool result = false;
		if (this.m_callbackDelegateList != null)
		{
			result = this.m_callbackDelegateList.ContainsKey(callName);
		}
		return result;
	}

	// Token: 0x06004567 RID: 17767 RVA: 0x00164AF0 File Offset: 0x00162CF0
	public void ResetGameObjTime()
	{
		this.m_gameObjTime = 0f;
	}

	// Token: 0x040039E0 RID: 14816
	private float m_gameObjTime;

	// Token: 0x040039E1 RID: 14817
	private float m_gameObjTimeRate = 1f;

	// Token: 0x040039E2 RID: 14818
	private float m_gameObjSleepTime;

	// Token: 0x040039E3 RID: 14819
	private bool m_updateStdLast;

	// Token: 0x040039E4 RID: 14820
	private bool m_updateCusOnly = true;

	// Token: 0x040039E5 RID: 14821
	private Dictionary<string, CustomGameObject.UpdateCustom> m_updateDelegateList;

	// Token: 0x040039E6 RID: 14822
	private Dictionary<string, float> m_updateDelegateListTime;

	// Token: 0x040039E7 RID: 14823
	private Dictionary<string, float> m_updateDelegateListCurrentTime;

	// Token: 0x040039E8 RID: 14824
	private Dictionary<string, CustomGameObject.Callback> m_callbackDelegateList;

	// Token: 0x040039E9 RID: 14825
	private Dictionary<string, float> m_callbackDelegateListCurrentTime;

	// Token: 0x02000A9B RID: 2715
	// (Invoke) Token: 0x060048A6 RID: 18598
	public delegate void UpdateCustom(string updateName, float timeRate);

	// Token: 0x02000A9C RID: 2716
	// (Invoke) Token: 0x060048AA RID: 18602
	public delegate void Callback(string callbackName);
}
