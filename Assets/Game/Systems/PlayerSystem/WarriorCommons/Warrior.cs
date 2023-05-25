using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika.Character
{
    [System.Serializable]
    public class Warrior : Character
    {
        [SerializeField] private LayerMask _monsterLayer;

        [SerializeField] private Transform _weaponArmIK;

        [SerializeField] private Transform _weaponPivot;

        [SerializeField] private Transform _shadowPlane;

        public LayerMask MonsterLayer { get => _monsterLayer; set => _monsterLayer = value; }
        public Transform WeaponArmIK { get => _weaponArmIK; set => _weaponArmIK = value; }
        public Transform WeaponPivot { get => _weaponPivot; set => _weaponPivot = value; }
        public Transform ShadowPlane { get => _shadowPlane; set => _shadowPlane = value; }

        public void ResetArmIK()
        {
            WeaponArmIK.eulerAngles = Vector3.zero;
        }
        /*private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Root.position, ViewRadius);
            Gizmos.DrawLine(Root.position, MovePath.Count > 0? MovePath[0] : Root.position);
        }*/

    }
}
