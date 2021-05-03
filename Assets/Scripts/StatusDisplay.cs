using System;
using System.Globalization;
using System.Collections;
using UnityEngine;

public class StatusDisplay : MonoBehaviour {

    public Canvas canvas;
    public TMPro.TextMeshProUGUI fuelText, oxygenText, distanceText, healthText;

    private bool fadeInFinished = false;
    private System.Globalization.CultureInfo customCulture;

    private void Start() {
        canvas.gameObject.SetActive(false);
        customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
    }

    void Update() {
        if (!fadeInFinished && GameStatusManager.isStarted()) {
            canvas.gameObject.SetActive(true);
            CanvasGroup panel = canvas.GetComponentInChildren<CanvasGroup>();
            StartCoroutine(DoFade(panel, panel.alpha, 1, 4f));
            fadeInFinished = true;
        }
        fuelText.text = Mathf.Ceil(Starship.fuel) + "%";
        oxygenText.text = Mathf.Ceil(Starship.oxygen) + "%";
        healthText.text = Mathf.Ceil(Starship.health) + "%";
        distanceText.text = String.Format("{0:0.00}", Starship.distance / 1000) + "";
    }

    public IEnumerator DoFade(CanvasGroup panel, float start, float end, float duration) {
        float counter = 0;
        while (counter < duration) {
            counter += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(start, end, counter / duration);

            yield return null;
        }
    }

}
