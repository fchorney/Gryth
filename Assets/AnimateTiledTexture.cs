using UnityEngine;
using System.Collections;
 
class AnimateTiledTexture : MonoBehaviour
{
    public int columns = 7;
    public int rows = 1;
    public float framesPerSecond = 3f;
 
    //the current frame to display
    private int index = 0;
 
    void Start()
    {
        StartCoroutine(updateTiling());
 
        //set the tile size of the texture (in UV units), based on the rows and columns
        Vector2 size = new Vector2(1f / columns, 1f / rows);
        renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }
 
    private IEnumerator updateTiling()
    {
        while (true)
        {
            //move to the next index
            index++;
            if (index >= rows * columns)
                index = 0;
 
            //split into x and y indexes
            Vector2 offset = new Vector2((float)index / columns - (index / columns), //x index
                                          -((index / columns) / (float)rows));          //y index
 
            renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
 
            yield return new WaitForSeconds(1f / framesPerSecond);
        }
 
    }
}