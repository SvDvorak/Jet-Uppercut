﻿using UnityEngine;

namespace Assets
{
    public class PlayerInteraction : MonoBehaviour
    {
        public float Force;

        private Vector3 _inputDownPosition;
        private Rigidbody2D _rigidbody;
        private GrabEnemy _grabEnemy;
        private Animator _animator;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _grabEnemy = GetComponent<GrabEnemy>();
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            var input = Input.touchSupported ? GetTouchInput() : GetMouseInput();

            if (input.Down)
            {
                _inputDownPosition = input.Position;
            }

            if (input.Up)
            {
                var inputEndPosition = input.Position;
                var toTarget = inputEndPosition - _inputDownPosition;

                if (toTarget.magnitude > 0.5f)
                {
                    GetComponent<GrabEnemy>().LetGo();

                    var jumpDirection = toTarget.normalized;

                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.AddForce(jumpDirection*Force, ForceMode2D.Impulse);

                    _animator.SetBool("InAir", true);
                }
                else
                {
                    _grabEnemy.PunchFace();
                }
            }

            var goingLeft = _rigidbody.velocity.x < 0;
            var s = transform.localScale;
            transform.localScale = new Vector3(goingLeft ? -Mathf.Abs(s.x) : Mathf.Abs(s.x), s.y, s.z);
        }

        private static InputState GetMouseInput()
        {
            return new InputState
            {
                Down = Input.GetMouseButtonDown(0),
                Up = Input.GetMouseButtonUp(0),
                Position = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            };
        }

        private static InputState GetTouchInput()
        {
            var input = new InputState();
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                input.Down = touch.phase == TouchPhase.Began;
                input.Up = touch.phase == TouchPhase.Ended;
                input.Position = Camera.main.ScreenToWorldPoint(touch.position);
            }

            return input;
        }
    }

    public class InputState
    {
        public bool Down { get; set; }
        public bool Up { get; set; }
        public Vector3 Position { get; set; }
    }
}
