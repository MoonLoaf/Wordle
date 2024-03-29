using UnityEngine;
using UnityEngine.UI;

public class LetterState : MonoBehaviour
{
    private readonly Color _correct = Color.green;
    private readonly Color _wrongPosition = Color.yellow;
    private readonly Color _wrong = Color.red;
    private readonly Color _unknown = Color.gray;
    
    private Image _letterBackground;

    private void Start()
    {
        _letterBackground = GetComponentInChildren<Image>();

        SetLetterColor(LetterEnum.Unknown);
    }

    public void SetLetterColor(LetterEnum state)
    {
        switch (state)
        {
            case LetterEnum.Correct:
                _letterBackground.color = _correct;
                break;
            
            case LetterEnum.WrongPosition:
                _letterBackground.color = _wrongPosition;
                break;
            
            case LetterEnum.Wrong:
                _letterBackground.color = _wrong;
                break;
            
            case LetterEnum.Unknown:
                _letterBackground.color = _unknown;
                break;
        }
    }
}
