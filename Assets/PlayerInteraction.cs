using UnityEngine;

namespace Assets
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float Force;

        private Vector3 _inputDownPosition;
        private Rigidbody2D _rigidbody;
        private GrabEnemy _grabEnemy;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _grabEnemy = GetComponent<GrabEnemy>();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _inputDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0) && _inputDownPosition != null)
            {
                var inputEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var toTarget = inputEndPosition - _inputDownPosition;

                if (toTarget.magnitude > 0.5f)
                {
                    GetComponent<GrabEnemy>().LetGo();

                    var jumpDirection = toTarget.normalized;

                    _rigidbody.AddForce(jumpDirection*Force, ForceMode2D.Impulse);
                }
                else
                {
                    _grabEnemy.PunchFace();
                }
            }
        }
    }
}
