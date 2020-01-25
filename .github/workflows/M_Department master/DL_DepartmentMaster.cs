using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DL_DepartmentMaster
/// </summary>
public class DL_DepartmentMaster
{
	public DL_DepartmentMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int insert(PL_DepartmentMaster prp)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.cmd.CommandText = "[USP_M_DEPARTMENT_MASTER]";
        Mycon.cmd.CommandType = CommandType.StoredProcedure;        
        Mycon.cmd.Parameters.AddWithValue("@DepartmentCode", prp.DepartmentCode);
        Mycon.cmd.Parameters.AddWithValue("@sptype", prp.sptype);
        Mycon.cmd.Parameters.AddWithValue("@DepartmentName", prp.DepartmentName);
        Mycon.cmd.Parameters.AddWithValue("@PayBillSignAuth", prp.PayBillSighAuth);
        Mycon.cmd.Parameters.AddWithValue("@PayBillSighAuthUpload", prp.PayBillSighAuthUpload);
        Mycon.cmd.Parameters.AddWithValue("@CreatedBy", prp.CreatedBy);

        SqlParameter p1 = new SqlParameter("@TableID", SqlDbType.VarChar, 10);
        p1.Direction = ParameterDirection.InputOutput;
        p1.Value = prp.TableID;
        Mycon.cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@msg", SqlDbType.VarChar, 200);
        p2.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p2);
        try
        {
            Mycon.Open();
            int RetValue = Mycon.cmd.ExecuteNonQuery();
            Mycon.Close();
            prp.msg = Mycon.cmd.Parameters["@msg"].Value.ToString();

             return RetValue;
        }
        catch (Exception ex)
        {
            prp.msg = ex.Message.ToString();

            return 0;
        }
        finally
        {
            if (Mycon.Mycon.State == ConnectionState.Open)
            {
                Mycon.Close();
            }
            Mycon.cmd.Dispose();
        }
    }

    public virtual int Delete(PL_DepartmentMaster prp)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.cmd.CommandText = "[USP_M_DEPARTMENT_MASTER]";
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.Parameters.AddWithValue("@spType", prp.sptype);
        Mycon.cmd.Parameters.AddWithValue("@TableID", prp.TableID);
        SqlParameter p2 = new SqlParameter("@msg", SqlDbType.VarChar, 200);
        p2.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p2);
        try
        {
            Mycon.Open();
            int retvalue = Mycon.cmd.ExecuteNonQuery();
            Mycon.Close();
            prp.msg = Mycon.cmd.Parameters["@msg"].Value.ToString();
            return retvalue;
        }
        catch (Exception ex)
        {
            prp.msg = ex.Message.ToString();
            return 0;
        }
        finally
        {
            if (Mycon.Mycon.State == ConnectionState.Open)
            {
                Mycon.Close();
            }
            Mycon.cmd.Dispose();
        }
    }

    public DataTable BindGrid(PL_DepartmentMaster prp)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandText = "USP_M_DEPARTMENT_MASTER";
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.Parameters.AddWithValue("@spType", prp.sptype);
        Mycon.cmd.Parameters.AddWithValue("@TableID", prp.TableID);       

        SqlParameter p1 = new SqlParameter("@msg", SqlDbType.VarChar, 200);
        p1.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p1);
        SqlDataReader dr = Mycon.cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        Mycon.Close();
        return dt;
    }
}