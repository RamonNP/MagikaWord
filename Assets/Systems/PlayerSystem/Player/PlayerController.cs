using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika.Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Warrior _warrior;
        private Coroutine _skinRotationRoutine;


        public Warrior Warrior { get => _warrior; set => _warrior = value; }

        public void FlipSkin(float posX)
        {

            float distX = posX - Warrior.Root.position.x;
            float target = distX > 0 ? 1 : distX < 0 ? -1 : Warrior.SkinPivot.localScale.x;
            Warrior.SkinPivot.localScale = new Vector3(target, 1, 1);


            return;
            if (target != 0)
            {
                if (_skinRotationRoutine != null)
                {
                    StopCoroutine(_skinRotationRoutine);
                }
                _skinRotationRoutine = StartCoroutine(FlipSkinCoroutine(target));
            }
        }

        private IEnumerator FlipSkinCoroutine(float target)
        {
            float minKeyRotation = -0.5f;
            float maxKeyRotation = 0.5f;

            Vector3 targetScale = new Vector3(target, 1, 1);


            while (true)
            {
                if (target != Warrior.SkinPivot.localScale.x)
                {
                    if (target == -1)
                    {
                        if (Warrior.SkinPivot.localScale.x < maxKeyRotation && Warrior.SkinPivot.localScale.x > minKeyRotation)
                            Warrior.SkinPivot.localScale = new Vector3(minKeyRotation, 1, 1);
                    }
                    if (target == 1)
                    {
                        if (Warrior.SkinPivot.localScale.x > minKeyRotation && Warrior.SkinPivot.localScale.x < maxKeyRotation)
                            Warrior.SkinPivot.localScale = new Vector3(maxKeyRotation, 1, 1);
                    }
                    Warrior.SkinPivot.localScale = Vector3.MoveTowards(Warrior.SkinPivot.localScale, targetScale, 10 * Time.deltaTime);
                }
                yield return null;
            }
        }
    }
}