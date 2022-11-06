using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A0D RID: 2573
public abstract class SocialPlatform : MonoBehaviour
{
	// Token: 0x06004402 RID: 17410
	public abstract void Initialize(GameObject callbackObject);

	// Token: 0x06004403 RID: 17411
	public abstract void Login(GameObject callbackObject);

	// Token: 0x06004404 RID: 17412
	public abstract void Logout();

	// Token: 0x06004405 RID: 17413
	public abstract void RequestMyProfile(GameObject callbackObject);

	// Token: 0x06004406 RID: 17414
	public abstract void RequestFriendList(GameObject callbackObject);

	// Token: 0x06004407 RID: 17415
	public abstract void SetScore(SocialDefine.ScoreType type, int score, GameObject callbackObject);

	// Token: 0x06004408 RID: 17416
	public abstract void CreateMyGameData(string gameId, GameObject callbackObject);

	// Token: 0x06004409 RID: 17417
	public abstract void RequestGameData(string userId, GameObject callbackObject);

	// Token: 0x0600440A RID: 17418
	public abstract void DeleteGameData(GameObject callbackObject);

	// Token: 0x0600440B RID: 17419
	public abstract void InviteFriend(GameObject callbackObject);

	// Token: 0x0600440C RID: 17420
	public abstract void SendEnergy(SocialUserData userData, GameObject callbackObject);

	// Token: 0x0600440D RID: 17421
	public abstract void ReceiveEnergy(string energyId, GameObject callbackObject);

	// Token: 0x0600440E RID: 17422
	public abstract void Feed(string feedCaption, string feedText, GameObject callbackObject);

	// Token: 0x0600440F RID: 17423
	public abstract void RequestInvitedFriend(GameObject callbackObject);

	// Token: 0x06004410 RID: 17424
	public abstract void RequestPermission(GameObject callbackObject);

	// Token: 0x06004411 RID: 17425
	public abstract void AddPermission(List<SocialInterface.Permission> permissions, GameObject callbackObject);
}
