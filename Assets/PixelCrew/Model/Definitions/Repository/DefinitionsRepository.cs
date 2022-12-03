using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository
{
    public class DefinitionsRepository<TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] Collection;

        public TDefType Get(string id)
        {
            TDefType result = default;

            if (string.IsNullOrEmpty(id) == true)
                return result;

            foreach (var item in Collection)
            {
                if (item.Id == id)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }
    }

    public interface IHaveId
    {
        string Id { get; }
    }
}