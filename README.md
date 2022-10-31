# INFO-F-413 Data structures & Algorithms - Karger & Fastcut

***

## Table des matières
1. [Introduction](#general-info)
2. [Technologies](#technologies)
3. [Content](#content)
4. [Program usage](#usage)
5. [Contributor](#contributor)

<a name="Introduction"></a>

### Introduction

***
This project is about finding the minimum cut of a graph. Two __randomized__ algorithms are implemented :

- Karger's contraction algorithm
- Karger-Stein FastCut

<a name="technologies"></a>

### Technologies

***
The technologies used to implement those algorithms are all .NET based. C# (.NET 6) has been used for algorithms implementation and a powershell graph file generator has been created.

<a name="content"></a>

### Content
***

This repository contains all the files of a traditional C# project as well as a PDF report for the analysis of the algorithms. For details, please refer to that report.

The program is able to perform multiple runs of both algorithms for a given amount of time and output basic statistics about the runs. The goal is simple and purely educative to illustrate theoretical concepts seen during my lectures and paper readings.

All the classes are documented with conventional XML "summary" notation.


<a name="usage"></a>

### Program usage

***
The program can be compiled with a traditional C#.NET compiler. In order to use the program, it must be followed by 4 arguments, which are respectively :

- File path to graph
- Number of seconds it should run
- Algorithm to execute (1 = Karger's Contraction ; 2 = FastCut)
- Expected answer

Example of command to run Karger's contraction algorithm on "myGraph.txt", for 60 seconds with expected mincut valuating at 3 :  
```./Assignment1 "myGraph.txt" 60 1 3```

At the end, the program outputs the number of runs, the number of successful runs, the ratio (estimated probability) and the total execution time.


<a name="contributor"></a>

### Contributor
***

This project has been made by Dudziak Thomas, ULB student.