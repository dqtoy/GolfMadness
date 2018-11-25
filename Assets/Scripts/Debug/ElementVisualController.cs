using UnityEngine;

public class ElementVisualController : MonoBehaviour {

	[SerializeField] private MeshRenderer _renderer;
	
	public void OnElementChange(Elemental.Element element)
	{
		if(_renderer == null)
			return;

		Color color = Color.magenta;
		
		switch (element)
		{
			case Elemental.Element.NONE:
				color = Color.white;
				break;
			case Elemental.Element.FIRE:
				color = Color.red;
				break;
			case Elemental.Element.WATER:
				color = Color.blue;
				break;
			case Elemental.Element.ICE:
				color = Color.cyan;
				break;
			case Elemental.Element.AIR:
				color = Color.white;
				break;
			case Elemental.Element.EARTH:
				color = Color.green;
				break;
			default:
				color = Color.magenta;
				Debug.Log("Element not supported: " + element.ToString());
				break;
			
		}

		color.a = _renderer.material.color.a;
		_renderer.material.color = color;

		var trailRenderer =  GetComponent<TrailRenderer>();
		if (trailRenderer != null)
		{
			color.a = trailRenderer.startColor.a;
			trailRenderer.startColor = color;
			color.a = trailRenderer.endColor.a;
			trailRenderer.endColor = color;
		}

	}
}
