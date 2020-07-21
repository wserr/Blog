#echo "Deleting bin folder..."
#rm -r ../../bin
#echo "Deleting obj folder..."
#rm -r ../../obj
echo "Build project..."
dotnet publish -c Release -r linux-arm ../../Blog.csproj
echo "Stop website on remote machine..."
ssh pi@192.168.0.2 -t "sudo systemctl stop Blog.service && logout"

echo "Sync release files to destination..."
rsync -a -v ../../bin/Release/netcoreapp3.1/linux-arm/publish/ pi@192.168.0.2:/home/pi/Blog/

echo "Start website on remote machine..."
ssh pi@192.168.0.2 -t "sudo systemctl start Blog.service && logout"

echo "Completed"

