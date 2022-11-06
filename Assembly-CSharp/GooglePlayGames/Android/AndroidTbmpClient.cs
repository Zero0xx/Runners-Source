using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x0200003F RID: 63
	internal class AndroidTbmpClient : ITurnBasedMultiplayerClient
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00008DD8 File Offset: 0x00006FD8
		internal AndroidTbmpClient(AndroidClient client)
		{
			this.mClient = client;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008DE8 File Offset: 0x00006FE8
		public void OnSignInSucceeded()
		{
			Logger.d("AndroidTbmpClient.OnSignInSucceeded");
			Logger.d("Querying for max match data size...");
			this.mMaxMatchDataSize = this.mClient.GHManager.CallGmsApi<int>("games.Games", "TurnBasedMultiplayer", "getMaxMatchDataSize", new object[0]);
			Logger.d("Max match data size: " + this.mMaxMatchDataSize);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008E50 File Offset: 0x00007050
		public void CreateQuickMatch(int minOpponents, int maxOpponents, int variant, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d(string.Format("AndroidTbmpClient.CreateQuickMatch, opponents {0}-{1}, var {2}", minOpponents, maxOpponents, variant));
			this.mClient.CallClientApi("tbmp create quick game", delegate
			{
				AndroidTbmpClient.ResultProxy resultProxy = new AndroidTbmpClient.ResultProxy(this, "createMatch");
				resultProxy.SetMatchCallback(callback);
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.TbmpUtils");
				using (AndroidJavaObject androidJavaObject = @class.CallStatic<AndroidJavaObject>("createQuickMatch", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					minOpponents,
					maxOpponents,
					variant
				}))
				{
					androidJavaObject.Call("setResultCallback", new object[]
					{
						resultProxy
					});
				}
			}, delegate(bool success)
			{
				if (!success)
				{
					Logger.w("Failed to create tbmp quick match: client disconnected.");
					if (callback != null)
					{
						callback(false, null);
					}
				}
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008EE0 File Offset: 0x000070E0
		public void CreateWithInvitationScreen(int minOpponents, int maxOpponents, int variant, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d(string.Format("AndroidTbmpClient.CreateWithInvitationScreen, opponents {0}-{1}, variant {2}", minOpponents, maxOpponents, variant));
			this.mClient.CallClientApi("tbmp launch invitation screen", delegate
			{
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.SelectOpponentsHelperActivity");
				@class.CallStatic("launch", new object[]
				{
					false,
					this.mClient.GetActivity(),
					new AndroidTbmpClient.SelectOpponentsProxy(this, callback, variant),
					Logger.DebugLogEnabled,
					minOpponents,
					maxOpponents
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					Logger.w("Failed to create tbmp w/ invite screen: client disconnected.");
					if (callback != null)
					{
						callback(false, null);
					}
				}
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008F70 File Offset: 0x00007170
		public void AcceptFromInbox(Action<bool, TurnBasedMatch> callback)
		{
			Logger.d(string.Format("AndroidTbmpClient.AcceptFromInbox", new object[0]));
			this.mClient.CallClientApi("tbmp launch inbox", delegate
			{
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.InvitationInboxHelperActivity");
				@class.CallStatic("launch", new object[]
				{
					false,
					this.mClient.GetActivity(),
					new AndroidTbmpClient.InvitationInboxProxy(this, callback),
					Logger.DebugLogEnabled
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					Logger.w("Failed to accept tbmp w/ inbox: client disconnected.");
					if (callback != null)
					{
						callback(false, null);
					}
				}
			});
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008FD0 File Offset: 0x000071D0
		public void AcceptInvitation(string invitationId, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d("AndroidTbmpClient.AcceptInvitation invitationId=" + invitationId);
			this.TbmpApiCall("accept invitation", "acceptInvitation", null, callback, new object[]
			{
				invitationId
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000900C File Offset: 0x0000720C
		public void DeclineInvitation(string invitationId)
		{
			Logger.d("AndroidTbmpClient.DeclineInvitation, invitationId=" + invitationId);
			this.TbmpApiCall("decline invitation", "declineInvitation", null, null, new object[]
			{
				invitationId
			});
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009048 File Offset: 0x00007248
		private void TbmpApiCall(string simpleDesc, string methodName, Action<bool> callback, Action<bool, TurnBasedMatch> tbmpCallback, params object[] args)
		{
			this.mClient.CallClientApi(simpleDesc, delegate
			{
				AndroidTbmpClient.ResultProxy resultProxy = new AndroidTbmpClient.ResultProxy(this, methodName);
				if (callback != null)
				{
					resultProxy.SetSuccessCallback(callback);
				}
				if (tbmpCallback != null)
				{
					resultProxy.SetMatchCallback(tbmpCallback);
				}
				this.mClient.GHManager.CallGmsApiWithResult("games.Games", "TurnBasedMultiplayer", methodName, resultProxy, args);
			}, delegate(bool success)
			{
				if (!success)
				{
					Logger.w("Failed to " + simpleDesc + ": client disconnected.");
					if (callback != null)
					{
						callback(false);
					}
				}
			});
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000090B0 File Offset: 0x000072B0
		public void TakeTurn(string matchId, byte[] data, string pendingParticipantId, Action<bool> callback)
		{
			Logger.d(string.Format("AndroidTbmpClient.TakeTurn matchId={0}, data={1}, pending={2}", matchId, (data != null) ? ("[" + data.Length + "bytes]") : "(null)", pendingParticipantId));
			this.TbmpApiCall("tbmp take turn", "takeTurn", callback, null, new object[]
			{
				matchId,
				data,
				pendingParticipantId
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000911C File Offset: 0x0000731C
		public int GetMaxMatchDataSize()
		{
			return this.mMaxMatchDataSize;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009124 File Offset: 0x00007324
		public void Finish(string matchId, byte[] data, MatchOutcome outcome, Action<bool> callback)
		{
			Logger.d(string.Format("AndroidTbmpClient.Finish matchId={0}, data={1} outcome={2}", matchId, (data != null) ? (data.Length + " bytes") : "(null)", outcome));
			Logger.d("Preparing list of participant results as Android ArrayList.");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.ArrayList", new object[0]);
			if (outcome != null)
			{
				foreach (string text in outcome.ParticipantIds)
				{
					Logger.d("Converting participant result to Android object: " + text);
					AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("com.google.android.gms.games.multiplayer.ParticipantResult", new object[]
					{
						text,
						JavaUtil.GetAndroidParticipantResult(outcome.GetResultFor(text)),
						outcome.GetPlacementFor(text)
					});
					Logger.d("Adding participant result to Android ArrayList.");
					androidJavaObject.Call<bool>("add", new object[]
					{
						androidJavaObject2
					});
					androidJavaObject2.Dispose();
				}
			}
			this.TbmpApiCall("tbmp finish w/ outcome", "finishMatch", callback, null, new object[]
			{
				matchId,
				data,
				androidJavaObject
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000926C File Offset: 0x0000746C
		public void AcknowledgeFinished(string matchId, Action<bool> callback)
		{
			Logger.d("AndroidTbmpClient.AcknowledgeFinished, matchId=" + matchId);
			this.TbmpApiCall("tbmp ack finish", "finishMatch", callback, null, new object[]
			{
				matchId
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000092A8 File Offset: 0x000074A8
		public void Leave(string matchId, Action<bool> callback)
		{
			Logger.d("AndroidTbmpClient.Leave, matchId=" + matchId);
			this.TbmpApiCall("tbmp leave", "leaveMatch", callback, null, new object[]
			{
				matchId
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000092E4 File Offset: 0x000074E4
		public void LeaveDuringTurn(string matchId, string pendingParticipantId, Action<bool> callback)
		{
			Logger.d("AndroidTbmpClient.LeaveDuringTurn, matchId=" + matchId + ", pending=" + pendingParticipantId);
			this.TbmpApiCall("tbmp leave during turn", "leaveMatchDuringTurn", callback, null, new object[]
			{
				matchId,
				pendingParticipantId
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009328 File Offset: 0x00007528
		public void Cancel(string matchId, Action<bool> callback)
		{
			Logger.d("AndroidTbmpClient.Cancel, matchId=" + matchId);
			this.TbmpApiCall("tbmp cancel", "cancelMatch", callback, null, new object[]
			{
				matchId
			});
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009364 File Offset: 0x00007564
		public void Rematch(string matchId, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d("AndroidTbmpClient.Rematch, matchId=" + matchId);
			this.TbmpApiCall("tbmp rematch", "rematch", null, callback, new object[]
			{
				matchId
			});
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000093A0 File Offset: 0x000075A0
		public void RegisterMatchDelegate(MatchDelegate deleg)
		{
			Logger.d("AndroidTbmpClient.RegisterMatchDelegate");
			if (deleg == null)
			{
				Logger.w("Can't register a null match delegate.");
				return;
			}
			this.mMatchDelegate = deleg;
			if (this.mMatchFromNotification != null)
			{
				Logger.d("Delivering pending match to the newly registered delegate.");
				TurnBasedMatch match = this.mMatchFromNotification;
				this.mMatchFromNotification = null;
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					deleg(match, true);
				});
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000942C File Offset: 0x0000762C
		private void OnSelectOpponentsResult(bool success, AndroidJavaObject opponents, bool hasAutoMatch, AndroidJavaObject autoMatchCriteria, Action<bool, TurnBasedMatch> callback, int variant)
		{
			Logger.d(string.Concat(new object[]
			{
				"AndroidTbmpClient.OnSelectOpponentsResult, success=",
				success,
				", hasAutoMatch=",
				hasAutoMatch
			}));
			if (!success)
			{
				Logger.w("Tbmp select opponents dialog terminated with failure.");
				if (callback != null)
				{
					Logger.d("Reporting select-opponents dialog failure to callback.");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						callback(false, null);
					});
				}
				return;
			}
			Logger.d("Creating TBMP match from opponents received from dialog.");
			this.mClient.CallClientApi("create match w/ opponents from dialog", delegate
			{
				AndroidTbmpClient.ResultProxy resultProxy = new AndroidTbmpClient.ResultProxy(this, "createMatch");
				resultProxy.SetMatchCallback(callback);
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.TbmpUtils");
				using (AndroidJavaObject androidJavaObject = @class.CallStatic<AndroidJavaObject>("create", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					opponents,
					variant,
					(!hasAutoMatch) ? null : autoMatchCriteria
				}))
				{
					androidJavaObject.Call("setResultCallback", new object[]
					{
						resultProxy
					});
				}
			}, delegate(bool ok)
			{
				if (!ok)
				{
					Logger.w("Failed to create match w/ opponents from dialog: client disconnected.");
					if (callback != null)
					{
						callback(false, null);
					}
				}
			});
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000950C File Offset: 0x0000770C
		private void OnInvitationInboxResult(bool success, string invitationId, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d(string.Concat(new object[]
			{
				"AndroidTbmpClient.OnInvitationInboxResult, success=",
				success,
				", invitationId=",
				invitationId
			}));
			if (!success)
			{
				Logger.w("Tbmp invitation inbox returned failure result.");
				if (callback != null)
				{
					Logger.d("Reporting tbmp invitation inbox failure to callback.");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						callback(false, null);
					});
				}
				return;
			}
			Logger.d("Accepting invite received from inbox: " + invitationId);
			this.TbmpApiCall("accept invite returned from inbox", "acceptInvitation", null, callback, new object[]
			{
				invitationId
			});
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000095BC File Offset: 0x000077BC
		private void OnInvitationInboxTurnBasedMatch(AndroidJavaObject matchObj, Action<bool, TurnBasedMatch> callback)
		{
			Logger.d("AndroidTbmpClient.OnInvitationTurnBasedMatch");
			Logger.d("Converting received match to our format...");
			TurnBasedMatch match = JavaUtil.ConvertMatch(this.mClient.PlayerId, matchObj);
			Logger.d("Resulting match: " + match);
			if (callback != null)
			{
				Logger.d("Invoking match callback w/ success.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					callback(true, match);
				});
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000963C File Offset: 0x0000783C
		internal void HandleMatchFromNotification(TurnBasedMatch match)
		{
			Logger.d("AndroidTbmpClient.HandleMatchFromNotification");
			Logger.d("Got match from notification: " + match);
			if (this.mMatchDelegate != null)
			{
				Logger.d("Delivering match directly to match delegate.");
				MatchDelegate del = this.mMatchDelegate;
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					del(match, true);
				});
			}
			else
			{
				Logger.d("Since we have no match delegate, holding on to the match until we have one.");
				this.mMatchFromNotification = match;
			}
		}

		// Token: 0x040000BC RID: 188
		private AndroidClient mClient;

		// Token: 0x040000BD RID: 189
		private int mMaxMatchDataSize;

		// Token: 0x040000BE RID: 190
		private TurnBasedMatch mMatchFromNotification;

		// Token: 0x040000BF RID: 191
		private MatchDelegate mMatchDelegate;

		// Token: 0x02000040 RID: 64
		private class ResultProxy : AndroidJavaProxy
		{
			// Token: 0x06000248 RID: 584 RVA: 0x000096D0 File Offset: 0x000078D0
			internal ResultProxy(AndroidTbmpClient owner, string method) : base("com.google.android.gms.common.api.ResultCallback")
			{
				this.mOwner = owner;
				this.mSuccessCodes.Add(0);
				this.mSuccessCodes.Add(5);
				this.mSuccessCodes.Add(3);
				this.mMethod = method;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00009730 File Offset: 0x00007930
			public void SetSuccessCallback(Action<bool> callback)
			{
				this.mSuccessCallback = callback;
			}

			// Token: 0x0600024A RID: 586 RVA: 0x0000973C File Offset: 0x0000793C
			public void SetMatchCallback(Action<bool, TurnBasedMatch> callback)
			{
				this.mMatchCallback = callback;
			}

			// Token: 0x0600024B RID: 587 RVA: 0x00009748 File Offset: 0x00007948
			public void AddSuccessCodes(params int[] codes)
			{
				foreach (int item in codes)
				{
					this.mSuccessCodes.Add(item);
				}
			}

			// Token: 0x0600024C RID: 588 RVA: 0x0000977C File Offset: 0x0000797C
			public void onResult(AndroidJavaObject result)
			{
				Logger.d("ResultProxy got result for method: " + this.mMethod);
				int statusCode = JavaUtil.GetStatusCode(result);
				bool isSuccess = this.mSuccessCodes.Contains(statusCode);
				TurnBasedMatch match = null;
				if (isSuccess)
				{
					Logger.d(string.Concat(new object[]
					{
						"SUCCESS result from method ",
						this.mMethod,
						": ",
						statusCode
					}));
					if (this.mMatchCallback != null)
					{
						Logger.d("Attempting to get match from result of " + this.mMethod);
						AndroidJavaObject androidJavaObject = JavaUtil.CallNullSafeObjectMethod(result, "getMatch", new object[0]);
						if (androidJavaObject != null)
						{
							Logger.d("Successfully got match from result of " + this.mMethod);
							match = JavaUtil.ConvertMatch(this.mOwner.mClient.PlayerId, androidJavaObject);
							androidJavaObject.Dispose();
						}
						else
						{
							Logger.w("Got a NULL match from result of " + this.mMethod);
						}
					}
				}
				else
				{
					Logger.w(string.Concat(new object[]
					{
						"ERROR result from ",
						this.mMethod,
						": ",
						statusCode
					}));
				}
				if (this.mSuccessCallback != null)
				{
					Logger.d(string.Concat(new object[]
					{
						"Invoking success callback (success=",
						isSuccess,
						") for result of method ",
						this.mMethod
					}));
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						this.mSuccessCallback(isSuccess);
					});
				}
				if (this.mMatchCallback != null)
				{
					Logger.d(string.Concat(new object[]
					{
						"Invoking match callback for result of method ",
						this.mMethod,
						": (success=",
						isSuccess,
						", match=",
						(match != null) ? match.ToString() : "(null)"
					}));
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						this.mMatchCallback(isSuccess, match);
					});
				}
			}

			// Token: 0x040000C0 RID: 192
			private AndroidTbmpClient mOwner;

			// Token: 0x040000C1 RID: 193
			private string mMethod = "?";

			// Token: 0x040000C2 RID: 194
			private Action<bool> mSuccessCallback;

			// Token: 0x040000C3 RID: 195
			private Action<bool, TurnBasedMatch> mMatchCallback;

			// Token: 0x040000C4 RID: 196
			private List<int> mSuccessCodes = new List<int>();
		}

		// Token: 0x02000041 RID: 65
		private class SelectOpponentsProxy : AndroidJavaProxy
		{
			// Token: 0x0600024D RID: 589 RVA: 0x0000999C File Offset: 0x00007B9C
			internal SelectOpponentsProxy(AndroidTbmpClient owner, Action<bool, TurnBasedMatch> callback, int variant) : base("com.google.example.games.pluginsupport.SelectOpponentsHelperActivity$Listener")
			{
				this.mOwner = owner;
				this.mCallback = callback;
				this.mVariant = variant;
			}

			// Token: 0x0600024E RID: 590 RVA: 0x000099CC File Offset: 0x00007BCC
			public void onSelectOpponentsResult(bool success, AndroidJavaObject opponents, bool hasAutoMatch, AndroidJavaObject autoMatchCriteria)
			{
				this.mOwner.OnSelectOpponentsResult(success, opponents, hasAutoMatch, autoMatchCriteria, this.mCallback, this.mVariant);
			}

			// Token: 0x040000C5 RID: 197
			private AndroidTbmpClient mOwner;

			// Token: 0x040000C6 RID: 198
			private Action<bool, TurnBasedMatch> mCallback;

			// Token: 0x040000C7 RID: 199
			private int mVariant;
		}

		// Token: 0x02000042 RID: 66
		private class InvitationInboxProxy : AndroidJavaProxy
		{
			// Token: 0x0600024F RID: 591 RVA: 0x000099EC File Offset: 0x00007BEC
			internal InvitationInboxProxy(AndroidTbmpClient owner, Action<bool, TurnBasedMatch> callback) : base("com.google.example.games.pluginsupport.InvitationInboxHelperActivity$Listener")
			{
				this.mOwner = owner;
				this.mCallback = callback;
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00009A08 File Offset: 0x00007C08
			public void onInvitationInboxResult(bool success, string invitationId)
			{
				this.mOwner.OnInvitationInboxResult(success, invitationId, this.mCallback);
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00009A20 File Offset: 0x00007C20
			public void onTurnBasedMatch(AndroidJavaObject match)
			{
				this.mOwner.OnInvitationInboxTurnBasedMatch(match, this.mCallback);
			}

			// Token: 0x040000C8 RID: 200
			private AndroidTbmpClient mOwner;

			// Token: 0x040000C9 RID: 201
			private Action<bool, TurnBasedMatch> mCallback;
		}
	}
}
