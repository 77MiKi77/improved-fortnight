# Requires ImageMagick. Regenerates transparent independent action sprites from the generated sheets.
$ErrorActionPreference='Stop'; $out='PetRose/assets/actions'; New-Item -Force -ItemType Directory $out | Out-Null
# 1408x768 sheet: five equal character cells per row; output visual references retain the shared source scale.
$names='idle','wave','jump','look','sleep','sit','sleepy','angry','shy','cheer'
for($i=0;$i -lt 10;$i++){ $x=($i%5)*281; $y=[math]::Floor($i/5)*384; magick assets/action-reference-sheet.png -crop "281x384+$x+$y" +repage -fuzz 12% -transparent '#ff00ff' "$out/$($names[$i]).png" }
Write-Host "Action sprites written to $out"
