using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/ActiveCardPhase")]
    public class ActiveCardPhase : Phase
    {
        public GameObject setbackEffect;



        public Objects.AreaLogic[] CheckArea;

        [System.NonSerialized]
        List<CardInstance> activeCards = new List<CardInstance>();

        int index;
        bool result;

        public override bool IsComplete()
        {
            //activeCards[index].viz.SetBackward(false);
            //bool result = activeCards[index].TickEffect();

            //if (result)
            //{
            //    activeCards[index].belongsToPlayer.MoveCard(activeCards[index], activeCards[index].belongsToPlayer.downCards, activeCards[index].belongsToPlayer.graveyardCards);
            //    activeCards.Remove(activeCards[index]);
            //    index = activeCards.Count - 1;
            //}

            //if (activeCards.Count <= 0) return true;
            //return false;

            return result;
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
                Debug.Log(Settings.gameManager.currentPlayer.username + "'s " + this.name + " Start");
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
                result = false;
                CheckCardsInArea();

                Settings.gameManager.StartCoroutine(TickEffect());

            }
        }


        void CheckCardsInArea()
        {
            activeCards.Clear();

            for (int i = 0; i < CheckArea.Length; i++)
            {
                if (CheckArea[i].domainCard != null)
                {
                    activeCards.Add(CheckArea[i].domainCard);
                }
            }

            activeCards.Sort(CompareSpeed);
            index = activeCards.Count - 1;
        }



        private static int CompareSpeed(CardInstance x, CardInstance y)
        {
            return x.viz.card.cardSpeed.CompareTo(y.viz.card.cardSpeed);
        }


        IEnumerator TickEffect()
        {
            while (activeCards.Count > 0)
            {
                activeCards[index].viz.SetBackward(false);
                yield return effectSetBackward();
                yield return activeCards[index].TickEffect();
                activeCards[index].belongsToPlayer.MoveCard(activeCards[index], activeCards[index].belongsToPlayer.downCards, activeCards[index].belongsToPlayer.graveyardCards);
                activeCards.Remove(activeCards[index]);
                index = activeCards.Count - 1;
            }

            result = true;
        }

        IEnumerator effectSetBackward()
        {
            GameObject go = Instantiate(setbackEffect);
            Settings.SetParentForCard(go.transform, activeCards[index].gameObject.transform);
            setbackEffect.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1f);
            Destroy(go);

        }

    }

}
