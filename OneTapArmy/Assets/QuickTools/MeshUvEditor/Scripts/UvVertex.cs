using System;
using UnityEngine;
namespace QuickTools.MeshUvEditor.Scripts
{

    [Serializable]
    public class UvVertex
    {
//-------Public Variables-------//
        public Vector2 Pos;
        public Color VertexColor;

//------Serialized Fields-------//


//------Private Variables-------//



#region UNITY_METHODS

        public UvVertex(Vector2 pos, Color color)
        {
            Pos = pos;
            VertexColor = color;
        }

#endregion


#region PUBLIC_METHODS

#endregion


#region PRIVATE_METHODS

#endregion


    }
}