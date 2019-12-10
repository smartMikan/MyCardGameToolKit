using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum CardPhase
{
    check, apply
}

enum CardState
{
    deck,hand,destroy,armor
}

public class Card : MonoBehaviour,IPointerDragInterface
{

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Start");
        CardManager.Instance.CurrentSelectedCard = this;
        GetComponent<Image>().raycastTarget = false;
        //
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
        //TO DO: Card Effect Check 
        CardManager.Instance.CurrentSelectedCard = null;
        CardManager.Instance.LastSelectedCard = this;
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer In");

    }

    float pointeDownTime;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");
    }

    public virtual void MyEffect()
    {
        Debug.Log("Card Base Class Effect");
    }
}
