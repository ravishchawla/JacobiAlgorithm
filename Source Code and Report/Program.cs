/*Ravish Chawla. 130412*/
using System;
using System.Data;
using System.Drawing;

using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace Jacobi
{
    class Program
    {

        /*The Main method executes the Jacobi Algorithm, after
                computing a random 5x5 matrix. It runs the 
                algorithm with different modes, setting the data
                in the Excel file after each. It also prompts
                the user after each run to try a different matrix, or
                exit. 
         */
         

        static int choice = 1, mode = 1;
        static void Main(string[] args)
        {

            Jacobi jacobi = new Jacobi();
                         
            while (choice == 1)
            {
                List<double> offsets = new List<double>();
                List<double> _offsets = new List<double>();
                try
                {

                    String time = DateTime.Now.TimeOfDay.TotalMinutes.ToString();
                    Console.Out.WriteLine("Current Time of Day is " + DateTime.Now.TimeOfDay);

                    Console.Out.WriteLine("\n<<<Please close the Excel Report before running the Program>>>");


                    




                    double[,] matrix = new double[5, 5];
                    Random gen = new Random();
                    Console.Out.Write("\n\t");
                    for (byte i = 0; i < matrix.GetLength(0); ++i)
                    {
                        for (byte j = 0; j < matrix.GetLength(1); ++j)
                        {
                            matrix[i, j] = matrix[j, i] = gen.NextDouble() * 100;
                            

                        }


                       
                    }


                  //  double[,] matrix = { { 2, 1, 1 }, { 1, 2, 1 }, { 1, 1, 2 } };

                    

                    double[,] originalMatrix = matrix;
                    offsets.Add(jacobi.off(matrix));
                    _offsets.Add(jacobi.off(matrix));
                    offsets.AddRange(jacobi.run(ref matrix, 1));
                    

                    Console.Out.WriteLine("\n\n");

                    




                    
                    generateReport(offsets, originalMatrix, matrix, 1);

                    matrix = originalMatrix;
                    _offsets.AddRange(jacobi.run(ref matrix, 2));
                    generateReport(_offsets, originalMatrix, matrix, 2);
                    Console.Out.WriteLine("\n\nNumber of Iterations: \n\tWith Sorting: " + offsets.Count + "\n\tWithout Sorting: " + _offsets.Count);
                    System.Diagnostics.Process.Start("Report.xlsx");
                    

                    Console.Out.WriteLine("\n01: Run Again\n02: Exit");
                    choice = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    


                }

                catch (IOException)
                {   
                    Console.Clear();
                    Console.Out.WriteLine("File was Open\n\tExit and Press Enter to Continue..");
                    Console.ReadLine();
                }


            }
            

            

        }

        /*This method generates the Excel report with the correct data
                for each iteration of the Jacobi algorithm. 
                It sets the functioning column with the data from the Offsets, 
                and writes the original and resulting diagonal matrices on the Spreadsheet
                as well.
         
         *args data -> List containg values of OFF()
         *     original -> the Original matrix
         *     result -> the Resulting Diagonal matrix
         *     workSheet -> which Worksheet to modify in the spreadsheet. 
         */

        public static void generateReport(List<double> data, double[,] original, double[,] result, int workSheet)
        {

            FileInfo newFile = new FileInfo("Report.xlsx");
            ExcelPackage pack = new ExcelPackage(newFile);
            ExcelWorksheet ws = pack.Workbook.Worksheets[workSheet];        
            
            int cellCount = 2;
            for(int i = 0; i < 100; i++)
            {
                String cell = "B" + cellCount;



                if (i < data.Count)
                {
                    ws.Row(cellCount).Hidden = false;
                    ws.Cells[cell].Value = data.ElementAt(i);
                }
                else
                    ws.Row(cellCount).Hidden = true;

                

                cellCount++;

            }
    
            /*From F105 to J109*/
            int o = 0;
            for (char c = 'V'; c <= 'Z'; c++)
            {
                int p = 0;
                
                for (int i = 3; i < 8; i++)
                {
                    String cell = c.ToString() + i;
                    ws.Cells[cell].Value = original[o, p];
                    p++;
                }
                o++;
            }

            o = 0;
            for (char c = 'V'; c <= 'Z'; c++)
            {
                int p = 0;
                
                for (int i = 13; i < 18; i++)
                {
                    String cell = c.ToString() + i;
                    ws.Cells[cell].Value = result[o, p];
                    if (o == p)
                        ws.Cells[c.ToString() + 22].Value = result[o, p];
                    p++;
                }
                o++;
            }
          //  ws.Cells["J12"].Value = "testing yo";


           // Console.Out.WriteLine("saving");
            
            pack.Save();
            
                



        }






    }
}
