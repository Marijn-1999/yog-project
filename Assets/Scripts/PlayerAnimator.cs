using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// VERY primitive animator example, now safe for hierarchy changes and missing references.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _anim;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private PlayerController _player; // optional manual assignment

        [Header("Settings")]
        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _landParticles;

        [Header("Audio Clips")]
        [SerializeField] private AudioSource _source;
        // [SerializeField] private AudioClip[] _footsteps;

        private bool _grounded;
        private ParticleSystem.MinMaxGradient _currentGradient;

        private void Awake()
        {
            // Only try to find the player if not manually assigned
            if (_player == null)
                _player = GetComponentInParent<PlayerController>();

            // Only get AudioSource if not assigned
            if (_source == null)
                _source = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_player != null)
            {
                _player.Jumped += OnJumped;
                _player.GroundedChanged += OnGroundedChanged;
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                _player.Jumped -= OnJumped;
                _player.GroundedChanged -= OnGroundedChanged;
            }
        }

        private void Update()
        {
            if (_player == null) return;

            DetectGroundColor();
            HandleSpriteFlip();
            HandleIdleSpeed();
            HandleCharacterTilt();
        }

        private void HandleSpriteFlip()
        {
            if (_sprite == null) return;
            if (_player.FrameInput.x != 0) _sprite.flipX = _player.FrameInput.x < 0;
        }

        private void HandleIdleSpeed()
        {
            if (_anim == null || _player == null) return;
            var inputStrength = Mathf.Abs(_player.FrameInput.x);
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
        }

        private void HandleCharacterTilt()
        {
            if (_anim == null || _player == null) return;
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
            _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            if (_anim != null) _anim.SetTrigger(JumpKey);
            if (_anim != null) _anim.ResetTrigger(GroundedKey);

            if (!_grounded) return;

            if (_jumpParticles != null) { SetColor(_jumpParticles); _jumpParticles.Play(); }
            if (_launchParticles != null) SetColor(_launchParticles);
        }

        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            if (!grounded) return;

            if (_landParticles != null)
            {
                SetColor(_landParticles);
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                _landParticles.Play();
            }

            if (_anim != null) _anim.SetTrigger(GroundedKey);

            // if (_source != null && _footsteps.Length > 0) _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
        }

        private void DetectGroundColor()
        {
            if (_jumpParticles == null && _launchParticles == null && _landParticles == null) return;

            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);
            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;

            var color = r.color;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);

            if (_jumpParticles != null) SetColor(_jumpParticles);
            if (_launchParticles != null) SetColor(_launchParticles);
            if (_landParticles != null) SetColor(_landParticles);
        }

        private void SetColor(ParticleSystem ps)
        {
            if (ps == null) return;
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
    }
}
