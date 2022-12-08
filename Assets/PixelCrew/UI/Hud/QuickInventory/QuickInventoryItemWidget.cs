namespace PixelCrew.UI.Hud.QuickInventory
{
    public class QuickInventoryItemWidget : InventoryItemWidget
    {
        protected override void Start()
        {
            base.Start();
            Trash.Retain(Session.QuickInventory.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }
    }
}