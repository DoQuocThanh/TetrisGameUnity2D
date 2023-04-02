using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TetrisBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime=0.8f;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rotationPosition;
    public static Transform[,] grid = new Transform[width, height];
    public bool End = false; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            
            transform.position += new Vector3(-1,0,0); 
            if(!ValidMove()){
                transform.position -= new Vector3(-1,0,0); 
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position += new Vector3(1,0,0); 
            if(!ValidMove()){
                transform.position -= new Vector3(1,0,0); 
            }
        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.RotateAround(transform.TransformPoint(rotationPosition),new Vector3(0,0,1),90);
            if(!ValidMove()){
                transform.RotateAround(transform.TransformPoint(rotationPosition),new Vector3(0,0,1),-90);
            }
        }

        if(Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime )){
            transform.position += new Vector3(0,-1,0);
            if(!ValidMove()){
                transform.position -= new Vector3(0,-1,0);
                addToGrid();
                CheckForLine();
                this.enabled = false;
                if(!End){
                    FindObjectOfType<Spawn>().NewT();
                }
                
            }
             
            previousTime = Time.time;              
        }
        void CheckForLine(){
            for(int i = height -1 ; i >= 0  ; i--) {
                if(HasLine(i)){
                    DeleteLine(i);
                    RowDown(i);
                }
            }

        }

        bool HasLine(int i){
            for(int j = 0; j < width ; j++) {
                    if(grid[j,i] == null)
                        return false;
            }
            return true;
        }

        void DeleteLine(int i){
            for(int j = 0; j < width ; j++) {
                    Destroy(grid[j,i].gameObject);
                    grid[j,i] = null; 
            }

           
        }

        void RowDown(int i){
            for(int y = i; y < height; y++) {
                for(int j = 0; j <  width; j++) {
                    if(grid[j,y] != null){
                        grid[j,y-1] = grid[j,y];
                        grid[j,y] = null;
                        grid[j,y-1].transform.position -= new Vector3(0,1,0);

                    }
                }
                
            }
        }

        bool ValidMove(){
           
            foreach(Transform children in transform){
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                
                if(roundedX < 0 || roundedY < 0 || roundedX >= width || roundedX >= height){
                    return false;

                }
                if(grid[roundedX,roundedY]!=null){
                    return false;

                }
            }
            
            return true;
        }

        void addToGrid(){
            foreach(Transform children in transform){
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                if (roundedY >= 18){
                       End = true;
                       SceneManager.LoadScene("End");
                }
                grid[roundedX,roundedY] = children;
            }

        }
    }
}
