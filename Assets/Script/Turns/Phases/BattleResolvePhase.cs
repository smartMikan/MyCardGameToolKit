using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{

    [CreateAssetMenu(menuName = "Turns/BattleResolvePhase")]
    public class BattleResolvePhase : Phase
    {
        public Element attackElement;
        public Element defenceElement;


        public override bool IsComplete()
        {
            if (forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Debug.Log(this.name + " End");
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Debug.Log(this.name + " Start");
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;

                PlayerHolder player = Settings.gameManager.CurrentPlayer;
                PlayerHolder enemy = Settings.gameManager.GetEnemyOf(player);

                if (player.attackingCards.Count == 0)
                {
                    forceExit = true;
                    return;
                }

                Dictionary<CardInstance, BlockInstance> blockDic = Settings.gameManager.GetBlockInstances();


                for (int i = 0; i < player.attackingCards.Count; i++)
                {
                    CardInstance attackerInstance = player.attackingCards[i];
                    Card card = attackerInstance.viz.Card;
                    CardProperty attack = card.GetProperty(attackElement);
                    if (attack == null)
                    {
                        Debug.LogError("this card dont have a attack property assigned! " + card.name);
                        continue;
                    }
                    int attackValue = attack.intValue;

                    BlockInstance bi = GetBlockInstanceOfAttacker(attackerInstance, blockDic);
                    if (bi != null)
                    {
                        for (int b = 0; b < bi.Blockers.Count; b++)
                        {
                            CardInstance blockerInstance = bi.Blockers[b];
                            Card blockCard = blockerInstance.viz.Card;
                            CardProperty defence = blockCard.GetProperty(defenceElement);
                            if (defence == null)
                            {
                                Debug.LogWarning("trying to block with a card with no defence property assigned! " + card.name);
                                continue;
                            }
                            attackValue -= defence.intValue;
                            if (defence.intValue<= attackValue)
                            {
                                //card died
                                blockerInstance.CardInstanceToGraveyard();
                            }
                        }
                    }

                    if (attackValue <= 0)
                    {
                        attackValue = 0;
                        //Card Died
                        attackerInstance.CardInstanceToGraveyard();
                    }

                    enemy.DoDamage(attackValue);

                    //trick
                    player.DropCreatureCard(attackerInstance, false);
                    //rotation changed
                    player.currentHolder.SetCardDown(attackerInstance);
                    //set flat last so rotation change can be seen
                    attackerInstance.SetFlatfooted(true);
                }

                Settings.gameManager.ClearBlockInstances();
                player.attackingCards.Clear();

                forceExit = true;
            }
        }

        BlockInstance GetBlockInstanceOfAttacker(CardInstance attacker, Dictionary<CardInstance, BlockInstance> blockDic)
        {
            BlockInstance result = null;
            blockDic.TryGetValue(attacker, out result);
            return result;
        }

    }
}
