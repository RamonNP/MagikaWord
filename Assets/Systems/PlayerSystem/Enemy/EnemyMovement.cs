using com.comp.magika.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika.Enemy
{
    public abstract class EnemyMovement :  MonoBehaviour
    {
        [SerializeField] private Warrior _warrior;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _tilemapLayer;
        [SerializeField] private LayerMask _decorationLayer;

        public Vector2 LastPosition { get; private set; }

        private float _speed;
        private Vector2 _auxDir;


        public void SetMoveParams(Vector2 _dir, float speed, bool removeTilemapCollision)
        {
            _auxDir = _dir.normalized;
            _speed = speed;

            FlipSkin(((Vector2)_warrior.Root.position + _dir.normalized).x);

            Move(removeTilemapCollision);

        }

        public void SetPosition(Vector2 pos)
        {
            Debug.Log("SetPosition " + pos);
            _auxDir = Vector2.zero;
            _warrior.Rb.position = pos;

            LastPosition = pos;
        }
        private void Move(bool removeTilemapCollision)
        {
            Physics2D.IgnoreLayerCollision(Mathf.RoundToInt(Mathf.Log(_playerLayer.value, 2)), Mathf.RoundToInt(Mathf.Log(_tilemapLayer.value, 2)), removeTilemapCollision);

            if (!removeTilemapCollision)
            {
                LastPosition = _warrior.Rb.position;
            }

            _warrior.Rb.MovePosition(_warrior.Rb.position + _auxDir * _speed * Time.fixedDeltaTime);

        }

        public void FlipSkin(float posX)
        {

            float distX = posX - _warrior.Root.position.x;
            float target = distX > 0 ? 1 : distX < 0 ? -1 : _warrior.SkinPivot.localScale.x;
            _warrior.SkinPivot.localScale = new Vector3(target, 1, 1);
        }
        
    }
}