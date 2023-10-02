using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utils
{
    public class Define
    {
        public enum Scene
        {
            Unknown,
            Login,
            Lobby,
            Game,
        }

        public enum Sound
        { 
            Bgm,
            Effect,
            MaxCount,
        }

        public enum UIEvent
        {
            Click,
            Drag,
            DragBegin,
            DragEnd,
        }

        public enum MouseEvent
        {
            Press,
            Click,
        }

        public enum CameraMode
        {
            QuarterView,
        }
    }
}