using UnityEngine;

public class Jump : MonoBehaviour
{
    public float Force;

    private Vector3 _inputDownPosition;
    private Rigidbody2D _rigidbody;

    public void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _inputDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && _inputDownPosition != null)
        {
            GetComponent<GrabEnemy>().Disconnect();

            var inputEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var jumpDirection = (inputEndPosition - _inputDownPosition).normalized;

            _rigidbody.AddForce(jumpDirection*Force, ForceMode2D.Impulse);
        }
    }
}
