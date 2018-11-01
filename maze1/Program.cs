using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication_maze
{

    class Program
    {
        public class MyPoint
        {
            public MyPoint parent { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public MyPoint(int a, int b)
            {
                this.X = a;
                this.Y = b;
            }
        }

        static void PrintPath(MyPoint p)
        {
            
            if (p.parent != null)
            {
                PrintPath(p.parent);
                Console.Write("(" + p.X + "," + p.Y + ")-->");
            }
        }
        static void maze_BFS(MyPoint p, int[,] data,int x_start,int x_end,int y_start,int y_end)
        {
            Queue q = new Queue();
            data[x_start, y_start] = -1;
            q.Enqueue(p);
            while (q.Count > 0)
            {
                MyPoint q_p = (MyPoint)q.Dequeue();
                for (int i = -1; i < 2; i++) //遍历可以到达的节点
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if ((q_p.X + i >= 0) && (q_p.X + i <= 9) && (q_p.Y + j >= 0) && (q_p.Y + j <= 9) && (q_p.X + i == q_p.X || q_p.Y == q_p.Y + j)) 
                        {
                            if (data[q_p.X + i, q_p.Y + j] == 0)
                            {
                                if (q_p.X + i == 9 && q_p.Y + j == 9)  
                                {
                                    Console.Write("("+x_start+","+y_start+")-->");     
                                     PrintPath(q_p); //递归输出路径  
                                    Console.Write("(" + x_end + "," + y_end + ")");
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    MyPoint temp = new MyPoint(q_p.X + i, q_p.Y + j);   
                                    data[q_p.X + i, q_p.Y + j] = -1;
                                    temp.parent = q_p;
                                    q.Enqueue(temp);
                                }
                            }
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string[] str = File.ReadAllLines(@"C:\Users\ICBC\Desktop\maze.txt", Encoding.Default);//Encoding.Default使用系统默认编码
            int mm = 10;
            int nn = 10;
            string[,] cost = new string[mm, nn];
            for (int i = 0; i < 10; i++)
            {
                str[i] = str[i].Replace("	", "");
            }
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    cost[i, j] = str[i].Substring(j, 1);

                }
            }
            int x_start = 0, x_end = 0;
            int y_start = 0, y_end = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (cost[i, j] == "S")
                    {
                        x_start = i;
                        y_start = j;
                    }
                    if (cost[i, j] == "E")
                    {
                        x_end = i;
                        y_end = j;
                    }
                }
            }
            cost[x_start, y_start] = "0";
            cost[x_end, y_end] = "0";
            int[,] maze = new int[10, 10];
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    maze[i, j] = int.Parse(cost[i, j]);
                }
            }
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
            /////////////////////////////////////////////////////////////////////////////////////////
            MyPoint bfs = new MyPoint(x_start, y_start);
            bfs.parent = null;
            maze_BFS(bfs, maze,x_start,x_end,y_start,y_end);
        }
    }
}