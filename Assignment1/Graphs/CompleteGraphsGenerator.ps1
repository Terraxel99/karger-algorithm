$from = 100
$until = 100


for ($count = $from; $count -le $until; $count++) {

    $v = $count
    $e = ($v * ($v - 1)) / 2

    $str = "$v`r`n$e"

    for ($i = 0; $i -lt $e; $i++) {
        for($j = $i; $j -lt $v; $j++){
            if($i -ne $j){
                $str += "`r`n" + $i.ToString() + " " + $j.ToString();
            }
        }
    }
    
    $str | Out-File -FilePath "./graph_k{$count}_.txt"
}


