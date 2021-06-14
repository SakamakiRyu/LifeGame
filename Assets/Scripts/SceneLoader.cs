using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    AudioSource _source;
    [SerializeField] AudioClip _clip;
   
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 音を鳴らせてSceneを変える
    /// </summary>
    public void SceneLoad()
    {
        _source.PlayOneShot(_clip);
        SceneManager.LoadScene("GameScene");
    }
}
