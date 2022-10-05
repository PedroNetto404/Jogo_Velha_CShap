using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_da_velha
{
    public class Board
    {
        const int TAM = 3; 
        public int[,] PositionMap { get; set; }
        public CharactersValues CharactersValues { get; set; }

        public Board()
        {
            int count = 1;
            PositionMap = new int[TAM, TAM]; 
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    PositionMap[i, j] = count;
                    count++;
                }
            }

            CharactersValues = new CharactersValues(); 
        }
        public bool TryUpdateBoard(int playPosition, CharactersKeys characterKey)
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (PositionMap[i, j] == playPosition)
                    {
                        PositionMap[i, j] = (int)characterKey;
                        return true; 
                    }  
                }
            }
            return false; 
        }

        public PossibleGameEndings IsOver(CharactersKeys currentPlayerCharacterKey)
        {
            if (IsDraw()) 
                return PossibleGameEndings.Draw;

            if (IsCurrentPlayerVictory(currentPlayerCharacterKey))
                return PossibleGameEndings.Victory;

            return PossibleGameEndings.NotEnding;
        }
        private bool IsDraw()
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (PositionMap[i, j] != (int)CharactersKeys.Crusader ||
                        PositionMap[i, j] != (int)CharactersKeys.Ball)
                        return false;
                }
            }
            return true;
        }

        private bool IsCurrentPlayerVictory(CharactersKeys currentPlayerCharacterKey)
        {
            string victoryPattern = string.Join("",
                Enumerable.Repeat(CharactersValues.Characters[(int)currentPlayerCharacterKey], TAM)); 

            string linePattern = "",
                colunmPattern = "",
                DiagPrincPattern = "",
                DiagSecPattern = ""; 

            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    try
                    {
                        if (PositionMap[i, j] == (int)currentPlayerCharacterKey)
                        {
                            linePattern += CharactersValues.Characters[PositionMap[i, j]];
                            if (i == j)
                                DiagPrincPattern += CharactersValues.Characters[PositionMap[i, j]];
                            if (i + j == TAM - 1)
                                DiagSecPattern += CharactersValues.Characters[PositionMap[i, j]];
                        }
                           
                        if (PositionMap[j,i] == (int)currentPlayerCharacterKey) 
                            colunmPattern += CharactersValues.Characters[PositionMap[j, i]]; 

                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                   
                }
                if (linePattern == victoryPattern || colunmPattern == victoryPattern)
                    return true;
                linePattern = "";
                colunmPattern = ""; 
            }

            if (DiagSecPattern == victoryPattern || DiagPrincPattern == victoryPattern)
                return true;

            return false; 
        }

        public void RenderImage()
        {

            int count = 1;
            string image = GetRenderTemplate();
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (PositionMap[i, j] == 10 || PositionMap[i, j] == 11)
                    {
                        string boardCharacter = CharactersValues.Characters[PositionMap[i, j]];
                        image = image.Replace(count.ToString(), boardCharacter);
                    }
                    count++;
                }
            }
            Console.WriteLine(image);
        }

        private string GetRenderTemplate()
        {
            return @"
 _______   _______   _______
|       | |       | |       |
|   1   | |   2   | |   3   |
|_______| |_______| |_______|
 _______   _______   _______
|       | |       | |       |
|   4   | |   5   | |   6   |
|_______| |_______| |_______|
 _______   _______   _______
|       | |       | |       |
|   7   | |   8   | |   9   |
|_______| |_______| |_______|
"; 
        }
    }
}
