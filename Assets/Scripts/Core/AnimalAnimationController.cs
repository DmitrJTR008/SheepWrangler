
using UnityEngine;

public class AnimalAnimationController
{
    private Animal   _animal;
    private Animator _animalAnimator;
    private string _currentAnimationName;
    public AnimalAnimationController(Animal animal, Animator animalAnimator)
    {
        _animal   = animal;
        _animalAnimator = animalAnimator;
    }

    public void ChangeAnimation(string animationName)
    {
        // Если переданное имя анимации совпадает с текущей анимацией, нет необходимости делать какие-либо изменения
        if (animationName.Equals(_currentAnimationName))
            return;
        
        
        _animalAnimator.SetBool(_currentAnimationName, false);

        // Устанавливаем новую анимацию
        _currentAnimationName = animationName;
        _animalAnimator.SetBool(animationName, true);
    }
}
