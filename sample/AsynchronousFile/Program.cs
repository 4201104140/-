﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public async Task SimpleWriteAsync()
        {
            string filePath = "simple.txt";
            string text = $"Hello World";

            await File.WriteAllTextAsync(filePath, text);
        }

        public async Task ProcessWriteAsync()
        {
            string filePath = "temp.txt";
            string text = $"Hello World{Environment.NewLine}";

            await WriteTextAsync(filePath, text);
        }

        async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using var sourceStream =
                new FileStream(
                    filePath,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true);

            await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }

        public async Task SimpleReadAsync()
        {
            string filePath = "simple.txt";
            string text = await File.ReadAllTextAsync(filePath);

            Console.WriteLine(text);
        }

        public async Task ProcessReadAsync()
        {
            try
            {
                string filePath = "temp.txt";
                if (File.Exists(filePath) != false)
                {
                    string text = await ReadTextAsync(filePath);
                    Console.WriteLine(text);
                }
                else
                {
                    Console.WriteLine($"file not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task<string> ReadTextAsync(string filePath)
        {
            using var sourceStream =
                new FileStream(
                    filePath,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    bufferSize: 4096, useAsync: true);

            var sb = new StringBuilder();

            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }

        public async Task SimpleParallelWriteAsync()
        {
            string folder = Directory.CreateDirectory("tempfolder").Name;
            IList<Task> writeTaskList = new List<Task>();

            for (int index = 11; index <= 20; ++index)
            {
                string fileName = $"file-{index:00}.txt";
                string filePath = $"{folder}/{fileName}";
                string text = $"In file {index}{Environment.NewLine}";

                writeTaskList.Add(File.WriteAllTextAsync(filePath, text));
            }

            await Task.WhenAll(writeTaskList);
        }
    }
}
