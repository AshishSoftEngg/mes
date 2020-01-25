using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_DepartmentMaster
/// </summary>
public class PL_DepartmentMaster
{
	public PL_DepartmentMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public String DepartmentCode
    {
        set;
        get;
    }
    public String DepartmentName
    {
        set;
        get;
    }
    public String CreatedBy
    {
        set;
        get;
    }

    
    public String msg
    {
        set;
        get;
    }
    public int sptype
    {
        set;
        get;
    }
    public int TableID
    {
        set;
        get;
    }
    public object PayBillSighAuth { get;  set; }
    public string PayBillSighAuthUpload { get; set; }
}