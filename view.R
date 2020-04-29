# 入力パラメータをファイル名とする
args <- commandArgs(trailingOnly = TRUE)
file_name <- args[1];

print(file_name)

# 先頭をタイトルとして解析
file_size_info <- read.delim(file_name, row.names = 1)

png("info.png", 640, 640)
hist(file_size_info$FileLength, breaks = 30)
dev.off()

