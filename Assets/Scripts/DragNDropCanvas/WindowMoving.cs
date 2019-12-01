using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowMoving : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform _window = null;

    private Vector2 _relativeDragStartPosition = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = eventData.position + _relativeDragStartPosition;

        float adjustedWindowWidth = ((_window.sizeDelta.x / 2f) * (((float)Screen.width) / ((float)Screen.currentResolution.width)));
        float adjustedWindowHeight = ((_window.sizeDelta.y / 2f) * (((float)Screen.height) / ((float)Screen.currentResolution.height)));

        newPos.x = Mathf.Clamp(newPos.x, adjustedWindowWidth, (Screen.width - adjustedWindowWidth));
        newPos.y = Mathf.Clamp(newPos.y, adjustedWindowHeight, (Screen.height - adjustedWindowHeight));

        _window.position = new Vector3(newPos.x, newPos.y, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _relativeDragStartPosition = new Vector2(_window.position.x, _window.position.y) - eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //
    }
}
