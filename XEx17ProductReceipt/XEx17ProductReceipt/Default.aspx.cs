using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XEx17ProductReceipt
{
    public partial class Default : System.Web.UI.Page
    {
        protected void grdProducts_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                lblError.Text = "A database error has occurred. " +
                    e.Exception.Message;
                e.ExceptionHandled = true;
                e.KeepInEditMode = true;
            }
        }
    }
}