using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AnimationClipData
{
    public string animationName;
    public Sprite[] frames;
}

[System.Serializable]
public class CharacterLayer
{
    public string name;
    public SpriteRenderer renderer;
    public List<AnimationClipData> animations;

    public Sprite[] GetAnimation(string animName)
    {	
        foreach (var clip in animations)
        {
            if (clip.animationName == animName && clip.frames.Length > 0)
	    {
                return clip.frames;
	    }
        }
        return null;
    }
}

public class SkinHandler : MonoBehaviour
{
    public CharacterLayer[] layers;
    public float fps = 8f;
    public string currentAnimation;

    private float timer;
    private int currentFrame;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame++;
            AdvanceFrame();
        }
    }

    void AdvanceFrame()
    {
        int maxFrames = 0;
        foreach (var layer in layers)
        {
            Sprite[] frames = layer.GetAnimation(currentAnimation);
            if (frames != null && frames.Length > maxFrames)
                maxFrames = frames.Length;
        }

        if (maxFrames == 0)
        {
            Debug.LogWarning("No frames found for animation: " + currentAnimation);
            return;
        }

        currentFrame = currentFrame % maxFrames;

        foreach (var layer in layers)
        {
            if (layer.renderer == null) continue;

            Sprite[] frames = layer.GetAnimation(currentAnimation);
            if (frames == null || frames.Length == 0)
	    {
		layer.renderer.enabled = false;
		continue;
	    } else {
		layer.renderer.enabled = true;
	    }

            int frameIndex = Mathf.Min(currentFrame, frames.Length - 1);
            layer.renderer.sprite = frames[frameIndex];
        }
    }

    // Call this from anywhere to switch animations
    public void PlayAnimation(string animName)
    {
        if (currentAnimation == animName) return;
        currentAnimation = animName;
        currentFrame = 0;
        timer = 0f;
    }
}
