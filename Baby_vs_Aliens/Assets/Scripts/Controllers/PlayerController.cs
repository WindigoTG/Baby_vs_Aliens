using UnityEngine;
using Baby_vs_Aliens.Tools;

namespace Baby_vs_Aliens
{
    public class PlayerController : BaseController, IUpdateableRegular
    {
        #region Fields

        private PlayerView _view;
        private PlayerConfig _config;
        private InputController _inputController;
        private IHealthHolder _health;

        private float _currentFireDelay;
        private float _currentRespawnTimer;

        #endregion


        #region ClassLifeCycles

        public PlayerController(InputController inputController)
        {
            _view = GameObject.Instantiate(Resources.Load<PlayerView>(References.PLAYER_PREFAB));
            AddGameObject(_view.gameObject);

            _inputController = inputController;
            _inputController.MovementVector.SubscribeOnChange(MovePlayer);
            _inputController.MousePosition.SubscribeOnChange(RotatePlayer);
            _inputController.AttackInput.SubscribeOnChange(Shoot);

            _config = Resources.Load<PlayerConfig>(References.PLAYER_CONFIG);

            _health = new HealthHolder(_config.MaxHealth);

            _view.Damaged += _health.TakeDamage;

            _health.Death += KillPlayer;

            (_health as HealthHolder).HealthPercentage.SubscribeOnChange(_view.HealthBar.SetBarSize);
        }

        #endregion


        #region Methods

        private void MovePlayer(Vector3 moveVector)
        {
            if (_health.IsDead)
                return;

            _view.SetMoveVector(moveVector.normalized * _config.Speed * Time.deltaTime);

            if (moveVector.x != 0 || moveVector.z != 0)
                _view.SetState(CharacterState.Run);
            else
                _view.SetState(CharacterState.Idle);
        }

        private void RotatePlayer(Vector3 mousePos)
        {
            var lookDirection = mousePos - _view.transform.position;

            Quaternion rotation = _view.transform.rotation;
            rotation.SetLookRotation(lookDirection);
            _view.transform.rotation = rotation;
        }

        private void Shoot (bool isAttacking)
        {
            if (_health.IsDead)
                return;

            if (!isAttacking)
                return;

            if (_currentFireDelay > 0)
                return;

            var  projectile = ServiceLocator.GetService<ObjectPoolManager>().
                GetBulletPool(_config.ProjectileConfig.Prefab).Pop().GetComponent<Projectile>();

            projectile.Init(_view.BulletSpawn, _view.transform.forward, _config.ProjectileConfig);
            _view.ShotParticles.Play();

            _currentFireDelay = _config.ProjectileConfig.FireDelay;
        }

        private void KillPlayer()
        {
            _view.HideCharacter();
            _view.DeathParticles.Play();
            _currentRespawnTimer = _config.RespawnTime;
        }

        private void RespawnPlayer()
        {
            _health.ResetHealth();
            _view.ShowCharacter();
        }

        public void UpdateRegular()
        {
            if (_currentFireDelay > 0)
                _currentFireDelay -= Time.deltaTime;

            if (_health.IsDead)
            {
                if (_currentRespawnTimer > 0)
                    _currentRespawnTimer -= Time.deltaTime;
                else
                    RespawnPlayer();
            }
        }

        protected override void OnDispose()
        {
            _inputController.MovementVector.UnSubscriptibeOnChange(MovePlayer);
            _inputController.MousePosition.UnSubscriptibeOnChange(RotatePlayer);
            _inputController.AttackInput.UnSubscriptibeOnChange(Shoot);
            _view.Damaged -= _health.TakeDamage;
            _health.Death -= KillPlayer;
            base.OnDispose();
        }

        #endregion
    }
}