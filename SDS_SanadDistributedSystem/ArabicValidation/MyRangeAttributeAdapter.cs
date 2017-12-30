using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDS_SanadDistributedSystem.ArabicValidation
{
    public class MyRangeAttributeAdapter : RangeAttributeAdapter
    {



        public MyRangeAttributeAdapter(ModelMetadata metaData, ControllerContext context, RangeAttribute attribute) : 
            base(metaData, context, attribute)
        {
            attribute.ErrorMessageResourceType = typeof(Messages);
            attribute.ErrorMessageResourceName = "RangeInvalid";

        }



    }
}