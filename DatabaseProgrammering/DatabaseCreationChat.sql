--Create Database Chat

Use ChatTest


Create Table User_Information
(UserID Int Not Null Primary Key Identity(1,1), UserName NVARCHAR(16) Not Null Unique,
 UserPassword NVARCHAR(24) Not null, Admin_level Int null);

 Create Table Message_Information
(MessageID Int Not Null Primary Key IDENTITY(1,1), UserID Int Constraint FK_user_message FOREIGN KEY REFERENCES User_Information(UserID) On Delete Cascade On Update Cascade,
 UserName NVARCHAR(16) Not Null FOREIGN KEY REFERENCES User_Information(UserName), 
 Message NVARCHAR(255) Not Null, Time NVARCHAR(255) Not Null);

 Insert Into User_Information(UserName,UserPassword,Admin_level)
 Values('Benjamin','Test123.',9)
