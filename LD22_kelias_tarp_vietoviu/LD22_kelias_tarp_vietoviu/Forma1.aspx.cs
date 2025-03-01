using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace LD22_kelias_tarp_vietoviu
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Table1.Visible = false;
            Table2.Visible = false;
            Button2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Register register = 
                InOut.ReadTxt(Server.MapPath("App_Data/U3.txt"));

            Table1Header();
            FillTable1(register);
            Button1.Visible = false;
            Table1.Visible = true;
            Button2.Visible = true;

            List<Road> road = TaskUtils.Travel(register.start, 
                register.ending, register, 0);

            File.Delete(Server.MapPath("Rezultatai.txt"));
            InOut.PrintCitiesTxt(Server.MapPath("Rezultatai.txt"), register);
            InOut.PrintRoadsTxt(Server.MapPath("Rezultatai.txt"), register);
            InOut.PrintRezultTxt(Server.MapPath("Rezultatai.txt"), road);

            Session["keliai"] = road;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button1.Visible = false;

            List<Road> path = (List<Road>)Session["keliai"];

            if (path.Count == 0)
            {
                TableCell cell = new TableCell();
                cell.Text = "Nėra trumpiausio kelio";

                TableRow row = new TableRow();
                row.Cells.Add(cell);

                Table2.Rows.Add(row);
            }
            else
            {
                Table2Header();
                FillTable2(path);
                Table2.Visible = true;
            }
        }

        /// <summary>
        /// Makes a table1 header
        /// </summary>
        private void Table1Header()
        {
            TableCell cell = new TableCell();
            cell.Text = "Duomenys";
            TableCell cellOne = new TableCell();
            cellOne.Text = "Miestai";
            TableCell cellTwo = new TableCell();
            cellTwo.Text = "Keliai";

            TableRow rowZero = new TableRow();
            rowZero.Cells.Add(cell);
            TableRow row = new TableRow();
            row.Cells.Add(cellOne);
            row.Cells.Add(cellTwo);

            Table1.Rows.Add(rowZero);
            Table1.Rows.Add(row);
        }

        /// <summary>
        /// Makes a table2 header
        /// </summary>
        private void Table2Header()
        {
            TableCell cellOne = new TableCell();
            cellOne.Text = "Rezultatai";

            TableRow row = new TableRow();
            row.Cells.Add(cellOne);

            Table2.Rows.Add(row);
        }

        /// <summary>
        /// Fills table1 with given parameters
        /// </summary>
        /// <param name="register"></param>
        private void FillTable1(Register register)
        {
            TableCell cellOne = new TableCell();
            cellOne = FillCellOne(register);

            TableCell cellTwo = new TableCell();
            cellTwo = FillCellTwo(register);

            TableRow row = new TableRow();
            row.Cells.Add(cellOne);
            row.Cells.Add(cellTwo);

            Table1.Rows.Add(row);
        }

        /// <summary>
        /// Table1 cell is filled with cities
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private TableCell FillCellOne(Register register)
        {
            TableCell cellOne = new TableCell();

            foreach (string city in register.GetCities())
            {
                cellOne.Text += city + "<br />";
            }

            return cellOne;
        }

        /// <summary>
        /// Table1 second cell is filled with posible roads
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private TableCell FillCellTwo(Register register)
        {
            TableCell cellTwo = new TableCell();

            foreach (Road road in register.GetRoads())
            {
                cellTwo.Text += road.Start + " -> "
                    + road.End + " " + road.Distance + " km" + "<br />";
            }

            return cellTwo;
        }

        /// <summary>
        /// Fills table2 with calculated results
        /// </summary>
        /// <param name="path"></param>
        private void FillTable2(List<Road> path)
        {
            TableCell cellOne = new TableCell();
            cellOne = FillCellOne2(path);

            TableRow row = new TableRow();
            row.Cells.Add(cellOne);

            Table2.Rows.Add(row);
        }

        /// <summary>
        /// Table2 cell is filled with results
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private TableCell FillCellOne2(List<Road> path)
        {
            TableCell cellOne = new TableCell();

            cellOne.Text = "Minimalus atstumas tarp vietovių" + "<br />";
            cellOne.Text += path[0].Start + " ir " 
                + path[path.Count - 1].End + " " 
                + TaskUtils.Distance(path) + " km" + "<br />";
            cellOne.Text += "Trasa eina per vietoves:" + "<br />";

            foreach(Road kelias in path)
            {
                cellOne.Text += kelias.Start + "<br />";
            }

            cellOne.Text += path[path.Count - 1].End;

            return cellOne;
        }
    }
}