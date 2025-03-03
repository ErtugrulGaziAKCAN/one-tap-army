using System;
using Nova;
using UnityEngine;
namespace QuickTools.QuickUpgradables.Scripts
{
    public partial class UpgradeableButton
    {
        [Serializable]
        private class CostTextDetail
        {
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