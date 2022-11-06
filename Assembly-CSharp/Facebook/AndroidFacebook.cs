using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000002 RID: 2
	internal sealed class AndroidFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		public string KeyHash
		{
			get
			{
				return this.keyHash;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002100 File Offset: 0x00000300
		public override int DialogMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
				this.CallFB("SetLimitEventUsage", value.ToString());
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		private AndroidJavaClass FB
		{
			get
			{
				if (this.fbJava == null)
				{
					this.fbJava = new AndroidJavaClass("com.facebook.unity.FB");
					if (this.fbJava == null)
					{
						throw new MissingReferenceException(string.Format("AndroidFacebook failed to load {0} class", "com.facebook.unity.FB"));
					}
				}
				return this.fbJava;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		private void CallFB(string method, string args)
		{
			this.FB.CallStatic(method, new object[]
			{
				args
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		protected override void OnAwake()
		{
			this.keyHash = string.Empty;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000003A0
		private bool IsErrorResponse(string response)
		{
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021A4 File Offset: 0x000003A4
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			if (string.IsNullOrEmpty(appId))
			{
				throw new ArgumentException("appId cannot be null or empty!");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("appId", appId);
			if (cookie)
			{
				dictionary.Add("cookie", true);
			}
			if (!logging)
			{
				dictionary.Add("logging", false);
			}
			if (!status)
			{
				dictionary.Add("status", false);
			}
			if (xfbml)
			{
				dictionary.Add("xfbml", true);
			}
			if (!string.IsNullOrEmpty(channelUrl))
			{
				dictionary.Add("channelUrl", channelUrl);
			}
			if (!string.IsNullOrEmpty(authResponse))
			{
				dictionary.Add("authResponse", authResponse);
			}
			if (frictionlessRequests)
			{
				dictionary.Add("frictionlessRequests", true);
			}
			string text = Json.Serialize(dictionary);
			this.onInitComplete = onInitComplete;
			this.CallFB("Init", text.ToString());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022A4 File Offset: 0x000004A4
		public void OnInitComplete(string message)
		{
			this.isInitialized = true;
			this.OnLoginComplete(message);
			if (this.onInitComplete != null)
			{
				this.onInitComplete();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022D8 File Offset: 0x000004D8
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			string args = Json.Serialize(new Dictionary<string, object>
			{
				{
					"scope",
					scope
				}
			});
			base.AddAuthDelegate(callback);
			this.CallFB("Login", args);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002314 File Offset: 0x00000514
		public void OnLoginComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
				this.userId = (string)dictionary["user_id"];
				this.accessToken = (string)dictionary["access_token"];
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
			if (dictionary.ContainsKey("key_hash"))
			{
				this.keyHash = (string)dictionary["key_hash"];
			}
			base.OnAuthResponse(new FBResult(message, null));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023C4 File Offset: 0x000005C4
		public void OnGroupCreateComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			string uniqueId = (string)dictionary["callback_id"];
			dictionary.Remove("callback_id");
			base.OnFacebookResponse(uniqueId, new FBResult(Json.Serialize(dictionary), null));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002410 File Offset: 0x00000610
		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("access_token"))
			{
				this.accessToken = (string)dictionary["access_token"];
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary["expiration_timestamp"]));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002470 File Offset: 0x00000670
		public override void Logout()
		{
			this.CallFB("Logout", string.Empty);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002484 File Offset: 0x00000684
		public void OnLogoutComplete(string message)
		{
			this.isLoggedIn = false;
			this.userId = string.Empty;
			this.accessToken = string.Empty;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024A4 File Offset: 0x000006A4
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
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["message"] = message;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			if (actionType != null && !string.IsNullOrEmpty(objectId))
			{
				dictionary["action_type"] = actionType.ToString();
				dictionary["object_id"] = objectId;
			}
			if (to != null)
			{
				dictionary["to"] = string.Join(",", to);
			}
			if (filters != null && filters.Count > 0)
			{
				string text = filters[0] as string;
				if (text != null)
				{
					dictionary["filters"] = text;
				}
			}
			if (maxRecipients != null)
			{
				dictionary["max_recipients"] = maxRecipients.Value;
			}
			if (!string.IsNullOrEmpty(data))
			{
				dictionary["data"] = data;
			}
			if (!string.IsNullOrEmpty(title))
			{
				dictionary["title"] = title;
			}
			this.CallFB("AppRequest", Json.Serialize(dictionary));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002620 File Offset: 0x00000820
		public void OnAppRequestsComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				string uniqueId = (string)dictionary["callback_id"];
				dictionary.Remove("callback_id");
				if (dictionary.Count > 0)
				{
					List<string> list = new List<string>(dictionary.Count - 1);
					foreach (string text in dictionary.Keys)
					{
						if (!text.StartsWith("to"))
						{
							dictionary2[text] = dictionary[text];
						}
						else
						{
							list.Add((string)dictionary[text]);
						}
					}
					dictionary2.Add("to", list);
					dictionary.Clear();
					base.OnFacebookResponse(uniqueId, new FBResult(Json.Serialize(dictionary2), null));
				}
				else
				{
					base.OnFacebookResponse(uniqueId, new FBResult(Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002758 File Offset: 0x00000958
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			if (!string.IsNullOrEmpty(toId))
			{
				dictionary.Add("to", toId);
			}
			if (!string.IsNullOrEmpty(link))
			{
				dictionary.Add("link", link);
			}
			if (!string.IsNullOrEmpty(linkName))
			{
				dictionary.Add("name", linkName);
			}
			if (!string.IsNullOrEmpty(linkCaption))
			{
				dictionary.Add("caption", linkCaption);
			}
			if (!string.IsNullOrEmpty(linkDescription))
			{
				dictionary.Add("description", linkDescription);
			}
			if (!string.IsNullOrEmpty(picture))
			{
				dictionary.Add("picture", picture);
			}
			if (!string.IsNullOrEmpty(mediaSource))
			{
				dictionary.Add("source", mediaSource);
			}
			if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(actionLink))
			{
				dictionary.Add("actions", new Dictionary<string, object>[]
				{
					new Dictionary<string, object>
					{
						{
							"name",
							actionName
						},
						{
							"link",
							actionLink
						}
					}
				});
			}
			if (!string.IsNullOrEmpty(reference))
			{
				dictionary.Add("ref", reference);
			}
			if (properties != null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				foreach (KeyValuePair<string, string[]> keyValuePair in properties)
				{
					if (keyValuePair.Value.Length >= 1)
					{
						if (keyValuePair.Value.Length == 1)
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value[0]);
						}
						else
						{
							Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
							dictionary3.Add("text", keyValuePair.Value[0]);
							dictionary3.Add("href", keyValuePair.Value[1]);
							dictionary2.Add(keyValuePair.Key, dictionary3);
						}
					}
				}
				dictionary.Add("properties", dictionary2);
			}
			this.CallFB("FeedRequest", Json.Serialize(dictionary));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000298C File Offset: 0x00000B8C
		public void OnFeedRequestComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				string uniqueId = (string)dictionary["callback_id"];
				dictionary.Remove("callback_id");
				if (dictionary.Count > 0)
				{
					foreach (string key in dictionary.Keys)
					{
						dictionary2[key] = dictionary[key];
					}
					dictionary.Clear();
					base.OnFacebookResponse(uniqueId, new FBResult(Json.Serialize(dictionary2), null));
				}
				else
				{
					base.OnFacebookResponse(uniqueId, new FBResult(Json.Serialize(dictionary2), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A7C File Offset: 0x00000C7C
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A88 File Offset: 0x00000C88
		public override void GameGroupCreate(string name, string description, string privacy = "CLOSED", FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["name"] = name;
			dictionary["description"] = description;
			dictionary["privacy"] = privacy;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			this.CallFB("GameGroupCreate", Json.Serialize(dictionary));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002AEC File Offset: 0x00000CEC
		public override void GameGroupJoin(string id, FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["id"] = id;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			this.CallFB("GameGroupJoin", Json.Serialize(dictionary));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B34 File Offset: 0x00000D34
		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback != null)
			{
				this.deepLinkDelegate = callback;
				this.CallFB("GetDeepLink", string.Empty);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B54 File Offset: 0x00000D54
		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate != null)
			{
				object empty = string.Empty;
				dictionary.TryGetValue("deep_link", out empty);
				this.deepLinkDelegate(new FBResult(empty.ToString(), null));
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["logEvent"] = logEvent;
			if (valueToSum != null)
			{
				dictionary["valueToSum"] = valueToSum.Value;
			}
			if (parameters != null)
			{
				dictionary["parameters"] = this.ToStringDict(parameters);
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C10 File Offset: 0x00000E10
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["logPurchase"] = logPurchase;
			dictionary["currency"] = (string.IsNullOrEmpty(currency) ? "USD" : currency);
			if (parameters != null)
			{
				dictionary["parameters"] = this.ToStringDict(parameters);
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C80 File Offset: 0x00000E80
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
			dictionary["app_id"] = appId;
			if (callback != null)
			{
				dictionary["callback_id"] = base.AddFacebookDelegate(callback);
			}
			this.CallFB("PublishInstall", Json.Serialize(dictionary));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CCC File Offset: 0x00000ECC
		public void OnPublishInstallComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				base.OnFacebookResponse((string)dictionary["callback_id"], new FBResult(string.Empty, null));
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D18 File Offset: 0x00000F18
		public override void ActivateApp(string appId = null)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
			if (!string.IsNullOrEmpty(appId))
			{
				dictionary["app_id"] = appId;
			}
			this.CallFB("ActivateApp", Json.Serialize(dictionary));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002D54 File Offset: 0x00000F54
		private Dictionary<string, string> ToStringDict(Dictionary<string, object> dict)
		{
			if (dict == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in dict)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value.ToString();
			}
			return dictionary;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002DD8 File Offset: 0x00000FD8
		private DateTime FromTimestamp(int timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds((double)timestamp);
		}

		// Token: 0x04000001 RID: 1
		public const int BrowserDialogMode = 0;

		// Token: 0x04000002 RID: 2
		private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

		// Token: 0x04000003 RID: 3
		private const string CallbackIdKey = "callback_id";

		// Token: 0x04000004 RID: 4
		private string keyHash;

		// Token: 0x04000005 RID: 5
		private FacebookDelegate deepLinkDelegate;

		// Token: 0x04000006 RID: 6
		private AndroidJavaClass fbJava;

		// Token: 0x04000007 RID: 7
		private InitDelegate onInitComplete;
	}
}
