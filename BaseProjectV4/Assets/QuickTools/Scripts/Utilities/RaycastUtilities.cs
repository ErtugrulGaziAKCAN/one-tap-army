using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace QuickTools.Scripts.Utilities
{
    public static class RaycastUtilities
    {
        public static bool PointerIsOverUI(Vector2 screenPos, out GameObject hit, out Vector2 hitPoint)
        {
            var pointerEventData = ScreenPosToPointerData(screenPos);
            var hitObject = UIRaycast(pointerEventData);
            hit = hitObject;
            hitPoint = pointerEventData.position;
            return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
        }

        static GameObject UIRaycast(PointerEventData pointerData)
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            return results.Count < 1 ? null : results[0].gameObject;
        }

        static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
            => new(EventSystem.current) { position = screenPos };
    }
}