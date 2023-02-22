/// ---------------------------------------------
/// Ultimate Character Controller
/// Copyright (c) Opsive. All Rights Reserved.
/// https://www.opsive.com
/// ---------------------------------------------

namespace Opsive.UltimateCharacterController.Demo.References
{
    using UnityEngine;

    /// <summary>
    /// Helper class which references the objects.
    /// </summary>
    [CreateAssetMenu(fileName = "ScriptableObject", menuName = "Opsive/Sc", order = 1)]
    public class ScriptableObjectReferences : ScriptableObject
    {
        [Tooltip("A reference to the Object References that should be checked.")]
        [SerializeField] protected ObjectReferences[] m_ObjectReferences;

        public ObjectReferences[] ObjectReferences { get { return m_ObjectReferences; } set { m_ObjectReferences = value; } }
    }
}