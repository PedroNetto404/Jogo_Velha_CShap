using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_da_velha
{
    public enum PossibleGameEndings
    {
        Draw,
        Victory,
        NotEnding
    }
    public class Game
    {
        private const int INVALID_POSITION = -1;
        public Board BoardGame { get; set; }
        public Player Player { get; set; }
        public Player Opponent { get; set; }
        public Player CurrentPlayer;
        public Game()
        {
            BoardGame = new Board();
            Player = new Player();
            Opponent = new Player();
        }
        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Blue; 
            Console.WriteLine("\nBem vindo ao jogo da velha!\n");
            ConfigurePlayers();
            ShowGameBoard();

            while (true)
            {
                int playerMove = GetPlayerMove();

                if (playerMove == INVALID_POSITION)
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Escolha apenas numeros");
                    continue;
                }

                bool moveMade = BoardGame.TryUpdateBoard(playerMove, CurrentPlayer.CharacterKey);

                if (!moveMade)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Posicao indisponivel...tente novamente");
                    continue;

                }

                var gameEnd = BoardGame.IsOver(CurrentPlayer.CharacterKey);

                if (gameEnd == PossibleGameEndings.NotEnding)
                {
                    ChangeShift();
                    ShowGameBoard();
                    continue;
                }
                    

                EndGame(gameEnd);
                break;
            }
        }



        private void EndGame(PossibleGameEndings possibleGameEndings)
        {
            if (possibleGameEndings == PossibleGameEndings.Draw)
            {

                ShowGameBoard();
                Console.ResetColor();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Fim de jogo! \n{Player.Name} e {Opponent.Name}, voces empataram.");
            }
            else if (possibleGameEndings == PossibleGameEndings.Victory)
            {
                ShowGameBoard();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Fim de jogo. \n{CurrentPlayer.Name} Voce ganhou!!!.");
                Console.ResetColor();
            }
        }

        private void ShowGameBoard()
        {
            Console.SetCursorPosition(50, 10);
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            BoardGame.RenderImage();
        }

        private void ChangeShift()
        {
            CurrentPlayer = CurrentPlayer == Player ? Opponent : Player;
        }

        private int GetPlayerMove()
        {
            int playerMove = -1;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{CurrentPlayer.Name} escolha a posicao da sua jogada...: ");
            try
            {
                Console.ForegroundColor = ConsoleColor.White; 
                playerMove = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Você deve escolher apenas os numeros validos...");
            }

            return playerMove;
        }

        private void ConfigurePlayers()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Entre com o nome do jogador: ");
            Player.Name = Console.ReadLine();
            Player.CharacterKey = CharactersKeys.Crusader;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Entre com o nome do oponente: ");
            Opponent.Name = Console.ReadLine();
            Opponent.CharacterKey = CharactersKeys.Ball;

            CurrentPlayer = Player;
        }
    }
}
