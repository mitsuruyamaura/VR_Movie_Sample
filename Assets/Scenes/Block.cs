using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType {
    Red,
    Blue,
    青,
    黒,
    
}


public class Block : MonoBehaviour
{
    public BlockType blockType;

    private void Start() {
        // デバッグ用
        CheckBlock();
    }

    public void CheckBlock() {

        // 上方向
        Ray ray = new Ray(transform.position, Vector3.up);

        // Ray が他のゲームオブジェクトのコライダーに接触したか判定
        if (Physics.Raycast(ray, out RaycastHit hit, 1.0f) == true) {

            // Ray の接触したゲームオブジェクトには、Block スクリプトがアタッチされているか判定
            if (hit.transform.gameObject.TryGetComponent(out Block block)) {

                // このブロックの色と、上方向にあるブロックの色が同じが判定
                if (blockType == block.blockType) {
                    
                    // 同じ種類のブロックでした
                    Debug.Log("同色 : " + blockType);
                }
                else {
                    // ブロック自体はあったものの、同じ色のブロックではなかった
                    Debug.Log("色違い : " + block.blockType);
                }
            }
        }
        
        // 右方向
        
        
    }
}