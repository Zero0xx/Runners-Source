using System;
using DataTable;

// Token: 0x02000684 RID: 1668
public class ErrorHandleMaintenanceUtil
{
	// Token: 0x06002C79 RID: 11385 RVA: 0x0010CA90 File Offset: 0x0010AC90
	public static string GetMaintenancePageURL()
	{
		return InformationDataTable.GetUrl(InformationDataTable.Type.MAINTENANCE_PAGE);
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x0010CAA8 File Offset: 0x0010ACA8
	public static bool IsExistMaintenancePage()
	{
		string maintenancePageURL = ErrorHandleMaintenanceUtil.GetMaintenancePageURL();
		return !string.IsNullOrEmpty(maintenancePageURL);
	}
}
