using TMPro;
using UnityEngine;

public class BucketScore : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshPro _counterText;
    [SerializeField] private BucketManager.BucketManagerDirection _direction;

    public BoxingGlove _boxingGlobe;
    
    private BucketManager _bucketManager;
    
    public void Initialize(BucketManager bucketManager)
    {
        _bucketManager = bucketManager;
        _counterText.text = "+" + _score.ToString();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        _bucketManager.AddScore(_score);

        var playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.Reset();
        }
    }
}
