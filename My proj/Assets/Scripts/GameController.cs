using UnityEngine;

public enum GameState {FreeRoam, Dialog, Battle}
public class GameController : MonoBehaviour 
{
    [SerializeField] PlayerController playerController;

    GameState state;

    public void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog) 
                state = GameState.FreeRoam;
        };
    }

    public void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        // ❌ REMOVE THIS → input handled by DialogManager internally
        // else if (state == GameState.Dialog)
        // {
        //     DialogManager.Instance.HandleUpdate();
        // }
        else if (state == GameState.Battle)
        {
            // Future Battle Logic
        }
    }
}
