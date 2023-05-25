using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.comp.magika.GameEventBus;

namespace com.comp.magika.Character
{
    [System.Serializable]
    public class Character
    {
        [SerializeField] private Transform _root;

        [SerializeField] private List<Vector3> movePath = new List<Vector3>();
        [SerializeField] private float _moveSpeed = 1;

        [SerializeField] private Transform _skinPivot;
        [SerializeField] private Transform _skinRoot;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _baseCollider;
        [SerializeField] private Rigidbody2D _rb;

        //[SerializeField] private AnimEventTrigger _animEventTrigger;
        [SerializeField] private AudioSource _audioSource;
        //[SerializeField] private AudioControllerObjct _audioController;

        private SpriteRenderer _defaultRenderer;
        [SerializeField] private Material _defaultMaterial;

        /*[System.Serializable]
        public struct AnimInfo
        {
            public AnimState State;
            public AnimationClip Clip;
        }
        [SerializeField] private List<AnimInfo> _animInfo;

        [System.Serializable]
        public struct WeaponTypeAnimOverride
        {
            //public Subtype Type;
            public AnimatorOverrideController weaponAnimator;
        }
        [SerializeField] private List<WeaponTypeAnimOverride> _weaponAnimOverride;*/

        public event System.Action<string> OnPhotonAnimState;

        //Call this function on 'Awake' from Controller class
        public void GetAllRenderers()
        {
            DefaultRenderer = Root.gameObject.AddComponent<SpriteRenderer>();
            DefaultRenderer.material = DefaultMaterial;

            SpriteRenderer[] rendererList = SkinPivot.GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < rendererList.Length; i++)
            {
                rendererList[i].material = DefaultRenderer.material;
            }
        }

        /*public void FlipSkin(Vector3 targetPos)
        {
            float distX  = (targetPos - Root.position).x;
            float scaleX = distX > 0 ? 1 : distX < 0 ? -1 : SkinPivot.localScale.x;
            SkinPivot.localScale = new Vector3(scaleX, 1, 1);

        }

        /*public void UpdateAnimByWeaponType(Subtype type)
        {
            WeaponTypeAnimOverride weapon = _weaponAnimOverride.Find(t => t.Type == type);
            if (weapon.Equals(new WeaponTypeAnimOverride()))
                return;

            SetOverrideAnimator(weapon.weaponAnimator);
        }*/

        private void SetOverrideAnimator(AnimatorOverrideController overrider)
        {
            _animator.runtimeAnimatorController = overrider;
        }
        /*
        public void SetAnimation(AnimState animState, bool stopAllLayers = true)
        {
            if (stopAllLayers)
            {
                //Animator.CrossFade(AnimState.NoneLayer0.ToString(),0);
                //Animator.CrossFade(AnimState.NoneLayer1.ToString(),0);

            }

            Animator.Play(animState.ToString(), 0, normalizedTime: 0);
            OnPhotonAnimState?.Invoke(animState.ToString());
        }*/

        public void SetWeaponAnimation(string stateName)
        {
            //Animator.SetTrigger(animState.ToString());
            Animator.Play(stateName);
        }

        public void PlayAudio(AudioClip clip, float volume, float pitch = 1)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clip, volume + Random.Range(-0.05f, 0.05f));
        }

        /*public void PlayAudio(Category categorie)
        {
            PlayAudio(_audioController.GetClip(categorie));
        }
        public void PlayAudio(Clip clip)
        {
            if (clip.Equals(new Clip()))
                return;

            _audioSource.PlayOneShot(clip.AudioClip, clip.Volume + Random.Range(-0.05f, 0.05f));
        }*/

        //
        public Rigidbody2D Rb { get => _rb; set => _rb = value; }

        public Transform Root
        {
            get
            {
                return _root;
            }

            set
            {
                _root = value;
            }
        }

        public List<Vector3> MovePath
        {
            get
            {
                return movePath;
            }

            set
            {
                movePath = value;
            }
        }

        public float MoveSpeed
        {
            get
            {
                return _moveSpeed;
            }

            set
            {
                _moveSpeed = value;
            }
        }

        public Animator Animator
        {
            get
            {
                return _animator;
            }

            set
            {
                _animator = value;
            }
        }

        public Transform SkinPivot
        {
            get
            {
                return _skinPivot;
            }

            set
            {
                _skinPivot = value;
            }
        }

        /*public List<AnimInfo> AnimationEvents {
            get {
                return _animInfo;
            }

            set {
                _animInfo = value;
            }
        }*/

        //public AnimEventTrigger AnimEventTrigger { get => _animEventTrigger; set => _animEventTrigger = value; }

        public Collider2D BaseCollider
        {
            get
            {
                return _baseCollider;
            }

            set
            {
                _baseCollider = value;
            }
        }

        public SpriteRenderer DefaultRenderer
        {
            get
            {
                return _defaultRenderer;
            }

            set
            {
                _defaultRenderer = value;
            }
        }

        public Material DefaultMaterial
        {
            get
            {
                return _defaultMaterial;
            }

            set
            {
                _defaultMaterial = value;
            }
        }

        public Transform SkinRoot
        {
            get
            {
                return _skinRoot;
            }

            set
            {
                _skinRoot = value;
            }
        }

        /*public AudioControllerObjct AudioController {
            get {
                return _audioController;
            }

            set {
                _audioController = value;
            }
        }*/

        public void SetAnimation(AnimState animState, bool stopAllLayers = true)
        {
            if (stopAllLayers)
            {
                //Animator.CrossFade(AnimState.NoneLayer0.ToString(),0);
                //Animator.CrossFade(AnimState.NoneLayer1.ToString(),0);

            }

            Animator.Play(animState.ToString(), 0, normalizedTime: 0);
            OnPhotonAnimState?.Invoke(animState.ToString());
        }
    }
}

