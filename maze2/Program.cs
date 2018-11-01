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
        public class Point
        {
            
            public int X { get; set; }
            public int Y { get; set; }

           
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
            
            Point point = new Point();
            int N = 10, M = 10;   //maze  hangleishu
            int[,] d = new int[N,M];
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };

            Queue<Point> que = new Queue<Point>();
            point.X = x_start;
            point.Y = y_start;
            que.Enqueue(point);
            d[x_start, y_start] = 0;
            //MyPoint p = new MyPoint(0,0);

            while (que.Count() > 0)
            {
                Point p = que.Peek();
                if (p.X == x_end&&p.Y==y_end)
                {
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    int xx = p.X + dx[i];
                    int yy = p.Y + dy[i];
                    if (xx >= 0 && xx <= N && yy >= 0 && yy <= M && maze[xx, yy] != 0 && d[xx, yy] == 0)
                    {
                        p.X = xx;
                        p.X = yy;
                        que.Enqueue(p);
                        d[xx, yy] = d[p.X, p.Y] + 1;
                    }
                }
               
            }
            Console.WriteLine(d[x_end, y_end]);



        }
    }
}