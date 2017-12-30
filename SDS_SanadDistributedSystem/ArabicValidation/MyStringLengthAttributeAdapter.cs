using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDS_SanadDistributedSystem.ArabicValidation
{
    public class MyStringLengthAttributeAdapter : StringLengthAttributeAdapter
    {

        public MyStringLengthAttributeAdapter(ModelMetadata metaData, ControllerContext context, StringLengthAttribute attribute) : 
            base(metaData, context, attribute)
        {
            attribute.ErrorMessageResourceType = typeof(Messages);
            attribute.ErrorMessageResourceName = "StringLengthInvalid";

        }

    }
}