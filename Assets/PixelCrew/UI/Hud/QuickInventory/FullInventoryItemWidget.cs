using PixelCrew.Model.Data;

namespace PixelCrew.UI.Hud.QuickInventory
{
    public class FullInventoryItemWidget : InventoryItemWidget
    {
        protected override void Start()
        {
            base.Start();
            _icon.gameObject.SetActive(false);
            _value.gameObject.SetActive(false);
            Trash.Retain(Session.FullInventoryModel.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        public override void SetData(InventoryItemData item, int index)
        {
            base.SetData(item, index);

            _icon.gameObject.SetActive(_icon.sprite != null);
            _value.gameObject.SetActive(_value.text != string.Empty);

            if (Session != null)
                Trash.Retain(Session.FullInventoryModel.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        public void OnSelect()
        {
            Session.FullInventoryModel.SelectedIndex.Value = Index;
        }
    }
}