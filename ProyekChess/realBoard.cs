using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyekChess
{
    public class realBoard
    {
        public board[,] boards { get; set; }
        public int val { get; set; }
        public realBoard()
        {
            boards = new board[4, 8];
            this.val = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.boards[i, j] = new board();
                    this.boards[i, j].bobotbidak = 0;
                }
            }
            boards[0, 0].nama = "Rook";
            boards[3, 0].nama = "Rook";
            boards[0, 7].nama = "Rook";
            boards[3, 7].nama = "Rook";

            boards[0, 0].bobotbidak = 5;
            boards[3, 0].bobotbidak = 5;
            boards[0, 7].bobotbidak = 5;
            boards[3, 7].bobotbidak = 5;

            boards[1, 0].nama = "King";
            boards[1, 0].bobotbidak = 15;

            boards[2, 0].nama = "Queen";
            boards[2, 0].bobotbidak = 10;

            boards[1, 7].nama = "King";
            boards[1, 7].bobotbidak = 1000;
            boards[2, 7].nama = "Queen";
            boards[2, 7].bobotbidak = 15;

            boards[0, 1].nama = "Knight";
            boards[3, 1].nama = "Knight";
            boards[0, 6].nama = "Knight";
            boards[3, 6].nama = "Knight";

            boards[0, 1].bobotbidak = 3;
            boards[3, 1].bobotbidak = 3;
            boards[0, 6].bobotbidak = 3;
            boards[3, 6].bobotbidak = 3;


            boards[1, 1].nama = "Bishop";
            boards[2, 1].nama = "Bishop";
            boards[1, 6].nama = "Bishop";
            boards[2, 6].nama = "Bishop";

            boards[1, 1].bobotbidak = 3;
            boards[2, 1].bobotbidak = 3;
            boards[1, 6].bobotbidak = 3;
            boards[2, 6].bobotbidak = 3;
            for (int i = 0; i < 4; i++)
            {
                boards[i, 0].warna_team = "Black";
                boards[i, 1].warna_team = "Black";

                boards[i, 6].warna_team = "White";
                boards[i, 7].warna_team = "White";
            }
        }
        public realBoard(board[,] board)
        {
            this.boards = new board[4, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.boards[i, j] = new board();
                    this.boards[i, j].nama = board[i, j].nama;
                    this.boards[i, j].warna_team = board[i, j].warna_team;
                    this.boards[i, j].bobotbidak = board[i, j].bobotbidak;
                }
            }
            this.val = 0;
        }
        public int countVal(String warna)
        {
            int tempval = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(this.boards[i,j].warna_team == warna)
                    {
                        tempval += this.boards[i, j].bobotbidak;
                    }
                    else
                    {
                        tempval -= this.boards[i, j].bobotbidak;
                    }
                }
            }
            return tempval;
        }
        public realBoard Clone()
        {
            return (realBoard) this.MemberwiseClone();
        }
    }
}
