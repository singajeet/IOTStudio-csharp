/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 20:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Designer.Others
{
	/// <summary>
	/// Description of GridCell.
	/// </summary>
	public class GridCell
	{
		public int X { get; set; }
		public int Y{ get; set; }
		public int CellSize { get; set; }
		
		public GridCell()
		{
		}
		
		public GridCell(int x, int y, int size)
		{
			X = x;
			Y = y;
			CellSize = size;
		}
		
		public bool IsPointInBounds(int x, int y)
		{
			if(CellSize<=0)
				throw new InvalidOperationException(string.Format("Invalid value of CellSize provided: {0}", CellSize));
			
			if (x >= X && x <= (X + CellSize) && y >= Y && y <= (Y + CellSize)) {
				return true;
			}
			
			return false;
		}
	}
}
