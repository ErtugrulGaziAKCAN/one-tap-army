using System;
using Nova;
using UnityEngine;
namespace QuickTools.QuickUpgradables.Scripts
{
    public partial class UpgradeableButton
    {
        [Serializable]
        public class CurrentValueTextDetail
        {
            public enum TextType
            {
                Level,
                Value
            }

            public TextType Type;
            public string Prefix;
            public string Suffix;

            [SerializeField] private TextBlock Text;

            public void SetText(string s)
            {
                Text.Text = s;
            }
        }
    }
}