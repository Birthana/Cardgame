using System.Collections;
using UnityEngine;
using TMPro;

public class Draggable : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 screenPoint;
    private Vector3 offset;
    [SerializeField] private EnergyManager energy;
    [SerializeField] private CardManager cards;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        energy = FindObjectOfType<EnergyManager>();
        cards = FindObjectOfType<CardManager>();
    }

    private void OnMouseDown()
    {
        if (energy.GetCurrentEnergy() > 0 && this.GetComponent<Card>().level <= energy.GetCurrentEnergy())
        {
            screenPoint = mainCamera.WorldToScreenPoint(transform.position);
            offset = new Vector3(0, 0, 0);
            AddLayers(10);
        }
    }

    private void OnMouseDrag()
    {
        if (energy.GetCurrentEnergy() > 0 && this.GetComponent<Card>().level <= energy.GetCurrentEnergy())
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            if (this.GetComponent<Card>().hasTarget)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(0.0f, this.transform.position.y, this.transform.position.z), 1.0f);
                this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                TargetArrow.instance.SetStartPosition(this.transform.position);
                Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, screenPoint.z);
                Vector3 currentPosition = mainCamera.ScreenToWorldPoint(currentScreenPoint) + offset;
                TargetArrow.instance.SetArrowPosition(currentPosition);
            }
            else
            {
                Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, screenPoint.z);
                Vector3 currentPosition = mainCamera.ScreenToWorldPoint(currentScreenPoint) + offset;
                transform.position = currentPosition;
                this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            }
        }
    }

    private void OnMouseUp()
    {
        if (energy.GetCurrentEnergy() > 0 && this.GetComponent<Card>().level <= energy.GetCurrentEnergy())
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            if (this.GetComponent<Card>().hasTarget)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
                if (hit)
                {
                    if (hit.collider.gameObject.GetComponent<Enemy>())
                    {
                        CardEffectManager effects = this.GetComponent<CardEffectManager>();
                        effects.SetTargets(hit.collider.gameObject);
                        effects.AddToStack();
                        CardEffectStack.instance.ResolveCardEffect();
                        cards.PlayCard(this.gameObject.GetComponent<Card>());
                        this.gameObject.SetActive(false);
                    }
                }
                TargetArrow.instance.DestroyLine();
            }
            AddLayers(-10);
            if (this.isActiveAndEnabled)
            {
                StartCoroutine(ReformattingHand());
            }
        }
    }

    IEnumerator ReformattingHand()
    {
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<LayoutManager>().AddCard(null);
    }

    public void AddLayers(int addLayers)
    {
        SpriteRenderer[] imageLayers = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer tempLayer in imageLayers)
        {
            tempLayer.sortingOrder += addLayers;
        }
        TextMeshPro[] textLayers = this.GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro tempText in textLayers)
        {
            tempText.sortingOrder += addLayers;
        }
    }
}
