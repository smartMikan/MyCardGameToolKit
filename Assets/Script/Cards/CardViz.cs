using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oukanu
{
    public class CardViz : MonoBehaviour
    {
        public Card card { get; private set; }
        public CardVizProperty[] properties;
        public GameObject statsHolder;
        public GameObject resourceHolder;
        public GameObject highlightHolder;
        public GameObject backgroundHolder;

        //private void Start()
        //{
        //    LoadCard(card);
        //}

        public void LoadCard(Card c)
        {
            if (c == null)
            {
                return;
            }
            card = c;

            c.cardType.OnSetType(this);

            CloseAll();


            for (int i = 0; i < c.properties.Length; i++)
            {
                CardProperty cp = c.properties[i];
                CardVizProperty p = GetProperty(cp.element);
                if (p == null)
                {
                    continue;
                }

                if (cp.element is ElementInt)
                {
                    p.text.text = cp.intValue.ToString();
                    p.text.gameObject.SetActive(true);
                }
                else if (cp.element is ElementText)
                {
                    p.text.text = cp.stringValue;
                    p.text.gameObject.SetActive(true);
                }
                else if (cp.element is ElementImage)
                {
                    p.img.sprite = cp.sprite;
                    p.img.gameObject.SetActive(true);
                }


            }

        }

        public void CloseAll()
        {
            foreach (CardVizProperty p in properties)
            {
                if(p.img != null)
                {
                    p.img.gameObject.SetActive(false);
                }
                if(p.text != null)
                {
                    p.text.gameObject.SetActive(false);
                }
            }
        }
        public CardVizProperty GetProperty(Element e)
        {
            CardVizProperty result = null;
            for (int i = 0; i < properties.Length; i++)
            {
                if(properties[i].element == e)
                {
                    result = properties[i];
                    break;
                }
            }

            return result;
        }


        public void SetHighight(bool state)
        {
            highlightHolder.SetActive(state);
        }


        public void SetBackward(bool state)
        {
            backgroundHolder.SetActive(state);
        }

    }

}