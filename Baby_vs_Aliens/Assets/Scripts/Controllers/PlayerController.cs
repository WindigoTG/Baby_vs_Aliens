using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class PlayerController : BaseController
    {
        private PlayerView _view;
        private InputController _inputController;

        public PlayerController(InputController inputController)
        {
            _view = GameObject.Instantiate(Resources.Load<PlayerView>(References.PLAYER_PREFAB));
            AddGameObject(_view.gameObject);

            _inputController = inputController;
            _inputController.MovementVector.SubscribeOnChange(Move);
            _inputController.MousePosition.SubscribeOnChange(Rotate);
        }

        private void Move(Vector3 moveVector)
        {
            _view.SetMoveVector(moveVector.normalized * 3 * Time.deltaTime);
        }

        private void Rotate(Vector3 mousePos)
        {
            var lookDirection = mousePos - _view.transform.position;

            Quaternion rotation = _view.transform.rotation;
            rotation.SetLookRotation(lookDirection);
            _view.transform.rotation = rotation;
        }

        protected override void OnDispose()
        {
            _inputController.MovementVector.UnSubscriptibeOnChange(Move);
            _inputController.MousePosition.UnSubscriptibeOnChange(Rotate);
            base.OnDispose();
        }
    }
}