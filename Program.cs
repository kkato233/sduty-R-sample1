using System;
using System.IO;

namespace read_ronbun
{
    class Program
    {
        static void Main(string[] args)
        {
            // 指定ディレクトリにあるファイルのサイズ一覧
            string dir = @"c:\work";
            if (args.Length > 0 && Directory.Exists(args[0])) {
                // パラメータでディレクトリ指定可能
                dir = args[0];
            }
            Console.WriteLine("FileName\tFileLength");
            foreach(var file in Directory.GetFiles(dir, "*.txt")) {
                string fileName = Path.GetFileName(file);
                FileInfo f = new FileInfo(file);
                Console.WriteLine($"{fileName}\t{f.Length}");
            }
        }
    }
}
