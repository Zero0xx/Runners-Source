using System;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x02000033 RID: 51
	public class PlayGamesHelperObject : MonoBehaviour
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00005A94 File Offset: 0x00003C94
		public static void CreateObject()
		{
			if (PlayGamesHelperObject.instance != null)
			{
				return;
			}
			if (Application.isPlaying)
			{
				GameObject gameObject = new GameObject("PlayGames_QueueRunner");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				PlayGamesHelperObject.instance = gameObject.AddComponent<PlayGamesHelperObject>();
			}
			else
			{
				PlayGamesHelperObject.instance = new PlayGamesHelperObject();
				PlayGamesHelperObject.sIsDummy = true;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005AF0 File Offset: 0x00003CF0
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005B00 File Offset: 0x00003D00
		private void OnDisable()
		{
			if (PlayGamesHelperObject.instance == this)
			{
				PlayGamesHelperObject.instance = null;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005B18 File Offset: 0x00003D18
		public static void RunOnGameThread(Action action)
		{
			if (PlayGamesHelperObject.sIsDummy)
			{
				return;
			}
			List<Action> obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				PlayGamesHelperObject.sQueue.Add(action);
				PlayGamesHelperObject.sQueueEmpty = false;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005B78 File Offset: 0x00003D78
		private void Update()
		{
			if (PlayGamesHelperObject.sIsDummy)
			{
				return;
			}
			if (PlayGamesHelperObject.sQueueEmpty)
			{
				return;
			}
			List<Action> list = new List<Action>();
			List<Action> obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				foreach (Action item in PlayGamesHelperObject.sQueue)
				{
					list.Add(item);
				}
				PlayGamesHelperObject.sQueue.Clear();
				PlayGamesHelperObject.sQueueEmpty = true;
			}
			foreach (Action action in list)
			{
				action();
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005C90 File Offset: 0x00003E90
		private void OnApplicationFocus(bool focused)
		{
			Logger.d("PlayGamesHelperObject.OnApplicationFocus " + focused);
			if (PlayGamesHelperObject.sFocusCallback != null)
			{
				PlayGamesHelperObject.sFocusCallback(focused);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005CC8 File Offset: 0x00003EC8
		private void OnApplicationPause(bool paused)
		{
			Logger.d("PlayGamesHelperObject.OnApplicationPause " + paused);
			if (PlayGamesHelperObject.sPauseCallback != null)
			{
				PlayGamesHelperObject.sPauseCallback(paused);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005D00 File Offset: 0x00003F00
		public static void SetFocusCallback(Action<bool> callback)
		{
			PlayGamesHelperObject.sFocusCallback = callback;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005D08 File Offset: 0x00003F08
		public static void SetPauseCallback(Action<bool> callback)
		{
			PlayGamesHelperObject.sPauseCallback = callback;
		}

		// Token: 0x0400008A RID: 138
		private static PlayGamesHelperObject instance = null;

		// Token: 0x0400008B RID: 139
		private static bool sIsDummy = false;

		// Token: 0x0400008C RID: 140
		private static List<Action> sQueue = new List<Action>();

		// Token: 0x0400008D RID: 141
		private static volatile bool sQueueEmpty = true;

		// Token: 0x0400008E RID: 142
		private static Action<bool> sPauseCallback = null;

		// Token: 0x0400008F RID: 143
		private static Action<bool> sFocusCallback = null;
	}
}
