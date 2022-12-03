using System;
using UnityEditor;

namespace PixelCrew.Utilities.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType retValue)
            where TEnumType : Enum
        {
            retValue = default;
            var names = property.enumNames;

            if (names == null || names.Length == 0)
            {
                return false;
            }

            string enumName = names[property.enumValueIndex];
            retValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
    }
}