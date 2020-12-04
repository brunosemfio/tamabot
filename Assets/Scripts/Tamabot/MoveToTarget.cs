using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tamabot
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(StatsManager))]
    public class MoveToTarget : MonoBehaviour
    {
        #region Animation

        private static readonly int Jump = Animator.StringToHash("Jump");

        #endregion
        
        #region Private

        private Rigidbody2D _rb;

        private Animator _animator;

        private StatsManager _stats;

        private Target _target;
        
        private bool _grounded;

        #endregion

        #region Inspector

        [SerializeField] private ConfigPreset jumpAngle;

        [SerializeField] private ConfigPreset jumpForce;

        [SerializeField] private LayerMask border;

        #endregion

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            _animator = GetComponent<Animator>();

            _stats = GetComponent<StatsManager>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(FindTarget), 0f, Random.Range(2f, 3f));
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (border == (border | (1 << other.gameObject.layer)))
            {
                if (!WallJump(other.GetContact(0).normal))
                {
                    _grounded = true;
                    
                    _animator.SetTrigger(Jump);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (border == (border | (1 << other.gameObject.layer)))
            {
                _grounded = false;
            }
        }

        private void FindTarget()
        {
            _target = Target.Closest(transform.position);
            if (_target == null) _target = Target.Rand();
        }

        private bool WallJump(Vector2 normal)
        {
            if (Vector2.Dot(Vector2.up, normal) <= .9f)
            {
                _rb.AddForce(normal * jumpForce.value, ForceMode2D.Impulse);

                return true;
            }

            return false;
        }

        private void Move()
        {
            if (!_grounded) return;

            _rb.velocity = Vector2.zero;

            if (_target == null) return;
            
            var direction = (_target.transform.position - transform.position).normalized;

            var rotation = Quaternion.Euler(0, 0, -jumpAngle.value * direction.x) * Vector2.up;

            _rb.AddForce(rotation * jumpForce.value, ForceMode2D.Impulse);
        }
    }
}