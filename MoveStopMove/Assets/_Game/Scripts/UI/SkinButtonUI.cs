using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButtonUI : MonoBehaviour
{
    [SerializeField] private Image itemImg;
    // Start is called before the first frame update
    private void Awake()
    {
        itemImg.sprite = SkinShopConfig.Ins.hair[1].hairImg;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
