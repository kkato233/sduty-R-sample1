## dotnet で データ解析した後で R のグラフを作成する

```
dotnet new console
```

データを解析して テキストファイルにするプログラムを書く
```
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
```

```
dotnet run > file_size_info.tsv
Rscript view.R file_size_info.tsv
```

これでこのような画像が生成される

![info.png](info.png)

### うまくいかない場合


パスが通ってなくて Rscript が起動できない場合
Windows の場合
```
set PATH="C:\Program Files\R\R-4.0.0\bin";%PATH%
```

git bash の場合
```
export PATH='/c/Program Files/R/R-4.0.0/bin':$PATH
```


