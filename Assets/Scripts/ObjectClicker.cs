using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClicker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log($"{ eventData.pointerEnter.gameObject.name }");
    }
}