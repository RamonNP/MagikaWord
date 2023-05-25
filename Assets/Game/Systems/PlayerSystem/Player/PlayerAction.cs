using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika.Character
{
    public abstract class PlayerAction : MonoBehaviour
    {
        public abstract void ToggleActive(bool active, object data);
    }
}
