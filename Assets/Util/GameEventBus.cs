using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika
{
    public class GameEventBus : Singleton<GameEventBus>
    {
        public Action<InputAction, KeyBehaviour, object> PressedKey;

        public enum InputAction { MoveAxis, Attack, Defense, Roll, Interaction, SwapSet, Run, Inventory, Quest, Chat };
        public enum KeyBehaviour { None, Down, Key, Up };
        public enum AnimState { Idle, Walking, Attack1, Attack2, HoldAttack, TakeDamage, Death, Slipping, Dash, Fall, TeleportIn, TeleportOut, Gather, Defense, NoneLayer0, NoneLayer1, InsideWater, Dependent, Trampoline, Wormhole, Mount }
    }

}
