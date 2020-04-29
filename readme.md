## dotnet で データを前処理した後で R でグラフを作る
dotnet と R の得意分野を組み合わせてデータを解析します。

dotnet : フォルダの中から ファイルを探してそのファイルサイズを取得
R : ヒストグラムを表示

`R.NET` を使って dotnet から 直接 R を実行します。

### 手順

```
dotnet new console
dotnet add package R.NET
```

dotnet の コード （指定のフォルダにあるファイルのサイズを求める）
```
using RDotNet;

static void Main(string[] args)
{
    // R 実行環境
    REngine engine = GetREngineInstance();

    // 指定ディレクトリにあるファイルのサイズ一覧
    string dir = @"c:\work";
    if (args.Length > 0 && Directory.Exists(args[0])) {
        // パラメータでディレクトリ指定可能
        dir = args[0];
    }
    var sw = new StreamWriter("file_size_list.tsv");

    sw.WriteLine("FileName\tFileLength");
    foreach(var file in Directory.GetFiles(dir, "*.txt")) {
        string fileName = Path.GetFileName(file);
        FileInfo f = new FileInfo(file);
        sw.WriteLine($"{fileName}\t{f.Length}");
    }
    sw.Flush();
    
    // R スクリプトを実行する
    engine.Evaluate(@"
file_size_info <- read.delim(""file_size_list.tsv"", row.names = 1);

png(""info.png"", 640, 640)
hist(file_size_info$FileLength, breaks = 30)
dev.off()
");

}


static REngine GetREngineInstance() {
    // R.dll のある ディレクトリを設定する
    if (Environment.Is64BitProcess) {
        REngine.SetEnvironmentVariables(@"C:\Program Files\R\R-4.0.0\bin\x64");
    } else {
        REngine.SetEnvironmentVariables(@"C:\Program Files\R\R-4.0.0\bin\i386");
    }
    REngine engine = REngine.GetInstance();
    return engine;
}
```

コマンドプロンプトで 以下を実施
```
dotnet run 
```

これでこのような画像が生成される

![info.png](info.png)

### うまくいかない場合

プログラム中の
```
REngine.SetEnvironmentVariables(@"C:\Program Files\R\R-4.0.0\bin\x64");
```
の部分のパスを R.dll の存在するパスになっているか確認する。
