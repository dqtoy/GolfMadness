using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DebugBallController : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 1f;
    private Rigidbody _rigidbody;

    private Rigidbody MyRigidbody
    {
        get
        {
            if (_rigidbody == null)
            {
                _rigidbody = gameObject.GetComponent<Rigidbody>();
            }

            return _rigidbody;
        }
    }


    void Update()
    {
        ApplyForceIfKeyDown(KeyCode.W, new Vector3(0, 0, _forceAmount));
        ApplyForceIfKeyDown(KeyCode.S, new Vector3(0, 0, -_forceAmount));
        ApplyForceIfKeyDown(KeyCode.A, new Vector3(-_forceAmount, 0, 0));
        ApplyForceIfKeyDown(KeyCode.D, new Vector3(_forceAmount, 0, 0));
    }

    void ApplyForceIfKeyDown(KeyCode code, Vector3 force)
    {
        if (Input.GetKeyDown(code))
        {
            MyRigidbody.AddForce(force);
        }
    }
}