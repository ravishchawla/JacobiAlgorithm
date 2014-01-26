Jacobi Algorithm
================

Algorithm
---------
The Jacobi Algorithm is a Linear Algebra algorithm that is used to calculate the EigenVectors and EigenValues of a matrix. 
The algorithms is recursive, and uses the results of a previous test as part of the computation for the next one. Due tot he recursive nature of the algorithm, the algorithm can run into an infinite loop. However, for most matrixes, a 
result is fairly easy to compute using the algorithm. 

Project
------
The Project was implemented in C#, which does not contain native support for Matrixes. To implement the Jacobi Algorithm, I segregated the program into different functions, that each perform differnet matrix operations, such as multiplication and identity. The algorithm itself uses these functions to run the Jacobi Algorithm until completion. 

The Algorithm implemented can itself take in any matrix as a parameter. Howevever, as part of the project, I ran preset and randomized tests on the algorithm. After each run, the results are plotted into an Excel Spreadseet, so that the data collected can be visualized in a graphical view. 

To run the project, esecute the executable file. The results are plotted in Report.xlsx.

Project by Ravish Chawla
