using UnityEngine;

namespace Baby_vs_Aliens
{
    public class RootController : MonoBehaviour
    {
        private MainController _mainController;

        private void Awake()
        {
            var playerProfile = new PlayerProfile();
            playerProfile.CurrentState.Value = GameState.Game;
            _mainController = new MainController(playerProfile);
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