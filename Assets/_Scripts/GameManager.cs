using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace HighHouse
{
    /// <summary>
    /// Game Manager for controlling the game. 
    /// Handles stuff like loading scenes and unloading areas that arent needed anymore.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        [SerializeField]
        protected string[] startScenes;

        protected List<string> loadedScenes;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }

        private void Start()
        {
            foreach(var scene in startScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                loadedScenes.Add(scene);
            }
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            loadedScenes.Add(scene);
        }

        public void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene);
            loadedScenes.Remove(scene);
        }

    }
}