using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BILL.Dto
{
    public class AccountCategoryDto : BaseDto
    {
        public string Name { get; set; }
    }
    public class ChangeCategoryNameDto : BaseDto
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
    public class ChangeCategoryShowDto : BaseDto
    {
        public string ShowName { get; set; }
        public string HideName { get; set; }
    }
}