using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManufactureConfig", menuName = "Configs/ManufactureConfig", order = 1)]
public class ManufactureConfig : ScriptableObject
{
    public bool RequireInput;
    public Product[] InputProducts;
    public Product OutputProduct;
    public float ProductionTime;
}
