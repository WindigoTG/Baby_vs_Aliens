using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Baby_vs_Aliens
{
    public class BaseController : IDisposable
    {
        #region Fields

        private List<BaseController> _baseControllers = new List<BaseController>();
        private List<GameObject> _gameObjects = new List<GameObject>();
        private bool _isDisposed;

        #endregion


        #region IDisposable

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            foreach (var baseController in _baseControllers)
                baseController?.Dispose();

            _baseControllers.Clear();

            foreach (var cachedGameObject in _gameObjects)
                Object.Destroy(cachedGameObject);

            _gameObjects.Clear();

            OnDispose();
        }

        #endregion


        #region Methods

        protected void AddController(BaseController baseController)
        {
            _baseControllers.Add(baseController);
        }

        protected void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        protected virtual void OnDispose()
        {
        }

        #endregion
    }
}