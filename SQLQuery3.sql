Create Table UserProfile  
    (  
        UserId int primary key identity(1, 1),  
        UserName varchar(50),  
        Password varchar(50),  
        IsActive bit  
    )  

	Insert into UserProfile  
Select 'ishit', 'ishit1234', 1 Union All  
Select 'ayush', 'ayush1234', 1 Union All  
Select 'ruben', 'ruben1234', 1 

select * From UserProfile