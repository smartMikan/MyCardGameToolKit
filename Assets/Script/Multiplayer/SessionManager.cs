using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager singleton;
        public delegate void OnSceneLoaded();
        public OnSceneLoaded onSceneLoaded;

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(this.gameObject);

            }
            else
            {
                DestroyImmediate(this.gameObject);
            }
        }



        public void LoadGameLevel(OnSceneLoaded callback)
        {
            onSceneLoaded = callback;
            StartCoroutine("MainGame");
        }

        public void LoadMenu()
        {
            StartCoroutine("menu");
        }

        IEnumerator LoadLevel(string scenename)
        {
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenename, UnityEngine.SceneManagement.LoadSceneMode.Single);
            if (onSceneLoaded!=null)
            {
                onSceneLoaded();
                onSceneLoaded = null;
            }
        }
    }
}

