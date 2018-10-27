using UnityEngine;

public class ElementVisualController : MonoBehaviour {

	[SerializeField] private MeshRenderer _renderer;
	public void OnElementChange(Elemental.Element element)
	{
		switch (element)
		{
			case Elemental.Element.NONE:
				_renderer.material.color = Color.white;
				break;
			case Elemental.Element.FIRE:
				_renderer.material.color = Color.red;
				break;
			case Elemental.Element.WATER:
				_renderer.material.color = Color.blue;
				break;
			case Elemental.Element.ICE:
				_renderer.material.color = Color.cyan;
				break;
			case Elemental.Element.AIR:
				_renderer.material.color = Color.white;
				break;
			case Elemental.Element.EARTH:
				_renderer.material.color = Color.green;
				break;
			default:
				_renderer.material.color = Color.magenta;
				Debug.Log("Element not supported: " + element.ToString());
				break;
			
		}
	}
}
