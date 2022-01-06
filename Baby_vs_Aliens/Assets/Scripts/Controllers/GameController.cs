using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baby_vs_Aliens
{
    public class GameController : BaseController, IUpdateableRegular
    {
        #region Fields

        private List<IUpdateableRegular> _updateablesRegular;

        #endregion


        #region ClassLifeCycles

        public GameController(PlayerProfile playerProfile)
        {
            _updateablesRegular = new List<IUpdateableRegular>();

            var inputController = new InputController();
            AddController(inputController);
            _updateablesRegular.Add(inputController);

            var playerController = new PlayerController(inputController);
            AddController(playerController);
            _updateablesRegular.Add(playerController);
        }

        #endregion


        #region IUpdateableRegular

        public void UpdateRegular()
        {
            for (int i = 0; i < _updateablesRegular.Count; i++)
            {
                _updateablesRegular[i].UpdateRegular();
            }
        }

        #endregion


        #region Methods

        protected override void OnDispose()
        {
            _updateablesRegular.Clear();
            base.OnDispose();
        }

        #endregion
    }
}