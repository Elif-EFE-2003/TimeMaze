using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CoinCount
{
    public class CoinCollector : MonoBehaviour
    {
        public static int score = 0;
        public TextMeshProUGUI goldText;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gold"))
            {
                score++;
                goldText.text = "Score " + score;

                AudioSource goldSound = other.GetComponent<AudioSource>();

                if(goldSound != null)
                {
                    goldSound.Play();
                    Destroy(other.gameObject, goldSound.clip.length);
                }
                else
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
