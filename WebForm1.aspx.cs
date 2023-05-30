using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }
        private void BindGridViewData()
        {
            GridView1.DataSource = EmployeeDataAccessLayer.GetAllEmployees();
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                GridView1.EditIndex = rowIndex;
                BindGridViewData();
            }
            else if (e.CommandName == "DeleteRow")
            {
                EmployeeDataAccessLayer.DeleteEmployee(Convert.ToInt32(e.CommandArgument));
                BindGridViewData();
            }
            else if (e.CommandName == "CancelUpdate")
            {
                GridView1.EditIndex = -1;
                BindGridViewData();
            }
            else if (e.CommandName == "UpdateRow")
            {
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;

                int employeeId = Convert.ToInt32(e.CommandArgument);


                string firstname = ((TextBox)GridView1.Rows[rowIndex].FindControl("txtFirst")).Text;
                string lastname = ((TextBox)GridView1.Rows[rowIndex].FindControl("txtLast")).Text;
                string gender = ((DropDownList)GridView1.Rows[rowIndex].FindControl("ddlGender")).SelectedValue;
                string city = ((TextBox)GridView1.Rows[rowIndex].FindControl("txtCity")).Text;

                EmployeeDataAccessLayer.UpdateEmployee(employeeId, firstname, lastname, gender, city);

                GridView1.EditIndex = -1;
                BindGridViewData();
            }
            else if (e.CommandName == "InsertRow")
            {
                string firstname = ((TextBox)GridView1.FooterRow.FindControl("txtFirstname")).Text;
                string lastname = ((TextBox)GridView1.FooterRow.FindControl("txtLastname")).Text;
                string gender = ((DropDownList)GridView1.FooterRow.FindControl("ddlGender")).SelectedValue;
                string city = ((TextBox)GridView1.FooterRow.FindControl("txtCity")).Text;
                EmployeeDataAccessLayer.InsertEmployee(firstname, lastname, gender, city);
                BindGridViewData();
            }
        }

    }

}

