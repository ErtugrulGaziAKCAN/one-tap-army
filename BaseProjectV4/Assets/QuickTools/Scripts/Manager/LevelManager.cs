using System.Collections.Generic;
using Obvious.Soap;
using Plugins.SaveGameFree.Scripts;
using UnityEngine;
namespace QuickTools.Scripts.Manager
{
    public class LevelManager : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private IntReference CurrentLevel;
        [SerializeField] private List<GameObject> TutorialLevelPrefabs;
        [SerializeField] private List<GameObject> LevelPrefabs;

        //------Private Variables-------//
        private const string CURRENT_LEVEL_SAVE_KEY = "CurrentLevel";


#region UNITY_METHODS

        private void Awake()
        {
            Load();
            InstantiateCurrentLevel();
        }

#endregion


#region PUBLIC_METHODS

        public void LevelStartedEvent()
        {
            
        }

        public void LevelFailedEvent()
        {
            
        }

        public void LevelCompletedEvent()
        {
            CurrentLevel.Value++;
            Save();
        }

#endregion


#region PRIVATE_METHODS

        private void InstantiateCurrentLevel()
        {
            if (LevelPrefabs.Count <= 0)
                return;

            if (CurrentLevel.Value <= TutorialLevelPrefabs.Count)
            {
                Instantiate(TutorialLevelPrefabs[(CurrentLevel.Value - 1) % TutorialLevelPrefabs.Count]);
            }
            else
            {
                Instantiate(LevelPrefabs[(CurrentLevel.Value - TutorialLevelPrefabs.Count - 1) % LevelPrefabs.Count]);
            }
        }

        private void Load()
        {
            CurrentLevel.Value = SaveGame.Exists(CURRENT_LEVEL_SAVE_KEY) ? SaveGame.Load(CURRENT_LEVEL_SAVE_KEY, 1) : 1;
        }

        private void Save()
        {
            SaveGame.Save(CURRENT_LEVEL_SAVE_KEY, CurrentLevel.Value);
        }

#endregion
    }
}