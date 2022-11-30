using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _required;
        [SerializeField] private bool _isRemoveAfterUse;

        [Header("Events")] [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            bool areAllRequiredMet = true;

            foreach (var item in _required)
            {
                int count = session.Data.Inventory.Count(item.Id);
                if (count < item.Value)
                {
                    areAllRequiredMet = false;
                }
            }

            if (areAllRequiredMet == true)
            {
                if (_isRemoveAfterUse == true)
                {
                    foreach (var item in _required)
                    {
                        session.Data.Inventory.Remove(item.Id, item.Value);
                    }
                }

                _onSuccess?.Invoke();
            }
            else
            {
                _onFail?.Invoke();
            }
        }
    }
}