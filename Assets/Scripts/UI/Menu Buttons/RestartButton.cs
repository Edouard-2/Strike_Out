public class RestartButton : MenuButton
{
    public override void Interact()
    {
        GameManager.Instance.RestartGame();
    }
}