﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Layer01_Common.Objects;
using Layer01_Common_Web.Common;
using DataObjects_Framework.Common;

namespace Layer03_Website
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection WebConfig = System.Configuration.ConfigurationManager.AppSettings;

            /*
            Layer01_Common.Common.Global_Variables.gConnection_Server = WebConfig["Server"];
            Layer01_Common.Common.Global_Variables.gConnection_Database = WebConfig["Database"];
            Layer01_Common.Common.Global_Variables.gConnection_Username = WebConfig["UserName"];
            Layer01_Common.Common.Global_Variables.gConnection_Password = WebConfig["Password"];
            */

            //Layer01_Common.Common.Global_Variables.gConnection_SqlServerConnectionString = WebConfig["SqlServer_ConnectionString"];
            //Layer01_Common.Common.Global_Variables.gConnection_SharePoint_Server = WebConfig["SharePointServer"];
            //Layer01_Common.Common.Global_Variables.gConnection_SharePoint_UserName = WebConfig["SharePointUserName"];
            //Layer01_Common.Common.Global_Variables.gConnection_SharePoint_Password = WebConfig["SharePointPassword"];

            DataObjects_Framework.Common.Do_Globals.gSettings.pConnectionString = WebConfig["SqlServer_ConnectionString"];
            //DataObjects_Framework.Common.Do_Globals.gSettings.pDataAccessType = DataObjects_Framework.Common.Do_Constants.eDataAccessType.DataAccess_SqlServer;
            DataObjects_Framework.Common.Do_Globals.gSettings.pDataAccessType = DataObjects_Framework.Common.Do_Constants.eDataAccessType.DataAccess_WCF;
            Do_Globals.gSettings.pWcfAddress = @"http://localhost:4802/WcfService.svc";
            DataObjects_Framework.Common.Do_Globals.gSettings.pUseSoftDelete = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception Ex = this.Server.GetLastError();
            if (Ex == null) { return; }

            Methods_Web.ErrorHandler(Ex, this.Server);
            Global_Web.gEx = Ex.InnerException == null ? Ex : Ex.InnerException;
            this.Response.Redirect("~/Page/ErrorPage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}