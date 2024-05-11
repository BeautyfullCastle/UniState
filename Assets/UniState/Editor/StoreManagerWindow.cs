using UniState.Core;
using UnityEditor;
using UnityEngine;

namespace UniState
{
    public class StoreManagerWindow : EditorWindow
    {
        [MenuItem("Window/Store Manager")]
        public static void ShowWindow()
        {
            GetWindow<StoreManagerWindow>("Store Manager");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Refresh Stores"))
            {
                Repaint();
            }

            if (StoreManager.Instance != null)
            {
                var stores = StoreManager.Instance.GetAllStores();
                foreach (var store in stores)
                {
                    GUILayout.Label($"Store Type: {store.Key}");
                    GUILayout.Label($"Store Value: {store.Value}");
                }
            }
            else
            {
                GUILayout.Label("StoreManager instance not found.");
            }
        }
    }

}