using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames.Android
{
	// Token: 0x02000049 RID: 73
	internal class OnStateResultProxy : AndroidJavaProxy
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000A874 File Offset: 0x00008A74
		internal OnStateResultProxy(AndroidClient androidClient, OnStateLoadedListener listener) : base("com.google.android.gms.common.api.ResultCallback")
		{
			this.mListener = listener;
			this.mAndroidClient = androidClient;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A890 File Offset: 0x00008A90
		private void OnStateConflict(int stateKey, string resolvedVersion, byte[] localData, byte[] serverData)
		{
			Logger.d(string.Concat(new object[]
			{
				"OnStateResultProxy.onStateConflict called, stateKey=",
				stateKey,
				", resolvedVersion=",
				resolvedVersion
			}));
			this.debugLogData("localData", localData);
			this.debugLogData("serverData", serverData);
			if (this.mListener != null)
			{
				Logger.d("OnStateResultProxy.onStateConflict invoking conflict callback.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					byte[] resolvedData = this.mListener.OnStateConflict(stateKey, localData, serverData);
					this.mAndroidClient.ResolveState(stateKey, resolvedVersion, resolvedData, this.mListener);
				});
			}
			else
			{
				Logger.w("No conflict callback specified! Cannot resolve cloud save conflict.");
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A958 File Offset: 0x00008B58
		private void OnStateLoaded(int statusCode, int stateKey, byte[] localData)
		{
			Logger.d(string.Concat(new object[]
			{
				"OnStateResultProxy.onStateLoaded called, status ",
				statusCode,
				", stateKey=",
				stateKey
			}));
			this.debugLogData("localData", localData);
			bool success = false;
			switch (statusCode)
			{
			case 0:
				Logger.d("Status is OK, so success.");
				success = true;
				break;
			default:
				if (statusCode != 2002)
				{
					Logger.e("Cloud load failed with status code " + statusCode);
					success = false;
					localData = null;
				}
				else
				{
					Logger.d("Status is KEY NOT FOUND, which is a success, but with no data.");
					success = true;
					localData = null;
				}
				break;
			case 3:
				Logger.d("Status is STALE DATA, so considering as success.");
				success = true;
				break;
			case 4:
				Logger.d("Status is NO DATA (no network?), so it's a failure.");
				success = false;
				localData = null;
				break;
			}
			if (this.mListener != null)
			{
				Logger.d("OnStateResultProxy.onStateLoaded invoking load callback.");
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					this.mListener.OnStateLoaded(success, stateKey, localData);
				});
			}
			else
			{
				Logger.w("No load callback specified!");
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		private void debugLogData(string tag, byte[] data)
		{
			Logger.d("   " + tag + ": " + Logger.describe(data));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public void onResult(AndroidJavaObject result)
		{
			Logger.d("OnStateResultProxy.onResult, result=" + result);
			int statusCode = JavaUtil.GetStatusCode(result);
			Logger.d("OnStateResultProxy: status code is " + statusCode);
			if (result == null)
			{
				Logger.e("OnStateResultProxy: result is null.");
				return;
			}
			Logger.d("OnstateResultProxy: retrieving result objects...");
			AndroidJavaObject androidJavaObject = JavaUtil.CallNullSafeObjectMethod(result, "getLoadedResult", new object[0]);
			AndroidJavaObject androidJavaObject2 = JavaUtil.CallNullSafeObjectMethod(result, "getConflictResult", new object[0]);
			Logger.d("Got result objects.");
			Logger.d("loadedResult = " + androidJavaObject);
			Logger.d("conflictResult = " + androidJavaObject2);
			if (androidJavaObject2 != null)
			{
				Logger.d("OnStateResultProxy: processing conflict.");
				int stateKey = androidJavaObject2.Call<int>("getStateKey", new object[0]);
				string resolvedVersion = androidJavaObject2.Call<string>("getResolvedVersion", new object[0]);
				byte[] localData = JavaUtil.ConvertByteArray(JavaUtil.CallNullSafeObjectMethod(androidJavaObject2, "getLocalData", new object[0]));
				byte[] serverData = JavaUtil.ConvertByteArray(JavaUtil.CallNullSafeObjectMethod(androidJavaObject2, "getServerData", new object[0]));
				Logger.d("OnStateResultProxy: conflict args parsed, calling.");
				this.OnStateConflict(stateKey, resolvedVersion, localData, serverData);
			}
			else if (androidJavaObject != null)
			{
				Logger.d("OnStateResultProxy: processing normal load.");
				int stateKey2 = androidJavaObject.Call<int>("getStateKey", new object[0]);
				byte[] localData2 = JavaUtil.ConvertByteArray(JavaUtil.CallNullSafeObjectMethod(androidJavaObject, "getLocalData", new object[0]));
				Logger.d("OnStateResultProxy: loaded args parsed, calling.");
				this.OnStateLoaded(statusCode, stateKey2, localData2);
			}
			else
			{
				Logger.e("OnStateResultProxy: both loadedResult and conflictResult are null!");
			}
		}

		// Token: 0x04000114 RID: 276
		private OnStateLoadedListener mListener;

		// Token: 0x04000115 RID: 277
		private AndroidClient mAndroidClient;
	}
}
