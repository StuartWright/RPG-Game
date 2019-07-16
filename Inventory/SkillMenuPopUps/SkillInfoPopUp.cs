using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillInfoPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public GameObject PopUpToShow;
    public Text SkillInfo;
    public bool DashSkill;
    public bool AOESkill;
    public bool PetSkill;
    public bool RageSkill;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(DashSkill)
        {
            PopUpToShow.SetActive(true);
            SkillInfo.text = "Jump through the air dealing '" + (int)SkillManager.Instance.DashDamage + "' damage to enemies behind you.";
        }
        if (AOESkill)
        {
            PopUpToShow.SetActive(true);
            SkillInfo.text = "Create a ring of fire around you dealing '" + (int)SkillManager.Instance.AOEDamage + "' to all enemies within.";
        }
        if (PetSkill)
        {
            PopUpToShow.SetActive(true);
            SkillInfo.text = "Summon a friendly pet to aid you in combat. Lasts for 60 seconds.";
        }
        if (RageSkill)
        {
            PopUpToShow.SetActive(true);
            SkillInfo.text = "Create a ring around you, any enemies inside this area will start to attack other enemies.";
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PopUpToShow.SetActive(false);
    }
}



    
