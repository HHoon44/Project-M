using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace ProjectM.Start
{
    public class UIStartLoading : MonoBehaviour
    {
        public TextMeshProUGUI loadStateDesc;

        private bool isComplete = false;
        private static string dot = string.Empty;

        private void Update()
        {
            if (!isComplete)
            {
                if (Time.frameCount % 300 == 0)
                {
                    if (dot.Length >= 3)
                    {
                        dot = string.Empty;
                    }
                    else
                    {
                        dot = string.Concat(dot, '.');
                    }
                }

                loadStateDesc.text = dot;
            }
        }
    }
}