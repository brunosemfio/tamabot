using UnityEngine;

namespace Tamabot
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(StatsManager))]
    public class MoveToTarget : MonoBehaviour
    {
        #region Private

        private Rigidbody2D _rb;

        private StatsManager _stats;

        private bool _grounded;

        private Target _target;

        #endregion

        #region Inspector

        [SerializeField] private ConfigPreset jumpAngle;

        [SerializeField] private ConfigPreset jumpForce;

        [SerializeField] private LayerMask border;

        #endregion

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _stats = GetComponent<StatsManager>();
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(FindTarget), 0f, Random.Range(3f, 4f));
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(FindTarget));
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
            _target = Target.Rand();
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

            if (_target != null)
            {
                var direction = _target.transform.position - transform.position;

                if (direction.y <= 0f)
                {
                    direction.Normalize();

                    var rotation = Quaternion.Euler(0, 0, -jumpAngle.value * direction.x) * Vector2.up;

                    _rb.AddForce(rotation * jumpForce.value / Mathf.Sqrt(_stats.Size), ForceMode2D.Impulse);
                }
            }
        }
    }
}