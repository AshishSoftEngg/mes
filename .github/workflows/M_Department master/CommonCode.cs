using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// Summary description for CommonCode
/// </summary>
public class CommonCode
{
    public CommonCode()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet getBalanceSheet(string FinYear)
    {
        MyConnection Mycon = new MyConnection();
        //Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "FA_RptBalanceSheet";
        Mycon.cmd.Parameters.AddWithValue("@finyear", FinYear);
        DataSet dt = new DataSet();
        Mycon.adp.SelectCommand = Mycon.cmd;
        Mycon.adp.Fill(dt);
        return dt;

    }
    public DataSet EQ1(string Q)
    {
        MyConnection Mycon = new MyConnection();
        DataSet dt = new DataSet();
        Mycon.Open();
        //Mycon.cmd.CommandType = CommandType.Text;
        //Mycon.cmd.CommandText = Q;
        Mycon.adp.SelectCommand.CommandText = Q;
        Mycon.adp.Fill(dt);
        Mycon.Close();
        return dt;
    }
    public DataTable getBalanceSheetP(string FinYear)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "FA_RptBalanceSheetP";
        Mycon.cmd.Parameters.AddWithValue("@finyear", FinYear);

        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        Mycon.Close();
        return dt;

    }
    public Boolean FormAuthorize(string UserID, string Url, string Centrecode, string ModuleID)
    {
        Url = "~" + Url;
        string str = "SELECT  menu_name, url, isnull(parent_menuid,0) as parent_menuid, menuid FROM mast_menumaster where Module_ID =" + ModuleID + " and menuid in(select menuid from role_menu where roleid in " +
                "(select  *  from dbo.[fnSplit]((select top(1) replace(u.role_Code,'''','') from user_module_roles " +
                " as u where UID_NO =" + UserID + " and (Center_Code='" + Centrecode + "' or Center_Code='ALLCEN' )), ','))) and url='" + Url + "'";

        DataTable dt = new DataTable();
        dt = EQ(str);
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        return false;
    }
    public virtual string Delete(string tablename, string idfield, string value)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.cmd.CommandText = "USP_Delete";
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@IDField", idfield);
        //Mycon.cmd.Parameters.AddWithValue("@WereClause",where);
        SqlParameter p1 = new SqlParameter("@Value", SqlDbType.VarChar, 10);
        p1.Direction = ParameterDirection.Input;
        p1.Value = value;
        Mycon.cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@msg", SqlDbType.VarChar, 8000);
        p2.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p2);
        try
        {
            Mycon.Open();
            Mycon.cmd.ExecuteNonQuery();
            Mycon.Close();

            return Mycon.cmd.Parameters["@msg"].Value.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
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
    public void FillDDLMonth(ref DropDownList ddl)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDDL_Month";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "MonthName";
        ddl.DataValueField = "MonthNumber";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
    }
    public void FillEmpCode(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "[USP_FillEmp]";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "EMP_NAME";
        ddl.DataValueField = "ECODE";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillSCODE(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "[USP_FillDLL_SCODE]";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "Sl_ID";
        ddl.DataValueField = "Sl_ID";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
        //ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillMONTH(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "[USP_FillDDL_Month]";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "MonthName";
        ddl.DataValueField = "MonthNumber";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public virtual DataTable Select(string tablename, string idfield, string value)
    {
        DataTable dt = new DataTable();
        MyConnection Mycon = new MyConnection();
        Mycon.cmd.CommandText = "Select_sp";
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@IDField", idfield);
        //Mycon.cmd.Parameters.AddWithValue("@WereClause",where);
        SqlParameter p1 = new SqlParameter("@Idvalue", SqlDbType.VarChar, 10);
        p1.Direction = ParameterDirection.Input;
        p1.Value = value;
        Mycon.cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@msg", SqlDbType.VarChar, 8000);
        p2.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p2);
        try
        {
            Mycon.Open();
            SqlDataReader sdr;
            sdr = Mycon.cmd.ExecuteReader();
            dt.Load(sdr);

            return dt;
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (Mycon.Mycon.State == ConnectionState.Open)
            {
                Mycon.Close();

            }
            Mycon.cmd.Dispose();
        }
        return dt;
    }
    public DataTable SelectEmpCodeByCategry(string sp_name, string Cat_ID)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;

        Mycon.cmd.Parameters.AddWithValue("@Cat_ID", Cat_ID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable SelectEmpCodeByCat(string sp_name, string CAT_CODE)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;

        Mycon.cmd.Parameters.AddWithValue("@CAT_CODE", CAT_CODE);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }

    public DataTable Voucher(string sp_name, string Voucher_Code)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;

        Mycon.cmd.Parameters.AddWithValue("@Voucher_Code", Voucher_Code);
        Mycon.cmd.CommandText = sp_name;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable SelectEmpByDesgination(string sp_name, int WorkCodeID, int DesgID)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@WorkCode", WorkCodeID);
        Mycon.cmd.Parameters.AddWithValue("@DesgID", DesgID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillDisplayEmployeeByWorkcodeAndDesignationAndDistrict(string sp_name, int WorkCode, Int32 DesgID, Int32 DistrictID)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name + "(" + WorkCode + "," + DistrictID + "," + DesgID + ")";
        //Mycon.cmd.Parameters.AddWithValue("@WorkCode", WorkCode);
        //Mycon.cmd.Parameters.AddWithValue("@DesignationId", DesgID);
        //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillDisplayEmployeeByWorkcodeAndDesignationALL(string sp_name, int WorkCode, Int32 DesgID)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name + "(" + DesgID + "," + WorkCode + ")";
        //Mycon.cmd.Parameters.AddWithValue("@WorkCode", WorkCode);
        //Mycon.cmd.Parameters.AddWithValue("@DesignationId", DesgID);
        //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillDisplaySPbyWorkCodeAndFinancialYear(string sp_name, int WorkCode, string FY)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@WorkCode", WorkCode);
        Mycon.cmd.Parameters.AddWithValue("@FinYear", FY);
        //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillGrid(string sp_name, string param_name, string id)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue(param_name, id);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillVoucherGrid(string sp_name, string param_name, string id)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue(param_name, id);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillGrid(string sp_name, string param_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name + "(" + param_name + ")";
        // Mycon.cmd.Parameters.AddWithValue(param_name, id);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillGrid1(string sp_name, string param_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name + " " + param_name;
        // Mycon.cmd.Parameters.AddWithValue(param_name, id);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillGrid(string sp_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable EQ(string Q)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = Q;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public int EQScaler(string Q)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = Q;
        int a;
        a = Convert.ToInt32(Mycon.cmd.ExecuteScalar());
        //dt.Load(sdr);
        Mycon.Close();
        return a;
    }
    public string EQSScaler(string Q)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = Q;
        string a1 = Convert.ToString(Mycon.cmd.ExecuteScalar().ToString());

        Mycon.Close();
        return a1;
    }
    public DataTable FillGridByView(string sp_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public void FillDDL_Loan(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        // ddl.Items.Insert(0, selectdesc);
        //ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }

    public void FillDDL(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillDDLNew(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLNew";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillDDLWithCode(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLWithCode";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public DataTable FillEmpDetailsSearch(string sp_name, string ECode, string EName, string Unit, string DescName, string DEPTT_CODE, string CompanyCode, string CenterCode)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@ECode", ECode);
        Mycon.cmd.Parameters.AddWithValue("@EName", EName);
        Mycon.cmd.Parameters.AddWithValue("@Unit", Unit);
        Mycon.cmd.Parameters.AddWithValue("@DesignationName", DescName);
        Mycon.cmd.Parameters.AddWithValue("@DEPTT_CODE", DEPTT_CODE);
        Mycon.cmd.Parameters.AddWithValue("@Comp_CODE", CompanyCode);
        Mycon.cmd.Parameters.AddWithValue("@Loc_CODE", CenterCode);
        //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }

    //public DataTable FillEmpDetailsSearch(string sp_name, string ECode, string EName, string Unit, string DescName, string DEPTT_CODE)
    //{
    //    MyConnection Mycon = new MyConnection();
    //    DataTable dt = new DataTable();
    //    Mycon.Open();
    //    Mycon.cmd.CommandType = CommandType.StoredProcedure;
    //    Mycon.cmd.CommandText = sp_name;
    //    Mycon.cmd.Parameters.AddWithValue("@ECode", ECode);
    //    Mycon.cmd.Parameters.AddWithValue("@EName", EName);
    //    Mycon.cmd.Parameters.AddWithValue("@Unit", Unit);
    //    Mycon.cmd.Parameters.AddWithValue("@DesignationName", DescName);
    //    Mycon.cmd.Parameters.AddWithValue("@DEPTT_CODE", DEPTT_CODE);

    //    //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
    //    SqlDataReader sdr;
    //    sdr = Mycon.cmd.ExecuteReader();
    //    dt.Load(sdr);
    //    Mycon.Close();
    //    return dt;
    //}

    //public DataTable FillEmpDetailsSearch(string sp_name, string ECode, string EName, string Unit, string DescName, string DEPTT_CODE, string Company)
    //{
    //    MyConnection Mycon = new MyConnection();
    //    DataTable dt = new DataTable();
    //    Mycon.Open();
    //    Mycon.cmd.CommandType = CommandType.StoredProcedure;
    //    Mycon.cmd.CommandText = sp_name;
    //    Mycon.cmd.Parameters.AddWithValue("@ECode", ECode);
    //    Mycon.cmd.Parameters.AddWithValue("@EName", EName);
    //    Mycon.cmd.Parameters.AddWithValue("@Unit", Unit);
    //    Mycon.cmd.Parameters.AddWithValue("@DesignationName", DescName);
    //    Mycon.cmd.Parameters.AddWithValue("@DEPTT_CODE", DEPTT_CODE);
    //    Mycon.cmd.Parameters.AddWithValue("@COMP_CODE", Company);
    //    //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
    //    SqlDataReader sdr;
    //    sdr = Mycon.cmd.ExecuteReader();
    //    dt.Load(sdr);
    //    Mycon.Close();
    //    return dt;
    //}

    public void FillDDL1(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL1";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        if (selectdesc != "")
        {
            ddl.Items.Insert(0, selectdesc);
            ddl.SelectedValue.Insert(0, "0");
        }
        Mycon.Close();
    }
    public void FillDDLORDER(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL_ORDER";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "NO";
        ddl.DataValueField = "ORDER_NO";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();

    }
    public void FillEmpByDesig(ref DropDownList ddl, string selectdesc, int DesignationID)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "Master_DisplayAll_EmployeeFilterByDesignation";
        Mycon.cmd.Parameters.AddWithValue("@DesignationID", DesignationID);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "EmployeeName";
        ddl.DataValueField = "EmployeeID";
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }

    public void FillDDLBySearchID(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string SearchFieldID, string SearchFieldValue, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLBySearchID";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        Mycon.cmd.Parameters.AddWithValue("@SearchFieldID", SearchFieldID);
        Mycon.cmd.Parameters.AddWithValue("@SearchFieldValue", SearchFieldValue);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public DataTable FillDDLS(ref DropDownList ddl, string selectdesc, string EMP_NO)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillLN_StopDLL";
        Mycon.cmd.Parameters.AddWithValue("@EMP_NO", EMP_NO);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "LOAN_TYPE_DESC";
        ddl.DataValueField = "LOAN_TYPE_CODE";
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
        return dt;
    }



    public DataTable FillEmpDetailsSearchNew(string sp_name, string ECode, string EName, string Unit, string DescName, string DEPTT_CODE, string Company)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@ECode", ECode);
        Mycon.cmd.Parameters.AddWithValue("@EName", EName);
        // Mycon.cmd.Parameters.AddWithValue("@Unit", Unit);
        // Mycon.cmd.Parameters.AddWithValue("@DesignationName", DescName);
        //Mycon.cmd.Parameters.AddWithValue("@DEPTT_CODE", DEPTT_CODE);
        //Mycon.cmd.Parameters.AddWithValue("@COMP_CODE", Company);
        //Mycon.cmd.Parameters.AddWithValue("@DistrictID", DistrictID);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public void FillDDLFY(ref DropDownList ddl, string selectdesc)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillFY";

        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = "Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }


    public void FillDDLSelfProc(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = selectdesc;
        //Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        //Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        //Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

        Mycon.Close();
    }
    public void FillDDLT(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLT";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(selectdesc, "0"));
        // ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }


    public void FillDDLTforzoo(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLTForzoo";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(selectdesc, "0"));
        // ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }

    public static String ConvertToXMLFormat<T>(ref List<T> list)
    {
        XmlDocument doc = new XmlDocument();
        XmlNode node = doc.CreateNode(XmlNodeType.Element, string.Empty, "root", null);

        foreach (T xml in list)
        {
            XmlElement element = doc.CreateElement("data");
            PropertyInfo[] allProperties = xml.GetType().GetProperties();
            foreach (PropertyInfo thisProperty in allProperties)
            {
                object value = thisProperty.GetValue(xml, null);
                XmlElement tmp = doc.CreateElement(thisProperty.Name);
                if (value != null)
                {
                    tmp.InnerXml = value.ToString();
                }
                else
                {
                    tmp.InnerXml = string.Empty;
                }
                element.AppendChild(tmp);
            }
            node.AppendChild(element);
        }
        doc.AppendChild(node);
        return doc.InnerXml;
    }
    public DataTable FillGrid_T_User(int PARAM_ID, int PARAM_SUBID, int YR_NO, int MTH_NO, int SAL_CODE)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_Get_T_SAL_USR_ENTRY_CHLD";
        Mycon.cmd.Parameters.AddWithValue("@PARAM_ID", PARAM_ID);
        Mycon.cmd.Parameters.AddWithValue("@PARAM_SUBID", PARAM_SUBID);
        Mycon.cmd.Parameters.AddWithValue("@YR_NO", YR_NO);
        Mycon.cmd.Parameters.AddWithValue("@MTH_NO", MTH_NO);
        Mycon.cmd.Parameters.AddWithValue("@SAL_CODE", SAL_CODE);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public int NoOFDate(string Q)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = Q;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        int a = Convert.ToInt32(dt.Rows[0][0].ToString());
        return a;
    }
    public DataTable FillddlByView(string sp_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.Text;
        Mycon.cmd.CommandText = sp_name;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public void FillDDLFrom(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename, string wherekey, string wherevalue)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLFrom";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        Mycon.cmd.Parameters.AddWithValue("@WhereField", wherekey);
        Mycon.cmd.Parameters.AddWithValue("@WhereValue", wherevalue);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillDDLIncomeHead(ref DropDownList ddl, string selectdesc)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "FillDLL_IncomeHeads";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = "Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillDDLExpenseHead(ref DropDownList ddl, string selectdesc)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "FillDLL_ExpenseHeads";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = "Name";
        ddl.DataValueField = "ID";
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void FillDDLORDERDA(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL_ORDERDA";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "NO";
        ddl.DataValueField = "OrderNo";
        ddl.DataBind();
        ddl.Items.Insert(0, "---Select---");
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public DataTable SelectEmpForChildrenAllowance(string sp_name, string FinYear, string Var)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@finYear", FinYear);
        Mycon.cmd.Parameters.AddWithValue("@update", Var);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public void FillDDLAmt(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLAmt";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.DataBind();
        Mycon.Close();
    }
    public void FillPayScale(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_Bind_PayScale";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.DataBind();
        Mycon.Close();
    }
    public void FillBank(ref DropDownList ddl)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "bank";
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        ddl.DataSource = dt;
        ddl.DataTextField = "Text";
        ddl.DataValueField = "Value";
        ddl.DataBind();
        Mycon.Close();
    }
    public int getAge(string DOB)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_Get_Age";
        Mycon.cmd.Parameters.AddWithValue("@DOB", DOB);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        int age = Convert.ToInt16(dt.Rows[0][0].ToString());
        return age;
    }

    public string SaveUpload(string fileName, byte[] fileData, string ext, string VIntNo, string VchType)
    {

        MyConnection Mycon = new MyConnection();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FilesUpload";

        Mycon.cmd.Parameters.AddWithValue("@VoucherIntNo", VIntNo);
        Mycon.cmd.Parameters.AddWithValue("@VoucherType", VchType);
        Mycon.cmd.Parameters.AddWithValue("@originalName", fileName);
        Mycon.cmd.Parameters.AddWithValue("@contentType", ext);
        Mycon.cmd.Parameters.AddWithValue("@fileData", fileData);

        SqlParameter p2 = new SqlParameter("@msg", SqlDbType.VarChar, 8000);
        p2.Direction = ParameterDirection.Output;
        Mycon.cmd.Parameters.Add(p2);
        try
        {
            Mycon.Open();
            Mycon.cmd.ExecuteNonQuery();
            Mycon.Close();

            return Mycon.cmd.Parameters["@msg"].Value.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
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

    public DataTable BindUploadGrd(string VoucherIntNo)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_SelectFileUpload";
        Mycon.cmd.Parameters.AddWithValue("@VoucherIntNo", VoucherIntNo);

        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        Mycon.Close();
        return dt;

    }
    //=============SALES========================
    public void FillDDL2(ref DropDownList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL2";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        ddl.Items.Insert(0, selectdesc);
        ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public void Fillchk(ref CheckBoxList ddl, string selectdesc, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillChklist";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();
        //ddl.Items.Insert(0, selectdesc);
        //ddl.SelectedValue.Insert(0, "0");
        Mycon.Close();
    }
    public string ChangeDateToMMddyyyy(string strDate)
    {
        string strdd = strDate.Substring(0, 2);
        string strMM = strDate.Substring(3, 2);
        string strYY = strDate.Substring(6, 4);
        string DateModified = strMM + "/" + strdd + "/" + strYY;
        return DateModified;
    }
    public DataTable FillOwnerSearch(string sp_name, string benificiaryname, string scheme, string regno)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@beneficiaryname", benificiaryname);
        Mycon.cmd.Parameters.AddWithValue("@scheme", scheme);
        Mycon.cmd.Parameters.AddWithValue("@regno", regno);

        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }



    public DataTable FillUnitId(string sp_name, string ProjectId)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@ProjectId", ProjectId);


        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }

    public DataTable FillEMDAmount(string sp_name, string ProjectId, string UnitId)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@Project_Id", ProjectId);
        Mycon.cmd.Parameters.AddWithValue("@UnitId", UnitId);

        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }

    public DataTable Fillprioritydesc(string sp_name, string rowid)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@RowId", rowid);


        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillOwnerSearch(string sp_name, string applicationno)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@beneficiaryname", applicationno);

        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable Fillcancellationcharges(string sp_name)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public void FillDDL17(ref DropDownList ddl, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLL2";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

        Mycon.Close();
    }


    //==========================================

    public DataTable getMonthYearToSalaryParameter(string CompanyCode, string Location, string Volume)
    {
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FindMonthYearSalParam";
        Mycon.cmd.Parameters.AddWithValue("@COMPANY_CODE", CompanyCode);
        Mycon.cmd.Parameters.AddWithValue("@LOCATION_CODE", CompanyCode);
        Mycon.cmd.Parameters.AddWithValue("@VOLUME", CompanyCode);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        Mycon.Close();
        return dt;

    }
    public DataTable FillGridIncrement(string sp_name, string id, string cmp, string loc, string vol)
    {
        MyConnection Mycon = new MyConnection();
        DataTable dt = new DataTable();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = sp_name;
        Mycon.cmd.Parameters.AddWithValue("@dte", id);
        Mycon.cmd.Parameters.AddWithValue("@Company_Code", cmp);
        Mycon.cmd.Parameters.AddWithValue("@Location_Code", loc);
        Mycon.cmd.Parameters.AddWithValue("@Volume", vol);
        SqlDataReader sdr;
        sdr = Mycon.cmd.ExecuteReader();
        dt.Load(sdr);
        Mycon.Close();
        return dt;
    }
    public DataTable FillCmpLogin(string username)
    {

        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillCmp_Login";
        Mycon.cmd.Parameters.Clear();
        Mycon.cmd.Parameters.AddWithValue("@UserID", username);

        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);

        Mycon.Close();
        return dt;
    }
    public DataTable FillLOCLogin(string username)
    {

        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillLOC_Login";
        Mycon.cmd.Parameters.Clear();
        Mycon.cmd.Parameters.AddWithValue("@UserID", username);

        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);

        Mycon.Close();
        return dt;
    }
    public void FillDDLStation(ref DropDownList ddl, string TextField, string ValueField, string tablename)
    {
        ddl.Items.Clear();
        MyConnection Mycon = new MyConnection();
        Mycon.Open();
        Mycon.cmd.CommandType = CommandType.StoredProcedure;
        Mycon.cmd.CommandText = "USP_FillDLLNew";
        Mycon.cmd.Parameters.AddWithValue("@TableName", tablename);
        Mycon.cmd.Parameters.AddWithValue("@TextField", TextField);
        Mycon.cmd.Parameters.AddWithValue("@ValueField", ValueField);
        SqlDataReader sdr;
        DataTable dt = new DataTable();
        sdr = Mycon.cmd.ExecuteReader();

        dt.Load(sdr);
        ddl.DataSource = dt;

        ddl.DataTextField = TextField;
        ddl.DataValueField = ValueField;
        ddl.DataBind();

        Mycon.Close();
    }
}

