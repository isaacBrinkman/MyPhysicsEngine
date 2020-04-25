using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
    public int value;

    void OnClick() {
        BattleStart();
    }

    public void BattleStart() {
        SceneManager.LoadScene("Battle");
    }

    public void DialogueStart() {
        SceneManager.LoadScene("Dialogue");
    }

    public void TownStart() {
        SceneManager.LoadScene("Town");
    }

    public void Town2Start() {
        SceneManager.LoadScene("Town 2");
        }

    public void goToLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void SignDialogueStart()
    {
        SceneManager.LoadScene("Sign Dialogue Test");
    }
}
