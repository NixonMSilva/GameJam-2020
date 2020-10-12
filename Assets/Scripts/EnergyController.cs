using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField] private GameObject energyBar;
    [SerializeField] float maxEnergy = 100f;

    private float energy;
    private UIBarController barController;

    private void Awake ()
    {
        energy = maxEnergy;
        barController = energyBar.GetComponent<UIBarController>();
    }

    private void Update ()
    {
        barController.SetSlideValue(energy);
    }

    public float GetEnergy ()
    {
        return energy;
    }

    public void ChangeEnergy (float value)
    {
        energy += value;
        if (energy > 100f)
            energy = 100f;
    }
}
