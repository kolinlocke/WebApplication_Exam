using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Layer01_Common;
using Layer01_Common.Common;
using Layer02_Objects;
using Layer02_Objects._System;
using DataObjects_Framework;
using DataObjects_Framework.Common;
using DataObjects_Framework.BaseObjects;
using DataObjects_Framework.DataAccess;
using DataObjects_Framework.Connection;
using DataObjects_Framework.Objects;

namespace Layer02_Objects.Modules_Objects
{
    public class ClsApplicant : Base
    {
        #region _Constructor

        public ClsApplicant()
        { this.Setup("RecruitmentTestApplicant", ""); }

        #endregion
    }
}
