using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekChess
{
    public partial class Form1 : Form
    {   
        int ctrgerak=0;
        int tempx=-1;
        int tempy=-1;
        String turn = "White";
        String computerTurn = "Black";
        public Form1()
        {
            InitializeComponent();
        }

        Button[,]  b = new Button[4,8];
        realBoard boards  = new realBoard();
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button temp = new Button();
                    temp.Location = new Point(70*i, 70*j);
                    temp.Size = new Size(70, 70);
                    if ((i % 2 == 1 && j % 2 == 1)|| (i % 2 == 0 && j % 2 == 0))
                    {
                        temp.BackColor = Color.Black;
                    }
                    else
                    {
                        temp.BackColor = Color.White;
                    }
                    b[i, j] = temp;
                    b[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    b[i, j].Click += new System.EventHandler(this.ClickedButton);
                    b[i, j].Tag = i + "," + j;
                    this.Controls.Add(temp);
                }
            }

            refresh();



        }
        private void ClickedButton(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string[] indexes = btn.Tag.ToString().Split(',');
            //in indexes[0] you've got the i index and in indexes[1] the j index
            if (ctrgerak == 0 && turn != computerTurn)
            {
                tempy = Int32.Parse(indexes[1]);
                tempx = Int32.Parse(indexes[0]);
                ctrgerak = 1;

            }
            else if (ctrgerak == 1 && turn != computerTurn)
            {

                board temps = boards.boards[tempx, tempy];
                bool legalmove = checkgerakan(tempx, tempy, Int32.Parse(indexes[0]), Int32.Parse(indexes[1]), temps.nama,boards.boards);
                if (legalmove == true)
                {
                    if (boards.boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])].warna_team == "kosong")
                    {
                        boards.boards[tempx, tempy] = boards.boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])];
                        boards.boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])] = temps;
                    }
                    else
                    {
                        board kosong = new board();
                        boards.boards[tempx, tempy] = kosong;
                        boards.boards[Int32.Parse(indexes[0]), Int32.Parse(indexes[1])] = temps;
                    }
                    turn = computerTurn;
                    dfs(1, boards, 1);
                    turn = "White";
                }
                else
                {
                    MessageBox.Show("Gerakan Tidak Legal");
                }
                ctrgerak = 0;
                refresh();
               
            }
        }   
        public bool checkgerakan(int x_start, int y_start,int x_end,int y_end,string nama,board[,] tempboards)
        {
            board temps1 = tempboards[x_start, y_start];
            if (temps1.warna_team != tempboards[x_end, y_end].warna_team)
            {
                if (nama == "Knight")
                {

                    if (x_start - x_end == 2 && y_start - y_end == 1)
                    {
                        return true;
                    }
                    if (x_start - x_end == -2 && y_start - y_end == 1)
                    {
                        return true;
                    }
                    if (x_start - x_end == 2 && y_start - y_end == -1)
                    {
                        return true;
                    }
                    if (x_start - x_end == -2 && y_start - y_end == -1)
                    {
                        return true;
                    }
                    if (x_start - x_end == 1 && y_start - y_end == 2)
                    {
                        return true;
                    }
                    if (x_start - x_end == -1 && y_start - y_end == 2)
                    {
                        return true;
                    }
                    if (x_start - x_end == 1 && y_start - y_end == -2)
                    {
                        return true;
                    }
                    if (x_start - x_end == -1 && y_start - y_end == -2)
                    {
                        return true;
                    }
                    else { return false; }

                }
                if (nama == "Rook")
                {
                    bool sementara = true;
                    if (x_start - x_end == 0 && y_start-y_end!=0)
                    {

                        if (y_end - y_start > 0)
                        {
                            for (int i = y_start + 1; i < y_end; i++)
                            {
                                if (tempboards[x_start,i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }

                        }
                        else if (y_end - y_start < 0)
                        {
                            for (int i = y_end + 1; i < y_start; i++)
                            {
                                if (tempboards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (x_start - x_end != 0 && y_start - y_end == 0)
                    {
                        ;
                        if (x_end - x_start > 0)
                        {
                            
                            for (int i = x_start+1; i < x_end; i++)
                            {
                                if (tempboards[i, y_start].nama != "kosong")
                                {
                                    sementara= false;
                                }
                            }
                        }
                        else if(x_end - x_start <0)
                        {
                            for (int i = x_end + 1; i < x_start; i++)
                            {
                                if (tempboards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (nama == "Bishop")
                {
                    if (Math.Abs(x_start - x_end) == Math.Abs(y_start - y_end))
                    {
                        bool sementara = true;
                        if (y_end > y_start)
                        {
                            //bawah
                            int jarak = y_end - y_start;
                            if (x_end > x_start)
                            {   //bawah-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                   if(tempboards[i + x_start, i + y_start].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //bawah-kiri
                               
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[x_start-i, y_start+i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        if (y_end < y_start)
                        {
                            int jarak = y_start - y_end;
                            if (x_end > x_start)
                            {   //atas-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[i + x_start,  y_start-i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //atas-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[x_start - i, y_start -i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (nama == "Queen")
                {
                    if (Math.Abs(x_start - x_end) == Math.Abs(y_start - y_end))
                    {
                        bool sementara = true;
                        if (y_end > y_start)
                        {
                            //bawah
                            int jarak = y_end - y_start;
                            if (x_end > x_start)
                            {   //bawah-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[i + x_start, i + y_start].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //bawah-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[x_start - i, y_start + i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        if (y_end < y_start)
                        {
                            int jarak = y_start - y_end;
                            if (x_end > x_start)
                            {   //atas-kanan
                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[i + x_start, y_start - i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            if (x_end < x_start)
                            {   //atas-kiri

                                for (int i = 1; i < jarak; i++)
                                {
                                    if (tempboards[x_start - i, y_start - i].nama != "kosong")
                                    {
                                        sementara = false;
                                    }
                                }
                                if (sementara == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    else if (x_start - x_end == 0 && y_start - y_end != 0)
                    {
                        bool sementara = true;

                        if (y_end - y_start > 0)
                        {
                            for (int i = y_start + 1; i < y_end; i++)
                            {
                                if (tempboards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }

                        }
                        else if (y_end - y_start < 0)
                        {
                            for (int i = y_end + 1; i < y_start; i++)
                            {
                                if (tempboards[x_start, i].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (x_start - x_end != 0 && y_start - y_end == 0)
                    {
                        bool sementara = true;
                        
                        if (x_end - x_start > 0)
                        {

                            for (int i = x_start + 1; i < x_end; i++)
                            {
                                if (tempboards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        else if (x_end - x_start < 0)
                        {
                            for (int i = x_end + 1; i < x_start; i++)
                            {
                                if (tempboards[i, y_start].nama != "kosong")
                                {
                                    sementara = false;
                                }
                            }
                        }
                        if (sementara == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (nama == "King")
                {
                  
                    if (x_start - x_end != 0 || y_start - y_end != 0)
                    {
                        
                        if ((Math.Abs(x_start - x_end) == 1&& Math.Abs(y_end - y_start) < 2) || (Math.Abs(y_end - y_start) == 1&& Math.Abs(x_start - x_end) <2 ))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
            

        }
        public void refresh()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(boards.boards[i,j].nama=="Bishop"&& boards.boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.bishop;
                    }
                    else if (boards.boards[i,j].nama == "Bishop" && boards.boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.bishop_2;
                    }
                    else if (boards.boards[i, j].nama == "King" && boards.boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.king_2;
                    }
                    else if (boards.boards[i, j].nama == "King" && boards.boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.king;
                    }
                    else if (boards.boards[i, j].nama == "Knight" && boards.boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.knight;
                    }
                    else if (boards.boards[i, j].nama == "Knight" && boards.boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.knight_2;
                    }
                    else if (boards.boards[i, j].nama == "Queen" && boards.boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.queen_2;
                    }
                    else if (boards.boards[i, j].nama == "Queen" && boards.boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.queen;
                    }
                    else if (boards.boards[i, j].nama == "Rook" && boards.boards[i, j].warna_team == "Black")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.rook;
                    }
                    else if (boards.boards[i, j].nama == "Rook" && boards.boards[i, j].warna_team == "White")
                    {
                        b[i, j].BackgroundImage = ProyekChess.Properties.Resources.rook_2;
                    }
                    else
                    {
                        b[i, j].BackgroundImage = base.BackgroundImage;
                    }
                }
            }
        }
        public board[,] Getstateboard()
        {
            return boards.boards;
        }
        public List<realBoard> getAllPossibleMove(realBoard boards){
            List<realBoard> possibleMove = new List<realBoard>();
            for (int i = 0;i < 8;i++){
                for(int j = 0;j < 4;j++){
                    if(boards.boards[j,i].warna_team == turn){
                        for(int k = 0;k < 8;k++){
                            for(int l = 0;l < 4;l++){
                                realBoard boardtemp = new realBoard(boards.boards);
                                board firststep = boardtemp.boards[j, i];
                                if (checkgerakan(j, i, l, k, boardtemp.boards[j, i].nama, boardtemp.boards))
                                {
                                    if (boardtemp.boards[l, k].warna_team == "kosong")
                                    {
                                        boardtemp.boards[j, i] = boardtemp.boards[l, k];
                                        boardtemp.boards[l, k] = firststep;
                                        boardtemp.val = 0;
                                    }
                                    else
                                    {
                                        if(boardtemp.boards[l,k].nama == "King")
                                        {
                                            boardtemp.val = int.MaxValue;
                                        }
                                        if (boardtemp.boards[l, k].nama == "Queen")
                                        {
                                            boardtemp.val = 15;
                                        }
                                        if (boardtemp.boards[l, k].nama == "Knight")
                                        {
                                            boardtemp.val = 3;
                                        }
                                        if (boardtemp.boards[l, k].nama == "Bishop")
                                        {
                                            boardtemp.val = 3;
                                        }
                                        if (boardtemp.boards[l, k].nama == "Rook")
                                        {
                                            boardtemp.val = 5;
                                        }
                                        board kosong = new board();
                                        boardtemp.boards[j, i] = kosong;
                                        boardtemp.boards[l, k] = firststep; 
                                    }
                                    possibleMove.Add(boardtemp);
                                }
                            }
                        }
                    }
                }
            }
            return possibleMove;
        }
        public  board[,] newBoard()
        {
            board[,] tempboard = new board[4, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tempboard[i, j] = new board();
                }
            }
            tempboard[0, 0].nama = "Rook";
            tempboard[3, 0].nama = "Rook";
            tempboard[0, 7].nama = "Rook";
            tempboard[3, 7].nama = "Rook";

            tempboard[0, 0].bobotbidak = 5;
            tempboard[3, 0].bobotbidak = 5;
            tempboard[0, 7].bobotbidak = 5;
            tempboard[3, 7].bobotbidak = 5;

            tempboard[1, 0].nama = "King";
            tempboard[1, 0].bobotbidak = 15;

            tempboard[2, 0].nama = "Queen";
            tempboard[2, 0].bobotbidak = 10;

            tempboard[1, 7].nama = "King";
            tempboard[1, 7].bobotbidak = 15;
            tempboard[2, 7].nama = "Queen";
            tempboard[2, 7].bobotbidak = 15;

            tempboard[0, 1].nama = "Knight";
            tempboard[3, 1].nama = "Knight";
            tempboard[0, 6].nama = "Knight";
            tempboard[3, 6].nama = "Knight";

            tempboard[0, 1].bobotbidak = 3;
            tempboard[3, 1].bobotbidak = 3;
            tempboard[0, 6].bobotbidak = 3;
            tempboard[3, 6].bobotbidak = 3;


            tempboard[1, 1].nama = "Bishop";
            tempboard[2, 1].nama = "Bishop";
            tempboard[1, 6].nama = "Bishop";
            tempboard[2, 6].nama = "Bishop";

            tempboard[1, 1].bobotbidak = 3;
            tempboard[2, 1].bobotbidak = 3;
            tempboard[1, 6].bobotbidak = 3;
            tempboard[2, 6].bobotbidak = 3;
            for (int i = 0; i < 4; i++)
            {
                tempboard[i, 0].warna_team = "Black";
                tempboard[i, 1].warna_team = "Black";

                tempboard[i, 6].warna_team = "White";
                tempboard[i, 7].warna_team = "White";
            }
            return tempboard;
        }
        public int dfs(int level,realBoard currentBoard,int maxlevel)
        {
            List<realBoard> possibleMove = getAllPossibleMove(currentBoard);
            List<int> tempVal = new List<int>();
            if (level == 1)
            {
                for (int i = 0; i < possibleMove.Count; i++)
                {
                    tempVal.Add(currentBoard.val + dfs(2, possibleMove[i], 3));
                }
                for (int i = 0; i < possibleMove.Count; i++)
                {
                    MessageBox.Show(tempVal[i].ToString());
                }
                int max = tempVal.Max();
                int index = -1;
                for (int i = 0; i < tempVal.Count; i++)
                {
                    if (tempVal[i] == max)
                    {
                        index = i;
                    }
                }
                if(possibleMove.Count > 0)
                {
                    boards.boards = possibleMove[index].boards;
                }
                
            }
            else if(level <= maxlevel)
            {
                if (level % 2 == 0)
                {
                    for (int i = 0; i < possibleMove.Count; i++)
                    {
                        tempVal.Add(currentBoard.val - dfs(level + 1, possibleMove[i], maxlevel));
                    }
                    int min = tempVal.Min();
                    return min;
                }
                else
                {
                    for (int i = 0; i < possibleMove.Count; i++)
                    {
                        tempVal.Add(currentBoard.val + dfs(level + 1, possibleMove[i], maxlevel));
                    }
                    int max = tempVal.Max();
                    return max;
                }
            }
            return 0;
        }

        int ctrtambah = 0;
        List<realBoard> cekmove;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ctrtambah == 0) {
                cekmove = getAllPossibleMove(boards);
            }
            boards = cekmove[ctrtambah];
            ctrtambah++;
            refresh();
        }
    }
}
