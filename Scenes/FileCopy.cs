using System;
using System.IO;
using UnityEngine;

public class FileCopy : MonoBehaviour
{
    private System.Random random = new System.Random();

    void Start()
    {
        StartCoroutine(CopyFile());
    }

    System.Collections.IEnumerator CopyFile()
    {
        while (true)
        {
            string sourcePath = Path.Combine(Application.dataPath, "new.txt");
            string targetPathA = Path.Combine(Application.dataPath, "DroneA.txt");
            string targetPathB = Path.Combine(Application.dataPath, "DroneB.txt");

            if (File.Exists(sourcePath))
            {
                string[] lines = File.ReadAllLines(sourcePath);

                if (lines.Length >= 3)
                {
                    // ���� ���� 0�̸� DroneA.txt��, �ƴϸ� DroneB.txt�� ����
                    string targetPath = random.Next(2) == 0 ? targetPathA : targetPathB;

                    File.WriteAllLines(targetPath, lines);
                    File.WriteAllText(sourcePath, string.Empty);
                }
            }
            

            yield return new WaitForSeconds(5); 
        }
    }
}
