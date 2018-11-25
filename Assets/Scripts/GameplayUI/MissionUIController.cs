using TMPro;
using UnityEngine;

public class MissionUIController : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _title;
   [SerializeField] private GameObject _star;
   
   public void Init(bool available, string text)
   {
      _title.text = text;
      gameObject.SetActive(available);
      UpdateState(false);
   }

   public void UpdateState(bool completed)
   {
      if (_star != null)
      {
         _star.SetActive(completed);
      }
   }
}