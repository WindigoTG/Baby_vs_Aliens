using UnityEngine;
using System.Collections.Generic;

namespace Baby_vs_Aliens.Tools
{
    public sealed class ObjectPoolManager
    {
        #region Fields

        private Dictionary<EnemyType, ObjectPool> _enemyPool = new Dictionary<EnemyType, ObjectPool>();
        private Dictionary<GameObject, ObjectPool> _bulletsPool = new Dictionary<GameObject, ObjectPool>();

        private Transform _enemyParent;
        private Transform _bulletsParent;

        #endregion


        #region ClassLifeCCycles

        public ObjectPoolManager()
        {

            _enemyParent = new GameObject(References.ENEMIES_PARENT_OBJECT).transform;
            _bulletsParent = new GameObject(References.BULLETS_PARENT_OBJECT).transform;
        }

        public ObjectPool GetEnemyPool(EnemyType type)
        {
            //TODO

            return _enemyPool[type];
        }

        public ObjectPool GetBulletPool(GameObject prefab)
        {
            if (!_bulletsPool.ContainsKey(prefab))
                _bulletsPool.Add(prefab, new ObjectPool(prefab, _bulletsParent));

            return _bulletsPool[prefab];
        }

        #endregion
    }
}