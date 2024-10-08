﻿using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProgramTests")]

class Program
{
    public static void Main(string[] args)
    {
        // Request the size of the data files from the user
        Console.Write("Enter the size of the data files in KB: ");
        if (!int.TryParse(Console.ReadLine(), out int dataSizeInKB))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        // Convert size from MB to number of double values
        int dataSize = dataSizeInKB * 1024 / sizeof(double);

        // Generate random data and write to files
        GenerateRandomDataFile("A.txt", dataSize);
        GenerateRandomDataFile("B.txt", dataSize);
        GenerateRandomDataFile("C.txt", dataSize);

        Console.WriteLine("Files A.txt, B.txt, and C.txt have been created with random data.");

        // Entering data from files
        double[] A = ReadArrayFromFile("A.txt");
        double[] B = ReadArrayFromFile("B.txt");
        double[] C = ReadArrayFromFile("C.txt");

        // Creating a Class Instance to Measure Time
        TimeMeasurement timer = new TimeMeasurement();

        // Measuring Total Execution Time
        timer.Start();
        double[] R = CalculateResultArray(A, B, C);
        timer.Stop();
        double elapsedTime = timer.GetElapsedTime();

        // Measuring CPU Time
        TimeSpan processTime = timer.GetProcessTime();
        double elapsedTimeUsingTimeGetTime = timer.GetElapsedTimeUsingTimeGetTime();

        // Output the results to a file
        WriteArrayToFile("R.txt", R);

        // Screen Time Display
        Console.WriteLine($"Elapsed Time (QueryPerformanceCounter): {elapsedTime} seconds");
        Console.WriteLine($"Elapsed Time (timeGetTime): {elapsedTimeUsingTimeGetTime} seconds");
        Console.WriteLine($"Process Time: {processTime.TotalSeconds} seconds");

        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();
        string csvFilePath = Path.Combine(currentDirectory, "results.csv");

        // Write results to a CSV file
        using (StreamWriter writer = new StreamWriter("results.csv", true))
        {
            writer.WriteLine($"{dataSizeInKB},{elapsedTime},{elapsedTimeUsingTimeGetTime},{processTime.TotalSeconds}");
        }
    }

    internal static void GenerateRandomDataFile(string fileName, int dataSize)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(fileName, false)) // Open file with overwrite mode
        {
            for (int i = 0; i < dataSize; i++)
            {
                double randomValue = random.NextDouble() * 100; // Generate a random double value between 0 and 100
                writer.WriteLine(randomValue);
            }
        }
    }

    internal static double[] ReadArrayFromFile(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        double[] array = new double[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            array[i] = double.Parse(lines[i]);
        }
        return array;
    }

    internal static void WriteArrayToFile(string fileName, double[] array)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (double value in array)
            {
                writer.WriteLine(value);
            }
        }
    }

    internal static double[] CalculateResultArray(double[] A, double[] B, double[] C)
    {
        int length = A.Length;
        double[] R = new double[length];
        double avgA = CalculateAverage(A);
        double avgB = CalculateAverage(B);
        double avgC = CalculateAverage(C);

        for (int i = 0; i < length; i++)
        {
            R[i] = A[i] * avgA + B[i] * avgB + C[i] * avgC;
        }

        return R;
    }

    internal static double CalculateAverage(double[] array)
    {
        double sum = 0;
        foreach (double value in array)
        {
            sum += value;
        }
        return sum / array.Length;
    }
}

