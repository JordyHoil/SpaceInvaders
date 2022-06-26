using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class EnemySpawner
    {
        private EnemyPresenter.Factory _factory;
        private EnemyConfig _enemyConfig;
        private AddressablesService _addressables;

        private List<EnemyPresenter> _enemies = new List<EnemyPresenter>();

        public EnemySpawner(EnemyPresenter.Factory factory, EnemyConfig enemyConfig, AddressablesService addressables)
        {
            // Set references
            _factory = factory;
            _enemyConfig = enemyConfig;
            _addressables = addressables;
        }

        public IReadOnlyList<EnemyPresenter> Enemies => _enemies.AsReadOnly();

        public void SpawnAll()
        {
            // Try to get enemy prefab
            var prefab = _addressables.GetGameObject("Enemy");

            // Handle error
            if (prefab == null)
            {
                return;
            }

            // Spawn enemies
            for (int col = 0; col < _enemyConfig.Columns; col++)
            {
                for (int row = 0; row < _enemyConfig.Rows; row++)
                {
                    // Spawn and init enemy
                    EnemyPresenter enemy = _factory.Create(prefab);
                    int type = row <= 1 ? 0 : row <= 3 ? 1 : 2;
                    enemy.Init(type, row, col);
                    _enemies.Add(enemy);
                }
            }
        }

        public void DespawnAll()
        {
            // Despawn enemies
            foreach (var enemy in _enemies)
            {
                Object.Destroy(enemy.gameObject);
            }

            // Clear list
            _enemies.Clear();
        }

        public void Despawn(EnemyPresenter enemy)
        {
            // Handle error
            if (enemy == null)
            {
                return;
            }

            // Despawn enemy
            Object.Destroy(enemy.gameObject);

            // Remove from the list
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
}
