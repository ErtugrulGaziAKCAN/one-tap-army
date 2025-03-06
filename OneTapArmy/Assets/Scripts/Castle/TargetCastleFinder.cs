using System.Collections.Generic;
using System.Linq;
using MonKey.Extensions;
using QuickTools.Scripts.Utilities;
namespace Castle
{
    public class TargetCastleFinder : QuickSingleton<TargetCastleFinder>
    {

//-------Public Variables-------//

//------Serialized Fields-------//


//------Private Variables-------//
        private List<CastleDataHolder> _allCastles;

#region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            _allCastles = GetComponentsInChildren<CastleDataHolder>().ToList();
        }

#endregion


#region PUBLIC_METHODS

        public CastleDataHolder GetRandomCastle(CastleDataHolder castleDataHolder) =>
            _allCastles.Where((c) => c != castleDataHolder).ToList().GetRandom();
        

#endregion


#region PRIVATE_METHODS

#endregion

    }
}