using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class unit : MonoBehaviour {
    public GameObject manager;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;

    int collision_counter;
    
    Vector2 goalPos = new Vector2 (+100, -100);
    Vector2 attractive_Force;
    Vector2 repulsive_Force;
    Vector2 initial_position = new Vector2(-100, 100);

    int goal_modifier = 5;
    int line_modifier = 10;
    // Use this for initialization
    void Start() {
        velocity = new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    void horizontal_path_force()
    {
        if (location.x - initial_position.x < 190)
        {
            Vector2 normal_to_line = new Vector2(location.x, 100);
            Vector2 diff = normal_to_line - location;
            diff = diff.normalized * line_modifier;
            applyForce(diff);
        }
    }

    void vertical_path_force()
    {
        if (initial_position.y - location.y < 200)
        {
            Vector2 normal_to_line = new Vector2(100, location.y);
            Vector2 diff = normal_to_line - location;
            diff = diff.normalized * line_modifier;
            applyForce(diff);
        }
    }

    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    Vector3 rep_force(Vector2 f)
    {

        //float new_x = -1/(f.x);// * f.x);
        //float new_y = -1/(f.y);// * f.y);
        float new_x = f.x;
        float new_y = f.y;
        Vector3 force = new Vector3(new_x, new_y, 0);
        //Debug.Log(force);
        this.GetComponent<Rigidbody2D>().AddForce(force);
        //Debug.DrawRay(this.transform.position, force, Color.blue);
        return force;
    }

    void avoid()
    {
        float nearby = manager.GetComponent<NewBehaviourScript>().avoid_distance;
        //Vector2 sum = Vector2.zero;
        //int count = 0;
        collision_counter = 0;
        foreach (GameObject other in manager.GetComponent<NewBehaviourScript>().units)
        {
            if (other != this.gameObject)
            {
                float d = Vector2.Distance(location, other.GetComponent<unit>().location);
                if (d < nearby)
                {
                    float y = -1;
                    float min_distance = 3;
                    if (d >= min_distance)
                    {
                        float m = (+1) / (nearby - min_distance);
                        y = m * d + (-m * nearby);
                    }
                    else if (d < min_distance)
                    {
                        y = -(d*d);
                        //Debug.Log(y);
                    }
                    Vector2 temp = (other.GetComponent<unit>().location - this.GetComponent<unit>().location);
                    //Debug.Log(temp);
                    if (d <= 1) {collision_counter += 1;}                  
                    rep_force(temp*y);
                }
            }
            else { continue; }
        }
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        this.GetComponent<Rigidbody2D>().AddForce(force);
        //Debug.DrawRay(this.transform.position, force, Color.white);
    }
    
    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        Vector2 gl;
        Vector2 first_goal = new Vector2(100, -100);
        /*
        Vector2 second_goal = new Vector2(100, -100);
        Vector2 center_of_swarm = new Vector2(-100, 100);

        int num_of_robots = 0;
        foreach (GameObject other in manager.GetComponent<NewBehaviourScript>().units)
        {
            center_of_swarm += other.GetComponent<Rigidbody2D>().position;
            num_of_robots++;
        }
        center_of_swarm = center_of_swarm / num_of_robots;
        
        int checker = 0;
        if ((center_of_swarm.x*center_of_swarm.x + center_of_swarm.y* center_of_swarm.y > 95*95 ||
            center_of_swarm.x * center_of_swarm.x + center_of_swarm.y * center_of_swarm.y > 105 * 105)
            && checker == 0)
        {
        */
        gl = seek(first_goal);
        /*       
            checker += 1;
        }
        else{gl = seek(second_goal);}
        */
        attractive_Force = gl;
        attractive_Force = attractive_Force.normalized * goal_modifier;
        applyForce(attractive_Force);
    }

    static void WriteString(string input)
    {
        string location = "C:\\Users\\savch\\Documents\\New Unity Project 1\\Assets\\data.txt";
        StreamWriter log = new StreamWriter(location, true);
        log.WriteLine(input);
        log.Close();
    }
    // Update is called once per frame
    void Update () {
        //line_homing();
        flock();
        avoid();
        horizontal_path_force();
        vertical_path_force();
        string name_collision = name;
        name_collision += ":";
        name_collision += collision_counter.ToString();
        name_collision += ":";
        name_collision += (Mathf.Abs(location.y - 100)).ToString();
        name_collision += ":";
        name_collision += (Mathf.Abs(100 - location.x)).ToString();
        name_collision += ":";
        name_collision += (Mathf.Sqrt( (location - goalPos).x* (location - goalPos).x+ (location - goalPos).y* (location - goalPos).y));
        name_collision += ":";
        name_collision += Time.time.ToString();
        WriteString(name_collision);
        //Debug.Log(collision_counter);
        //Debug.Log(name);
        goalPos = manager.transform.position;
	}
}
