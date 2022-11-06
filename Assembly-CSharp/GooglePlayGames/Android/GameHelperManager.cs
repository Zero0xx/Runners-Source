using System;
using System.Collections.Generic;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000043 RID: 67
	internal class GameHelperManager
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00009A34 File Offset: 0x00007C34
		internal GameHelperManager(AndroidClient client)
		{
			this.mAndroidClient = client;
			Logger.d("Setting up GameHelperManager.");
			Logger.d("GHM creating GameHelper.");
			int num = 7;
			Logger.d("GHM calling GameHelper constructor with flags=" + num);
			this.mGameHelper = new AndroidJavaObject("com.google.example.games.basegameutils.GameHelper", new object[]
			{
				this.mAndroidClient.GetActivity(),
				num
			});
			if (this.mGameHelper == null)
			{
				throw new Exception("Failed to create GameHelper.");
			}
			Logger.d("GHM setting up GameHelper.");
			this.mGameHelper.Call("enableDebugLog", new object[]
			{
				Logger.DebugLogEnabled
			});
			GameHelperManager.GameHelperListener gameHelperListener = new GameHelperManager.GameHelperListener(this, 1000);
			Logger.d("GHM Setting GameHelper options.");
			this.mGameHelper.Call("setMaxAutoSignInAttempts", new object[]
			{
				0
			});
			AndroidJavaClass gmsClass = JavaUtil.GetGmsClass("games.Games$GamesOptions");
			AndroidJavaObject androidJavaObject = gmsClass.CallStatic<AndroidJavaObject>("builder", new object[0]);
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("setSdkVariant", new object[]
			{
				37143
			});
			AndroidJavaObject androidJavaObject3 = androidJavaObject.Call<AndroidJavaObject>("build", new object[0]);
			this.mGameHelper.Call("setGamesApiOptions", new object[]
			{
				androidJavaObject3
			});
			androidJavaObject3.Dispose();
			androidJavaObject2.Dispose();
			androidJavaObject.Dispose();
			Logger.d("GHM calling GameHelper.setup");
			this.mGameHelper.Call("setup", new object[]
			{
				gameHelperListener
			});
			Logger.d("GHM: GameHelper setup done.");
			Logger.d("GHM Setting up lifecycle.");
			PlayGamesHelperObject.SetPauseCallback(delegate(bool paused)
			{
				if (paused)
				{
					this.OnPause();
				}
				else
				{
					this.OnResume();
				}
			});
			Logger.d("GHM calling GameHelper.onStart to try initial auth.");
			this.mGameHelper.Call("onStart", new object[]
			{
				this.mAndroidClient.GetActivity()
			});
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009C28 File Offset: 0x00007E28
		internal GameHelperManager.ConnectionState State
		{
			get
			{
				if (this.mGameHelper.Call<bool>("isSignedIn", new object[0]))
				{
					return GameHelperManager.ConnectionState.Connected;
				}
				if (this.mGameHelper.Call<bool>("isConnecting", new object[0]))
				{
					return GameHelperManager.ConnectionState.Connecting;
				}
				return GameHelperManager.ConnectionState.Disconnected;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009C68 File Offset: 0x00007E68
		internal bool Connecting
		{
			get
			{
				return this.mGameHelper.Call<bool>("isConnecting", new object[0]);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009C80 File Offset: 0x00007E80
		private void OnResume()
		{
			this.mPaused = false;
			Logger.d("GHM got OnResume, relaying to GameHelper");
			this.mGameHelper.Call("onStart", new object[]
			{
				this.mAndroidClient.GetActivity()
			});
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009CC4 File Offset: 0x00007EC4
		private void OnPause()
		{
			Logger.d("GHM got OnPause, relaying to GameHelper");
			this.mPaused = true;
			foreach (GameHelperManager.OnStopDelegate onStopDelegate in this.mOnStopDelegates)
			{
				onStopDelegate();
			}
			this.mGameHelper.Call("onStop", new object[0]);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009D50 File Offset: 0x00007F50
		private void OnSignInFailed(int origin)
		{
			Logger.d("GHM got onSignInFailed, origin " + origin + ", notifying AndroidClient.");
			if (origin == 1001)
			{
				Logger.d("GHM got onSignInFailed from Sign In Helper. Showing error message.");
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.example.games.pluginsupport.SignInHelperManager"))
				{
					androidJavaClass.CallStatic("showErrorDialog", new object[]
					{
						this.mAndroidClient.GetActivity()
					});
				}
				Logger.d("Error message shown.");
			}
			this.mAndroidClient.OnSignInFailed();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009DFC File Offset: 0x00007FFC
		private void OnSignInSucceeded(int origin)
		{
			Logger.d("GHM got onSignInSucceeded, origin " + origin + ", notifying AndroidClient.");
			if (origin == 1000)
			{
				this.mAndroidClient.OnSignInSucceeded();
			}
			else if (origin == 1001)
			{
				Logger.d("GHM got helper's OnSignInSucceeded.");
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009E54 File Offset: 0x00008054
		internal void BeginUserInitiatedSignIn()
		{
			Logger.d("GHM Starting user-initiated sign in.");
			Logger.d("Forcing GameHelper's connect-on-start flag to true.");
			this.mGameHelper.Call("setConnectOnStart", new object[]
			{
				true
			});
			Logger.d("GHM launching sign-in Activity via SignInHelperManager.launchSignIn");
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.example.games.pluginsupport.SignInHelperManager");
			androidJavaClass.CallStatic("launchSignIn", new object[]
			{
				this.mAndroidClient.GetActivity(),
				new GameHelperManager.GameHelperListener(this, 1001),
				Logger.DebugLogEnabled
			});
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009EE4 File Offset: 0x000080E4
		public AndroidJavaObject GetApiClient()
		{
			return this.mGameHelper.Call<AndroidJavaObject>("getApiClient", new object[0]);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009EFC File Offset: 0x000080FC
		public AndroidJavaObject GetInvitation()
		{
			bool flag = this.mGameHelper.Call<bool>("hasInvitation", new object[0]);
			if (flag)
			{
				return this.mGameHelper.Call<AndroidJavaObject>("getInvitation", new object[0]);
			}
			return null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009F40 File Offset: 0x00008140
		public AndroidJavaObject GetTurnBasedMatch()
		{
			bool flag = this.mGameHelper.Call<bool>("hasTurnBasedMatch", new object[0]);
			if (flag)
			{
				return this.mGameHelper.Call<AndroidJavaObject>("getTurnBasedMatch", new object[0]);
			}
			return null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009F84 File Offset: 0x00008184
		public void ClearInvitationAndTurnBasedMatch()
		{
			Logger.d("GHM clearing invitation and turn-based match on GameHelper.");
			this.mGameHelper.Call("clearInvitation", new object[0]);
			this.mGameHelper.Call("clearTurnBasedMatch", new object[0]);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009FC8 File Offset: 0x000081C8
		public bool IsConnected()
		{
			return this.mGameHelper.Call<bool>("isSignedIn", new object[0]);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009FE0 File Offset: 0x000081E0
		public bool Paused
		{
			get
			{
				return this.mPaused;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009FE8 File Offset: 0x000081E8
		public void SignOut()
		{
			Logger.d("GHM SignOut");
			this.mGameHelper.Call("signOut", new object[0]);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A018 File Offset: 0x00008218
		public void AddOnStopDelegate(GameHelperManager.OnStopDelegate del)
		{
			this.mOnStopDelegates.Add(del);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A028 File Offset: 0x00008228
		private object[] makeGmsCallArgs(object[] args)
		{
			object[] array = new object[args.Length + 1];
			array[0] = this.GetApiClient();
			for (int i = 1; i < array.Length; i++)
			{
				array[i] = args[i - 1];
			}
			return array;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A068 File Offset: 0x00008268
		public ReturnType CallGmsApi<ReturnType>(string className, string fieldName, string methodName, params object[] args)
		{
			object[] args2 = this.makeGmsCallArgs(args);
			if (fieldName != null)
			{
				return JavaUtil.GetGmsField(className, fieldName).Call<ReturnType>(methodName, args2);
			}
			return JavaUtil.GetGmsClass(className).CallStatic<ReturnType>(methodName, args2);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public void CallGmsApi(string className, string fieldName, string methodName, params object[] args)
		{
			object[] args2 = this.makeGmsCallArgs(args);
			if (fieldName != null)
			{
				JavaUtil.GetGmsField(className, fieldName).Call(methodName, args2);
			}
			else
			{
				JavaUtil.GetGmsClass(className).CallStatic(methodName, args2);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A0DC File Offset: 0x000082DC
		public void CallGmsApiWithResult(string className, string fieldName, string methodName, AndroidJavaProxy callbackProxy, params object[] args)
		{
			using (AndroidJavaObject androidJavaObject = this.CallGmsApi<AndroidJavaObject>(className, fieldName, methodName, args))
			{
				androidJavaObject.Call("setResultCallback", new object[]
				{
					callbackProxy
				});
			}
		}

		// Token: 0x040000CA RID: 202
		private const string SignInHelperManagerClass = "com.google.example.games.pluginsupport.SignInHelperManager";

		// Token: 0x040000CB RID: 203
		private const string BaseGameUtilsPkg = "com.google.example.games.basegameutils";

		// Token: 0x040000CC RID: 204
		private const string GameHelperClass = "com.google.example.games.basegameutils.GameHelper";

		// Token: 0x040000CD RID: 205
		private const string GameHelperListenerClass = "com.google.example.games.basegameutils.GameHelper$GameHelperListener";

		// Token: 0x040000CE RID: 206
		private const int ORIGIN_MAIN_ACTIVITY = 1000;

		// Token: 0x040000CF RID: 207
		private const int ORIGIN_SIGN_IN_HELPER_ACTIVITY = 1001;

		// Token: 0x040000D0 RID: 208
		private AndroidJavaObject mGameHelper;

		// Token: 0x040000D1 RID: 209
		private AndroidClient mAndroidClient;

		// Token: 0x040000D2 RID: 210
		private bool mPaused;

		// Token: 0x040000D3 RID: 211
		private List<GameHelperManager.OnStopDelegate> mOnStopDelegates = new List<GameHelperManager.OnStopDelegate>();

		// Token: 0x02000044 RID: 68
		internal enum ConnectionState
		{
			// Token: 0x040000D5 RID: 213
			Disconnected,
			// Token: 0x040000D6 RID: 214
			Connecting,
			// Token: 0x040000D7 RID: 215
			Connected
		}

		// Token: 0x02000045 RID: 69
		private class GameHelperListener : AndroidJavaProxy
		{
			// Token: 0x06000267 RID: 615 RVA: 0x0000A158 File Offset: 0x00008358
			internal GameHelperListener(GameHelperManager mgr, int origin) : base("com.google.example.games.basegameutils.GameHelper$GameHelperListener")
			{
				this.mContainer = mgr;
				this.mOrigin = origin;
			}

			// Token: 0x06000268 RID: 616 RVA: 0x0000A174 File Offset: 0x00008374
			private void onSignInFailed()
			{
				Logger.d("GHM/GameHelperListener got onSignInFailed, origin " + this.mOrigin + ", notifying GHM.");
				this.mContainer.OnSignInFailed(this.mOrigin);
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000A1B4 File Offset: 0x000083B4
			private void onSignInSucceeded()
			{
				Logger.d("GHM/GameHelperListener got onSignInSucceeded, origin " + this.mOrigin + ", notifying GHM.");
				this.mContainer.OnSignInSucceeded(this.mOrigin);
			}

			// Token: 0x040000D8 RID: 216
			private GameHelperManager mContainer;

			// Token: 0x040000D9 RID: 217
			private int mOrigin;
		}

		// Token: 0x02000A61 RID: 2657
		// (Invoke) Token: 0x060047BE RID: 18366
		public delegate void OnStopDelegate();
	}
}
