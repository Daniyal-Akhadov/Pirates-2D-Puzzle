using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Utilities.Disposables;

namespace PixelCrew.Model.Models
{
    public class StatsModel : IDisposable
    {
        private readonly PlayerData _data;
        private readonly CompositeDisposable _trash = new();
        public readonly ObservableProperty<StatId> InterfaceSelectedStat = new();
        public event Action OnChanged;
        public event Action<StatId> OnUpgraded;

        public StatsModel(PlayerData data)
        {
            _data = data;
            _trash.Retain(InterfaceSelectedStat.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void LevelUp(StatId id)
        {
            var definition = DefinitionsFacade.Instance.PlayerDefinitions.GetStat(id);
            var nextLevel = GetCurrentLevel(id) + 1;

            if (definition.Levels.Length > nextLevel)
            {
                var price = definition.Levels[nextLevel].Price;

                if (_data.Inventory.IsEnough(price))
                {
                    _data.Inventory.Remove(price.ItemId, price.Count);
                    _data.LevelData.LevelUp(id);

                    PostProcessLevelUp(id);
                    OnChanged?.Invoke();
                    OnUpgraded?.Invoke(id);
                }
            }
        }

        private void PostProcessLevelUp(StatId id)
        {
            switch (id)
            {
                case StatId.Hp:
                    _data.Health.Value = (int)GetValue(id);
                    break;
                case StatId.Speed:
                    break;
                case StatId.RangeDamage:
                    break;
                case StatId.CriticalDamage:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }

        public float GetValue(StatId id, int level = -1)
        {
            return GetLevelDef(id, level).Value;
        }

        public StatLevelDef GetLevelDef(StatId id, int level = -1)
        {
            if (level == -1)
                level = GetCurrentLevel(id);

            var def = DefinitionsFacade.Instance.PlayerDefinitions.GetStat(id);
            return def.Levels.Length > level ? def.Levels[level] : default;
        }

        public int GetCurrentLevel(StatId id) => _data.LevelData.GetLevel(id);

        public void Dispose()
        {
        }
    }
}