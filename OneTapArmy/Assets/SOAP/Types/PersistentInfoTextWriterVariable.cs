using UnityEngine;
using Obvious.Soap;
using UI;

[CreateAssetMenu(fileName = "scriptable_variable_" + nameof(PersistentInfoTextWriter),
    menuName = "Soap/ScriptableVariables/" + nameof(PersistentInfoTextWriter))]
public class PersistentInfoTextWriterVariable : ScriptableVariable<PersistentInfoTextWriter>
{
    public void SetText(string message)
    {
        Value.SetText(message);
    }

    public void SetPersistentTextActivity(bool isActive)
    {
        Value.SetPersistentTextActivity(isActive);
    }
}