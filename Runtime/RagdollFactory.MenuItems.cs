#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Gann4Games.RagdollFactory
{
    public partial class RagdollFactory
    {
        [MenuItem("Gann4Games/Tools/Ragdoll Factory", false, priority = 1)]
        private static void LoadRagdollFactory(MenuCommand menuCommand)
        {
            GameObject obj = Selection.activeObject as GameObject;
            if (obj != null)
            {
                obj.AddComponent<RagdollFactory>();
                Undo.RegisterCreatedObjectUndo(obj, "Created Collision Builder");
            }
        }

        [MenuItem("Gann4Games/Tools/Ragdoll Factory", true, priority = 1)]
        private static bool LoadRagdollFactoryValidation(MenuCommand menuCommand)
        {
            Object selectedObject = Selection.activeObject;
            if(!selectedObject) return false;
            
            bool isGameObject = selectedObject is GameObject;
            bool hasRagdollFactory = ((GameObject)selectedObject).GetComponent<RagdollFactory>();
            return isGameObject && !hasRagdollFactory;
        }
    }
}
#endif