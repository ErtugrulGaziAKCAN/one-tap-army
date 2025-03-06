using System;
using System.Collections.Generic;
using AI_Controllers.DataHolder;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Castle
{
    public class CastleDataHolder : MonoBehaviour
    {
//-------Public Variables-------//
        [ReadOnly] public List<SoldierAIDataHolderCore> SpawnedAIList;
        public Action OnMemberKilledRival;
        [ReadOnly] public int CurrentCastleLevel = 1;

//------Serialized Fields-------//


//------Private Variables-------//

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion
    }
}