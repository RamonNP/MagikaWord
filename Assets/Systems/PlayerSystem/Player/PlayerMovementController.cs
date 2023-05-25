using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.comp.magika.Character
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
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

            _playerController.FlipSkin(((Vector2)_playerController.Warrior.Root.position + _dir.normalized).x);

            Move(removeTilemapCollision);

        }

        public void SetPosition(Vector2 pos)
        {
            Debug.Log("SetPosition " + pos);
            _auxDir = Vector2.zero;
            _playerController.Warrior.Rb.position = pos;

            LastPosition = pos;
        }
        private void Move(bool removeTilemapCollision)
        {
            Physics2D.IgnoreLayerCollision(Mathf.RoundToInt(Mathf.Log(_playerLayer.value, 2)), Mathf.RoundToInt(Mathf.Log(_tilemapLayer.value, 2)), removeTilemapCollision);

            if (!removeTilemapCollision)
            {
                LastPosition = _playerController.Warrior.Rb.position;
            }

            _playerController.Warrior.Rb.MovePosition(_playerController.Warrior.Rb.position + _auxDir * _speed * Time.fixedDeltaTime);

        }
    }
}
