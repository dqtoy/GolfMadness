using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.Reset();
        }
    }
}
