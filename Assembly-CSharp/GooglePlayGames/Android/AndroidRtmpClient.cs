using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000039 RID: 57
	internal class AndroidRtmpClient : IRealTimeMultiplayerClient
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00007484 File Offset: 0x00005684
		public AndroidRtmpClient(AndroidClient client)
		{
			this.mClient = client;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000074C0 File Offset: 0x000056C0
		public void CreateQuickGame(int minOpponents, int maxOpponents, int variant, RealTimeMultiplayerListener listener)
		{
			Logger.d(string.Format("AndroidRtmpClient.CreateQuickGame, opponents={0}-{1}, variant={2}", minOpponents, maxOpponents, variant));
			if (!this.PrepareToCreateRoom("CreateQuickGame", listener))
			{
				return;
			}
			this.mRtmpListener = listener;
			this.mVariant = variant;
			this.mClient.CallClientApi("rtmp create quick game", delegate
			{
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.RtmpUtils");
				@class.CallStatic("createQuickGame", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					minOpponents,
					maxOpponents,
					variant,
					new AndroidRtmpClient.RoomUpdateProxy(this),
					new AndroidRtmpClient.RoomStatusUpdateProxy(this),
					new AndroidRtmpClient.RealTimeMessageReceivedProxy(this)
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					this.FailRoomSetup("Failed to create game because GoogleApiClient was disconnected");
				}
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007570 File Offset: 0x00005770
		public void CreateWithInvitationScreen(int minOpponents, int maxOpponents, int variant, RealTimeMultiplayerListener listener)
		{
			Logger.d(string.Format("AndroidRtmpClient.CreateWithInvitationScreen, opponents={0}-{1}, variant={2}", minOpponents, maxOpponents, variant));
			if (!this.PrepareToCreateRoom("CreateWithInvitationScreen", listener))
			{
				return;
			}
			this.mRtmpListener = listener;
			this.mVariant = variant;
			this.mClient.CallClientApi("rtmp create with invitation screen", delegate
			{
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.SelectOpponentsHelperActivity");
				this.mLaunchedExternalActivity = true;
				@class.CallStatic("launch", new object[]
				{
					true,
					this.mClient.GetActivity(),
					new AndroidRtmpClient.SelectOpponentsProxy(this),
					Logger.DebugLogEnabled,
					minOpponents,
					maxOpponents
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					this.FailRoomSetup("Failed to create game because GoogleApiClient was disconnected");
				}
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007610 File Offset: 0x00005810
		public void AcceptFromInbox(RealTimeMultiplayerListener listener)
		{
			Logger.d("AndroidRtmpClient.AcceptFromInbox.");
			if (!this.PrepareToCreateRoom("AcceptFromInbox", listener))
			{
				return;
			}
			this.mRtmpListener = listener;
			this.mClient.CallClientApi("rtmp accept with inbox screen", delegate
			{
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.InvitationInboxHelperActivity");
				this.mLaunchedExternalActivity = true;
				@class.CallStatic("launch", new object[]
				{
					true,
					this.mClient.GetActivity(),
					new AndroidRtmpClient.InvitationInboxProxy(this),
					Logger.DebugLogEnabled
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					this.FailRoomSetup("Failed to accept from inbox because GoogleApiClient was disconnected");
				}
			});
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007668 File Offset: 0x00005868
		public void AcceptInvitation(string invitationId, RealTimeMultiplayerListener listener)
		{
			Logger.d("AndroidRtmpClient.AcceptInvitation " + invitationId);
			if (!this.PrepareToCreateRoom("AcceptInvitation", listener))
			{
				return;
			}
			this.mRtmpListener = listener;
			this.mClient.ClearInvitationIfFromNotification(invitationId);
			this.mClient.CallClientApi("rtmp accept invitation", delegate
			{
				Logger.d("Accepting invite via support lib.");
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.RtmpUtils");
				@class.CallStatic("accept", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					invitationId,
					new AndroidRtmpClient.RoomUpdateProxy(this),
					new AndroidRtmpClient.RoomStatusUpdateProxy(this),
					new AndroidRtmpClient.RealTimeMessageReceivedProxy(this)
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					this.FailRoomSetup("Failed to accept invitation because GoogleApiClient was disconnected");
				}
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000076F0 File Offset: 0x000058F0
		public void SendMessage(bool reliable, string participantId, byte[] data)
		{
			this.SendMessage(reliable, participantId, data, 0, data.Length);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007700 File Offset: 0x00005900
		public void SendMessageToAll(bool reliable, byte[] data)
		{
			this.SendMessage(reliable, null, data, 0, data.Length);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007710 File Offset: 0x00005910
		public void SendMessageToAll(bool reliable, byte[] data, int offset, int length)
		{
			this.SendMessage(reliable, null, data, offset, length);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007720 File Offset: 0x00005920
		public void SendMessage(bool reliable, string participantId, byte[] data, int offset, int length)
		{
			Logger.d(string.Format("AndroidRtmpClient.SendMessage, reliable={0}, participantId={1}, data[]={2} bytes, offset={3}, length={4}", new object[]
			{
				reliable,
				participantId,
				data.Length,
				offset,
				length
			}));
			if (!this.CheckConnectedRoom("SendMessage"))
			{
				return;
			}
			if (this.mSelf != null && this.mSelf.ParticipantId.Equals(participantId))
			{
				Logger.d("Ignoring request to send message to self, " + participantId);
				return;
			}
			byte[] dataToSend = Misc.GetSubsetBytes(data, offset, length);
			if (participantId == null)
			{
				List<Participant> connectedParticipants = this.GetConnectedParticipants();
				foreach (Participant participant in connectedParticipants)
				{
					if (participant.ParticipantId != null && !participant.Equals(this.mSelf))
					{
						this.SendMessage(reliable, participant.ParticipantId, dataToSend, 0, dataToSend.Length);
					}
				}
				return;
			}
			this.mClient.CallClientApi("send message to " + participantId, delegate
			{
				if (this.mRoom != null)
				{
					string text = this.mRoom.Call<string>("getRoomId", new object[0]);
					if (reliable)
					{
						this.mClient.GHManager.CallGmsApi<int>("games.Games", "RealTimeMultiplayer", "sendReliableMessage", new object[]
						{
							null,
							dataToSend,
							text,
							participantId
						});
					}
					else
					{
						this.mClient.GHManager.CallGmsApi<int>("games.Games", "RealTimeMultiplayer", "sendUnreliableMessage", new object[]
						{
							dataToSend,
							text,
							participantId
						});
					}
				}
				else
				{
					Logger.w("Not sending message because real-time room was torn down.");
				}
			}, null);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000078B8 File Offset: 0x00005AB8
		public List<Participant> GetConnectedParticipants()
		{
			Logger.d("AndroidRtmpClient.GetConnectedParticipants");
			if (!this.CheckConnectedRoom("GetConnectedParticipants"))
			{
				return null;
			}
			object obj = this.mParticipantListsLock;
			List<Participant> result;
			lock (obj)
			{
				result = this.mConnectedParticipants;
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007920 File Offset: 0x00005B20
		public Participant GetParticipant(string id)
		{
			Logger.d("AndroidRtmpClient.GetParticipant: " + id);
			if (!this.CheckConnectedRoom("GetParticipant"))
			{
				return null;
			}
			object obj = this.mParticipantListsLock;
			List<Participant> list;
			lock (obj)
			{
				list = this.mAllParticipants;
			}
			if (list == null)
			{
				Logger.e("RtmpGetParticipant called without a valid room!");
				return null;
			}
			foreach (Participant participant in list)
			{
				if (participant.ParticipantId.Equals(id))
				{
					return participant;
				}
			}
			Logger.e("Participant not found in room! id: " + id);
			return null;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007A14 File Offset: 0x00005C14
		public Participant GetSelf()
		{
			Logger.d("AndroidRtmpClient.GetSelf");
			if (!this.CheckConnectedRoom("GetSelf"))
			{
				return null;
			}
			object obj = this.mParticipantListsLock;
			Participant participant;
			lock (obj)
			{
				participant = this.mSelf;
			}
			if (participant == null)
			{
				Logger.e("Call to RtmpGetSelf() can only be made when in a room. Returning null.");
			}
			return participant;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007A8C File Offset: 0x00005C8C
		public void LeaveRoom()
		{
			Logger.d("AndroidRtmpClient.LeaveRoom");
			if (this.mRtmpActive && this.mRoom == null)
			{
				Logger.w("AndroidRtmpClient.LeaveRoom: waiting for room; deferring leave request.");
				this.mLeaveRoomRequested = true;
			}
			else
			{
				this.mClient.CallClientApi("leave room", delegate
				{
					this.Clear("LeaveRoom called");
				}, null);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007AEC File Offset: 0x00005CEC
		public void OnStop()
		{
			if (this.mLaunchedExternalActivity)
			{
				Logger.d("OnStop: EXTERNAL ACTIVITY is pending, so not clearing RTMP.");
			}
			else
			{
				this.Clear("leaving room because game is stopping.");
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007B14 File Offset: 0x00005D14
		public bool IsRoomConnected()
		{
			return this.mRoom != null && this.mDeliveredRoomConnected;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007B2C File Offset: 0x00005D2C
		public void DeclineInvitation(string invitationId)
		{
			Logger.d("AndroidRtmpClient.DeclineInvitation " + invitationId);
			this.mClient.ClearInvitationIfFromNotification(invitationId);
			this.mClient.CallClientApi("rtmp decline invitation", delegate
			{
				this.mClient.GHManager.CallGmsApi("games.Games", "RealTimeMultiplayer", "declineInvitation", new object[]
				{
					invitationId
				});
			}, delegate(bool success)
			{
				if (!success)
				{
					Logger.w("Failed to decline invitation. GoogleApiClient was disconnected");
				}
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007BAC File Offset: 0x00005DAC
		private bool PrepareToCreateRoom(string method, RealTimeMultiplayerListener listener)
		{
			if (this.mRtmpActive)
			{
				Logger.e("Cannot call " + method + " while a real-time game is active.");
				if (listener != null)
				{
					Logger.d("Notifying listener of failure to create room.");
					listener.OnRoomConnected(false);
				}
				return false;
			}
			this.mAccumulatedProgress = 0f;
			this.mLastReportedProgress = 0f;
			this.mRtmpListener = listener;
			this.mRtmpActive = true;
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007C18 File Offset: 0x00005E18
		private bool CheckConnectedRoom(string method)
		{
			if (this.mRoom == null || !this.mDeliveredRoomConnected)
			{
				Logger.e("Method " + method + " called without a connected room. You must create or join a room AND wait until you get the OnRoomConnected(true) callback.");
				return false;
			}
			return true;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007C54 File Offset: 0x00005E54
		private void Clear(string reason)
		{
			Logger.d("RtmpClear: clearing RTMP (reason: " + reason + ").");
			if (this.mRoom != null)
			{
				Logger.d("RtmpClear: Room still active, so leaving room.");
				string text = this.mRoom.Call<string>("getRoomId", new object[0]);
				Logger.d("RtmpClear: room id to leave is " + text);
				this.mClient.GHManager.CallGmsApi("games.Games", "RealTimeMultiplayer", "leave", new object[]
				{
					new NoopProxy("com.google.android.gms.games.multiplayer.realtime.RoomUpdateListener"),
					text
				});
				Logger.d("RtmpClear: left room.");
				this.mRoom = null;
			}
			else
			{
				Logger.d("RtmpClear: no room active.");
			}
			if (this.mDeliveredRoomConnected)
			{
				Logger.d("RtmpClear: looks like we must call the OnLeftRoom() callback.");
				RealTimeMultiplayerListener listener = this.mRtmpListener;
				if (listener != null)
				{
					Logger.d("Calling OnLeftRoom() callback.");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						listener.OnLeftRoom();
					});
				}
			}
			else
			{
				Logger.d("RtmpClear: no need to call OnLeftRoom() callback.");
			}
			this.mLeaveRoomRequested = false;
			this.mDeliveredRoomConnected = false;
			this.mRoom = null;
			this.mConnectedParticipants = null;
			this.mAllParticipants = null;
			this.mSelf = null;
			this.mRtmpListener = null;
			this.mVariant = 0;
			this.mRtmpActive = false;
			this.mAccumulatedProgress = 0f;
			this.mLastReportedProgress = 0f;
			this.mLaunchedExternalActivity = false;
			Logger.d("RtmpClear: RTMP cleared.");
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007DCC File Offset: 0x00005FCC
		private string[] SubtractParticipants(List<Participant> a, List<Participant> b)
		{
			List<string> list = new List<string>();
			if (a != null)
			{
				foreach (Participant participant in a)
				{
					list.Add(participant.ParticipantId);
				}
			}
			if (b != null)
			{
				foreach (Participant participant2 in b)
				{
					if (list.Contains(participant2.ParticipantId))
					{
						list.Remove(participant2.ParticipantId);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007EB4 File Offset: 0x000060B4
		private void UpdateRoom()
		{
			List<AndroidJavaObject> list = new List<AndroidJavaObject>();
			Logger.d("UpdateRoom: Updating our cached data about the room.");
			string str = this.mRoom.Call<string>("getRoomId", new object[0]);
			Logger.d("UpdateRoom: room id: " + str);
			Logger.d("UpdateRoom: querying for my player ID.");
			string text = this.mClient.GHManager.CallGmsApi<string>("games.Games", "Players", "getCurrentPlayerId", new object[0]);
			Logger.d("UpdateRoom: my player ID is: " + text);
			Logger.d("UpdateRoom: querying for my participant ID in the room.");
			string text2 = this.mRoom.Call<string>("getParticipantId", new object[]
			{
				text
			});
			Logger.d("UpdateRoom: my participant ID is: " + text2);
			AndroidJavaObject androidJavaObject = this.mRoom.Call<AndroidJavaObject>("getParticipantIds", new object[0]);
			list.Add(androidJavaObject);
			int num = androidJavaObject.Call<int>("size", new object[0]);
			Logger.d("UpdateRoom: # participants: " + num);
			List<Participant> list2 = new List<Participant>();
			List<Participant> list3 = new List<Participant>();
			this.mSelf = null;
			for (int i = 0; i < num; i++)
			{
				Logger.d("UpdateRoom: querying participant #" + i);
				string text3 = androidJavaObject.Call<string>("get", new object[]
				{
					i
				});
				Logger.d(string.Concat(new object[]
				{
					"UpdateRoom: participant #",
					i,
					" has id: ",
					text3
				}));
				AndroidJavaObject androidJavaObject2 = this.mRoom.Call<AndroidJavaObject>("getParticipant", new object[]
				{
					text3
				});
				list.Add(androidJavaObject2);
				Participant participant = JavaUtil.ConvertParticipant(androidJavaObject2);
				list3.Add(participant);
				if (participant.ParticipantId.Equals(text2))
				{
					Logger.d("Participant is SELF.");
					this.mSelf = participant;
				}
				if (participant.IsConnectedToRoom)
				{
					list2.Add(participant);
				}
			}
			if (this.mSelf == null)
			{
				Logger.e("List of room participants did not include self,  participant id: " + text2 + ", player id: " + text);
				this.mSelf = new Participant("?", text2, Participant.ParticipantStatus.Unknown, new Player("?", text), false);
			}
			list2.Sort();
			list3.Sort();
			object obj = this.mParticipantListsLock;
			string[] array;
			string[] array2;
			lock (obj)
			{
				array = this.SubtractParticipants(list2, this.mConnectedParticipants);
				array2 = this.SubtractParticipants(this.mConnectedParticipants, list2);
				this.mConnectedParticipants = list2;
				this.mAllParticipants = list3;
				Logger.d("UpdateRoom: participant list now has " + this.mConnectedParticipants.Count + " participants.");
			}
			Logger.d("UpdateRoom: cleanup.");
			foreach (AndroidJavaObject androidJavaObject3 in list)
			{
				androidJavaObject3.Dispose();
			}
			Logger.d("UpdateRoom: newly connected participants: " + array.Length);
			Logger.d("UpdateRoom: newly disconnected participants: " + array2.Length);
			if (this.mDeliveredRoomConnected)
			{
				if (array.Length > 0 && this.mRtmpListener != null)
				{
					Logger.d("UpdateRoom: calling OnPeersConnected callback");
					this.mRtmpListener.OnPeersConnected(array);
				}
				if (array2.Length > 0 && this.mRtmpListener != null)
				{
					Logger.d("UpdateRoom: calling OnPeersDisconnected callback");
					this.mRtmpListener.OnPeersDisconnected(array2);
				}
			}
			if (this.mLeaveRoomRequested)
			{
				this.Clear("deferred leave-room request");
			}
			if (!this.mDeliveredRoomConnected)
			{
				this.DeliverRoomSetupProgressUpdate();
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000082A8 File Offset: 0x000064A8
		private void FailRoomSetup(string reason)
		{
			Logger.d("Failing room setup: " + reason);
			RealTimeMultiplayerListener listener = this.mRtmpListener;
			this.Clear("Room setup failed: " + reason);
			if (listener != null)
			{
				Logger.d("Invoking callback OnRoomConnected(false) to signal failure.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					listener.OnRoomConnected(false);
				});
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008310 File Offset: 0x00006510
		private bool CheckRtmpActive(string method)
		{
			if (!this.mRtmpActive)
			{
				Logger.d("Got call to " + method + " with RTMP inactive. Ignoring.");
				return false;
			}
			return true;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008338 File Offset: 0x00006538
		private void OnJoinedRoom(int statusCode, AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnJoinedRoom, status " + statusCode);
			if (!this.CheckRtmpActive("OnJoinedRoom"))
			{
				return;
			}
			this.mRoom = room;
			this.mAccumulatedProgress += 20f;
			if (statusCode != 0)
			{
				this.FailRoomSetup("OnJoinedRoom error code " + statusCode);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000083A0 File Offset: 0x000065A0
		private void OnLeftRoom(int statusCode, AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnLeftRoom, status " + statusCode);
			if (!this.CheckRtmpActive("OnLeftRoom"))
			{
				return;
			}
			this.Clear("Got OnLeftRoom " + statusCode);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000083EC File Offset: 0x000065EC
		private void OnRoomConnected(int statusCode, AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnRoomConnected, status " + statusCode);
			if (!this.CheckRtmpActive("OnRoomConnected"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
			if (statusCode != 0)
			{
				this.FailRoomSetup("OnRoomConnected error code " + statusCode);
			}
			else
			{
				Logger.d("AndroidClient.OnRoomConnected: room setup succeeded!");
				RealTimeMultiplayerListener listener = this.mRtmpListener;
				if (listener != null)
				{
					Logger.d("Invoking callback OnRoomConnected(true) to report success.");
					PlayGamesHelperObject.RunOnGameThread(delegate
					{
						this.mDeliveredRoomConnected = true;
						listener.OnRoomConnected(true);
					});
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008498 File Offset: 0x00006698
		private void OnRoomCreated(int statusCode, AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnRoomCreated, status " + statusCode);
			if (!this.CheckRtmpActive("OnRoomCreated"))
			{
				return;
			}
			this.mRoom = room;
			this.mAccumulatedProgress += 20f;
			if (statusCode != 0)
			{
				this.FailRoomSetup("OnRoomCreated error code " + statusCode);
			}
			this.UpdateRoom();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008508 File Offset: 0x00006708
		private void OnConnectedToRoom(AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnConnectedToRoom");
			if (!this.CheckRtmpActive("OnConnectedToRoom"))
			{
				return;
			}
			this.mAccumulatedProgress += 10f;
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008550 File Offset: 0x00006750
		private void OnDisconnectedFromRoom(AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnDisconnectedFromRoom");
			if (!this.CheckRtmpActive("OnDisconnectedFromRoom"))
			{
				return;
			}
			this.Clear("Got OnDisconnectedFromRoom");
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008584 File Offset: 0x00006784
		private void OnP2PConnected(string participantId)
		{
			Logger.d("AndroidClient.OnP2PConnected: " + participantId);
			if (!this.CheckRtmpActive("OnP2PConnected"))
			{
				return;
			}
			this.UpdateRoom();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000085B0 File Offset: 0x000067B0
		private void OnP2PDisconnected(string participantId)
		{
			Logger.d("AndroidClient.OnP2PDisconnected: " + participantId);
			if (!this.CheckRtmpActive("OnP2PDisconnected"))
			{
				return;
			}
			this.UpdateRoom();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000085DC File Offset: 0x000067DC
		private void OnPeerDeclined(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeerDeclined");
			if (!this.CheckRtmpActive("OnPeerDeclined"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
			if (!this.mDeliveredRoomConnected)
			{
				this.FailRoomSetup("OnPeerDeclined received during setup");
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008628 File Offset: 0x00006828
		private void OnPeerInvitedToRoom(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeerInvitedToRoom");
			if (!this.CheckRtmpActive("OnPeerInvitedToRoom"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008660 File Offset: 0x00006860
		private void OnPeerJoined(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeerJoined");
			if (!this.CheckRtmpActive("OnPeerJoined"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008698 File Offset: 0x00006898
		private void OnPeerLeft(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeerLeft");
			if (!this.CheckRtmpActive("OnPeerLeft"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
			if (!this.mDeliveredRoomConnected)
			{
				this.FailRoomSetup("OnPeerLeft received during setup");
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000086E4 File Offset: 0x000068E4
		private void OnPeersConnected(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeersConnected");
			if (!this.CheckRtmpActive("OnPeersConnected"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000871C File Offset: 0x0000691C
		private void OnPeersDisconnected(AndroidJavaObject room, AndroidJavaObject participantIds)
		{
			Logger.d("AndroidClient.OnPeersDisconnected.");
			if (!this.CheckRtmpActive("OnPeersDisconnected"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008754 File Offset: 0x00006954
		private void OnRoomAutoMatching(AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnRoomAutoMatching");
			if (!this.CheckRtmpActive("OnRoomAutomatching"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000878C File Offset: 0x0000698C
		private void OnRoomConnecting(AndroidJavaObject room)
		{
			Logger.d("AndroidClient.OnRoomConnecting.");
			if (!this.CheckRtmpActive("OnRoomConnecting"))
			{
				return;
			}
			this.mRoom = room;
			this.UpdateRoom();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000087C4 File Offset: 0x000069C4
		private void OnRealTimeMessageReceived(AndroidJavaObject message)
		{
			Logger.d("AndroidClient.OnRealTimeMessageReceived.");
			if (!this.CheckRtmpActive("OnRealTimeMessageReceived"))
			{
				return;
			}
			RealTimeMultiplayerListener listener = this.mRtmpListener;
			if (listener != null)
			{
				byte[] messageData;
				using (AndroidJavaObject androidJavaObject = message.Call<AndroidJavaObject>("getMessageData", new object[0]))
				{
					messageData = JavaUtil.ConvertByteArray(androidJavaObject);
				}
				bool isReliable = message.Call<bool>("isReliable", new object[0]);
				string senderId = message.Call<string>("getSenderParticipantId", new object[0]);
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					listener.OnRealTimeMessageReceived(isReliable, senderId, messageData);
				});
			}
			message.Dispose();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000088A8 File Offset: 0x00006AA8
		private void OnSelectOpponentsResult(bool success, AndroidJavaObject opponents, bool hasAutoMatch, AndroidJavaObject autoMatchCriteria)
		{
			Logger.d("AndroidRtmpClient.OnSelectOpponentsResult, success=" + success);
			if (!this.CheckRtmpActive("OnSelectOpponentsResult"))
			{
				return;
			}
			this.mLaunchedExternalActivity = false;
			if (!success)
			{
				Logger.w("Room setup failed because select-opponents UI failed.");
				this.FailRoomSetup("Select opponents UI failed.");
				return;
			}
			this.mClient.CallClientApi("creating room w/ select-opponents result", delegate
			{
				Logger.d("Creating room via support lib's RtmpUtil.");
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.RtmpUtils");
				@class.CallStatic("create", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					opponents,
					this.mVariant,
					(!hasAutoMatch) ? null : autoMatchCriteria,
					new AndroidRtmpClient.RoomUpdateProxy(this),
					new AndroidRtmpClient.RoomStatusUpdateProxy(this),
					new AndroidRtmpClient.RealTimeMessageReceivedProxy(this)
				});
			}, delegate(bool ok)
			{
				if (!ok)
				{
					this.FailRoomSetup("GoogleApiClient lost connection");
				}
			});
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000894C File Offset: 0x00006B4C
		private void OnInvitationInboxResult(bool success, string invitationId)
		{
			Logger.d(string.Concat(new object[]
			{
				"AndroidRtmpClient.OnInvitationInboxResult, success=",
				success,
				", invitationId=",
				invitationId
			}));
			if (!this.CheckRtmpActive("OnInvitationInboxResult"))
			{
				return;
			}
			this.mLaunchedExternalActivity = false;
			if (!success || invitationId == null || invitationId.Length == 0)
			{
				Logger.w("Failed to setup room because invitation inbox UI failed.");
				this.FailRoomSetup("Invitation inbox UI failed.");
				return;
			}
			this.mClient.ClearInvitationIfFromNotification(invitationId);
			this.mClient.CallClientApi("accept invite from inbox", delegate
			{
				Logger.d("Accepting invite from inbox via support lib.");
				AndroidJavaClass @class = JavaUtil.GetClass("com.google.example.games.pluginsupport.RtmpUtils");
				@class.CallStatic("accept", new object[]
				{
					this.mClient.GHManager.GetApiClient(),
					invitationId,
					new AndroidRtmpClient.RoomUpdateProxy(this),
					new AndroidRtmpClient.RoomStatusUpdateProxy(this),
					new AndroidRtmpClient.RealTimeMessageReceivedProxy(this)
				});
			}, delegate(bool ok)
			{
				if (!ok)
				{
					this.FailRoomSetup("GoogleApiClient lost connection.");
				}
			});
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008A28 File Offset: 0x00006C28
		private void DeliverRoomSetupProgressUpdate()
		{
			Logger.d("AndroidRtmpClient: DeliverRoomSetupProgressUpdate");
			if (!this.mRtmpActive || this.mRoom == null || this.mDeliveredRoomConnected)
			{
				return;
			}
			float progress = this.CalcRoomSetupPercentage();
			if (progress < this.mLastReportedProgress)
			{
				progress = this.mLastReportedProgress;
			}
			else
			{
				this.mLastReportedProgress = progress;
			}
			Logger.d("room setup progress: " + progress + "%");
			if (this.mRtmpListener != null)
			{
				Logger.d("Delivering progress to callback.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mRtmpListener.OnRoomSetupProgress(progress);
				});
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private float CalcRoomSetupPercentage()
		{
			if (!this.mRtmpActive || this.mRoom == null)
			{
				return 0f;
			}
			if (this.mDeliveredRoomConnected)
			{
				return 100f;
			}
			float num = this.mAccumulatedProgress;
			if (num > 50f)
			{
				num = 50f;
			}
			float num2 = 100f - num;
			int num3 = (this.mAllParticipants != null) ? this.mAllParticipants.Count : 0;
			int num4 = (this.mConnectedParticipants != null) ? this.mConnectedParticipants.Count : 0;
			if (num3 == 0)
			{
				return num;
			}
			return num + num2 * ((float)num4 / (float)num3);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008B98 File Offset: 0x00006D98
		internal void OnSignInSucceeded()
		{
		}

		// Token: 0x040000A8 RID: 168
		private AndroidClient mClient;

		// Token: 0x040000A9 RID: 169
		private AndroidJavaObject mRoom;

		// Token: 0x040000AA RID: 170
		private RealTimeMultiplayerListener mRtmpListener;

		// Token: 0x040000AB RID: 171
		private bool mRtmpActive;

		// Token: 0x040000AC RID: 172
		private bool mLaunchedExternalActivity;

		// Token: 0x040000AD RID: 173
		private bool mDeliveredRoomConnected;

		// Token: 0x040000AE RID: 174
		private bool mLeaveRoomRequested;

		// Token: 0x040000AF RID: 175
		private int mVariant;

		// Token: 0x040000B0 RID: 176
		private object mParticipantListsLock = new object();

		// Token: 0x040000B1 RID: 177
		private List<Participant> mConnectedParticipants = new List<Participant>();

		// Token: 0x040000B2 RID: 178
		private List<Participant> mAllParticipants = new List<Participant>();

		// Token: 0x040000B3 RID: 179
		private Participant mSelf;

		// Token: 0x040000B4 RID: 180
		private float mAccumulatedProgress;

		// Token: 0x040000B5 RID: 181
		private float mLastReportedProgress;

		// Token: 0x0200003A RID: 58
		private class RoomUpdateProxy : AndroidJavaProxy
		{
			// Token: 0x0600021A RID: 538 RVA: 0x00008C34 File Offset: 0x00006E34
			internal RoomUpdateProxy(AndroidRtmpClient owner) : base("com.google.android.gms.games.multiplayer.realtime.RoomUpdateListener")
			{
				this.mOwner = owner;
			}

			// Token: 0x0600021B RID: 539 RVA: 0x00008C48 File Offset: 0x00006E48
			public void onJoinedRoom(int statusCode, AndroidJavaObject room)
			{
				this.mOwner.OnJoinedRoom(statusCode, room);
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00008C58 File Offset: 0x00006E58
			public void onLeftRoom(int statusCode, AndroidJavaObject room)
			{
				this.mOwner.OnLeftRoom(statusCode, room);
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00008C68 File Offset: 0x00006E68
			public void onRoomConnected(int statusCode, AndroidJavaObject room)
			{
				this.mOwner.OnRoomConnected(statusCode, room);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00008C78 File Offset: 0x00006E78
			public void onRoomCreated(int statusCode, AndroidJavaObject room)
			{
				this.mOwner.OnRoomCreated(statusCode, room);
			}

			// Token: 0x040000B7 RID: 183
			private AndroidRtmpClient mOwner;
		}

		// Token: 0x0200003B RID: 59
		private class RoomStatusUpdateProxy : AndroidJavaProxy
		{
			// Token: 0x0600021F RID: 543 RVA: 0x00008C88 File Offset: 0x00006E88
			internal RoomStatusUpdateProxy(AndroidRtmpClient owner) : base("com.google.android.gms.games.multiplayer.realtime.RoomStatusUpdateListener")
			{
				this.mOwner = owner;
			}

			// Token: 0x06000220 RID: 544 RVA: 0x00008C9C File Offset: 0x00006E9C
			public void onConnectedToRoom(AndroidJavaObject room)
			{
				this.mOwner.OnConnectedToRoom(room);
			}

			// Token: 0x06000221 RID: 545 RVA: 0x00008CAC File Offset: 0x00006EAC
			public void onDisconnectedFromRoom(AndroidJavaObject room)
			{
				this.mOwner.OnDisconnectedFromRoom(room);
			}

			// Token: 0x06000222 RID: 546 RVA: 0x00008CBC File Offset: 0x00006EBC
			public void onP2PConnected(string participantId)
			{
				this.mOwner.OnP2PConnected(participantId);
			}

			// Token: 0x06000223 RID: 547 RVA: 0x00008CCC File Offset: 0x00006ECC
			public void onP2PDisconnected(string participantId)
			{
				this.mOwner.OnP2PDisconnected(participantId);
			}

			// Token: 0x06000224 RID: 548 RVA: 0x00008CDC File Offset: 0x00006EDC
			public void onPeerDeclined(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeerDeclined(room, participantIds);
			}

			// Token: 0x06000225 RID: 549 RVA: 0x00008CEC File Offset: 0x00006EEC
			public void onPeerInvitedToRoom(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeerInvitedToRoom(room, participantIds);
			}

			// Token: 0x06000226 RID: 550 RVA: 0x00008CFC File Offset: 0x00006EFC
			public void onPeerJoined(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeerJoined(room, participantIds);
			}

			// Token: 0x06000227 RID: 551 RVA: 0x00008D0C File Offset: 0x00006F0C
			public void onPeerLeft(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeerLeft(room, participantIds);
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00008D1C File Offset: 0x00006F1C
			public void onPeersConnected(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeersConnected(room, participantIds);
			}

			// Token: 0x06000229 RID: 553 RVA: 0x00008D2C File Offset: 0x00006F2C
			public void onPeersDisconnected(AndroidJavaObject room, AndroidJavaObject participantIds)
			{
				this.mOwner.OnPeersDisconnected(room, participantIds);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00008D3C File Offset: 0x00006F3C
			public void onRoomAutoMatching(AndroidJavaObject room)
			{
				this.mOwner.OnRoomAutoMatching(room);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00008D4C File Offset: 0x00006F4C
			public void onRoomConnecting(AndroidJavaObject room)
			{
				this.mOwner.OnRoomConnecting(room);
			}

			// Token: 0x040000B8 RID: 184
			private AndroidRtmpClient mOwner;
		}

		// Token: 0x0200003C RID: 60
		private class RealTimeMessageReceivedProxy : AndroidJavaProxy
		{
			// Token: 0x0600022C RID: 556 RVA: 0x00008D5C File Offset: 0x00006F5C
			internal RealTimeMessageReceivedProxy(AndroidRtmpClient owner) : base("com.google.android.gms.games.multiplayer.realtime.RealTimeMessageReceivedListener")
			{
				this.mOwner = owner;
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00008D70 File Offset: 0x00006F70
			public void onRealTimeMessageReceived(AndroidJavaObject message)
			{
				this.mOwner.OnRealTimeMessageReceived(message);
			}

			// Token: 0x040000B9 RID: 185
			private AndroidRtmpClient mOwner;
		}

		// Token: 0x0200003D RID: 61
		private class SelectOpponentsProxy : AndroidJavaProxy
		{
			// Token: 0x0600022E RID: 558 RVA: 0x00008D80 File Offset: 0x00006F80
			internal SelectOpponentsProxy(AndroidRtmpClient owner) : base("com.google.example.games.pluginsupport.SelectOpponentsHelperActivity$Listener")
			{
				this.mOwner = owner;
			}

			// Token: 0x0600022F RID: 559 RVA: 0x00008D94 File Offset: 0x00006F94
			public void onSelectOpponentsResult(bool success, AndroidJavaObject opponents, bool hasAutoMatch, AndroidJavaObject autoMatchCriteria)
			{
				this.mOwner.OnSelectOpponentsResult(success, opponents, hasAutoMatch, autoMatchCriteria);
			}

			// Token: 0x040000BA RID: 186
			private AndroidRtmpClient mOwner;
		}

		// Token: 0x0200003E RID: 62
		private class InvitationInboxProxy : AndroidJavaProxy
		{
			// Token: 0x06000230 RID: 560 RVA: 0x00008DA8 File Offset: 0x00006FA8
			internal InvitationInboxProxy(AndroidRtmpClient owner) : base("com.google.example.games.pluginsupport.InvitationInboxHelperActivity$Listener")
			{
				this.mOwner = owner;
			}

			// Token: 0x06000231 RID: 561 RVA: 0x00008DBC File Offset: 0x00006FBC
			public void onInvitationInboxResult(bool success, string invitationId)
			{
				this.mOwner.OnInvitationInboxResult(success, invitationId);
			}

			// Token: 0x06000232 RID: 562 RVA: 0x00008DCC File Offset: 0x00006FCC
			public void onTurnBasedMatch(AndroidJavaObject match)
			{
				Logger.e("Bug: RTMP proxy got onTurnBasedMatch(). Shouldn't happen. Ignoring.");
			}

			// Token: 0x040000BB RID: 187
			private AndroidRtmpClient mOwner;
		}
	}
}
