using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class EditorFacebookAccessToken : MonoBehaviour
{
	// Token: 0x06000041 RID: 65 RVA: 0x000032D0 File Offset: 0x000014D0
	private IEnumerator Start()
	{
		if (EditorFacebookAccessToken.fbSkin != null)
		{
			yield break;
		}
		string downloadUrl = IntegratedPluginCanvasLocation.FbSkinUrl;
		WWW www = new WWW(downloadUrl);
		yield return www;
		if (www.error != null)
		{
			FbDebug.Error("Could not find the Facebook Skin: " + www.error);
			yield break;
		}
		EditorFacebookAccessToken.fbSkin = (www.assetBundle.mainAsset as GUISkin);
		www.assetBundle.Unload(false);
		yield break;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000032E4 File Offset: 0x000014E4
	private void OnGUI()
	{
		float top = (float)(Screen.height / 2) - this.windowHeight / 2f;
		float left = (float)(Screen.width / 2) - 296f;
		if (EditorFacebookAccessToken.fbSkin != null)
		{
			GUI.skin = EditorFacebookAccessToken.fbSkin;
			this.greyButton = EditorFacebookAccessToken.fbSkin.GetStyle("greyButton");
		}
		else
		{
			this.greyButton = GUI.skin.button;
		}
		GUI.ModalWindow(this.GetHashCode(), new Rect(left, top, 592f, this.windowHeight), new GUI.WindowFunction(this.OnGUIDialog), "Unity Editor Facebook Login");
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000338C File Offset: 0x0000158C
	private void OnGUIDialog(int windowId)
	{
		GUI.enabled = !this.isLoggingIn;
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Space(10f);
		GUILayout.Label("User Access Token:", new GUILayoutOption[0]);
		GUILayout.EndVertical();
		this.accessToken = GUILayout.TextField(this.accessToken, GUI.skin.textArea, new GUILayoutOption[]
		{
			GUILayout.MinWidth(400f)
		});
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("Find Access Token", new GUILayoutOption[0]))
		{
			Application.OpenURL(string.Format("https://developers.facebook.com/tools/accesstoken/?app_id={0}", FB.AppId));
		}
		GUILayout.FlexibleSpace();
		GUIContent content = new GUIContent("Login");
		Rect rect = GUILayoutUtility.GetRect(content, GUI.skin.button);
		if (GUI.Button(rect, content))
		{
			EditorFacebook component = FBComponentFactory.GetComponent<EditorFacebook>(IfNotExist.AddNew);
			component.AccessToken = this.accessToken;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["batch"] = "[{\"method\":\"GET\", \"relative_url\":\"me?fields=id\"},{\"method\":\"GET\", \"relative_url\":\"app?fields=id\"}]";
			dictionary["method"] = "POST";
			dictionary["access_token"] = this.accessToken;
			FB.API("/", HttpMethod.GET, new FacebookDelegate(component.MockLoginCallback), dictionary);
			this.isLoggingIn = true;
		}
		GUI.enabled = true;
		GUIContent content2 = new GUIContent("Cancel");
		Rect rect2 = GUILayoutUtility.GetRect(content2, this.greyButton);
		if (GUI.Button(rect2, content2, this.greyButton))
		{
			FBComponentFactory.GetComponent<EditorFacebook>(IfNotExist.AddNew).MockCancelledLoginCallback();
			UnityEngine.Object.Destroy(this);
		}
		GUILayout.EndHorizontal();
		if (Event.current.type == EventType.Repaint)
		{
			this.windowHeight = rect2.y + rect2.height + (float)GUI.skin.window.padding.bottom;
		}
	}

	// Token: 0x0400000A RID: 10
	private const float windowWidth = 592f;

	// Token: 0x0400000B RID: 11
	private float windowHeight = 200f;

	// Token: 0x0400000C RID: 12
	private string accessToken = string.Empty;

	// Token: 0x0400000D RID: 13
	private bool isLoggingIn;

	// Token: 0x0400000E RID: 14
	private static GUISkin fbSkin;

	// Token: 0x0400000F RID: 15
	private GUIStyle greyButton;
}
