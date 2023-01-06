using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor.Map
{
    public enum EditMode
    {
        SelectMode = 0,
        EditMode = 1,
        DeleteMode = 2
    }
    public class BlockDrawer : MonoBehaviour
    {
        [SerializeField] private GameObject block;
        
        public EditMode editMode;
        public IEditModeCommand Command;
        
        public bool isDrawing;

        [Button]
        public void DeleteMode()
        {
            editMode = Map.EditMode.DeleteMode;
        }
        
        [Button]        
        public void EditMode()
        {
            editMode = Map.EditMode.EditMode;
        }

        [Button]
        public void SelectMode()
        {
            editMode = Map.EditMode.SelectMode;
        }

        public void Update()
        {
            
        }
    }
}