using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Layer01_Common;
using Layer01_Common.Common;
using Layer01_Common.Objects;


namespace Layer01_Common_Web.Objects
{
    [Serializable()]
    public class ClsBindGridColumn_Web : ClsBindGridColumn
    {
        #region _Variables

        public Unit mUnit_Width;
        public ButtonColumnType mButtonType;
        public string mCommandName;
        
        public string mFieldText;
        public string mFooterText;
        public string mFieldNavigateUrl_Field;
        public string mFieldNavigateUrl_Text;

        #endregion

        #region _Constructor

        public ClsBindGridColumn_Web(string FieldName)
        { this.Setup(FieldName, FieldName); }

        public ClsBindGridColumn_Web(
            string FieldName
            , string FieldDesc
            , Unit? Width = null
            , string DataFormat = ""
            , Layer01_Constants.eSystem_Lookup_FieldType FieldType = Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Static
            , bool IsVisible = true
            , bool Enabled = true
            , bool IsFilter = true)
        {
            this.Setup(
                FieldName
                , FieldDesc
                , 100
                , DataFormat
                , FieldType
                , IsVisible
                , Enabled
                , IsFilter);
            this.mUnit_Width = Width == null ? new Unit("100px") : Width.Value;
        }

        protected override void Setup(string FieldName, string FieldDesc, int Width = 100, string DataFormat = "", Layer01_Constants.eSystem_Lookup_FieldType FieldType = Layer01_Constants.eSystem_Lookup_FieldType.FieldType_Static, bool IsVisible = true, bool Enabled = true, bool IsFilter = true)
        {
            base.Setup(FieldName, FieldDesc, Width, DataFormat, FieldType, IsVisible, Enabled, IsFilter);
            if (this.mUnit_Width == null) { this.mUnit_Width = new Unit(this.mWidth.ToString() + "px"); }
        }

        #endregion

    }
}