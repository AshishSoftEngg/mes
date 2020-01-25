using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Xml.Linq;
using System.IO;

using System.Configuration;

public partial class Payroll_Master_M_Department : System.Web.UI.Page
{
    #region Page Properties
    PL_DepartmentMaster PL = new PL_DepartmentMaster();
    BL_DepartmentMaster BL = new BL_DepartmentMaster();
    CommonCode cc = new CommonCode();
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                DeptCode();
                BindGrid();

                //checkLoginAndBindGrid();
                //ViewState["VolumeID"] = "";
                btnDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Messagebox.Show(ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (validation())
            {
                if (btnSave.Text == "Save")
                {
                    PL.sptype = 0;

                    PL.DepartmentCode = lblDepartmentCode.Text;
                    PL.DepartmentName = txtDepartmentName.Text;
                    PL.PayBillSighAuth = txtPayBSA.Text;
                    PL.CreatedBy = Session["UserName"].ToString();


                    if (FUuploadSign.HasFile)//upload photo
                    {
                        string Attach, FilePath = "";
                        Random _r = new Random();
                        string n = _r.Next().ToString();
                        Attach = Path.GetFileName(FUuploadSign.FileName.ToString());
                        FilePath = "../EmployeeImages/UploadSigningAutherity/" + n + Attach;
                        lbl_img.Text = FilePath.ToString();
                        FileInfo fileinfo = new FileInfo(Attach);
                        string fileextention = fileinfo.Extension.ToLower();
                        int sz = FUuploadSign.PostedFile.ContentLength;
                        if (sz < (20 * 1024) || sz > (200 * 1024))
                        {
                            Messagebox.Show("Please upload File with size greater than 20 kb and less than 200Kb!!"); 
                            return;
                        }
                        if (fileextention == ".jpg" || fileextention == ".jpeg" || fileextention == ".png" || fileextention == ".gif" || fileextention == ".bmp")
                        {
                            string SaveLocation = Server.MapPath("../EmployeeImages/UploadSigningAutherity/") + n + Attach;
                            FUuploadSign.SaveAs(SaveLocation);
                            PL.PayBillSighAuthUpload = SaveLocation;

                        }

                        else
                        {
                            Messagebox.Show("Please upload File with extention jpg,jpeg,gir,bmp or png !!");
                            return;

                        }
                    }

                }
                else
                {
                    PL.sptype = 1;
                    PL.TableID = Convert.ToInt32(lblTabid.Text.Trim());
                    PL.DepartmentCode = lblDepartmentCode.Text;
                    PL.DepartmentName = txtDepartmentName.Text;
                    PL.PayBillSighAuth = txtPayBSA.Text;
                    PL.CreatedBy = Session["UserName"].ToString();

                    if (FUuploadSign.HasFile)//upload photo
                    {
                        string Attach, FilePath = "";
                        Random _r = new Random();
                        string n = _r.Next().ToString();
                        Attach = Path.GetFileName(FUuploadSign.FileName.ToString());
                        FilePath = "../EmployeeImages/UploadSigningAutherity/" + n + Attach;
                        lbl_img.Text = FilePath.ToString();
                        FileInfo fileinfo = new FileInfo(Attach);
                        string fileextention = fileinfo.Extension.ToLower();
                        int sz = FUuploadSign.PostedFile.ContentLength;
                        if (sz < (20 * 1024) || sz > (200 * 1024))
                        {
                            Messagebox.Show("Please upload File with size greater than 20 kb and less than 200Kb!!");
                            return;
                        }
                        if (fileextention == ".jpg" || fileextention == ".jpeg" || fileextention == ".png" || fileextention == ".gif" || fileextention == ".bmp")
                        {
                            string SaveLocation = Server.MapPath("../EmployeeImages/UploadSigningAutherity/") + n + Attach;
                            FUuploadSign.SaveAs(SaveLocation);
                            PL.PayBillSighAuthUpload = SaveLocation;

                        }

                        else
                        {
                            Messagebox.Show("Please upload File with extention jpg,jpeg,gir,bmp or png !!");
                            return;

                        }
                    }
                    btnSave.Text = "Save";
                }

  
                BL.insert(PL);
                Messagebox.Show(PL.msg);
                BindGrid();
                Reset();
                btnReset.Focus();

            }
        }
        catch (Exception ex)
        {
            Messagebox.Show(ex.ToString());
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
        }
        catch (Exception ex)
        {
            Messagebox.Show(ex.ToString());
        }

    }   
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            PL.sptype = 1;
            PL.TableID = Convert.ToInt32(lblTabid.Text.Trim());
            // PL.CCODE = ddlDesignCatagoryCode.SelectedValue;
            PL.DepartmentCode = lblDepartmentCode.Text.Trim();
            BL.insert(PL);
            lblMsg.Text = "";
            Messagebox.Show(PL.msg);

            btnDelete.Enabled = false;
            Reset();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Messagebox.Show(ex.ToString());
        }
    }
    protected void LinkbtnSelect_Click(object sender, EventArgs e)
     {
         try
         {
             LinkButton lnk = (LinkButton)sender;
             GridViewRow grdRow = (GridViewRow)lnk.Parent.Parent;
             int row = grdRow.RowIndex;

             Label lblDepartmentCode = (Label)grdDepartmentMaster.Rows[row].FindControl("lblDepartmentCode");
             Label lblDepartmentName = (Label)grdDepartmentMaster.Rows[row].FindControl("lblDepartmentName");
            Label lblPayBillSignAuth = (Label)grdDepartmentMaster.Rows[row].FindControl("lblPayBillSignAuth");
          //  HyperLink linkSignToview = (HyperLink)grdDepartmentMaster.Rows[row].FindControl("linkSignToview");
            Label tabid = (Label)grdDepartmentMaster.Rows[row].FindControl("lbltabid");
             lblTabid.Text = tabid.Text;
             // ddlDesignCatagoryCode.SelectedValue = lblgDesignCatagoryCode.Text.Trim();
             txtDepartmentName.Text = lblDepartmentName.Text;
             lblDepartmentCode.Text = lblDepartmentCode.Text.Trim();
            txtPayBSA.Text = lblPayBillSignAuth.Text.Trim();
          //  img_photo.ImageUrl = linkSignToview.Text.Trim();
            btnDelete.Enabled = true;
             btnSave.Text = "Update";
         }
         catch (Exception ex)
         {
             Messagebox.Show(ex.ToString());
         }
    }

    protected void grdDesignationMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDepartmentMaster.PageIndex = e.NewPageIndex;
        BindGrid();
       
    }
     
    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton lnk = (ImageButton)sender;
            GridViewRow grdRow = (GridViewRow)lnk.Parent.Parent;
            int row = grdRow.RowIndex;

            Label lblDepartmentCode = (Label)grdDepartmentMaster.Rows[row].FindControl("lblDepartmentCode");
            Label lblDepartmentName = (Label)grdDepartmentMaster.Rows[row].FindControl("lblDepartmentName");
            Label lblPayBillSignAuth = (Label)grdDepartmentMaster.Rows[row].FindControl("lblPayBillSignAuth");

            Label tabid = (Label)grdDepartmentMaster.Rows[row].FindControl("lbltabid");
            PL.sptype = 2;
            PL.TableID = Convert.ToInt32(tabid.Text);
            PL.DepartmentCode = lblDepartmentCode.Text;
            PL.DepartmentName = lblDepartmentName.Text;
            PL.PayBillSighAuth = lblPayBillSignAuth.Text;

            BL.Delete(PL);
            Reset();
            Messagebox.Show(PL.msg);
            BindGrid();
        }
        catch (Exception ex)
        {
            Messagebox.Show(ex.ToString());
        }

    }

   
    #endregion

    #region Page Private Methods
    public void DeptCode()
    {
        string st = Convert.ToString(cc.EQSScaler("select [dbo].[get_Max_DEPT_CODE]();"));
        lblDepartmentCode.Text = st;

    }
    public void BindGrid()
    {
        PL.sptype = 3;
        PL.TableID = 0;
        DataTable dt = BL.Bindgrid(PL);
        grdDepartmentMaster.DataSource = dt;
        ViewState["dt1"] = dt;
        grdDepartmentMaster.DataBind();

    }
    private Boolean validation()
    {
        if (txtDepartmentName.Text == "")
        {
            Messagebox.Show("Please Enter Department Name");
            //lblMsg.Text ="Please Enter Department Name";
            return false;
        }
        if (txtPayBSA.Text == "")
        {
            Messagebox.Show("Please Enter PayBill Signing Authetication");           
            return false;
        }

        return true;
    }
    protected void Reset()
    {
        txtDepartmentName.Text = "";
        DeptCode();
        txtPayBSA.Text = "";
        lblMsg.Text = "";
        btnSave.Enabled = true;
        btnSave.Text = "Save";
    }
    #endregion
}