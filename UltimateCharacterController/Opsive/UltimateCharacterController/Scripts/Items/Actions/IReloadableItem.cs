/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Items.Actions
{
    using Opsive.Shared.Inventory;
    using Opsive.UltimateCharacterController.Items.Actions.Modules;

    public interface IReloadableItemModule:
        IModuleGetReloadItemSubstateIndex, IModuleStartItemReload, IModuleCanReloadItem, IModuleReloadItem, IModuleItemReloadComplete,IModuleShouldReload
    {
        /// <summary>
        /// Can the camera zoom while the item is reloading?
        /// </summary>
        /// <returns>True if the camera can zoom while the item is reloading.</returns>
        bool CanReloadCameraZoom();
    }

    /// <summary>
    /// Interface for an item that can be reloaded.
    /// </summary>
    public interface IReloadableItem
    {
        /// <summary>
        /// Returns the item that the ReloadableItem is attached to.
        /// </summary>
        /// <returns>The item that the ReloadableItem is attached to.</returns>
        CharacterItem CharacterItem { get; }

        /// <summary>
        /// Returns the Reloadable Item module that is currently enabled.
        /// </summary>
        IReloadableItemModule ReloadableItemModule { get; }

        /// <summary>
        /// Can the camera zoom while the item is reloading?
        /// </summary>
        /// <returns>True if the camera can zoom while the item is reloading.</returns>
        bool CanReloadCameraZoom();

        int GetReloadItemSubstateIndex();

        /// <summary>
        /// Starts to reload the item.
        /// </summary>
        void StartItemReload();

        /// <summary>
        /// Can the item be reloaded?
        /// </summary>
        /// <param name="checkEquipStatus">Should the reload ensure the item is equipped?</param>
        /// <returns>True if the item can be reloaded.</returns>
        bool CanReloadItem(bool checkEquipStatus);

        /// <summary>
        /// Reloads the item.
        /// </summary>
        /// <param name="fullClip">Should the full clip be force reloaded?</param>
        void ReloadItem(bool fullClip);

        /// <summary>
        /// The item has finished reloading.
        /// </summary>
        /// <param name="success">Was the item reloaded successfully?</param>
        /// <param name="immediateReload">Should the item be reloaded immediately?</param>
        void ItemReloadComplete(bool success, bool immediateReload);

        /// <summary>
        /// Should the item be reloaded? An IReloadableItem reference will be returned if the item can be reloaded.
        /// </summary>
        /// <param name="characterItem">The item which may need to be reloaded.</param>
        /// <param name="itemIdentifier">The ItemIdentifier that is being reloaded.</param>
        /// <param name="fromPickup">Is the item being reloaded from a pickup?</param>
        /// <returns>A reference to the IReloadableItem if the item can be reloaded. Null if the item cannot be reloaded.</returns>
        bool ShouldReload(CharacterItem characterItem, IItemIdentifier itemIdentifier, bool fromPickup);
    }
}