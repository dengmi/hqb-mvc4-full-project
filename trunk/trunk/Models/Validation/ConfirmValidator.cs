using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Models
{
    public class ConfirmValidatorAttribute : Attribute
    {
        public String ConfirmPropertyName { get; set; }

        public ConfirmValidatorAttribute(string name)
        {
            ConfirmPropertyName = name;
        }
    }

    public class ConfirmValidator : ModelValidator
    {

        private string confirmPropertyName;

        public ConfirmValidator(ModelMetadata metaData, ControllerContext context, string confirmProperty)

            : base(metaData, context)
        {

            confirmPropertyName = confirmProperty;

        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {

            if (container == null)
            {
                yield break;
            }

            PropertyInfo pi = container.GetType().GetProperty(confirmPropertyName);

            if (pi != null)
            {
                var confirmValue = pi.GetValue(container, null);
                if ((Metadata.Model ?? String.Empty).Equals(confirmValue ?? String.Empty))
                {
                    yield return new ModelValidationResult()
                    {
                        Message = "不能设置自身为父节点!"
                    };
                }
            }
            else
            {
                throw new InvalidOperationException("属性" + confirmPropertyName + "不存在");
            }
        }
    }



    public class ConfirmValidatorProvider : AssociatedValidatorProvider
    {

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {

            foreach (ConfirmValidatorAttribute attr in attributes.OfType<ConfirmValidatorAttribute>())
            {

                yield return new ConfirmValidator(metadata, context, attr.ConfirmPropertyName);

            }

        }

    }


}
