using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsTest : MonoBehaviour
{
    public MyRGB myBall;
    public unityBall unityBall; // For this will have to add force
    public MyRGB myFloor;
    public Toggle offset;
    public Toggle center;
    public Toggle sameSide;
    public Toggle unity;
    public Toggle gravity;
    public Toggle friction;
    public Dropdown amount;
    public Dropdown direction;



    public void ClearAll()
    {
        MyRGB[] all = FindObjectsOfType<MyRGB>();
        foreach(MyRGB c in all)
        {
            if(c.name == "BlackSquare")
            {
                continue;
            }
            Destroy(c.gameObject);
        }
        unityBall[] all2 = FindObjectsOfType<unityBall>();
        foreach(unityBall c in all2)
        {
            Destroy(c.gameObject);
        }

    }

    public void Create()
    {
        if (!unity.isOn)
        {
            MyRGB[] col = new MyRGB[5];
            ClearAll();
            // 2 balls
            for (int i = 0; i < amount.value + 2; i++)
            {
                
                // for the amount create the balls
                col[i] = Instantiate(myBall, Vector3.zero, Quaternion.identity);


            }
            if (amount.value == 2)
            {
                col[4] = Instantiate(myBall, Vector3.zero, Quaternion.identity);
            }
            Center(col);
            Place(col);
            Offset(col);
            Direction(col);
            Gravity(col);
            Friction(col);
        }
        else
        {
            print("here");
            unityBall[] col = new unityBall[5];
            ClearAll();
            // 2 balls
            for (int i = 0; i < amount.value + 2; i++)
            {
                print('h');
                // for the amount create the balls
                col[i] = Instantiate(unityBall, Vector3.zero, Quaternion.identity);

            }
            if (amount.value == 2)
            {
                col[4] = Instantiate(unityBall, Vector3.zero, Quaternion.identity);
            }
            Center(col);
            Place(col);
            Offset(col);
            Direction(col);
            Gravity(col);
            foreach(unityBall c in col)
            {
                if (c == null) continue;
                print(c.velocity);
            }
        }
    
    }
        
    private void Gravity(MyRGB[] col)
    {
        if (gravity.isOn)
        {
            foreach (MyRGB c in col)
            {
                c.gravityScale = 1;
            }
            
        }
    }

    private void Gravity(unityBall[] col)
    {
        if (gravity.isOn)
        {
            foreach (unityBall c in col)
            {
                c.rgb.gravityScale = 1;
            }
        }
    }
    private void Friction(MyRGB[] col)
    {
        if (friction.isOn)
        {
            
            foreach (MyRGB c in col)
            {
                c.frictionScale = 1;
            }


        }
    }

    // Places the balls where they belong
    private void Place(MyRGB[] col)
    {
        if(amount.value == 0)
        {
            col[1].transform.position = new Vector3(-4, 0);
            col[1].velocity = Vector3.right;

        }

        else if(amount.value == 1)
        {
            if (sameSide.isOn)
            {
                // then place both balls on the same side
                col[1].transform.position = new Vector3(-4, 1.7f);
                col[2].transform.position = new Vector3(-4, -1.7f);
                col[1].velocity = Vector3.right;
                col[2].velocity = Vector3.right;

            }
            else
            {
                
                col[0].transform.position = Vector3.zero;
                col[1].transform.position = new Vector3(-4, 0);
                col[2].transform.position = new Vector3(4, 0);
                col[1].velocity = Vector3.right;
                col[2].velocity = Vector3.left;          
            }
        }
        else if(amount.value == 2)
        {
            col[1].transform.position = new Vector3(-4, 1.7f);
            col[2].transform.position = new Vector3(-4, -1.7f);
            col[1].velocity = Vector3.right;
            col[2].velocity = Vector3.right;
            col[3].transform.position = new Vector3(4, 1.7f);
            col[4].transform.position = new Vector3(4, -1.7f);
            col[3].velocity = Vector3.left;
            col[4].velocity = Vector3.left;
        }
    }

    private void Center(MyRGB[] col)
    {
        if (center.isOn)
        {
            col[0].velocity = Vector3.left;

        }
    }

    private void Offset(MyRGB[] col)
    {
        if (offset.isOn)
        {
            // then need to still make sure they hit at the same time
            // give it a random place within a certain raduis and move towards the ball
            if (amount.value == 0)
            {
                float x = -4;
                float y = Random.Range(-2, 2);
                col[1].transform.position = new Vector3(x, y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(-1, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
            }
            if (amount.value == 1 && sameSide.isOn)
            {
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(x, -y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3(norm.x + x, -(norm.y + y)).normalized;
            }
            if (amount.value == 1 && !sameSide.isOn)
            {
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(-x, -y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3(-(norm.x + x), -(norm.y + y)).normalized;
            }

            if (amount.value == 2)
            {
                // then combine the last two
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(x, -y);
                col[3].transform.position = new Vector3(-x, y);
                col[4].transform.position = new Vector3(-x, -y);

                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3((norm.x + x), -(norm.y + y)).normalized;
                col[3].velocity = new Vector3(-(norm.x + x), (norm.y + y)).normalized;
                col[4].velocity = new Vector3(-(norm.x + x), -(norm.y + y)).normalized;

            }
        }

    }

    private void Direction(MyRGB[] col)
    {
        // if the direction is horizontal leave it
        if(direction.value == 1)
        {
            foreach(MyRGB c in col)
            {
                if(c == null)
                {
                    continue;
                }
                // flip the x and y for transform and velocity
                float x = c.transform.position.x;
                float y = c.transform.position.y;
                c.transform.position = new Vector3(y, x);
                x = c.velocity.x;
                y = c.velocity.y;
                c.velocity = new Vector3(y, x);
            }
        }
    }

    // Places the balls where they belong
    private void Place(unityBall[] col)
    {
        if (amount.value == 0)
        {
            col[1].transform.position = new Vector3(-4, 0);
            col[1].velocity = Vector3.right;

        }

        else if (amount.value == 1)
        {
            if (sameSide.isOn)
            {
                // then place both balls on the same side
                col[1].transform.position = new Vector3(-4, 1.7f);
                col[2].transform.position = new Vector3(-4, -1.7f);
                col[1].velocity = Vector3.right;
                col[2].velocity = Vector3.right;

            }
            else
            {

                col[0].transform.position = Vector3.zero;
                col[1].transform.position = new Vector3(-4, 0);
                col[2].transform.position = new Vector3(4, 0);
                col[1].velocity = Vector3.right;
                col[2].velocity = Vector3.left;
            }
        }
        else if (amount.value == 2)
        {
            col[1].transform.position = new Vector3(-4, 1.7f);
            col[2].transform.position = new Vector3(-4, -1.7f);
            col[1].velocity = Vector3.right;
            col[2].velocity = Vector3.right;
            col[3].transform.position = new Vector3(4, 1.7f);
            col[4].transform.position = new Vector3(4, -1.7f);
            col[3].velocity = Vector3.left;
            col[4].velocity = Vector3.left;
        }
    }

    private void Center(unityBall[] col)
    {
        if (center.isOn)
        {
            col[0].velocity = Vector3.left;

        }
    }

    private void Offset(unityBall[] col)
    {
        if (offset.isOn)
        {
            // then need to still make sure they hit at the same time
            // give it a random place within a certain raduis and move towards the ball
            if (amount.value == 0)
            {
                float x = -4;
                float y = Random.Range(-2, 2);
                col[1].transform.position = new Vector3(x, y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(-1, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
            }
            if (amount.value == 1 && sameSide.isOn)
            {
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(x, -y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3(norm.x + x, -(norm.y + y)).normalized;
            }
            if (amount.value == 1 && !sameSide.isOn)
            {
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(-x, -y);
                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3(-(norm.x + x), -(norm.y + y)).normalized;
            }

            if (amount.value == 2)
            {
                // then combine the last two
                float x = -4;
                float y = Random.Range(1.6f, 3);
                col[1].transform.position = new Vector3(x, y);
                col[2].transform.position = new Vector3(x, -y);
                col[3].transform.position = new Vector3(-x, y);
                col[4].transform.position = new Vector3(-x, -y);

                Vector3 dir = col[0].transform.position - col[1].transform.position;
                float dis = dir.magnitude;
                Vector3 norm = dir / dis;
                x = Random.Range(0, 2);
                y = Random.Range(0, 1);
                norm = new Vector3(norm.x + x, norm.y + y);
                col[1].velocity = norm.normalized;
                col[2].velocity = new Vector3((norm.x + x), -(norm.y + y)).normalized;
                col[3].velocity = new Vector3(-(norm.x + x), (norm.y + y)).normalized;
                col[4].velocity = new Vector3(-(norm.x + x), -(norm.y + y)).normalized;

            }
        }

    }

    private void Direction(unityBall[] col)
    {
        // if the direction is horizontal leave it
        if (direction.value == 1)
        {
            foreach (unityBall c in col)
            {
                if (c == null)
                {
                    continue;
                }
                // flip the x and y for transform and velocity
                float x = c.transform.position.x;
                float y = c.transform.position.y;
                c.transform.position = new Vector3(y, x);
                x = c.velocity.x;
                y = c.velocity.y;
                c.velocity = new Vector3(y, x);
            }
        }
    }


}
