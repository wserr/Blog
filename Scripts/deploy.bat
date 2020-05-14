echo "Blog Service is stopping..."
plink -ssh pi@192.168.0.2 -t sudo systemctl stop Blog.service
echo "Blog Service stopped"
echo "Copying files to destination"
pscp -r ..\bin\Release\netcoreapp3.1\linux-arm\publish\* pi@192.168.0.2:/home/pi/Blog/
echo "Files copied"
echo "Starting Blog Service..."
plink -ssh pi@192.168.0.2 -t sudo systemctl start Blog.service
echo "Blog Service started"
pause