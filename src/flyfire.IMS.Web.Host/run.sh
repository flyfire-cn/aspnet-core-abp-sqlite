cd $(dirname $0)
pwd
nohup dotnet ./flyfire.IMS.Web.Host.dll 2>>error.log 1>/dev/null &

