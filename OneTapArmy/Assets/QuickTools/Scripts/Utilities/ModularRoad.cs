using QuickTools.Scripts.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
namespace QuickTools.Scripts.Utilities
{
    public class ModularRoad : MonoBehaviour
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [TabGroup("Config"), SerializeField, EnumToggleButtons]
        private SideType Side = SideType.Straight;

        [TabGroup("Config"), SerializeField] private float Width = 6f;
        [TabGroup("Config"), SerializeField] private float Length = 5f;
        [TabGroup("Config"), SerializeField] private bool HasDepth;


        [TabGroup("References"), SerializeField]
        private GameObject MiddlePart;

        [TabGroup("References"), SerializeField]
        private GameObject DepthPiece;

        [TabGroup("References"), SerializeField]
        private GameObject RadialSideL, RadialSideR;

        [TabGroup("References"), SerializeField]
        private GameObject CurbSideL, CurbSideR;

        [TabGroup("References"), SerializeField]
        private GameObject StraightSideL, StraightSideR;


//------Private Variables-------//
        private const float ORIGINAL_ROAD_WIDTH = 6f;
        private const float ORIGINAL_ROAD_LENGTH = 5f;

        private enum SideType
        {
            Radial,
            Curb,
            Straight,
        }

#region UNITY_METHODS

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

        [Button(ButtonSizes.Large)]
        private void Apply()
        {
            MiddlePart.transform.localScale =
                Vector3.one.WithX(Width / ORIGINAL_ROAD_WIDTH).WithZ(Length / ORIGINAL_ROAD_LENGTH);

            DepthPiece.SetActive(HasDepth);
            DepthPiece.transform.localScale = Vector3.one.WithX((Width + 1) / (ORIGINAL_ROAD_WIDTH + 1))
                .WithZ(Length / ORIGINAL_ROAD_LENGTH);

            CurbSideL.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);
            CurbSideR.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);
            RadialSideL.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);
            RadialSideR.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);
            StraightSideL.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);
            StraightSideR.transform.localScale = Vector3.one.WithZ(Length / ORIGINAL_ROAD_LENGTH);

            CurbSideL.transform.localPosition = Vector3.zero.WithX(-Width / 2f);
            CurbSideR.transform.localPosition = Vector3.zero.WithX(Width / 2f);
            RadialSideL.transform.localPosition = Vector3.zero.WithX(-Width / 2f);
            RadialSideR.transform.localPosition = Vector3.zero.WithX(Width / 2f);
            StraightSideL.transform.localPosition = Vector3.zero.WithX(-Width / 2f);
            StraightSideR.transform.localPosition = Vector3.zero.WithX(Width / 2f);

            CurbSideL.transform.parent.gameObject.SetActive(Side == SideType.Curb);
            RadialSideL.transform.parent.gameObject.SetActive(Side == SideType.Radial);
            StraightSideL.transform.parent.gameObject.SetActive(Side == SideType.Straight);
        }

#endregion
    }
}