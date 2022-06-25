using UnityEngine;

namespace SpaceInvaders
{
    public class PlayerSpawner
    {
        private PlayerController.Factory _factory;
        private PlayerController _player;
        private AddressablesService _addressables;

        public PlayerSpawner(PlayerController.Factory factory, AddressablesService addressables)
        {
            _factory = factory;
            _addressables = addressables;
        }

        public void Spawn()
        {
            // Try to get player prefab
            var prefab = _addressables.GetGameObject("Player");

            // Handle error
            if (prefab == null)
            {
                return;
            }

            // Spawn player
            _player = _factory.Create(prefab);
        }

        public void Despawn()
        {
            // The player hasn't been spawned yet
            if (_player == null)
            {
                return;
            }

            // Despawn player
            Object.Destroy(_player.gameObject);
            _player = null;
        }
    }
}