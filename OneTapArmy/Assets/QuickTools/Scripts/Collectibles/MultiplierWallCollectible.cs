using AI_Controllers.DataHolder.Core;
using Nova;
using Plugins.CW.LeanPool.Required.Scripts;
using QuickTools.Scripts.Collectibles.Core;
using QuickTools.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Collectibles
{
    public class MultiplierWallCollectible : CollectibleCore
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField,BoxGroup("Design"),InfoBox("It randomizes the multiply value")] private Vector2 MultiplyAmount;
        [SerializeField, BoxGroup("References")] private TextBlock MultiplyText;
        [SerializeField, BoxGroup("References")] private ScriptableListAIDataHolderCore SpawnedAllies;
        
//------Private Variables-------//

#region UNITY_METHODS

        protected override void Start()
        {
            base.Start();
            MultiplyText.Text = MultiplyAmount + "X";
        }

#endregion


#region PUBLIC_METHODS
        
#endregion


#region PRIVATE_METHODS

#endregion
        public override void OnCollide(GameObject collidedObject)
        {
            base.OnCollide(collidedObject);
            var multiplyCount = Random.Range(MultiplyAmount.x, MultiplyAmount.y);
            for (var i = 0; i < multiplyCount; i++)
            {
                var clonedObject = collidedObject.GetComponent<AIDataHolderCore>();
                var spawned = LeanPool.Spawn(clonedObject, clonedObject.transform.position.WithAddedZ(-.15f * i),
                    Quaternion.identity);
                if(spawned.IsAllyAI)
                    SpawnedAllies.Add(spawned);
            }
        }
    }
}