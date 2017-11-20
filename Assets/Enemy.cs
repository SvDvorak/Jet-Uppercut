﻿using DG.Tweening;
using UnityEngine;

namespace Assets
{
    public class Enemy : MonoBehaviour
    {
        public float HoverMove = 0.2f;
        public float CurrentHealth;
	    public Transform Weapon;

        private const float PunchRotationAmount = 45;
        private const float HoverLoopTime = 2f;

        private Vector3 _initialScale;
        private Vector3 _moveTarget;

        public void Start()
        {
            _moveTarget = transform.position;
            _initialScale = transform.localScale;
	        _player = GameObject.Find("Player");
        }

        private float _timePlayed = 0;
        private float _totalTime = 0.6f;
	    private GameObject _player;

	    public void Update()
        {
            var hoverProgress = Time.time % HoverLoopTime / HoverLoopTime;
            transform.position = (_moveTarget + Vector3.up * HoverMove * Mathf.Sin(hoverProgress * 2 * Mathf.PI));

	        AimAt(_player.transform.position);
	        //if (_timePlayed < _totalTime)
	        //{
	        //    var progress = _timePlayed / _totalTime;
	        //    transform.localScale = _initialScale * (1 - progress) + _initialScale * 1.15f * progress;
	        //    _timePlayed += Time.deltaTime;
	        //}
	        //else
	        //{
	        //    _timePlayed = 0;
	        //}
        }

	    private void AimAt(Vector3 position)
	    {
		    var toTarget = transform.position - position;
			var s = Weapon.localScale;

		    var onRight = toTarget.x > 0;
		    if (onRight)
		    {
			    Weapon.right = toTarget;
			    Weapon.localScale = new Vector3(Mathf.Abs(s.x), s.y, s.z);
		    }
		    else
		    {
			    Weapon.right = -toTarget;
			    Weapon.localScale = new Vector3(-Mathf.Abs(s.x), s.y, s.z);
		    }
	    }

	    public void Hurt(int damage)
        {
            CurrentHealth -= damage;
            transform.DOPunchScale(_initialScale * 0.15f, 0.2f);
            var punchRotation = (CurrentHealth % 2) * PunchRotationAmount - PunchRotationAmount / 2;
            transform.DOPunchRotation(Vector3.forward * punchRotation, 0.2f);

            if (CurrentHealth < 0)
            {
                Events.instance.Raise(new EnemyKilledEvent(this));
                Destroy(gameObject);
            }
        }
    }

    public class EnemyKilledEvent : GameEvent
    {
        public Enemy Enemy { get; private set; }

        public EnemyKilledEvent(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}
