using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SaveData;
using UnityEngine;

// Token: 0x02000A14 RID: 2580
public class SoundManager : MonoBehaviour
{
	// Token: 0x17000941 RID: 2369
	// (get) Token: 0x06004438 RID: 17464 RVA: 0x00160158 File Offset: 0x0015E358
	// (set) Token: 0x06004439 RID: 17465 RVA: 0x0016018C File Offset: 0x0015E38C
	public static float BgmVolume
	{
		get
		{
			return (!(SoundManager.s_instance != null)) ? 0f : SoundManager.s_instance.m_bgmVolume;
		}
		set
		{
			if (SoundManager.s_instance != null)
			{
				SoundManager.s_instance.m_bgmVolume = value;
				for (SoundManager.SourceId sourceId = SoundManager.SourceId.BGM_NORMAL; sourceId < SoundManager.SourceId.SE; sourceId++)
				{
					SoundManager.GetSource(sourceId).masterVolume = value;
				}
			}
		}
	}

	// Token: 0x17000942 RID: 2370
	// (get) Token: 0x0600443A RID: 17466 RVA: 0x001601D4 File Offset: 0x0015E3D4
	// (set) Token: 0x0600443B RID: 17467 RVA: 0x00160208 File Offset: 0x0015E408
	public static float SeVolume
	{
		get
		{
			return (!(SoundManager.s_instance != null)) ? 0f : SoundManager.s_instance.m_seVolume;
		}
		set
		{
			if (SoundManager.s_instance != null)
			{
				SoundManager.s_instance.m_seVolume = value;
				SoundManager.GetSource(SoundManager.SourceId.SE).masterVolume = value;
			}
		}
	}

	// Token: 0x0600443C RID: 17468 RVA: 0x00160234 File Offset: 0x0015E434
	private void Start()
	{
		this.Initialize();
		SoundManager.s_instance = this;
		SystemSaveManager instance = SystemSaveManager.Instance;
		if (instance != null)
		{
			SystemData systemdata = instance.GetSystemdata();
			if (systemdata != null)
			{
				SoundManager.BgmVolume = (float)systemdata.bgmVolume / 100f;
				SoundManager.SeVolume = (float)systemdata.seVolume / 100f;
			}
		}
	}

	// Token: 0x0600443D RID: 17469 RVA: 0x00160290 File Offset: 0x0015E490
	private void Update()
	{
		base.gameObject.transform.position = new Vector3(-1000f, -1000f, 0f);
	}

