/* MTavaresAssignment3
 * Tile.cs
 * Assignment 3
 *  Revision History
 *      Margaret Tavares, 2016.10.30
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTavaresAssignment3
{
    /// <summary>
    /// This has the get and set information of where the Tile is in the array.
    /// Makes information public so game can access it.
    /// </summary>
    public class Tile : Button
    {
        private int indexRow;
        private int indexCol;
        /// <summary>
        /// Gets and Sets information of the tile's row index.
        /// </summary>
        public int IndexRow
        {
            get
            {
                return indexRow;
            }

            set
            {
                indexRow = value;
            }
        }
        /// <summary>
        /// Gets and Sets information of the tile's column index.
        /// </summary>
        public int IndexCol
        {
            get
            {
                return indexCol;
            }

            set
            {
                indexCol = value;
            }
        }
    }
}
