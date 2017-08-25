using System.Collections;
using UnityEngine;

public class GrabEnemy : MonoBehaviour
{
    public Transform Offset;

    private FixedJoint2D _grabJoint;
    private bool _isGrabbing;
    private Transform _lastGrabbed;
    
    public void Start()
    {
        _grabJoint = GetComponent<FixedJoint2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.transform != _lastGrabbed)
        {
            _grabJoint.enabled = true;
            _grabJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();

            transform.position = collision.transform.position - Offset.transform.localPosition;
            _lastGrabbed = collision.transform;
            _isGrabbing = true;
        }
    }

    public void Disconnect()
    {
        if (_isGrabbing)
        {
            _isGrabbing = false;

            _grabJoint.enabled = false;
            _grabJoint.connectedBody = null;

            StartCoroutine(ResetLastGrabbed());
        }
    }

    public IEnumerator ResetLastGrabbed()
    {
        yield return new WaitForSeconds(1);
        if(!_isGrabbing)
        {
            _lastGrabbed = null;
        }
    }
}
