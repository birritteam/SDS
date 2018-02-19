using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.Models.customValidation
{
    public class CenterServiceValidation : ValidationAttribute//, IClientModelValidator
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            sds_dbEntities db = new sds_dbEntities();
            centerservice centerService = (centerservice)validationContext.ObjectInstance;
            var centerservices = db.centerservices.ToList();

            foreach (var item in centerservices)
            {
                if (item.enabled == true && item.idservice_FK == centerService.idservice_FK 
                    && centerService.enabled == true
                    && item.idcenter_FK == centerService.idcenter_FK)
                {
                return new ValidationResult("الخدمة مفعلة ضمن المركز");
                }
            }

            return null;
        }
    }
}