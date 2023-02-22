﻿/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------


namespace Opsive.UltimateCharacterController.Editor.Utility
{
    using Opsive.Shared.Utility;
    using UnityEditor;

    /// <summary>
    /// Editor script which will define or remove the Ultimate Character Controller compiler symbols so the components are aware of the asset import status.
    /// </summary>
    [InitializeOnLoad]
    public class DefineCompilerSymbols
    {
        private static string s_FirstPersonControllerSymbol = "FIRST_PERSON_CONTROLLER";
        private static string s_ThirdPersonControllerSymbol = "THIRD_PERSON_CONTROLLER";
        private static string s_MultiplayerSymbol = "ULTIMATE_CHARACTER_CONTROLLER_MULTIPLAYER";
        private static string s_VRSymbol = "ULTIMATE_CHARACTER_CONTROLLER_VR";
        private static string s_AgilitySymbol = "ULTIMATE_CHARACTER_CONTROLLER_AGILITY";
        private static string s_ClimbingSymbol = "ULTIMATE_CHARACTER_CONTROLLER_CLIMBING";
        private static string s_UniversalRPSymbol = "ULTIMATE_CHARACTER_CONTROLLER_UNIVERSALRP";
        private static string s_HDRPSymbol = "ULTIMATE_CHARACTER_CONTROLLER_HDRP";
        private static string s_TextMeshProSymbol = "TEXTMESH_PRO_PRESENT";

        /// <summary>
        /// If the specified classes exist then the compiler symbol should be defined, otherwise the symbol should be removed.
        /// </summary>
        static DefineCompilerSymbols()
        {
            // The First Person Controller Combat MovementType will exist when the First Person Controller asset is imported.
            var firstPersonControllerExists = TypeUtility.GetType("Opsive.UltimateCharacterController.FirstPersonController.Character.MovementTypes.Combat") != null;
#if FIRST_PERSON_CONTROLLER
            if (!firstPersonControllerExists) {
                RemoveSymbol(s_FirstPersonControllerSymbol);
            }
#else
            if (firstPersonControllerExists) {
                AddSymbol(s_FirstPersonControllerSymbol);
            }
#endif

            // The Third Person Controller Combat MovementType will exist when the Third Person Controller asset is imported.
            var thirdPersonControllerExists = TypeUtility.GetType("Opsive.UltimateCharacterController.ThirdPersonController.Character.MovementTypes.Combat") != null;
#if THIRD_PERSON_CONTROLLER
            if (!thirdPersonControllerExists) {
                RemoveSymbol(s_ThirdPersonControllerSymbol);
            }
#else
            if (thirdPersonControllerExists) {
                AddSymbol(s_ThirdPersonControllerSymbol);
            }
#endif

            // Network Character Locomotion Handler will exist if the multiplayer add-on is imported.
            var multiplayerExists = TypeUtility.GetType("Opsive.UltimateCharacterController.AddOns.Multiplayer.Character.NetworkCharacterLocomotionHandler") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_MULTIPLAYER
            if (!multiplayerExists) {
                RemoveSymbol(s_MultiplayerSymbol);
            }
#else
            if (multiplayerExists) {
                AddSymbol(s_MultiplayerSymbol);
            }
#endif

            // VR Add-On Inspector will exist if the VR add-on is imported.
            var vrExists = TypeUtility.GetType("Opsive.UltimateCharacterController.AddOns.VR.Editor.VRAddOnInspector") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_VR
            if (!vrExists) {
                RemoveSymbol(s_VRSymbol);
            }
#else
            if (vrExists) {
                AddSymbol(s_VRSymbol);
            }
#endif

            // Agility Add-On Inspector will exist if the Agility Pack is imported.
            var agilityExists = TypeUtility.GetType("Opsive.UltimateCharacterController.AddOns.Agility.Editor.AgilityAddOnInspector") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_AGILITY
            if (!agilityExists) {
                RemoveSymbol(s_AgilitySymbol);
            }
#else
            if (agilityExists) {
                AddSymbol(s_AgilitySymbol);
            }
#endif

            // Climbing Add-On Inspector will exist if the Agility Pack is imported.
            var climbingExists = TypeUtility.GetType("Opsive.UltimateCharacterController.AddOns.Climbing.Editor.ClimbingAddOnInspector") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_CLIMBING
            if (!climbingExists) {
                RemoveSymbol(s_ClimbingSymbol);
            }
#else
            if (climbingExists) {
                AddSymbol(s_ClimbingSymbol);
            }
#endif

            // The URP data will exists when the URP is imported. This assembly definition must be added to the Opsive.UltimateCaracterController.Editor assembly definition.
            var universalrpExists = TypeUtility.GetType("UnityEngine.Rendering.Universal.ForwardRendererData") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_UNIVERSALRP
            if (!universalrpExists) {
                RemoveSymbol(s_UniversalRPSymbol);
            }
#else
            if (universalrpExists) {
                AddSymbol(s_UniversalRPSymbol);
            }
#endif
            var hdrpExists = TypeUtility.GetType("UnityEngine.Rendering.HighDefinition.CustomPassVolume") != null;
#if ULTIMATE_CHARACTER_CONTROLLER_HDRP
            if (!hdrpExists) {
                RemoveSymbol(s_HDRPSymbol);
            }
#else
            if (hdrpExists) {
                AddSymbol(s_HDRPSymbol);
            }
#endif
            // The TMP_Text component will exist when the TextMesh Pro asset is imported.
            var textMeshProExists = TypeUtility.GetType("TMPro.TMP_Text") != null;
#if TEXTMESH_PRO_PRESENT
            if (!textMeshProExists) {
                RemoveSymbol(s_TextMeshProSymbol);
            }
#else
            if (textMeshProExists) {
                AddSymbol(s_TextMeshProSymbol);
            }
#endif
        }

        /// <summary>
        /// Adds the specified symbol to the compiler definitions.
        /// </summary>
        /// <param name="symbol">The symbol to add.</param>
        private static void AddSymbol(string symbol)
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (symbols.Contains(symbol)) {
                return;
            }
            symbols += (";" + symbol);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
        }

        /// <summary>
        /// Remove the specified symbol from the compiler definitions.
        /// </summary>
        /// <param name="symbol">The symbol to remove.</param>
        private static void RemoveSymbol(string symbol)
        {
            var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (!symbols.Contains(symbol)) {
                return;
            }
            if (symbols.Contains(";" + symbol)) {
                symbols = symbols.Replace(";" + symbol, "");
            } else {
                symbols = symbols.Replace(symbol, "");
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols);
        }
    }
}