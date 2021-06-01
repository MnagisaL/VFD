using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    private List<string> animationTransitionData = new List<string>(); //アニメーターの遷移の名前のデータ

    [SerializeField]
    private string[] animationPointandTransiitions;  //アニメーションを起こす場所と遷移

    private Animator characterAnimator;

    private CharacterText characterText;

    private bool[] animationOnceArray;

    private int animationLine = 0;

    private void OnEnable()
    {
        characterText = this.transform.root.gameObject.GetComponentInChildren<CharacterText>();
    }

    void Start()
    {
        characterAnimator = this.GetComponent<Animator>();
        animationOnceArray = new bool[animationPointandTransiitions.Length];
    }

    void LateUpdate()
    {
        AnimationRun();
    }

    private void Checkanimationstring(string transition)  //データに入っていない遷移だったらエラー
    {
        if (!animationTransitionData.Contains(transition))
            Debug.LogError("そのアニメーションはありません" + transition);
    }

    private string[] SpilitPointandTransiition(int animationLine)  //文字を","で分ける
    {
        return animationPointandTransiitions[animationLine].Split(',');
    }

    private void InitializeanimationOnceArray()
    {
        for (int i = 0; i < animationOnceArray.Length; i++)
        {
            animationOnceArray[i] = false;
        }
    }

    public float GetAnimationClipLength(string clipName)
    {
        float clipLength = 0;

        var rac = characterAnimator.runtimeAnimatorController;
        var clips = rac.animationClips.Where(x => x.name == clipName);
        foreach (var clip in clips)
        {
            clipLength = clip.length;
        }
        return clipLength;
    }

    private void AnimationRun()  //アニメーション実行
    {
        if (characterText.GetisCheckTextEnd())
            InitializeanimationOnceArray();
        if (animationOnceArray[animationLine]) return;
        if (int.Parse(SpilitPointandTransiition(animationLine)[0]) == characterText.GettextLine())
        {
            Checkanimationstring(SpilitPointandTransiition(animationLine)[1]);
            characterAnimator.SetTrigger(SpilitPointandTransiition(animationLine)[1]);
            animationOnceArray[animationLine] = true;
            animationLine++;
            if (animationLine >= animationPointandTransiitions.Length)
                animationLine = 0;
        }
    }

}
