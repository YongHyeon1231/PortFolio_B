using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utils
{
    public class Define
    {
        public enum WorldObject
        {
            Unknown,
            Player,
            Monster,
        }

        public enum State
        {
        Die,
        Moving,
        Idle,
        Skill,
        Channelling,
        Jumping,
        Falling,
        }

        public enum Layer
        {
            Monster = 8,
            Ground = 9,
            Block = 10,
        }

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
            PointerDown,
            PointerUp,
            Click,
        }

        public enum CameraMode
        {
            QuarterView,
        }
    }
}