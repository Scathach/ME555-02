using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject[] units;
    public GameObject[] lines;
    public GameObject unitPrefab;
    //public GameObject Horizontal_Line;
    //public GameObject Vertical_Line;
    public int num_lines = 2;
    public int numUnits = 25;

    public Vector3 range = new Vector3(5, 5, 5);

    [Range(0, 200)]
    public int avoid_distance = 50;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(this.transform.position, range * 2);
        Gizmos.color = Color.green;
        //Vector3 start = new Vector3(-100, 100, 0);
        //Vector3 mid = new Vector3(+100, 100, 0);
        //Vector3 goal = new Vector3(+100, -100, 0);
        //Gizmos.DrawLine(start,mid);
        //Gizmos.DrawLine(mid, goal);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(this.transform.position, 0.2f);
    }

    // Use this for initialization
    static void initialize_data()
    {
        string location = "C:\\Users\\savch\\Documents\\New Unity Project 1\\Assets\\data.txt";
        StreamWriter log = new StreamWriter(location, true);
        log.WriteLine("New Run");
        log.Close();
    }

    void Start () {
        initialize_data();
        //lines = new GameObject[num_lines];
        //lines[0] = Instantiate(Horizontal_Line, this.transform.position, Quaternion.identity) as GameObject;
        //lines[0].GetComponent<line>.manager2 = this.gameObject;
        //lines[1] = Instantiate(Vertical_Line, this.transform.position, Quaternion.identity) as GameObject;


        int RADIUS = 50;
        units = new GameObject[numUnits];
        for(int i =0; i < numUnits; i++)
        {
            int x = Random.Range(-RADIUS, RADIUS);
            int y = Random.Range(-RADIUS, RADIUS);

            if (x * x + y * y < RADIUS * RADIUS)
            {
                Vector3 unitPos = new Vector3(x-100, y+100, 0);
                units[i] = Instantiate(unitPrefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
                units[i].name = "robot" + i;
                units[i].GetComponent<unit>().manager = this.gameObject;
            }
            else
            {
                i--;
            }

            
        }
	}
	
    
	// Update is called once per frame
	//void Update () {
		
	//}
}
