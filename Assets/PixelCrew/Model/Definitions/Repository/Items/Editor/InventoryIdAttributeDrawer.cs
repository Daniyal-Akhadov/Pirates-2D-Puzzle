using System.Linq;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEditor;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Editor
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]
    public class InventoryIdAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var definitions = DefinitionsFacade.Instance.Items.DefinitionsForEditor;
            var allId = definitions.Select(definition => definition.Id).ToList();
            int index = Mathf.Max(allId.IndexOf(property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, allId.ToArray());
            property.stringValue = allId[index];
        }
    }
}