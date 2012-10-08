using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class AllowFileExtAttribute : ValidationAttribute
    {      
        public override bool IsValid(object value)
        {
            var val = value as string;
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return val.Equals("Hello,world!", StringComparison.OrdinalIgnoreCase);
        }
    }

    public class AllowFileExtValidator : DataAnnotationsModelValidator<AllowFileExtAttribute>
    {
        public AllowFileExtValidator(ModelMetadata metadata, ControllerContext context, AllowFileExtAttribute attribute)
            : base(metadata, context, attribute)
        {}
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] {
                new ModelClientValidationRule {
                    ErrorMessage = "上传的格式不被允许",
                    ValidationType = "custom"
                }
            };
        }
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return base.Validate(container);
        }
    }

    //public class AllowFileExtAttribute : Attribute
    //{
    //    public String AllowFileExt { get; set; }

    //    public AllowFileExtAttribute(string name)
    //    {
    //        AllowFileExt = name;
    //    }
    //}

    //public class AllowFileExt : ModelValidator
    //{
    //    private string allowFileExt;
    //    public AllowFileExt(ModelMetadata metaData, ControllerContext context, string allowFile)
    //        : base(metaData, context)
    //    {
    //        allowFileExt = allowFile;
    //    }

    //    public override IEnumerable<ModelValidationResult> Validate(object container)
    //    {
    //        if (container == null)
    //        {
    //            yield break;
    //        }
    //        var p = container.GetType();

    //        var pi = container.GetType().GetProperty("UploadFile").GetValue(container, null) as HttpPostedFileWrapper;
    //        var fileName = pi.FileName;
    //        yield return new ModelValidationResult()
    //        {
    //            Message = "上传的文件类型不被允许!"
    //        };
    //        //if (pi != null)
    //        //{
    //        //    var fileExtValue = pi.GetValue(container, null);
    //        //    if (!(fileExtValue ?? string.Empty).ToString().Split(',').Contains(Metadata.Model ?? String.Empty))
    //        //    {
    //        //        yield return new ModelValidationResult()
    //        //       {
    //        //           Message = "上传的文件类型不被允许!"
    //        //       };
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    throw new InvalidOperationException("属性" + allowFileExt + "不存在");
    //        //}
    //    }
    //}

    //public class AllowFileExtProvider : AssociatedValidatorProvider
    //{
    //    protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
    //    {
    //        foreach (AllowFileExtAttribute attr in attributes.OfType<AllowFileExtAttribute>())
    //        {
    //            yield return new AllowFileExt(metadata, context, attr.AllowFileExt);
    //        }
    //    }
    //}
}
