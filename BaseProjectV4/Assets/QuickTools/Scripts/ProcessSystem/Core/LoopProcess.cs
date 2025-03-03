using System;
namespace QuickTools.Scripts.ProcessSystem.Core
{
    public class LoopProcess : ProcessCore
    {
//-------Public Variables-------//


//------Serialized Fields-------//


//------Private Variables-------//


#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        protected override void OnProcessStart(Action onPreparationCompleted)
        {
            onPreparationCompleted?.Invoke();
        }

        protected override void OnProcess()
        {
        }

        protected override void OnProcessEnd()
        {
            StartProcessing();
        }

#endregion
    }
}