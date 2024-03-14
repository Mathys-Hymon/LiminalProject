using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDScript : MonoBehaviour
{
    [SerializeField] private GameObject dice;
    [SerializeField] private TextMeshProUGUI diceText;
    [SerializeField] private RawImage crosshair;

    private bool roll, visible;

    public static PlayerHUDScript instance;

    void Start()
    {
        instance = this;
    }

    public void SetCrosshairVisible(bool _visible)
    {
        visible = _visible;
        float lerpColor = 0;
        if(visible)
        {
            lerpColor = 1;
        }
        if(!roll)
        {
            crosshair.color = Vector4.Lerp(crosshair.color, new Vector4(1, 1, 1, lerpColor), 20 * Time.deltaTime);
        }
    }

    public void RollDice(int DiceResult, IInteractable interactableRef)
    {
        if(roll == false)
        {
            StartCoroutine(SetResultAfterAnimation(DiceResult, interactableRef));
        }
    }

    private IEnumerator SetResultAfterAnimation(int DiceResult, IInteractable interactableRef)
    {
        int rolledNumber = 0;
        int rollAnim = 0;
        int maxRollAnim = 250;
        roll = true;

        if (DiceResult < 0)
        {
            rolledNumber = Random.Range(1, 7);
        }
        else
        {
            rolledNumber = DiceResult;
        }

        while (rollAnim < maxRollAnim)
        {
            if(rollAnim <= maxRollAnim - (maxRollAnim / 3))
            {
                dice.transform.rotation = Quaternion.Slerp(dice.transform.rotation, dice.transform.rotation * Quaternion.Euler(Random.Range(0, 200), 0, Random.Range(-200, 0)), 30 * Time.deltaTime);
            }

            else
            {
                dice.transform.LookAt(MouseLook.instance.transform.position);
            }


            diceText.text = rolledNumber.ToString();
            yield return new WaitForSeconds(Time.deltaTime);
            rollAnim++;
        }

        yield return new WaitForSeconds(1f);

        interactableRef.GetDiceResult(rolledNumber);
        roll = false;
        yield return new WaitForSeconds(0.25f);
        dice.SetActive(false);
    }

    public void DiceResult(bool passedChallenge)
    {
        if(passedChallenge)
        {

        }
        else
        {
            
        }
    }



    private void Update()
    {
        if(roll)
        {
            diceText.alpha = Mathf.Lerp(diceText.alpha, 1f, 3 * Time.deltaTime);
            crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), 6 * Time.deltaTime);
            crosshair.color = Vector4.Lerp(crosshair.color, new Vector4(1, 1, 1, 1), 20 * Time.deltaTime);
            dice.SetActive(true);
        }
        else
        {
            diceText.alpha = Mathf.Lerp(diceText.alpha, 0f, 3 * Time.deltaTime);
            crosshair.transform.localScale = Vector3.Lerp(crosshair.transform.localScale, new Vector3(0.06f, 0.06f, 0.06f), 6 * Time.deltaTime);
            if(!visible)
            {
                crosshair.color = Vector4.Lerp(crosshair.color, new Vector4(1, 1, 1, 0), 20 * Time.deltaTime);
            }
        }
    }
}
