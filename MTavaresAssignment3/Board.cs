/* MTavaresAssignment3
 * Board.cs
 * Assignment 3
 *  Revision History
 *      Margaret Tavares, 2016.10.30
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MTavaresAssignment3
{
    /// <summary>
    /// The Class for the N-Puzzle Game.
    /// </summary>
    public partial class Board : Form
    {
        public const int STARTX = 104;
        public const int STARTY = 120;
        public const int WIDTH = 80;
        public const int HEIGHT = 80;
        public int numOfCols;
        public int numOfRows;
        public Tile[,] btnTile;
        Tile emptyTile;
        public const int SCRAMBLE = 33;

        /// <summary>
        /// The Constructor for the N-Puzzle Game.
        /// </summary>
        public Board()
        {
            InitializeComponent();
        }
        /// <summary>
        /// After user inputs number of rows and columns, then clicks "Generate", game will 
        /// populate tiles in an array according to user's specifications.
        /// Array is also scrambled to create a puzzle for the user to solve.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            bool error = false;
            string errMessage = string.Format("");

            try
            {
                numOfRows = int.Parse(txtRows.Text);
                numOfCols = int.Parse(txtColumns.Text);
            }
            catch (Exception)
            {
                if (txtRows.Text == "")
                {
                    errMessage += "Please input a number for rows. \n";
                }
                if (txtColumns.Text == "")
                {
                    errMessage += "Please input a number for columns. \n";
                }

                if (errMessage != "")
                {
                    MessageBox.Show(errMessage);
                }
                return;
            }         
                        
            if (error == false)
            {
                RemoveAllTiles();
                btnTile = new Tile[numOfRows, numOfCols];
                int count = 1;
                
                for (int i = 0; i < numOfRows; ++i)
                {
                    for (int j = 0; j < numOfCols; ++j)
                    {
                        btnTile[i, j] = new Tile();
                        btnTile[i, j].Size = new Size(WIDTH, HEIGHT);
                        btnTile[i, j].Location = new Point(j * WIDTH + STARTX, i * HEIGHT + STARTY);
                        btnTile[i, j].Text = count++.ToString();
                        btnTile[i, j].Click += new EventHandler (btnMove);
                        btnTile[i, j].IndexRow = i;
                        btnTile[i, j].IndexCol = j;
                        btnTile[i, j].BackgroundImage = Properties.Resources.backgroundImage;       
                        this.Controls.Add(btnTile[i, j]);
                    }
                }
                btnTile[numOfRows - 1, numOfCols - 1].Visible = false;
                emptyTile = btnTile[numOfRows - 1, numOfCols - 1];
                for (int i = 0; i < SCRAMBLE; i++)
                {
                    Randomize();
                }              
            }
        }
        /// <summary>
        /// Checks to see if the tile that is not visible can be moved in a direction, then moves 
        /// it after a user clicks on specified tile.
        /// Specified tile then moves into non-visible tile's location.
        /// </summary>
        /// <param name="sender">Tile that has been clicked by the user that moves in 
        /// the direction of the empty square next to it.</param>
        /// <param name="Click"></param>
        private void btnMove(object sender, EventArgs Click)
        {
            Tile t = (Tile)sender;
            Tile z;
            try
            {
                z = btnTile[t.IndexRow + 1, t.IndexCol];
                if (z.Visible == false)
                {
                    MoveTile(t, z);
                    WinCheck();
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                z = btnTile[t.IndexRow, t.IndexCol + 1];
                if (z.Visible == false)
                {
                    MoveTile(t, z);
                    WinCheck();
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                z = btnTile[t.IndexRow, t.IndexCol - 1];
                if (z.Visible == false)
                {
                    MoveTile(t, z);
                    WinCheck();
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                z = btnTile[t.IndexRow - 1, t.IndexCol];
                if (z.Visible == false)
                {
                    MoveTile(t, z);
                    WinCheck();
                    return;
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Method that makes the tiles move into a new position, relocating each tile to a different tile's 
        /// location to make a puzzle for the user to solve.
        /// </summary>
        private void Randomize()
        {            
            Random r = new Random();
            int start = r.Next(0,4);

            try
            {
                if (start == 0)
                {
                    Tile z = btnTile[emptyTile.IndexRow - 1, emptyTile.IndexCol];
                    MoveTile(z, emptyTile);
                    z = btnTile[emptyTile.IndexRow, emptyTile.IndexCol - 1];
                    MoveTile(z, emptyTile);
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                if (start == 1)
                {
                    Tile z = btnTile[emptyTile.IndexRow, emptyTile.IndexCol - 1];
                    MoveTile(z, emptyTile);
                    z = btnTile[emptyTile.IndexRow +1, emptyTile.IndexCol];
                    MoveTile(z, emptyTile);
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                if (start == 2)
                {
                    Tile z = btnTile[emptyTile.IndexRow, emptyTile.IndexCol + 1];
                    MoveTile(z, emptyTile);
                    return;
                }
            }
            catch (Exception) { }
            try
            {
                if (start == 3)
                {
                    Tile z = btnTile[emptyTile.IndexRow + 1, emptyTile.IndexCol];
                    MoveTile(z, emptyTile);
                    return;
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Method that determines if all tiles are positioned in the correct sequence.
        /// Once all tiles have been moved to the correct sequence, a message will appear 
        /// stating they have won.
        /// </summary>
        private void WinCheck()
        {
            int count = 1;
                       
            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfCols; j++)
                {
                    if (btnTile[i, j].Text != count.ToString())
                    {
                        return;
                    }
                    count++;
                }
            }
            MessageBox.Show("You have won the game!", "Congratulations! ", MessageBoxButtons.OK, 
                MessageBoxIcon.Exclamation);
            RemoveAllTiles();
        }
        /// <summary>
        /// Method that makes all current tiles on the field non-visible, clearing them from the game.
        /// </summary>
        private void RemoveAllTiles()
        {
            foreach (object item in this.Controls)
            {
                if (item.GetType().Equals(typeof(Tile)))
                {
                    Tile gameTile = item as Tile;
                    gameTile.Visible = false;
                }
            }
        }
        /// <summary>
        /// Swaps two tiles with another to allow user to move tile selected through the game.
        /// </summary>
        /// <param name="a">Tile user chose</param>
        /// <param name="b">Tile that gets swapped with tile user chose</param>
        private void MoveTile(Tile a, Tile b)
        {
            Tile tempTile = new Tile();
            tempTile.Location = a.Location;
            a.Location = b.Location;
            b.Location = tempTile.Location;
            btnTile[a.IndexRow, a.IndexCol] = b;
            btnTile[b.IndexRow, b.IndexCol] = a;
            tempTile.IndexRow = a.IndexRow;
            tempTile.IndexCol = a.IndexCol;
            a.IndexRow = b.IndexRow;
            a.IndexCol = b.IndexCol;
            b.IndexRow = tempTile.IndexRow;
            b.IndexCol = tempTile.IndexCol;
        }
        /// <summary>
        /// Cylces through each tile in array and writes it to a save file.
        /// </summary>
        /// <param name="filename"> Name of the file.</param>
        private void doSave(Stream filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            writer.WriteLine(numOfRows);
            writer.WriteLine(numOfCols);
            foreach (Tile Tile in btnTile)
            {
                writer.WriteLine(Tile.Text);
            }           
            writer.Close();
            filename.Close();
        }
        /// <summary>
        /// Clears all tiles from current screen, reads text from text file to an integer, 
        /// converts it into the array, displays it on game screen.
        /// </summary>
        /// <param name="filename">Loads the text file selected from previous save.</param>
        private void doLoad(Stream filename)
        {
            RemoveAllTiles();
            StreamReader reader = new StreamReader(filename);
            numOfRows = Convert.ToInt32(reader.ReadLine());
            numOfCols = Convert.ToInt32(reader.ReadLine());
            for (int i = 0; i < numOfRows; ++i)
            {
                for (int j = 0; j < numOfCols; ++j)
                {
                    btnTile[i, j] = new Tile();
                    btnTile[i, j].Size = new Size(WIDTH, HEIGHT);
                    btnTile[i, j].Location = new Point(j * WIDTH + STARTX, i * HEIGHT + STARTY);
                    btnTile[i, j].Text = reader.ReadLine();
                    btnTile[i, j].Click += new EventHandler(btnMove);
                    btnTile[i, j].IndexRow = i;
                    btnTile[i, j].IndexCol = j;
                    this.Controls.Add(btnTile[i, j]);
                    if (int.Parse(btnTile[i, j].Text) == numOfRows * numOfCols)
                    {
                        btnTile[i, j].Visible = false;
                    }
                }
            }
            reader.Close();
            filename.Close();
        }
        /// <summary>
        /// When "Save" button is clicked, window will pop up allowing user to save file. 
        /// If error occurs in which game is unable to be saved, an error message will 
        /// be displayed to user.
        /// </summary>
        /// <param name="sender">Save Button</param>
        /// <param name="e">Save current game to a text file.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog result = new SaveFileDialog();
            switch (result.ShowDialog())
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        Stream filename = result.OpenFile();
                        doSave(filename);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to save file correctly.");
                    }
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// When "Load" button is clicked, window will pop up allowing user to specify 
        /// what previous save file they would like to open. If error occurs in which game 
        /// is unable to be loaded, an error message will appear.
        /// </summary>
        /// <param name="sender">Load Button</param>
        /// <param name="e">Load saved file</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog result = new OpenFileDialog();
            switch (result.ShowDialog())
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        Stream filename = result.OpenFile();
                        doLoad(filename);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to load file correctly.");
                    }
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }

        private void txtRows_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
