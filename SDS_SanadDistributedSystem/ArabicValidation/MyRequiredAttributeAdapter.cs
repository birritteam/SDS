using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace SDS_SanadDistributedSystem.ArabicValidation
{
    public class MyRequiredAttributeAdapter : RequiredAttributeAdapter
    {


        public MyRequiredAttributeAdapter(ModelMetadata metaData,ControllerContext context,RequiredAttribute attribute)
            :base (metaData,context,attribute)
        {
            attribute.ErrorMessageResourceType = typeof(Messages);
            attribute.ErrorMessageResourceName = "PropertyValueRequired";

        }


    }
}