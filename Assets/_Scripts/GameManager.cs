using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;

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
        [SerializeField]
        protected NavMeshSurface navMesh;

        protected List<string> loadedScenes = new List<string>();
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }

        private IEnumerator Start()
        {
            //Load the scenes you need at the beginning.
            foreach(var scene in startScenes)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                loadedScenes.Add(scene);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            RebuildNavMesh();
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            loadedScenes.Add(scene);
        }

        public void UnloadScene(string scene)
        {
            if (loadedScenes.Contains(scene))
            {
                SceneManager.UnloadSceneAsync(scene);
                loadedScenes.Remove(scene);
            }
        }

        public void RebuildNavMesh()
        {
            navMesh.BuildNavMesh();
        }

    }
}