using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.UI.Widgets
{
    public class DataGroup<TDataType, TItemType> where TItemType : MonoBehaviour, IItemRenderer<TDataType>
    {
        private readonly TItemType _prefab;
        private readonly Transform _container;

        protected readonly List<TItemType> CreatedItems = new();

        public DataGroup(TItemType prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public virtual void SetData(IList<TDataType> data, bool turnOf = true)
        {
            for (int i = CreatedItems.Count; i < data.Count; i++)
            {
                Debug.Log("I Create");
                var item = Object.Instantiate(_prefab, _container);
                CreatedItems.Add(item);
            }

            for (int i = 0; i < data.Count; i++)
            {
                Debug.Log("I set data");
                CreatedItems[i].SetData(data[i], i);
                CreatedItems[i].gameObject.SetActive(true);
            }

            if (turnOf == true)
                for (int i = data.Count; i < CreatedItems.Count; i++)
                {
                    CreatedItems[i].gameObject.SetActive(false);
                    Debug.Log($"I turn of!!!! {CreatedItems[i].gameObject.name}");
                }
        }
    }

    public interface IItemRenderer<TDataType>
    {
        void SetData(TDataType dataType, int index);
    }
}