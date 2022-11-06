using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using SaveData;
using Text;
using UnityEngine;

// Token: 0x02000A48 RID: 2632
public class GeneralUtil
{
	// Token: 0x0600462B RID: 17963 RVA: 0x0016D4E8 File Offset: 0x0016B6E8
	public static void RemoveCharaTexture(List<CharaType> exclusionCharaList = null)
	{
		global::Debug.Log("GeneralUtil RemoveCharaTexture !");
		List<CharaType> list;
		if (exclusionCharaList != null && exclusionCharaList.Count > 0)
		{
			list = new List<CharaType>(exclusionCharaList);
		}
		else
		{
			list = new List<CharaType>();
		}
		int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
		CharaType charaType = CharaType.UNKNOWN;
		CharaType charaType2 = CharaType.UNKNOWN;
		DeckUtil.GetDeckData(deckCurrentStockIndex, ref charaType, ref charaType2);
		if (charaType != CharaType.UNKNOWN)
		{
			list.Add(charaType);
		}
		if (charaType2 != CharaType.UNKNOWN)
		{
			list.Add(charaType2);
		}
		ServerPlayerState playerState = ServerInterface.PlayerState;
		if (playerState != null)
		{
			List<CharaType> characterTypeList = playerState.GetCharacterTypeList(ServerPlayerState.CHARA_SORT.NONE, false, 0);
			if (characterTypeList != null && characterTypeList.Count > 0)
			{
				foreach (CharaType charaType3 in characterTypeList)
				{
					if (list == null || !list.Contains(charaType3))
					{
						TextureRequestChara request = new TextureRequestChara(charaType3, null);
						TextureAsyncLoadManager.Instance.Remove(request);
						global::Debug.Log("GeneralUtil RemoveCharaTexture type:" + charaType3);
					}
				}
			}
		}
	}

	// Token: 0x0600462C RID: 17964 RVA: 0x0016D614 File Offset: 0x0016B814
	public static int gcd(int m, int n)
	{
		if (m == 0 || n == 0)
		{
			return 0;
		}
		while (m != n)
		{
			if (m > n)
			{
				m -= n;
			}
			else
			{
				n -= m;
			}
		}
		return m;
	}

	// Token: 0x0600462D RID: 17965 RVA: 0x0016D648 File Offset: 0x0016B848
	public static int lcm(int m, int n)
	{
		if (m == 0 || n == 0)
		{
			return 0;
		}
		return m / GeneralUtil.gcd(m, n) * n;
	}

	// Token: 0x0600462E RID: 17966 RVA: 0x0016D664 File Offset: 0x0016B864
	public static string GetDateStringHour(DateTime targetTime, int addTimeHour = 0)
	{
		return GeneralUtil.GetDateString(targetTime, addTimeHour * 60 * 60 * 1000);
	}

	// Token: 0x0600462F RID: 17967 RVA: 0x0016D67C File Offset: 0x0016B87C
	public static string GetDateStringMinute(DateTime targetTime, int addTimeMinute = 0)
	{
		return GeneralUtil.GetDateString(targetTime, addTimeMinute * 60 * 1000);
	}

	// Token: 0x06004630 RID: 17968 RVA: 0x0016D690 File Offset: 0x0016B890
	public static string GetDateStringSecond(DateTime targetTime, int addTimeSecond = 0)
	{
		return GeneralUtil.GetDateString(targetTime, addTimeSecond * 1000);
	}

	// Token: 0x06004631 RID: 17969 RVA: 0x0016D6A0 File Offset: 0x0016B8A0
	public static string GetDateString(DateTime targetTime, int addTimeMillisecond = 0)
	{
		string format = "{0:D2}/{1:D2}";
		DateTime dateTime = targetTime.AddMilliseconds((double)addTimeMillisecond);
		return string.Format(format, dateTime.Month, dateTime.Day);
	}

