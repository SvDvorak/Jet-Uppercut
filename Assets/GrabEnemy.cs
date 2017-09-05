using System.Collections;
using UnityEngine;

namespace Assets
{
    public class GrabEnemy : MonoBehaviour
    {
        public Transform Offset;

        private bool _isGrabbing;
        private GameObject _grabbedGuy;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Events.instance.AddListener<EnemyKilledEvent>(EnemyKilled);
            _animator = GetComponent<Animator>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && collision.gameObject != _grabbedGuy)
            {
                _grabbedGuy = collision.gameObject;
                _isGrabbing = true;

                _rigidbody.isKinematic = true;
                _rigidbody.velocity = Vector3.zero;

                UpdateAnimator();
            }
        }

        private void UpdateAnimator()
        {
            _animator.SetBool("Grabbed", _isGrabbing);
        }

        public void Update()
        {
            if (_isGrabbing)
            {
                var toGrabOffset = (_grabbedGuy.transform.position - Offset.transform.localPosition) - transform.localPosition;

                transform.position = transform.position + toGrabOffset * 0.2f;
            }
        }

        public void LetGo()
        {
            if (_isGrabbing)
            {
                _isGrabbing = false;
                _rigidbody.isKinematic = false;

                StartCoroutine(ResetLastGrabbed());

                UpdateAnimator();
            }
        }

        public IEnumerator ResetLastGrabbed()
        {
            yield return new WaitForSeconds(1);
            if(!_isGrabbing)
            {
                _grabbedGuy = null;
            }
        }

        public void PunchFace()
        {
            if (_isGrabbing)
            {
                var enemy = _grabbedGuy.GetComponent<Enemy>();
                enemy.Hurt(1);
            }
        }

        private void EnemyKilled(EnemyKilledEvent e)
        {
            if(e.Enemy.gameObject == _grabbedGuy)
            {
                LetGo();
            }
        }
    }
}
