using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BucketManager : MonoBehaviour
{
    [SerializeField] private GameObject _movingItem;
    [SerializeField] private float _movementDuration;
    [SerializeField] private TextMeshPro _targetScoreText;
    [SerializeField] private TextMeshPro _currentScoreText;

    [SerializeField] private int _targetScore;
    private int _score;
    
    public List<BucketScore> buckets;
    
    public enum BucketManagerDirection
    {
        LEFT,
        RIGHT,
        FORWARD
    }

    void Start()
    {
        _score = 0;

        _targetScore = UnityEngine.Random.RandomRange(9,14);
        _targetScoreText.text = _targetScore.ToString();
        foreach (var bucket in buckets)
        {
            bucket.Initialize(this);
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _currentScoreText.text = _score.ToString();

        if (_score == _targetScore)
        {
            ServicesManager.Instance.MissionsManager.ForceWin();
            return;
        }

        if (_score > _targetScore)
        {
            ServicesManager.Instance.MissionsManager.ForceLose();
            return;
        }
        
    }
    
    public void MovePlatform(BucketManagerDirection newDirection)
    {
        _movingItem.transform.DOMove(_movingItem.transform.position + GetDirectionFromBucketManagerDirection(newDirection), _movementDuration);
    }

    Vector3 GetDirectionFromBucketManagerDirection(BucketManagerDirection direction)
    {
        switch (direction)
        {
            case BucketManagerDirection.LEFT:
                return Vector3.left;
                break;
            case BucketManagerDirection.RIGHT:
                return Vector3.right;
                break;
            case BucketManagerDirection.FORWARD:
                return Vector3.forward;
                break;
        }

        return Vector3.zero;
    }
}
