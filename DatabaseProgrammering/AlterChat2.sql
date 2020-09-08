use Chat
Alter Table Message_Information
Alter Column UserID Int Null UNIQUE
Alter Column  UserName NVARCHAR(16) Not Null UNIQUE