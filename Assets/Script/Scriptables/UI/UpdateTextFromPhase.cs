using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO.UI;
using UnityEngine.UI;

namespace Oukanu
{
    public class UpdateTextFromPhase : UIPropertyUpdater
    {
        public Variable.PhaseVariable currentPhase;
        
        public Text targetText;

        /// <summary>
        /// Use this to update a text UI element based on the target string variable
        /// </summary>
        public override void Raise()
        {
            targetText.text = currentPhase.value.phaseName;
        }

       
    }
}

