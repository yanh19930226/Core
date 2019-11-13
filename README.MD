1.关于core程序在IIS上的发布
   安装.netcoreHosting和SDK：https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.6-windows-hosting-bundle-installer（这个只是hosting）
 2.将core程序在centos上发布
  安装vm、  安装centos最小版本、下载putty
  centos最小版手动安装网卡：1.cd  /etc/sysconfg 2.cd  /network-scripts/3.编辑文件：vi ifcig-ens33 将onboot改为yes 4.重启网卡：service network restart
    5.ifconfig找不到 sudo yum install net-tools
    service sshd status 查看ssh状态
   安装Nginx:  https://www.tecmint.com/install-nginx-on-centos-7/
   yum install epel-release
   yum install nginx 
   start nginx 
   firewall-cmd --zone=public --permanent --add-service=http
   firewall-cmd --zone=public --permanent --add-service=https
   firewall-cmd --reload
   给centos安装core的运行环境
   https://dotnet.microsoft.com/download/linux-package-manager/centos/sdk-current


   测试：在centos上新建文件夹mkdir netcore,在netcore文件夹下新建hellowcore文件夹，在hellowcore文件夹下新建webapi的项目dotnet new webapi运行
            使用新的putty客户端连接webapi获取值测试curl http://localhost:5000/api/values
   

使用ftp上传发布的网站到centos
使用puuty测试返回的网站的html代码：curl http://localhost:5000


nginx的配置：
 1.cd /etc/nginx/ 2.修改nginx配置文件 vi 
 
 相关文章：https://www.cnblogs.com/ibeisha/p/centos-aspnet-supervisor.html





