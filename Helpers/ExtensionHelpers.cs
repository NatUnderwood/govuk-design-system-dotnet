﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Primitives;

namespace GovUkDesignSystem.Helpers
{
    //qq:DCC do we still need these?
    public static class ExtensionHelpers
    {

        public static TAttributeType GetSingleCustomAttribute<TAttributeType>(this MemberInfo property)
            where TAttributeType : Attribute
        {
            return property.GetCustomAttributes(typeof(TAttributeType)).SingleOrDefault() as TAttributeType;
        }

        //qq:DCC Can probably remove this once other changes are complete
        public static string GetCurrentValue<TModel, TProperty>(
            TModel model,
            PropertyInfo property,
            Expression<Func<TModel, TProperty>> propertyLambdaExpression)
            where TModel : GovUkViewModel
        {
            TProperty propertyValue = ExpressionHelpers.GetPropertyValueFromModelAndExpression(model, propertyLambdaExpression);
            if (model.HasSuccessfullyParsedValue(property))
            {
                return propertyValue.ToString();
            }
            else if (propertyValue != null)
            {
                return propertyValue.ToString();
            }

            string parameterName = $"GovUk_Text_{property.Name}";
            StringValues unparsedValues = model.GetUnparsedValues(parameterName);

            string unparsedValueOrNull = unparsedValues.Count > 0 ? unparsedValues[0] : null;
            return unparsedValueOrNull;
        }
    }
}
