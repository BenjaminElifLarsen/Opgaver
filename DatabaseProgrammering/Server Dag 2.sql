--backup database WideWorldImporters to disk='C:\Documents\DBBackups\backup.bak' with init;

--alter database WideWorldImporters SET RECOVERY FULL
--backup database WideWorldImporters to disk='C:\test\backup.bak'
--backup log WideWorldImporters to disk='C:\test\WWI.trn' --database recovery mode is simple and cannot do the log backup
--backup database WideWorldImporters to disk='C:\test\backup2.bak' with differential
--backup log WideWorldImporters to disk='C:\test\WWI2.trn';

--backup database WideWorldImporters To Disk = 'C:\test\backup - mirror (1).bak',
--Disk = 'C:\test\backup - mirror (2).bak',
--Disk = 'C:\test\backup - mirror (3).bak'

/*
Backup database WideWorldImporters To Disk ='C:\test\original.bak'
Mirror To disk ='C:\test\mirror.bak';
*/

Restore database WideWorldImporters from disk = 'C:\test\backup.bak' with recovery;
Restore database WideWorldImporters from disk = 'C:\test\backup2.bak' with norecovery;

