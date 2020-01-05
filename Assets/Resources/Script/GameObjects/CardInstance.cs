using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstance : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        
    }

    public void OnHighlight()
    {

        //Vector3 o = Vector3.one * 1.5f;

        //this.transform.localScale = o;
        Debug.Log(this.gameObject.name);
    }
}
