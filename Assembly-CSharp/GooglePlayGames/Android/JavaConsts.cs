using System;

namespace GooglePlayGames.Android
{
	// Token: 0x02000046 RID: 70
	internal class JavaConsts
	{
		// Token: 0x040000DA RID: 218
		public const int GAMEHELPER_CLIENT_ALL = 7;

		// Token: 0x040000DB RID: 219
		public const int STATE_HIDDEN = 2;

		// Token: 0x040000DC RID: 220
		public const int STATE_REVEALED = 1;

		// Token: 0x040000DD RID: 221
		public const int STATE_UNLOCKED = 0;

		// Token: 0x040000DE RID: 222
		public const int TYPE_INCREMENTAL = 1;

		// Token: 0x040000DF RID: 223
		public const int TYPE_STANDARD = 0;

		// Token: 0x040000E0 RID: 224
		public const int STATUS_OK = 0;

		// Token: 0x040000E1 RID: 225
		public const int STATUS_STALE_DATA = 3;

		// Token: 0x040000E2 RID: 226
		public const int STATUS_NO_DATA = 4;

		// Token: 0x040000E3 RID: 227
		public const int STATUS_DEFERRED = 5;

		// Token: 0x040000E4 RID: 228
		public const int STATUS_KEY_NOT_FOUND = 2002;

		// Token: 0x040000E5 RID: 229
		public const int STATUS_CONFLICT = 2000;

		// Token: 0x040000E6 RID: 230
		public const int SDK_VARIANT = 37143;

		// Token: 0x040000E7 RID: 231
		public const string GmsPkg = "com.google.android.gms";

		// Token: 0x040000E8 RID: 232
		public const string ResultCallbackClass = "com.google.android.gms.common.api.ResultCallback";

		// Token: 0x040000E9 RID: 233
		public const string RoomStatusUpdateListenerClass = "com.google.android.gms.games.multiplayer.realtime.RoomStatusUpdateListener";

		// Token: 0x040000EA RID: 234
		public const string RoomUpdateListenerClass = "com.google.android.gms.games.multiplayer.realtime.RoomUpdateListener";

		// Token: 0x040000EB RID: 235
		public const string RealTimeMessageReceivedListenerClass = "com.google.android.gms.games.multiplayer.realtime.RealTimeMessageReceivedListener";

		// Token: 0x040000EC RID: 236
		public const string OnInvitationReceivedListenerClass = "com.google.android.gms.games.multiplayer.OnInvitationReceivedListener";

		// Token: 0x040000ED RID: 237
		public const string ParticipantResultClass = "com.google.android.gms.games.multiplayer.ParticipantResult";

		// Token: 0x040000EE RID: 238
		public const string PluginSupportPkg = "com.google.example.games.pluginsupport";

		// Token: 0x040000EF RID: 239
		public const string SupportRtmpUtilsClass = "com.google.example.games.pluginsupport.RtmpUtils";

		// Token: 0x040000F0 RID: 240
		public const string SupportTbmpUtilsClass = "com.google.example.games.pluginsupport.TbmpUtils";

		// Token: 0x040000F1 RID: 241
		public const string SupportSelectOpponentsHelperActivity = "com.google.example.games.pluginsupport.SelectOpponentsHelperActivity";

		// Token: 0x040000F2 RID: 242
		public const string SupportSelectOpponentsHelperActivityListener = "com.google.example.games.pluginsupport.SelectOpponentsHelperActivity$Listener";

		// Token: 0x040000F3 RID: 243
		public const string SupportInvitationInboxHelperActivity = "com.google.example.games.pluginsupport.InvitationInboxHelperActivity";

		// Token: 0x040000F4 RID: 244
		public const string SupportInvitationInboxHelperActivityListener = "com.google.example.games.pluginsupport.InvitationInboxHelperActivity$Listener";

		// Token: 0x040000F5 RID: 245
		public const string SignInHelperManagerClass = "com.google.example.games.pluginsupport.SignInHelperManager";

		// Token: 0x040000F6 RID: 246
		public const int STATUS_NOT_INVITED_YET = 0;

		// Token: 0x040000F7 RID: 247
		public const int STATUS_INVITED = 1;

		// Token: 0x040000F8 RID: 248
		public const int STATUS_JOINED = 2;

		// Token: 0x040000F9 RID: 249
		public const int STATUS_DECLINED = 3;

		// Token: 0x040000FA RID: 250
		public const int STATUS_LEFT = 4;

		// Token: 0x040000FB RID: 251
		public const int STATUS_FINISHED = 5;

		// Token: 0x040000FC RID: 252
		public const int STATUS_UNRESPONSIVE = 6;

		// Token: 0x040000FD RID: 253
		public const int INVITATION_TYPE_REAL_TIME = 0;

		// Token: 0x040000FE RID: 254
		public const int INVITATION_TYPE_TURN_BASED = 1;

		// Token: 0x040000FF RID: 255
		public const int MATCH_STATUS_AUTO_MATCHING = 0;

		// Token: 0x04000100 RID: 256
		public const int MATCH_STATUS_ACTIVE = 1;

		// Token: 0x04000101 RID: 257
		public const int MATCH_STATUS_COMPLETE = 2;

		// Token: 0x04000102 RID: 258
		public const int MATCH_STATUS_EXPIRED = 3;

		// Token: 0x04000103 RID: 259
		public const int MATCH_STATUS_CANCELED = 4;

		// Token: 0x04000104 RID: 260
		public const int MATCH_TURN_STATUS_INVITED = 0;

		// Token: 0x04000105 RID: 261
		public const int MATCH_TURN_STATUS_MY_TURN = 1;

		// Token: 0x04000106 RID: 262
		public const int MATCH_TURN_STATUS_THEIR_TURN = 2;

		// Token: 0x04000107 RID: 263
		public const int MATCH_TURN_STATUS_COMPLETE = 3;

		// Token: 0x04000108 RID: 264
		public const int MATCH_VARIANT_ANY = -1;

		// Token: 0x04000109 RID: 265
		public const int MATCH_RESULT_UNINITIALIZED = -1;

		// Token: 0x0400010A RID: 266
		public const int PLACING_UNINITIALIZED = -1;

		// Token: 0x0400010B RID: 267
		public const int MATCH_RESULT_WIN = 0;

		// Token: 0x0400010C RID: 268
		public const int MATCH_RESULT_LOSS = 1;

		// Token: 0x0400010D RID: 269
		public const int MATCH_RESULT_TIE = 2;

		// Token: 0x0400010E RID: 270
		public const int MATCH_RESULT_NONE = 3;

		// Token: 0x0400010F RID: 271
		public const int MATCH_RESULT_DISCONNECT = 4;

		// Token: 0x04000110 RID: 272
		public const int MATCH_RESULT_DISAGREED = 5;
	}
}
