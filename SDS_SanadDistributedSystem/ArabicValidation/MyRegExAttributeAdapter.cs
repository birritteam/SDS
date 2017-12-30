using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace SDS_SanadDistributedSystem.ArabicValidation
{
    public class MyRegExAttributeAdapter : RegularExpressionAttributeAdapter
    {

        public MyRegExAttributeAdapter (ModelMetadata metaData, ControllerContext context, RegularExpressionAttribute attribute) : 
            base(metaData, context, attribute)
        {
            attribute.ErrorMessageResourceType = typeof(Messages);
            attribute.ErrorMessageResourceName = "RegExInvalid";

        } 

    }
}