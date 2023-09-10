using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MoveAndCopyDrone : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public string filePathA = "DroneA.txt";
    public string filePathB = "DroneB.txt";
    public Text textX; // UI Text component for displaying the drone's X position
    public Text textY; // UI Text component for displaying the drone's Y position
    public Text textZ; // UI Text component for displaying the drone's Z position

    private System.Random random = new System.Random();


    void Start()
    {
        StartCoroutine(InvokeEnterKey());
    }

    System.Collections.IEnumerator InvokeEnterKey()
    {
        while (true)
        {
            yield return new WaitForSeconds(10); // 10�� ���

            OnEnterKeyPressed(); // "����Ű�� ����" ó��
            CopyFile();
        }
    }


    void OnEnterKeyPressed()
    {
        if (File.Exists(filePathA) || File.Exists(filePathB))
        {
            string[] lines;

            if (File.Exists(filePathA))
            {
                lines = File.ReadAllLines(filePathA);
            }
            else
            {
                lines = File.ReadAllLines(filePathB);
            }


            if (lines.Length >= 3)
            {
                if (float.TryParse(lines[0], out float targetX) &&
                    float.TryParse(lines[1], out float targetY) &&
                    float.TryParse(lines[2], out float targetZ))
                {
                    targetPosition = new Vector3(targetX, targetY, targetZ);
                    isMoving = true;

                    // Update the text components with the new positions
                    textX.text = $"Target X: {targetX}";
                    textY.text = $"Target Y: {targetY}";
                    textZ.text = $"Target Z: {targetZ}";
                }
            }
        }
    }


    void CopyFile()
    {

        string sourcePath = Path.Combine(Application.dataPath, "new.txt");
        string targetPathA = Path.Combine(Application.dataPath, filePathA);
        string targetPathB = Path.Combine(Application.dataPath, filePathB);

        if (File.Exists(sourcePath))
        {

            string[] linesSourceFile = File.ReadAllLines(sourcePath);

            if (linesSourceFile.Length >= 3)
            {

                // ���� ���� 0�̸� DroneA.txt��, �ƴϸ� DroneB.txt�� ����
                string selectedFilePath = random.Next(2) == 0 ? filePathA : filePathB;

                File.WriteAllLines(selectedFilePath, linesSourceFile);
            }
        }

    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f) // ����� �����ٰ� �ǴܵǸ� ������ �������� ����
            {
                isMoving = false;
            }
        }
    }

}
