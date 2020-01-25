using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BL_DepartmentMaster
/// </summary>
public class BL_DepartmentMaster
{
	public BL_DepartmentMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int insert(PL_DepartmentMaster prp)
    {
        DL_DepartmentMaster obj = new DL_DepartmentMaster();
        return obj.insert(prp);
    }
    public virtual int Delete(PL_DepartmentMaster prp)
    {
        DL_DepartmentMaster obj = new DL_DepartmentMaster();
        return obj.Delete(prp);
    }

    public DataTable Bindgrid(PL_DepartmentMaster prp)
    {
        DL_DepartmentMaster obj = new DL_DepartmentMaster();
        return obj.BindGrid(prp);
    }
}