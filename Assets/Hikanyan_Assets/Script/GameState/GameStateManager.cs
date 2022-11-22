using System;
using UnityEngine;

public class GameStateManager
{
    private InGameState _gameState;
    /// <summary>
    /// InGameState���O������Q�Ƃ���p�̃v���p�e�B
    /// </summary>
    public InGameState GameState => _gameState;
    public enum InGameState
    {
        /// <summary>
        /// �Q�[���X�^�[�g���o���ł��邱�Ƃ�\��
        /// </summary>
        Start,

        /// <summary>
        /// �Q�[���V�[����2D�̂��̂ł��邱�Ƃ�\��
        /// </summary>
        Game2D,

        /// <summary>
        /// �Q�[���V�[����3D�ł��邱�Ƃ�\��
        /// </summary>
        Game3D,

        /// <summary>
        /// �C�x���g���[�r�[������Ă�����
        /// </summary>
        Movie,

        /// <summary>
        /// �v���C���[���S�[�����Ă����Ԃ�\��
        /// </summary>
        Goal
    }
    /// <summary>
    /// GameState�̕ύX���s���ۂɌĂяo�����\�b�h
    /// </summary>
    /// <param name="gameState">�ύX����State</param>
    public void GameStateChange(InGameState gameState)
    {
        InGameState beforeState = _gameState;
        _gameState = gameState;
        Debug.Log($"GameState��{beforeState}����A{_gameState}�ɕύX����܂����B");
    }
}
