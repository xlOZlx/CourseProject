CREATE DATABASE FantasyLibraryData;
USE FantasyLibraryData;

CREATE TABLE UserAccounts -- аккаунт
(
UserLogin nvarchar(50) primary key,
UserPassword nvarchar(50)
);

CREATE TABLE Author -- инфо об авторе
(
AuthorName nvarchar(150) primary key
);
CREATE TABLE Books -- книги
( 
BookName nvarchar(150) primary key,
AuthorName nvarchar(150),
CoverBook varbinary(max),
SeriesName nvarchar(150),
NumberInSeries int,
LinkToBook nvarchar(max),
CONSTRAINT FK_AuthorName_Author foreign key(AuthorName) references Author(AuthorName)
);

CREATE TABLE ReadBooks -- прочитанные книги
(
UserLogin nvarchar(50) primary key,
BookName nvarchar(150),
CONSTRAINT FK_ReadBooks_Users foreign key(UserLogin) references UserAccounts(UserLogin),
CONSTRAINT FK_ReadBooks_Books foreign key(BookName) references Books(BookName)
);

CREATE TABLE ReadingBooks -- в процессе чтения
(
UserLogin nvarchar(50) primary key,
BookName nvarchar(150),
CONSTRAINT FK_ReadingBooks_Users foreign key(UserLogin) references UserAccounts(UserLogin),
CONSTRAINT FK_ReadingBooks_Books foreign key(BookName) references Books(BookName)
);

CREATE TABLE BookReviews
(
BookName nvarchar(150) primary key,
UserLogin nvarchar(50),
ReviewText nvarchar(max),
CONSTRAINT FK_BookReviews_Books foreign key(BookName) references Books(BookName),
CONSTRAINT FK_BookReviews_Users foreign key(UserLogin) references UserAccounts(UserLogin)
)