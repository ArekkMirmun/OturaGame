using System.Collections.Generic;
using UnityEngine;

public class SpellsManager : MonoBehaviour
{
    private List<SkillBase> skills = new List<SkillBase>();

    private void Start()
    {
        // Obtiene todas las habilidades en el personaje
        skills.AddRange(GetComponentsInChildren<SkillBase>());
    }

    private void Update()
    {
        foreach (SkillBase skill in skills)
        {
            if (Input.GetKeyDown(skill.activationKey))
            {
                skill.Activate();
            }
        }
    }
}
