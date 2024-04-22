using UnityEngine;
using UnityEngine.UI;

public class ToolbarColor : MonoBehaviour
{
    public Color selectedColor, notSelectedColor;

    public Image imagen;

    private void Awake()
    {
        imagen = GetComponent<Image>();
    }

    public void SelectSlot()
    {
        imagen.color = selectedColor;
    }
    public void DeselectSlot()
    {
        imagen.color=notSelectedColor;
    }

}
