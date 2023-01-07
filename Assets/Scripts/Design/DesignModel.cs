using System;
using ModestTree;

namespace Design
{
    public enum DesignScreens
    {
        None = 0,
        EditorMenu = 1,
        MapEditor = 2
    }
    
    public class DesignModel
    {
        public bool IsDesignMode;
        public DesignScreens CurrentScreen;

        public Action<bool> Subscribers = (bool state) => {};

        public void SetDesignMode()
        {
            if (IsDesignMode)
            {
                return;
            }
            IsDesignMode = true;
            Subscribers.Invoke(IsDesignMode);
        }

        public void SetGameMode()
        {
            if (!IsDesignMode)
            {
                return;
            }
            IsDesignMode = false;
            Subscribers.Invoke(IsDesignMode);
        }
    }
}