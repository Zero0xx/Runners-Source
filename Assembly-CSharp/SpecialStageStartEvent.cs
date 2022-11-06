using System;
using System.Collections.Generic;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class SpecialStageStartEvent : MonoBehaviour
{
	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0005CB94 File Offset: 0x0005AD94
	public bool IsEnd
	{
		get
		{
			return this.m_isEnd;
		}
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0005CB9C File Offset: 0x0005AD9C
	private void Start()
	{
		BackKeyManager.AddWindowCallBack(base.gameObject);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x0005CBAC File Offset: 0x0005ADAC
	private void OnDestroy()
	{
		this.SetColision(false);
		BackKeyManager.RemoveWindowCallBack(base.gameObject);
		if (this.m_windowEventData != null)
		{
			this.m_windowEventData = null;
		}
		if (this.m_dailyWindowUI != null)
		{
			this.m_dailyWindowUI = null;
		}
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0005CBF8 File Offset: 0x0005ADF8
	public static SpecialStageStartEvent Create(GameObject obj)
	{
		if (obj != null)
		{
			SpecialStageStartEvent specialStageStartEvent = obj.GetComponent<SpecialStageStartEvent>();
			if (specialStageStartEvent == null)
			{
				specialStageStartEvent = obj.AddComponent<SpecialStageStartEvent>();
			}
			else if (GeneralWindow.IsCreated("SpecialStageStartEvent"))
			{
				GeneralWindow.Close();
			}
			if (specialStageStartEvent != null)
			{
				specialStageStartEvent.m_mode = SpecialStageStartEvent.Mode.Load;
				specialStageStartEvent.SetColision(true);
			}
			return specialStageStartEvent;
		}
		return null;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x0005CC64 File Offset: 0x0005AE64
	public void CloseWindow()
	{
		this.SetColision(false);
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x0005CC70 File Offset: 0x0005AE70
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
				name = "SpecialStageStartEvent",
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

	// Token: 0x06000FBF RID: 4031 RVA: 0x0005CE60 File Offset: 0x0005B060
	private string GetText(string cellID)
	{
		string text = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_SPECIFIC, "Production", cellID);
		if (text == null)
		{
			text = "NoText";
		}
		return text;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0005CE88 File Offset: 0x0005B088
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

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0005CF44 File Offset: 0x0005B144
	private void SetTextureData()
	{
		GameObject gameObject = new GameObject("FaceTextures");
		if (gameObject != null)
		{
			if (gameObject != null)
			{
				gameObject.transform.parent = base.transform;
			}
			foreach (string name in this.m_loadTexList)
			{
				GameObject gameObject2 = GameObject.Find(name);
				if (gameObject2 != null)
				{
					gameObject2.transform.parent = gameObject.transform;
				}
			}
		}
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0005CFFC File Offset: 0x0005B1FC
	private void DestroyTextureData()
	{
		GameObject gameObject = GameObjectUtil.FindChildGameObject(base.gameObject, "FaceTextures");
		if (gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
			Resources.UnloadUnusedAssets();
		}
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0005D034 File Offset: 0x0005B234
	private void CreateLoadObject()
	{
		GameObject gameObject = new GameObject("SceneLoader");
		if (gameObject != null)
		{
			this.m_sceneLoader = gameObject.AddComponent<ResourceSceneLoader>();
		}
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x0005D064 File Offset: 0x0005B264
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

	// Token: 0x06000FC5 RID: 4037 RVA: 0x0005D0BC File Offset: 0x0005B2BC
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

	// Token: 0x06000FC6 RID: 4038 RVA: 0x0005D21C File Offset: 0x0005B41C
	private string GetResourceName()
	{
		return "EventResourcePictureCardShowTextures" + this.m_textWindowId.ToString("D2");
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0005D238 File Offset: 0x0005B438
	public void SavePictureEvent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				systemdata.pictureShowEventId = EventManager.Instance.Id;
				systemdata.pictureShowProgress = this.m_textWindowId;
			}
			instance.SaveSystemData();
		}
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0005D288 File Offset: 0x0005B488
	public void StartPlayDailyMissionResult()
	{
		GameObject menuAnimUIObject = HudMenuUtility.GetMenuAnimUIObject();
		if (menuAnimUIObject != null)
		{
			this.m_dailyWindowUI = GameObjectUtil.FindChildGameObjectComponent<DailyWindowUI>(menuAnimUIObject, "DailyWindowUI");
			if (this.m_dailyWindowUI != null)
			{
				this.m_dailyWindowUI.gameObject.SetActive(true);
				this.m_dailyWindowUI.PlayStart();
			}
		}
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0005D2E8 File Offset: 0x0005B4E8
	private int GetNextWindowId()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null && EventManager.Instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			EventProductionData puductionData = EventManager.Instance.GetPuductionData();
			if (systemdata != null && puductionData != null)
			{
				int pictureShowProgress = systemdata.pictureShowProgress;
				for (int i = 0; i < puductionData.startCollectCount.Length; i++)
				{
					int num = puductionData.startCollectCount[i];
					if (EventManager.Instance.CollectCount >= (long)num && i > pictureShowProgress)
					{
						return i;
					}
				}
			}
		}
		return -1;
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x0005D384 File Offset: 0x0005B584
	public void Update()
	{
		switch (this.m_mode)
		{
		case SpecialStageStartEvent.Mode.Idle:
			base.enabled = false;
			break;
		case SpecialStageStartEvent.Mode.Load:
			this.SetColision(true);
			this.m_textWindowId = this.GetNextWindowId();
			if (this.m_textWindowId == -1)
			{
				if (this.IsDailyMissionComplete())
				{
					this.StartPlayDailyMissionResult();
					this.m_mode = SpecialStageStartEvent.Mode.DailyMission;
				}
				else
				{
					this.m_mode = SpecialStageStartEvent.Mode.End;
				}
			}
			else if (this.LoadTexture())
			{
				this.m_mode = SpecialStageStartEvent.Mode.WaitLoad;
			}
			else
			{
				this.m_mode = SpecialStageStartEvent.Mode.Start;
			}
			break;
		case SpecialStageStartEvent.Mode.WaitLoad:
			if (this.m_sceneLoader != null && this.m_sceneLoader.Loaded)
			{
				this.SetTextureData();
				UnityEngine.Object.Destroy(this.m_sceneLoader.gameObject);
				this.m_mode = SpecialStageStartEvent.Mode.Start;
			}
			break;
		case SpecialStageStartEvent.Mode.Start:
			if (this.StartEvent())
			{
				this.m_mode = SpecialStageStartEvent.Mode.WaitEnd;
			}
			else
			{
				this.m_mode = SpecialStageStartEvent.Mode.End;
			}
			break;
		case SpecialStageStartEvent.Mode.WaitEnd:
			if (GeneralWindow.IsCreated("SpecialStageStartEvent") && GeneralWindow.IsButtonPressed)
			{
				GeneralWindow.Close();
				this.DestroyTextureData();
				this.SavePictureEvent();
				if (SpecialStageStartEvent.IsPictureEvent())
				{
					this.m_mode = SpecialStageStartEvent.Mode.NextPicture;
				}
				else if (this.IsDailyMissionComplete())
				{
					this.StartPlayDailyMissionResult();
					this.m_mode = SpecialStageStartEvent.Mode.DailyMission;
				}
				else
				{
					this.m_mode = SpecialStageStartEvent.Mode.End;
				}
			}
			break;
		case SpecialStageStartEvent.Mode.NextPicture:
			this.m_mode = SpecialStageStartEvent.Mode.Load;
			break;
		case SpecialStageStartEvent.Mode.DailyMission:
			if (this.m_dailyWindowUI != null && this.m_dailyWindowUI.IsEnd)
			{
				this.m_dailyWindowUI = null;
				this.m_mode = SpecialStageStartEvent.Mode.End;
			}
			break;
		case SpecialStageStartEvent.Mode.End:
			this.m_isEnd = true;
			this.m_mode = SpecialStageStartEvent.Mode.Idle;
			break;
		}
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x0005D560 File Offset: 0x0005B760
	private void SetColision(bool flag)
	{
		if (flag)
		{
			if (!this.m_alertFlag)
			{
				HudMenuUtility.SetConnectAlertSimpleUI(true);
			}
		}
		else if (this.m_alertFlag)
		{
			HudMenuUtility.SetConnectAlertSimpleUI(false);
		}
		this.m_alertFlag = flag;
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x0005D5A4 File Offset: 0x0005B7A4
	private bool IsDailyMissionComplete()
	{
		return false;
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x0005D5A8 File Offset: 0x0005B7A8
	public void OnClickPlatformBackButton(WindowBase.BackButtonMessage msg)
	{
		if (!this.m_isEnd && msg != null)
		{
			msg.StaySequence();
		}
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
	public static bool IsPictureEvent()
	{
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null && EventManager.Instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				if (systemdata.pictureShowEventId != EventManager.Instance.Id)
				{
					return true;
				}
				int pictureShowProgress = systemdata.pictureShowProgress;
				EventProductionData puductionData = EventManager.Instance.GetPuductionData();
				if (puductionData != null)
				{
					int num = 0;
					foreach (int num2 in puductionData.startCollectCount)
					{
						if (EventManager.Instance.CollectCount >= (long)num2 && num > pictureShowProgress)
						{
							return true;
						}
						num++;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x0005D680 File Offset: 0x0005B880
	public static List<int> GetProductFaceIdList()
	{
		List<int> list = new List<int>();
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null && EventManager.Instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			EventProductionData puductionData = EventManager.Instance.GetPuductionData();
			if (systemdata != null && puductionData != null)
			{
				int pictureShowProgress = systemdata.pictureShowProgress;
				for (int i = 0; i < puductionData.startCollectCount.Length; i++)
				{
					int num = puductionData.startCollectCount[i];
					if (EventManager.Instance.CollectCount >= (long)num && i > pictureShowProgress)
					{
						list.Add(i);
					}
				}
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
						for (int k = 0; k < windowBodyData.face_count; k++)
						{
							if (k < windowBodyData.product.Length && !list2.Contains(windowBodyData.product[k].face_id))
							{
								list2.Add(windowBodyData.product[k].face_id);
							}
						}
					}
				}
			}
		}
		return list2;
	}

	// Token: 0x04000D81 RID: 3457
	private SpecialStageStartEvent.Mode m_mode;

	// Token: 0x04000D82 RID: 3458
	private ResourceSceneLoader m_sceneLoader;

	// Token: 0x04000D83 RID: 3459
	private int m_textWindowId = -1;

	// Token: 0x04000D84 RID: 3460
	private List<string> m_loadTexList = new List<string>();

	// Token: 0x04000D85 RID: 3461
	private WindowEventData m_windowEventData;

	// Token: 0x04000D86 RID: 3462
	private DailyWindowUI m_dailyWindowUI;

	// Token: 0x04000D87 RID: 3463
	private bool m_isNotPlaybackDefaultBgm;

	// Token: 0x04000D88 RID: 3464
	private bool m_isEnd;

	// Token: 0x04000D89 RID: 3465
	private bool m_alertFlag;

	// Token: 0x0200023A RID: 570
	private enum Mode
	{
		// Token: 0x04000D8B RID: 3467
		Idle,
		// Token: 0x04000D8C RID: 3468
		Load,
		// Token: 0x04000D8D RID: 3469
		WaitLoad,
		// Token: 0x04000D8E RID: 3470
		Start,
		// Token: 0x04000D8F RID: 3471
		WaitEnd,
		// Token: 0x04000D90 RID: 3472
		NextPicture,
		// Token: 0x04000D91 RID: 3473
		DailyMission,
		// Token: 0x04000D92 RID: 3474
		End
	}
}
