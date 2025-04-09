using System;
using _00.Work.MOON._01.Script.SO.Entity;
using _00.Work.MOON._01.Script.SO.Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _00.Work.MOON._01.Script.Entity.Player
{
    public class PlayerMovement : EntityMovement
    {
        private PlayerInputSO _playerInput;
        private PlayerMoveStatInfoSO _statInfo;

        protected override void Awake()
        {
            GetMoveStatsInfo();
            base.Awake();
        }

        private void GetMoveStatsInfo()
        {
            _statInfo = statInfo as PlayerMoveStatInfoSO;
            _playerInput = _statInfo.PlayerInput;
        }

        protected void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                ChangeStat(EntityMoveStatType.Normal);
            }
        }

        protected override void Move()
        {
            rigidbody.AddForce(transform.forward * (_playerInput.MovementKey.y * _moveSpeed), ForceMode.Force);
        }
    }
}
