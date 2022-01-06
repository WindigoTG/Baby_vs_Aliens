using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class MainController : BaseController, IUpdateableRegular
    {
        PlayerProfile _playerProfile;
        GameController _gameController;

        public MainController(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            OnChangeGameState(_playerProfile.CurrentState.Value);
            playerProfile.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Menu:
                    _gameController.Dispose();
                    break;

                case GameState.Game:
                    _gameController = new GameController(_playerProfile);
                    break;

                default:
                    _gameController.Dispose();
                    break;
            }
        }

        protected override void OnDispose()
        {
            _playerProfile.CurrentState.UnSubscriptibeOnChange(OnChangeGameState);
            base.OnDispose();
        }

        public void UpdateRegular()
        {
            _gameController?.UpdateRegular();
        }
    }
}