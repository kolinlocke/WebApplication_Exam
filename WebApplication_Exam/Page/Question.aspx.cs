using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;
using Layer01_Common_Web.Objects;
using Layer01_Common_Web_Telerik.Objects;
using Layer02_Objects;
using Layer02_Objects._System;
using Layer02_Objects.Modules_Objects;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;
using WebApplication_Exam;
using WebApplication_Exam._Base;

namespace WebApplication_Exam.Page
{
    public partial class Question : ClsBaseList_Page
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);

            ClsBindDefinition BindDef = new ClsBindDefinition();
            BindDef.List_Gc = new List<ClsBindGridColumn>();
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("RecruitmentTestQuestionsID", "Question ID", 120));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Question_Stripped", "Question Description", 400));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Category_Desc", "Category", 200));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("QuestionType_Desc", "Question Type", 200));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("Status_Desc", "Status", 120));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("UserName_CreatedBy", "Created By", 120));
            BindDef.List_Gc.Add(new ClsBindGridColumn_Web_Telerik("UserName_ApprovedBy", "Approved By", 120));
            BindDef.KeyName = "RecruitmentTestQuestionsID";
            BindDef.AllowPaging = true;
            BindDef.AllowSort = true;
            BindDef.IsPersistent = true;

            this.Setup(
                Layer02_Objects._System.Layer02_Constants.eSystem_Modules.Question
                , new ClsQuestion(this.pCurrentUser)
                , BindDef
                , true
                , true);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            this.pBtn_New.Text = "New Question";
            this.pBtn_New.Width = 150;
        }

        protected override void DeleteRecord(long KeyID)
        {
            /*
            Customer DeleteRecord Rights
            A Question Record can be deleted by:
                The user who created the record and has the Delete Rights and the record is not approved.
                A user who has the Delete and View Rights and the record is not approved.
            */

            Keys ClsKey = new Keys();
            ClsKey.Add("RecruitmentTestQuestionsID", Convert.ToInt64(KeyID));

            ClsQuestion Obj_Question = new ClsQuestion(this.pCurrentUser);
            Obj_Question.Load(ClsKey);

            if (this.pCurrentUser.CheckAccess(Layer02_Constants.eSystem_Modules.Question, Layer02_Constants.eAccessLib.eAccessLib_Delete))
            {
                Int64 CurrentUserID = Do_Methods.Convert_Int64(this.pCurrentUser.pDrUser["RecruitmentTestUserID"], 0);
                Int64 OwnerUserID = Do_Methods.Convert_Int64(Obj_Question.pDr["RecruitmentTestUserID_CreatedBy"], 0);
                bool IsApproved = Do_Methods.Convert_Boolean(Obj_Question.pDr["IsApproved"]);

                if (!
                        (
                            (
                                (CurrentUserID == OwnerUserID)
                                || (this.pCurrentUser.CheckAccess(Layer02_Constants.eSystem_Modules.Question, Layer02_Constants.eAccessLib.eAccessLib_View))
                            )
                            && (!IsApproved)
                        )
                    )
                { this.Show_EventMsg("You can't delete this question.", ClsBaseMain_Master.eStatus.Event_Error); }
                else
                { Obj_Question.Delete(); }
            }
            this.pGridList.RebindGrid();    
        }
    }
}