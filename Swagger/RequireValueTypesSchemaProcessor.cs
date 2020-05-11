using System;
using HttpPatchSample.Models;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema;
using NJsonSchema.Generation;

namespace HttpPatchSample.Swagger
{
    /// <summary>
    /// Schema processor that makes all value types (int, string, bool, etc.) required in OpenApi
    /// Classes that inherits from PatchRequest are omitted (since all properties in these classes are optional)
    /// </summary>
    public class RequireValueTypesSchemaProcessor : ISchemaProcessor
    {
        private static readonly Type _patchRequestType = typeof(PatchDtoBase);

        public void Process(SchemaProcessorContext context)
        {
            var schema = context.Schema;
            if (context.Type.IsSubclassOf(_patchRequestType)
                || context.Type == typeof(ValidationProblemDetails))
            {
                // Classes that inherits from PatchRequest are omitted (since all properties in these classes are optional)
                return;
            }

            foreach (var propertyKeyValue in schema.Properties)
            {
                var property = propertyKeyValue.Value;
                string propertyName = property.Name;
                if (property.Type == JsonObjectType.String || property.Type == JsonObjectType.Boolean ||
                    property.Type == JsonObjectType.Integer || property.Type == JsonObjectType.Number
                    || property.Type == JsonObjectType.None /* enum */
                )
                {
                    if (!schema.RequiredProperties.Contains(propertyName))
                    {
                        schema.RequiredProperties.Add(propertyName);
                    }
                }

                if (property.Type == JsonObjectType.String)
                {
                    if (property.Format != "date-time")
                    {
                        property.IsNullableRaw = false;
                    }
                }
            }
        }
    }
}