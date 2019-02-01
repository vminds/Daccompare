# Daccompare
Compares two dacpac files and generates a resulting SQL script which can be used to deploy the changes.
This is not supported when using SQLpackage.exe hence I created this simple console app.
SQLpackage.exe only supports comparing against an actual SQL database, not another dacpac.

* https://github.com/vminds/Daccompare


features are: 
* Allows to specify 2 dacpac files and generate a deployable script from that.
* Allows to replace referenced databases variables with the actual database name.

This could be handy when you want to generate a deployment SQL file during an automated build process (CI/CD)
For example you can input an empty DB dacpac and the dacpac built and it will result in SQL deploy script.
Used when the deployment tool in place does not support dacpac based deployments (i.e. using sqlpackage.exe)

Usage: call daccompare.exe with desired parameters:

* /sourcefile=full path to source dacpac file
* /targetfile=full path to target dacpac file
* /scriptfile=full path to sql script file to generate
* /nosqlcmd optional param comment out sqlcmd section

