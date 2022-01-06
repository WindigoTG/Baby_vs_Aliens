using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class RootController : MonoBehaviour
    {
        private MainController _mainController;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            var playerProfile = new PlayerProfile();
            playerProfile.CurrentState.Value = GameState.Game;
            _mainController = new MainController(playerProfile);

            ServiceLocator.AddService(new ObjectPoolManager());
        }

        private void Update()
        {
            _mainController.UpdateRegular();
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}