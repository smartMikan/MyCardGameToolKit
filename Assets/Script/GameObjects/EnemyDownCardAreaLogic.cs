﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/EnemyDownCardsAreaLogic")]
    public class EnemyDownCardAreaLogic : AreaLogic
    {

        public CardVariables enemySelectedCard;

        public override void Execute()
        {
            if (domainCard)
            {
                return;
            }
            domainCard = enemySelectedCard.value;
            enemySelectedCard.value.belongsToArea = this;
        }
       


    }
}

