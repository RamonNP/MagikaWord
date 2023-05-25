using UnityEngine;
using static com.comp.magika.GameEventBus;

namespace com.comp.magika.Character
{
    public class ActionWalk : PlayerAction
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerMovementController _playerMovementController;

        [SerializeField] private float _baseWalkSpeed;
        [SerializeField] private float _walkAnimSpeed;

        [SerializeField] private float _baseRunSpeed;
        [SerializeField] private float _runAnimSpeed;

        private bool _firstPassWalk = false;
        private bool _firstPassRun;

        private bool _isRunning;
        private bool _doWalk;

        public void OnEnable()
        {
            GameEventBus.Instance.PressedKey += InputReceived;
            Debug.Log("Registrado");
            //_playerController.Warrior.AnimEventTrigger.Execute += AnimFrameEvent;
        }

        private void OnDisable()
        {
            GameEventBus.Instance.PressedKey -= InputReceived;
            //_playerController.Warrior.AnimEventTrigger.Execute -= AnimFrameEvent;

            ResetValues();
        }

        public override void ToggleActive(bool active, object data)
        {
            if (!active)
            {
                ResetValues();
            }

            this.enabled = active;
        }
        public void InputReceived(InputAction button, KeyBehaviour behaviour, object data)
        {
            Debug.Log("INPUT");
            switch (button)
            {
                case InputAction.MoveAxis:
                    DoWalk(behaviour, (Vector2)data);
                    break;
                case InputAction.Run:
                    DoRun(behaviour);
                    break;

            }
        }

        private void DoRun(KeyBehaviour behaviour)
        {
            if (behaviour == KeyBehaviour.Key)
            {
                   
            }

            if (behaviour == KeyBehaviour.Up)
            {
                
                _firstPassRun = false;
                _isRunning = false;
                //_playerController.Warrior.Animator.SetFloat("WalkSpeed", _walkAnimSpeed);
            }

            if (behaviour == KeyBehaviour.Key && !_firstPassRun && _doWalk)
            {
                
                _firstPassRun = true;
                _isRunning = true;
                //_playerController.Warrior.Animator.SetFloat("WalkSpeed", _runAnimSpeed);
            }
        }

        private void DoWalk(KeyBehaviour behaviour, Vector2 newDir)
        {
            if (!_firstPassWalk && newDir != Vector2.zero)
            {
                _doWalk = true;
                _firstPassWalk = true;

                _playerController.Warrior.SetAnimation(AnimState.Walking, false);
                //_runParticle.Play();

                _playerController.Warrior.Animator.SetFloat("WalkSpeed", _isRunning ? _runAnimSpeed : _walkAnimSpeed);
            }


            if (newDir == Vector2.zero && _doWalk)
            {
                _playerController.Warrior.SetAnimation(AnimState.Idle, false);
                ResetValues();
            }
            else
            {
                _playerMovementController.SetMoveParams(newDir, _playerController.Warrior.MoveSpeed + (_isRunning ? BaseRunSpeed : BaseWalkSpeed), false);
            }


        }
        private void ResetValues()
        {
            _isRunning = false;
            _doWalk = false;
            _firstPassWalk = false;
            _firstPassRun = false;
            //_runParticle.Stop();
        }

        public float BaseRunSpeed
        {
            get
            {
                return _baseRunSpeed;
            }

            set
            {
                _baseRunSpeed = value;
            }
        }

        public float BaseWalkSpeed
        {
            get
            {
                return _baseWalkSpeed;
            }

            set
            {
                _baseWalkSpeed = value;
            }
        }
    }
}
