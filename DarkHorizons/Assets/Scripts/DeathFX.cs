using UnityEngine;
using UnityEngine.Rendering;

public class DeathFX : MonoBehaviour
{
    public GameObject m_Volume;
    public void ApplyFX()
    {
        if (m_Volume != null)
        {
            m_Volume.SetActive(true);
        }
    }

}