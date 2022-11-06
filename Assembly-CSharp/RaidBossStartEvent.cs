using System;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class RaidBossStartEvent : MonoBehaviour
{
	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0005938C File Offset: 0x0005758C
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00059394 File Offset: 0x00057594
	private void Start()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x000593A4 File Offset: 0x000575A4
	private void OnDestroy()
	{
		this.SetColision(false);
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
		if (this.m_windowEventData != null)
		{
			this.m_windowEventData = null;
		}
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000593D8 File Offset: 0x000575D8
	public static RaidBossStartEvent Create(GameObject obj, RaidBossStartEvent.ProductType type)
	{
		if (obj != null)
		{
			RaidBossStartEvent raidBossStartEvent = obj.GetComponent<RaidBossStartEvent>();
			if (raidBossStartEvent == null)
			{
				raidBossStartEvent = obj.AddComponent<RaidBossStartEvent>();
			}
			else if (GeneralWindow.IsCreated("RaidBossStartEvent"))
			{
				GeneralWindow.Close();
			}
			if (raidBossStartEvent != null)
			{
				raidBossStartEvent.ResetParam(type);
			}
			return raidBossStartEvent;
		}
		return null;
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0005943C File Offset: 0x0005763C
	public void CloseWindow()
	{
		this.SetColision(false);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00059448 File Offset: 0x00057648
	private bool StartEvent()
	{
		this.m_windowEventData = EventManager.Instance.GetWindowEvenData(this.m_textWindowId);
		this.m_isNotPlaybackDefaultBgm = false;
		this.m_isEnd = false;
		if (this.m_windowEventData != null)
		{
			GeneralWindow.CInfo.Event[] array = new GeneralWindow.CInfo.Event[this.m_windowEventData.body.Length];
			for (int i = 0; i < this.m_windowEventData.body.Length; i++)
			{
				WindowBodyData windowBodyData = this.m_windowEventData.body[i];
				GeneralWindow.CInfo.Event.FaceWindow[] array2 = null;
				if (windowBodyData.product != null)
				{
					array2 = new GeneralWindow.CInfo.Event.FaceWindow[windowBodyData.product.Length];
					for (int j = 0; j < windowBodyData.product.Length; j++)
					{
						WindowProductData windowProductData = windowBodyData.product[j];
						array2[j] = new GeneralWindow.CInfo.Event.FaceWindow
						{
							texture = this.GetTexture(windowProductData.face_id),
							name = ((windowProductData.name_cell_id == null) ? null : MileageMapText.GetName(windowProductData.name_cell_id)),
							effectType = windowProductData.effect,
							animType = windowProductData.anim,
							reverseType = windowProductData.reverse,
							showingType = windowProductData.showing
						};
					}
				}
				array[i] = new GeneralWindow.CInfo.Event
				{
					faceWindows = array2,
					arrowType = windowBodyData.arrow,
					bgmCueName = windowBodyData.bgm,
					seCueName = windowBodyData.se,
					message = this.GetText(windowBodyData.text_cell_id)
				};
			}
			GeneralWindow.Create(new GeneralWindow.CInfo
			{
				name = "RaidBossStartEvent",
				buttonType = GeneralWindow.ButtonType.OkNextSkip,
				caption = MileageMapText.GetMapCommonText(this.m_windowEventData.title_cell_id),
				events = array,
				isNotPlaybackDefaultBgm = this.m_isNotPlaybackDefaultBgm,
				isSpecialEvent = true
			});
			return true;
		}
		return false;
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00059638 File Offset: 0x00057838
	private string GetText(string cellID)
	{
		string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, "Production", cellID);
		if (text == null)
		{
			text = "NoText";
		}
		return text;
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00059660 File Offset: 0x00057860
	private bool LoadTexture()
	{
		this.m_loadTexList.Clear();
		this.m_loadTexList = this.GetResourceNameList();
		if (this.m_loadTexList.Count > 0)
		{
			this.CreateLoadObject();
			if (this.m_sceneLoader != null)
			{
				foreach (string name in this.m_loadTexList)
				{
					ResourceSceneLoader.ResourceInfo info = new ResourceSceneLoader.ResourceInfo(ResourceCategory.EVENT_RESOURCE, name, true, false, false, null, false);
					this.m_sceneLoader.AddLoadAndResourceManager(info);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0005971C File Offset: 0x0005791C
	private void DestroyTextureData()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "FaceTextures");
		if (gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
			Resources.UnloadUnusedAssets();
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00059754 File Offset: 0x00057954
	private void CreateLoadObject()
	{
		GameObject gameObject = new GameObject("SceneLoader");
		if (gameObject != null)
		{
			this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
		}
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00059784 File Offset: 0x00057984
	private Texture GetTexture(int id)
	{
		string faceTextureName = MileageMapUtility.GetFaceTextureName(id);
		GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, faceTextureName);
		if (!(gameObject != null))
		{
			return MileageMapUtility.GetFaceTexture(id);
		}
		AssetBundleTexture component = gameObject.GetComponent<AssetBundleTexture>();
		if (component != null)
		{
			return component.m_tex;
		}
		return null;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000597DC File Offset: 0x000579DC
	private List<string> GetResourceNameList()
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		if (EventManager.Instance != null)
		{
			WindowEventData windowEvenData = EventManager.Instance.GetWindowEvenData(this.m_textWindowId);
			if (windowEvenData != null)
			{
				foreach (WindowBodyData windowBodyData in windowEvenData.body)
				{
					for (int j = 0; j < windowBodyData.face_count; j++)
					{
						if (j < windowBodyData.product.Length && !list2.Contains(windowBodyData.product[j].face_id))
						{
							list2.Add(windowBodyData.product[j].face_id);
						}
					}
				}
			}
		}
		if (list2.Count > 0)
		{
			foreach (int face_id in list2)
			{
				Texture faceTexture = MileageMapUtility.GetFaceTexture(face_id);
				if (faceTexture == null)
				{
					string faceTextureName = MileageMapUtility.GetFaceTextureName(face_id);
					GameObject gameObject = ResourceManager.Instance.GetGameObject(ResourceCategory.EVENT_RESOURCE, faceTextureName);
					if (gameObject == null)
					{
						list.Add(faceTextureName);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0005993C File Offset: 0x00057B3C
	private int GetTextWindowId()
	{
		if (EventManager.Instance != null)
		{
			EventRaidProductionData raidProductionData = EventManager.Instance.GetRaidProductionData();
			if (raidProductionData != null)
			{
				if (this.m_productType == RaidBossStartEvent.ProductType.MileagePage)
				{
					EventProductionData mileagePage = raidProductionData.mileagePage;
					if (mileagePage != null && this.m_productProgress < mileagePage.textWindowId.Length)
					{
						return mileagePage.textWindowId[this.m_productProgress];
					}
				}
				else if (this.m_firstBattle)
				{
					EventProductionData firstBattle = raidProductionData.firstBattle;
					if (firstBattle != null && 0 < firstBattle.textWindowId.Length)
					{
						return firstBattle.textWindowId[0];
					}
				}
				else
				{
					EventProductionData eventTop = raidProductionData.eventTop;
					if (eventTop != null && this.m_productProgress < eventTop.textWindowId.Length)
					{
						return eventTop.textWindowId[this.m_productProgress];
					}
				}
			}
		}
		return -1;
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00059A0C File Offset: 0x00057C0C
	public void SaveEventTopPagePictureEvent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.pictureShowEventId = EventManager.Instance.Id;
				systemdata.pictureShowProgress = this.m_productProgress;
			}
			instance.SaveSystemData();
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00059A5C File Offset: 0x00057C5C
	public void SaveMileagePagePictureEvent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.pictureShowEventId = EventManager.Instance.Id;
				systemdata.pictureShowEmergeRaidBossProgress = this.m_productProgress;
			}
			instance.SaveSystemData();
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00059AAC File Offset: 0x00057CAC
	public static bool IsTopMenuProduction()
	{
		if (EventManager.Instance != null && SystemSaveManager.Instance)
		{
			ServerEventUserRaidBossState raidBossState = EventManager.Instance.RaidBossState;
			SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
			if (systemdata == null || raidBossState == null)
			{
				return false;
			}
			EventRaidProductionData raidProductionData = EventManager.Instance.GetRaidProductionData();
			if (raidProductionData != null)
			{
				EventProductionData mileagePage = raidProductionData.mileagePage;
				if (mileagePage != null)
				{
					int pictureShowEmergeRaidBossProgress = systemdata.pictureShowEmergeRaidBossProgress;
					int numRaidBossEncountered = raidBossState.NumRaidBossEncountered;
					int numBeatedEncounter = raidBossState.NumBeatedEncounter;
					for (int i = 0; i < mileagePage.startCollectCount.Length; i++)
					{
						int num = mileagePage.startCollectCount[i];
						if (numRaidBossEncountered >= num && numBeatedEncounter >= num - 1 && i > pictureShowEmergeRaidBossProgress)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00059B7C File Offset: 0x00057D7C
	private int GetNextWindowProductIndex(RaidBossStartEvent.ProductType type, ref bool firstBattle)
	{
		int result = -1;
		if (EventManager.Instance != null && !EventManager.Instance.IsChallengeEvent())
		{
			return result;
		}
		if (SystemSaveManager.Instance == null || EventManager.Instance == null)
		{
			return result;
		}
		SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
		EventRaidProductionData raidProductionData = EventManager.Instance.GetRaidProductionData();
		ServerEventUserRaidBossState raidBossState = EventManager.Instance.RaidBossState;
		if (systemdata == null || raidProductionData == null || raidBossState == null)
		{
			return result;
		}
		if (EventManager.Instance.UserRaidBossList == null)
		{
			return result;
		}
		int numRaidBossEncountered = raidBossState.NumRaidBossEncountered;
		int numBeatedEncounter = raidBossState.NumBeatedEncounter;
		if (type == RaidBossStartEvent.ProductType.MileagePage)
		{
			EventProductionData mileagePage = raidProductionData.mileagePage;
			if (mileagePage != null)
			{
				int pictureShowEmergeRaidBossProgress = systemdata.pictureShowEmergeRaidBossProgress;
				for (int i = 0; i < mileagePage.startCollectCount.Length; i++)
				{
					int num = mileagePage.startCollectCount[i];
					if (numRaidBossEncountered >= num && numBeatedEncounter >= num - 1 && i > pictureShowEmergeRaidBossProgress)
					{
						return i;
					}
				}
			}
		}
		else if (type == RaidBossStartEvent.ProductType.EventTopPage)
		{
			if (systemdata.pictureShowRaidBossFirstBattle == 1)
			{
				firstBattle = true;
				return 0;
			}
			EventProductionData eventTop = raidProductionData.eventTop;
			if (eventTop != null)
			{
				int pictureShowProgress = systemdata.pictureShowProgress;
				for (int j = 0; j < eventTop.startCollectCount.Length; j++)
				{
					int num2 = eventTop.startCollectCount[j];
					if (numRaidBossEncountered >= num2 && numBeatedEncounter >= num2 - 1 && j > pictureShowProgress)
					{
						return j;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00059D10 File Offset: 0x00057F10
	public void SavePictureEvent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				RaidBossStartEvent.ProductType productType = this.m_productType;
				if (productType != RaidBossStartEvent.ProductType.MileagePage)
				{
					if (productType == RaidBossStartEvent.ProductType.EventTopPage)
					{
						if (this.m_firstBattle)
						{
							systemdata.pictureShowEventId = EventManager.Instance.Id;
							systemdata.pictureShowRaidBossFirstBattle = 0;
						}
						else
						{
							systemdata.pictureShowEventId = EventManager.Instance.Id;
							systemdata.pictureShowProgress = this.m_productProgress;
						}
					}
				}
				else
				{
					systemdata.pictureShowEventId = EventManager.Instance.Id;
					systemdata.pictureShowEmergeRaidBossProgress = this.m_productProgress;
				}
			}
			instance.SaveSystemData();
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00059DC4 File Offset: 0x00057FC4
	public void Update()
	{
		switch (this.m_mode)
		{
		case RaidBossStartEvent.Mode.Idle:
			base.enabled = false;
			break;
		case RaidBossStartEvent.Mode.CheckId:
			if (this.m_productProgress == -1)
			{
				this.m_mode = RaidBossStartEvent.Mode.End;
			}
			else
			{
				this.m_textWindowId = this.GetTextWindowId();
				if (!this.m_commonResourceLoaded)
				{
					GameObjectUtil.SendMessageFindGameObject("MainMenuButtonEvent", "OnMenuEventClicked", base.gameObject, SendMessageOptions.DontRequireReceiver);
				}
				this.m_mode = RaidBossStartEvent.Mode.WaitCommonResource;
			}
			break;
		case RaidBossStartEvent.Mode.WaitCommonResource:
			if (this.m_commonResourceLoaded)
			{
				if (this.LoadTexture())
				{
					this.m_mode = RaidBossStartEvent.Mode.WaitLoad;
				}
				else
				{
					this.m_mode = RaidBossStartEvent.Mode.Start;
				}
			}
			break;
		case RaidBossStartEvent.Mode.Load:
			this.SetColision(true);
			this.m_productProgress = this.GetNextWindowProductIndex(this.m_productType, ref this.m_firstBattle);
			this.m_mode = RaidBossStartEvent.Mode.CheckId;
			break;
		case RaidBossStartEvent.Mode.WaitLoad:
			if (this.m_sceneLoader != null && this.m_sceneLoader.Loaded)
			{
				UnityEngine.Object.Destroy(this.m_sceneLoader.gameObject);
				this.m_mode = RaidBossStartEvent.Mode.Start;
			}
			break;
		case RaidBossStartEvent.Mode.Start:
			if (!RaidBossWindow.IsOpenAdvent())
			{
				if (this.StartEvent())
				{
					this.m_mode = RaidBossStartEvent.Mode.WaitEnd;
				}
				else
				{
					this.m_mode = RaidBossStartEvent.Mode.End;
				}
			}
			break;
		case RaidBossStartEvent.Mode.WaitEnd:
			if (GeneralWindow.IsCreated("RaidBossStartEvent") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.DestroyTextureData();
				this.SavePictureEvent();
				this.m_firstBattle = false;
				this.m_productProgress = this.GetNextWindowProductIndex(this.m_productType, ref this.m_firstBattle);
				this.m_mode = RaidBossStartEvent.Mode.CheckId;
			}
			break;
		case RaidBossStartEvent.Mode.Next:
			this.m_mode = RaidBossStartEvent.Mode.Load;
			break;
		case RaidBossStartEvent.Mode.End:
			this.m_isEnd = true;
			this.m_mode = RaidBossStartEvent.Mode.Idle;
			break;
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00059FA0 File Offset: 0x000581A0
	private void ResetParam(RaidBossStartEvent.ProductType type)
	{
		this.m_mode = RaidBossStartEvent.Mode.Load;
		this.m_productType = type;
		this.SetColision(true);
		if (type == RaidBossStartEvent.ProductType.EventTopPage)
		{
			this.m_commonResourceLoaded = true;
		}
		this.m_isEnd = false;
		base.enabled = true;
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00059FD4 File Offset: 0x000581D4
	private void SetColision(bool flag)
	{
		if (flag)
		{
			if (!this.m_alertFlag)
			{
				HudMenuUtility.SetConnectAlertMenuButtonUI(true);
			}
		}
		else if (this.m_alertFlag)
		{
			HudMenuUtility.SetConnectAlertMenuButtonUI(false);
		}
		this.m_alertFlag = flag;
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0005A018 File Offset: 0x00058218
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!this.m_isEnd && msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0005A034 File Offset: 0x00058234
	public void OnButtonEventCallBack()
	{
		this.m_commonResourceLoaded = true;
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0005A040 File Offset: 0x00058240
	public static List<int> GetProductFaceIdList(bool mileageProduct)
	{
		if (SystemSaveManager.Instance == null || EventManager.Instance == null)
		{
			return null;
		}
		SystemData systemdata = SystemSaveManager.Instance.GetSystemdata();
		EventRaidProductionData raidProductionData = EventManager.Instance.GetRaidProductionData();
		ServerEventUserRaidBossState raidBossState = EventManager.Instance.RaidBossState;
		if (systemdata == null || raidProductionData == null || raidBossState == null)
		{
			return null;
		}
		if (EventManager.Instance.UserRaidBossList == null)
		{
			return null;
		}
		List<int> list = new List<int>();
		int numRaidBossEncountered = raidBossState.NumRaidBossEncountered;
		int numBeatedEncounter = raidBossState.NumBeatedEncounter;
		if (mileageProduct)
		{
			EventProductionData mileagePage = raidProductionData.mileagePage;
			if (mileagePage != null)
			{
				int pictureShowEmergeRaidBossProgress = systemdata.pictureShowEmergeRaidBossProgress;
				for (int i = 0; i < mileagePage.startCollectCount.Length; i++)
				{
					int num = mileagePage.startCollectCount[i];
					if (numRaidBossEncountered >= num && numBeatedEncounter >= num - 1 && i > pictureShowEmergeRaidBossProgress)
					{
						list.Add(mileagePage.textWindowId[i]);
					}
				}
			}
		}
		EventProductionData firstBattle = raidProductionData.firstBattle;
		if (firstBattle != null && systemdata.pictureShowRaidBossFirstBattle == 1)
		{
			list.Add(firstBattle.textWindowId[0]);
		}
		EventProductionData eventTop = raidProductionData.eventTop;
		int pictureShowProgress = systemdata.pictureShowProgress;
		for (int j = 0; j < eventTop.startCollectCount.Length; j++)
		{
			int num2 = eventTop.startCollectCount[j];
			if (numRaidBossEncountered >= num2 && numBeatedEncounter >= num2 - 1 && j > pictureShowProgress)
			{
				list.Add(eventTop.textWindowId[j]);
			}
		}
		List<int> list2 = new List<int>();
		if (list.Count > 0 && EventManager.Instance != null)
		{
			foreach (int texWindowId in list)
			{
				WindowEventData windowEvenData = EventManager.Instance.GetWindowEvenData(texWindowId);
				if (windowEvenData != null)
				{
					foreach (WindowBodyData windowBodyData in windowEvenData.body)
					{
						for (int l = 0; l < windowBodyData.face_count; l++)
						{
							if (l < windowBodyData.product.Length && !list2.Contains(windowBodyData.product[l].face_id))
							{
								list2.Add(windowBodyData.product[l].face_id);
							}
						}
					}
				}
			}
		}
		return list2;
	}

	// Token: 0x04000D2B RID: 3371
	private const string WINDOW_NAME = "RaidBossStartEvent";

	// Token: 0x04000D2C RID: 3372
	private RaidBossStartEvent.Mode m_mode;

	// Token: 0x04000D2D RID: 3373
	private ResourceSceneLoader m_sceneLoader;

	// Token: 0x04000D2E RID: 3374
	private int m_productProgress = -1;

	// Token: 0x04000D2F RID: 3375
	private int m_textWindowId = -1;

	// Token: 0x04000D30 RID: 3376
	private List<string> m_loadTexList = new List<string>();

	// Token: 0x04000D31 RID: 3377
	private WindowEventData m_windowEventData;

	// Token: 0x04000D32 RID: 3378
	private RaidBossStartEvent.ProductType m_productType;

	// Token: 0x04000D33 RID: 3379
	private bool m_isNotPlaybackDefaultBgm;

	// Token: 0x04000D34 RID: 3380
	private bool m_isEnd;

	// Token: 0x04000D35 RID: 3381
	private bool m_alertFlag;

	// Token: 0x04000D36 RID: 3382
	private bool m_firstBattle;

	// Token: 0x04000D37 RID: 3383
	private bool m_commonResourceLoaded;

	// Token: 0x02000233 RID: 563
	public enum ProductType
	{
		// Token: 0x04000D39 RID: 3385
		MileagePage,
		// Token: 0x04000D3A RID: 3386
		EventTopPage
	}

	// Token: 0x02000234 RID: 564
	private enum Mode
	{
		// Token: 0x04000D3C RID: 3388
		Idle,
		// Token: 0x04000D3D RID: 3389
		CheckId,
		// Token: 0x04000D3E RID: 3390
		WaitCommonResource,
		// Token: 0x04000D3F RID: 3391
		Load,
		// Token: 0x04000D40 RID: 3392
		WaitLoad,
		// Token: 0x04000D41 RID: 3393
		Start,
		// Token: 0x04000D42 RID: 3394
		WaitEnd,
		// Token: 0x04000D43 RID: 3395
		Next,
		// Token: 0x04000D44 RID: 3396
		End
	}
}
