using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class ProgramTests
{
    [SetUp]
    public void Setup()
    {
        // Setup code if needed
    }

    [Test]
    public void TestGenerateRandomDataFile()
    {
        int dataSizeInKB = 1;
        int dataSize = dataSizeInKB * 1024 / sizeof(double);

        Program.GenerateRandomDataFile("TestA.txt", dataSize);
        Program.GenerateRandomDataFile("TestB.txt", dataSize);
        Program.GenerateRandomDataFile("TestC.txt", dataSize);

        // Verify the number of lines in each file
        int linesA = File.ReadAllLines("TestA.txt").Length;
        int linesB = File.ReadAllLines("TestB.txt").Length;
        int linesC = File.ReadAllLines("TestC.txt").Length;

        // Expected: 128 lines in each file
        Assert.AreEqual(128, linesA);
        Assert.AreEqual(128, linesB);
        Assert.AreEqual(128, linesC);
    }

    [Test]
    public void TestReadArrayFromFile()
    {
        double[] array = Program.ReadArrayFromFile("TestA.txt");

        // Expected: 128 elements in the array
        Assert.AreEqual(128, array.Length);
    }

    [Test]
    public void TestCalculateAverage()
    {
        double[] array = { 1.0, 2.0, 3.0, 4.0 };
        double average = Program.CalculateAverage(array);

        // Expected: 2.5
        Assert.AreEqual(2.5, average);
    }

    [Test]
    public void TestCalculateResultArray()
    {
        double[] A = { 1.0, 2.0, 3.0, 4.0 };
        double[] B = { 2.0, 3.0, 4.0, 5.0 };
        double[] C = { 3.0, 4.0, 5.0, 6.0 };

        double[] R = Program.CalculateResultArray(A, B, C);

        // Expected: 4 elements in the result array
        Assert.AreEqual(4, R.Length);
    }

    [Test]
    public void TestWriteArrayToFile()
    {
        double[] array = { 1.0, 2.0, 3.0, 4.0 };
        Program.WriteArrayToFile("TestR.txt", array);

        int lines = File.ReadAllLines("TestR.txt").Length;

        // Expected: 4 lines in the file
        Assert.AreEqual(4, lines);
    }
}
