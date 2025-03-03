using System.Collections.Generic;

namespace AI_StateSystem
{
    public class State
    {
//-------Public Variables-------//

//------Serialized Fields-------//


//------Private Variables-------//
        private readonly List<StateAction> _updateActions;


#region PUBLIC_METHODS

        public State(List<StateAction> updateActions)
        {
            _updateActions = updateActions;
        }

        public void Start()
        {
            if (_updateActions is null)
                return;
            
            foreach (var action in _updateActions)
            {
                action.OnStart();
            }
        }
        
        public void End()
        {
            if (_updateActions is null)
                return;
            
            foreach (var action in _updateActions)
            {
                action.OnEnd();
            }
        }

        public void Tick()
        {
            if (_updateActions is null)
                return;

            foreach (var t in _updateActions)
                t.OnUpdate();
        }
        
        public void FixedTick()
        {
            if (_updateActions is null)
                return;

            foreach (var t in _updateActions)
                t.OnFixedUpdate();
        }


#endregion


#region PRIVATE_METHODS


#endregion

    
    }
}