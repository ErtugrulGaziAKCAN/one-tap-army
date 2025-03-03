using UnityEngine;
using Obvious.Soap;
using QuickTools.Scripts.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "scriptable_variable_" + nameof(FloatingTextSpawner),
    menuName = "Soap/ScriptableVariables/" + nameof(FloatingTextSpawner))]
public class FloatingTextSpawnerVariable : ScriptableVariable<FloatingTextSpawner>
{
    [Button]
    public void SpawnText(string text) => Value.Spawn(text);
}