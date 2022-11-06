using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

// Token: 0x02000661 RID: 1633
public class MileageMapDataManager : MonoBehaviour
{
	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x0010AE5C File Offset: 0x0010905C
	public static MileageMapDataManager Instance
	{
		get
		{
			return MileageMapDataManager.instance;
		}
	}

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0010AE64 File Offset: 0x00109064
	// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x0010AE6C File Offset: 0x0010906C
	public int MileageStageIndex
	{
		get
		{
			return this.m_mileageStageIndex;
		}
		set
		{
			this.m_mileageStageIndex = value;
		}
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x0010AE78 File Offset: 0x00109078
	protected void Awake()
	{
		this.SetInstance();
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x0010AE80 File Offset: 0x00109080
	private void Start()
	{
		if (this.m_mileage_datas == null)
		{
			this.m_mileage_datas = new Dictionary<string, MileageMapData>();
		}
		base.enabled = false;
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x0010AEA0 File Offset: 0x001090A0
	private void OnDestroy()
	{
		if (MileageMapDataManager.instance == this)
		{
			MileageMapDataManager.instance = null;
		}
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x0010AEB8 File Offset: 0x001090B8
	private void SetInstance()
	{
		if (MileageMapDataManager.instance == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			MileageMapDataManager.instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x0010AEEC File Offset: 0x001090EC
	public void SetData(TextAsset xml_data)
	{
		string s = AESCrypt.Decrypt(xml_data.text);
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(MileageMapData[]));
		StringReader textReader = new StringReader(s);
		MileageMapData[] array = (MileageMapData[])xmlSerializer.Deserialize(textReader);
		if (array[0] != null)
		{
			int episode = array[0].scenario.episode;
			int chapter = array[0].scenario.chapter;
			string key = this.GetKey(episode, chapter);
			if (!this.IsExist(key))
			{
				if (this.m_current_key == null)
				{
					this.m_current_key = key;
				}
				this.m_mileage_datas.Add(key, array[0]);
			}
		}
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x0010AF8C File Offset: 0x0010918C
	public void SetCurrentData(int episode, int chapter)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key))
		{
			this.m_current_key = key;
		}
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x0010AFB8 File Offset: 0x001091B8
	public void DestroyData()
	{
		this.m_mileage_datas.Clear();
		this.DestroyFaceAndBGData(false);
		this.m_current_key = null;
	}

	// Token: 0x06002BFB RID: 11259 RVA: 0x0010AFD4 File Offset: 0x001091D4
	public void DestroyFaceAndBGData(bool keepFlag = false)
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "MileageMapFace");
		List<GameObject> list = new List<GameObject>();
		if (gameObject != null)
		{
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
				if (!this.m_loadingFaceList.Contains(gameObject2.name))
				{
					if (keepFlag)
					{
						if (!this.m_keepList.Contains(gameObject2.name))
						{
							list.Add(gameObject2);
						}
					}
					else
					{
						list.Add(gameObject2);
					}
				}
			}
		}
		GameObject gameObject3 = GameObjectUtil.FindChildGameObject(base.gameObject, "MileageMapBG");
		if (gameObject3 != null)
		{
			for (int j = 0; j < gameObject3.transform.childCount; j++)
			{
				GameObject gameObject4 = gameObject3.transform.GetChild(j).gameObject;
				if (keepFlag)
				{
					if (!this.m_keepList.Contains(gameObject4.name))
					{
						list.Add(gameObject4);
					}
				}
				else
				{
					list.Add(gameObject4);
				}
			}
		}
		foreach (GameObject obj in list)
		{
			UnityEngine.Object.Destroy(obj);
		}
		if (!keepFlag)
		{
			this.m_keepList.Clear();
		}
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x0010B16C File Offset: 0x0010936C
	public MileageMapData GetMileageMapData()
	{
		if (this.IsExist(this.m_current_key))
		{
			return this.m_mileage_datas[this.m_current_key];
		}
		return null;
	}

	// Token: 0x06002BFD RID: 11261 RVA: 0x0010B1A0 File Offset: 0x001093A0
	public ServerMileageReward GetMileageReward(int episode, int chapter, int point)
	{
		foreach (ServerMileageReward serverMileageReward in this.m_rewardList)
		{
			if (serverMileageReward.m_episode == episode && serverMileageReward.m_chapter == chapter && serverMileageReward.m_point == point)
			{
				return serverMileageReward;
			}
		}
		return null;
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x0010B230 File Offset: 0x00109430
	public MileageMapData GetMileageMapData(int episode, int chapter)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key))
		{
			return this.m_mileage_datas[key];
		}
		return null;
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x0010B260 File Offset: 0x00109460
	public int GetRouteId(int episode, int chapter, int point)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key) && point < 5)
		{
			return this.m_mileage_datas[key].map_data.route_data[point].id;
		}
		return -1;
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x0010B2AC File Offset: 0x001094AC
	public void SetPointIncentiveData(int episode, int chapter, int point, RewardData src_reward)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key))
		{
			int num = this.m_mileage_datas[key].event_data.point.Length;
			if (point < num)
			{
				this.m_mileage_datas[key].event_data.point[point].reward.Set(src_reward);
			}
		}
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x0010B314 File Offset: 0x00109514
	public void SetChapterIncentiveData(int episode, int chapter, int index, RewardData src_reward)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key) && this.m_mileage_datas[key].map_data.reward != null)
		{
			int num = this.m_mileage_datas[key].map_data.reward.Length;
			if (index < num)
			{
				this.m_mileage_datas[key].map_data.reward[index].Set(src_reward);
			}
		}
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x0010B394 File Offset: 0x00109594
	public void SetEpisodeIncentiveData(int episode, int chapter, int index, RewardData src_reward)
	{
		string key = this.GetKey(episode, chapter);
		if (this.IsExist(key) && this.m_mileage_datas[key].scenario.reward != null)
		{
			int num = this.m_mileage_datas[key].scenario.reward.Length;
			if (index < num)
			{
				this.m_mileage_datas[key].scenario.reward[index].Set(src_reward);
			}
		}
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x0010B414 File Offset: 0x00109614
	public void SetRewardData(List<ServerMileageReward> rewardList)
	{
		this.m_rewardList.Clear();
		if (rewardList != null)
		{
			foreach (ServerMileageReward item in rewardList)
			{
				this.m_rewardList.Add(item);
			}
		}
	}

	// Token: 0x06002C04 RID: 11268 RVA: 0x0010B48C File Offset: 0x0010968C
	public bool IsExist(string key)
	{
		return !string.IsNullOrEmpty(key) && this.m_mileage_datas != null && this.m_mileage_datas.ContainsKey(key);
	}

	// Token: 0x06002C05 RID: 11269 RVA: 0x0010B4C0 File Offset: 0x001096C0
	public bool IsExist(int episode, int chapter)
	{
		string key = this.GetKey(episode, chapter);
		return this.IsExist(key);
	}

	// Token: 0x06002C06 RID: 11270 RVA: 0x0010B4E0 File Offset: 0x001096E0
	private string GetKey(int episode, int chapter)
	{
		return episode.ToString("000") + chapter.ToString("00");
	}

	// Token: 0x06002C07 RID: 11271 RVA: 0x0010B500 File Offset: 0x00109700
	public void AddKeepList(string name)
	{
		foreach (string b in this.m_keepList)
		{
			if (name == b)
			{
				return;
			}
		}
		this.m_keepList.Add(name);
	}

	// Token: 0x06002C08 RID: 11272 RVA: 0x0010B57C File Offset: 0x0010977C
	public void SetLoadingFaceId(List<int> loadingList)
	{
		this.m_loadingFaceList.Clear();
		foreach (int face_id in loadingList)
		{
			this.m_loadingFaceList.Add(MileageMapUtility.GetFaceTextureName(face_id));
		}
	}

	// Token: 0x040028C5 RID: 10437
	private Dictionary<string, MileageMapData> m_mileage_datas;

	// Token: 0x040028C6 RID: 10438
	private string m_current_key;

	// Token: 0x040028C7 RID: 10439
	private int m_mileageStageIndex = 1;

	// Token: 0x040028C8 RID: 10440
	private List<string> m_loadingFaceList = new List<string>();

	// Token: 0x040028C9 RID: 10441
	private List<string> m_keepList = new List<string>();

	// Token: 0x040028CA RID: 10442
	private List<ServerMileageReward> m_rewardList = new List<ServerMileageReward>();

	// Token: 0x040028CB RID: 10443
	private static MileageMapDataManager instance;
}
