/*Ravish Chawla. 130412*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jacobi
{
    class Jacobi
    {
     
        
        /*
            This method runs the entire Jacobi Algorithm. 
                  It takes in a reference to a 2 Dimensional matrix, and 
                  based on the matrix, finds an appropriate sub-matrix
                  , computes its eigenvalues, eigenvectors, and the Givvens matrix. 
                  It runs in a conditional Do-While loop that stops when OFF() of the 
                  matrix is less than 10^-9. 
          
         * return offsets -> list of values of OFF() computed throughout the iterations. 
         * args ref A -> reference to original 2D matrix. 
         *      choice -> mode in which the user wants to compute the maximum sub-matrix. 
         */

        public List<double> run(ref double[,] A, int choice)
        {

            List<double> offsets = new List<double>();
            int count = 0;
            int b_i = 0, b_j = 0;
            double b_max = 0;
            double offset;

            do
            {
   
                /*Computing the submatrix based on mode 
                 */

                b_max = 0;
                if (choice == 1)
                {
                    for (int i = 0; i < A.GetLength(0); ++i)
                    {
                        for (int j = 0; j < A.GetLength(1); ++j)
                        {


                            if (i < j && Math.Abs(A[i, j]) > b_max)
                            {

                                b_max = Math.Abs(A[i, j]);
                                b_i = i;
                                b_j = j;
                            }





                        }
                    }
                }
                else if (choice == 2)
                {


                    if (b_i < (A.GetLength(0)-1))
                        b_i++;
                    else if (b_j < (A.GetLength(1)-1))
                    {
                        b_i = 0;
                        b_j++;
                    }
                    if (b_i == b_j)
                        b_i++;

                    if ((b_i >= (A.GetLength(0) - 1)) && (b_j >= (A.GetLength(1)) - 1))
                    {
                        b_i = 1;
                        b_j = 0;
                    }

                    

                }


                        
                


                double[,] B = new double[2, 2];
                B[0, 0] = A[b_i, b_i];
                B[0, 1] = B[1, 0] = A[b_i, b_j];
                B[1, 1] = A[b_j, b_j];


                /*Perform the remaining operations on the submatrix. 
                 */

                double[] eigens = calculateEigens(B);

                double[,] eigenVecs = calculateEigenVecs(B, eigens);

                double[,] G = promote(eigenVecs, b_i, b_j, A.GetLength(0));

                double[,] G1 = getTranspose(G);

                A = multiply(multiply(G1, A), G);


           
                
                
                
               //Console.In.Read();
                offset = off(A);
                offsets.Add(offset);
            }
            while (offset > Math.Pow(10, -9));// && ++count < 10000);

            return offsets;
         

        }

        /*This method retururns the Eigenvalues of a given 2x2 matrix. 
                It uses pre-defined formulas to compute the values, 
                and returns an array with the respective values
         
         *args B -> submatrix to compute Eigenvalues for 
         *return sol -> array with EigenValues
         
         */
        double[] calculateEigens(double[,] B)
        {
            double a = B[0, 0];
            double b = B[0, 1];
            double d = B[1, 1];

            double one = (a + d) / 2 + Math.Sqrt((b * b) + Math.Pow((a - d) / 2, 2));
            double two = (a + d) / 2 - Math.Sqrt((b * b) + Math.Pow((a - d) / 2, 2));

            double[] sol = { one, two };

            return sol;

        }

        /*
         This method returns the Eivenvectors of a given 2x2 matrix and 
                its eigenvalues. It uses predefined formulas to 
                compute the values and return a 2x2 column-based array with 
                the values of the matrix. 
          
          
         
         * args B -> 2d matrix for which EigenVectors need to be computed
         *      eigens -> array containing the Eigenvalues of the same matrix
         * return songs -> column-based array with the Orthagonal Eigenvectors
         */

        double[,] calculateEigenVecs(double[,] B, double[] eigens)
        {

            double a = B[0, 0];
            double b = B[0, 1];
            double d = B[1, 1];
            double[,] songs = new double[2, 2];
            int i = 0;

            if(b == 0)
            {
                songs[0,0] = songs[1,1] = 1;
                songs[0, 1] = songs[1, 0] = 0;
                    return songs;
            }
        
            double[,] bass = {{a-eigens[0],b},{b,d-eigens[0]}};

            //double[] rone = {-b,bass[0,0]};
            songs[0,0] = -b;
            songs[1,0] = bass[0,0];
            double mag = Math.Sqrt((songs[1,0]*songs[1,0]) + (songs[0,0]*songs[0,0]));
            songs[0,0] /= mag;
            songs[1,0] /= mag;

            songs[0,1] = -songs[1,0];
            songs[1,1] = songs[0,0];

            return songs;
        }


        /*
         * This method promotes a 2x2 matrix to a different dimension. 
                Given the values to substitute, and new dimension, 
                it creates an identity matrix and replaces the chosen 
                values with the arguments. 
         * 
         * args vert -> the matrix containing the values to substitue
         *      b_i -> the ith value that denotes the row/column to substitute
         *      b_j -> the jth value that dentoes the row/column to substitute
         *      dim -> the dimension to promote the matrix to 
         *return identity -> The identity matrix with substituted values
         */

        double[,] promote(double[,] vert, int b_i, int b_j, int dim)
        {

            double[,] identity = getIdentity(dim);

            identity[b_i, b_i] = vert[0, 0];
            identity[b_i, b_j] = vert[0, 1];
            identity[b_j, b_i] = vert[1, 0];
            identity[b_j, b_j] = vert[1, 1];

            return identity;

            
        }
        
        /*
         * This method returns a (dim x dim) identity matrix
         
         * args dim -> the dimension to get identity matrix of
         * return mouse -> the identity matrix
         * */

        double[,] getIdentity(int dim)
        {
            double[,] mouse = new double[dim, dim];

            for (int i = 0; i < dim; ++i)
            {
                for (int j = 0; j < dim; ++j)
                {
                    if (i == j)
                        mouse[i, j] = 1;
                    else
                        mouse[i, j] = 0;
                }
            }

            return mouse;
        }

        double[,] getTranspose(double[,] nul)
        {

            double[,] result = new double[nul.GetLength(0), nul.GetLength(1)];
            for (int i = 0; i < nul.GetLength(0); i++)
                for (int j = 0; j < nul.GetLength(1); j++)
                    result[j, i] = nul[i, j];

            return result;


        }

        double[,] multiply(double[,] one, double[,] two)
        {

            int m = one.GetLength(0);
            int n = one.GetLength(1);
            int p = two.GetLength(0);
            int q = two.GetLength(1);
            double sum = 0;
            double[,] multiply = new double[m, q];
            for (short c = 0; c < m; c++)
            {
                for (short d = 0; d < q; d++)
                {
                    for (short k = 0; k < p; k++)
                    {
                        sum = sum + one[c, k] * two[k, d];
                    }

                    multiply[c, d] = sum;
                    sum = 0;
                }
            }


            return multiply;

        }

      public  double off(double[,] wut)
        {
            double why = 0;
            for (int i = 0; i < wut.GetLength(0); i++)
            {
                for (int j = 0; j < wut.GetLength(1); j++)
                {
                   if(i!=j)
                        why += (wut[i, j] * wut[i, j]);
                }
            }

            return why;
        }

    }
}

























    


    

