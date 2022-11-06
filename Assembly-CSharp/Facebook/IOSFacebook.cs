using System;
using System.Collections.Generic;
using Facebook.MiniJSON;

namespace Facebook
{
	// Token: 0x02000016 RID: 22
	internal class IOSFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00003EAC File Offset: 0x000020AC
		private void iosInit(string appId, bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003EB0 File Offset: 0x000020B0
		private void iosLogin(string scope)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003EB4 File Offset: 0x000020B4
		private void iosLogout()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003EB8 File Offset: 0x000020B8
		private void iosSetShareDialogMode(int mode)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003EBC File Offset: 0x000020BC
		private void iosFeedRequest(int requestId, string toId, string link, string linkName, string linkCaption, string linkDescription, string picture, string mediaSource, string actionName, string actionLink, string reference)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003EC0 File Offset: 0x000020C0
		private void iosAppRequest(int requestId, string message, string actionType, string objectId, string[] to = null, int toLength = 0, string filters = "", string[] excludeIds = null, int excludeIdsLength = 0, bool hasMaxRecipients = false, int maxRecipients = 0, string data = "", string title = "")
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003EC4 File Offset: 0x000020C4
		private void iosCreateGameGroup(int requestId, string name, string description, string privacy)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003EC8 File Offset: 0x000020C8
		private void iosJoinGameGroup(int requestId, string id)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003ECC File Offset: 0x000020CC
		private void iosFBSettingsPublishInstall(int requestId, string appId)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003ED0 File Offset: 0x000020D0
		private void iosFBSettingsActivateApp(string appId)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003ED4 File Offset: 0x000020D4
		private void iosFBAppEventsLogEvent(string logEvent, double valueToSum, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003ED8 File Offset: 0x000020D8
		private void iosFBAppEventsLogPurchase(double logPurchase, string currency, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003EDC File Offset: 0x000020DC
		private void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003EE0 File Offset: 0x000020E0
		private void iosGetDeepLink()
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003EE4 File Offset: 0x000020E4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003EEC File Offset: 0x000020EC
		public override int DialogMode
		{
			get
			{
				return this.dialogMode;
			}
			set
			{
				this.dialogMode = value;
				this.iosSetShareDialogMode(this.dialogMode);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003F04 File Offset: 0x00002104
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003F0C File Offset: 0x0000210C
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
				this.iosFBAppEventsSetLimitEventUsage(value);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F1C File Offset: 0x0000211C
		protected override void OnAwake()
		{
			this.accessToken = "NOT_USED_ON_IOS_FACEBOOK";
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F2C File Offset: 0x0000212C
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			this.iosInit(appId, cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
			this.externalInitDelegate = onInitComplete;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003F54 File Offset: 0x00002154
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			this.iosLogin(scope);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003F64 File Offset: 0x00002164
		public override void Logout()
		{
			this.iosLogout();
			this.isLoggedIn = false;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003F74 File Offset: 0x00002174
		public override void AppRequest(string message, OGActionType actionType, string objectId, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentNullException("message", "message cannot be null or empty!");
			}
			if (actionType != null && string.IsNullOrEmpty(objectId))
			{
				throw new ArgumentNullException("objectId", "You cannot provide an actionType without an objectId");
			}
			if (actionType == null && !string.IsNullOrEmpty(objectId))
			{
				throw new ArgumentNullException("actionType", "You cannot provide an objectId without an actionType");
			}
			string text = null;
			if (filters != null && filters.Count > 0)
			{
				text = (filters[0] as string);
			}
			this.iosAppRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), message, (actionType == null) ? null : actionType.ToString(), objectId, to, (to == null) ? 0 : to.Length, (text == null) ? string.Empty : text, excludeIds, (excludeIds == null) ? 0 : excludeIds.Length, maxRecipients != null, (maxRecipients == null) ? 0 : maxRecipients.Value, data, title);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004084 File Offset: 0x00002284
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.iosFeedRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040B8 File Offset: 0x000022B8
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000040C4 File Offset: 0x000022C4
		public override void GameGroupCreate(string name, string description, string privacy = "CLOSED", FacebookDelegate callback = null)
		{
			this.iosCreateGameGroup(Convert.ToInt32(base.AddFacebookDelegate(callback)), name, description, privacy);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040E8 File Offset: 0x000022E8
		public override void GameGroupJoin(string id, FacebookDelegate callback = null)
		{
			this.iosJoinGameGroup(Convert.ToInt32(base.AddFacebookDelegate(callback)), id);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004100 File Offset: 0x00002300
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback == null)
			{
				return;
			}
			this.deepLinkDelegate = callback;
			this.iosGetDeepLink();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004118 File Offset: 0x00002318
		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate == null)
			{
				return;
			}
			object empty = string.Empty;
			dictionary.TryGetValue("deep_link", out empty);
			this.deepLinkDelegate(new FBResult(empty.ToString(), null));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004168 File Offset: 0x00002368
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (valueToSum != null)
			{
				this.iosFBAppEventsLogEvent(logEvent, (double)valueToSum.Value, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
			}
			else
			{
				this.iosFBAppEventsLogEvent(logEvent, 0.0, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041D4 File Offset: 0x000023D4
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (string.IsNullOrEmpty(currency))
			{
				currency = "USD";
			}
			this.iosFBAppEventsLogPurchase((double)logPurchase, currency, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004218 File Offset: 0x00002418
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			this.iosFBSettingsPublishInstall(Convert.ToInt32(base.AddFacebookDelegate(callback)), appId);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004230 File Offset: 0x00002430
		public override void ActivateApp(string appId = null)
		{
			this.iosFBSettingsActivateApp(appId);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000423C File Offset: 0x0000243C
		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, object> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.Count > 0)
			{
				nativeDict.keys = new string[dict.Count];
				nativeDict.vals = new string[dict.Count];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, object> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.Key;
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.Value.ToString();
					nativeDict.numEntries++;
				}
			}
			return nativeDict;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004314 File Offset: 0x00002514
		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, string> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.Count > 0)
			{
				nativeDict.keys = new string[dict.Count];
				nativeDict.vals = new string[dict.Count];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.Key;
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.Value;
					nativeDict.numEntries++;
				}
			}
			return nativeDict;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000043E8 File Offset: 0x000025E8
		private void OnInitComplete(string msg)
		{
			this.isInitialized = true;
			if (!string.IsNullOrEmpty(msg))
			{
				this.OnLogin(msg);
			}
			this.externalInitDelegate();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000441C File Offset: 0x0000261C
		public void OnLogin(string msg)
		{
			if (string.IsNullOrEmpty(msg))
			{
				base.OnAuthResponse(new FBResult("{\"cancelled\":true}", null));
				return;
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(msg);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
			}
			this.ParseLoginDict(dictionary);
			base.OnAuthResponse(new FBResult(msg, null));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004480 File Offset: 0x00002680
		public void ParseLoginDict(Dictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("user_id"))
			{
				this.userId = (string)parameters["user_id"];
			}
			if (parameters.ContainsKey("access_token"))
			{
				this.accessToken = (string)parameters["access_token"];
			}
			if (parameters.ContainsKey("expiration_timestamp"))
			{
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)parameters["expiration_timestamp"]));
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000450C File Offset: 0x0000270C
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> parameters = (Dictionary<string, object>)Json.Deserialize(message);
			this.ParseLoginDict(parameters);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000452C File Offset: 0x0000272C
		private DateTime FromTimestamp(int timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds((double)timestamp);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004554 File Offset: 0x00002754
		public void OnLogout(string msg)
		{
			this.isLoggedIn = false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004560 File Offset: 0x00002760
		public void OnRequestComplete(string msg)
		{
			int num = msg.IndexOf(":");
			if (num <= 0)
			{
				FbDebug.Error("Malformed callback from ios.  I expected the form id:message but couldn't find either the ':' character or the id.");
				FbDebug.Error("Here's the message that errored: " + msg);
				return;
			}
			string text = msg.Substring(0, num);
			string text2 = msg.Substring(num + 1);
			FbDebug.Info("id:" + text + " msg:" + text2);
			base.OnFacebookResponse(text, new FBResult(text2, null));
		}

		// Token: 0x0400002E RID: 46
		private const string CancelledResponse = "{\"cancelled\":true}";

		// Token: 0x0400002F RID: 47
		private int dialogMode = 1;

		// Token: 0x04000030 RID: 48
		private InitDelegate externalInitDelegate;

		// Token: 0x04000031 RID: 49
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x02000017 RID: 23
		private class NativeDict
		{
			// Token: 0x060000CE RID: 206 RVA: 0x000045D4 File Offset: 0x000027D4
			public NativeDict()
			{
				this.numEntries = 0;
				this.keys = null;
				this.vals = null;
			}

			// Token: 0x04000032 RID: 50
			public int numEntries;

			// Token: 0x04000033 RID: 51
			public string[] keys;

			// Token: 0x04000034 RID: 52
			public string[] vals;
		}

		// Token: 0x02000018 RID: 24
		public enum FBInsightsFlushBehavior
		{
			// Token: 0x04000036 RID: 54
			FBInsightsFlushBehaviorAuto,
			// Token: 0x04000037 RID: 55
			FBInsightsFlushBehaviorExplicitOnly
		}
	}
}
