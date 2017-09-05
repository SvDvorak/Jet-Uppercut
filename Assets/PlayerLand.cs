using UnityEngine;

public class PlayerLand : MonoBehaviour
{
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _animator.SetBool("InAir", false);
        }
    }
}
