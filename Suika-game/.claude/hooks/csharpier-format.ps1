$input_json = $input | Out-String
try {
    $parsed = $input_json | ConvertFrom-Json
    $file = $parsed.tool_input.file_path
    if ($file -and $file.EndsWith(".cs")) {
        dotnet csharpier format "$file"
    }
} catch {
    # 입력이 JSON이 아니거나 파일 경로가 없으면 조용히 종료
}