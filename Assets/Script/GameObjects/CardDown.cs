using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Game Objects/CardDown")]
    public class CardDown : ObjectsLogic
    {
        public override void OnClick(CardInstance c)
        {
            Debug.Log("This Card is on the table");
        }

        public override void OnHighlight(CardInstance c)
        {
            //Vector3 o = Vector3.one * 1.5f;

            //this.transform.localScale = o;
           
        }
    }

}