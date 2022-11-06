using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using UnityEngine;

namespace Facebook
{
	// Token: 0x02000005 RID: 5
	internal class EditorFacebook : AbstractFacebook, IFacebook
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002E28 File Offset: 0x00001028
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002E2C File Offset: 0x0000102C
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002E30 File Offset: 0x00001030
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002E38 File Offset: 0x00001038
		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E44 File Offset: 0x00001044
		protected override void OnAwake()
		{
			base.StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded)));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E64 File Offset: 0x00001064
		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			base.StartCoroutine(this.OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E90 File Offset: 0x00001090
		private IEnumerator OnInit(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			while (this.fb == null)
			{
				yield return null;
			}
			this.fb.Init(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate);
			this.isInitialized = true;
			if (onInitComplete != null)
			{
				onInitComplete();
			}
			yield break;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002F48 File Offset: 0x00001148
		private void OnDllLoaded(IFacebook fb)
		{
			this.fb = (AbstractFacebook)fb;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F58 File Offset: 0x00001158
		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F68 File Offset: 0x00001168
		public override void Logout()
		{
			this.isLoggedIn = false;
			this.userId = string.Empty;
			this.accessToken = string.Empty;
			this.fb.UserId = string.Empty;
			this.fb.AccessToken = string.Empty;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002FA8 File Offset: 0x000011A8
		public override void AppRequest(string message, OGActionType actionType, string objectId, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.fb.AppRequest(message, actionType, objectId, to, filters, excludeIds, maxRecipients, data, title, callback);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002FD4 File Offset: 0x000011D4
		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003004 File Offset: 0x00001204
		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003010 File Offset: 0x00001210
		public override void GameGroupCreate(string name, string description, string privacy = "CLOSED", FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook GameGroupCreate Dialog on Editor");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000301C File Offset: 0x0000121C
		public override void GameGroupJoin(string id, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook GameGroupJoin Dialog on Editor");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003028 File Offset: 0x00001228
		public override void GetAuthResponse(FacebookDelegate callback = null)
		{
			this.fb.GetAuthResponse(callback);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003038 File Offset: 0x00001238
		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000303C File Offset: 0x0000123C
		public override void ActivateApp(string appId = null)
		{
			FbDebug.Info("This only needs to be called for iOS or Android.");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003048 File Offset: 0x00001248
		public override void GetDeepLink(FacebookDelegate callback)
		{
			FbDebug.Info("No Deep Linking in the Editor");
			if (callback != null)
			{
				callback(new FBResult("<platform dependent>", null));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000306C File Offset: 0x0000126C
		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003078 File Offset: 0x00001278
		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003084 File Offset: 0x00001284
		public void MockLoginCallback(FBResult result)
		{
			UnityEngine.Object.Destroy(FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew));
			if (result.Error != null)
			{
				this.BadAccessToken(result.Error);
				return;
			}
			try
			{
				List<object> list = (List<object>)Json.Deserialize(result.Text);
				List<string> list2 = new List<string>();
				foreach (object obj in list)
				{
					if (obj is Dictionary<string, object>)
					{
						Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
						if (dictionary.ContainsKey("body"))
						{
							list2.Add((string)dictionary["body"]);
						}
					}
				}
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)Json.Deserialize(list2[0]);
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)Json.Deserialize(list2[1]);
				if (FB.AppId != (string)dictionary3["id"])
				{
					this.BadAccessToken("Access token is not for current app id: " + FB.AppId);
				}
				else
				{
					this.userId = (string)dictionary2["id"];
					this.fb.UserId = this.userId;
					this.fb.AccessToken = this.accessToken;
					this.isLoggedIn = true;
					base.OnAuthResponse(new FBResult(string.Empty, null));
				}
			}
			catch (Exception ex)
			{
				this.BadAccessToken("Could not get data from access token: " + ex.Message);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000324C File Offset: 0x0000144C
		public void MockCancelledLoginCallback()
		{
			base.OnAuthResponse(new FBResult(string.Empty, null));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003260 File Offset: 0x00001460
		private void BadAccessToken(string error)
		{
			FbDebug.Error(error);
			this.userId = string.Empty;
			this.fb.UserId = string.Empty;
			this.accessToken = string.Empty;
			this.fb.AccessToken = string.Empty;
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew);
		}

		// Token: 0x04000008 RID: 8
		private AbstractFacebook fb;

		// Token: 0x04000009 RID: 9
		private FacebookDelegate loginCallback;
	}
}
