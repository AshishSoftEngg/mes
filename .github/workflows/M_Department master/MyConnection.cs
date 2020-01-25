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

/// <summary>
/// Summary description for MyConnection
/// </summary>
public class MyConnection
{
    public SqlConnection Mycon = new SqlConnection();
    public SqlCommand cmd;
    public SqlDataAdapter adp;
    public SqlCommandBuilder cb;
    public SqlTransaction trans;
    public MyConnection()
    {
        Mycon.ConnectionString = ConfigurationManager.ConnectionStrings["CON1"].ToString();
        cmd = new SqlCommand("", Mycon);
        adp = new SqlDataAdapter("", Mycon);
        cb = new SqlCommandBuilder(adp);
        adp.InsertCommand = new SqlCommand("", Mycon);
        adp.UpdateCommand = new SqlCommand("", Mycon);
        adp.SelectCommand = new SqlCommand("", Mycon);
        adp.DeleteCommand = new SqlCommand("", Mycon);
cmd.CommandTimeout = 20000000;
        //
        // TODO: Add constructor logic here
        //
    }
    public void Open()
    {
        if (Mycon.State == ConnectionState.Closed)
        {
            Mycon.Open();
        }
    }
    public void Close()
    {
        if (Mycon.State == ConnectionState.Open)
        {
            Mycon.Close();
        }
    }
}
