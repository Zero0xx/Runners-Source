using System;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000047 RID: 71
	internal class JavaUtil
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000A21C File Offset: 0x0000841C
		public static AndroidJavaClass GetGmsClass(string className)
		{
			return JavaUtil.GetClass("com.google.android.gms." + className);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A230 File Offset: 0x00008430
		public static AndroidJavaClass GetClass(string className)
		{
			if (JavaUtil.mClassDict.ContainsKey(className))
			{
				return JavaUtil.mClassDict[className];
			}
			AndroidJavaClass result;
			try
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass(className);
				JavaUtil.mClassDict[className] = androidJavaClass;
				result = androidJavaClass;
			}
			catch (Exception ex)
			{
				Logger.e("JavaUtil failed to load Java class: " + className);
				throw ex;
			}
			return result;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public static AndroidJavaObject GetGmsField(string className, string fieldName)
		{
			string key = className + "/" + fieldName;
			if (JavaUtil.mFieldDict.ContainsKey(key))
			{
				return JavaUtil.mFieldDict[key];
			}
			AndroidJavaClass gmsClass = JavaUtil.GetGmsClass(className);
			AndroidJavaObject @static = gmsClass.GetStatic<AndroidJavaObject>(fieldName);
			JavaUtil.mFieldDict[key] = @static;
			return @static;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A304 File Offset: 0x00008504
		public static int GetStatusCode(AndroidJavaObject result)
		{
			if (result == null)
			{
				return -1;
			}
			AndroidJavaObject androidJavaObject = result.Call<AndroidJavaObject>("getStatus", new object[0]);
			return androidJavaObject.Call<int>("getStatusCode", new object[0]);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A33C File Offset: 0x0000853C
		public static AndroidJavaObject CallNullSafeObjectMethod(AndroidJavaObject target, string methodName, params object[] args)
		{
			AndroidJavaObject result;
			try
			{
				result = target.Call<AndroidJavaObject>(methodName, args);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("null"))
				{
					result = null;
				}
				else
				{
					Logger.w("CallObjectMethod exception: " + ex);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public static byte[] ConvertByteArray(AndroidJavaObject byteArrayObj)
		{
			global::Debug.Log("ConvertByteArray.");
			if (byteArrayObj == null)
			{
				return null;
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("java.lang.reflect.Array");
			global::Debug.Log("Calling java.lang.reflect.Array.getLength.");
			int num = androidJavaClass.CallStatic<int>("getLength", new object[]
			{
				byteArrayObj
			});
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = androidJavaClass.CallStatic<byte>("getByte", new object[]
				{
					byteArrayObj,
					i
				});
			}
			return array;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A43C File Offset: 0x0000863C
		public static int GetAndroidParticipantResult(MatchOutcome.ParticipantResult result)
		{
			switch (result)
			{
			case MatchOutcome.ParticipantResult.None:
				return 3;
			case MatchOutcome.ParticipantResult.Win:
				return 0;
			case MatchOutcome.ParticipantResult.Loss:
				return 1;
			case MatchOutcome.ParticipantResult.Tie:
				return 2;
			default:
				return -1;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A470 File Offset: 0x00008670
		public static TurnBasedMatch.MatchStatus ConvertMatchStatus(int code)
		{
			switch (code)
			{
			case 0:
				return TurnBasedMatch.MatchStatus.AutoMatching;
			case 1:
				return TurnBasedMatch.MatchStatus.Active;
			case 2:
				return TurnBasedMatch.MatchStatus.Complete;
			case 3:
				return TurnBasedMatch.MatchStatus.Expired;
			case 4:
				return TurnBasedMatch.MatchStatus.Cancelled;
			default:
				Logger.e("Unknown match status code: " + code);
				return TurnBasedMatch.MatchStatus.Unknown;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public static TurnBasedMatch.MatchTurnStatus ConvertTurnStatus(int code)
		{
			switch (code)
			{
			case 0:
				return TurnBasedMatch.MatchTurnStatus.Invited;
			case 1:
				return TurnBasedMatch.MatchTurnStatus.MyTurn;
			case 2:
				return TurnBasedMatch.MatchTurnStatus.TheirTurn;
			case 3:
				return TurnBasedMatch.MatchTurnStatus.Complete;
			default:
				Logger.e("Unknown match turn status: " + code);
				return TurnBasedMatch.MatchTurnStatus.Unknown;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A508 File Offset: 0x00008708
		public static Participant ConvertParticipant(AndroidJavaObject participant)
		{
			string displayName = participant.Call<string>("getDisplayName", new object[0]);
			string participantId = participant.Call<string>("getParticipantId", new object[0]);
			Player player = null;
			bool connectedToRoom = participant.Call<bool>("isConnectedToRoom", new object[0]);
			Participant.ParticipantStatus status;
			switch (participant.Call<int>("getStatus", new object[0]))
			{
			case 0:
				status = Participant.ParticipantStatus.NotInvitedYet;
				break;
			case 1:
				status = Participant.ParticipantStatus.Invited;
				break;
			case 2:
				status = Participant.ParticipantStatus.Joined;
				break;
			case 3:
				status = Participant.ParticipantStatus.Declined;
				break;
			case 4:
				status = Participant.ParticipantStatus.Left;
				break;
			case 5:
				status = Participant.ParticipantStatus.Finished;
				break;
			case 6:
				status = Participant.ParticipantStatus.Unresponsive;
				break;
			default:
				status = Participant.ParticipantStatus.Unknown;
				break;
			}
			AndroidJavaObject androidJavaObject = JavaUtil.CallNullSafeObjectMethod(participant, "getPlayer", new object[0]);
			if (androidJavaObject != null)
			{
				player = new Player(androidJavaObject.Call<string>("getDisplayName", new object[0]), androidJavaObject.Call<string>("getPlayerId", new object[0]));
				androidJavaObject.Dispose();
			}
			return new Participant(displayName, participantId, status, player, connectedToRoom);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A620 File Offset: 0x00008820
		public static TurnBasedMatch ConvertMatch(string playerId, AndroidJavaObject matchObj)
		{
			List<AndroidJavaObject> list = new List<AndroidJavaObject>();
			Logger.d("AndroidTbmpClient.ConvertMatch, playerId=" + playerId);
			List<Participant> list2 = new List<Participant>();
			string matchId = matchObj.Call<string>("getMatchId", new object[0]);
			AndroidJavaObject androidJavaObject = JavaUtil.CallNullSafeObjectMethod(matchObj, "getData", new object[0]);
			list.Add(androidJavaObject);
			byte[] data = JavaUtil.ConvertByteArray(androidJavaObject);
			bool canRematch = matchObj.Call<bool>("canRematch", new object[0]);
			int availableAutomatchSlots = matchObj.Call<int>("getAvailableAutoMatchSlots", new object[0]);
			string selfParticipantId = matchObj.Call<string>("getParticipantId", new object[]
			{
				playerId
			});
			AndroidJavaObject androidJavaObject2 = matchObj.Call<AndroidJavaObject>("getParticipantIds", new object[0]);
			list.Add(androidJavaObject2);
			int num = androidJavaObject2.Call<int>("size", new object[0]);
			for (int i = 0; i < num; i++)
			{
				string text = androidJavaObject2.Call<string>("get", new object[]
				{
					i
				});
				AndroidJavaObject androidJavaObject3 = matchObj.Call<AndroidJavaObject>("getParticipant", new object[]
				{
					text
				});
				list.Add(androidJavaObject3);
				Participant item = JavaUtil.ConvertParticipant(androidJavaObject3);
				list2.Add(item);
			}
			string pendingParticipantId = matchObj.Call<string>("getPendingParticipantId", new object[0]);
			TurnBasedMatch.MatchTurnStatus turnStatus = JavaUtil.ConvertTurnStatus(matchObj.Call<int>("getTurnStatus", new object[0]));
			TurnBasedMatch.MatchStatus matchStatus = JavaUtil.ConvertMatchStatus(matchObj.Call<int>("getStatus", new object[0]));
			int variant = matchObj.Call<int>("getVariant", new object[0]);
			foreach (AndroidJavaObject androidJavaObject4 in list)
			{
				if (androidJavaObject4 != null)
				{
					androidJavaObject4.Dispose();
				}
			}
			list2.Sort();
			return new TurnBasedMatch(matchId, data, canRematch, selfParticipantId, list2, availableAutomatchSlots, pendingParticipantId, turnStatus, matchStatus, variant);
		}

		// Token: 0x04000111 RID: 273
		private static Dictionary<string, AndroidJavaClass> mClassDict = new Dictionary<string, AndroidJavaClass>();

		// Token: 0x04000112 RID: 274
		private static Dictionary<string, AndroidJavaObject> mFieldDict = new Dictionary<string, AndroidJavaObject>();
	}
}
