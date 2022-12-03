using PixelCrew.Creatures.Hero;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class AddInventoryComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _value;
        [SerializeField] private UnityEvent _onComplete;

        public void Add(GameObject element)
        {
            if (element.TryGetComponent(out Hero hero))
            {
                hero.AddInInventory(_id, _value, _onComplete);
            }
        }
    }
}