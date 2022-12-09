using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Utilities.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;

        public readonly StringProperty InterfaceSelection = new();
        private readonly CompositeDisposable _trash = new();

        public string Used => _data.PerksData.Used.Value;

        public bool IsDoubleJumpSupported
        {
            get
            {
                if (string.IsNullOrEmpty(Used) == true)
                    return false;

                return Used == "double_jump" && GetPerkDefinitionBy(Used).Cooldown.IsReady && InvokeUsed();
            }
        }

        public bool IsSuperShieldSupported
        {
            get
            {
                if (string.IsNullOrEmpty(Used) == true)
                    return false;

                return Used == "super_shield" && GetPerkDefinitionBy(Used).Cooldown.IsReady && InvokeUsed();
            }
        }

        public bool IsSuperThrowSupported
        {
            get
            {
                if (string.IsNullOrEmpty(Used) == true)
                    return false;

                var first = Used == "super_throw";
                var second = GetPerkDefinitionBy(Used).Cooldown.IsReady;

                Debug.Log(first);
                Debug.Log(second);

                return Used == "super_throw" && GetPerkDefinitionBy(Used).Cooldown.IsReady && InvokeUsed();
            }
        }

        public bool IsDashSupported
        {
            get
            {
                if (string.IsNullOrEmpty(Used) == true)
                    return false;

                var first = Used == "dash";
                var second = GetPerkDefinitionBy(Used).Cooldown.IsReady;

                Debug.Log(first);
                Debug.Log(second);

                return Used == "dash" && GetPerkDefinitionBy(Used).Cooldown.IsReady && InvokeUsed();
            }
        }

        public event Action OnChanged;

        public event Action OnUsedPerk;

        public PerksModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefinitionsFacade.Instance.PerksRepository.All[0].Id;
            _trash.Retain(_data.PerksData.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Dispose()
        {
            _trash?.Dispose();
        }

        public void TryUnlock(string id)
        {
            var definition = DefinitionsFacade.Instance.PerksRepository.Get(id);
            var isEnoughResources = _data.Inventory.IsEnough(definition.Price);

            if (isEnoughResources == true)
            {
                _data.Inventory.Remove(definition.Price.ItemId, definition.Price.Count);
                _data.PerksData.AddPerk(id);
                OnChanged?.Invoke();
            }
        }

        public void UsePerk(string selectedPerk)
        {
            _data.PerksData.Used.Value = selectedPerk;
        }

        public bool IsUsed(string id)
        {
            return _data.PerksData.Used.Value == id;
        }

        public bool IsUnlocked(string id)
        {
            return _data.PerksData.IsUnlocked(id);
        }

        public bool CanBuy(string id)
        {
            var definition = DefinitionsFacade.Instance.PerksRepository.Get(id);
            return _data.Inventory.IsEnough(definition.Price);
        }

        private PerkDefinition GetPerkDefinitionBy(string id)
        {
            return DefinitionsFacade.Instance.PerksRepository.Get(id);
        }

        private bool InvokeUsed()
        {
            OnUsedPerk?.Invoke();
            return true;
        }
    }
}