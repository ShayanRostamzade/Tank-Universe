using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public Image background;
	private Image joystick;
	private Vector3 position;
	private float R;
	private float teta;

	private void Awake()
	{
		R = 6;
		joystick = background.transform.GetChild(0).GetComponent<Image>();
		background.gameObject.SetActive(false);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		background.gameObject.SetActive(true);

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y,
																 100));

		print(Vector3.Distance(eventData.position, Camera.main.transform.position));

		background.GetComponent<Image>().rectTransform.position = pos;
		//new Vector3(pos.x, pos.y, 0);
		//-Camera.main.transform.position.z);

		//OnDrag(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{

		Vector2 pos;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.GetComponent<Image>().rectTransform,
																   eventData.position,
																   eventData.pressEventCamera,
																   out pos))
		{
			pos.x = pos.x / background.rectTransform.sizeDelta.x;
			pos.y = pos.y / background.rectTransform.sizeDelta.y;
			position = new Vector3(pos.x, 0, pos.y);

			position = position.magnitude > 1 ? position.normalized : position;

			joystick.rectTransform.anchoredPosition = new Vector3(position.x * (background.rectTransform.sizeDelta.x / 2),
																  position.z * (background.rectTransform.sizeDelta.y / 2));
		}



	}

	public void OnPointerUp(PointerEventData eventData)
	{
		position = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
		background.gameObject.SetActive(false);
	}

	public Vector3 Coordinates()
	{
		return position;
	}


}
