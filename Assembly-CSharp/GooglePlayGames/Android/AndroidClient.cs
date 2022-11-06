using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000035 RID: 53
	public class AndroidClient : IPlayGamesClient
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00005FC8 File Offset: 0x000041C8
		public AndroidClient()
		{
			this.mRtmpClient = new AndroidRtmpClient(this);
			this.mTbmpClient = new AndroidTbmpClient(this);
			this.RunOnUiThread(delegate
			{
				Logger.d("Initializing Android Client.");
				Logger.d("Creating GameHelperManager to manage GameHelper.");
				this.mGHManager = new GameHelperManager(this);
				this.mGHManager.AddOnStopDelegate(new GameHelperManager.OnStopDelegate(this.mRtmpClient.OnStop));
			});
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000601C File Offset: 0x0000421C
		public void Authenticate(Action<bool> callback, bool silent)
		{
			if (this.mAuthState != AndroidClient.AuthState.NoAuth)
			{
				Logger.w("Authenticate() called while an authentication process was active. " + this.mAuthState);
				this.mAuthCallback = callback;
				return;
			}
			Logger.d("Making sure PlayGamesHelperObject is ready.");
			PlayGamesHelperObject.CreateObject();
			Logger.d("PlayGamesHelperObject created.");
			this.mSilentAuth = silent;
			Logger.d("AUTH: starting auth process, silent=" + this.mSilentAuth);
			this.RunOnUiThread(delegate
			{
				GameHelperManager.ConnectionState state = this.mGHManager.State;
				if (state != GameHelperManager.ConnectionState.Connecting)
				{
					if (state != GameHelperManager.ConnectionState.Connected)
					{
						this.mAuthCallback = callback;
						if (this.mSilentAuth)
						{
							Logger.d("AUTH: not connected and silent=true, so failing.");
							this.mAuthState = AndroidClient.AuthState.NoAuth;
							this.InvokeAuthCallback(false);
						}
						else
						{
							Logger.d("AUTH: not connected and silent=false, so starting flow.");
							this.mAuthState = AndroidClient.AuthState.InProgress;
							this.mGHManager.BeginUserInitiatedSignIn();
						}
					}
					else
					{
						Logger.d("AUTH: already connected! Proceeding to achievement load phase.");
						this.mAuthCallback = callback;
						this.DoInitialAchievementLoad();
					}
				}
				else
				{
					Logger.d("AUTH: connection in progress; auth now pending.");
					this.mAuthCallback = callback;
					this.mAuthState = AndroidClient.AuthState.AuthPending;
				}
			});
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000060BC File Offset: 0x000042BC
		private void DoInitialAchievementLoad()
		{
			Logger.d("AUTH: Now performing initial achievement load...");
			this.mAuthState = AndroidClient.AuthState.LoadingAchs;
			this.mGHManager.CallGmsApiWithResult("games.Games", "Achievements", "load", new AndroidClient.OnAchievementsLoadedResultProxy(this), new object[]
			{
				false
			});
			Logger.d("AUTH: Initial achievement load call made.");
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006114 File Offset: 0x00004314
		private void OnAchievementsLoaded(int statusCode, AndroidJavaObject buffer)
		{
			if (this.mAuthState == AndroidClient.AuthState.LoadingAchs)
			{
				Logger.d("AUTH: Initial achievement load finished.");
				if (statusCode == 0 || statusCode == 3 || statusCode == 5)
				{
					Logger.d("Processing achievement buffer.");
					this.mAchievementBank.ProcessBuffer(buffer);
					Logger.d("Closing achievement buffer.");
					buffer.Call("close", new object[0]);
					Logger.d("AUTH: Auth process complete!");
					this.mAuthState = AndroidClient.AuthState.Done;
					this.InvokeAuthCallback(true);
					this.CheckForConnectionExtras();
					this.mRtmpClient.OnSignInSucceeded();
					this.mTbmpClient.OnSignInSucceeded();
				}
				else
				{
					Logger.w("AUTH: Failed to load achievements, status code " + statusCode);
					this.mAuthState = AndroidClient.AuthState.NoAuth;
					this.InvokeAuthCallback(false);
				}
			}
			else
			{
				Logger.w("OnAchievementsLoaded called unexpectedly in auth state " + this.mAuthState);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000061F8 File Offset: 0x000043F8
		private void InvokeAuthCallback(bool success)
		{
			if (this.mAuthCallback == null)
			{
				return;
			}
			Logger.d("AUTH: Calling auth callback: success=" + success);
			Action<bool> cb = this.mAuthCallback;
			this.mAuthCallback = null;
			PlayGamesHelperObject.RunOnGameThread(delegate
			{
				cb(success);
			});
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000625C File Offset: 0x0000445C
		private void RetrieveUserInfo()
		{
			Logger.d("Attempting to retrieve player info.");
			using (AndroidJavaObject androidJavaObject = this.mGHManager.CallGmsApi<AndroidJavaObject>("games.Games", "Players", "getCurrentPlayer", new object[0]))
			{
				if (this.mUserId == null)
				{
					this.mUserId = androidJavaObject.Call<string>("getPlayerId", new object[0]);
					Logger.d("Player ID: " + this.mUserId);
				}
				if (this.mUserDisplayName == null)
				{
					this.mUserDisplayName = androidJavaObject.Call<string>("getDisplayName", new object[0]);
					Logger.d("Player display name: " + this.mUserDisplayName);
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006330 File Offset: 0x00004530
		internal void OnSignInSucceeded()
		{
			Logger.d("AndroidClient got OnSignInSucceeded.");
			this.RetrieveUserInfo();
			if (this.mAuthState == AndroidClient.AuthState.AuthPending || this.mAuthState == AndroidClient.AuthState.InProgress)
			{
				Logger.d("AUTH: Auth succeeded. Proceeding to achievement loading.");
				this.DoInitialAchievementLoad();
			}
			else if (this.mAuthState == AndroidClient.AuthState.LoadingAchs)
			{
				Logger.w("AUTH: Got OnSignInSucceeded() while in achievement loading phase (unexpected).");
				Logger.w("AUTH: Trying to fix by issuing a new achievement load call.");
				this.DoInitialAchievementLoad();
			}
			else
			{
				Logger.d("Normal lifecycle OnSignInSucceeded received.");
				this.RunPendingActions();
				this.CheckForConnectionExtras();
				this.mRtmpClient.OnSignInSucceeded();
				this.mTbmpClient.OnSignInSucceeded();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000063D4 File Offset: 0x000045D4
		internal void OnSignInFailed()
		{
			Logger.d("AndroidClient got OnSignInFailed.");
			if (this.mAuthState == AndroidClient.AuthState.AuthPending)
			{
				if (this.mSilentAuth)
				{
					Logger.d("AUTH: Auth flow was pending, but silent=true, so failing.");
					this.mAuthState = AndroidClient.AuthState.NoAuth;
					this.InvokeAuthCallback(false);
				}
				else
				{
					Logger.d("AUTH: Auth flow was pending and silent=false, so doing noisy auth.");
					this.mAuthState = AndroidClient.AuthState.InProgress;
					this.mGHManager.BeginUserInitiatedSignIn();
				}
			}
			else if (this.mAuthState == AndroidClient.AuthState.InProgress)
			{
				Logger.d("AUTH: FAILED!");
				this.mAuthState = AndroidClient.AuthState.NoAuth;
				this.InvokeAuthCallback(false);
			}
			else if (this.mAuthState == AndroidClient.AuthState.LoadingAchs)
			{
				Logger.d("AUTH: FAILED (while loading achievements).");
				this.mAuthState = AndroidClient.AuthState.NoAuth;
				this.InvokeAuthCallback(false);
			}
			else if (this.mAuthState == AndroidClient.AuthState.NoAuth)
			{
				Logger.d("Normal OnSignInFailed received.");
			}
			else if (this.mAuthState == AndroidClient.AuthState.Done)
			{
				Logger.e("Authentication has been lost!");
				this.mAuthState = AndroidClient.AuthState.NoAuth;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000064CC File Offset: 0x000046CC
		private void RunPendingActions()
		{
			if (this.mActionsPendingSignIn.Count > 0)
			{
				Logger.d("Running pending actions on the UI thread.");
				while (this.mActionsPendingSignIn.Count > 0)
				{
					Action action = this.mActionsPendingSignIn[0];
					this.mActionsPendingSignIn.RemoveAt(0);
					action();
				}
				Logger.d("Done running pending actions on the UI thread.");
			}
			else
			{
				Logger.d("No pending actions to run on UI thread.");
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006544 File Offset: 0x00004744
		public bool IsAuthenticated()
		{
			return this.mAuthState == AndroidClient.AuthState.Done && !this.mSignOutInProgress;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006560 File Offset: 0x00004760
		public void SignOut()
		{
			Logger.d("AndroidClient.SignOut");
			this.mSignOutInProgress = true;
			this.RunWhenConnectionStable(delegate
			{
				Logger.d("Calling GHM.SignOut");
				this.mGHManager.SignOut();
				this.mAuthState = AndroidClient.AuthState.NoAuth;
				this.mSignOutInProgress = false;
				Logger.d("Now signed out.");
			});
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006588 File Offset: 0x00004788
		internal AndroidJavaObject GetActivity()
		{
			AndroidJavaObject @static;
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				@static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
			return @static;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000065E0 File Offset: 0x000047E0
		internal void RunOnUiThread(Action action)
		{
			using (AndroidJavaObject activity = this.GetActivity())
			{
				activity.Call("runOnUiThread", new object[]
				{
					new AndroidJavaRunnable(action.Invoke)
				});
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006644 File Offset: 0x00004844
		private void RunWhenConnectionStable(Action a)
		{
			this.RunOnUiThread(delegate
			{
				if (this.mGHManager.Paused || this.mGHManager.Connecting)
				{
					Logger.d("Action scheduled for later (connection currently in progress).");
					this.mActionsPendingSignIn.Add(a);
				}
				else
				{
					a();
				}
			});
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006678 File Offset: 0x00004878
		internal void CallClientApi(string desc, Action call, Action<bool> callback)
		{
			Logger.d("Requesting API call: " + desc);
			this.RunWhenConnectionStable(delegate
			{
				if (this.mGHManager.IsConnected())
				{
					Logger.d("Connected! Calling API: " + desc);
					call();
					if (callback != null)
					{
						PlayGamesHelperObject.RunOnGameThread(delegate
						{
							callback(true);
						});
					}
				}
				else
				{
					Logger.w("Not connected! Failed to call API :" + desc);
					if (callback != null)
					{
						PlayGamesHelperObject.RunOnGameThread(delegate
						{
							callback(false);
						});
					}
				}
			});
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000066D0 File Offset: 0x000048D0
		public string GetUserId()
		{
			return this.mUserId;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000066D8 File Offset: 0x000048D8
		public string GetUserDisplayName()
		{
			return this.mUserDisplayName;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000066E0 File Offset: 0x000048E0
		public void UnlockAchievement(string achId, Action<bool> callback)
		{
			Logger.d("AndroidClient.UnlockAchievement: " + achId);
			Achievement achievement = this.GetAchievement(achId);
			if (achievement != null && achievement.IsUnlocked)
			{
				Logger.d("...was already unlocked, so no-op.");
				if (callback != null)
				{
					callback(true);
				}
				return;
			}
			this.CallClientApi("unlock ach " + achId, delegate
			{
				this.mGHManager.CallGmsApi("games.Games", "Achievements", "unlock", new object[]
				{
					achId
				});
			}, callback);
			achievement = this.GetAchievement(achId);
			if (achievement != null)
			{
				achievement.IsUnlocked = (achievement.IsRevealed = true);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006794 File Offset: 0x00004994
		public void RevealAchievement(string achId, Action<bool> callback)
		{
			Logger.d("AndroidClient.RevealAchievement: " + achId);
			Achievement achievement = this.GetAchievement(achId);
			if (achievement != null && achievement.IsRevealed)
			{
				Logger.d("...was already revealed, so no-op.");
				if (callback != null)
				{
					callback(true);
				}
				return;
			}
			this.CallClientApi("reveal ach " + achId, delegate
			{
				this.mGHManager.CallGmsApi("games.Games", "Achievements", "reveal", new object[]
				{
					achId
				});
			}, callback);
			achievement = this.GetAchievement(achId);
			if (achievement != null)
			{
				achievement.IsRevealed = true;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006840 File Offset: 0x00004A40
		public void IncrementAchievement(string achId, int steps, Action<bool> callback)
		{
			Logger.d(string.Concat(new object[]
			{
				"AndroidClient.IncrementAchievement: ",
				achId,
				", steps ",
				steps
			}));
			this.CallClientApi("increment ach " + achId, delegate
			{
				this.mGHManager.CallGmsApi("games.Games", "Achievements", "increment", new object[]
				{
					achId,
					steps
				});
			}, callback);
			Achievement achievement = this.GetAchievement(achId);
			if (achievement != null)
			{
				achievement.CurrentSteps += steps;
				if (achievement.CurrentSteps >= achievement.TotalSteps)
				{
					achievement.CurrentSteps = achievement.TotalSteps;
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006908 File Offset: 0x00004B08
		public List<Achievement> GetAchievements()
		{
			return this.mAchievementBank.GetAchievements();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006918 File Offset: 0x00004B18
		public Achievement GetAchievement(string achId)
		{
			return this.mAchievementBank.GetAchievement(achId);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006928 File Offset: 0x00004B28
		public void ShowAchievementsUI()
		{
			Logger.d("AndroidClient.ShowAchievementsUI.");
			this.CallClientApi("show achievements ui", delegate
			{
				using (AndroidJavaObject androidJavaObject = this.mGHManager.CallGmsApi<AndroidJavaObject>("games.Games", "Achievements", "getAchievementsIntent", new object[0]))
				{
					using (AndroidJavaObject activity = this.GetActivity())
					{
						Logger.d(string.Concat(new object[]
						{
							"About to show achievements UI with intent ",
							androidJavaObject,
							", activity ",
							activity
						}));
						if (androidJavaObject != null && activity != null)
						{
							activity.Call("startActivityForResult", new object[]
							{
								androidJavaObject,
								9999
							});
						}
					}
				}
			}, null);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006958 File Offset: 0x00004B58
		private AndroidJavaObject GetLeaderboardIntent(string lbId)
		{
			return (lbId != null) ? this.mGHManager.CallGmsApi<AndroidJavaObject>("games.Games", "Leaderboards", "getLeaderboardIntent", new object[]
			{
				lbId
			}) : this.mGHManager.CallGmsApi<AndroidJavaObject>("games.Games", "Leaderboards", "getAllLeaderboardsIntent", new object[0]);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000069B4 File Offset: 0x00004BB4
		public void ShowLeaderboardUI(string lbId)
		{
			Logger.d("AndroidClient.ShowLeaderboardUI, lb=" + ((lbId != null) ? lbId : "(all)"));
			this.CallClientApi("show LB ui", delegate
			{
				using (AndroidJavaObject leaderboardIntent = this.GetLeaderboardIntent(lbId))
				{
					using (AndroidJavaObject activity = this.GetActivity())
					{
						Logger.d(string.Concat(new object[]
						{
							"About to show LB UI with intent ",
							leaderboardIntent,
							", activity ",
							activity
						}));
						if (leaderboardIntent != null && activity != null)
						{
							activity.Call("startActivityForResult", new object[]
							{
								leaderboardIntent,
								9999
							});
						}
					}
				}
			}, null);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006A18 File Offset: 0x00004C18
		public void SubmitScore(string lbId, long score, Action<bool> callback)
		{
			Logger.d(string.Concat(new object[]
			{
				"AndroidClient.SubmitScore, lb=",
				lbId,
				", score=",
				score
			}));
			this.CallClientApi(string.Concat(new object[]
			{
				"submit score ",
				score,
				", lb ",
				lbId
			}), delegate
			{
				this.mGHManager.CallGmsApi("games.Games", "Leaderboards", "submitScore", new object[]
				{
					lbId,
					score
				});
			}, callback);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006ABC File Offset: 0x00004CBC
		public void LoadState(int slot, OnStateLoadedListener listener)
		{
			Logger.d("AndroidClient.LoadState, slot=" + slot);
			this.CallClientApi("load state slot=" + slot, delegate
			{
				OnStateResultProxy callbackProxy = new OnStateResultProxy(this, listener);
				this.mGHManager.CallGmsApiWithResult("appstate.AppStateManager", null, "load", callbackProxy, new object[]
				{
					slot
				});
			}, null);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006B28 File Offset: 0x00004D28
		internal void ResolveState(int slot, string resolvedVersion, byte[] resolvedData, OnStateLoadedListener listener)
		{
			Logger.d(string.Format("AndroidClient.ResolveState, slot={0}, ver={1}, data={2}", slot, resolvedVersion, resolvedData));
			this.CallClientApi("resolve state slot=" + slot, delegate
			{
				this.mGHManager.CallGmsApiWithResult("appstate.AppStateManager", null, "resolve", new OnStateResultProxy(this, listener), new object[]
				{
					slot,
					resolvedVersion,
					resolvedData
				});
			}, null);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public void UpdateState(int slot, byte[] data, OnStateLoadedListener listener)
		{
			Logger.d(string.Format("AndroidClient.UpdateState, slot={0}, data={1}", slot, Logger.describe(data)));
			this.CallClientApi("update state, slot=" + slot, delegate
			{
				this.mGHManager.CallGmsApi("appstate.AppStateManager", null, "update", new object[]
				{
					slot,
					data
				});
			}, null);
			listener.OnStateSaved(true, slot);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006C34 File Offset: 0x00004E34
		public void SetCloudCacheEncrypter(BufferEncrypter encrypter)
		{
			Logger.d("Ignoring cloud cache encrypter (not used in Android)");
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006C40 File Offset: 0x00004E40
		public void RegisterInvitationDelegate(InvitationReceivedDelegate deleg)
		{
			Logger.d("AndroidClient.RegisterInvitationDelegate");
			if (deleg == null)
			{
				Logger.w("AndroidClient.RegisterInvitationDelegate called w/ null argument.");
				return;
			}
			this.mInvitationDelegate = deleg;
			if (!this.mRegisteredInvitationListener)
			{
				Logger.d("Registering an invitation listener.");
				this.RegisterInvitationListener();
			}
			if (this.mInvitationFromNotification != null)
			{
				Logger.d("Delivering pending invitation from notification now.");
				Invitation inv = this.mInvitationFromNotification;
				this.mInvitationFromNotification = null;
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					if (this.mInvitationDelegate != null)
					{
						this.mInvitationDelegate(inv, true);
					}
				});
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public Invitation GetInvitationFromNotification()
		{
			Logger.d("AndroidClient.GetInvitationFromNotification");
			Logger.d("Returning invitation: " + ((this.mInvitationFromNotification != null) ? this.mInvitationFromNotification.ToString() : "(null)"));
			return this.mInvitationFromNotification;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006D1C File Offset: 0x00004F1C
		public bool HasInvitationFromNotification()
		{
			bool flag = this.mInvitationFromNotification != null;
			Logger.d("AndroidClient.HasInvitationFromNotification, returning " + flag);
			return flag;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006D4C File Offset: 0x00004F4C
		private void RegisterInvitationListener()
		{
			Logger.d("AndroidClient.RegisterInvitationListener");
			this.CallClientApi("register invitation listener", delegate
			{
				this.mGHManager.CallGmsApi("games.Games", "Invitations", "registerInvitationListener", new object[]
				{
					new AndroidClient.OnInvitationReceivedProxy(this)
				});
			}, null);
			this.mRegisteredInvitationListener = true;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006D78 File Offset: 0x00004F78
		public IRealTimeMultiplayerClient GetRtmpClient()
		{
			return this.mRtmpClient;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006D80 File Offset: 0x00004F80
		public ITurnBasedMultiplayerClient GetTbmpClient()
		{
			return this.mTbmpClient;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006D88 File Offset: 0x00004F88
		internal GameHelperManager GHManager
		{
			get
			{
				return this.mGHManager;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006D90 File Offset: 0x00004F90
		internal void ClearInvitationIfFromNotification(string invitationId)
		{
			Logger.d("AndroidClient.ClearInvitationIfFromNotification: " + invitationId);
			if (this.mInvitationFromNotification != null && this.mInvitationFromNotification.InvitationId.Equals(invitationId))
			{
				Logger.d("Clearing invitation from notification: " + invitationId);
				this.mInvitationFromNotification = null;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private void CheckForConnectionExtras()
		{
			Logger.d("AndroidClient: CheckInvitationFromNotification.");
			Logger.d("AndroidClient: looking for invitation in our GameHelper.");
			Invitation invFromNotif = null;
			AndroidJavaObject invitation = this.mGHManager.GetInvitation();
			AndroidJavaObject turnBasedMatch = this.mGHManager.GetTurnBasedMatch();
			this.mGHManager.ClearInvitationAndTurnBasedMatch();
			if (invitation != null)
			{
				Logger.d("Found invitation in GameHelper. Converting.");
				invFromNotif = this.ConvertInvitation(invitation);
				Logger.d("Found invitation in our GameHelper: " + invFromNotif);
			}
			else
			{
				Logger.d("No invitation in our GameHelper. Trying SignInHelperManager.");
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.SignInHelperManager");
				using (AndroidJavaObject androidJavaObject = @class.CallStatic<AndroidJavaObject>("getInstance", new object[0]))
				{
					if (androidJavaObject.Call<bool>("hasInvitation", new object[0]))
					{
						invFromNotif = this.ConvertInvitation(androidJavaObject.Call<AndroidJavaObject>("getInvitation", new object[0]));
						Logger.d("Found invitation in SignInHelperManager: " + invFromNotif);
						androidJavaObject.Call("forgetInvitation", new object[0]);
					}
					else
					{
						Logger.d("No invitation in SignInHelperManager either.");
					}
				}
			}
			TurnBasedMatch turnBasedMatch2 = null;
			if (turnBasedMatch != null)
			{
				Logger.d("Found match in GameHelper. Converting.");
				turnBasedMatch2 = JavaUtil.ConvertMatch(this.mUserId, turnBasedMatch);
				Logger.d("Match from GameHelper: " + turnBasedMatch2);
			}
			else
			{
				Logger.d("No match in our GameHelper. Trying SignInHelperManager.");
				AndroidJavaClass class2 = JavaUtil.GetClass("com.google.example.games.pluginsupport.SignInHelperManager");
				using (AndroidJavaObject androidJavaObject2 = class2.CallStatic<AndroidJavaObject>("getInstance", new object[0]))
				{
					if (androidJavaObject2.Call<bool>("hasTurnBasedMatch", new object[0]))
					{
						turnBasedMatch2 = JavaUtil.ConvertMatch(this.mUserId, androidJavaObject2.Call<AndroidJavaObject>("getTurnBasedMatch", new object[0]));
						Logger.d("Found match in SignInHelperManager: " + turnBasedMatch2);
						androidJavaObject2.Call("forgetTurnBasedMatch", new object[0]);
					}
					else
					{
						Logger.d("No match in SignInHelperManager either.");
					}
				}
			}
			if (invFromNotif != null)
			{
				if (this.mInvitationDelegate != null)
				{
					Logger.d("Invoking invitation received delegate to deal with invitation  from notification.");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						if (this.mInvitationDelegate != null)
						{
							this.mInvitationDelegate(invFromNotif, true);
						}
					});
				}
				else
				{
					Logger.d("No delegate to handle invitation from notification; queueing.");
					this.mInvitationFromNotification = invFromNotif;
				}
			}
			if (turnBasedMatch2 != null)
			{
				this.mTbmpClient.HandleMatchFromNotification(turnBasedMatch2);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007094 File Offset: 0x00005294
		private Invitation ConvertInvitation(AndroidJavaObject invObj)
		{
			Logger.d("Converting Android invitation to our Invitation object.");
			string invId = invObj.Call<string>("getInvitationId", new object[0]);
			int num = invObj.Call<int>("getInvitationType", new object[0]);
			Participant inviter;
			using (AndroidJavaObject androidJavaObject = invObj.Call<AndroidJavaObject>("getInviter", new object[0]))
			{
				inviter = JavaUtil.ConvertParticipant(androidJavaObject);
			}
			int variant = invObj.Call<int>("getVariant", new object[0]);
			int num2 = num;
			Invitation.InvType invType;
			if (num2 != 0)
			{
				if (num2 != 1)
				{
					Logger.e("Unknown invitation type " + num);
					invType = Invitation.InvType.Unknown;
				}
				else
				{
					invType = Invitation.InvType.TurnBased;
				}
			}
			else
			{
				invType = Invitation.InvType.RealTime;
			}
			Invitation invitation = new Invitation(invType, invId, inviter, variant);
			Logger.d("Converted invitation: " + invitation.ToString());
			return invitation;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007194 File Offset: 0x00005394
		private void OnInvitationReceived(AndroidJavaObject invitationObj)
		{
			Logger.d("AndroidClient.OnInvitationReceived. Converting invitation...");
			Invitation inv = this.ConvertInvitation(invitationObj);
			Logger.d("Invitation: " + inv.ToString());
			if (this.mInvitationDelegate != null)
			{
				Logger.d("Delivering invitation to invitation received delegate.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					if (this.mInvitationDelegate != null)
					{
						this.mInvitationDelegate(inv, false);
					}
				});
			}
			else
			{
				Logger.w("AndroidClient.OnInvitationReceived discarding invitation because  delegate is null.");
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007214 File Offset: 0x00005414
		private void OnInvitationRemoved(string invitationId)
		{
			Logger.d("AndroidClient.OnInvitationRemoved: " + invitationId);
			this.ClearInvitationIfFromNotification(invitationId);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007230 File Offset: 0x00005430
		public string PlayerId
		{
			get
			{
				return this.mUserId;
			}
		}

		// Token: 0x04000091 RID: 145
		private const int RC_UNUSED = 9999;

		// Token: 0x04000092 RID: 146
		private GameHelperManager mGHManager;

		// Token: 0x04000093 RID: 147
		private AndroidClient.AuthState mAuthState;

		// Token: 0x04000094 RID: 148
		private bool mSilentAuth;

		// Token: 0x04000095 RID: 149
		private string mUserId;

		// Token: 0x04000096 RID: 150
		private string mUserDisplayName;

		// Token: 0x04000097 RID: 151
		private Action<bool> mAuthCallback;

		// Token: 0x04000098 RID: 152
		private AchievementBank mAchievementBank = new AchievementBank();

		// Token: 0x04000099 RID: 153
		private List<Action> mActionsPendingSignIn = new List<Action>();

		// Token: 0x0400009A RID: 154
		private bool mSignOutInProgress;

		// Token: 0x0400009B RID: 155
		private AndroidRtmpClient mRtmpClient;

		// Token: 0x0400009C RID: 156
		private AndroidTbmpClient mTbmpClient;

		// Token: 0x0400009D RID: 157
		private InvitationReceivedDelegate mInvitationDelegate;

		// Token: 0x0400009E RID: 158
		private bool mRegisteredInvitationListener;

		// Token: 0x0400009F RID: 159
		private Invitation mInvitationFromNotification;

		// Token: 0x02000036 RID: 54
		private enum AuthState
		{
			// Token: 0x040000A1 RID: 161
			NoAuth,
			// Token: 0x040000A2 RID: 162
			AuthPending,
			// Token: 0x040000A3 RID: 163
			InProgress,
			// Token: 0x040000A4 RID: 164
			LoadingAchs,
			// Token: 0x040000A5 RID: 165
			Done
		}

		// Token: 0x02000037 RID: 55
		private class OnAchievementsLoadedResultProxy : AndroidJavaProxy
		{
			// Token: 0x060001E4 RID: 484 RVA: 0x000073D0 File Offset: 0x000055D0
			internal OnAchievementsLoadedResultProxy(AndroidClient c) : base("com.google.android.gms.common.api.ResultCallback")
			{
				this.mOwner = c;
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x000073E4 File Offset: 0x000055E4
			public void onResult(AndroidJavaObject result)
			{
				Logger.d("OnAchievementsLoadedResultProxy invoked");
				Logger.d("    result=" + result);
				int statusCode = JavaUtil.GetStatusCode(result);
				AndroidJavaObject androidJavaObject = JavaUtil.CallNullSafeObjectMethod(result, "getAchievements", new object[0]);
				this.mOwner.OnAchievementsLoaded(statusCode, androidJavaObject);
				if (androidJavaObject != null)
				{
					androidJavaObject.Dispose();
				}
			}

			// Token: 0x040000A6 RID: 166
			private AndroidClient mOwner;
		}

		// Token: 0x02000038 RID: 56
		private class OnInvitationReceivedProxy : AndroidJavaProxy
		{
			// Token: 0x060001E6 RID: 486 RVA: 0x00007440 File Offset: 0x00005640
			internal OnInvitationReceivedProxy(AndroidClient owner) : base("com.google.android.gms.games.multiplayer.OnInvitationReceivedListener")
			{
				this.mOwner = owner;
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00007454 File Offset: 0x00005654
			public void onInvitationReceived(AndroidJavaObject invitationObj)
			{
				Logger.d("OnInvitationReceivedProxy.onInvitationReceived");
				this.mOwner.OnInvitationReceived(invitationObj);
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x0000746C File Offset: 0x0000566C
			public void onInvitationRemoved(string invitationId)
			{
				Logger.d("OnInvitationReceivedProxy.onInvitationRemoved");
				this.mOwner.OnInvitationRemoved(invitationId);
			}

			// Token: 0x040000A7 RID: 167
			private AndroidClient mOwner;
		}
	}
}
