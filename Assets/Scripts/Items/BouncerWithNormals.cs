using UnityEngine;

public class BouncerWithNormals : MonoBehaviour
{
    [SerializeField] private float _reboundPower;
    
    private void OnCollisionEnter(Collision other)
    {
        var contact = other.contacts[0];

        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            
            rigidbody.AddForce(-contact.normal * _reboundPower, ForceMode.Force);
        }
    }
}
