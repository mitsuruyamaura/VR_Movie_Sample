using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �Q�[���I�u�W�F�N�g���N���b�N�����ۂɁA�Q�[���I�u�W�F�N�g�̏����擾���邽�߂̃N���X
/// </summary>
public class ObjectClicker : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log($"{ eventData.pointerEnter.gameObject.name }");
    }
}