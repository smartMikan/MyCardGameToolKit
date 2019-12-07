using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardArea : MonoBehaviour,IPointerEnterHandler
{
    Card QccupiedCard;


    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (CardManager.Instance.CurrentSelectedCard != null)
        {
            Debug.Log("Area Enter");
            CardManager.Instance.CurrentSelectedCard.gameObject.transform.position = gameObject.transform.position;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
