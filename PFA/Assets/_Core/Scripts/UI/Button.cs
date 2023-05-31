using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInterface : MonoBehaviour
{
    [SerializeField] List<RawImage> listRawImage = new List<RawImage>();
    [SerializeField] List<Texture> listTexture = new List<Texture>();
    [SerializeField] List<string> listName = new List<string>();
    void Start()
    {
        listRawImage.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddImage(Texture texture)
    {
        for (int i = 0; i < listRawImage.Count; i++)
        {
            if (listRawImage[i] == null)
            {
                listRawImage[i].texture=texture;
                return;
            }
            
        }
    }
    public void AddObject(string name)
    {
        for (int i = 0; i < listRawImage.Count; i++)
        {
            if (listName[i] == name)
            {
                AddImage(listTexture[i]);
                return;
            }

        }
    }
}
