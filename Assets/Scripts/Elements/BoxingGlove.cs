using DG.Tweening;
using UnityEngine;

public class BoxingGlove : MonoBehaviour
{

    public float _duration = 0.5f;
    public float _maxScale;
    public float positionOffset;
    public float _delayUntilReturn = 1f;

    private float _initialZ;
    private Vector3 _initialScale;

    private Animator _animator;
    
    void Start()
    {
        _initialZ = transform.position.z;
        _initialScale = transform.localScale;
        _animator = GetComponent<Animator>();
    }
    
    public void Punch()
    {
        _animator.SetTrigger("punch");
        
        /*
        Sequence Test_Sequence = DOTween.Sequence();
        Test_Sequence.Append(transform.DOScale (new Vector3 (_maxScale, _maxScale, _maxScale), _duration));
        Test_Sequence.Append(transform.DOMoveZ(_initialZ + positionOffset, _duration*2f));
        Test_Sequence.AppendInterval(_delayUntilReturn);
        Test_Sequence.Append(transform.DOScale (_initialScale, _duration*0.5f));
        Test_Sequence.Append(transform.DOMoveZ(_initialZ, _duration*2f));
        */
    }


    private void OnCollisionEnter(Collision other)
    {
       
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            Punch();
        }
    }
}