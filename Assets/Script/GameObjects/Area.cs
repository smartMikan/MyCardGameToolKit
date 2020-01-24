using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    public class Area : MonoBehaviour
    {

        public AreaLogic areaLogic;
        public void OnDrop()
        {
            areaLogic.Execute();
        }
    }
}