	// Token: 0x0600443E RID: 17470 RVA: 0x001602C4 File Offset: 0x0015E4C4
	private void FixedUpdate()
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		for (SoundManager.SourceId sourceId = SoundManager.SourceId.BGM_NORMAL; sourceId < SoundManager.SourceId.SE; sourceId++)
		{
			SoundManager.GetSource(sourceId).FixedUpdate();
		}
	}

	// Token: 0x0600443F RID: 17471 RVA: 0x00160300 File Offset: 0x0015E500
	private void OnDestroy()
	{
		this.m_cueSheetNameList = new List<string>();
	}

	// Token: 0x06004440 RID: 17472 RVA: 0x00160310 File Offset: 0x0015E510
	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			this.SetProxyToCriFs();
		}
	}

	// Token: 0x06004441 RID: 17473 RVA: 0x00160320 File Offset: 0x0015E520
	public static void AddTitleCueSheet()
	{
		SoundManager.AddCueSheet("BGM", "BGM_title.acb", "BGM_title_streamfiles.awb", false);
		SoundManager.AddCueSheet("SE", "se_runners_title.acb", null, false);
	}

	// Token: 0x06004442 RID: 17474 RVA: 0x00160354 File Offset: 0x0015E554
	public static void AddMainMenuCommonCueSheet()
	{
		SoundManager.AddCueSheet("BGM", "BGM_menu.acb", "BGM_menu_streamfiles.awb", true);
		SoundManager.AddCueSheet("BGM_menu_v2", "BGM_menu_v2.acb", "BGM_menu_v2_streamfiles.awb", true);
		SoundManager.AddCueSheet("SE", "se_runners.acb", null, true);
	}

	// Token: 0x06004443 RID: 17475 RVA: 0x00160394 File Offset: 0x0015E594
	public static void AddMainMenuEventCueSheet()
	{
		if (EventManager.Instance != null && EventManager.Instance.IsInEvent())
		{
			SoundManager.AddEventBgmCueSheet(EventManager.Instance.Type);
			SoundManager.AddEventSeCueSheet(EventManager.Instance.Type);
		}
	}

	// Token: 0x06004444 RID: 17476 RVA: 0x001603E0 File Offset: 0x0015E5E0
	public static void AddStageCommonCueSheet()
	{
		SoundManager.AddCueSheet("SE", "se_runners.acb", null, true);
	}

	// Token: 0x06004445 RID: 17477 RVA: 0x001603F4 File Offset: 0x0015E5F4
	public static void AddStageCueSheet(string stageCueSheetName)
	{
		if (stageCueSheetName != null && stageCueSheetName != string.Empty)
		{
			SoundManager.AddCueSheet("BGM", stageCueSheetName + ".acb", stageCueSheetName + "_streamfiles.awb", true);
		}
		SoundManager.AddCueSheet("BGM_jingle", "BGM_jingle.acb", "BGM_jingle_streamfiles.awb", true);
		if (EventManager.Instance != null)
		{
			if (EventManager.Instance.IsSpecialStage())
			{
				SoundManager.AddEventSeCueSheet(EventManager.EventType.SPECIAL_STAGE);
			}
			if (EventManager.Instance.IsRaidBossStage())
			{
				SoundManager.AddEventSeCueSheet(EventManager.EventType.RAID_BOSS);
			}
		}
	}

	// Token: 0x06004446 RID: 17478 RVA: 0x00160488 File Offset: 0x0015E688
	private static void AddEventBgmCueSheet(EventManager.EventType type)
	{
		if (EventCommonDataTable.Instance != null)
		{
			string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.MenuBgmFileName);
			if (data != null && data != string.Empty)
			{
				SoundManager.AddCueSheet("BGM_" + EventManager.GetEventTypeName(type), data + ".acb", data + "_streamfiles.awb", true);
			}
		}
	}

	// Token: 0x06004447 RID: 17479 RVA: 0x001604F4 File Offset: 0x0015E6F4
	private static void AddEventSeCueSheet(EventManager.EventType type)
	{
		if (EventCommonDataTable.Instance != null)
		{
			string data = EventCommonDataTable.Instance.GetData(EventCommonDataItem.SeFileName);
			if (data != null && data != string.Empty)
			{
				SoundManager.AddCueSheet("SE_" + EventManager.GetEventTypeName(type), data + ".acb", null, true);
			}
		}
	}

	// Token: 0x06004448 RID: 17480 RVA: 0x00160558 File Offset: 0x0015E758
	private void Initialize()
	{
		CriWareErrorHandler criWareErrorHandler = UnityEngine.Object.FindObjectOfType<CriWareErrorHandler>();
		if (criWareErrorHandler != null)
		{
			criWareErrorHandler.enabled = false;
		}
		CriAtomEx.RegisterAcf(null, Path.Combine(CriWare.streamingAssetsPath, "Android/Sonic_Runners_Sound.acf"));
		for (int i = 0; i < this.m_sources.Length; i++)
		{
			this.m_sources[i] = new SoundManager.Source(base.gameObject.AddComponent<CriAtomSource>());
		}
		this.SetProxyToCriFs();
	}

	// Token: 0x06004449 RID: 17481 RVA: 0x001605CC File Offset: 0x0015E7CC
	private static bool IsPlayingStatus(CriAtomSource.Status status)
	{
		return status == CriAtomSource.Status.Playing || status == CriAtomSource.Status.Prep;
	}

	// Token: 0x0600444A RID: 17482 RVA: 0x001605DC File Offset: 0x0015E7DC
	private static bool IsPlayingStatus(CriAtomExPlayback.Status status)
	{
		return status == CriAtomExPlayback.Status.Playing || status == CriAtomExPlayback.Status.Prep;
	}

	// Token: 0x0600444B RID: 17483 RVA: 0x001605EC File Offset: 0x0015E7EC
	private static List<SoundManager.Playback> FindPlayingPlayback(string cueName, string cueSheetName)
	{
		List<SoundManager.Playback> list = new List<SoundManager.Playback>();
		foreach (SoundManager.Source source in SoundManager.s_instance.m_sources)
		{
			foreach (SoundManager.PlayId key in source.playbacks.Keys)
			{
				SoundManager.Playback playback = source.playbacks[key];
				if (SoundManager.IsPlayingStatus(playback.status) && playback.cueName == cueName && playback.cueSheet == cueSheetName)
				{
					list.Add(playback);
				}
			}
		}
		return list;
	}

	// Token: 0x0600444C RID: 17484 RVA: 0x001606CC File Offset: 0x0015E8CC
	public static string[] GetCueSheetNameList()
	{
		if (SoundManager.s_instance == null)
		{
			return new string[0];
		}
		return SoundManager.s_instance.m_cueSheetNameList.ToArray();
	}

	// Token: 0x0600444D RID: 17485 RVA: 0x00160700 File Offset: 0x0015E900
	public static bool ExistsCueSheet(string cueSheetName)
	{
		return !(SoundManager.s_instance == null) && SoundManager.s_instance.m_cueSheetNameList.Contains(cueSheetName);
	}

	// Token: 0x0600444E RID: 17486 RVA: 0x00160730 File Offset: 0x0015E930
	public static CriAtomEx.CueInfo[] GetCueInfoList(string cueSheetName)
	{
		if (!SoundManager.ExistsCueSheet(cueSheetName))
		{
			return null;
		}
		CriAtomExAcb acb = CriAtom.GetAcb(cueSheetName);
		return (acb == null) ? null : acb.GetCueInfoList();
	}

	// Token: 0x0600444F RID: 17487 RVA: 0x00160764 File Offset: 0x0015E964
	public static SoundManager.Source[] GetSourseList()
	{
		if (SoundManager.s_instance == null)
		{
			return new SoundManager.Source[0];
		}
		return SoundManager.s_instance.m_sources;
	}

	// Token: 0x06004450 RID: 17488 RVA: 0x00160788 File Offset: 0x0015E988
	private static SoundManager.Source GetSource(SoundManager.SourceId sourceId)
	{
		return SoundManager.s_instance.m_sources[(int)sourceId];
	}

	// Token: 0x06004451 RID: 17489 RVA: 0x00160798 File Offset: 0x0015E998
	private static SoundManager.PlayId Play(SoundManager.SourceId sourceId, string cueName, string cueSheetName, bool loopFlag = false)
	{
		SoundManager.Source source = SoundManager.GetSource(sourceId);
		if (sourceId >= SoundManager.SourceId.BGM_NORMAL && sourceId < SoundManager.SourceId.SE)
		{
			source.Stop();
		}
		if (!SoundManager.ExistsCueSheet(cueSheetName))
		{
			global::Debug.LogWarning("CueSheet " + cueSheetName + " not loaded.");
			return SoundManager.PlayId.NONE;
		}
		source.cueSheet = cueSheetName;
		source.cueName = cueName;
		source.loop = loopFlag;
		return source.Play(cueName);
	}

	// Token: 0x06004452 RID: 17490 RVA: 0x00160800 File Offset: 0x0015EA00
	private static void Change(SoundManager.SourceId sourceId, string cueName, string cueSheetName, bool loopFlag = false)
	{
		SoundManager.Source source = SoundManager.GetSource(sourceId);
		if (SoundManager.IsPlayingStatus(source.status) && source.cueName == cueName)
		{
			return;
		}
		SoundManager.Play(sourceId, cueName, cueSheetName, loopFlag);
	}

	// Token: 0x06004453 RID: 17491 RVA: 0x00160840 File Offset: 0x0015EA40
	private static void Stop(SoundManager.SourceId sourceId)
	{
		SoundManager.GetSource(sourceId).Stop();
	}

	// Token: 0x06004454 RID: 17492 RVA: 0x00160850 File Offset: 0x0015EA50
	private static void Stop(SoundManager.SourceId sourceId, SoundManager.PlayId playId)
	{
		SoundManager.GetSource(sourceId).Stop(playId);
	}

	// Token: 0x06004455 RID: 17493 RVA: 0x00160860 File Offset: 0x0015EA60
	private static void Stop(SoundManager.SourceId sourceId, string cueName, string cueSheetName)
	{
		foreach (SoundManager.Playback playback in SoundManager.FindPlayingPlayback(cueName, cueSheetName))
		{
			playback.Stop();
		}
	}

	// Token: 0x06004456 RID: 17494 RVA: 0x001608C8 File Offset: 0x0015EAC8
	private static void Pause(SoundManager.SourceId sourceId, bool sw)
	{
		SoundManager.GetSource(sourceId).Pause(sw);
	}

	// Token: 0x06004457 RID: 17495 RVA: 0x001608D8 File Offset: 0x0015EAD8
	private static void PausePlaying(SoundManager.SourceId sourceId, bool sw)
	{
		SoundManager.Source source = SoundManager.GetSource(sourceId);
		foreach (SoundManager.PlayId key in source.playbacks.Keys)
		{
			SoundManager.Playback playback = source.playbacks[key];
			playback.Pause(sw);
		}
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x00160958 File Offset: 0x0015EB58
	public static string GetDownloadURL()
	{
		return NetBaseUtil.AssetServerURL + "sound/Android/";
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x0016096C File Offset: 0x0015EB6C
	public static string GetDownloadedDataPath()
	{
		return CriWare.installTargetPath + "/";
	}

	// Token: 0x0600445A RID: 17498 RVA: 0x00160980 File Offset: 0x0015EB80
	public static void AddCueSheet(string cueSheetName, string acbFile, string awbFile, bool isUrlLoad = false)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		if (SoundManager.ExistsCueSheet(cueSheetName))
		{
			return;
		}
		SoundManager.s_instance.m_cueSheetNameList.Add(cueSheetName);
		string str = (!isUrlLoad) ? "Android/" : SoundManager.GetDownloadedDataPath();
		string acbFile2 = (acbFile == null) ? null : (str + acbFile);
		string awbFile2 = (awbFile == null) ? null : (str + awbFile);
		CriAtomCueSheet criAtomCueSheet = CriAtom.AddCueSheet(cueSheetName, acbFile2, awbFile2, null);
	}

	// Token: 0x0600445B RID: 17499 RVA: 0x00160A04 File Offset: 0x0015EC04
	public static void RemoveCueSheet(string cueSheetName)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.s_instance.m_cueSheetNameList.Remove(cueSheetName);
		CriAtom.RemoveCueSheet(cueSheetName);
	}

	// Token: 0x0600445C RID: 17500 RVA: 0x00160A3C File Offset: 0x0015EC3C
	public static void BgmPlay(string cueName, string cueSheetName = "BGM", bool loop = false)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new string[]
		{
			"BgmPlay(",
			cueName,
			", ",
			cueSheetName,
			")"
		}));
		SoundManager.Play(SoundManager.SourceId.BGM_NORMAL, cueName, cueSheetName, loop);
	}

	// Token: 0x0600445D RID: 17501 RVA: 0x00160A94 File Offset: 0x0015EC94
	public static void BgmChange(string cueName, string cueSheetName = "BGM")
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new string[]
		{
			"BgmChange(",
			cueName,
			", ",
			cueSheetName,
			")"
		}));
		SoundManager.Change(SoundManager.SourceId.BGM_NORMAL, cueName, cueSheetName, true);
	}

	// Token: 0x0600445E RID: 17502 RVA: 0x00160AEC File Offset: 0x0015ECEC
	public static void BgmStop()
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("BgmStop()");
		SoundManager.Stop(SoundManager.SourceId.BGM_NORMAL);
		SoundManager.Stop(SoundManager.SourceId.BGM_CROSSFADE);
	}

	// Token: 0x0600445F RID: 17503 RVA: 0x00160B18 File Offset: 0x0015ED18
	public static void BgmPause(bool sw)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("BgmPause(" + sw + ")");
		SoundManager.Pause(SoundManager.SourceId.BGM_NORMAL, sw);
		SoundManager.Pause(SoundManager.SourceId.BGM_CROSSFADE, sw);
	}

	// Token: 0x06004460 RID: 17504 RVA: 0x00160B54 File Offset: 0x0015ED54
	public static void BgmFadeOut(float fadeOutTime)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("BgmFadeOut(" + fadeOutTime + ")");
		SoundManager.GetSource(SoundManager.SourceId.BGM_NORMAL).FadeOutStart(fadeOutTime);
		SoundManager.GetSource(SoundManager.SourceId.BGM_CROSSFADE).FadeOutStart(fadeOutTime);
	}

	// Token: 0x06004461 RID: 17505 RVA: 0x00160BA4 File Offset: 0x0015EDA4
	public static void BgmCrossFadePlay(string cueName, string cueSheetName = "BGM", float fadeOutTime = 0f)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new object[]
		{
			"BgmCrossFadePlay(",
			cueName,
			", ",
			cueSheetName,
			", ",
			fadeOutTime,
			")"
		}));
		SoundManager.GetSource(SoundManager.SourceId.BGM_NORMAL).FadeOffStart(fadeOutTime);
		SoundManager.Play(SoundManager.SourceId.BGM_CROSSFADE, cueName, cueSheetName, true);
	}

	// Token: 0x06004462 RID: 17506 RVA: 0x00160C1C File Offset: 0x0015EE1C
	public static void ItemBgmCrossFadePlay(string cueName, string cueSheetName = "BGM", float fadeOutTime = 0f)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new object[]
		{
			"BgmCrossFadePlay(",
			cueName,
			", ",
			cueSheetName,
			", ",
			fadeOutTime,
			")"
		}));
		SoundManager.Source source = SoundManager.GetSource(SoundManager.SourceId.BGM_CROSSFADE);
		if (SoundManager.IsPlayingStatus(source.status) && source.cueName == cueName)
		{
			return;
		}
		SoundManager.GetSource(SoundManager.SourceId.BGM_NORMAL).FadeOffStart(fadeOutTime);
		SoundManager.Play(SoundManager.SourceId.BGM_CROSSFADE, cueName, cueSheetName, true);
	}

	// Token: 0x06004463 RID: 17507 RVA: 0x00160CBC File Offset: 0x0015EEBC
	public static void BgmCrossFadeStop(float fadeOutTime = 0f, float fadeInTime = 0f)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new object[]
		{
			"BgmCrossFadeStop(",
			fadeOutTime,
			", ",
			fadeInTime,
			")"
		}));
		SoundManager.GetSource(SoundManager.SourceId.BGM_CROSSFADE).FadeOutStart(fadeOutTime);
		SoundManager.GetSource(SoundManager.SourceId.BGM_NORMAL).FadeInStart(fadeInTime, -1f);
	}

	// Token: 0x06004464 RID: 17508 RVA: 0x00160D34 File Offset: 0x0015EF34
	public static SoundManager.PlayId SePlay(string cueName, string cueSheetName = "SE")
	{
		if (SoundManager.s_instance == null)
		{
			return SoundManager.PlayId.NONE;
		}
		SoundManager.OutputPlayLog(string.Concat(new string[]
		{
			"SePlay(",
			cueName,
			", ",
			cueSheetName,
			")"
		}));
		return SoundManager.Play(SoundManager.SourceId.SE, cueName, cueSheetName, false);
	}

	// Token: 0x06004465 RID: 17509 RVA: 0x00160D8C File Offset: 0x0015EF8C
	public static void SeStop(string cueName, string cueSheetName = "SE")
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog(string.Concat(new string[]
		{
			"SeStop(",
			cueName,
			", ",
			cueSheetName,
			")"
		}));
		SoundManager.Stop(SoundManager.SourceId.SE, cueName, cueSheetName);
	}

	// Token: 0x06004466 RID: 17510 RVA: 0x00160DE4 File Offset: 0x0015EFE4
	public static void SeStop(SoundManager.PlayId playId)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("SeStop(" + playId + ")");
		SoundManager.Stop(SoundManager.SourceId.SE, playId);
	}

	// Token: 0x06004467 RID: 17511 RVA: 0x00160E24 File Offset: 0x0015F024
	public static void SePause(bool sw)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("SePause(" + sw + ")");
		SoundManager.Pause(SoundManager.SourceId.SE, sw);
	}

	// Token: 0x06004468 RID: 17512 RVA: 0x00160E64 File Offset: 0x0015F064
	public static void SePausePlaying(bool sw)
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.OutputPlayLog("SePausePlaying(" + sw + ")");
		SoundManager.PausePlaying(SoundManager.SourceId.SE, sw);
	}

	// Token: 0x06004469 RID: 17513 RVA: 0x00160EA4 File Offset: 0x0015F0A4
	private void SetProxyToCriFs()
	{
		string text;
		ushort num;
		SoundManager.GetSystemProxy(out text, out num);
		if (text != this.proxyHost || num != this.proxyPort)
		{
			CriFsUtility.SetProxyServer(text, num);
			global::Debug.Log(string.Concat(new object[]
			{
				"SetProxyToCriFs: ",
				text,
				":",
				num
			}));
			this.proxyHost = text;
			this.proxyPort = num;
		}
	}

	// Token: 0x0600446A RID: 17514 RVA: 0x00160F1C File Offset: 0x0015F11C
	public static void SetProxyForDownloadData()
	{
		if (SoundManager.s_instance == null)
		{
			return;
		}
		SoundManager.s_instance.SetProxyToCriFs();
	}

	// Token: 0x0600446B RID: 17515 RVA: 0x00160F3C File Offset: 0x0015F13C
	private static void GetSystemProxy(out string host, out ushort port)
	{
		Binding.Instance.GetSystemProxy(out host, out port);
	}

	// Token: 0x0600446C RID: 17516 RVA: 0x00160F4C File Offset: 0x0015F14C
	private static void OutputPlayLog(string s)
	{
		if (SoundManager.s_instance != null && SoundManager.s_instance.m_isOutputPlayLog)
		{
			global::Debug.Log("SoundManager PlayLog: " + s);
		}
	}

	// Token: 0x0600446D RID: 17517 RVA: 0x00160F80 File Offset: 0x0015F180
	[Conditional("DEBUG_INFO")]
	private static void DebugLog(string s)
	{
		global::Debug.Log("@ms " + s);
	}

	// Token: 0x0600446E RID: 17518 RVA: 0x00160F94 File Offset: 0x0015F194
	[Conditional("DEBUG_INFO")]
	private static void DebugLogWarning(string s)
	{
		global::Debug.LogWarning("@ms " + s);
	}

	// Token: 0x04003982 RID: 14722
	private const string PLATFORM_DATA_PATH = "Android/";

	// Token: 0x04003983 RID: 14723
	private const string ACF_FILE_PATH = "Android/Sonic_Runners_Sound.acf";

	// Token: 0x04003984 RID: 14724
	[SerializeField]
	private bool m_isOutputPlayLog;

	// Token: 0x04003985 RID: 14725
	private static SoundManager s_instance;

	// Token: 0x04003986 RID: 14726
	private List<string> m_cueSheetNameList = new List<string>();

	// Token: 0x04003987 RID: 14727
	private SoundManager.Source[] m_sources = new SoundManager.Source[3];

	// Token: 0x04003988 RID: 14728
	private float m_bgmVolume = 1f;

	// Token: 0x04003989 RID: 14729
	private float m_seVolume = 1f;

	// Token: 0x0400398A RID: 14730
	private string proxyHost;

	// Token: 0x0400398B RID: 14731
	private ushort proxyPort;

	// Token: 0x02000A15 RID: 2581
	public enum SourceId
	{
		// Token: 0x0400398D RID: 14733
		NONE = -1,
		// Token: 0x0400398E RID: 14734
		BGM_NORMAL,
		// Token: 0x0400398F RID: 14735
		BGM_BEGIN = 0,
		// Token: 0x04003990 RID: 14736
		BGM_CROSSFADE,
		// Token: 0x04003991 RID: 14737
		SE,
		// Token: 0x04003992 RID: 14738
		BGM_END = 2,
		// Token: 0x04003993 RID: 14739
		COUNT
	}

	// Token: 0x02000A16 RID: 2582
	public enum PlayId
	{
		// Token: 0x04003995 RID: 14741
		NONE
	}

	// Token: 0x02000A17 RID: 2583
	public class Playback
	{
		// Token: 0x0600446F RID: 17519 RVA: 0x00160FA8 File Offset: 0x0015F1A8
		public Playback(CriAtomExPlayback playback, string cueName, string cueSheet)
		{
			this.m_atomExPlayback = playback;
			this.cueName = cueName;
			this.cueSheet = cueSheet;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x00160FC8 File Offset: 0x0015F1C8
		// (set) Token: 0x06004471 RID: 17521 RVA: 0x00160FD0 File Offset: 0x0015F1D0
		public string cueName { get; private set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x00160FDC File Offset: 0x0015F1DC
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x00160FE4 File Offset: 0x0015F1E4
		public string cueSheet { get; private set; }

		// Token: 0x06004474 RID: 17524 RVA: 0x00160FF0 File Offset: 0x0015F1F0
		public void Stop()
		{
			this.m_atomExPlayback.Stop();
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x00161000 File Offset: 0x0015F200
		public void Pause(bool sw)
		{
			this.m_atomExPlayback.Pause(sw);
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x00161010 File Offset: 0x0015F210
		public CriAtomExPlayback.Status status
		{
			get
			{
				return this.m_atomExPlayback.status;
			}
		}

		// Token: 0x04003996 RID: 14742
		private CriAtomExPlayback m_atomExPlayback;
	}

	// Token: 0x02000A18 RID: 2584
	public class Source
	{
		// Token: 0x06004477 RID: 17527 RVA: 0x00161020 File Offset: 0x0015F220
		public Source(CriAtomSource atomSource)
		{
			this.m_atomSource = atomSource;
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x0016104C File Offset: 0x0015F24C
		public Dictionary<SoundManager.PlayId, SoundManager.Playback> playbacks
		{
			get
			{
				return this.m_playbacks;
			}
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x00161054 File Offset: 0x0015F254
		private SoundManager.PlayId GeneratePlayId()
		{
			do
			{
				SoundManager.Source.s_playIdBase = (SoundManager.Source.s_playIdBase + 1) % (SoundManager.PlayId)2147483647;
			}
			while (SoundManager.Source.s_playIdBase == SoundManager.PlayId.NONE || this.m_playbacks.ContainsKey(SoundManager.Source.s_playIdBase));
			return SoundManager.Source.s_playIdBase;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x00161098 File Offset: 0x0015F298
		public SoundManager.PlayId playId
		{
			get
			{
				return this.m_playId;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x001610A0 File Offset: 0x0015F2A0
		public CriAtomSource.Status status
		{
			get
			{
				return this.m_atomSource.status;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x001610B0 File Offset: 0x0015F2B0
		// (set) Token: 0x0600447E RID: 17534 RVA: 0x001610C0 File Offset: 0x0015F2C0
		public string cueName
		{
			get
			{
				return this.m_atomSource.cueName;
			}
			set
			{
				this.m_atomSource.cueName = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x001610D0 File Offset: 0x0015F2D0
		// (set) Token: 0x06004480 RID: 17536 RVA: 0x001610E0 File Offset: 0x0015F2E0
		public string cueSheet
		{
			get
			{
				return this.m_atomSource.cueSheet;
			}
			set
			{
				this.m_atomSource.cueSheet = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x001610F0 File Offset: 0x0015F2F0
		// (set) Token: 0x06004482 RID: 17538 RVA: 0x00161100 File Offset: 0x0015F300
		public float volume
		{
			get
			{
				return this.m_atomSource.volume;
			}
			set
			{
				this.m_atomSource.volume = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x00161110 File Offset: 0x0015F310
		// (set) Token: 0x06004484 RID: 17540 RVA: 0x00161120 File Offset: 0x0015F320
		public bool loop
		{
			get
			{
				return this.m_atomSource.loop;
			}
			set
			{
				this.m_atomSource.loop = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x00161130 File Offset: 0x0015F330
		// (set) Token: 0x06004486 RID: 17542 RVA: 0x00161138 File Offset: 0x0015F338
		public float masterVolume
		{
			get
			{
				return this.m_masterVolume;
			}
			set
			{
				this.m_masterVolume = value;
				if (this.m_fadeType == SoundManager.Source.FadeType.NONE)
				{
					this.volume = value;
				}
			}
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x00161154 File Offset: 0x0015F354
		public SoundManager.PlayId Play(string cueName)
		{
			this.m_atomSource.volume = this.m_masterVolume;
			CriAtomExPlayback playback = this.m_atomSource.Play(cueName);
			this.m_playId = this.GeneratePlayId();
			this.m_playbacks.Add(this.m_playId, new SoundManager.Playback(playback, cueName, this.cueSheet));
			this.Removes();
			return this.m_playId;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x001611B8 File Offset: 0x0015F3B8
		public void Stop()
		{
			this.m_atomSource.Stop();
			this.FadeClear();
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x001611CC File Offset: 0x0015F3CC
		public void Pause(bool sw)
		{
			this.m_atomSource.Pause(sw);
			this.m_isPaused = sw;
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x001611E4 File Offset: 0x0015F3E4
		public void Stop(SoundManager.PlayId playId)
		{
			if (this.m_playbacks.ContainsKey(playId))
			{
				this.m_playbacks[playId].Stop();
			}
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00161214 File Offset: 0x0015F414
		public void Pause(SoundManager.PlayId playId, bool sw)
		{
			if (this.m_playbacks.ContainsKey(playId))
			{
				this.m_playbacks[playId].Pause(sw);
			}
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x0016123C File Offset: 0x0015F43C
		private void Removes()
		{
			List<SoundManager.PlayId> list = new List<SoundManager.PlayId>();
			foreach (SoundManager.PlayId playId in this.m_playbacks.Keys)
			{
				if (this.m_playbacks[playId].status == CriAtomExPlayback.Status.Removed)
				{
					list.Add(playId);
				}
			}
			foreach (SoundManager.PlayId key in list)
			{
				this.m_playbacks.Remove(key);
			}
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x0016131C File Offset: 0x0015F51C
		public void FadeOutStart(float fadeTime)
		{
			this.FadeStart(SoundManager.Source.FadeType.OUT, fadeTime, -1f);
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x0016132C File Offset: 0x0015F52C
		public void FadeOffStart(float fadeTime)
		{
			this.FadeStart(SoundManager.Source.FadeType.OFF, fadeTime, -1f);
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x0016133C File Offset: 0x0015F53C
		public void FadeInStart(float fadeTime, float startVolume = -1f)
		{
			this.FadeStart(SoundManager.Source.FadeType.IN, fadeTime, startVolume);
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x00161348 File Offset: 0x0015F548
		private void FadeStart(SoundManager.Source.FadeType fadeType, float fadeTime, float startVolume)
		{
			if (SoundManager.IsPlayingStatus(this.status))
			{
				if (fadeTime == 0f)
				{
					this.FadeEnd(fadeType);
				}
				else
				{
					this.m_fadeType = fadeType;
					this.m_fadeTime = fadeTime;
					if (startVolume != -1f)
					{
						this.volume = startVolume;
					}
				}
			}
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x0016139C File Offset: 0x0015F59C
		private void FadeEnd(SoundManager.Source.FadeType fadeType)
		{
			if (fadeType == SoundManager.Source.FadeType.OUT)
			{
				this.Stop();
				this.volume = this.m_masterVolume;
			}
			else
			{
				this.volume = this.GetTargetVolume(fadeType);
			}
			this.FadeClear();
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x001613DC File Offset: 0x0015F5DC
		private void FadeClear()
		{
			this.m_fadeType = SoundManager.Source.FadeType.NONE;
			this.m_fadeTime = 0f;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x001613F0 File Offset: 0x0015F5F0
		private float GetTargetVolume(SoundManager.Source.FadeType fadeType)
		{
			return (fadeType != SoundManager.Source.FadeType.IN) ? 0f : this.m_masterVolume;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x0016140C File Offset: 0x0015F60C
		public void FixedUpdate()
		{
			if (this.m_fadeType != SoundManager.Source.FadeType.NONE && !this.m_isPaused)
			{
				float targetVolume = this.GetTargetVolume(this.m_fadeType);
				if (this.m_fadeTime > Time.fixedDeltaTime)
				{
					this.volume += (targetVolume - this.volume) * Time.fixedDeltaTime / this.m_fadeTime;
					this.m_fadeTime -= Time.fixedDeltaTime;
				}
				else
				{
					this.FadeEnd(this.m_fadeType);
				}
			}
		}

		// Token: 0x04003999 RID: 14745
		private static SoundManager.PlayId s_playIdBase;

		// Token: 0x0400399A RID: 14746
		private float m_masterVolume = 1f;

		// Token: 0x0400399B RID: 14747
		private bool m_isPaused;

		// Token: 0x0400399C RID: 14748
		private SoundManager.Source.FadeType m_fadeType;

		// Token: 0x0400399D RID: 14749
		private float m_fadeTime;

		// Token: 0x0400399E RID: 14750
		private Dictionary<SoundManager.PlayId, SoundManager.Playback> m_playbacks = new Dictionary<SoundManager.PlayId, SoundManager.Playback>();

		// Token: 0x0400399F RID: 14751
		private CriAtomSource m_atomSource;

		// Token: 0x040039A0 RID: 14752
		private SoundManager.PlayId m_playId;

		// Token: 0x02000A19 RID: 2585
		private enum FadeType
		{
			// Token: 0x040039A2 RID: 14754
			NONE,
			// Token: 0x040039A3 RID: 14755
			OUT,
			// Token: 0x040039A4 RID: 14756
			OFF,
			// Token: 0x040039A5 RID: 14757
			IN
		}
	}
}
