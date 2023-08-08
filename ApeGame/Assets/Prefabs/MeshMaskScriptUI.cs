using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshMaskScriptUI : MaskableGraphic
{
    float count = 0;
    bool rising = true;
  
    protected override void OnPopulateMesh(VertexHelper vertexHelper)
    {
        vertexHelper.Clear();

        if((count * 5f) > 1025) {
            rising = false;
        } else if((count * 5f) < 200) {
            rising = true;
        }

        Vector3 vec_00 = new Vector3(0, 0);
        Vector3 vec_01 = new Vector3(0, 50);
        Vector3 vec_10 = new Vector3(count * 5f, 0);
        Vector3 vec_11 = new Vector3(count * 5f, 50);

        if(rising)
            ++count;
        else
            --count;
        
        print(vec_10);
        vertexHelper.AddUIVertexQuad(new UIVertex[]
        {
            new UIVertex { position = vec_00, color = Color.green },
            new UIVertex { position = vec_01, color = Color.green },
            new UIVertex { position = vec_11, color = Color.green },
            new UIVertex { position = vec_10, color = Color.green },
        });

    }

    private void Update()
    {
        SetVerticesDirty();
    }

}