	// Token: 0x06004632 RID: 17970 RVA: 0x0016D6E4 File Offset: 0x0016B8E4
	public static string GetTimeLimitString(DateTime targetTime, bool slightlyChangeColor = false)
	{
		DateTime currentTime = NetBase.GetCurrentTime();
		TimeSpan timeSpan = targetTime - currentTime;
		string result;
		if (timeSpan.Ticks > 0L)
		{
			if (timeSpan.TotalHours >= 100.0)
			{
				result = "99:59:59";
			}
			else if (timeSpan.TotalSeconds > 60.0 || !slightlyChangeColor)
			{
				result = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else
			{
				result = string.Format("[ff0000]{0:D2}:{1:D2}:{2:D2}[-]", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
		}
		else if (slightlyChangeColor)
		{
			result = "[ff0000]00:00:00[-]";
		}
		else
		{
			result = "00:00:00";
		}
		return result;
	}

	// Token: 0x06004633 RID: 17971 RVA: 0x0016D7D0 File Offset: 0x0016B9D0
	public static string GetTimeLimitString(TimeSpan limitTimeSpan, bool slightlyChangeColor = false)
	{
		TimeSpan timeSpan = limitTimeSpan;
		string result;
		if (timeSpan.Ticks > 0L)
		{
			if (timeSpan.TotalHours >= 100.0)
			{
				result = "99:59:59";
			}
			else if (timeSpan.TotalSeconds > 60.0 || !slightlyChangeColor)
			{
				result = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else
			{
				result = string.Format("[ff0000]{0:D2}:{1:D2}:{2:D2}[-]", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
		}
		else if (slightlyChangeColor)
		{
			result = "[ff0000]00:00:00[-]";
		}
		else
		{
			result = "00:00:00";
		}
		return result;
	}

	// Token: 0x06004634 RID: 17972 RVA: 0x0016D8B0 File Offset: 0x0016BAB0
	public static bool SetButtonFunc(GameObject parent, string buttonObjectName, GameObject target, string functionName)
	{
		bool result = false;
		if (parent == null)
		{
			parent = GameObject.Find("UI Root (2D)");
		}
		if (parent != null)
		{
			UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(parent, buttonObjectName);
			if (uibuttonMessage == null)
			{
				GameObject gameObject = GameObjectUtil.FindChildGameObject(parent, buttonObjectName);
				if (gameObject != null)
				{
					uibuttonMessage = gameObject.AddComponent<UIButtonMessage>();
				}
			}
			if (uibuttonMessage != null)
			{
				uibuttonMessage.target = target;
				uibuttonMessage.functionName = functionName;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06004635 RID: 17973 RVA: 0x0016D930 File Offset: 0x0016BB30
	public static bool SetButtonAnimFinished(GameObject parent, string buttonObjectName, EventDelegate.Callback func)
	{
		bool result = false;
		if (parent == null)
		{
			parent = GameObject.Find("UI Root (2D)");
		}
		if (parent != null)
		{
			List<UIPlayAnimation> list = GameObjectUtil.FindChildGameObjectsComponents<UIPlayAnimation>(parent, buttonObjectName);
			foreach (UIPlayAnimation uiplayAnimation in list)
			{
				if (uiplayAnimation != null && uiplayAnimation.onFinished.Count <= 0)
				{
					EventDelegate.Add(uiplayAnimation.onFinished, func, true);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06004636 RID: 17974 RVA: 0x0016D9E8 File Offset: 0x0016BBE8
	public static bool IsOverTimeHour(DateTime baseTime, int addTimeHour = 0)
	{
		return GeneralUtil.IsOverTime(baseTime, addTimeHour * 60 * 60 * 1000);
	}

	// Token: 0x06004637 RID: 17975 RVA: 0x0016DA00 File Offset: 0x0016BC00
	public static bool IsOverTimeMinute(DateTime baseTime, int addTimeMinute = 0)
	{
		return GeneralUtil.IsOverTime(baseTime, addTimeMinute * 60 * 1000);
	}

	// Token: 0x06004638 RID: 17976 RVA: 0x0016DA14 File Offset: 0x0016BC14
	public static bool IsOverTimeSecond(DateTime baseTime, int addTimeSecond = 0)
	{
		return GeneralUtil.IsOverTime(baseTime, addTimeSecond * 1000);
	}

	// Token: 0x06004639 RID: 17977 RVA: 0x0016DA24 File Offset: 0x0016BC24
	public static bool IsOverTime(DateTime baseTime, int addTimeMillisecond = 0)
	{
		bool result = false;
		DateTime currentTime = NetBase.GetCurrentTime();
		if ((currentTime - baseTime).TotalMilliseconds > (double)addTimeMillisecond)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x0016DA54 File Offset: 0x0016BC54
	public static bool IsInTime(DateTime startTime, DateTime endTime, int addTimeMillisecond = 0)
	{
		bool result = false;
		DateTime dateTime = NetBase.GetCurrentTime().AddMilliseconds((double)addTimeMillisecond);
		if (dateTime.Ticks >= startTime.Ticks && dateTime.Ticks < endTime.Ticks)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x0016DA9C File Offset: 0x0016BC9C
	public static void RandomList<T>(ref List<T> listData)
	{
		int i = listData.Count;
		while (i > 1)
		{
			i--;
			int index = UnityEngine.Random.Range(0, listData.Count);
			T value = listData[index];
			listData[index] = listData[i];
			listData[i] = value;
		}
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x0016DAF4 File Offset: 0x0016BCF4
	public static void CleanAllCache()
	{
		Caching.CleanCache();
		if (InformationImageManager.Instance != null)
		{
			InformationImageManager.Instance.DeleteImageFiles();
		}
	}

	// Token: 0x0600463D RID: 17981 RVA: 0x0016DB24 File Offset: 0x0016BD24
	public static bool IsNetwork()
	{
		return true;
	}

	// Token: 0x0600463E RID: 17982 RVA: 0x0016DB28 File Offset: 0x0016BD28
	public static void ShowNoCommunication(string windowName = "ShowNoCommunication")
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = windowName,
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_caption"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_FIXATION_TEXT, "NetworkError", "ui_Lbl_cutoff"),
			isPlayErrorSe = true
		});
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x0016DB8C File Offset: 0x0016BD8C
	public static void ShowEventEnd(string windowName = "ShowEventEnd")
	{
		GeneralWindow.Create(new GeneralWindow.CInfo
		{
			name = windowName,
			buttonType = GeneralWindow.ButtonType.Ok,
			caption = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "event_finished_game_result_caption"),
			message = TextUtility.GetText(TextManager.TextType.TEXTTYPE_EVENT_COMMON_TEXT, "Common", "event_finished_guidance"),
			isPlayErrorSe = true
		});
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x0016DBF0 File Offset: 0x0016BDF0
	public static bool CheckChaoTexture(UITexture target, int chaoId)
	{
		bool result = false;
		if (target != null && target.mainTexture != null && chaoId >= 0)
		{
			string name = target.mainTexture.name;
			if (name.IndexOf("default") == -1)
			{
				string[] array = name.Split(new char[]
				{
					'_'
				});
				if (array != null && array.Length > 1 && !string.IsNullOrEmpty(array[array.Length - 1]))
				{
					int num = int.Parse(array[array.Length - 1]);
					if (num == chaoId)
					{
						result = true;
					}
					else if (num % 10000 == chaoId % 10000)
					{
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x0016DCA4 File Offset: 0x0016BEA4
	public static bool CheckChaoTexture(Texture targetTexture, int chaoId)
	{
		bool result = false;
		if (targetTexture != null && chaoId >= 0)
		{
			string name = targetTexture.name;
			if (name.IndexOf("default") == -1)
			{
				string[] array = name.Split(new char[]
				{
					'_'
				});
				if (array != null && array.Length > 1 && !string.IsNullOrEmpty(array[array.Length - 1]))
				{
					int num = int.Parse(array[array.Length - 1]);
					if (num == chaoId)
					{
						result = true;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x0016DD28 File Offset: 0x0016BF28
	public static GameObject SetToggleObject(GameObject btnTargetObject, GameObject parent, List<string> btnFuncName, List<string> toggleObjectName, int currentSelectIndex, bool enabled = true)
	{
		GameObject gameObject = null;
		if (parent != null && toggleObjectName != null && toggleObjectName.Count > 0 && btnFuncName != null && btnFuncName.Count > 0 && btnFuncName.Count == toggleObjectName.Count)
		{
			gameObject = GameObjectUtil.FindChildGameObject(parent, "tab_mask");
			if (gameObject != null)
			{
				gameObject.SetActive(!enabled);
			}
			UIToggle uitoggle = null;
			for (int i = 0; i < toggleObjectName.Count; i++)
			{
				string name = toggleObjectName[i];
				string functionName = btnFuncName[i];
				UIToggle uitoggle2 = GameObjectUtil.FindChildGameObjectComponent<UIToggle>(parent, name);
				UIButtonMessage uibuttonMessage = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(parent, name);
				if (uitoggle2 != null)
				{
					if (uitoggle == null)
					{
						uitoggle = uitoggle2;
					}
					if (i == currentSelectIndex)
					{
						uitoggle2.startsActive = true;
						uitoggle = uitoggle2;
					}
					else
					{
						uitoggle2.startsActive = false;
					}
				}
				if (uibuttonMessage != null)
				{
					uibuttonMessage.target = btnTargetObject;
					uibuttonMessage.functionName = functionName;
					uibuttonMessage.gameObject.SetActive(true);
				}
			}
			if (uitoggle != null)
			{
				uitoggle.gameObject.SendMessage("Start");
				MonoBehaviour component = btnTargetObject.GetComponent<MonoBehaviour>();
				if (component != null)
				{
					component.StartCoroutine(GeneralUtil.InitCoroutine(uitoggle.gameObject));
				}
			}
		}
		return gameObject;
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x0016DE84 File Offset: 0x0016C084
	private static IEnumerator InitCoroutine(GameObject target)
	{
		yield return null;
		target.SetActive(false);
		target.SetActive(true);
		yield break;
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x0016DEA8 File Offset: 0x0016C0A8
	public static void SetGameObjectOutMoveEnabled(GameObject target, bool enabled)
	{
		Vector3 localPosition = target.transform.localPosition;
		float num = localPosition.x;
		if (enabled)
		{
			if (num > 225000f)
			{
				num -= 300000f;
			}
		}
		else if (num < 225000f)
		{
			num += 300000f;
		}
		target.gameObject.transform.localPosition = new Vector3(num, localPosition.y, localPosition.z);
	}

	// Token: 0x06004645 RID: 17989 RVA: 0x0016DF20 File Offset: 0x0016C120
	public static bool IsGameObjectOutMoveEnabled(GameObject target)
	{
		bool result = true;
		float x = target.transform.localPosition.x;
		if (x > 225000f)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06004646 RID: 17990 RVA: 0x0016DF54 File Offset: 0x0016C154
	public static bool SetEventBanner(GameObject parentObject = null, string targetObjectName = "event_banner")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					parentObject = gameObject2;
				}
			}
		}
		if (parentObject != null)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject3 != null)
			{
				EventManager.EventType eventType = EventManager.EventType.UNKNOWN;
				if (EventManager.Instance != null)
				{
					eventType = EventManager.Instance.Type;
				}
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject3, "badge_alert");
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject3, "banner_slot");
				GameObject gameObject6 = GameObjectUtil.FindChildGameObject(gameObject3, "CollectAnimalOption");
				if (gameObject5 != null)
				{
					gameObject5.SetActive(true);
				}
				if (gameObject6 != null)
				{
					bool flag = eventType != EventManager.EventType.UNKNOWN && eventType != EventManager.EventType.GACHA && eventType != EventManager.EventType.ADVERT;
					gameObject6.SetActive(flag);
					if (flag)
					{
						UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject6, "Lbl_num_event_obj");
						UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject6, "ui_Lbl_word_object_total_main");
						if (uilabel != null && uilabel2 != null)
						{
							switch (eventType)
							{
							case EventManager.EventType.SPECIAL_STAGE:
							{
								SpecialStageInfo specialStageInfo = EventManager.Instance.SpecialStageInfo;
								if (specialStageInfo != null)
								{
									long num = specialStageInfo.totalPoint;
									if (num >= 10000000L)
									{
										num = 9999999L;
										uilabel.text = HudUtility.GetFormatNumString<long>(num) + "+";
									}
									else
									{
										uilabel.text = HudUtility.GetFormatNumString<long>(num);
									}
									uilabel2.text = specialStageInfo.rightTitle;
								}
								else
								{
									uilabel.text = "0";
									uilabel2.text = string.Empty;
								}
								break;
							}
							case EventManager.EventType.RAID_BOSS:
							{
								RaidBossInfo raidBossInfo = EventManager.Instance.RaidBossInfo;
								if (raidBossInfo != null)
								{
									long num = raidBossInfo.totalDestroyCount;
									if (num >= 10000000L)
									{
										num = 9999999L;
										uilabel.text = HudUtility.GetFormatNumString<long>(num) + "+";
									}
									else
									{
										uilabel.text = HudUtility.GetFormatNumString<long>(num);
									}
									uilabel2.text = raidBossInfo.rightTitle;
								}
								else
								{
									uilabel.text = "0";
									uilabel2.text = string.Empty;
								}
								break;
							}
							case EventManager.EventType.COLLECT_OBJECT:
							{
								EtcEventInfo etcEventInfo = EventManager.Instance.EtcEventInfo;
								if (etcEventInfo != null)
								{
									long num = etcEventInfo.totalPoint;
									if (num >= 10000000L)
									{
										num = 9999999L;
										uilabel.text = HudUtility.GetFormatNumString<long>(num) + "+";
									}
									else
									{
										uilabel.text = HudUtility.GetFormatNumString<long>(num);
									}
									uilabel2.text = etcEventInfo.rightTitle;
								}
								else
								{
									uilabel.text = "0";
									uilabel2.text = string.Empty;
								}
								break;
							}
							}
						}
					}
				}
				if (gameObject4 != null)
				{
					bool active = false;
					switch (eventType)
					{
					case EventManager.EventType.SPECIAL_STAGE:
					case EventManager.EventType.COLLECT_OBJECT:
					case EventManager.EventType.GACHA:
						active = false;
						break;
					case EventManager.EventType.RAID_BOSS:
					{
						RaidBossInfo raidBossInfo2 = EventManager.Instance.RaidBossInfo;
						if (raidBossInfo2 != null && raidBossInfo2.IsAttention())
						{
							active = true;
						}
						break;
					}
					}
					gameObject4.SetActive(active);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004647 RID: 17991 RVA: 0x0016E2B8 File Offset: 0x0016C4B8
	public static bool SetRouletteBannerBtn(GameObject parentObject, string targetObjectName, GameObject functionTarget, string functionName, RouletteCategory category, bool enabled)
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
			}
		}
		if (parentObject != null)
		{
			GameObject gameObject2 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject2 != null)
			{
				if (enabled)
				{
					gameObject2.SetActive(true);
					UITexture componentInChildren = gameObject2.GetComponentInChildren<UITexture>();
					UIButtonMessage componentInChildren2 = gameObject2.GetComponentInChildren<UIButtonMessage>();
					if (componentInChildren != null)
					{
						RouletteInformationManager.InfoBannerRequest bannerRequest = new RouletteInformationManager.InfoBannerRequest(componentInChildren);
						if (category == RouletteCategory.SPECIAL)
						{
							category = RouletteCategory.PREMIUM;
						}
						RouletteInformationManager.Instance.LoadInfoBaner(bannerRequest, category);
					}
					if (componentInChildren2 != null && functionTarget != null)
					{
						componentInChildren2.functionName = functionName;
						componentInChildren2.target = functionTarget;
					}
				}
				else
				{
					gameObject2.SetActive(false);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004648 RID: 17992 RVA: 0x0016E38C File Offset: 0x0016C58C
	public static bool SetEndlessRankingBtnIcon(GameObject parentObject = null, string targetObjectName = "Btn_1_ranking")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "0_Endless");
					if (gameObject3 != null)
					{
						parentObject = gameObject3;
					}
				}
			}
		}
		GameObject targetObject = null;
		if (parentObject != null)
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject4 != null)
			{
				targetObject = gameObject4;
			}
		}
		return GeneralUtil.SetRankingBtnIcon(targetObject, RankingUtil.RankingMode.ENDLESS);
	}

	// Token: 0x06004649 RID: 17993 RVA: 0x0016E420 File Offset: 0x0016C620
	public static bool SetEndlessRankingTime(GameObject parentObject = null, string targetObjectName = "Btn_1_ranking")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "0_Endless");
					if (gameObject3 != null)
					{
						parentObject = gameObject3;
					}
				}
			}
		}
		GameObject targetObject = null;
		if (parentObject != null)
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject4 != null)
			{
				targetObject = gameObject4;
			}
		}
		return GeneralUtil.SetRankingTime(targetObject, RankingUtil.RankingMode.ENDLESS);
	}

	// Token: 0x0600464A RID: 17994 RVA: 0x0016E4B4 File Offset: 0x0016C6B4
	public static bool SetQuickRankingBtnIcon(GameObject parentObject = null, string targetObjectName = "Btn_1_ranking")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "1_Quick");
					if (gameObject3 != null)
					{
						parentObject = gameObject3;
					}
				}
			}
		}
		GameObject targetObject = null;
		if (parentObject != null)
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject4 != null)
			{
				targetObject = gameObject4;
			}
		}
		return GeneralUtil.SetRankingBtnIcon(targetObject, RankingUtil.RankingMode.QUICK);
	}

