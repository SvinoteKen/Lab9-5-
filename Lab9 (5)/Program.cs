using Lab9__5_.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9__5_
{
    class Program
    {
        
        public struct Game
        {
            public int scoreFirstTeam;
            public int scoreSecondTeam;
            public string firstTeam;
            public string secondTeam;
            public int penaltsFirstTeam;
            public int penaltsSecondTeam;


            public string ConvertToString()
            {
                if (penaltsFirstTeam == -1)
                {
                   string result = firstTeam + " " + secondTeam + " " + scoreFirstTeam + "-" + scoreSecondTeam;
                   return result;
                }
                else
                {
                   string result = firstTeam + " " + secondTeam + " " + scoreFirstTeam + "-" + scoreSecondTeam+"("+penaltsFirstTeam+":"+penaltsSecondTeam+")";
                   return result;
                }
               
            }

        }

        static void Main(string[] args)
        {
            BinaryTree<Game> tree = new BinaryTree<Game>();
            string[] country = { "COL", "URU", "BRA", "CHI", "FRA", "NIG", "GER", "ALG", "NED", "MEX", "CRC", "GRE", "ARG", "SWI", "BEL", "USA" };
            Game[] games = new Game[8];
            Random random = new Random();
            for (int i = 0, j = 0; i < games.Length; i++, j += 2)
            {
                Game game;
                game.firstTeam = country[j];
                game.secondTeam = country[j + 1];
                int A = -1, B = -1;
                game.penaltsFirstTeam = A;
                game.penaltsSecondTeam = B;
                if (random.NextDouble() < 0.75)
                {
                    game.scoreFirstTeam = random.Next(0, 3);
                    game.scoreSecondTeam = random.Next(0, 3);
                    CheckDraw(game.scoreFirstTeam,game.scoreSecondTeam,game,random);
                    /*if (game.scoreFirstTeam == game.scoreSecondTeam)
                    {
                        
                        Draw(random, ref A,ref B);
                        game.penaltsFirstTeam = A;
                        game.penaltsSecondTeam = B;
                    }*/
                }
                else
                {
                    game.scoreFirstTeam = random.Next(0, 8);
                    game.scoreSecondTeam = random.Next(0, 8);
                    if (game.scoreFirstTeam == game.scoreSecondTeam)
                    {

                        Draw(random, ref A, ref B);
                        game.penaltsFirstTeam = A;
                        game.penaltsSecondTeam = B;
                    }
                }
                games[i] = game;
            }
            Game empty;
            empty.firstTeam = "";
            empty.secondTeam = "";
            empty.scoreFirstTeam = 0;
            empty.scoreSecondTeam = 0;
            empty.penaltsFirstTeam = 0;
            empty.penaltsSecondTeam = 0;
            BinaryTreeNode<Game>[] node = new BinaryTreeNode<Game>[15];
            node[0] = tree.AddRoot(empty);
            node[1] = tree.AddLeft(node[0], empty);
            node[2] = tree.AddRight(node[0], empty);
            for (int i = 1, j = 3; i < (node.Length - 1) / 2; i++)
            {
                node[j] = tree.AddLeft(node[i], empty);
                j++;
                node[j] = tree.AddRight(node[i], empty);
                j++;
            }

            for (int i = node.Length - 1, j = games.Length - 1; j > -1; i--, j--)
            {
                node[i].data = games[j];
            }

            for (int i = 6; i > -1; i--)
            {
                node[i].data = ResultOfNextGame(node[i].left.data, node[i].right.data, random);
            }
            PreOrderTraversal(node[0]);

        }
        static Game ResultOfNextGame(Game game, Game game1, Random random) 
        {
            int A = -1, B = -1;
            game.penaltsFirstTeam = A;
            game.penaltsSecondTeam = B;
            string FirstTeam = "";
            string SecondTeam = "";
            if (game.scoreFirstTeam > game.scoreSecondTeam)
            {
                FirstTeam = game.firstTeam;
            }
            if (game.scoreFirstTeam < game.scoreSecondTeam)
            {
               FirstTeam = game.secondTeam;
            }
            if(game.scoreFirstTeam==game.scoreSecondTeam)
            {
                Draw(random, ref A, ref B);
                game.penaltsFirstTeam = A;
                game.penaltsSecondTeam = B;
                if (A > B) { FirstTeam = game.firstTeam;  } else { FirstTeam = game.secondTeam; }
            }
            if (game1.scoreFirstTeam > game1.scoreSecondTeam)
            {
                SecondTeam = game1.firstTeam;
            }
            if(game1.scoreFirstTeam < game1.scoreSecondTeam)
            {
                SecondTeam = game1.secondTeam;
            }
            if (game1.scoreFirstTeam == game1.scoreSecondTeam)
            {
                Draw(random, ref A, ref B);
                game1.penaltsFirstTeam = A;
                game1.penaltsSecondTeam = B;
                if (A > B) { SecondTeam = game1.firstTeam;  } else { SecondTeam = game1.secondTeam; }
            }
            Game NextMatch;
            NextMatch.firstTeam = FirstTeam;
            NextMatch.secondTeam = SecondTeam;
            NextMatch.penaltsFirstTeam = -1;
            NextMatch.penaltsSecondTeam = -1;
            if (random.NextDouble() < 0.75)
            {
                NextMatch.scoreFirstTeam = random.Next(0, 3);
                NextMatch.scoreSecondTeam = random.Next(0, 3);
                if (NextMatch.scoreFirstTeam == NextMatch.scoreSecondTeam)
                {
                    Draw(random, ref A, ref B);
                    NextMatch.penaltsFirstTeam = A;
                    NextMatch.penaltsSecondTeam = B;
                }
            }
            else
            {
                NextMatch.scoreFirstTeam = random.Next(0, 8);
                NextMatch.scoreSecondTeam = random.Next(0, 8);
                if ( NextMatch.scoreFirstTeam == NextMatch.scoreSecondTeam)
                {
                    Draw(random, ref A, ref B);
                    NextMatch.penaltsFirstTeam = A;
                    NextMatch.penaltsSecondTeam = B;
                }
            }
            return NextMatch;
        }
        static void PreOrder(BinaryTreeNode<Game> node, int level = 0) 
        {
            if (node != null)
            {
                string result = node.data.ConvertToString();
                for (int i = 0; i < level; i++)
                    result = "   " + result;
                Console.WriteLine(result);
                PreOrder(node.left,level + 1);
                PreOrder(node.right,level + 1);
                
            }
        }
        static void PreOrderTraversal(BinaryTreeNode<Game> root)
        {
            PreOrder(root);
        }
        static void Draw(Random random, ref int A, ref int B)
        {
            A = random.Next(0, 5);
            B = random.Next(0, 5);
            int A2, B2;
            if (A == B)
            {
                while (A == B)
                {
                    A2 = random.Next(0, 5);
                    B2 = random.Next(0, 5);
                    A += A2;
                    B += B2;
                }
            }
            
        }
        static void CheckDraw(int first,int second,Game game,Random random)
        {
            int A = -1, B = -1;
            if (first == second)
            {

                Draw(random, ref A, ref B);
                game.penaltsFirstTeam = A;
                game.penaltsSecondTeam = B;
            }
        }
    }
}
