using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ゲームオブジェクトをクリックした際に、ゲームオブジェクトの情報を取得するためのクラス
/// </summary>
public class ObjectClicker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log($"{ eventData.pointerEnter.gameObject.name }");
    }
}