	// Token: 0x0600464B RID: 17995 RVA: 0x0016E548 File Offset: 0x0016C748
	public static bool SetQuickRankingTime(GameObject parentObject = null, string targetObjectName = "Btn_1_ranking")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					GameObject gameObject3 = GameObjectUtil.FindChildGameObject(gameObject2, "1_Quick");
					if (gameObject3 != null)
					{
						parentObject = gameObject3;
					}
				}
			}
		}
		GameObject targetObject = null;
		if (parentObject != null)
		{
			GameObject gameObject4 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject4 != null)
			{
				targetObject = gameObject4;
			}
		}
		return GeneralUtil.SetRankingTime(targetObject, RankingUtil.RankingMode.QUICK);
	}

	// Token: 0x0600464C RID: 17996 RVA: 0x0016E5DC File Offset: 0x0016C7DC
	private static bool SetRankingBtnIcon(GameObject targetObject, RankingUtil.RankingMode mode)
	{
		bool result = false;
		TimeSpan span = default(TimeSpan);
		if (targetObject != null)
		{
			UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(targetObject, "img_icon_league");
			UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(targetObject, "img_icon_league_sub");
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(targetObject, "Lbl_league_rank");
			UILabel uilabel2 = GameObjectUtil.FindChildGameObjectComponent<UILabel>(targetObject, "Lbl_limit");
			if (SingletonGameObject<RankingManager>.Instance != null && ServerInterface.PlayerState != null)
			{
				int leagueType = ServerInterface.PlayerState.m_leagueIndex;
				RankingUtil.RankingScoreType scoreType = RankingManager.EndlessRivalRankingScoreType;
				RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
				if (mode == RankingUtil.RankingMode.QUICK)
				{
					scoreType = RankingManager.QuickRivalRankingScoreType;
					leagueType = ServerInterface.PlayerState.m_leagueIndexQuick;
				}
				if (uisprite != null && uisprite2 != null)
				{
					uisprite.spriteName = RankingUtil.GetLeagueIconName((LeagueType)leagueType);
					uisprite2.spriteName = RankingUtil.GetLeagueIconName2((LeagueType)leagueType);
				}
				List<RankingUtil.Ranker> cacheRankingList = SingletonGameObject<RankingManager>.Instance.GetCacheRankingList(mode, scoreType, rankerType);
				bool flag = false;
				if (cacheRankingList != null && cacheRankingList.Count > 0)
				{
					RankingUtil.Ranker ranker = cacheRankingList[0];
					result = true;
					if (ranker != null)
					{
						flag = true;
						if (uilabel != null)
						{
							uilabel.text = string.Empty + (ranker.rankIndex + 1);
						}
						span = SingletonGameObject<RankingManager>.Instance.GetRankigResetTimeSpan(mode, scoreType, rankerType);
						if (uilabel2 != null)
						{
							uilabel2.text = RankingUtil.GetResetTime(span, false);
						}
					}
					global::Debug.Log(string.Concat(new object[]
					{
						"SetRankingBtnIcon mode:",
						mode,
						"  rankingList:",
						cacheRankingList.Count
					}));
				}
				if (!flag && uilabel != null)
				{
					uilabel.text = string.Empty;
				}
			}
		}
		return result;
	}

	// Token: 0x0600464D RID: 17997 RVA: 0x0016E79C File Offset: 0x0016C99C
	private static bool SetRankingTime(GameObject targetObject, RankingUtil.RankingMode mode)
	{
		bool result = false;
		TimeSpan span = default(TimeSpan);
		if (targetObject != null)
		{
			UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(targetObject, "Lbl_limit");
			if (SingletonGameObject<RankingManager>.Instance != null && ServerInterface.PlayerState != null)
			{
				RankingUtil.RankingScoreType scoreType = RankingManager.EndlessRivalRankingScoreType;
				RankingUtil.RankingRankerType rankerType = RankingUtil.RankingRankerType.RIVAL;
				if (mode == RankingUtil.RankingMode.QUICK)
				{
					scoreType = RankingManager.QuickRivalRankingScoreType;
				}
				span = SingletonGameObject<RankingManager>.Instance.GetRankigResetTimeSpan(mode, scoreType, rankerType);
				result = true;
				if (uilabel != null)
				{
					uilabel.text = RankingUtil.GetResetTime(span, false);
				}
			}
		}
		return result;
	}

	// Token: 0x0600464E RID: 17998 RVA: 0x0016E828 File Offset: 0x0016CA28
	public static bool SetDailyBattleBtnIcon(GameObject parentObject = null, string targetObjectName = "Btn_2_battle")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					parentObject = gameObject2;
				}
			}
		}
		bool result = false;
		TimeSpan limitTimeSpan = default(TimeSpan);
		if (parentObject != null)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			UIButtonMessage x = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(parentObject, targetObjectName);
			if (x != null)
			{
			}
			if (gameObject3 != null)
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject3, "duel_mine_set");
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject3, "duel_adversary_set");
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_limit");
				if (SingletonGameObject<DailyBattleManager>.Instance != null)
				{
					bool flag = false;
					if (SingletonGameObject<DailyBattleManager>.Instance.currentWinFlag >= 3)
					{
						flag = true;
					}
					else if (SingletonGameObject<DailyBattleManager>.Instance.currentWinFlag >= 2)
					{
						flag = true;
					}
					ServerDailyBattleDataPair currentDataPair = SingletonGameObject<DailyBattleManager>.Instance.currentDataPair;
					bool flag2 = false;
					if (gameObject4 != null)
					{
						if (currentDataPair != null && currentDataPair.myBattleData != null)
						{
							gameObject4.SetActive(true);
							GameObject gameObject6 = GameObjectUtil.FindChildGameObject(gameObject4, "duel_win_set");
							GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject4, "duel_lose_set");
							UITexture texUser = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject4, "img_icon_friends");
							GameObject imgUser = GameObjectUtil.FindChildGameObject(gameObject4, "img_icon_friends_default");
							if (string.IsNullOrEmpty(currentDataPair.myBattleData.userId))
							{
								flag = false;
							}
							if (gameObject6 != null)
							{
								gameObject6.SetActive(flag);
							}
							if (gameObject7 != null)
							{
								gameObject7.SetActive(!flag);
							}
							if (texUser != null && imgUser != null)
							{
								string userId = currentDataPair.myBattleData.userId;
								if (string.IsNullOrEmpty(userId))
								{
									userId = ServerInterface.SettingState.m_userId;
								}
								imgUser.gameObject.SetActive(true);
								texUser.alpha = 0f;
								texUser.mainTexture = RankingUtil.GetProfilePictureTexture(userId, delegate(Texture2D _faceTexture)
								{
									texUser.gameObject.SetActive(true);
									texUser.mainTexture = _faceTexture;
									texUser.alpha = 1f;
									imgUser.gameObject.SetActive(false);
								});
							}
							flag2 = true;
						}
						else
						{
							gameObject4.SetActive(false);
						}
					}
					if (gameObject5 != null)
					{
						if (currentDataPair != null && currentDataPair.rivalBattleData != null && !string.IsNullOrEmpty(currentDataPair.rivalBattleData.userId))
						{
							gameObject5.SetActive(true);
							GameObject gameObject8 = GameObjectUtil.FindChildGameObject(gameObject5, "duel_win_set");
							GameObject gameObject9 = GameObjectUtil.FindChildGameObject(gameObject5, "duel_lose_set");
							UITexture texUser = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject5, "img_icon_friends");
							GameObject imgUser = GameObjectUtil.FindChildGameObject(gameObject5, "img_icon_friends_default");
							if (gameObject8 != null)
							{
								gameObject8.SetActive(!flag);
							}
							if (gameObject9 != null)
							{
								gameObject9.SetActive(flag);
							}
							if (texUser != null && imgUser != null)
							{
								if (currentDataPair.rivalBattleData.CheckFriend())
								{
									string userId2 = currentDataPair.rivalBattleData.userId;
									imgUser.gameObject.SetActive(true);
									texUser.alpha = 0f;
									texUser.mainTexture = RankingUtil.GetProfilePictureTexture(userId2, delegate(Texture2D _faceTexture)
									{
										texUser.gameObject.SetActive(true);
										texUser.mainTexture = _faceTexture;
										texUser.alpha = 1f;
										imgUser.gameObject.SetActive(false);
									});
								}
								else
								{
									texUser.gameObject.SetActive(false);
									imgUser.gameObject.SetActive(true);
								}
							}
							flag2 = true;
						}
						else
						{
							gameObject5.SetActive(false);
						}
					}
					if (!flag2)
					{
						global::Debug.Log("DailyBattle not start !!!!!!!");
					}
					else
					{
						limitTimeSpan = SingletonGameObject<DailyBattleManager>.Instance.GetLimitTimeSpan();
						result = true;
						if (uilabel != null)
						{
							uilabel.text = GeneralUtil.GetTimeLimitString(limitTimeSpan, true);
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600464F RID: 17999 RVA: 0x0016EC3C File Offset: 0x0016CE3C
	public static bool SetDailyBattleTime(GameObject parentObject = null, string targetObjectName = "Btn_2_battle")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					parentObject = gameObject2;
				}
			}
		}
		bool result = false;
		TimeSpan limitTimeSpan = default(TimeSpan);
		if (parentObject != null)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			UIButtonMessage x = GameObjectUtil.FindChildGameObjectComponent<UIButtonMessage>(parentObject, targetObjectName);
			if (x != null)
			{
			}
			if (gameObject3 != null)
			{
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject3, "Lbl_limit");
				if (SingletonGameObject<DailyBattleManager>.Instance != null)
				{
					limitTimeSpan = SingletonGameObject<DailyBattleManager>.Instance.GetLimitTimeSpan();
					result = true;
					if (uilabel != null)
					{
						uilabel.text = GeneralUtil.GetTimeLimitString(limitTimeSpan, true);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06004650 RID: 18000 RVA: 0x0016ED14 File Offset: 0x0016CF14
	public static bool SetRouletteBtnIcon(GameObject parentObject = null, string targetObjectName = "Btn_roulette")
	{
		if (parentObject == null)
		{
			GameObject gameObject = GameObject.Find("UI Root (2D)");
			if (gameObject != null)
			{
				parentObject = gameObject;
				GameObject gameObject2 = GameObjectUtil.FindChildGameObject(gameObject, "MainMenuUI4");
				if (gameObject2 != null)
				{
					parentObject = gameObject2;
				}
			}
		}
		if (parentObject != null)
		{
			GameObject gameObject3 = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject3 != null)
			{
				GameObject gameObject4 = GameObjectUtil.FindChildGameObject(gameObject3, "badge_spin");
				GameObject gameObject5 = GameObjectUtil.FindChildGameObject(gameObject3, "badge_egg");
				GameObject gameObject6 = GameObjectUtil.FindChildGameObject(gameObject3, "badge_alert");
				GameObject gameObject7 = GameObjectUtil.FindChildGameObject(gameObject3, "event_icon");
				if (gameObject4 != null)
				{
					bool active = false;
					GameObject gameObject8 = gameObject4.transform.FindChild("Lbl_roulette_volume").gameObject;
					if (gameObject8 != null)
					{
						UILabel component = gameObject8.GetComponent<UILabel>();
						if (component != null)
						{
							int num = 0;
							ServerWheelOptions wheelOptions = ServerInterface.WheelOptions;
							if (wheelOptions != null)
							{
								num = wheelOptions.m_numRemaining;
								if (num >= 1)
								{
									active = true;
								}
							}
							component.text = num.ToString();
						}
					}
					gameObject4.SetActive(active);
				}
				if (gameObject5 != null)
				{
					bool active2 = false;
					if (RouletteManager.Instance != null && RouletteManager.Instance.specialEgg >= 10)
					{
						active2 = true;
					}
					gameObject5.SetActive(active2);
				}
				if (gameObject6 != null)
				{
					bool active3 = HudMenuUtility.IsSale(Constants.Campaign.emType.ChaoRouletteCost);
					gameObject6.SetActive(active3);
				}
				if (gameObject7 != null)
				{
					bool active4 = false;
					EventManager instance = EventManager.Instance;
					if (instance != null)
					{
						active4 = instance.IsInEvent();
					}
					gameObject7.SetActive(active4);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004651 RID: 18001 RVA: 0x0016EED0 File Offset: 0x0016D0D0
	public static bool SetCharasetBtnIcon(GameObject parentObject = null, string targetObjectName = "Btn_charaset")
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null)
		{
			PlayerData playerData = instance.PlayerData;
			if (playerData != null)
			{
				CharaType mainChara = playerData.MainChara;
				CharaType subChara = playerData.SubChara;
				int mainChaoID = playerData.MainChaoID;
				int subChaoID = playerData.SubChaoID;
				return GeneralUtil.SetCharasetBtnIcon(mainChara, subChara, mainChaoID, subChaoID, parentObject, targetObjectName);
			}
		}
		return false;
	}

	// Token: 0x06004652 RID: 18002 RVA: 0x0016EF2C File Offset: 0x0016D12C
	public static bool SetCharasetBtnIcon(CharaType mainChara, CharaType subChara, int mainChaoId, int subChaoId, GameObject parentObject = null, string targetObjectName = "Btn_charaset")
	{
		if (parentObject == null)
		{
			parentObject = GameObject.Find("UI Root (2D)");
		}
		if (parentObject != null)
		{
			GameObject gameObject = GameObjectUtil.FindChildGameObject(parentObject, targetObjectName);
			if (gameObject != null)
			{
				UITexture uitexture = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "img_chao_main");
				UITexture uitexture2 = GameObjectUtil.FindChildGameObjectComponent<UITexture>(gameObject, "img_chao_sub");
				UISprite uisprite = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_main");
				UISprite uisprite2 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_player_sub");
				UILabel uilabel = GameObjectUtil.FindChildGameObjectComponent<UILabel>(gameObject, "Lbl_player_main_lv");
				UISprite uisprite3 = GameObjectUtil.FindChildGameObjectComponent<UISprite>(gameObject, "img_decknum");
				UITexture[] componentsInChildren = gameObject.GetComponentsInChildren<UITexture>();
				if (componentsInChildren != null && componentsInChildren.Length > 0)
				{
					foreach (UITexture uitexture3 in componentsInChildren)
					{
						uitexture3.gameObject.SetActive(false);
					}
				}
				if (uitexture != null)
				{
					if (mainChaoId >= 0)
					{
						ChaoTextureManager.CallbackInfo info = new ChaoTextureManager.CallbackInfo(uitexture, null, true);
						ChaoTextureManager.Instance.GetTexture(mainChaoId, info);
						uitexture.gameObject.SetActive(true);
					}
					else
					{
						uitexture.gameObject.SetActive(false);
					}
				}
				if (uitexture2 != null)
				{
					if (subChaoId >= 0)
					{
						ChaoTextureManager.CallbackInfo info2 = new ChaoTextureManager.CallbackInfo(uitexture2, null, true);
						ChaoTextureManager.Instance.GetTexture(subChaoId, info2);
						uitexture2.gameObject.SetActive(true);
					}
					else
					{
						uitexture2.gameObject.SetActive(false);
					}
				}
				if (uisprite != null)
				{
					uisprite.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(mainChara);
					uisprite.gameObject.SetActive(true);
				}
				if (uisprite2 != null)
				{
					uisprite2.spriteName = "ui_tex_player_set_" + CharaTypeUtil.GetCharaSpriteNameSuffix(subChara);
					uisprite2.gameObject.SetActive(true);
				}
				if (uilabel != null)
				{
					string text = TextManager.GetText(TextManager.TextType.TEXTTYPE_COMMON_TEXT, "MainMenu", "ui_LevelNumber").text;
					uilabel.text = text.Replace("{PARAM}", SaveDataUtil.GetCharaLevel(mainChara).ToString());
				}
				int deckCurrentStockIndex = DeckUtil.GetDeckCurrentStockIndex();
				if (uisprite3 != null)
				{
					uisprite3.spriteName = "ui_chao_set_deck_tab_" + (deckCurrentStockIndex + 1);
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004653 RID: 18003 RVA: 0x0016F170 File Offset: 0x0016D370
	public static void SetPresentItemCount(MsgUpdateMesseageSucceed msg)
	{
		if (msg != null && msg.m_presentStateList != null && msg.m_presentStateList.Count > 0)
		{
			List<ServerPresentState> presentStateList = msg.m_presentStateList;
			foreach (ServerPresentState serverPresentState in presentStateList)
			{
				ServerItem serverItem = new ServerItem((ServerItem.Id)serverPresentState.m_itemId);
				if (serverItem.idType == ServerItem.IdType.EGG_ITEM || serverItem.idType == ServerItem.IdType.RAIDRING || serverItem.idType == ServerItem.IdType.ITEM_ROULLETE_TICKET || serverItem.idType == ServerItem.IdType.PREMIUM_ROULLETE_TICKET)
				{
					GeneralUtil.AddItemCount(serverItem.id, (long)serverPresentState.m_numItem);
				}
			}
		}
	}

	// Token: 0x06004654 RID: 18004 RVA: 0x0016F24C File Offset: 0x0016D44C
	private static void AddItemCount(ServerItem.Id itemId, long count)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			instance.ItemData.AddEtcItemCount(itemId, count);
		}
	}

	// Token: 0x06004655 RID: 18005 RVA: 0x0016F284 File Offset: 0x0016D484
	public static void SetItemCount(ServerItem.Id itemId, long count)
	{
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			if (itemId != ServerItem.Id.RING && itemId != ServerItem.Id.RSRING)
			{
				instance.ItemData.SetEtcItemCount(itemId, count);
			}
			else if (itemId == ServerItem.Id.RING)
			{
				instance.ItemData.RingCount = (uint)count;
				instance.ItemData.RingCountOffset = 0;
			}
			else if (itemId == ServerItem.Id.RSRING)
			{
				instance.ItemData.RedRingCount = (uint)count;
				instance.ItemData.RedRingCountOffset = 0;
			}
		}
	}

	// Token: 0x06004656 RID: 18006 RVA: 0x0016F324 File Offset: 0x0016D524
	public static bool SetItemCountOffset(ServerItem.Id itemId, long offset)
	{
		bool result = false;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			if (itemId != ServerItem.Id.RING && itemId != ServerItem.Id.RSRING)
			{
				result = instance.ItemData.SetEtcItemCountOffset(itemId, offset);
			}
			else
			{
				if (itemId == ServerItem.Id.RING)
				{
					instance.ItemData.RingCountOffset = (int)offset;
				}
				else if (itemId == ServerItem.Id.RSRING)
				{
					instance.ItemData.RedRingCountOffset = (int)offset;
				}
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06004657 RID: 18007 RVA: 0x0016F3B4 File Offset: 0x0016D5B4
	public static long GetItemCount(ServerItem.Id itemId)
	{
		long result = 0L;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			if (itemId != ServerItem.Id.RING && itemId != ServerItem.Id.RSRING)
			{
				result = instance.ItemData.GetEtcItemCount(itemId);
			}
			else if (itemId == ServerItem.Id.RING)
			{
				result = (long)instance.ItemData.DisplayRingCount;
			}
			else if (itemId == ServerItem.Id.RSRING)
			{
				result = (long)instance.ItemData.DisplayRedRingCount;
			}
		}
		return result;
	}

	// Token: 0x06004658 RID: 18008 RVA: 0x0016F440 File Offset: 0x0016D640
	public static long GetItemCountOffset(ServerItem.Id itemId)
	{
		long result = 0L;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			if (itemId != ServerItem.Id.RING && itemId != ServerItem.Id.RSRING)
			{
				result = instance.ItemData.GetEtcItemCountOffset(itemId);
			}
			else if (itemId == ServerItem.Id.RING)
			{
				result = (long)instance.ItemData.RingCountOffset;
			}
			else if (itemId == ServerItem.Id.RSRING)
			{
				result = (long)instance.ItemData.RedRingCountOffset;
			}
		}
		return result;
	}

	// Token: 0x06004659 RID: 18009 RVA: 0x0016F4CC File Offset: 0x0016D6CC
	public bool IsItemCount(ServerItem.Id itemId)
	{
		bool result = false;
		SaveDataManager instance = SaveDataManager.Instance;
		if (instance != null && instance.ItemData != null)
		{
			result = (itemId == ServerItem.Id.RING || itemId == ServerItem.Id.RSRING || instance.ItemData.IsEtcItemCount(itemId));
		}
		return result;
	}

	// Token: 0x04003AC6 RID: 15046
	public const float OUT_AREA = 300000f;
}
