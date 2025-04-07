using UnityEngine;
using System.Collections;
using Health;

public class SpikesCircle : SkillBase
{
    [SerializeField] private GameObject skillPrefab; // Prefab de la habilidad
    [SerializeField] private GameObject previewPrefab; // Prefab de la previsualización (círculo rojo)
    [SerializeField] private float previewRadius = 2f; // Radio editable de la previsualización
    [SerializeField] private LayerMask groundLayer; // Capa del suelo para colocar la previsualización
    [SerializeField] private float cooldown = 1f; // Cooldown de la habilidad
    [SerializeField] private Transform playerCamera; // Cámara del jugador
    [SerializeField] private AudioSource soundEffect; // Sonido de la habilidad

    private GameObject previewInstance;
    public bool isPreviewing = false;
    private bool canUseSkill = true; // Controla el cooldown
    private Vector3 targetPosition; // Guarda la posición donde se colocará la habilidad
    public Player player;

    void Start()
    {
        activationKey = KeyCode.Alpha1; // Tecla para activar la habilidad
    }

    void Update()
    {
        if (isPreviewing)
        {
            UpdatePreviewPosition();

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                CastSkill();
            }
        }
    }

    public override void Activate()
    {
        if (!canUseSkill) return;

        if (!isPreviewing)
        {
            TogglePreview(true);
        }
    }

    void TogglePreview(bool state)
    {
        isPreviewing = state;

        if (state && previewInstance == null)
        {
            previewInstance = Instantiate(previewPrefab);
            previewInstance.transform.localScale = new Vector3(previewRadius * 2, 1, previewRadius * 2);
        }
        else if (!state && previewInstance)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
    }

    void UpdatePreviewPosition()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            targetPosition = hit.point; // Guarda la posición donde impactó el rayo
            //print(previewInstance);
            if (previewInstance)
            {
                //print("Posición impactante: " + targetPosition);
                previewInstance.transform.position = targetPosition + Vector3.up * 0.1f; // Lo levanto un poco para evitar solaparse con el suelo
            }
        }
    }

    void CastSkill()
    {
        if (previewInstance)
        {
            GameObject go = Instantiate(skillPrefab, targetPosition, Quaternion.identity);
            soundEffect.Play();
            Destroy(go,3f);
            TogglePreview(false);
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(cooldown);
        canUseSkill = true;
    }
}
