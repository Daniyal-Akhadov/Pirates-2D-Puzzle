using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Windows.LocaleWindow
{
    public class LocalizationWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWidget _prefab;
        [SerializeField] private List<LocaleInfo> _localesInfo;

        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

        private readonly string[] _idLocales = { "en", "ru", "es" };

        protected override void Start()
        {
            base.Start();
            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
            _dataGroup.SetData(ComposeData());
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.Instance.SetLocale(selectedLocale);
        }

        private List<LocaleInfo> ComposeData()
        {
            var result = new List<LocaleInfo>();

            foreach (var lo in _localesInfo)
            {
                result.Add(new LocaleInfo { Icon = lo.Icon, LocaleId = lo.LocaleId });
            }

            return result;
        }
    }

    [Serializable]
    public class LocaleInfo
    {
        public string LocaleId;
        public Sprite Icon;
    }
}