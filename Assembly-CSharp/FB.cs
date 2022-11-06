using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Facebook;
using UnityEngine;

// Token: 0x02000008 RID: 8
public sealed class FB : ScriptableObject
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000048 RID: 72 RVA: 0x00003594 File Offset: 0x00001794
	private static IFacebook FacebookImpl
	{
		get
		{
			if (FB.facebook == null)
			{
				throw new NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
			}
			return FB.facebook;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000049 RID: 73 RVA: 0x000035B0 File Offset: 0x000017B0
	public static string AppId
	{
		get
		{
			return FB.appId;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600004A RID: 74 RVA: 0x000035B8 File Offset: 0x000017B8
	public static string UserId
	{
		get
		{
			return (FB.facebook == null) ? string.Empty : FB.facebook.UserId;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600004B RID: 75 RVA: 0x000035D8 File Offset: 0x000017D8
	public static string AccessToken
	{
		get
		{
			return (FB.facebook == null) ? string.Empty : FB.facebook.AccessToken;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600004C RID: 76 RVA: 0x000035F8 File Offset: 0x000017F8
	public static DateTime AccessTokenExpiresAt
	{
		get
		{
			return (FB.facebook == null) ? DateTime.MinValue : FB.facebook.AccessTokenExpiresAt;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600004D RID: 77 RVA: 0x00003618 File Offset: 0x00001818
	public static bool IsLoggedIn
	{
		get
		{
			return FB.facebook != null && FB.facebook.IsLoggedIn;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00003634 File Offset: 0x00001834
	public static bool IsInitialized
	{
		get
		{
			return FB.facebook != null && FB.facebook.IsInitialized;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003650 File Offset: 0x00001850
	public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.Init(onInitComplete, FBSettings.AppId, FBSettings.Cookie, FBSettings.Logging, FBSettings.Status, FBSettings.Xfbml, FBSettings.FrictionlessRequests, onHideUnity, authResponse);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003684 File Offset: 0x00001884
	public static void Init(InitDelegate onInitComplete, string appId, bool cookie = true, bool logging = true, bool status = true, bool xfbml = false, bool frictionlessRequests = true, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.appId = appId;
		FB.cookie = cookie;
		FB.logging = logging;
		FB.status = status;
		FB.xfbml = xfbml;
		FB.frictionlessRequests = frictionlessRequests;
		FB.authResponse = authResponse;
		FB.OnInitComplete = onInitComplete;
		FB.OnHideUnity = onHideUnity;
		if (!FB.isInitCalled)
		{
			FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(typeof(IFacebook));
			if (versionAttributeOfType == null)
			{
				FbDebug.Warn("Cannot find Facebook SDK Version");
			}
			else
			{
				FbDebug.Info(string.Format("Using SDK {0}, Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
			}
			FBComponentFactory.GetComponent<AndroidFacebookLoader>(IfNotExist.AddNew);
			FB.isInitCalled = true;
			return;
		}
		FbDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");
		if (FB.FacebookImpl != null)
		{
			FB.OnDllLoaded();
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003740 File Offset: 0x00001940
	private static void OnDllLoaded()
	{
		FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(FB.FacebookImpl.GetType());
		if (versionAttributeOfType != null)
		{
			FbDebug.Log(string.Format("Finished loading Facebook dll. Version {0} Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
		}
		FB.FacebookImpl.Init(FB.OnInitComplete, FB.appId, FB.cookie, FB.logging, FB.status, FB.xfbml, FBSettings.ChannelUrl, FB.authResponse, FB.frictionlessRequests, FB.OnHideUnity);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000037BC File Offset: 0x000019BC
	public static void Login(string scope = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.Login(scope, callback);
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000037CC File Offset: 0x000019CC
	public static void Logout()
	{
		FB.FacebookImpl.Logout();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x000037D8 File Offset: 0x000019D8
	public static void AppRequest(string message, OGActionType actionType, string objectId, string[] to, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, actionType, objectId, to, null, null, null, data, title, callback);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003804 File Offset: 0x00001A04
	public static void AppRequest(string message, OGActionType actionType, string objectId, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, actionType, objectId, null, filters, excludeIds, maxRecipients, data, title, callback);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000382C File Offset: 0x00001A2C
	public static void AppRequest(string message, string[] to = null, List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, null, null, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003854 File Offset: 0x00001A54
	public static void Feed(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		FB.FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003880 File Offset: 0x00001A80
	public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00003890 File Offset: 0x00001A90
	public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x000038A0 File Offset: 0x00001AA0
	[Obsolete("use FB.ActivateApp()")]
	public static void PublishInstall(FacebookDelegate callback = null)
	{
		FB.FacebookImpl.PublishInstall(FB.AppId, callback);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000038B4 File Offset: 0x00001AB4
	public static void ActivateApp()
	{
		FB.FacebookImpl.ActivateApp(FB.AppId);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000038C8 File Offset: 0x00001AC8
	public static void GetDeepLink(FacebookDelegate callback)
	{
		FB.FacebookImpl.GetDeepLink(callback);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x000038D8 File Offset: 0x00001AD8
	public static void GameGroupCreate(string name, string description, string privacy = "CLOSED", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.GameGroupCreate(name, description, privacy, callback);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000038E8 File Offset: 0x00001AE8
	public static void GameGroupJoin(string id, FacebookDelegate callback = null)
	{
		FB.FacebookImpl.GameGroupJoin(id, callback);
	}

	// Token: 0x04000010 RID: 16
	public static InitDelegate OnInitComplete;

	// Token: 0x04000011 RID: 17
	public static HideUnityDelegate OnHideUnity;

	// Token: 0x04000012 RID: 18
	private static IFacebook facebook;

	// Token: 0x04000013 RID: 19
	private static string authResponse;

	// Token: 0x04000014 RID: 20
	private static bool isInitCalled;

	// Token: 0x04000015 RID: 21
	private static string appId;

	// Token: 0x04000016 RID: 22
	private static bool cookie;

	// Token: 0x04000017 RID: 23
	private static bool logging;

	// Token: 0x04000018 RID: 24
	private static bool status;

	// Token: 0x04000019 RID: 25
	private static bool xfbml;

	// Token: 0x0400001A RID: 26
	private static bool frictionlessRequests;

	// Token: 0x02000009 RID: 9
	public sealed class AppEvents
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003900 File Offset: 0x00001B00
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000391C File Offset: 0x00001B1C
		public static bool LimitEventUsage
		{
			get
			{
				return FB.facebook != null && FB.facebook.LimitEventUsage;
			}
			set
			{
				FB.facebook.LimitEventUsage = value;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000392C File Offset: 0x00001B2C
		public static void LogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000393C File Offset: 0x00001B3C
		public static void LogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
		}
	}

	// Token: 0x0200000A RID: 10
	public sealed class Canvas
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003954 File Offset: 0x00001B54
		public static void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FB.FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000397C File Offset: 0x00001B7C
		public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000398C File Offset: 0x00001B8C
		public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetAspectRatio(width, height, layoutParams);
		}
	}

	// Token: 0x0200000B RID: 11
	public sealed class Android
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000039A0 File Offset: 0x00001BA0
		public static string KeyHash
		{
			get
			{
				AndroidFacebook androidFacebook = FB.facebook as AndroidFacebook;
				return (!(androidFacebook != null)) ? string.Empty : androidFacebook.KeyHash;
			}
		}
	}

	// Token: 0x0200000C RID: 12
	public abstract class RemoteFacebookLoader : MonoBehaviour
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000039E0 File Offset: 0x00001BE0
		public static IEnumerator LoadFacebookClass(string className, FB.RemoteFacebookLoader.LoadedDllCallback callback)
		{
			string url = string.Format(IntegratedPluginCanvasLocation.DllUrl, className);
			WWW www = new WWW(url);
			FbDebug.Log("loading dll: " + url);
			yield return www;
			if (www.error != null)
			{
				FbDebug.Error(www.error);
				if (FB.RemoteFacebookLoader.retryLoadCount < 3)
				{
					FB.RemoteFacebookLoader.retryLoadCount++;
				}
				www.Dispose();
				yield break;
			}
			WWW authTokenWww = new WWW(IntegratedPluginCanvasLocation.KeyUrl);
			yield return authTokenWww;
			if (authTokenWww.error != null)
			{
				FbDebug.Error("Cannot load from " + IntegratedPluginCanvasLocation.KeyUrl + ": " + authTokenWww.error);
				authTokenWww.Dispose();
				yield break;
			}
			Assembly assembly = Security.LoadAndVerifyAssembly(www.bytes, authTokenWww.text);
			if (assembly == null)
			{
				FbDebug.Error("Could not securely load assembly from " + url);
				www.Dispose();
				yield break;
			}
			Type facebookClass = assembly.GetType("Facebook." + className);
			if (facebookClass == null)
			{
				FbDebug.Error(className + " not found in assembly!");
				www.Dispose();
				yield break;
			}
			IFacebook fb = typeof(FBComponentFactory).GetMethod("GetComponent").MakeGenericMethod(new Type[]
			{
				facebookClass
			}).Invoke(null, new object[]
			{
				IfNotExist.AddNew
			}) as IFacebook;
			if (fb == null)
			{
				FbDebug.Error(className + " couldn't be created.");
				www.Dispose();
				yield break;
			}
			callback(fb);
			www.Dispose();
			yield break;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006D RID: 109
		protected abstract string className { get; }

		// Token: 0x0600006E RID: 110 RVA: 0x00003A10 File Offset: 0x00001C10
		private IEnumerator Start()
		{
			IEnumerator loader = FB.RemoteFacebookLoader.LoadFacebookClass(this.className, new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded));
			while (loader.MoveNext())
			{
				object obj = loader.Current;
				yield return obj;
			}
			UnityEngine.Object.Destroy(this);
			yield break;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A2C File Offset: 0x00001C2C
		private void OnDllLoaded(IFacebook fb)
		{
			FB.facebook = fb;
			FB.OnDllLoaded();
		}

		// Token: 0x0400001B RID: 27
		private const string facebookNamespace = "Facebook.";

		// Token: 0x0400001C RID: 28
		private const int maxRetryLoadCount = 3;

		// Token: 0x0400001D RID: 29
		private static int retryLoadCount;

		// Token: 0x02000A60 RID: 2656
		// (Invoke) Token: 0x060047BA RID: 18362
		public delegate void LoadedDllCallback(IFacebook fb);
	}

	// Token: 0x0200000D RID: 13
	public abstract class CompiledFacebookLoader : MonoBehaviour
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000071 RID: 113
		protected abstract IFacebook fb { get; }

		// Token: 0x06000072 RID: 114 RVA: 0x00003A44 File Offset: 0x00001C44
		private void Start()
		{
			FB.facebook = this.fb;
			FB.OnDllLoaded();
			UnityEngine.Object.Destroy(this);
		}
	}
}
