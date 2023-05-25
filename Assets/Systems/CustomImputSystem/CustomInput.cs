using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static com.comp.magika.GameEventBus;

namespace com.comp.magika.Joystick
{
    public class CustomInput : Singleton<CustomInput>
    {
        [System.Serializable]
        public class Activation
        {
            public InputAction action;
            public bool IsActive;
            [HideInInspector] public UnityEvent execute;
        }

        [SerializeField] private List<Activation> _activation;

        private Vector2 _axis;

        private KeyBehaviour ReturnKeyBehaviour(string key)
        {
            if (Input.GetButtonDown(key)) 
                return KeyBehaviour.Down;
            else if (Input.GetButton(key)) 
                return KeyBehaviour.Key;
            else if (Input.GetButtonUp(key))
                return KeyBehaviour.Up;

            return KeyBehaviour.None;
        }

        private KeyBehaviour ReturnAxisBehaviour(InputAction key)
        {
            if (key == InputAction.MoveAxis)
            {
                _axis.x = Input.GetAxisRaw("Horizontal");
                _axis.y = Input.GetAxisRaw("Vertical");

                if(_axis.magnitude > 0)
                    return KeyBehaviour.Key;
                else return KeyBehaviour.None;

            }
            if (key == InputAction.Run)
            {
                _axis.x = Input.GetAxisRaw(key.ToString());

                if (_axis.magnitude > 0)
                    return KeyBehaviour.Key;
                else return KeyBehaviour.Up;
            }

            return KeyBehaviour.None;
        }

        protected override void Awake()
        {
            Debug.Log("Awake");
            base.Awake();
            
            foreach(Activation item in _activation)
            {
                if (!item.IsActive)
                    continue;

                item.execute.AddListener(() =>
                {
                    object data = null;

                    if(item.action == InputAction.MoveAxis)
                    {
                        data = GetAxis(InputAction.MoveAxis);
                        GameEventBus.Instance.PressedKey?.Invoke(item.action, ReturnAxisBehaviour(InputAction.MoveAxis), data);
                        return;
                    }


                    KeyBehaviour kb = ReturnKeyBehaviour(item.action.ToString());

                    if (kb != KeyBehaviour.None)
                    {

                        //Debug.Log(item.action.ToString());
                        GameEventBus.Instance.PressedKey?.Invoke(item.action, kb, data);
                    }

                });
            }
        }

        private void Update()
        {
            foreach (var input in _activation)
            {
                if (input.IsActive)
                {
                    Debug.Log(input.action);
                    input.execute.Invoke();
                }
            }
        }

        private Vector2 GetAxis(InputAction input)
        {
            if (input == InputAction.MoveAxis)
            {
                _axis.x = Input.GetAxisRaw("Horizontal");
                _axis.y = Input.GetAxisRaw("Vertical");
            }
            if (input == InputAction.Run)
            {
                _axis.x = Input.GetAxisRaw(input.ToString());
            }


            return _axis;
        }
        public void ToggleMoveKeys(bool isAcitive)
        {
            for(int n = 0; n < _activation.Count; n++)
            {
                _activation[n].IsActive = _activation[n].action != InputAction.Chat? isAcitive : true;
            }
        }
    }

}
